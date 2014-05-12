using System;
using Mono.Data.Sqlite;

namespace Assets.Scripts.Core.Editor.Sync.Drive.GoogleDrive
{
    public class GoogleDriveDetection : IDriveDetection
    {
        public string DriveName
        {
            get { return "Google Drive"; }
        }

#if UNITY_STANDALONE_WIN
        public string DrivePath
        {
            get
            {
                var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                             "\\Google\\Drive\\sync_config.db";
                var connection = new SqliteConnection("URI=file:" + @dbPath + ",version=3");
                connection.Open();
                var command = new SqliteCommand("select * from data where entry_key='local_sync_root_path'", connection);
                var reader = command.ExecuteReader();
                reader.Read();
                var path = reader["data_value"].ToString().Substring(4);
                connection.Close();
                return path;
            }
        }
#endif

#if UNITY_STANDALONE_OSX
        public string DrivePath
        {
            get
            { throw new NotImplementedException(); }
        }
#endif
    }
}