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
    public partial class training_registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
                {
                    Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }
                
                GetDataTraining();
                GetSectionName();
                GetVendorName();
                divInternal.Visible = false;
                divVendor.Visible = false;
                divRegulation.Visible = false;
                divGovInstitut.Visible = false;
                divValidityPeriod.Visible = false;
                divAddInfo.Visible = false;

                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                //btnRefresh.Visible = true;
                //btnNew.Visible = true;
                //btnGetCode.Enabled = false;

                //txtTrainingStartDate.Disabled = true;
                //txtTrainingEndDate.Disabled = true;
                //txtLocation.Disabled = true;
                //txtCost.Disabled = true;
                //txtTrainingTitle.Disabled = true;
            }
        }

        protected void GetCodeUploadFile()
        {
            int a;
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string query = "select count(upload_code) from Training";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    txtUploadCode.Value = "UP-DOC-1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    txtUploadCode.Value = "UP-DOC-" + a.ToString();
                }
            }
        }

        protected void GetDataTraining()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableTraining.DataSource = dtb;
            TableTraining.DataBind();

            TableTraining.Columns[2].Visible = false;
            TableTraining.Columns[23].Visible = false;
            TableTraining.Columns[24].Visible = false;
            TableTraining.Columns[25].Visible = false;

            TableTraining.UseAccessibleHeader = true;
            TableTraining.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetSectionName()
        {
            ddlInternal.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddSectionName");

            SqlDataReader dr;


            try
            {
                
                ListItem newItem = new ListItem();
                newItem.Text = "<Select Section>";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlInternal.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["section"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlInternal.Items.Add(newItem);
                }
                dr.Close();
            }
            catch (Exception err)
            {
                string _ErrorMsg = err.Message;
            }
            finally
            {
                Con.Close();
            }
        }

        protected void GetVendorName()
        {
            ddlVendor.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Vendors";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddVendorname");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "<Select Vendor>";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlVendor.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["legal_name"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlVendor.Items.Add(newItem);
                }
                dr.Close();
            }
            catch (Exception err)
            {
                string _ErrorMsg = err.Message;
            }
            finally
            {
                Con.Close();
            }
        }

        protected void ddlTrainerOrganizer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlTrainerOrganizer.SelectedItem.Value) == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
                divInternal.Visible = true;
                divVendor.Visible = false;
                ddlVendor.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }
            else if (Convert.ToInt32(ddlTrainerOrganizer.SelectedItem.Value) == 2)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
                divVendor.Visible = true;
                divInternal.Visible = false;
                ddlInternal.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }
            else if (Convert.ToInt32(ddlTrainerOrganizer.SelectedItem.Value) == 3)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
                divVendor.Visible = false;
                divInternal.Visible = false;
                ddlInternal.SelectedValue = "00000000-0000-0000-0000-000000000000";
                ddlVendor.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
                divInternal.Visible = false;
                divVendor.Visible = false;
                ddlInternal.SelectedValue = "00000000-0000-0000-0000-000000000000";
                ddlVendor.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }
        }

        protected void ddlInternal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCertificate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dllType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (Convert.ToInt32(dllType.SelectedItem.Value) == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
                divRegulation.Visible = true;
                divGovInstitut.Visible = true;
                divValidityPeriod.Visible = true;
                divAddInfo.Visible = true;
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
                divRegulation.Visible = false;
                divGovInstitut.Visible = false;
                divValidityPeriod.Visible = false;
                divAddInfo.Visible = false;
                txtRegulation.Value = "";
                txtGovernmentInstitution.Value = "";
                txtValidityPeriod.Value = "";
                
            }
            
        }

        protected void CheckUploadDocumentCompliance()
        {
            if (FileUploadAddInfo.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadAddInfo.PostedFiles.Count();
                if (filecount <= 6)
                {
                    foreach (HttpPostedFile postfiles in FileUploadAddInfo.PostedFiles)
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
                                        GetCodeUploadFile();
                                        serverfolder = Server.MapPath("~/UploadDocumentTraining/Compliance/" + "/" + txtUploadCode.Value.Trim() + "/");
                                        //check Folder avlalible or not  
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // create Folder  
                                            Directory.CreateDirectory(serverfolder);
                                        }
                                        serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                        FileUploadAddInfo.SaveAs(serverpath);
                                        label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded  successfully<br/>";
                                        hlbComplianceOK.Value = "OK";
                                        break;

                                }

                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
                }

            }
            else
            {
                hlbComplianceOK.Value = "OK";
            }

        }

        protected void CheckUploadDocumentSupport()
        {
            if (FileUploadSupportingDoc.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadSupportingDoc.PostedFiles.Count();
                if (filecount <= 6)
                {
                    foreach (HttpPostedFile postfiles in FileUploadSupportingDoc.PostedFiles)
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
                                        GetCodeUploadFile();
                                        serverfolder = Server.MapPath("~/UploadDocumentTraining/Support_Document/" + "/" + txtUploadCode.Value.Trim() + "/");
                                        //check Folder avlalible or not  
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // create Folder  
                                            Directory.CreateDirectory(serverfolder);
                                        }
                                        serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                        FileUploadSupportingDoc.SaveAs(serverpath);
                                        label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded successfully<br/>";
                                        hlbSupportingOK.Value = "OK";
                                        break;

                                }

                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
                }

            }
            else
            {
                hlbSupportingOK.Value = "OK";

                //string serverfolderUpdate = string.Empty;
                //GetCodeTraining();
                //serverfolderUpdate = Server.MapPath("~/UploadDocumentTraining/Support_Document/" + "/" + txtCodeTraining.Value.Trim() + "/");
                ////check Folder avlalible or not  
                //if (!Directory.Exists(serverfolderUpdate))
                //{
                //    // create Folder  
                //    Directory.CreateDirectory(serverfolderUpdate);
                //}
            }

        }

        protected void GetCodeTraining()
        {
            string GetInitialTitle;
            int a;
            GetInitialTitle = GetInitials(txtTrainingTitle.Value.Trim());

            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CountCodeTitle");
            sqlcomm.Parameters.AddWithValue("@training_code", GetInitialTitle);

            sqlcomm.ExecuteNonQuery();
            SqlDataReader dr = sqlcomm.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    txtCodeTraining.Value = GetInitialTitle + "-" + "";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    txtCodeTraining.Value = GetInitialTitle + "-" + a.ToString();
                }
            }
        }

        protected void GetCodeTrainingEdit()
        {
            if (txtTrainingTitleEdit.Value != txtTrainingTitle.Value)
            {
                string GetInitialTitle;
                int a;
                GetInitialTitle = GetInitials(txtTrainingTitle.Value.Trim());

                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Training";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "CountCodeTitle");
                sqlcomm.Parameters.AddWithValue("@training_code", GetInitialTitle);

                sqlcomm.ExecuteNonQuery();
                SqlDataReader dr = sqlcomm.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        txtCodeTraining.Value = GetInitialTitle + "-" + "";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        txtCodeTraining.Value = GetInitialTitle + "-" + a.ToString();
                    }
                }
            }
            else
            {
                txtCodeTraining.Value = txtCodeTrainingEdit.Value;
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlTrainerOrganizer.SelectedItem.Text== "<Select Trainer>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectTrainerOrganizer();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else if (ddlMethod.SelectedItem.Text == "<Select Method>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectMethod();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else if (ddlCertificate.SelectedItem.Text == "<Select>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectCertificate();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else if (dllType.SelectedItem.Text == "<Select Type>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectType();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else if (txtTrainingTitle.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldTainingTitle();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else if (txtTrainingStartDate.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldStartDate();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else if (txtTrainingEndDate.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldEndDate();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else if (txtLocation.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "Fieldlocation();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else if (txtCost.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldCost();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
                GetDataTraining();
            }
            else
            {
                CheckUploadDocumentCompliance();
                CheckUploadDocumentSupport();

                if (hlbSupportingOK.Value == "OK" && hlbComplianceOK.Value == "OK")
                {
                    GetCodeTraining();
                    //Save Data Vendor to DB
                    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                    SqlConnection Con = new SqlConnection(path);
                    Con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "sp_HRIS_DB_Training";
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Connection = Con;
                    sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                    sqlcomm.Parameters.AddWithValue("@training_code", txtCodeTraining.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@training_title", txtTrainingTitle.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@training_start_date", txtTrainingStartDate.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@training_end_date", txtTrainingEndDate.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerOrganizer.SelectedItem.Text);
                    sqlcomm.Parameters.AddWithValue("@id_trainer_vendor", ddlVendor.SelectedValue);
                    sqlcomm.Parameters.AddWithValue("@id_trainer_internal", ddlInternal.SelectedValue);
                    sqlcomm.Parameters.AddWithValue("@location", txtLocation.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@method", ddlMethod.SelectedItem.Text);
                    sqlcomm.Parameters.AddWithValue("@certificate", ddlCertificate.SelectedItem.Text);
                    sqlcomm.Parameters.AddWithValue("@training_type", dllType.SelectedItem.Text);
                    sqlcomm.Parameters.AddWithValue("@regulation", txtRegulation.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@government_institution", txtGovernmentInstitution.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@validity_period", txtValidityPeriod.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@cost", txtCost.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@status", "ACTIVE");
                    sqlcomm.Parameters.AddWithValue("@createby", txtCreateBy.Value.Trim());
                    //sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
                    //sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@upload_code", txtUploadCode.Value.Trim());

                    sqlcomm.ExecuteNonQuery();

                    GetDataTraining();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                    Con.Close();
                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;
                    //btnRefresh.Visible = true;
                    //btnNew.Visible = true;
                    //txtTrainingStartDate.Disabled = true;
                    //txtTrainingEndDate.Disabled = true;
                    //txtLocation.Disabled = true;
                    //txtCost.Disabled = true;
                    //btnGetCode.Enabled = false;
                }
            }
            
        }

        protected void CheckUploadDocumentComplianceUpdate()
        {
            if (FileUploadAddInfo.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadAddInfo.PostedFiles.Count();
                if (filecount <= 6)
                {
                    foreach (HttpPostedFile postfiles in FileUploadAddInfo.PostedFiles)
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
                                        if (txtUploadCodeEdit.Value == "&nbsp;")
                                        {
                                            GetCodeUploadFile();
                                            serverfolder = Server.MapPath("~/UploadDocumentTraining/Compliance/" + "/" + txtUploadCode.Value.Trim() + "/");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            FileUploadAddInfo.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded  successfully<br/>";
                                            hlbComplianceOK.Value = "OK";
                                        }
                                        else
                                        {
                                            serverfolder = Server.MapPath("~/UploadDocumentTraining/Compliance/" + "/" + txtUploadCodeEdit.Value.Trim() + "/");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            FileUploadAddInfo.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded  successfully<br/>";
                                            hlbComplianceOK.Value = "OK";
                                        }
                                        break;


                                }

                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
                }

            }
            else
            {
                hlbComplianceOK.Value = "OK";
            }

        }

        protected void CheckUploadDocumentSupportUpdate()
        {
            if (FileUploadSupportingDoc.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadSupportingDoc.PostedFiles.Count();
                if (filecount <= 6)
                {
                    foreach (HttpPostedFile postfiles in FileUploadSupportingDoc.PostedFiles)
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
                                        if (txtUploadCodeEdit.Value == "&nbsp;")
                                        {
                                            GetCodeUploadFile();
                                            serverfolder = Server.MapPath("~/UploadDocumentTraining/Support_Document/" + "/" + txtUploadCode.Value.Trim() + "/");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            FileUploadSupportingDoc.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded successfully<br/>";
                                            hlbSupportingOK.Value = "OK";
                                        }
                                        else
                                        {
                                            serverfolder = Server.MapPath("~/UploadDocumentTraining/Support_Document/" + "/" + txtUploadCodeEdit.Value.Trim() + "/");
                                            //check Folder avlalible or not  
                                            if (!Directory.Exists(serverfolder))
                                            {
                                                // create Folder  
                                                Directory.CreateDirectory(serverfolder);
                                            }
                                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                            FileUploadSupportingDoc.SaveAs(serverpath);
                                            label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded successfully<br/>";
                                            hlbSupportingOK.Value = "OK";
                                        }
                                        break;

                                }

                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
                }

            }
            else
            {
                hlbSupportingOK.Value = "OK";

                //string serverfolderUpdate = string.Empty;
                //GetCodeTraining();
                //serverfolderUpdate = Server.MapPath("~/UploadDocumentTraining/Support_Document/" + "/" + txtCodeTraining.Value.Trim() + "/");
                ////check Folder avlalible or not  
                //if (!Directory.Exists(serverfolderUpdate))
                //{
                //    // create Folder  
                //    Directory.CreateDirectory(serverfolderUpdate);
                //}
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dllType.SelectedItem.Text == "Compliance")
            {
                CheckUploadDocumentComplianceUpdate();
                if(hlbComplianceOK.Value == "OK")
                {
                    if (txtUploadCodeEdit.Value == "&nbsp;")
                    {
                        GetCodeUploadFile();
                        GetCodeTrainingEdit();
                        string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                        SqlConnection Con = new SqlConnection(path);
                        Con.Open();
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.CommandText = "sp_HRIS_DB_Training";
                        sqlcomm.CommandType = CommandType.StoredProcedure;
                        sqlcomm.Connection = Con;
                        sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
                        sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_code", txtCodeTraining.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_title", txtTrainingTitle.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_start_date", txtTrainingStartDate.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_end_date", txtTrainingEndDate.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerOrganizer.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@id_trainer_vendor", ddlVendor.SelectedValue);
                        sqlcomm.Parameters.AddWithValue("@id_trainer_internal", ddlInternal.SelectedValue);
                        sqlcomm.Parameters.AddWithValue("@location", txtLocation.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@method", ddlMethod.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@certificate", ddlCertificate.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@training_type", dllType.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@regulation", txtRegulation.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@government_institution", txtGovernmentInstitution.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@validity_period", txtValidityPeriod.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@cost", txtCost.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                        sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());
                        sqlcomm.Parameters.AddWithValue("@upload_code", txtUploadCode.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Text.ToString());
                        sqlcomm.ExecuteNonQuery();

                        GetDataTraining();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                        Con.Close();
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                    }
                    else
                    {
                        GetCodeTrainingEdit();
                        string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                        SqlConnection Con = new SqlConnection(path);
                        Con.Open();
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.CommandText = "sp_HRIS_DB_Training";
                        sqlcomm.CommandType = CommandType.StoredProcedure;
                        sqlcomm.Connection = Con;
                        sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
                        sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_code", txtCodeTraining.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_title", txtTrainingTitle.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_start_date", txtTrainingStartDate.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_end_date", txtTrainingEndDate.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerOrganizer.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@id_trainer_vendor", ddlVendor.SelectedValue);
                        sqlcomm.Parameters.AddWithValue("@id_trainer_internal", ddlInternal.SelectedValue);
                        sqlcomm.Parameters.AddWithValue("@location", txtLocation.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@method", ddlMethod.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@certificate", ddlCertificate.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@training_type", dllType.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@regulation", txtRegulation.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@government_institution", txtGovernmentInstitution.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@validity_period", txtValidityPeriod.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@cost", txtCost.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                        sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());
                        sqlcomm.Parameters.AddWithValue("@upload_code", txtUploadCodeEdit.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Text.ToString());

                        sqlcomm.ExecuteNonQuery();

                        GetDataTraining();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                        Con.Close();
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                    }
                }
            }
            else
            {
                CheckUploadDocumentSupportUpdate();
                if (hlbSupportingOK.Value == "OK")
                {
                    if (txtUploadCodeEdit.Value == "&nbsp;")
                    {
                        GetCodeUploadFile();
                        GetCodeTrainingEdit();
                        string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                        SqlConnection Con = new SqlConnection(path);
                        Con.Open();
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.CommandText = "sp_HRIS_DB_Training";
                        sqlcomm.CommandType = CommandType.StoredProcedure;
                        sqlcomm.Connection = Con;
                        sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
                        sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_code", txtCodeTraining.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_title", txtTrainingTitle.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_start_date", txtTrainingStartDate.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_end_date", txtTrainingEndDate.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerOrganizer.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@id_trainer_vendor", ddlVendor.SelectedValue);
                        sqlcomm.Parameters.AddWithValue("@id_trainer_internal", ddlInternal.SelectedValue);
                        sqlcomm.Parameters.AddWithValue("@location", txtLocation.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@method", ddlMethod.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@certificate", ddlCertificate.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@training_type", dllType.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@regulation", txtRegulation.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@government_institution", txtGovernmentInstitution.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@validity_period", txtValidityPeriod.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@cost", txtCost.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                        sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());
                        sqlcomm.Parameters.AddWithValue("@upload_code", txtUploadCode.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Text.ToString());

                        sqlcomm.ExecuteNonQuery();

                        GetDataTraining();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                        Con.Close();
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                    }
                    else
                    {
                        GetCodeTrainingEdit();
                        string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                        SqlConnection Con = new SqlConnection(path);
                        Con.Open();
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.CommandText = "sp_HRIS_DB_Training";
                        sqlcomm.CommandType = CommandType.StoredProcedure;
                        sqlcomm.Connection = Con;
                        sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
                        sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_code", txtCodeTraining.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_title", txtTrainingTitle.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_start_date", txtTrainingStartDate.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@training_end_date", txtTrainingEndDate.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerOrganizer.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@id_trainer_vendor", ddlVendor.SelectedValue);
                        sqlcomm.Parameters.AddWithValue("@id_trainer_internal", ddlInternal.SelectedValue);
                        sqlcomm.Parameters.AddWithValue("@location", txtLocation.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@method", ddlMethod.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@certificate", ddlCertificate.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@training_type", dllType.SelectedItem.Text);
                        sqlcomm.Parameters.AddWithValue("@regulation", txtRegulation.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@government_institution", txtGovernmentInstitution.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@validity_period", txtValidityPeriod.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@cost", txtCost.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                        sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());
                        sqlcomm.Parameters.AddWithValue("@upload_code", txtUploadCodeEdit.Value.Trim());
                        sqlcomm.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Text.ToString());

                        sqlcomm.ExecuteNonQuery();

                        GetDataTraining();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                        Con.Close();
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                    }
                }

            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
            GetDataTraining();
           
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
            //btnRefresh.Visible = true;
            //btnNew.Visible = false;
            //btnGetCode.Enabled = false;

            //txtTrainingStartDate.Disabled = false;
            //txtTrainingEndDate.Disabled = false;
            //txtLocation.Disabled = false;
            //txtCost.Disabled = false;
            divStatus.Visible = true;
            ddlStatus.SelectedItem.Text = row.Cells[18].Text;
            hlbID.Value = row.Cells[2].Text.ToString();
            txtCodeTrainingEdit.Value = row.Cells[3].Text.ToString();
            txtCodeTraining.Value = row.Cells[3].Text.ToString();
            txtTrainingTitleEdit.Value = row.Cells[4].Text.ToString();
            txtTrainingTitle.Value = row.Cells[4].Text.ToString();
            txtTrainingStartDate.Value = row.Cells[5].Text.ToString();
            txtTrainingEndDate.Value = row.Cells[6].Text.ToString();
            ddlTrainerOrganizer.SelectedItem.Text = row.Cells[7].Text.ToString();
            ddlVendor.SelectedItem.Text = row.Cells[8].Text;
            ddlVendor.SelectedValue = row.Cells[23].Text;
            ddlInternal.SelectedItem.Text = row.Cells[9].Text;
            ddlInternal.SelectedValue = row.Cells[24].Text;
            txtLocation.Value = row.Cells[10].Text;
            ddlMethod.SelectedItem.Text = row.Cells[11].Text;
            ddlCertificate.SelectedItem.Text = row.Cells[12].Text;
            dllType.SelectedItem.Text = row.Cells[13].Text;
            txtRegulation.Value = row.Cells[14].Text.ToString();
            txtGovernmentInstitution.Value = row.Cells[15].Text.ToString();
            txtValidityPeriod.Value = row.Cells[16].Text.ToString();
            txtCost.Value = row.Cells[17].Text.ToString();
            txtCreateBy.Value = row.Cells[19].Text.ToString();
            txtCreateDate.Value = row.Cells[20].Text.ToString();
            txtModifiedBy.Value = row.Cells[21].Text.ToString();
            txtModifiedDate.Value = row.Cells[22].Text.ToString();
            txtUploadCodeEdit.Value = row.Cells[25].Text.ToString();

            if (ddlTrainerOrganizer.SelectedItem.Text == "INTERNAL")
            {
                divInternal.Visible = true;
                divVendor.Visible = false;
                ddlVendor.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }
            else if (ddlTrainerOrganizer.SelectedItem.Text == "VENDOR")
            {
                divInternal.Visible = false;
                divVendor.Visible = true;
                ddlInternal.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                divInternal.Visible = false;
                divVendor.Visible = false;
                ddlInternal.SelectedValue = "00000000-0000-0000-0000-000000000000";
                ddlVendor.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }

            if (dllType.SelectedItem.Text == "Compliance")
            {
                divRegulation.Visible = true;
                divGovInstitut.Visible = true;
                divValidityPeriod.Visible = true;
                divAddInfo.Visible = true;
            }
            else
            {
                divRegulation.Visible = false;
                divGovInstitut.Visible = false;
                divValidityPeriod.Visible = false;
                divAddInfo.Visible = false;
                txtRegulation.Value = "";
                txtGovernmentInstitution.Value = "";
                txtValidityPeriod.Value = "";
            }
            if (txtUploadCodeEdit.Value == "&nbsp;")
            {

            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            txtUploadCodeEdit.Value = row.Cells[25].Text.ToString();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlDocumentDownload').modal();", true);
            GetDataTraining();
        }

        protected void TableTraining_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableTraining_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableTraining_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void btnGetCode_Click(object sender, EventArgs e)
        //{
        //    //txtTrainingStartDate.Disabled = false;
        //    //txtTrainingEndDate.Disabled = false;
        //    //txtLocation.Disabled = false;
        //    //txtCost.Disabled = false;
            

            

        //}

        static string GetInitials(string name)
        {
            // StringSplitOptions.RemoveEmptyEntries excludes empty spaces returned by the Split method

            string[] nameSplit = name.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            string initials = "";

            foreach (string item in nameSplit)
            {
                initials += item.Substring(0, 1).ToUpper();
            }

            return initials;
        }

        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
            
        //}

        protected void ddlDocDownload_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btDownload_Click(object sender, EventArgs e)
        {
           
            string zipFileName = ddlDocDownload.SelectedItem.Text.ToString() + "-" + txtUploadCodeEdit.Value + ".zip";
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "fileName=" + zipFileName);
            byte[] buffer = new byte[4096];
            ZipOutputStream zipOutputStream = new ZipOutputStream(Response.OutputStream);
            zipOutputStream.SetLevel(3);
            try
            {
                DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/UploadDocumentTraining" + "/" + ddlDocDownload.SelectedItem.Text.ToString() + "/" + txtUploadCodeEdit.Value));
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

        //protected void btnNew_Click(object sender, EventArgs e)
        //{
        //    txtTrainingTitle.Disabled = false;
        //    btnNew.Visible = false;
        //    btnGetCode.Enabled = true;
        //}

        protected void btnGenExcel_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
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
                    wb.Worksheets.Add(dt, "Data Master Training");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Data Master Training" + "_" + _vDate1 + ".xlsx");
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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewTraining').modal();", true);
            GetDataTraining();
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("training_registration.aspx");
        }

        protected void btnCloseModalDownload_Click(object sender, EventArgs e)
        {
            Response.Redirect("training_registration.aspx");
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}