using CDCImplementation.CDCLogic.Strategies;
using CDCImplementation.DataLake;
using CDCImplementation.DataRetrieval;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CDCImplementation.CDCLogic
{
    public class CDCRunnerContext<TObject, TState>
    {
        protected AbstractDataLake<TObject, TState> dataLake;
        protected IObjectStorage<TObject>[] dataSourcePartitions;
        protected ICDCStrategy<TState> cdcStrategy;

        public string DataSourceId { get; }

        public CDCRunnerContext(
            ICDCStrategy<TState> cdcStrategy,
            AbstractDataLake<TObject, TState> dataLake, 
            string dataSourceId, 
            params IObjectStorage<TObject>[] dataSourcePartitions)
        {
            this.cdcStrategy = cdcStrategy;
            this.dataLake = dataLake;
            this.dataSourcePartitions = dataSourcePartitions;
            this.DataSourceId = dataSourceId;
        }

        public void StartCDC()
        {
            // for move and rename pattern: clean tmp files
            this.dataLake.InitializeCDC();
            TState currentState = this.dataLake.GetCurrentState();

            using CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions po = new ParallelOptions
            {
                CancellationToken = cts.Token
            };

            ConcurrentBag<TState> partialStates = new ConcurrentBag<TState>();
            var cdcRunPrefix = DateTimeOffset.Now.ToString("yyyyMMdd_HHmmss");
            Parallel.ForEach(this.dataSourcePartitions, po, (ds, pls, i) =>
            {
                // not implemented, but potentially runner.StartCDC tasks could be executed remotely
                try
                {
                    var cdcRunner = new CDCRunner();
                    var partialState = cdcRunner.StartCDC(currentState, this.dataLake, this.cdcStrategy, ds, this.DataSourceId, $"{cdcRunPrefix}_{i}");
                    partialStates.Add(partialState);
                }
                catch (Exception exc)
                {
                    cts.Cancel();
                    throw exc;
                }
            });

            var newState = this.cdcStrategy.JoinPartialStates();
            this.dataLake.SetCurrentState(newState);

            // rename tmp files
            this.dataLake.CommitCDC();
        }
    }
}
