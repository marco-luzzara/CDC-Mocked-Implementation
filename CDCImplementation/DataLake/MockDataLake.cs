using CDCImplementation.CDCLogic;
using CDCImplementation.Infrastructure.FileSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace CDCImplementation.DataLake
{
    /**
     * File structure
     * BaseFolder/
     *     sourceId/
     *         sync.json
     *         data/
     *             20200804_122312_{idRunner}.tmp|json
     */ 
    public class MockDataLake : AbstractDataLake
    {
        public string BaseFolder { get; }

        protected readonly string SYNC_FILENAME = "sync";
        protected readonly string DATA_DIRNAME = "data";
        protected readonly string FILETMP_EXTENSION = "tmp";
        protected readonly string FILE_EXTENSION = "json";

        public MockDataLake(string baseFolder)
        {
            this.BaseFolder = baseFolder;

            // does nothing if already exists
            Directory.CreateDirectory(this.BaseFolder);
        }

        // considered atomic for move and rename pattern
        public override void CommitCDC(string sourceId)
        {
            string sourceIdDirPath = BuildSourceIdDirPath(sourceId);
            DirectoryUtils.RenameFilesByExt(sourceIdDirPath, FILETMP_EXTENSION, FILE_EXTENSION, true);
        }

        public override TState GetCurrentState<TState>(string sourceId, string partitionId)
        {
            string syncFilePath = BuildSyncPath(sourceId, partitionId);

            if (!File.Exists(syncFilePath))
                return null;

            string syncFileContent = File.ReadAllText(syncFilePath);

            return JsonConvert.DeserializeObject<TState>(syncFileContent);
        }

        public override void InitializeCDC(string sourceId)
        {
            string sourceIdFolder = Path.Join(this.BaseFolder, sourceId);

            Directory.CreateDirectory(sourceIdFolder);
            int tmpFiles = DirectoryUtils.DeleteFilesByExt(sourceIdFolder, FILETMP_EXTENSION);
            // if (tmpFiles > 0) last cdcCycle failed
        }

        public override void InsertFreshRows<TObject>(IEnumerable<ObjWithState<TObject>> freshRows, string sourceId, string partitionId)
        {
            string dataDirpath = BuildDataDirPath(sourceId);
            Directory.CreateDirectory(dataDirpath);

            string freshRowsFilePath = BuildDataFilePath(sourceId, partitionId);
            string freshRowsFileContent = JsonConvert.SerializeObject(freshRows);

            File.WriteAllText(freshRowsFilePath, freshRowsFileContent);
        }

        public override IEnumerable<TObject> ReadAll<TObject>(string sourceId)
        {
            throw new NotImplementedException();
        }

        // creates a temporary sync file
        public override void SetCurrentState<TState>(TState newState, string sourceId, string partitionId)
        {
            string syncFilePath = BuildSyncPath(sourceId, partitionId);
            string syncFileContent = JsonConvert.SerializeObject(newState);

            File.WriteAllText(syncFilePath, syncFileContent);
        }

        protected string BuildSourceIdDirPath(string sourceId)
        {
            return Path.Join(this.BaseFolder, sourceId);
        }

        protected string BuildDataDirPath(string sourceId)
        {
            return Path.Join(BuildSourceIdDirPath(sourceId), DATA_DIRNAME);
        }

        protected string BuildSyncPath(string sourceId, string partitionId)
        {
            return Path.Join(BuildSourceIdDirPath(sourceId), $"{SYNC_FILENAME}_{partitionId}.{FILETMP_EXTENSION}");
        }

        protected string BuildDataFilePath(string sourceId, string partitionId)
        {
            return Path.Join(BuildDataDirPath(sourceId), $"{DateTimeOffset.Now:yyyyMMdd_HHmmssfff}_{partitionId}.{FILETMP_EXTENSION}");
        }
    }
}
