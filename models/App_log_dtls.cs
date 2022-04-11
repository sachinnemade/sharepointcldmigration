using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudinaryMigration.models
{
    public class App_log_dtls
    {

        //Createsql = "CREATE TABLE IF NOT EXISTS app_log_dtls  (rowid INTEGER PRIMARY KEY AUTOINCREMENT, " +
        //    "inputfoldername text, filename text, cldfoldername text, isProcessed int, isError int, desc text, updatedatetime DATETIME,  Timestamp DATETIME";

        public Int64 rowid { get; set; }
        public String inputfoldername { get; set; }
        public String filename { get; set; }
        public String cldfoldername { get; set; }
        public int isProcessed { get; set; }
        public int isError { get; set; }
        public String desc { get; set; }
        public DateTime updatedatetime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
