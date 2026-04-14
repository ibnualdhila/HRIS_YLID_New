using ClosedXML.Excel;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class company_agreement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            //GetDataCompanyAgreement();
            if (!IsPostBack)
            {
                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();

                GetVendorName();
                GetPIC();
                GetLocation();
                GetDataCompanyAgreement();

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                
            }
                
        }

        protected void GetLocation()
        {
            ddlLocationCode.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddLocation");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "<Select Location>";
                newItem.Value = "0";
                ddlLocationCode.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["branch_name"].ToString();
                    newItem.Value = dr["code_branch"].ToString();
                    ddlLocationCode.Items.Add(newItem);
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

        protected void GetDataCompanyAgreement()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableCompanyAgreement.DataSource = dtb;
            TableCompanyAgreement.DataBind();

            TableCompanyAgreement.Columns[2].Visible = false;
            TableCompanyAgreement.Columns[5].Visible = false;
            TableCompanyAgreement.Columns[23].Visible = false;

            TableCompanyAgreement.Columns[13].Visible = false;
            TableCompanyAgreement.Columns[14].Visible = false;
            TableCompanyAgreement.Columns[15].Visible = false;
            TableCompanyAgreement.Columns[24].Visible = false;
            TableCompanyAgreement.Columns[25].Visible = false;

            TableCompanyAgreement.UseAccessibleHeader = true;
            TableCompanyAgreement.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetVendorName()
        {
            ddlVendor.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Institutions";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddInstitutionname");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "<Select Institution>";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlVendor.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["institution_name"].ToString();
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

        protected void GetPIC()
        {
            ddlPIC.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "PICNameHR");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "<Select PIC>";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlPIC.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["fullname"].ToString();
                    newItem.Value = dr["nik"].ToString();
                    ddlPIC.Items.Add(newItem);
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

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
            GetDataCompanyAgreement();
        }

        protected void ddlPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
            GetDataCompanyAgreement();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
            GetDataCompanyAgreement();
        }

        protected void ddlReminder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
            GetDataCompanyAgreement();
        }

        protected void ddlStartNotificationEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
            GetDataCompanyAgreement();
        }

        protected void CheckUploadDocument()
        {
            if (FileUploadDocument.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadDocument.PostedFiles.Count();
                if (filecount <= 3)
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
                                        serverfolder = Server.MapPath("~/UploadCompanyAgreement/" + txtAgreementNumber.Value.Trim() + "/");
                                        //check Folder avlalible or not  
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // create Folder  
                                            Directory.CreateDirectory(serverfolder);
                                        }
                                        serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                        FileUploadDocument.SaveAs(serverpath);
                                        label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded  successfully<br/>";
                                        hlbUploadOK.Value = "OK";
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
                hlbUploadOK.Value = "OK";
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //DateTime EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Value);
            //DateTime ExpiredDate = Convert.ToDateTime(txtExpirationDate.Value);

            //double TotalDayToExpired = ExpiredDate.Subtract(EffectiveDate).TotalDays;
            if (txtAgreementNumber.Value=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldAgreementNumber();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (txtNameAggreement.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldNameAgreement();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (txtVendorServices.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldInstitutionServices();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (txtEffectiveDate.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldEffective();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (txtExpirationDate.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldExpiration();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (txtRemaks.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldRemark();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (txtEmailAddress.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldEmailAddress1();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (txtEmailAddress2.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldEmailAddress2();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (txtEmailAddress3.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldEmailAddress3();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (ddlVendor.SelectedItem.Text== "<Select Vendor>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "CheckVendor();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (ddlPIC.SelectedItem.Text == "<Select PIC>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "CheckPIC();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else if (ddlStartNotificationEmail.SelectedItem.Text == "<Select Start>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "CheckNotifEmail();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
                GetDataCompanyAgreement();
            }
            else
            {
                CheckUploadDocument();
                if (hlbUploadOK.Value == "OK")
                {
                    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                    SqlConnection Con = new SqlConnection(path);
                    Con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Connection = Con;
                    sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                    sqlcomm.Parameters.AddWithValue("@agreement_number", txtAgreementNumber.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@agreement_name", txtNameAggreement.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@id_institution", ddlVendor.SelectedValue.ToString());
                    sqlcomm.Parameters.AddWithValue("@institution_services", txtVendorServices.Value.ToString());
                    sqlcomm.Parameters.AddWithValue("@nik_pic", ddlPIC.SelectedValue.ToString());
                    sqlcomm.Parameters.AddWithValue("@effective_date", txtEffectiveDate.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@expiration_date", txtExpirationDate.Value.Trim());
                    //sqlcomm.Parameters.AddWithValue("@reminder", ddlReminder.SelectedItem.Text.ToString());
                    sqlcomm.Parameters.AddWithValue("@notification_to_email", ddlStartNotificationEmail.SelectedItem.Text.ToString());
                    sqlcomm.Parameters.AddWithValue("@email", txtEmailAddress.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@email2", txtEmailAddress2.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@email3", txtEmailAddress3.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@remaks", txtRemaks.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@location", ddlLocationCode.SelectedValue.ToString());
                    sqlcomm.Parameters.AddWithValue("@status", "ACTIVE");
                    sqlcomm.Parameters.AddWithValue("@createby", txtCreateBy.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

                    sqlcomm.ExecuteNonQuery();
                    Con.Close();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                    GetDataCompanyAgreement();
                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;
                    
                }
            }
            
                
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            CheckUploadDocument();
            if (hlbUploadOK.Value == "OK")
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
                sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@agreement_number", txtAgreementNumber.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@agreement_name", txtNameAggreement.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@id_institution", ddlVendor.SelectedValue.ToString());
                sqlcomm.Parameters.AddWithValue("@institution_services", txtVendorServices.Value.ToString());
                sqlcomm.Parameters.AddWithValue("@nik_pic", ddlPIC.SelectedValue.ToString());
                sqlcomm.Parameters.AddWithValue("@effective_date", txtEffectiveDate.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@expiration_date", txtExpirationDate.Value.Trim());
                //sqlcomm.Parameters.AddWithValue("@reminder", ddlReminder.SelectedItem.Text.ToString());
                sqlcomm.Parameters.AddWithValue("@notification_to_email", ddlStartNotificationEmail.SelectedItem.Text.ToString());
                sqlcomm.Parameters.AddWithValue("@email", txtEmailAddress.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@email2", txtEmailAddress2.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@email3", txtEmailAddress3.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@remaks", txtRemaks.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@location", ddlLocationCode.SelectedValue.ToString());
                sqlcomm.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Text.ToString());
                sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());

                sqlcomm.ExecuteNonQuery();
                Con.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                GetDataCompanyAgreement();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                
            }
        }

        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("company_agreement.aspx");
        //}

        protected void TableCompanyAgreement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Int32 daytoexpired;
                daytoexpired = Convert.ToInt32(e.Row.Cells[12].Text.ToString());
                if (daytoexpired > 0)
                {
                    e.Row.Cells[12].Text = daytoexpired.ToString();
                }
                else
                {
                    e.Row.Cells[12].Text = "Has Expired";
                    e.Row.Cells[12].ForeColor = Color.Red;

                    e.Row.Cells[2].ForeColor = Color.Red;
                    e.Row.Cells[3].ForeColor = Color.Red;
                    e.Row.Cells[4].ForeColor = Color.Red;
                    e.Row.Cells[5].ForeColor = Color.Red;
                    e.Row.Cells[6].ForeColor = Color.Red;
                    e.Row.Cells[7].ForeColor = Color.Red;
                    e.Row.Cells[8].ForeColor = Color.Red;
                    e.Row.Cells[9].ForeColor = Color.Red;
                    e.Row.Cells[10].ForeColor = Color.Red;
                    e.Row.Cells[11].ForeColor = Color.Red;
                    e.Row.Cells[13].ForeColor = Color.Red;
                    e.Row.Cells[14].ForeColor = Color.Red;
                    e.Row.Cells[15].ForeColor = Color.Red;
                    e.Row.Cells[16].ForeColor = Color.Red;
                    e.Row.Cells[17].ForeColor = Color.Red;
                    e.Row.Cells[18].ForeColor = Color.Red;
                    e.Row.Cells[19].ForeColor = Color.Red;
                    e.Row.Cells[20].ForeColor = Color.Red;
                    e.Row.Cells[21].ForeColor = Color.Red;
                    e.Row.Cells[22].ForeColor = Color.Red;
                    e.Row.Cells[23].ForeColor = Color.Red;
                    e.Row.Cells[24].ForeColor = Color.Red;
                    e.Row.Cells[25].ForeColor = Color.Red;
                }
            }
        }

        protected void TableCompanyAgreement_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableCompanyAgreement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
            GetDataCompanyAgreement();
            divStatus.Visible = true;
            //GetVendorName();
            //GetPIC();
            //GetDataCompanyAgreement();
            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
            
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hlbID.Value= row.Cells[2].Text;
            ddlPIC.SelectedItem.Text = row.Cells[9].Text;
            ddlPIC.SelectedValue = row.Cells[8].Text;
            ddlVendor.SelectedItem.Text = row.Cells[6].Text;
            ddlVendor.SelectedValue = row.Cells[5].Text;
            //ddlReminder.SelectedItem.Text = row.Cells[13].Text;
            ddlStartNotificationEmail.SelectedItem.Text = row.Cells[14].Text;
            txtAgreementNumber.Value = row.Cells[3].Text;
            txtNameAggreement.Value = row.Cells[4].Text;
            txtEffectiveDate.Value = row.Cells[10].Text;
            txtExpirationDate.Value = row.Cells[11].Text;
            ddlLocationCode.SelectedValue = row.Cells[23].Text;
            ddlLocationCode.SelectedItem.Text = row.Cells[17].Text;
            txtRemaks.Value = row.Cells[16].Text;
            txtVendorServices.Value = row.Cells[7].Text;
            txtEmailAddress.Value = row.Cells[15].Text;
            txtEmailAddress2.Value = row.Cells[24].Text;
            txtEmailAddress3.Value = row.Cells[25].Text;
            ddlStatus.SelectedItem.Text = row.Cells[18].Text;
            txtCreateBy.Value = row.Cells[19].Text;
            txtCreateDate.Value = row.Cells[20].Text;
            txtModifiedBy.Value = row.Cells[21].Text;
            txtModifiedDate.Value = row.Cells[22].Text;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;


            //try
            //{
            string _Path = Server.MapPath("~/UploadCompanyAgreement" + "/" + row.Cells[3].Text + "/");
            if (Directory.Exists(_Path))
            {
                string zipFileName = row.Cells[3].Text + ".zip";
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "fileName=" + zipFileName);
                byte[] buffer = new byte[4096];
                ZipOutputStream zipOutputStream = new ZipOutputStream(Response.OutputStream);
                zipOutputStream.SetLevel(3);

                DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/UploadCompanyAgreement" + "/" + row.Cells[3].Text + "/"));
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
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DownloadFailed();", true);
            }
            //}
            //catch (Exception)
            //{
            //    throw;
                
            //}
        }

        protected void btnGenExcel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlGenerateExcel').modal();", true);
            GetDataCompanyAgreement();
        }

        protected void ddlLocationCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmitGenerate_Click(object sender, EventArgs e)
        {
            if (txtStartDate.Value == "" || txtEndDate.Value == "")
            {
                GetDataCompanyAgreement();
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "GenerateExcelAll");
                sqlcomm.Parameters.AddWithValue("@DateFrom", txtStartDate.Value);
                sqlcomm.Parameters.AddWithValue("@DateTo", txtEndDate.Value);

                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                using (DataTable dt = new DataTable())
                {
                    //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                    //sFileName = sFileName.Replace("/", "");
                    sda.Fill(dt);
                    string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Company Agreement Record");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                        Response.ClearContent();
                        Response.AppendHeader("content-disposition", "attachment; filename=" + "Company Agreement Record" + "_" + _vDate1 + ".xlsx");
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
            else
            {
                GetDataCompanyAgreement();
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "GenerateExcelRangeDate");
                sqlcomm.Parameters.AddWithValue("@DateFrom", txtStartDate.Value);
                sqlcomm.Parameters.AddWithValue("@DateTo", txtEndDate.Value);

                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                using (DataTable dt = new DataTable())
                {
                    //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                    //sFileName = sFileName.Replace("/", "");
                    sda.Fill(dt);
                    string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Company Agreement Record");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                        Response.ClearContent();
                        Response.AppendHeader("content-disposition", "attachment; filename=" + "Company Agreement Record" + "_" + _vDate1 + ".xlsx");
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
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewCompanyAgreement').modal();", true);
            GetDataCompanyAgreement();
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("company_agreement.aspx");
        }

        protected void btnCloseModalGenerateRange_Click(object sender, EventArgs e)
        {
            Response.Redirect("company_agreement.aspx");
        }
    }
}