using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.DiffWhere
{
    public class CDCStrategyByDiffWhere : ICDCStrategy<DiffWhereState>
    {
        public (IEnumerable<ObjWithState<TObject>>, DiffWhereState) GetFreshRows<TObject>(IEnumerable<TObject> rows, DiffWhereState currentState)
        {
            throw new NotImplementedException();
        }
    }
}
