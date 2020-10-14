using SecureFileUpload.FileUtilities;
using SecureFileUpload.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SecureFileUpload
{
    public partial class FileUpload : System.Web.UI.Page
    {
        private enum FileStorageProvider
        {
            Local,
            AzureStorageBlobs
        }

        private IFileStorage fileStorage;
        private IVirusScanner virusScanner;

        public FileUpload()
        {
            this.virusScanner = new CloudmersiveVirusScanner();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetFileStorage(CurrentFileStorageProvider);
            UpdateFileList();
            ResetForm();
        }

        private void UpdateFileList()
        {
            var items = new List<ListItem>();

            foreach (string filepath in fileStorage.GetFiles())
            {
                items.Add(new ListItem { Text = filepath, Value = filepath });
            }

            dlFiles.DataSource = items;
            dlFiles.DataBind();
        }

        protected void dlFiles_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFile")
            {
                string file = e.CommandArgument as string;
                fileStorage.DeleteFile(file);
                ShowResult($"Deleted {file}");
                UpdateFileList();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (fuCsvFile.PostedFile != null)
            {
                if (fuCsvFile.PostedFile.ContentLength > 0)
                {
                    var memoryStream = StreamUtility.CopyToMemoryStream(fuCsvFile.PostedFile.InputStream);

                    var scanResult = virusScanner.ScanStream(StreamUtility.CopyToMemoryStream(memoryStream));
                    if (scanResult.IsSafe)
                    {
                        var parseErrors = CsvFile.Validate(StreamUtility.CopyToMemoryStream(memoryStream));

                        if (parseErrors.Count > 0)
                        {
                            ShowError("Errors found in file: <br />" + string.Join("<br />", parseErrors));
                        }
                        else
                        {
                            try
                            {
                                fileStorage.SavePostedFile(fuCsvFile.PostedFile.FileName, StreamUtility.CopyToMemoryStream(memoryStream));
                                ShowResult("The file has been uploaded.");
                                UpdateFileList();
                            }
                            catch (Exception ex)
                            {
                                ShowError(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        ShowError($"Virus scan found issues: {scanResult.Message}");
                    }
                }
                else
                {
                    ShowError("Empty file uploaded.");
                }
            }
            else
            {
                ShowResult("Please select a file to upload.");
            }
        }

        private void ResetForm()
        {
            pnlResult.Visible = false;
            pnlError.Visible = false;
        }

        private void ShowResult(string resultMessage)
        {
            pnlResult.Visible = true;
            pnlError.Visible = false;
            litResults.Text = resultMessage;
        }

        private void ShowError(string errorMessage)
        {
            pnlResult.Visible = false;
            pnlError.Visible = true;
            litError.Text = errorMessage;
        }

        private FileStorageProvider CurrentFileStorageProvider
        {
            get
            {
                return (FileStorageProvider)Enum.Parse(typeof(FileStorageProvider), rblStorageProvider.SelectedValue);
            }
        }

        protected void rblStorageProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFileStorage(CurrentFileStorageProvider);
            UpdateFileList();
        }

        private void SetFileStorage(FileStorageProvider provider)
        {
            switch (provider)
            {
                case FileStorageProvider.AzureStorageBlobs:
                    this.fileStorage = new AzureFileStorage();
                    break;
                default:
                    this.fileStorage = new LocalFileStorage(Server.MapPath("App_Data"));
                    break;
            }
        }
    }
}