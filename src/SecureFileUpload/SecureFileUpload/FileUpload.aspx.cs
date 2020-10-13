using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SecureFileUpload
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateFileList();
            ResetForm();
        }

        private void UpdateFileList()
        {
            string SaveLocation = Server.MapPath("App_Data");

            var items = new List<ListItem>();

            foreach (string filepath in System.IO.Directory.GetFiles(SaveLocation))
            {
                string filename = System.IO.Path.GetFileName(filepath);
                items.Add(new ListItem { Text = filename, Value = filename });
            }

            dlFiles.DataSource = items;
            dlFiles.DataBind();
        }

        protected void dlFiles_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFile")
            {
                string file = e.CommandArgument as string;
                string SaveLocation = System.IO.Path.Combine(Server.MapPath("App_Data"), file);
                System.IO.File.Delete(SaveLocation);
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
                    string fn = System.IO.Path.GetFileName(fuCsvFile.PostedFile.FileName);
                    string SaveLocation = System.IO.Path.Combine(Server.MapPath("App_Data"), fn);
                    try
                    {
                        fuCsvFile.PostedFile.SaveAs(SaveLocation);
                        ShowResult("The file has been uploaded.");
                        UpdateFileList();
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex.Message);
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