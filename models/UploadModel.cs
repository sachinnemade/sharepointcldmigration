using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudinaryMigration.models
{
    public class UploadModel
    {
        public Int64 rowid { get; set; }
        public String FilePath { get; set; }
        public String publicID { get; set; }
    }
}
