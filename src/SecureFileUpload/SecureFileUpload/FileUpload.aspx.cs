using SecureFileUpload.FileUtilities;
using SecureFileUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using TinyCsvParser;

namespace SecureFileUpload
{
    public partial class FileUpload : System.Web.UI.Page
    {
        private IFileStorage fileStorage;

        public FileUpload()
        {
            this.fileStorage = new LocalFileStorage(Server.MapPath("App_Data"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    var parseErrors = CsvFile.Validate(fuCsvFile.PostedFile.InputStream);

                    if (parseErrors.Count > 0)
                    {
                        ShowError("Errors found in file: <br />" + string.Join("<br />", parseErrors));
                    }
                    else
                    {
                        try
                        {
                            fileStorage.SavePostedFile(fuCsvFile.PostedFile);
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
    }
}