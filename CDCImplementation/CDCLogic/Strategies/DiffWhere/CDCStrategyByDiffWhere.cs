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
            where TObject : class
        {
            if (currentState is null || currentState.HashedRows is null)
                currentState = new DiffWhereState(new Dictionary<string, string>());

            var diffWhereRows = rows.Cast<IDiffWhereCompatible>();
            HashAlgorithm sha = SHA256.Create();
            var currentHashDict = diffWhereRows.ToDictionary(x => x.GetKeyHash(sha), x => (x.GetNonKeyHash(sha), (TObject) x, false));

            var hashDictionaryFromDL = currentState.HashedRows;

            var freshRowsWithState = InternalGetFreshRows(currentHashDict, hashDictionaryFromDL);
            return (freshRowsWithState.Item1.Cast<ObjWithState<TObject>>(), freshRowsWithState.Item2);
        }

        // optimization: currentHashDict has item3 = bool. this value specifies if the key already exists in the hashDictionaryFromDL.
        // to find the inserts, instead of doing a difference between dictionaries, I take only those tuples with item3 = false.
        protected (IEnumerable<ObjWithState<TObject>>, DiffWhereState) InternalGetFreshRows<TObject>(
            Dictionary<string, (string, TObject, bool)> currentHashDict,
            IDictionary<string, string> hashDictionaryFromDL)
            where TObject : class
        {
            var now = DateTimeOffset.Now;
            var freshRows = new List<ObjWithState<TObject>>();
            foreach (var pair in hashDictionaryFromDL)
            {
                if (!currentHashDict.ContainsKey(pair.Key))
                {
                    // deletion
                    freshRows.Add(new ObjCDCDiffWhere<TObject>(null, ObjectState.Deleted, now)
                    {
                        KeyHash = pair.Key,
                        NonKeyHash = pair.Value
                    });
                }
                else
                {
                    if (currentHashDict[pair.Key].Item1 != pair.Value)
                    {
                        // updation
                        freshRows.Add(new ObjCDCDiffWhere<TObject>(currentHashDict[pair.Key].Item2, ObjectState.Updated, now)
                        {
                            KeyHash = pair.Key,
                            NonKeyHash = pair.Value
                        });
                    }
                    // else they did not change meanwhile

                    // exclude from insertion
                    currentHashDict[pair.Key] = (currentHashDict[pair.Key].Item1, currentHashDict[pair.Key].Item2, true);
                }
            }

            // insertion
            var insertedRows = currentHashDict
                .Where(x => !x.Value.Item3)
                .Select(x => new ObjCDCDiffWhere<TObject>(x.Value.Item2, ObjectState.Inserted, now)
                { 
                    KeyHash = x.Key,
                    NonKeyHash = x.Value.Item1
                });
            freshRows.AddRange(insertedRows);

            return (freshRows, new DiffWhereState(currentHashDict.ToDictionary(x => x.Key, x => x.Value.Item1)));
        }
    }
}
