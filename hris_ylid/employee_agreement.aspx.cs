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
    public partial class employee_agreement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                GetPIC();
                GetEmployees();
                GetDataEmployeeAgreement();

                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                
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

        protected void GetEmployees()
        {
            ddlEmployee.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "GetEmployees");

            SqlDataReader dr;

            try
            {

                ListItem newItem = new ListItem();
                newItem.Text = "<Select Name>";
                newItem.Value = "0";
                ddlEmployee.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["fullname"].ToString();
                    newItem.Value = dr["nik"].ToString();
                    ddlEmployee.Items.Add(newItem);
                }
                dr.Close();
            }
            catch (Exception err)
            {
                Con.Close();
                string _ErrorMsg = err.Message;
            }
            finally
            {
                Con.Close();
            }
        }

        protected void GetDetailEmployee()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailEmployee");
            sqlcomm.Parameters.AddWithValue("@nik", ddlEmployee.SelectedValue);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    txtEmployeeStatus.Value = (string)dr["status"];
                    txtWorkingStatus.Value = (string)dr["category"];
                    txtLocation.Value = (string)dr["Location"];
                    hlbCodeLocation.Value = (string)dr["code_location"];
                    txtJoinDate.Value = (string)dr["join_date"];
                    //Session.Add("id", (Guid)dr["id"]);
                    //Session.Add("fullname", (string)dr["fullname"]);
                    //Session.Add("nik", (string)dr["nik"]);
                    //Session.Add("Location", (string)dr["Location"]);
                    //Session.Add("status", (string)dr["status"]);
                    //Session.Add("category", (string)dr["category"]);
                    //Session.Add("join_date", (DateTime)dr["join_date"]);
                    //Session.Add("date_of_birth", (DateTime)dr["date_of_birth"]);

                }

            }
            else
            {
                Con.Close();
            }

        }

        protected void GetDataEmployeeAgreement()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployeeAgreement.DataSource = dtb;
            TableEmployeeAgreement.DataBind();

            TableEmployeeAgreement.Columns[2].Visible = false;
            //TableEmployeeAgreement.Columns[4].Visible = false;
            TableEmployeeAgreement.Columns[10].Visible = false;

            TableEmployeeAgreement.Columns[15].Visible = false;
            TableEmployeeAgreement.Columns[16].Visible = false;
            TableEmployeeAgreement.Columns[17].Visible = false;
            TableEmployeeAgreement.Columns[23].Visible = false;
            TableEmployeeAgreement.Columns[24].Visible = false;

            TableEmployeeAgreement.UseAccessibleHeader = true;
            TableEmployeeAgreement.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void TableEmployeeAgreement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployee.SelectedItem.Text == "<Select Name>")
            {
                Response.Redirect("employee_agreement.aspx");
            }
            else
            {
                GetDetailEmployee();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
                GetDataEmployeeAgreement();
            }
        }

        protected void ddlPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
            GetDataEmployeeAgreement();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
            GetDataEmployeeAgreement();
        }

        protected void ddlReminder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
            GetDataEmployeeAgreement();
        }

        protected void ddlStartNotificationEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
            GetDataEmployeeAgreement();
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
                                        serverfolder = Server.MapPath("~/UploadEmployeeAgreement/" + txtAgreementNumber.Value.Trim() + "/");
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
           if (ddlStartNotificationEmail.SelectedItem.Text == "<Select Start>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "CheckNotifEmail();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
                GetDataEmployeeAgreement();
            }
            else if (txtAgreementNumber.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldAgreementNumber();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
                GetDataEmployeeAgreement();
            }
            else if (txtEndOfAgreementDate.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldAgreementDate();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
                GetDataEmployeeAgreement();
            }
            else if (txtDescription.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldDescription();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
                GetDataEmployeeAgreement();
            }
            else if (txtEmailAddress.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldEmailAddress1();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
                GetDataEmployeeAgreement();
            }
            else if (txtEmailAddress2.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldEmailAddress2();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
                GetDataEmployeeAgreement();
            }
            else if (txtEmailAddress3.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FieldEmailAddress3();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
                GetDataEmployeeAgreement();
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
                    sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Connection = Con;
                    sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                    sqlcomm.Parameters.AddWithValue("@agreement_number", txtAgreementNumber.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@nik_employee", ddlEmployee.SelectedValue.ToString());
                    sqlcomm.Parameters.AddWithValue("@nik_pic", ddlPIC.SelectedValue.ToString());
                    sqlcomm.Parameters.AddWithValue("@end_of_agreement_date", txtEndOfAgreementDate.Value.ToString());
                    sqlcomm.Parameters.AddWithValue("@description", txtDescription.Value.ToString());
                    sqlcomm.Parameters.AddWithValue("@code_location", hlbCodeLocation.Value.ToString());
                    //sqlcomm.Parameters.AddWithValue("@reminder", ddlReminder.SelectedItem.Text.ToString());
                    sqlcomm.Parameters.AddWithValue("@notification_to_email", ddlStartNotificationEmail.SelectedItem.Text.ToString());
                    sqlcomm.Parameters.AddWithValue("@email", txtEmailAddress.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@email2", txtEmailAddress2.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@email3", txtEmailAddress3.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@status", "ACTIVE");
                    sqlcomm.Parameters.AddWithValue("@createby", txtCreateBy.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

                    sqlcomm.ExecuteNonQuery();
                    Con.Close();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                    GetDataEmployeeAgreement();
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
                sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
                sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@agreement_number", txtAgreementNumber.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@end_of_agreement_date", txtEndOfAgreementDate.Value.ToString());
                sqlcomm.Parameters.AddWithValue("@description", txtDescription.Value.ToString());
                //sqlcomm.Parameters.AddWithValue("@reminder", ddlReminder.SelectedItem.Text.ToString());
                sqlcomm.Parameters.AddWithValue("@notification_to_email", ddlStartNotificationEmail.SelectedItem.Text.ToString());
                sqlcomm.Parameters.AddWithValue("@email", txtEmailAddress.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@email2", txtEmailAddress2.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@email3", txtEmailAddress3.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Text.ToString());
                sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@nik_employee", hlbNIK.Value);

                sqlcomm.ExecuteNonQuery();
                Con.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                GetDataEmployeeAgreement();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                
            }
        }

        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("employee_agreement.aspx");
        //}

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
            GetDataEmployeeAgreement();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
           
            divStatusAgreement.Visible = true;
            ddlEmployee.Enabled = false;
            
            hlbID.Value = row.Cells[2].Text;
            ddlPIC.SelectedItem.Text = row.Cells[11].Text;
            ddlPIC.SelectedValue = row.Cells[10].Text;
            ddlEmployee.SelectedItem.Text = row.Cells[5].Text;
            ddlEmployee.SelectedValue = row.Cells[4].Text;
            hlbNIK.Value = row.Cells[4].Text;
            //ddlReminder.SelectedItem.Text = row.Cells[15].Text;
            ddlStartNotificationEmail.SelectedItem.Text = row.Cells[16].Text;
            txtAgreementNumber.Value = row.Cells[3].Text;
            txtDescription.Value = row.Cells[13].Text;
            txtEndOfAgreementDate.Value = row.Cells[12].Text;
            txtEmployeeStatus.Value = row.Cells[8].Text;
            txtJoinDate.Value = row.Cells[7].Text;
            txtLocation.Value = row.Cells[6].Text;
            txtWorkingStatus.Value = row.Cells[9].Text;
            txtEmailAddress.Value = row.Cells[17].Text;
            ddlStatus.SelectedItem.Text = row.Cells[18].Text;
            txtCreateBy.Value = row.Cells[19].Text;
            txtCreateDate.Value = row.Cells[20].Text;
            txtModifiedBy.Value = row.Cells[21].Text;
            txtModifiedDate.Value = row.Cells[22].Text;
            txtEmailAddress2.Value = row.Cells[23].Text;
            txtEmailAddress3.Value = row.Cells[24].Text;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string _Path = Server.MapPath("~/UploadEmployeeAgreement" + "/" + row.Cells[3].Text + "/");
            if (Directory.Exists(_Path))
            {

                string zipFileName = row.Cells[3].Text + ".zip";
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "fileName=" + zipFileName);
                byte[] buffer = new byte[4096];
                ZipOutputStream zipOutputStream = new ZipOutputStream(Response.OutputStream);
                zipOutputStream.SetLevel(3);
                //try
                //{
                DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/UploadEmployeeAgreement" + "/" + row.Cells[3].Text + "/"));
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
            else //catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DownloadFailed();", true); //throw;
            }
        }

        protected void btnGenExcel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlGenerateExcel').modal();", true);
            GetDataEmployeeAgreement();
        }

        protected void btnSubmitGenerate_Click(object sender, EventArgs e)
        {
            if (txtStartDate.Value == "" || txtEndDate.Value == "")
            {
                GetDataEmployeeAgreement();
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "GenerateExcelAll");

                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                using (DataTable dt = new DataTable())
                {
                    //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                    //sFileName = sFileName.Replace("/", "");
                    sda.Fill(dt);
                    string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Employee Agreement Record");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                        Response.ClearContent();
                        Response.AppendHeader("content-disposition", "attachment; filename=" + "Employee Agreement Record" + "_" + _vDate1 + ".xlsx");
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
                GetDataEmployeeAgreement();
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
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
                        wb.Worksheets.Add(dt, "Employee Agreement Record");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                        Response.ClearContent();
                        Response.AppendHeader("content-disposition", "attachment; filename=" + "Employee Agreement Record" + "_" + _vDate1 + ".xlsx");
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

        protected void TableEmployeeAgreement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Int32 daytoexpired;
                daytoexpired = Convert.ToInt32(e.Row.Cells[14].Text.ToString());
                if (daytoexpired < 0 && e.Row.Cells[8].Text == "ACTIVE" && e.Row.Cells[9].Text != "PERMANENT" && e.Row.Cells[18].Text == "ACTIVE")
                {
                    e.Row.Cells[14].Text = "End of Agreement";
                    e.Row.Cells[14].ForeColor = Color.Red;

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
                    e.Row.Cells[12].ForeColor = Color.Red;
                    e.Row.Cells[13].ForeColor = Color.Red;
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
                    
                }
                else if (daytoexpired < 0 && e.Row.Cells[8].Text=="NON ACTIVE")
                {
                    e.Row.Cells[14].Text = "End of Agreement";
                }
                else if (daytoexpired < 0 && e.Row.Cells[9].Text == "PERMANENT")
                {
                    e.Row.Cells[14].Text = "End of Agreement";
                }
                else if (daytoexpired > 0)
                {
                    e.Row.Cells[14].Text = daytoexpired.ToString();
                }
                else
                {
                    e.Row.Cells[14].Text = "End of Agreement";
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployeeAgreement').modal();", true);
            GetDataEmployeeAgreement();
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("employee_agreement.aspx");
        }

        protected void btnCloseModalGenerateRange_Click(object sender, EventArgs e)
        {
            Response.Redirect("employee_agreement.aspx");
        }
    }
}