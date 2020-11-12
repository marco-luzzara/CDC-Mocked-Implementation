using CDCImplementation.CDCLogic.Strategies;
using CDCImplementation.DataLake;
using CDCImplementation.DataRetrieval;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CDCImplementation.CDCLogic
{
    public class CDCRunnerContext<TObject, TState> 
        where TState : class
        where TObject : class
    {
        protected readonly CDCRunnerContextConfig configs;

        public CDCRunnerContext(CDCRunnerContextConfig configs)
        {
            this.configs = configs;
        }

        public void StartCDC()
        {
            // for move and rename pattern: clean tmp files
            this.configs.DataLake.InitializeCDC(this.configs.DataSourceId);

            using CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions po = new ParallelOptions
            {
                CancellationToken = cts.Token
            };

            Parallel.ForEach(this.configs.DataSourcePartitions, po, (ds, pls, i) =>
            {
                // not implemented, but potentially runner.StartCDC tasks could be executed remotely
                try
                {
                    var cdcRunner = new CDCRunner();
                    cdcRunner.StartCDC(new CDCRunner.CDCConfig<TObject, TState>()
                    {
                        CdcStrategy = this.configs.CdcStrategy,
                        DataLake = this.configs.DataLake,
                        DataSource = ds,
                        SourceId = this.configs.DataSourceId,
                        PartitionId = i.ToString()
                    });
                }
                catch (Exception exc)
                {
                    cts.Cancel();
                    throw exc;
                }
            });

            // rename tmp files
            this.configs.DataLake.CommitCDC(this.configs.DataSourceId);
        }

        public class CDCRunnerContextConfig
        {
            public ICDCStrategy<TState> CdcStrategy { get; set; }
            public AbstractDataLake DataLake { get; set; }
            public string DataSourceId { get; set; }
            public IEnumerable<IObjectStorage<TObject>> DataSourcePartitions { get; set; }
        }
    }
}
