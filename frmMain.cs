using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CloudinaryMigration.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CloudinaryMigration
{
    public partial class frmMain : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        String sqlitedbpath = "";
        //Account account = new Account(
        //    "anuvu-dev",
        //    "336625465193458",
        //    "WuvXWKKtWJrcfNulItmGqvViSGU");

        Account account = new Account("","","");

        Cloudinary cloudinary;
        SQLLiteHelper sQLLiteHelper;
        Int32 threadCount = 10;
        static String tempPathForLongFilepaths = "";
        public frmMain()
        {
            InitializeComponent();
            sqlitedbpath = ConfigurationManager.AppSettings["sqllitedbpath"];

            string cldAccountname = ConfigurationManager.AppSettings["cldAccountname"];
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            string secretKey = ConfigurationManager.AppSettings["secretKey"];
            threadCount =Int16.Parse( ConfigurationManager.AppSettings["threadcount"].ToString());
            tempPathForLongFilepaths = ConfigurationManager.AppSettings["tempPathForLongFilepaths"];
            account = new Account(cldAccountname,apiKey,secretKey);
            this.Text = "Account Name:" + cldAccountname + "; apiKey" + apiKey + "; secretKey" + secretKey;

            sQLLiteHelper = new SQLLiteHelper(sqlitedbpath);

            Directory.Delete(tempPathForLongFilepaths, true);
            Directory.CreateDirectory(tempPathForLongFilepaths);

            rchTextboxUnprocessedDtls.Text = "";
            //try
            //{
            //    string Path = "E:\\New folder\\testcld\\OneDrive - Global Eagle\\MARCH CYCLE (AWS WORKAROUND)\\AIRLINES\\AIR NEW ZEALAND\\0222\\Featured Images\\Crew Space Highlight Titles\\Series\\One Lane Bridge_69443\\One Lane Bridge_Season_02_93575\\Key Art\\OLB Digital Billboards Folder\\maosx\\test123456\\test.jpg";

            //    Alphaleonis.Win32.Filesystem.File.Exists(Path);


            //    Byte[] filebytes = Alphaleonis.Win32.Filesystem.File.ReadAllBytes(Path);

            //    File.WriteAllBytes("D:\\Foo.jpg", filebytes);

            //    System.IO.File.Copy(Path, "D:\\abc.jpg");
            //}
            //catch (Exception ex) {
            //    throw ex;
            //}

            //string filepath = "D:\\test.jpg";//fldg.SelectedPath;
            //String publicIDPath = "Ingest/New folder/testcld/OneDrive - Global Eagle/MARCH CYCLE (AWS WORKAROUND)/AIRLINES/AIR NEW ZEALAND/0222/Featured Images/Crew Space Highlight Titles/Series/One Lane Bridge_69443/One Lane Bridge_Season_02_93575/Key Art/OLB Digital Billboards Folder/maosx/test123456/test.jpg";
            //try
            //{
            //    cloudinary = new Cloudinary(account);

            //    var uploadParams = new ImageUploadParams()
            //    {
            //        File = new FileDescription(filepath),
            //        PublicId = publicIDPath,

            //    };

            //    var uploadResult = cloudinary.Upload(uploadParams, "auto");

            //}
            //catch (Exception ex) {
            //    throw ex;
            //}

            //Zip File case
            //try
            //{
            //    var uploadRawParams = new RawUploadParams()
            //    {
            //        File = new FileDescription("D:\\Sachin\\Projects\\Cloudinary\\Working\\INTERNATIONAL LAYERED INDESIGN.zip"),
            //        PublicId = "Ingest/INTERNATIONAL LAYERED INDESIGN.zip",
            //    };
            //    var uploadResult = cloudinary.Upload(uploadRawParams);

            //}
            //catch (Exception ex) {
            //    throw ex;
            //}


        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldg = new System.Windows.Forms.FolderBrowserDialog();
            DialogResult dlgResult = fldg.ShowDialog();

            if (dlgResult == DialogResult.OK)
            {
                textBox1.Text = fldg.SelectedPath;
                txtFolderSelected.Text = new DirectoryInfo(textBox1.Text).Name;
                //String[] allDirs = Directory.GetDirectories(textBox1.Text);
                //String[] allFiles = Directory.GetFiles(textBox1.Text);
                //MessageBox.Show("Directories :" + String.Join(" | ", allDirs) + ". \n Files :" + String.Join(" | ", allFiles));
                //MessageBox.Show("allfiles :" + String.Join(" | ", allfiles));
            }
            //const string strCmdText = "/C Set CLOUDINARY_URL=cloudinary://336625465193458:WuvXWKKtWJrcfNulItmGqvViSGU@anuvu-dev&cld sync --push D:\test myfolder/test3";
            //Process.Start("CMD.exe", strCmdText);

            //ProcessStartInfo pStartInfo = new ProcessStartInfo();
            //pStartInfo.FileName = "CMD";
            //pStartInfo.Arguments = @"/C Set CLOUDINARY_URL=cloudinary://336625465193458:WuvXWKKtWJrcfNulItmGqvViSGU@anuvu-dev && cld sync --push D:\test myfolder/test3 ";
            ////pStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //Process.Start(pStartInfo).WaitForExit();


        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            String jobID = DateTime.Now.ToString("ddMMyyyy:hhmmss");
            cloudinary = new Cloudinary(account);
            List<UploadModel> uploadDetails = new List<UploadModel>();
            List<String> files = new List<string>();

            try
            {
                sQLLiteHelper.InsertData("Info", "Process started", jobID, "");

                if (txtDestinationpath.Text.Contains("\\"))
                {
                    MessageBox.Show("Invalid character in Cloudinary path", "", MessageBoxButtons.OK);
                    return;
                }
                Int64 rowIDfrom = sQLLiteHelper.getRowID(txtFolderSelected.Text.Trim(), txtDestinationpath.Text.Trim(), "min"); ;
                Int64 rowIDto = sQLLiteHelper.getRowID(txtFolderSelected.Text.Trim(),txtDestinationpath.Text.Trim(),"max");

                if( (txtrowidfrom.Text.Trim().Length > 0 && Int64.Parse(txtrowidfrom.Text.Trim()) < rowIDfrom) ||
                    (txtrowidto.Text.Trim().Length > 0 && Int64.Parse(txtrowidto.Text.Trim()) > rowIDto))
                {
                    MessageBox.Show("Invalid Rowid From and To value, it should be between " + rowIDfrom + " and " + rowIDto);
                    return;
                }


                if (txtrowidfrom.Text.Trim().Length>0)
                rowIDfrom = Int64.Parse(txtrowidfrom.Text.Trim());

                if (txtrowidto.Text.Trim().Length > 0)
                rowIDto = Int64.Parse(txtrowidto.Text.Trim());

                Int64 counter = 0;
                String rowids = "";
                while (rowIDfrom <= rowIDto )
                {
                    String publicIDPath = "";
                    App_log_dtls app_Log_Dtls= sQLLiteHelper.getApp_log_dtls(rowIDfrom);

                    try
                    {
                        if (app_Log_Dtls.filename.Trim().Length == 0)
                        {
                            sQLLiteHelper.InsertData("Error", rowIDfrom.ToString() + " | rowid not found ", jobID, "");
                            continue;
                        }

                        if (app_Log_Dtls.isProcessed == 1)
                        {
                            continue;
                        }

                        if (app_Log_Dtls.isProcessed == 0)
                        {
                            ++counter;
                            rowids = rowids + ";" + app_Log_Dtls.rowid.ToString();
                            //continue;
                        }

                        String filepath = app_Log_Dtls.filename;
                        if (chkUploadPath.Checked)
                        {
                            publicIDPath = (txtDestinationpath.Text.Replace("\\", "/") + "/" + txtFolderSelected.Text + "/" + filepath.Replace(textBox1.Text + "\\", "")).Replace("\\", "/");
                            files.Add(publicIDPath);
                        }
                        else
                        {
                            publicIDPath = (txtDestinationpath.Text.Replace("\\", "/") + "/" + filepath.Replace(textBox1.Text + "\\", "")).Replace("\\", "/");
                            files.Add(publicIDPath);

                        }
                        uploadDetails.Add(new UploadModel { FilePath = filepath, publicID = publicIDPath, rowid = app_Log_Dtls.rowid });

                        if (uploadDetails.Count >= threadCount)
                        {
                            List<Task> tasks = new List<Task>();

                            foreach (var uploadfiledtl in uploadDetails)
                            {
                                Task task = Task.Factory.StartNew(() => createUploadThread(uploadfiledtl, sQLLiteHelper, cloudinary, jobID, uploadfiledtl.rowid));
                                tasks.Add(task);
                            }
                            Task.WaitAll(tasks.ToArray());
                            uploadDetails.Clear();
                        }
                        

                    }
                    catch(Exception ex)
                    {
                        sQLLiteHelper.InsertData("Error", "Error while processing of rowid " + app_Log_Dtls.rowid, jobID, "");
                    }
                    finally {
                        rowIDfrom = rowIDfrom + 1;
                    }

                }
                
                if (uploadDetails.Count > 0)
                {
                    List<Task> tasks = new List<Task>();

                    foreach (var uploadfiledtl in uploadDetails)
                    {
                        Task task = Task.Factory.StartNew(() => createUploadThread(uploadfiledtl, sQLLiteHelper, cloudinary, jobID,uploadfiledtl.rowid));
                        tasks.Add(task);
                    }
                    Task.WaitAll(tasks.ToArray());
                    uploadDetails.Clear();

                }
                rchTextboxUnprocessedDtls.Text = "Unprocesed count =" + counter + ". " + "Unprocessed rows" + rowids;
                //MessageBox.Show("Unprocessed rows" + rowids);
                MessageBox.Show("job " + jobID + " completed.");

            }
            catch (Exception ex)
            {
                sQLLiteHelper.InsertData("Error", "Process aborted", jobID, "");
            }
            finally
            {
                this.Cursor = Cursors.Default;
                sQLLiteHelper.InsertData("Info", "Process Finished", jobID, "");
            }

        }

        static void createUploadThread(UploadModel uploadfiledtl, SQLLiteHelper sQLLiteHelper, Cloudinary cloudinary, String jobID,Int64 rowid)
        {

            try
            {
                String filePath = uploadfiledtl.FilePath;

                if (uploadfiledtl.FilePath.Length>250) {
                    //tempPathForLongFilepaths
                    if (Alphaleonis.Win32.Filesystem.File.Exists(uploadfiledtl.FilePath)) { 
                    }

                    
                    Byte[] filebytes = Alphaleonis.Win32.Filesystem.File.ReadAllBytes(filePath);

                    
                    String newFileNamewithPath = tempPathForLongFilepaths + "\\" + uploadfiledtl.rowid + "_" + Alphaleonis.Win32.Filesystem.Path.GetFileName(filePath);

                    File.WriteAllBytes(newFileNamewithPath , filebytes);
                    filePath = newFileNamewithPath;
                    //System.IO.File.Copy(Path, "D:\\abc.jpg");

                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    PublicId = uploadfiledtl.publicID,

                };

                var uploadResult = cloudinary.Upload(uploadParams, "auto");
                sQLLiteHelper.InsertData("Info", rowid.ToString() + " | Upload completed", jobID, uploadfiledtl.publicID);
                sQLLiteHelper.updateRowIDStatus(rowid, 1, 0, "");

            }
            catch (Exception ex)
            {
                sQLLiteHelper.InsertData("Error", rowid.ToString() + " | Upload Error " + ex.Message, jobID, "");
                sQLLiteHelper.updateRowIDStatus(rowid, 0, 1, ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (sQLLiteHelper.getRowID(txtFolderSelected.Text.Trim(), txtDestinationpath.Text.Trim(),"max")>0) {
                MessageBox.Show("folder and DB is already synced");
                return;
            }

            if (txtDestinationpath.Text.Contains("\\"))
            {
                MessageBox.Show("Invalid character in Cloudinary path", "", MessageBoxButtons.OK);
                return;
            }


            foreach (var filepath in Directory.EnumerateFiles(textBox1.Text, "*", SearchOption.AllDirectories))
            {

                sQLLiteHelper.InsertDataFordbSync(txtFolderSelected.Text.Trim(), filepath, txtDestinationpath.Text.Trim(), 0, 0, "");

            }
            MessageBox.Show("synched to DB!");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
