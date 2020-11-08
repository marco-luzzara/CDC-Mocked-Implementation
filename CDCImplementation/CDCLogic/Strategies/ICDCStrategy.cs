using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies
{
    public interface ICDCStrategy<TState>
    {
        (IEnumerable<ObjWithState<TObject>>, TState) GetFreshRows<TObject>(IEnumerable<TObject> rows, TState currentState);
    }
}
