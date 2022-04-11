using CloudinaryMigration.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudinaryMigration
{
    public class SQLLiteHelper
    {
        SQLiteConnection sqlite_conn;

        public SQLLiteHelper(String sqlitedbpath) {
            
            sqlite_conn = CreateConnection(sqlitedbpath);
            CreateTable(sqlite_conn);
            //InsertData(sqlite_conn);
            //ReadData(sqlite_conn);
        }

        static SQLiteConnection CreateConnection(String sqlitedbpath)
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=" + sqlitedbpath +"; Version = 3; New = True; Compress = True; ");
         // Open the connection:
         try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }
        static void CreateTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
            //string Createsql = "DROP TABLE IF EXISTS table_name; CREATE TABLE IF NOT EXISTS app_log  (rowid INTEGER PRIMARY KEY AUTOINCREMENT, logType VARCHAR(20), desc text, Timestamp2 DATETIME)";


            //string Createsql = "DROP TABLE IF EXISTS app_log; CREATE TABLE IF NOT EXISTS app_log  (rowid INTEGER PRIMARY KEY AUTOINCREMENT, logType VARCHAR(20), desc text, Timestamp2 DATETIME)";
            string Createsql = "CREATE TABLE IF NOT EXISTS app_log  (rowid INTEGER PRIMARY KEY AUTOINCREMENT, logType VARCHAR(20), desc text, Timestamp DATETIME, filename text,jobID varchar(100))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();

            Createsql = "CREATE TABLE IF NOT EXISTS app_log_dtls  (rowid INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "inputfoldername text, filename text, cldfoldername text, isProcessed int, isError int, desc text, updatedatetime DATETIME,  Timestamp DATETIME)";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();

        }


        public void InsertDataold(String logType, String desc, String jobID, String publicID)
        {
            try
            {
                desc = desc.Replace("'", "''");
                publicID = publicID.Replace("'", "''");

                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();

                sqlite_cmd.CommandText = "INSERT INTO app_log (logType, filename,Timestamp,desc,jobID) VALUES('" + logType + "', '" + publicID + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss ") + "','" + desc +"','" + jobID +"'); ";
                sqlite_cmd.ExecuteNonQuery();

            }
            catch { 
            
            }    
        }

        public void InsertData(String logType, String desc, String jobID, String publicID)
        {
            try
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();

                //sqlite_cmd.CommandText = "INSERT INTO app_log (logType, filename,Timestamp,desc,jobID) VALUES('" + logType + "', '" + publicID + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss ") + "','" + desc + "','" + jobID + "'); ";
                sqlite_cmd.CommandText = "INSERT INTO app_log (logType, filename,Timestamp,desc,jobID) VALUES(@logType, @filename, @Timestamp,@desc,@jobID)";
                sqlite_cmd.Parameters.AddWithValue("@logType", logType);
                sqlite_cmd.Parameters.AddWithValue("@filename", publicID);
                sqlite_cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                sqlite_cmd.Parameters.AddWithValue("@desc", desc);
                sqlite_cmd.Parameters.AddWithValue("@jobID", jobID);

                sqlite_cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertDataFordbSync(String inputfoldername, String filename, String cldfoldername, int isProcessed, int isError, String desc)
        {
            try
            {

                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();

                //sqlite_cmd.CommandText = "INSERT INTO app_log (logType, filename,Timestamp,desc,jobID) VALUES('" + logType + "', '" + publicID + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss ") + "','" + desc + "','" + jobID + "'); ";
                sqlite_cmd.CommandText = "INSERT INTO app_log_dtls (inputfoldername, filename,cldfoldername,isProcessed,isError,desc,Timestamp) " +
                    "VALUES(@inputfoldername, @filename, @cldfoldername, @isProcessed, @isError, @desc, @Timestamp)";
                sqlite_cmd.Parameters.AddWithValue("@inputfoldername", inputfoldername);
                sqlite_cmd.Parameters.AddWithValue("@filename", filename);
                sqlite_cmd.Parameters.AddWithValue("@cldfoldername", cldfoldername);
                sqlite_cmd.Parameters.AddWithValue("@isProcessed", 0);
                sqlite_cmd.Parameters.AddWithValue("@isError", 0);
                sqlite_cmd.Parameters.AddWithValue("@desc", "");
                sqlite_cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                sqlite_cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Int64 getRowID(String inputfoldername,String cldfoldername,String type)
        {
            try
            {
                SQLiteCommand cmdSelect = new SQLiteCommand();

                if (type == "max")
                {
                    cmdSelect = new SQLiteCommand(
                            "SELECT max(rowid) rowid FROM app_log_dtls WHERE inputfoldername=@inputfoldername and cldfoldername = @cldfoldername ", sqlite_conn);
                }
                else
                {
                    cmdSelect = new SQLiteCommand(
                            "SELECT min(rowid) rowid FROM app_log_dtls WHERE inputfoldername=@inputfoldername and cldfoldername = @cldfoldername ", sqlite_conn);
                }

                cmdSelect.Parameters.AddWithValue("@inputfoldername", inputfoldername);
                cmdSelect.Parameters.AddWithValue("@cldfoldername", cldfoldername);

                App_log_dtls app_log_dtls = new App_log_dtls();
                Int64 maxRowid = 0;
                if (cmdSelect.ExecuteScalar().ToString().Trim().Length > 0 && Int64.Parse(cmdSelect.ExecuteScalar().ToString()) > 0)
                {
                    maxRowid = Int64.Parse(cmdSelect.ExecuteScalar().ToString());
                }
                return maxRowid;

            }
            catch (Exception ex) {
                
                return 0;
            }
        }

        public void updateRowIDStatus(Int64 rowid,int isProcessed, int isError, String errorDesc)
        {
            try {
                SQLiteCommand cmdSelect = new SQLiteCommand();

                cmdSelect = new SQLiteCommand(
                        "update app_log_dtls set isProcessed=@isProcessed, isError = @isError, desc=@errorDesc,updatedatetime=@updatedatetime " +
                        "WHERE rowid=@rowid", sqlite_conn);

                //"@Timestamp", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")

                cmdSelect.Parameters.AddWithValue("@rowid", rowid);
                cmdSelect.Parameters.AddWithValue("@isProcessed", isProcessed);
                cmdSelect.Parameters.AddWithValue("@isError", isError);
                cmdSelect.Parameters.AddWithValue("@errorDesc", errorDesc);
                cmdSelect.Parameters.AddWithValue("@updatedatetime", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                cmdSelect.ExecuteScalar();

            }
            catch(Exception ex) {
                throw ex;
            }
        }

        public App_log_dtls getApp_log_dtls(Int64 rowid) {
            
            try
            {
                SQLiteCommand cmdSelect = new SQLiteCommand(
                    "SELECT * FROM app_log_dtls WHERE rowid=@rowid", sqlite_conn);
                cmdSelect.Parameters.AddWithValue("@rowid", rowid);

                App_log_dtls app_log_dtls = new App_log_dtls();

                SQLiteDataReader reader = cmdSelect.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Int64 id = (Int64)reader["rowid"];
                        string inputfoldername = (string)reader["inputfoldername"];
                        string filename = (string)reader["filename"];
                        string cldfoldername = (string)reader["cldfoldername"];
                        int isProcessed = (int)reader["isProcessed"];
                        int isError = (int)reader["isError"];
                        string desc = (string)reader["desc"];
                        //DateTime Timestamp = (DateTime)reader["Timestamp"];
                        //DateTime updatedatetime = (DateTime)reader["updatedatetime"];
                        app_log_dtls.rowid = id;
                        app_log_dtls.inputfoldername = inputfoldername;
                        app_log_dtls.filename = filename;
                        app_log_dtls.cldfoldername = cldfoldername;
                        app_log_dtls.isProcessed = isProcessed;
                        app_log_dtls.isError = isError;
                        app_log_dtls.desc = desc;
                        //app_log_dtls.Timestamp = Timestamp;
                        //app_log_dtls.updatedatetime = updatedatetime;

                    }
                }

                return app_log_dtls;

            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}
