using CDCImplementation.DataLake.StoredObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.DiffWhere
{
    public class CDCStrategyByDiffWhere : ICDCStrategy<DiffWhereState>
    {
        public (IEnumerable<ObjWithState<TObject>>, DiffWhereState) GetFreshRows<TObject>(IEnumerable<TObject> rows, DiffWhereState currentState)
        {
            var diffWhereRows = rows.Cast<IDiffWhereCompatible>();
            HashAlgorithm sha = SHA256.Create();
            var currentHashDict = diffWhereRows.ToDictionary(x => x.GetKeyHash(sha), x => (x.GetNonKeyHash(sha), x, false));

            var hashDictionaryFromDL = currentState.HashedRows;

            var freshRowsWithState = InternalGetFreshRows(currentHashDict, hashDictionaryFromDL);
            return (freshRowsWithState.Item1.Cast<ObjWithState<TObject>>(), freshRowsWithState.Item2);
        }

        public DiffWhereState JoinPartialStates(params DiffWhereState[] partialStates)
        {
            var totalDict = partialStates
                .SelectMany(s => s.HashedRows)
                .ToDictionary(x => x.Key, x => x.Value);

            return new DiffWhereState(totalDict);
        }

        // optimization: currentHashDict has item3 = bool. this value specifies if the key already exists in the hashDictionaryFromDL.
        // to find the inserts, instead of doing a difference between dictionaries, I take only those tuples with item3 = false.
        protected (IEnumerable<ObjWithState<IDiffWhereCompatible>>, DiffWhereState) InternalGetFreshRows(
            Dictionary<string, (string, IDiffWhereCompatible, bool)> currentHashDict,
            IDictionary<string, string> hashDictionaryFromDL)
        {
            var now = DateTimeOffset.Now;
            var freshRows = new List<ObjWithState<IDiffWhereCompatible>>();
            foreach (var pair in hashDictionaryFromDL)
            {
                if (!currentHashDict.ContainsKey(pair.Key))
                {
                    // deletion
                    freshRows.Add(new ObjCDCDiffWhere<IDiffWhereCompatible>(null, ObjectState.Deleted)
                    {
                        CreationTime = now,
                        KeyHash = pair.Key,
                        NonKeyHash = pair.Value
                    });
                }
                else
                {
                    if (currentHashDict[pair.Key].Item1 != pair.Value)
                    {
                        // updation
                        freshRows.Add(new ObjCDCDiffWhere<IDiffWhereCompatible>(currentHashDict[pair.Key].Item2, ObjectState.Updated)
                        {
                            CreationTime = now,
                            KeyHash = pair.Key,
                            NonKeyHash = pair.Value
                        });
                    }

                    // exclude from insertion
                    currentHashDict[pair.Key] = (currentHashDict[pair.Key].Item1, currentHashDict[pair.Key].Item2, true);
                }
            }

            // insertion
            var insertedRows = currentHashDict
                .Where(x => !x.Value.Item3)
                .Select(x => new ObjCDCDiffWhere<IDiffWhereCompatible>(x.Value.Item2, ObjectState.Inserted)
                { 
                    CreationTime = now,
                    KeyHash = x.Key,
                    NonKeyHash = x.Value.Item1
                });
            freshRows.AddRange(insertedRows);

            return (freshRows, new DiffWhereState(currentHashDict.ToDictionary(x => x.Key, x => x.Value.Item1)));
        }
    }
}
