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
    public partial class vendor_registration : System.Web.UI.Page
    {

        private Boolean vEditRecord;
        private Boolean vNewRecord;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
                {
                    Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }

                GetDataVendor();
                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
            }
            CheckUserAccess(Session["Oid"].ToString());
        }

        private void CheckUserAccess(string vNIK)
        {
            vEditRecord = false;
            vNewRecord = false;

            string vUserCode = vNIK;

            string vPath = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection vCon = new SqlConnection(vPath);
            SqlCommand vSQLComm = new SqlCommand();
            vSQLComm.Connection = vCon;

            vCon.Open();

            vSQLComm.CommandText = "sp_HRIS_DB_UserControl";
            vSQLComm.CommandType = CommandType.StoredProcedure;
            vSQLComm.Parameters.AddWithValue("@Code", vUserCode);
            vSQLComm.Parameters.AddWithValue("@Flag", "ModuleAccess");
            vSQLComm.Parameters.AddWithValue("@FlagModule", "data_vendor");

            SqlDataReader vDR = vSQLComm.ExecuteReader();
            if (vDR.HasRows)
            {
                while (vDR.Read())
                {
                    vEditRecord = Convert.ToBoolean(vDR["RoleWrite"]); 
                    vNewRecord = Convert.ToBoolean(vDR["RoleCreate"]); 
                }
            }
            //foreach (Row _Row in dr)
        }


        public bool CheckLegalNameDuplicate(string legal_name)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "SELECT legal_name FROM Vendors WHERE legal_name=@legal_name";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@legal_name", legal_name);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("legal_name", (string)dr["legal_name"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckBrandNameDuplicate(string brand_name)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "SELECT brand_name FROM Vendors WHERE brand_name=@brand_name";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@brand_name", brand_name);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("brand_name", (string)dr["brand_name"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void CheckUploadDocument()
        {
            label1.Text = "<b>Status uploaded file :<b/><br/>";
            labbel2.Text = "<b>Status not uploaded file :<b/><br/>";
            label1.Visible = true;
            if (FileUploadDocument.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadDocument.PostedFiles.Count();
                if (filecount <= 6)
                {
                    foreach (HttpPostedFile postfiles in FileUploadDocument.PostedFiles)
                    {
                        //Get The File Extension  
                        string filetype = Path.GetExtension(postfiles.FileName);
                        if (filetype.ToLower() == ".pdf")
                        {
                            //Get The File Size In Bite  
                            double filesize = postfiles.ContentLength;
                            if (filesize < (2048576))
                            {
                                fileuploadcount++;
                                string serverfolder = string.Empty;
                                string serverpath = string.Empty;
                                // Adding File Into Scecific Folder Depend On his Extension
                                switch (filetype)
                                {
                                    case ".pdf":
                                        serverfolder = Server.MapPath("~/UploadDocumentVendor" + "/" + txtLegalName.Value.Trim() + "/");
                                        //check Folder avlalible or not  
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // create Folder  
                                            Directory.CreateDirectory(serverfolder);
                                        }
                                        serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                        FileUploadDocument.SaveAs(serverpath);
                                        label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded successfully<br/>";
                                        hlbOK.Value = "OK";
                                        break;

                                }

                            }
                            else
                            {
                                labbel2.Text += "[" + postfiles.FileName + "]- files not uploded size is greater then(2)MB.<br/>Your File Size is(" + (filesize / (1024 * 1034)) + ") MB </br>";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
                            }
                        }
                        else
                        {
                            labbel2.Text += "[" + postfiles.FileName + "]- file type must be pdf <br/>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
                        }
                    }
                }
                else
                {
                    label1.Visible = false;
                    labbel2.Text = "you are select(" + filecount + ")files <br/>";
                    labbel2.Text += "please select Maximum (6) files !!!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
                }

            }
            else
            {
                label1.Visible = false;
                label3.Visible = false;
                labbel2.Text = "<b>Please select the file for upload !!!</b></br>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedUpload();", true);
            }

        }

        protected void CheckUpdateDocument()
        {
            label1.Text = "<b>Status uploaded file :<b/><br/>";
            labbel2.Text = "<b>Status not uploaded file :<b/><br/>";
            label1.Visible = true;
            if (FileUploadDocument.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadDocument.PostedFiles.Count();
                if (filecount <= 6)
                {
                    foreach (HttpPostedFile postfiles in FileUploadDocument.PostedFiles)
                    {
                        //Get The File Extension  
                        string filetype = Path.GetExtension(postfiles.FileName);
                        if (filetype.ToLower() == ".pdf")
                        {
                            //Get The File Size In Bite  
                            double filesize = postfiles.ContentLength;
                            if (filesize < (2048576))
                            {
                                fileuploadcount++;
                                string serverfolder = string.Empty;
                                string serverpath = string.Empty;
                                // Adding File Into Scecific Folder Depend On his Extension
                                switch (filetype)
                                {
                                    case ".pdf":
                                        serverfolder = Server.MapPath("~/UploadDocumentVendor" + "/" + txtLegalName.Value.Trim() + "/");
                                        //check Folder avlalible or not  
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // create Folder  
                                            Directory.CreateDirectory(serverfolder);
                                        }
                                        serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                        FileUploadDocument.SaveAs(serverpath);
                                        label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded  successfully<br/>";
                                        hlbOK.Value = "OK";
                                        break;

                                }

                            }
                            else
                            {
                                labbel2.Text += "[" + postfiles.FileName + "]- files not uploded size is greater then(2)MB.<br/>Your File Size is(" + (filesize / (1024 * 1034)) + ") MB </br>";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
                            }
                        }
                        else
                        {
                            labbel2.Text += "[" + postfiles.FileName + "]- file type must be pdf <br/>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
                        }
                    }
                }
                else
                {
                    label1.Visible = false;
                    labbel2.Text = "you are select(" + filecount + ")files <br/>";
                    labbel2.Text += "please select Maximum (6) files !!!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
                }

            }
            else
            {
                hlbOK.Value = "OK";
            }

        }

        protected void CheckDuplicate()
        {
            bool islegalnameduplicate = CheckLegalNameDuplicate(txtLegalName.Value);
            bool isbrandnameduplicate = CheckBrandNameDuplicate(txtBrandName.Value);

            if (islegalnameduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateDataLegalName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (isbrandnameduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateDataBrandName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else
            {
                CheckUploadDocument();
            }
        }

        protected void GetDataVendor()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Vendors";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableVendor.DataSource = dtb;
            TableVendor.DataBind();

            TableVendor.Columns[2].Visible = false;

            TableVendor.UseAccessibleHeader = true;
            TableVendor.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtLegalName.Value=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldLegalName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (txtBrandName.Value=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldBrandName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (txtAddress.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldAddress();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (txtCity.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldCity();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (txtPhone.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldPhone();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (txtPICName.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldPICName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (txtPICPhone.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldPICPhone();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (txtPICEmail.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldPICEmail();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else if (txtCompanySite.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldCompanySite();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                GetDataVendor();
            }
            else
            {
                CheckDuplicate();
                if (hlbOK.Value == "OK")
                {
                    //Save Data Vendor to DB
                    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                    SqlConnection Con = new SqlConnection(path);
                    Con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "sp_HRIS_DB_Vendors";
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Connection = Con;
                    sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                    sqlcomm.Parameters.AddWithValue("@legal_name", txtLegalName.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@brand_name", txtBrandName.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@address", txtAddress.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@city", txtCity.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@phone", txtPhone.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@pic_name", txtPICName.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@pic_phone", txtPICPhone.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@pic_email", txtPICEmail.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@company_site", txtCompanySite.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
                    sqlcomm.Parameters.AddWithValue("@createby", txtCreateBy.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

                    sqlcomm.ExecuteNonQuery();
                    Con.Close();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                    GetDataVendor();
                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;

                }
            }
            
            #region OldUploadFiles
            //string filePathDeed = FileUploadDeedofEstablishment.PostedFile.FileName; // getting the file path of uploaded file  
            //string filenameDeed = Path.GetFileName(filePathDeed); // getting the file name of uploaded file  

            //string filePathtax_info = FileUploadTaxInformation.PostedFile.FileName; // getting the file path of uploaded file  
            //string filenametax_info = Path.GetFileName(filePathtax_info); // getting the file name of uploaded file  

            //string filePathtax_number = FileUploadTaxNumber.PostedFile.FileName; // getting the file path of uploaded file  
            //string filenametax_number = Path.GetFileName(filePathtax_number); // getting the file name of uploaded file  

            //string filePathbusiness_permit = FileUploadBusinessPermit.PostedFile.FileName; // getting the file path of uploaded file  
            //string filenamebusiness_permit = Path.GetFileName(filePathbusiness_permit); // getting the file name of uploaded file 

            //string filePathylid_vendor_form = FileUploadYLIDVendorForm.PostedFile.FileName; // getting the file path of uploaded file  
            //string filenameylid_vendor_form = Path.GetFileName(filePathylid_vendor_form); // getting the file name of uploaded file 

            //string filePathylid_sup_declaration = FileUploadYLIDSupplierDeclarationForm.PostedFile.FileName; // getting the file path of uploaded file  
            //string filenameylid_sup_declaration = Path.GetFileName(filePathylid_sup_declaration); // getting the file name of uploaded file 

            //string extDeed = Path.GetExtension(filenameDeed); // getting the file extension of uploaded file  
            //string exttax_info = Path.GetExtension(filenametax_info); // getting the file extension of uploaded file
            //string exttax_number = Path.GetExtension(filenametax_number); // getting the file extension of uploaded file  
            //string extbusiness_permit = Path.GetExtension(filenamebusiness_permit); // getting the file extension of uploaded file
            //string extylid_vendor_form = Path.GetExtension(filenameylid_vendor_form); // getting the file extension of uploaded file  
            //string extylid_sup_declaration = Path.GetExtension(filenameylid_sup_declaration); // getting the file extension of uploaded file

            //string type = "application/pdf";

            //Stream fs_Deed = FileUploadDeedofEstablishment.PostedFile.InputStream;
            //Stream fs_tax_info = FileUploadTaxInformation.PostedFile.InputStream;
            //Stream fs_tax_number = FileUploadTaxNumber.PostedFile.InputStream;
            //Stream fs_business_permit = FileUploadBusinessPermit.PostedFile.InputStream;
            //Stream fs_ylid_vendor_form = FileUploadYLIDVendorForm.PostedFile.InputStream;
            //Stream fs_ylid_sup_declaration = FileUploadYLIDSupplierDeclarationForm.PostedFile.InputStream;

            //BinaryReader br_Deed = new BinaryReader(fs_Deed); //reads the binary files  
            //Byte[] bytes_Deed = br_Deed.ReadBytes((Int32)fs_Deed.Length); //counting the file length into bytes

            //BinaryReader br_tax_info = new BinaryReader(fs_tax_info); //reads the binary files  
            //Byte[] bytes_tax_info = br_tax_info.ReadBytes((Int32)fs_tax_info.Length); //counting the file length into bytes

            //BinaryReader br_tax_number = new BinaryReader(fs_tax_number); //reads the binary files  
            //Byte[] bytes_tax_number = br_tax_number.ReadBytes((Int32)fs_tax_number.Length); //counting the file length into bytes

            //BinaryReader br_business_permit = new BinaryReader(fs_business_permit); //reads the binary files  
            //Byte[] bytes_business_permit = br_business_permit.ReadBytes((Int32)fs_business_permit.Length); //counting the file length into bytes

            //BinaryReader br_ylid_vendor_form = new BinaryReader(fs_ylid_vendor_form); //reads the binary files  
            //Byte[] bytes_ylid_vendor_form = br_ylid_vendor_form.ReadBytes((Int32)fs_ylid_vendor_form.Length); //counting the file length into bytes

            //BinaryReader br_ylid_sup_declaration = new BinaryReader(fs_ylid_sup_declaration); //reads the binary files  
            //Byte[] bytes_ylid_sup_declaration = br_ylid_sup_declaration.ReadBytes((Int32)fs_ylid_sup_declaration.Length); //counting the file length into bytes

            //sqlcomm.Parameters.AddWithValue("@document_name_deed", SqlDbType.VarChar).Value= filenameDeed;
            //sqlcomm.Parameters.AddWithValue("@format_deed", SqlDbType.VarChar).Value = type;
            //sqlcomm.Parameters.AddWithValue("@upload_deed", SqlDbType.Binary).Value = bytes_Deed;
            //sqlcomm.Parameters.AddWithValue("@document_name_tax_info", SqlDbType.VarChar).Value = filenametax_info;
            //sqlcomm.Parameters.AddWithValue("@format_tax_info", SqlDbType.VarChar).Value = type;
            //sqlcomm.Parameters.AddWithValue("@upload_tax_info", SqlDbType.Binary).Value = bytes_tax_info;
            //sqlcomm.Parameters.AddWithValue("@document_name_tax_number", SqlDbType.VarChar).Value = filenametax_number;
            //sqlcomm.Parameters.AddWithValue("@format_tax_number", SqlDbType.VarChar).Value = type;
            //sqlcomm.Parameters.AddWithValue("@upload_tax_number", SqlDbType.Binary).Value = bytes_tax_number;
            //sqlcomm.Parameters.AddWithValue("@document_name_business_permit", SqlDbType.VarChar).Value = filenamebusiness_permit;
            //sqlcomm.Parameters.AddWithValue("@format_business_permit", SqlDbType.VarChar).Value = type;
            //sqlcomm.Parameters.AddWithValue("@upload_business_permit", SqlDbType.Binary).Value = bytes_business_permit;
            //sqlcomm.Parameters.AddWithValue("@document_name_ylid_vendor_form", SqlDbType.VarChar).Value = filenameylid_vendor_form;
            //sqlcomm.Parameters.AddWithValue("@format_ylid_vendor_form", SqlDbType.VarChar).Value = type;
            //sqlcomm.Parameters.AddWithValue("@upload_ylid_vendor_form", SqlDbType.Binary).Value = bytes_ylid_vendor_form;
            //sqlcomm.Parameters.AddWithValue("@document_name_ylid_sup_declaration", SqlDbType.VarChar).Value = filenameylid_sup_declaration;
            //sqlcomm.Parameters.AddWithValue("@format_ylid_sup_declaration", SqlDbType.VarChar).Value = type;
            //sqlcomm.Parameters.AddWithValue("@upload_ylid_sup_declaration", SqlDbType.Binary).Value = bytes_ylid_sup_declaration;
            #endregion
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            CheckUpdateDocument();
            if (hlbOK.Value == "OK")
            {
                //Save Data Vendor to DB
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Vendors";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
                sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@legal_name", txtLegalName.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@brand_name", txtBrandName.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@address", txtAddress.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@city", txtCity.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@phone", txtPhone.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@pic_name", txtPICName.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@pic_phone", txtPICPhone.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@pic_email", txtPICEmail.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@company_site", txtCompanySite.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
                sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());

                sqlcomm.ExecuteNonQuery();
                Con.Close();
                GetDataVendor();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
               
            }
        }

        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("vendor_registration.aspx");
        //}

        protected void btnGenExcel_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Vendors";
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
                    wb.Worksheets.Add(dt, "Data Vendor");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Data Vendor" + "_" + _vDate1 + ".xlsx");
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
            sqlcomm.Dispose();
            Con.Close();
            Con.Dispose();
        }

        protected void TableVendor_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableVendor_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableVendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GetDataVendor();
            if (vEditRecord == true)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;

                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                //((LinkButton)row.Cells[0].FindControl("btnEdit")).Enabled = false;
                hlbID.Value = row.Cells[2].Text.ToString();
                txtLegalName.Value = row.Cells[3].Text.ToString();
                txtBrandName.Value = row.Cells[4].Text.ToString();
                txtAddress.Value = row.Cells[5].Text.ToString();
                txtCity.Value = row.Cells[6].Text.ToString();
                txtPhone.Value = row.Cells[7].Text.ToString();
                txtPICName.Value = row.Cells[8].Text.ToString();
                txtPICPhone.Value = row.Cells[9].Text.ToString();
                txtPICEmail.Value = row.Cells[10].Text.ToString();
                txtCompanySite.Value = row.Cells[11].Text.ToString();
                ckActive.Checked = Convert.ToBoolean(row.Cells[12].Text.ToString());
                txtCreateBy.Value = row.Cells[13].Text.ToString();
                txtCreateDate.Value = row.Cells[14].Text.ToString();
                txtModifiedBy.Value = row.Cells[15].Text.ToString();
                txtModifiedDate.Value = row.Cells[16].Text.ToString();
            }
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
                DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/UploadDocumentVendor" + "/" + row.Cells[3].Text + "/"));
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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            GetDataVendor();

            if (vNewRecord == true)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewVendor').modal();", true);
                
            }
            
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("vendor_registration.aspx");
        }
    }
}