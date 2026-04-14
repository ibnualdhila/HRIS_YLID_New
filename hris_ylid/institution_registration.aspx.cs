using ClosedXML.Excel;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class institution_registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                GetDataInstitution();
                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;

            }
        }

        protected void GetDataInstitution()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Institutions";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableInstitution.DataSource = dtb;
            TableInstitution.DataBind();

            TableInstitution.Columns[1].Visible = false;
            TableInstitution.Columns[2].Visible = false;

            TableInstitution.UseAccessibleHeader = true;
            TableInstitution.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
            GetDataInstitution();
        }

        public bool CheckInstitutionNameDuplicate(string institution_name)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "SELECT institution_name FROM Institutions WHERE institution_name=@institution_name";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@institution_name", institution_name);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("institution_name", (string)dr["institution_name"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        //protected void CheckUploadDocument()
        //{
        //    label1.Text = "<b>Status uploaded file :<b/><br/>";
        //    labbel2.Text = "<b>Status not uploaded file :<b/><br/>";
        //    label1.Visible = true;
        //    if (FileUploadDocument.HasFiles)
        //    {
        //        int filecount = 0;
        //        int fileuploadcount = 0;
        //        //check No of Files Selected  
        //        filecount = FileUploadDocument.PostedFiles.Count();
        //        if (filecount <= 6)
        //        {
        //            foreach (HttpPostedFile postfiles in FileUploadDocument.PostedFiles)
        //            {
        //                //Get The File Extension  
        //                string filetype = Path.GetExtension(postfiles.FileName);
        //                if (filetype.ToLower() == ".pdf")
        //                {
        //                    //Get The File Size In Bite  
        //                    double filesize = postfiles.ContentLength;
        //                    if (filesize < (2048576))
        //                    {
        //                        fileuploadcount++;
        //                        string serverfolder = string.Empty;
        //                        string serverpath = string.Empty;
        //                        // Adding File Into Scecific Folder Depend On his Extension
        //                        switch (filetype)
        //                        {
        //                            case ".pdf":
        //                                serverfolder = Server.MapPath("~/UploadDocumentInstitution" + "/" + txtInstitutionName.Value.Trim() + "/");
        //                                //check Folder avlalible or not  
        //                                if (!Directory.Exists(serverfolder))
        //                                {
        //                                    // create Folder  
        //                                    Directory.CreateDirectory(serverfolder);
        //                                }
        //                                serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
        //                                FileUploadDocument.SaveAs(serverpath);
        //                                label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded  successfully<br/>";
        //                                hlbOK.Value = "OK";
        //                                break;

        //                        }

        //                    }
        //                    else
        //                    {
        //                        labbel2.Text += "[" + postfiles.FileName + "]- files not uploded size is greater then(2)MB.<br/>Your File Size is(" + (filesize / (1024 * 1034)) + ") MB </br>";
        //                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
        //                    }
        //                }
        //                else
        //                {
        //                    labbel2.Text += "[" + postfiles.FileName + "]- file type must be pdf <br/>";
        //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            label1.Visible = false;
        //            labbel2.Text = "you are select(" + filecount + ")files <br/>";
        //            labbel2.Text += "please select Maximum (6) files !!!";
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
        //        }

        //    }
        //    else
        //    {
        //        label1.Visible = false;
        //        label3.Visible = false;
        //        labbel2.Text = "<b>Please select the file for upload !!!</b></br>";
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedUpload();", true);
        //    }

        //}

        //protected void CheckUpdateDocument()
        //{
        //    label1.Text = "<b>Status uploaded file :<b/><br/>";
        //    labbel2.Text = "<b>Status not uploaded file :<b/><br/>";
        //    label1.Visible = true;
        //    if (FileUploadDocument.HasFiles)
        //    {
        //        int filecount = 0;
        //        int fileuploadcount = 0;
        //        //check No of Files Selected  
        //        filecount = FileUploadDocument.PostedFiles.Count();
        //        if (filecount <= 6)
        //        {
        //            foreach (HttpPostedFile postfiles in FileUploadDocument.PostedFiles)
        //            {
        //                //Get The File Extension  
        //                string filetype = Path.GetExtension(postfiles.FileName);
        //                if (filetype.ToLower() == ".pdf")
        //                {
        //                    //Get The File Size In Bite  
        //                    double filesize = postfiles.ContentLength;
        //                    if (filesize < (2048576))
        //                    {
        //                        fileuploadcount++;
        //                        string serverfolder = string.Empty;
        //                        string serverpath = string.Empty;
        //                        // Adding File Into Scecific Folder Depend On his Extension
        //                        switch (filetype)
        //                        {
        //                            case ".pdf":
        //                                serverfolder = Server.MapPath("~/UploadDocumentInstitution" + "/" + txtInstitutionName.Value.Trim() + "/");
        //                                //check Folder avlalible or not  
        //                                if (!Directory.Exists(serverfolder))
        //                                {
        //                                    // create Folder  
        //                                    Directory.CreateDirectory(serverfolder);
        //                                }
        //                                serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
        //                                FileUploadDocument.SaveAs(serverpath);
        //                                label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded  successfully<br/>";
        //                                hlbOK.Value = "OK";
        //                                break;

        //                        }

        //                    }
        //                    else
        //                    {
        //                        labbel2.Text += "[" + postfiles.FileName + "]- files not uploded size is greater then(2)MB.<br/>Your File Size is(" + (filesize / (1024 * 1034)) + ") MB </br>";
        //                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
        //                    }
        //                }
        //                else
        //                {
        //                    labbel2.Text += "[" + postfiles.FileName + "]- file type must be pdf <br/>";
        //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            label1.Visible = false;
        //            labbel2.Text = "you are select(" + filecount + ")files <br/>";
        //            labbel2.Text += "please select Maximum (6) files !!!";
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
        //        }

        //    }
        //    else
        //    {
        //        hlbOK.Value = "OK";
        //    }

        //}

        //protected void CheckDuplicate()
        //{
        //    //bool islegalnameduplicate = CheckInstitutionNameDuplicate(txtInstitutionName.Value);

        //    //if (islegalnameduplicate)
        //    //{
        //    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateDataInstitutionName();", true);
        //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
        //    //    GetDataInstitution();
        //    //}
        //    //else
        //    //{
        //    //    CheckUploadDocument();
        //    //}
        //}

        protected void btnGenExcel_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Institutions";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "GenerateExcel");

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            using (DataTable dt = new DataTable())
            {
                //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                //sFileName = sFileName.Replace("/", "");
                sda.Fill(dt);
                string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Data Institution");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Data Institution" + "_" + _vDate1 + ".xlsx");
                    Response.ContentType = "application/excel";
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }

        protected void TableInstitution_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableInstitution_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableInstitution_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
            GetDataInstitution();
            btnSubmit.Visible = false;
            btnUpdate.Visible = true;

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //((LinkButton)row.Cells[0].FindControl("btnEdit")).Enabled = false;
            hlbID.Value = row.Cells[2].Text.ToString();
            txtInstitutionName.Value = row.Cells[3].Text.ToString();
            txtAddress.Value = row.Cells[4].Text.ToString();
            txtCity.Value = row.Cells[5].Text.ToString();
            txtPhone.Value = row.Cells[8].Text.ToString();
            txtPICName.Value = row.Cells[6].Text.ToString();
            txtPICPhone.Value = row.Cells[7].Text.ToString();
            txtPICEmail.Value = row.Cells[9].Text.ToString();
            txtInstitutionSite.Value = row.Cells[10].Text.ToString();
            ckActive.Checked = Convert.ToBoolean(row.Cells[11].Text.ToString());
            txtCreateBy.Value = row.Cells[12].Text.ToString();
            txtCreateDate.Value = row.Cells[13].Text.ToString();
            txtModifiedBy.Value = row.Cells[14].Text.ToString();
            txtModifiedDate.Value = row.Cells[15].Text.ToString();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string zipFileName = row.Cells[3].Text + ".zip";
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "fileName=" + zipFileName);
            byte[] buffer = new byte[4096];
            ZipOutputStream zipOutputStream = new ZipOutputStream(Response.OutputStream);
            zipOutputStream.SetLevel(3);
            try
            {
                DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/UploadDocumentInstitution" + "/" + row.Cells[3].Text + "/"));
                foreach (var i in DI.GetFiles())
                {
                    Stream fs = File.OpenRead(i.FullName);
                    ZipEntry zipEntry = new ZipEntry(ZipEntry.CleanName(i.Name));
                    zipEntry.Size = fs.Length;
                    zipOutputStream.PutNextEntry(zipEntry);
                    int count = fs.Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        zipOutputStream.Write(buffer, 0, count);
                        count = fs.Read(buffer, 0, buffer.Length);
                        if (!Response.IsClientConnected)
                        {
                            break;
                        }
                        Response.Flush();
                    }
                    fs.Close();
                }
                zipOutputStream.Close();
                Response.Flush();
                Response.End();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool islegalnameduplicate = CheckInstitutionNameDuplicate(txtInstitutionName.Value);

            if (txtInstitutionName.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldInstitutionName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else if (txtAddress.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldAddress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else if (txtCity.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldCity();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else if (txtPhone.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldPhone();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else if (txtPICName.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldPICName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else if (txtPICPhone.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldPICPhone();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else if (txtPICEmail.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldPICEmail();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else if (txtInstitutionSite.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldCompanySite();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else if (islegalnameduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateDataInstitutionName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewInstitution').modal();", true);
                GetDataInstitution();
            }
            else
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Institutions";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@institution_name", txtInstitutionName.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@address", txtAddress.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@city", txtCity.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@phone", txtPhone.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@pic_name", txtPICName.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@pic_phone", txtPICPhone.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@pic_email", txtPICEmail.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@institution_site", txtInstitutionSite.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
                sqlcomm.Parameters.AddWithValue("@createby", txtCreateBy.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

                sqlcomm.ExecuteNonQuery();
                Con.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                GetDataInstitution();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Save Data Vendor to DB
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Institutions";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
            sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@institution_name", txtInstitutionName.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@address", txtAddress.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@city", txtCity.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@phone", txtPhone.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@pic_name", txtPICName.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@pic_phone", txtPICPhone.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@pic_email", txtPICEmail.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@institution_site", txtInstitutionSite.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
            sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
            sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());

            sqlcomm.ExecuteNonQuery();
            Con.Close();
            GetDataInstitution();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("institution_registration.aspx");
        }
    }
}