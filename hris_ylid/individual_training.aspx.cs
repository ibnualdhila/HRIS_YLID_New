using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class individual_training : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                GetTrainingTitle();
                //GetDataParticipantTraining();
                GetEmployees();
                //GetDataTrainingRecord();
                //txtCreateBy.Value = Session["Fullname"].ToString();
                //txtCreateDate.Value = DateTime.Now.ToString();
                //txtModifiedBy.Value = Session["Fullname"].ToString();
                //txtModifiedDate.Value = DateTime.Now.ToString();
                btnRemove.Attributes.Add("onclick", "javascript:return DeleteConfirm()");
                btnUpdateStatusBatch.Attributes.Add("onclick", "javascript:return CloseConfirm()");
            }
        }

        protected void GetDataParticipantTraining()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewTrainingParticipant");
            sqlcomm.Parameters.AddWithValue("@id_training_title", hlbID.Value.Trim());
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableParticipant.DataSource = dtb;
            TableParticipant.DataBind();

            TableParticipant.Columns[1].Visible = false;
            TableParticipant.Columns[2].Visible = false;
            

            TableParticipant.UseAccessibleHeader = true;
            TableParticipant.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetEmployees()
        {
            ddlEmployees.Items.Clear();
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
                newItem.Text = "<Select Participant>";
                newItem.Value = "0";
                ddlEmployees.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["fullname"].ToString();
                    newItem.Value = dr["nik"].ToString();
                    ddlEmployees.Items.Add(newItem);
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

        protected void GetDetailTraining()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddDetailTraining");
            sqlcomm.Parameters.AddWithValue("@training_code", ddlTrainingTitle.SelectedValue);
            
            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("id", (Guid)dr["id"]);
                Session.Add("training_code", (string)dr["training_code"]);
                Session.Add("training_title", (string)dr["training_title"]);
                Session.Add("training_start_date", (DateTime)dr["training_start_date"]);
                Session.Add("training_end_date", (DateTime)dr["training_end_date"]);
                Session.Add("trainer_type", (string)dr["trainer_type"]);
                Session.Add("Vendor Trainer", (string)dr["Vendor Trainer"].ToString());
                Session.Add("Internal Trainer", (string)dr["Internal Trainer"].ToString());
                Session.Add("location", (string)dr["location"]);
                Session.Add("method", (string)dr["method"]);
                Session.Add("certificate", (string)dr["certificate"]);
                Session.Add("training_type", (string)dr["training_type"]);
                Session.Add("regulation", (string)dr["regulation"].ToString());
                Session.Add("government_institution", (string)dr["government_institution"].ToString());
                Session.Add("validity_period", (string)dr["validity_period"].ToString());
                Session.Add("cost", (string)dr["cost"]);
                Session.Add("status", (string)dr["status"]);
                Session.Add("createby", (string)dr["createby"]);
                Session.Add("create_date", (DateTime)dr["create_date"]);
                Session.Add("modifiedby", (string)dr["modifiedby"]);
                Session.Add("modified_date", (DateTime)dr["modified_date"]);
            }
            else
            {
                
            }
        }

        protected void GetTrainingTitle()
        {
            ddlTrainingTitle.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddTrainingTittle");

            SqlDataReader dr;

            try
            {

                ListItem newItem = new ListItem();
                newItem.Text = "<Select Training>";
                newItem.Value = "0";
                ddlTrainingTitle.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["training_title"].ToString() /*+ " (" +dr["training_code"].ToString() +")"*/;
                    newItem.Value = dr["training_code"].ToString();
                    ddlTrainingTitle.Items.Add(newItem);
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

        protected void ddlTrainingTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTrainingTitle.SelectedItem.Text== "<Select Training>")
            {
                Response.Redirect("individual_training.aspx");
            }
            else
            {
                divParticipantList.Visible = true;
                GetDetailTraining(); 
                txtTrainerType.Value = Session["trainer_type"].ToString();
                hlbID.Value = Session["id"].ToString();
                txtCodeTraining.Value = Session["training_code"].ToString();
                txtTrainingStartDate.Value = Session["training_start_date"].ToString();
                txtTrainingEndDate.Value = Session["training_end_date"].ToString();
                txtLocation.Value = Session["location"].ToString();
                txtInternalTraining.Value = Session["Internal Trainer"].ToString();
                txtVendorTraining.Value = Session["Vendor Trainer"].ToString();
                txtMethod.Value = Session["method"].ToString();
                txtTrainingType.Value = Session["training_type"].ToString();
                txtCertificateAvailable.Value = Session["certificate"].ToString();
                txtCost.Value = Session["cost"].ToString();
                GetDataParticipantTraining();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlTableEmployees').modal();", true);
            GetEmployees();
        }

        protected void DeleteRecord(int empid)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DeleteRecord");
            sqlcomm.Parameters.AddWithValue("@nik_participant", empid);
            sqlcomm.Parameters.AddWithValue("@id_training_title", hlbID.Value);

            sqlcomm.ExecuteNonQuery();
            Con.Close();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow in TableParticipant.Rows)
            {
                //Searching CheckBox("chkSelect") in an individual row of Grid  
                CheckBox chkdel = (CheckBox)grow.FindControl("chkSelect");
                //If CheckBox is checked than delete the record with particular empid  
                if (chkdel.Checked)
                {
                    int empid = Convert.ToInt32(grow.Cells[3].Text);
                    DeleteRecord(empid);
                }
            }
            //Displaying the Data in GridView  
            GetDataParticipantTraining();
        }

        protected void TableParticipant_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableParticipant_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableParticipant_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool CheckParticipantDuplicate(string nik, string id_training)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckDuplicateParticipant");
            sqlcomm.Parameters.AddWithValue("@nik_participant", nik);
            sqlcomm.Parameters.AddWithValue("@id_training_title", id_training);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("nik_participant", (string)dr["nik_participant"]);
                Session.Add("id_training_title", (Guid)dr["id_training_title"]);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isexistsduplicate = CheckParticipantDuplicate(ddlEmployees.SelectedValue, hlbID.Value.ToString());

            if (ddlEmployees.SelectedItem.Text.ToString() == "<Select Participant>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedAddParticipant();", true);
            }
            else if (isexistsduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateParticipant();", true);
            }
            else
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@id_training_title", hlbID.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@nik_participant", ddlEmployees.SelectedValue);
                sqlcomm.Parameters.AddWithValue("@date_complete", SqlDateTime.Null);
                sqlcomm.Parameters.AddWithValue("@createby", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@create_date", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());

                sqlcomm.ExecuteNonQuery();

                GetDataParticipantTraining();
                //GetDataTrainingRecord();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                Con.Close();
            }
        }

        protected void btnEditStatus_Click(object sender, EventArgs e)
        {
            GetDataParticipantTraining();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlStatusParticipant').modal();", true);
            hlbNIK.Value = row.Cells[3].Text.ToString();

        }

        protected void btnUpdateBatch_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUpdateBatch').modal();", true);
        }

        protected void CheckUploadCertificate()
        {
            if (FileUploadCertificate.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadCertificate.PostedFiles.Count();
                if (filecount <= 6)
                {
                    foreach (HttpPostedFile postfiles in FileUploadCertificate.PostedFiles)
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
                                        serverfolder = Server.MapPath("~/UploadDocumentIndividualTraining/Certificate/" + txtCodeTraining.Value.Trim() + "/" + hlbNIK.Value.Trim() + "/");
                                        //check Folder avlalible or not  
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // create Folder  
                                            Directory.CreateDirectory(serverfolder);
                                        }
                                        serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                        FileUploadCertificate.SaveAs(serverpath);
                                        //label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded successfully<br/>";
                                        hlbCertificateOK.Value = "OK";
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
                hlbCertificateOK.Value = "OK";
            }

        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (ddlTrainingResult.SelectedItem.Text.ToString() == "<Select Result>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectResult();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlStatusParticipant').modal();", true);
                GetDataParticipantTraining();
            }
            else
            {
                CheckUploadCertificate();
                if (hlbCertificateOK.Value == "OK")
                {
                    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                    SqlConnection Con = new SqlConnection(path);
                    Con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Connection = Con;
                    sqlcomm.Parameters.AddWithValue("@StatementType", "UpdateIndividualTrainingStatus");
                    sqlcomm.Parameters.AddWithValue("@nik_participant", hlbNIK.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@training_result", ddlTrainingResult.SelectedItem.Text.ToString());
                    sqlcomm.Parameters.AddWithValue("@date_complete", txtCompletionDate.Value.ToString());
                    sqlcomm.Parameters.AddWithValue("@note", txtNote.Value.ToString());
                    sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                    sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());
                    sqlcomm.Parameters.AddWithValue("@id_training_title", hlbID.Value);

                    sqlcomm.ExecuteNonQuery();

                    GetDataParticipantTraining();
                    //GetDataTrainingRecord();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                    Con.Close();
                }
            }
            
                
        }

        protected void ddlTrainingResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckUploadAttendance()
        {
            if (FileUploadAttendance.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadAttendance.PostedFiles.Count();
                if (filecount <= 6)
                {
                    foreach (HttpPostedFile postfiles in FileUploadAttendance.PostedFiles)
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
                                        serverfolder = Server.MapPath("~/UploadDocumentIndividualTraining/Attendance/" + "/" + txtCodeTraining.Value.Trim() + "/");
                                        //check Folder avlalible or not  
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // create Folder  
                                            Directory.CreateDirectory(serverfolder);
                                        }
                                        serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
                                        FileUploadAttendance.SaveAs(serverpath);
                                        //label1.Text += "[" + postfiles.FileName + "]- pdf file uploaded successfully<br/>";
                                        AttendanceOK.Value = "OK";
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedUpload();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUpdateBatch').modal();", true);
            }

        }

        public bool CheckParticipantStatus(string id_training_title)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "select * from IndividualTraining where id_training_title=@id_training_title and training_status='In Progress'";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@id_training_title", id_training_title);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("id_training_title", (string)dr["id_training_title"].ToString());
                //Session.Add("username", (string)dr["username"]);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnUpdateStatusBatch_Click(object sender, EventArgs e)
        {
            bool chekparticipant = CheckParticipantStatus(hlbID.Value);
            
            if (chekparticipant)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedCloseBatch();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUpdateBatch').modal();", true);
            }
            else
            {
                CheckUploadAttendance();
                if (AttendanceOK.Value == "OK")
                {
                    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                    SqlConnection Con = new SqlConnection(path);
                    Con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Connection = Con;
                    sqlcomm.Parameters.AddWithValue("@StatementType", "UpdateBatch");
                    sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());

                    sqlcomm.ExecuteNonQuery();

                    GetDataParticipantTraining();
                    //GetDataTrainingRecord();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
                    Con.Close();
                }
            }
            
        }

        protected void TableTrainingRecord_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableTrainingRecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableTrainingRecord_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnImportFile_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlImportExcel').modal();", true);
            GetDataParticipantTraining();
        }

        private DateTime ConvertDateFormat(string _dateString)
        {
            DateTime _dateValue;
            DateTime _datecomplete;
            if (DateTime.TryParse(_dateString, out _dateValue))
            {

                if (DateTime.TryParseExact(_dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _datecomplete))
                {
                    _dateValue = _datecomplete;
                    return _dateValue;
                }
                else
                { return _dateValue; }
            }
            else
            {
                _dateValue = DateTime.MinValue;
                return _dateValue;
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            

            string path = string.Concat(Server.MapPath("~/UploadFiles/" + FileUploadImportExcel.FileName));
            FileUploadImportExcel.SaveAs(path);

            

            #region DefineTable
            DataTable dt = new DataTable();
            dt.Columns.Add("nik_participant", typeof(System.String));
            dt.Columns.Add("training_status", typeof(System.String));
            dt.Columns.Add("training_result", typeof(System.String));
            dt.Columns.Add("date_complete", typeof(System.DateTime));
            #endregion

            XLWorkbook _vWBUpload = new XLWorkbook(path);
            foreach (IXLWorksheet _vWorksheet in _vWBUpload.Worksheets)
            {
                int _ExcelRow = 2;

                while (_vWorksheet.Cell(_ExcelRow, 2).Value.ToString().Length > 0)
                {

                    DataRow _dr = dt.NewRow();

                    //var _test = ConvertDateFormat(_vWorksheet.Cell(_ExcelRow, 3).Value.ToString());

                    #region DefineValue
                    _dr["nik_participant"] = _vWorksheet.Cell(_ExcelRow, 1).Value.ToString();
                    _dr["training_status"] = _vWorksheet.Cell(_ExcelRow, 5).Value.ToString();
                    _dr["training_result"] = _vWorksheet.Cell(_ExcelRow, 4).Value.ToString();
                    _dr["date_complete"] = ConvertDateFormat(_vWorksheet.Cell(_ExcelRow, 3).Value.ToString());//_vWorksheet.Cell(_ExcelRow, 3).Value.ToString();
                    #endregion

                    dt.Rows.Add(_dr);

                    _ExcelRow = _ExcelRow + 1;
                }

                string pathdb = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(pathdb);
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;

                Con.Open();
                
                foreach (DataRow _dr1 in dt.Rows)
                {
                    //string getnik = _dr1["nik_participant"].ToString().Substring(4, _dr1["nik_participant"].ToString().Length - 4);
                    #region ParametersValue
                    sqlcomm.Parameters.AddWithValue("@StatementType", "SaveImportExcel");
                    sqlcomm.Parameters.AddWithValue("@id_training_title", hlbID.Value.Trim());
                    sqlcomm.Parameters.AddWithValue("@nik_participant", _dr1["nik_participant"]);
                    sqlcomm.Parameters.AddWithValue("@training_status", _dr1["training_status"]);
                    sqlcomm.Parameters.AddWithValue("@training_result", _dr1["training_result"]);
                    sqlcomm.Parameters.AddWithValue("@date_complete", _dr1["date_complete"]);
                    sqlcomm.Parameters.AddWithValue("@createby", Session["Fullname"].ToString());
                    //sqlcomm.Parameters.AddWithValue("@create_date", DateTime.Now.ToString());
                    sqlcomm.Parameters.AddWithValue("@modifiedby", Session["Fullname"].ToString());
                    //sqlcomm.Parameters.AddWithValue("@modified_date", DateTime.Now.ToString());
                    #endregion

                    sqlcomm.ExecuteNonQuery();

                    sqlcomm.Parameters.Clear();

                    GetDataParticipantTraining();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "UploadSuccess();", true);


                }
                Con.Close();
                File.Delete(Server.MapPath("~/UploadFiles/" + FileUploadImportExcel.FileName));
            }
        }

        protected void btnCloseModalImport_Click(object sender, EventArgs e)
        {
            Response.Redirect("individual_training.aspx");
        }
    }
}