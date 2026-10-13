using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.Word;

//using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Drawing.Charts;
//using DocumentFormat.OpenXml.Vml.Office;
using Newtonsoft.Json;
//using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class employeess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
                {
                    Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }

                if (Session["ImportSuccess"] != null)
                {

                    string message = Session["ImportSuccess"].ToString();
                    message = System.Web.HttpUtility.JavaScriptStringEncode(message);
                    Session.Remove("ImportSuccess");

                    ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                          $@"
                        setTimeout(function() {{
                            if (window.Toastify) {{
                                Toastify({{
                                    text: '{message}',
                                    duration: 4000,
                                    gravity: 'top',
                                    position: 'center',
                                    close: true,
                                    style: {{ background: '#50C878' }}
                                }}).showToast();
                            }} else {{
                                alert('{message}');
                            }}

                            setTimeout(function(){{
                                window.location.href = '{ResolveUrl("~/DownloadImportLog.aspx")}';
                            }}, 1000);

                        }}, 200);
                        ", true);

                }


                if (Session["ImportError"] != null)
                {
                    string message = Session["ImportError"].ToString().Replace("'", "\\'");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastError",$"showErrorToast('{message}');",true);

                    Session.Remove("ImportError");
                }



                BindDataTableEmployees();
                //GetPositionName();
                //GetDivisionName();
                //GetSectionName();
                //GetSubSectionName();
                //GetReportPositionName();
                //GetLocation();
                //GetInstitutionName();
                //BindDataTableCandidateJoined();



                divIntitution.Visible = false;

                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();

                btnNew.Visible = true;
                btnSubmit.Visible = false;
                //btnUpdate.Visible = false;
                //btnAdd.Visible = false;

                txtNIK.Disabled = true;
                txtJoinDate.Disabled = true;


                

            }
        }

        protected void GetInstitutionName()
        {
            ddlInstitution.Items.Clear();
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
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlInstitution.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["institution_name"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlInstitution.Items.Add(newItem);
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

        protected void BindDataTableCandidateJoined()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewCandidateJoined");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            if (dtb.Rows.Count <= 0)
            {
                dtb.Rows.Add(dtb.NewRow());
            }
            TableCandidateJoin.DataSource = dtb;
            TableCandidateJoin.DataBind();

            TableCandidateJoin.UseAccessibleHeader = true;
            TableCandidateJoin.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();

        }

        protected void BindDataTableEmployees()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            if (dtb.Rows.Count <= 0)
            {
                dtb.Rows.Add(dtb.NewRow());
            }
            ViewState["myViewState"] = dtb;
            TableEmployees.DataSource = dtb;
            TableEmployees.DataBind();

            //TableEmployees.Columns[0].Visible = false;
            TableEmployees.Columns[2].Visible = false;
            //TableEmployees.Columns[3].Visible = false;

            //TableEmployees.Columns[22].Visible = false;
            //TableEmployees.Columns[23].Visible = false;
            //TableEmployees.Columns[24].Visible = false;
            TableEmployees.Columns[25].Visible = false;
            TableEmployees.Columns[26].Visible = false;
            TableEmployees.Columns[27].Visible = false;
            TableEmployees.Columns[28].Visible = false;
            TableEmployees.Columns[29].Visible = false;
            TableEmployees.Columns[30].Visible = false;
            //TableEmployees.Columns[31].Visible = false;

            TableEmployees.UseAccessibleHeader = true;
            TableEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }
        private void BindDataTableCareerHistory(GridView _Grid1, string _vNIK)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_Employees", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "HistoryMutationEmployee");
                sqlcomm.Parameters.AddWithValue("@nik", _vNIK);


                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["myViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }
                    _Grid1.DataSource = dtb;

                    _Grid1.DataBind();

                    _Grid1.UseAccessibleHeader = true;
                    _Grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    _Grid1.Columns[0].Visible = false;

                }
            }
        }
        private void BindDataTableEmergencyContact(GridView _Grid1, string _vID)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_Employees", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "ViewEmergencyContact");
                sqlcomm.Parameters.AddWithValue("@id", _vID);

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["myViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }
                    _Grid1.DataSource = dtb;
                    _Grid1.DataBind();
                    _Grid1.UseAccessibleHeader = true;
                    _Grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //_Grid1.Columns[1].Visible = false;

                }
            }
        }
        protected void BindDataTableHistoryTrainingEmployee(GridView _GridTraining)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_IndividualTraining", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "ViewParticipantTrainingDetail");
                sqlcomm.Parameters.AddWithValue("@nik_participant", hlbDetailNIK.Text);

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["myViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }
                    _GridTraining.DataSource = dtb;
                    _GridTraining.DataBind();

                    _GridTraining.UseAccessibleHeader = true;
                    _GridTraining.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void BindDataTableHistoryMutationEmployee(GridView _GridTraining)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_Employees", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "HistoryMutationEmployee");
                sqlcomm.Parameters.AddWithValue("@nik", hlbDetailNIK.Text);

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["myViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }

                    _GridTraining.DataSource = dtb;
                    _GridTraining.DataBind();

                    _GridTraining.UseAccessibleHeader = true;
                    _GridTraining.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void GetMaster()
        {
            ddlCity.Items.Clear();
            ddlProvince.Items.Clear();
            ddlNationality.Items.Clear();
            //ddlPOB.Items.Clear();
            //ddl1stChildPOB.Items.Clear();
            //ddl2ndChildPOB.Items.Clear();
            //ddl3rdChildPOB.Items.Clear();
            //ddlSpousePOB.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();

            sqlcomm.CommandText = "sp_HRIS_DB_Master";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            Con.Open();
            // Country
            try
            {
                //if (_Parameter == "Country")
                //{

                sqlcomm.Parameters.Clear();
                sqlcomm.Parameters.AddWithValue("@StatementType", "Country");

                SqlDataReader dr;

                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlNationality.Items.Add(newItem);

                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["Name"].ToString();
                    newItem.Value = dr["Oid"].ToString();
                    ddlNationality.Items.Add(newItem);
                }
                dr.Close();
                //}
                //if (_Parameter == "City")
                //{

                sqlcomm.Parameters.Clear();
                sqlcomm.Parameters.AddWithValue("@StatementType", "City");

                SqlDataReader dr1;

                ListItem newItem1 = new ListItem();
                newItem1.Text = "";
                newItem1.Value = "00000000-0000-0000-0000-000000000000";
                ddlCity.Items.Add(newItem1);
                //ddl1stChildPOB.Items.Add(newItem1);
                //ddl2ndChildPOB.Items.Add(newItem1);
                //ddl3rdChildPOB.Items.Add(newItem1);
                //ddlSpousePOB.Items.Add(newItem1);
                //ddlPOB.Items.Add(newItem1);

                dr1 = sqlcomm.ExecuteReader();

                while (dr1.Read())
                {
                    newItem1 = new ListItem();
                    newItem1.Text = dr1["Name"].ToString();
                    newItem1.Value = dr1["Oid"].ToString();
                    ddlCity.Items.Add(newItem1);
                    //ddlPOB.Items.Add(newItem1);
                    //ddl1stChildPOB.Items.Add(newItem1);
                    //ddl2ndChildPOB.Items.Add(newItem1);
                    //ddl3rdChildPOB.Items.Add(newItem1);
                    //ddlSpousePOB.Items.Add(newItem1);
                }
                dr1.Close();
                //}
                //if (_Parameter == "Province")
                //{

                sqlcomm.Parameters.Clear();
                sqlcomm.Parameters.AddWithValue("@StatementType", "Province");

                SqlDataReader dr2;

                ListItem newItem2 = new ListItem();
                newItem2.Text = "";
                newItem2.Value = "00000000-0000-0000-0000-000000000000";
                ddlProvince.Items.Add(newItem2);

                dr2 = sqlcomm.ExecuteReader();

                while (dr2.Read())
                {
                    newItem2 = new ListItem();
                    newItem2.Text = dr2["Name"].ToString();
                    newItem2.Value = dr2["Oid"].ToString();
                    ddlProvince.Items.Add(newItem2);
                }
                dr2.Close();
                //}
                Con.Close();
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

        protected void GetPositionName()
        {
            ddlPosition.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddPositionName");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlPosition.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["position"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlPosition.Items.Add(newItem);
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

        protected void GetReportPositionName()
        {
            ddlReportPosition.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddPositionName");

            SqlDataReader dr;

            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlReportPosition.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["position"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlReportPosition.Items.Add(newItem);
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

        protected void GetDivisionName()
        {
            ddlDivision.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddDivisionName");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlDivision.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["division_name"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlDivision.Items.Add(newItem);
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

        protected void GetSectionName()
        {
            ddlSection.Items.Clear();
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
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlSection.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["section"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlSection.Items.Add(newItem);
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
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
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

        protected void CheckUploadDocument()
        {
            if (FileUploadEDocs.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                lbErrorUploadNotif.InnerText = ""; // Clear previous messages

                // Check the number of selected files
                filecount = FileUploadEDocs.PostedFiles.Count();
                string[] allowedExtensions = { ".pdf", ".docx", ".xlsx", ".jpg", ".jpeg", ".png" };

                if (filecount <= 1)
                {
                    foreach (HttpPostedFile postfiles in FileUploadEDocs.PostedFiles)
                    {
                        // Get the File Extension
                        string filetype = Path.GetExtension(postfiles.FileName);

                        if (IsValidFileType(filetype, allowedExtensions))
                        {
                            // Get the File Size In Byte
                            double filesize = postfiles.ContentLength;

                            if (filesize < (2048576))
                            {
                                fileuploadcount++;
                                string serverfolder = string.Empty;
                                string serverpath = string.Empty;

                                // Adding File Into Specific Folder Depend On his Extension
                                switch (filetype.ToLower())
                                {
                                    case ".pdf":
                                    case ".docx":
                                    case ".xlsx":
                                    case ".jpg":
                                    case ".jpeg":
                                    case ".png":
                                        string _vNIK = txtNIK.Value;
                                        //string _vCode = txtMAWBNo.Value; //Session["Code"].ToString();
                                        string _vDocType = ddlDocType.SelectedValue;
                                        serverfolder = Server.MapPath("~/eDocs_Files/Employee" + "/" + _vNIK + "/" + _vDocType + "/");

                                        // Ensure the folder is available
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // Create the folder
                                            Directory.CreateDirectory(serverfolder);
                                        }

                                        // Append a unique identifier to avoid overwriting
                                        string uniqueFileName = Path.GetFileNameWithoutExtension(postfiles.FileName)
                                            + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") // Add timestamp
                                            + Path.GetExtension(postfiles.FileName);

                                        serverpath = Path.Combine(serverfolder, uniqueFileName);
                                        postfiles.SaveAs(serverpath);
                                        lbErrorUploadNotif.InnerText += "[" + uniqueFileName + "]- " + filetype + " file uploaded successfully";
                                        lblOK.Value = "OK";
                                        lblFileName.Value = uniqueFileName;
                                        break;
                                }
                            }
                            else
                            {
                                //lbErrorUploadNotif.InnerText += "[" + postfiles.FileName + "]- File not uploaded; size is greater than 2MB. Your File Size is " + (filesize / (1024 * 1034)) + " MB";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "toastr.error('Save failed!,files not uploded size is greater then(2)MB');", true);
                            }
                        }
                        else
                        {
                            //lbErrorUploadNotif.InnerText += "[" + postfiles.FileName + "]- Invalid file type. Allowed types are pdf/docx/xlsx/jpg/jpeg/png";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "toastr.error('Save failed!,file type must be .png or .xlsx or .doc or .pdf and .jpg');", true);
                        }
                    }
                }
                else
                {
                    //lbErrorUploadNotif.InnerText = "You have selected " + filecount + " files. Please select a maximum of 1 files." ;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "toastr.error('Save failed!,Please select Maximum (6) files');", true);
                }
            }
            else
            {
                lbErrorUploadNotif.InnerText = "No Files Upload.";
            }
        }
        private bool IsValidFileType(string fileType, string[] allowedExtensions)
        {
            return Array.Exists(allowedExtensions, ext => ext.Equals(fileType, StringComparison.OrdinalIgnoreCase));
        }
        protected void ViewEdocsFilesUpload()
        {
            string _vNIK = lblNIK.Value;
            

            string _vPath = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection _vSQLCon = new SqlConnection(_vPath);
            SqlCommand _vSQLComm = new SqlCommand();

            _vSQLCon.Open();
            _vSQLComm.Connection = _vSQLCon;
            _vSQLComm.CommandType = CommandType.StoredProcedure;
            _vSQLComm.CommandText = "sp_HRIS_DB_EDocs";

            _vSQLComm.Parameters.AddWithValue("@Flag", "GetMonitoringEDocs");
            _vSQLComm.Parameters.AddWithValue("@NIK", _vNIK);
            _vSQLComm.Parameters.AddWithValue("@Category", "SP");

            DataTable _dt = new DataTable();
            SqlDataReader _dr = _vSQLComm.ExecuteReader();
            _dt.Load(_dr);

            TableeDocsFiles.DataSource = _dt;
            TableeDocsFiles.DataBind();
            TableeDocsFiles.UseAccessibleHeader = true;
            TableeDocsFiles.HeaderRow.TableSection = TableRowSection.TableHeader;

            TableeDocsFiles.Columns[0].Visible = false;
            TableeDocsFiles.Columns[6].Visible = false;
            TableeDocsFiles.Columns[7].Visible = false;

            _dt.Dispose();
            _vSQLCon.Close();
            _vSQLComm.Dispose();
            _vSQLCon.Dispose();

            _vSQLCon.Close();
        }
        protected void GetSubSectionName()
        {
            ddlSubSection.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddSubSectionName");

            SqlDataReader dr;

            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlSubSection.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["sub_section"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlSubSection.Items.Add(newItem);
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

        protected void TableEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {

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


        protected void btnDetail_Click(object sender, EventArgs e)
        {
            // update 14 Juli 2025 
            TableEmployees.Columns[2].Visible = false;
            TableEmployees.Columns[22].Visible = false;
            TableEmployees.Columns[23].Visible = false;
            TableEmployees.Columns[24].Visible = false;
            TableEmployees.Columns[25].Visible = false;
            TableEmployees.Columns[26].Visible = false;
            TableEmployees.Columns[27].Visible = false;
            TableEmployees.Columns[28].Visible = false;
            TableEmployees.Columns[29].Visible = false;
            TableEmployees.Columns[30].Visible = false;

            TableEmployees.UseAccessibleHeader = true;
            TableEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

            //BindDataTableEmployees();
            /////////////////////////////////////////////

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlTableDetailEmployee').modal();", true);
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string vid = TableEmployees.DataKeys[row.RowIndex]["id"].ToString();

            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            //sqlcomm.Parameters.AddWithValue("@StatementType", "HistoryMutationEmployee");
            //sqlcomm.Parameters.AddWithValue("@nik", row.Cells[5].Text);

            //DataTable dtb = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            //sda.Fill(dtb);

            //ViewState["myViewState"] = dtb;
            //TableHistoryMutation.DataSource = dtb;
            //TableHistoryMutation.DataBind();
            //TableHistoryMutation.UseAccessibleHeader = true;
            //if (TableHistoryMutation.HeaderRow != null)
            //{
            //    TableHistoryMutation.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}

            //sqlcomm.Parameters.Clear();

            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewPersonal");
            sqlcomm.Parameters.AddWithValue("@id", row.Cells[2].Text);

            DataTable dtb1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(sqlcomm);
            sda1.Fill(dtb1);
            if (dtb1.Rows.Count <= 0)
            {
                dtb1.Rows.Add(dtb1.NewRow());
            }
            else
            {
                foreach (DataRow dr in dtb1.Rows)
                {
                    if (vid != null) //(dr["id"].ToString() != "")
                    {
                        hlbID.Value = vid;//dr["id"].ToString();
                    }
                    else
                    {
                        hlbID.Value = "0";
                    }

                    lbDetailProbationEndDate.Text = "-";
                    lbRetiredDate.Text = "-";
                    lblContractStartDate.Text = "-";
                    lblContractEndDate.Text = "-";
                    lbJamsostekDetail.Text = "-";
                    lblBPJSKesehatan.Text = "-";
                    lblNPWP.Text = "-";
                    lblEmailOffice.Text = "-";
                    lblSourceOfHire.Text = "-";
                    lblPOB.Text = "-";
                    lblReligion.Text = "-";
                    lblGender.Text = "-";
                    lblBloodType.Text = "-";
                    lblNationality.Text = "-";
                    lbKTPDetail.Text = "-";
                    lbAddressDetail.Text = "-";
                    lbDomicileDetail.Text = "-";
                    lblCity.Text = "-";
                    lblProvince.Text = "-";
                    lblPostalCode.Text = "-";
                    lblMobilePhone.Text = "-";
                    //lblEmergencyContactNo.Text = "-";
                    lblMaritalStatus.Text = "-";
                    lblBankAccount.Text = "-";
                    lblBankAccountName.Text = "-";
                    lblBankName.Text = "-";
                    lblEducation1.Text = "-";
                    lblInstitute1.Text = "-";
                    lblMajor1.Text = "-";
                    lblEducation2.Text = "-";
                    lblInstitute2.Text = "-";
                    lblMajor2.Text = "-";
                    lblNameOfSpouse.Text = "-";
                    lblKTPPasangan.Text = "-";
                    lblSpouseDOB.Text = "-";
                    lblSpousePOB.Text = "-";
                    lblNameOfChild1.Text = "-";
                    lblChild1ID.Text = "-";
                    lblChild1DOB.Text = "-";
                    lblChild1POB.Text = "-";
                    lblNameOfChild2.Text = "-";
                    lblChild2ID.Text = "-";
                    lblChild2DOB.Text = "-";
                    lblChild2POB.Text = "-";
                    lblNameOfChild3.Text = "-";
                    lblChild3ID.Text = "-";
                    lblChild3DOB.Text = "-";
                    lblChild3POB.Text = "-";
                    lblEffectiveEndDate.Text = "-";

                    lblNIK.Value = dr["nik"].ToString();

                    hlbDetailNIK.Text = dr["nik"].ToString(); //row.Cells[5].Text;
                    lbDetailFullname.Text = dr["fullname"].ToString(); //row.Cells[6].Text;
                    lbDetailPosition.Text = dr["Position"].ToString(); //row.Cells[8].Text;
                    lbDetailDivision.Text = dr["Division"].ToString(); //row.Cells[9].Text;
                    lbDetailSection.Text = dr["Section"].ToString(); //row.Cells[10].Text;
                    lbDetailSubSection.Text = dr["SubSection"].ToString(); //row.Cells[11].Text;

                    lblReportPosition.Text= dr["report_position"].ToString();

                    lbDetailLocation.Text = dr["Location"].ToString(); //row.Cells[13].Text;
                    lbDetailJoinDate.Text = DateTime.Parse(dr["join_date"].ToString()).ToString("yyyy-MMM-dd"); //DateTime.Parse(row.Cells[15].Text).ToString("yyyy-MMM-dd");
                    lbDetailEmploymentStatus.Text = dr["category"].ToString();  //row.Cells[4].Text;
                    if (lbDetailEmploymentStatus.Text.ToUpper() == "PERMANENT")
                    {
                        lbDetailProbationEndDate.Text = ConvertDateFormat(dr["join_date"].ToString()).AddDays(90).ToString("yyyy-MMM-dd"); //DateTime.Parse(row.Cells[15].Text).AddDays(90).ToString("yyyy-MMM-dd");
                        lbRetiredDate.Text = ConvertDateFormat(dr["Retired Date"].ToString()).ToString("yyyy-MMM-dd"); //DateTime.Parse(row.Cells[17].Text).ToString("yyyy-MMM-dd");
                    }
                    if (lbDetailEmploymentStatus.Text.ToUpper() == "CONTRACT")
                    {
                        lblContractStartDate.Text = ConvertDateFormat(dr["ContractStartDate"].ToString()).ToString("yyyy-MMM-dd");//ConvertDateFormat(row.Cells[32].Text).ToString("yyyy-MMM-dd");  //DateTime.TryParse(row.Cells[32].Text).ToString("yyyy-MMM-dd");
                        lblContractEndDate.Text = ConvertDateFormat(dr["ContractEndDate"].ToString()).ToString("yyyy-MMM-dd"); //DateTime.Parse(row.Cells[33].Text).ToString("yyyy-MMM-dd");
                    }
                    lbDetailWorkPeriod.Text = dr["WORK PERIOD"].ToString();  //row.Cells[16].Text;
                    lbJamsostekDetail.Text = dr["id_bpjamsostek"].ToString();  //row.Cells[29].Text;
                    lblBPJSKesehatan.Text = dr["BPJSKSNo"].ToString();
                    lblNPWP.Text = dr["NPWPNo"].ToString();
                    lblEmailOffice.Text = dr["WorkingEmail"].ToString();
                    lblSourceOfHire.Text = dr["SourceOfHire"].ToString();
                    lblInstitution.Text = dr["institution_name"].ToString();
                    lblCareerHistory.Text = dr["CareerHistory"].ToString();

                    lblPOB.Text = dr["POB"].ToString();
                    lblDOB.Text = ConvertDateFormat(dr["date_of_birth"].ToString()).ToString("yyyy-MMM-dd");
                    int _Age = DateTime.Now.Year - ConvertDateFormat(dr["date_of_birth"].ToString()).Year;
                    lblAge.Text = _Age.ToString();
                    lblReligion.Text = dr["Religion"].ToString();
                    lblGender.Text = dr["Gender"].ToString();
                    lblBloodType.Text = dr["BloodType"].ToString();
                    lblNationality.Text = dr["Nationality"].ToString();
                    lbKTPDetail.Text = dr["id_ktp"].ToString();  //row.Cells[14].Text;
                    lbAddressDetail.Text = dr["address"].ToString();  //row.Cells[27].Text;
                    lbDomicileDetail.Text = dr["domicile"].ToString();  //row.Cells[28].Text;
                    lblCity.Text = dr["City"].ToString();
                    lblProvince.Text = dr["Province"].ToString();
                    lblPostalCode.Text = dr["PostalCode"].ToString();
                    lblMobilePhone.Text = dr["MobilePhone"].ToString();
                    //lblEmergencyContactNo.Text = dr["EmergencyContactNumber"].ToString();
                    lblMaritalStatus.Text = dr["MaritalStatus"].ToString();
                    lblBankAccount.Text = dr["BankAccount"].ToString();
                    lblBankAccountName.Text = dr["BankAccountName"].ToString();
                    lblBankName.Text = dr["BankName"].ToString();

                    lblEmailPersonal.Text = dr["PersonalEmail"].ToString();
                    lblFamilyCardNo.Text = dr["FamilyCardNumber"].ToString();

                    if (dr["Education1"].ToString().Length > 0 && dr["Education1"].ToString() != "<Select Value>") { lblEducation1.Text = dr["Education1"].ToString(); }
                    if (dr["Institute1"].ToString().Length > 0 && dr["Institute1"].ToString() != "<Select Value>") { lblInstitute1.Text = dr["Institute1"].ToString(); }
                    if (dr["Major1"].ToString().Length > 0 && dr["Major1"].ToString() != "<Select Value>") { lblMajor1.Text = dr["Major1"].ToString(); }//lblMajor1.Text = dr["Major1"].ToString();
                    if (dr["Education2"].ToString().Length > 0 && dr["Education2"].ToString() != "<Select Value>") { lblEducation2.Text = dr["Education2"].ToString(); }//lblEducation2.Text = dr["Education2"].ToString();
                    if (dr["Institute2"].ToString().Length > 0 && dr["Institute2"].ToString() != "<Select Value>") { lblInstitute2.Text = dr["Institute2"].ToString(); }//lblInstitute2.Text = dr["Institute2"].ToString();
                    if (dr["Major2"].ToString().Length > 0 && dr["Major2"].ToString() != "<Select Value>") { lblMajor2.Text = dr["Major2"].ToString(); }//lblMajor2.Text = dr["Major2"].ToString();

                    lblNameOfSpouse.Text = dr["NameOfSpouse"].ToString();
                    lblKTPPasangan.Text = dr["NoIDSpouse"].ToString();
                    lblSpouseDOB.Text = ConvertDateFormat(dr["DOBSpouse"].ToString()).ToString("yyyy-MMM-dd");
                    lblSpousePOB.Text = dr["POBSpouse"].ToString();

                    lblNameOfChild1.Text = dr["NameOf1stChild"].ToString();
                    lblChild1ID.Text = dr["NoID1stChild"].ToString();
                    lblChild1DOB.Text = ConvertDateFormat(dr["DOB1stChild"].ToString()).ToString("yyyy-MMM-dd");
                    lblChild1POB.Text = dr["POB1stChild"].ToString();

                    lblNameOfChild2.Text = dr["NameOf2ndChild"].ToString();
                    lblChild2ID.Text = dr["NoID2ndChild"].ToString();
                    lblChild2DOB.Text = ConvertDateFormat(dr["DOB2ndChild"].ToString()).ToString("yyyy-MMM-dd");
                    lblChild2POB.Text = dr["POB2ndChild"].ToString();

                    lblNameOfChild3.Text = dr["NameOf3rdChild"].ToString();
                    lblChild3ID.Text = dr["NoID3rdChild"].ToString();
                    lblChild3DOB.Text = ConvertDateFormat(dr["DOB2ndChild"].ToString()).ToString("yyyy-MMM-dd");
                    lblChild3POB.Text = dr["POB3rdChild"].ToString();

                    lblEffectiveEndDate.Text = ConvertDateFormat(dr["EffectiveEndDate"].ToString()).ToString("yyyy-MMM-dd");


                }
            }
            Con.Close();
            //hlbDetailNIK.Text = row.Cells[5].Text;
            //lbDetailFullname.Text = row.Cells[6].Text;
            //lbDetailPosition.Text = row.Cells[8].Text;
            //lbDetailDivision.Text = row.Cells[9].Text;
            //lbDetailSection.Text = row.Cells[10].Text;
            //lbDetailSubSection.Text = row.Cells[11].Text;
            //lbDetailLocation.Text = row.Cells[13].Text;
            //lbDetailJoinDate.Text = DateTime.Parse(row.Cells[15].Text).ToString("yyyy-MMM-dd");
            //lbDetailEmploymentType.Text = row.Cells[4].Text;
            //lbDetailWorkPeriod.Text = row.Cells[16].Text;

            //lbKTPDetail.Text = row.Cells[14].Text;
            //lbJamsostekDetail.Text = row.Cells[29].Text;
            //lbAddressDetail.Text = row.Cells[27].Text;
            //lbDomicileDetail.Text = row.Cells[28].Text;

            //lbDetailProbationEndDate.Text = "-";
            //lbRetiredDate.Text = "-";
            //lblContractStartDate.Text = "-";
            //lblContractEndDate.Text = "-";


            BindDataTableHistoryTrainingEmployee(TableViewUserTraining);
            BindDataTableEmergencyContact(TableEmergencyContact, vid);
            BindDataTableHistoryMutationEmployee(TableHistoryMutation);

            string Fromfile = "~/Photos/" + row.Cells[5].Text + ".jpg";
            PhotoUser.ImageUrl = Fromfile;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            Response.Redirect("employeess_detail.aspx?EmployeeID=" + (row.Cells[2].Text) + "&NIK=" + (row.Cells[5].Text) + "&NewEmployee=false");

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
            //GetPositionName();
            //GetDivisionName();
            //GetSectionName();
            //GetSubSectionName();
            //GetReportPositionName();
            //GetLocation();
            //GetInstitutionName();

            //GetMaster();

            //LinkButton btn = (LinkButton)sender;
            //GridViewRow row = (GridViewRow)btn.NamingContainer;

            //string _vid_Edit = TableEmployees.DataKeys[row.RowIndex]["id"].ToString();

            //hlbID.Value = row.Cells[2].Text;

            //txtNIK.Value = row.Cells[5].Text;
            //lblNIK.Value = txtNIK.Value;

            //ViewEdocsFilesUpload();

            //txtFullname.Value = row.Cells[6].Text;
            //txtJoinDate.Value = row.Cells[15].Text;
            //txtBirthDate.Value = row.Cells[7].Text;

            //string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            //SqlConnection Con = new SqlConnection(path);
            //Con.Open();
            //SqlCommand sqlcomm = new SqlCommand();
            //sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            //sqlcomm.CommandType = CommandType.StoredProcedure;
            //sqlcomm.Connection = Con;

            ////sqlcomm.Parameters.AddWithValue("@StatementType", "ViewEmergencyContact");
            ////sqlcomm.Parameters.AddWithValue("@id", _vid_Edit);// row.Cells[2].Text);
            ////DataTable dtb_EditEmergencyContact = new DataTable();
            ////SqlDataAdapter sda_EditEmergencyContact = new SqlDataAdapter(sqlcomm);

            ////if (dtb_EditEmergencyContact != null)
            ////{
            ////    sda_EditEmergencyContact.Fill(dtb_EditEmergencyContact);
            ////    ViewState["myViewState"] = dtb_EditEmergencyContact;
            ////    TableEditEmergencyContact.DataSource = dtb_EditEmergencyContact;
            ////    TableEditEmergencyContact.DataBind();

            ////    TableEditEmergencyContact.UseAccessibleHeader = true;
            ////    TableEditEmergencyContact.HeaderRow.TableSection = TableRowSection.TableHeader;
            ////    TableEditEmergencyContact.Columns[1].Visible = false;
            ////}
            ////sqlcomm.Parameters.Clear();

            //sqlcomm.Parameters.AddWithValue("@StatementType", "ViewPersonal");
            //sqlcomm.Parameters.AddWithValue("@id", _vid_Edit);// row.Cells[2].Text);
            //DataTable dtb1 = new DataTable();
            //SqlDataAdapter sda1 = new SqlDataAdapter(sqlcomm);
            //sda1.Fill(dtb1);

            //sqlcomm.Dispose();
            //Con.Close();

            //foreach (DataRow dr in dtb1.Rows)
            //{
            //    if (dr["category"].ToString() == "OUTSOURCED")//row.Cells[4].Text == "OUTSOURCED")
            //    {
            //        divIntitution.Visible = true;
            //    }
            //    else
            //    {
            //        divIntitution.Visible = false;
            //    }

            //    if (dr["institution_name"] != null && dr["institution_name"].ToString() != "&nbsp;" && dr["institution_name"].ToString().Length > 0) //row.Cells[31].Text != "&nbsp;")
            //    {
            //        ddlInstitution.SelectedItem.Text = dr["institution_name"].ToString();//row.Cells[31].Text;
            //        divIntitution.Visible = true;
            //    }
            //    else
            //    {
            //        ddlInstitution.SelectedItem.Text = "";
            //        divIntitution.Visible = false;
            //    }

            //    if (dr["id_institution"] != null && dr["id_institution"].ToString() != "&nbsp;" && dr["id_institution"].ToString().Length > 0) // row.Cells[30].Text != "&nbsp;")
            //    {
            //        ddlInstitution.SelectedValue = dr["id_institution"].ToString();//row.Cells[30].Text;
            //        divIntitution.Visible = true;
            //    }
            //    else
            //    {
            //        ddlInstitution.SelectedValue = "00000000-0000-0000-0000-000000000000";
            //        divIntitution.Visible = false;
            //    }


            //    if (dr["Position"] != null && dr["Position"].ToString() != "&nbsp;" && dr["Position"].ToString().Length > 0)  //row.Cells[8].Text != "&nbsp;")
            //    {
            //        ddlPosition.SelectedItem.Text = dr["Position"].ToString(); //row.Cells[8].Text;
            //    }
            //    else
            //    {
            //        ddlPosition.SelectedItem.Text = "";
            //    }
            //    if (dr["id_position"] != null && dr["id_position"].ToString() != "&nbsp;" && dr["id_position"].ToString().Length > 0)  //row.Cells[22].Text != "&nbsp;")
            //    {
            //        ddlPosition.SelectedValue = dr["id_position"].ToString(); //row.Cells[22].Text;
            //    }
            //    else
            //    {
            //        ddlPosition.SelectedValue = "00000000-0000-0000-0000-000000000000";
            //    }

            //    if (dr["Division"] != null || dr["Division"].ToString() != "&nbsp;" && dr["Division"].ToString().Length > 0)  //row.Cells[9].Text != "&nbsp;")
            //    {
            //        ddlDivision.SelectedItem.Text = dr["Division"].ToString(); //row.Cells[9].Text;
            //    }
            //    else
            //    {
            //        ddlDivision.SelectedItem.Text = "";
            //    }
            //    if (dr["id_division"] != null && dr["id_division"].ToString() != "&nbsp;" && dr["id_division"].ToString().Length > 0)  //row.Cells[23].Text != "&nbsp;")
            //    {
            //        ddlDivision.SelectedValue = dr["id_division"].ToString(); // row.Cells[23].Text;
            //    }
            //    else
            //    {
            //        ddlDivision.SelectedValue = "00000000-0000-0000-0000-000000000000";
            //    }

            //    if (dr["Section"] != null && dr["Section"].ToString() != "&nbsp;" && dr["Section"].ToString().Length > 0)  //row.Cells[10].Text != "&nbsp;")
            //    {
            //        ddlSection.SelectedItem.Text = dr["Section"].ToString(); // row.Cells[10].Text;
            //    }
            //    else
            //    {
            //        ddlSection.SelectedItem.Text = "";
            //    }
            //    if (dr["id_section"] != null && dr["id_section"].ToString() != "&nbsp;" && dr["id_section"].ToString().Length > 0)  //row.Cells[24].Text != "&nbsp;")
            //    {
            //        ddlSection.SelectedValue = dr["id_section"].ToString(); // row.Cells[24].Text;
            //    }
            //    else
            //    {
            //        ddlSection.SelectedValue = "00000000-0000-0000-0000-000000000000";
            //    }

            //    if (dr["SubSection"] != null && dr["SubSection"].ToString() != "&nbsp;" && dr["SubSection"].ToString().Length > 0)  //row.Cells[11].Text != "&nbsp;")
            //    {
            //        ddlSubSection.SelectedItem.Text = dr["SubSection"].ToString(); // row.Cells[11].Text;
            //    }
            //    else
            //    {
            //        ddlSubSection.SelectedItem.Text = "";
            //    }
            //    if (dr["id_sub_section"] != null && dr["id_sub_section"].ToString() != "&nbsp;" && dr["id_sub_section"].ToString().Length > 0)  //row.Cells[25].Text != "&nbsp;")
            //    {
            //        ddlSubSection.SelectedValue = dr["id_sub_section"].ToString(); //row.Cells[25].Text;
            //    }
            //    else
            //    {
            //        ddlSubSection.SelectedValue = "00000000-0000-0000-0000-000000000000";
            //    }

            //    if (dr["Location"] != null && dr["Location"].ToString() != "&nbsp;" && dr["Location"].ToString().Length > 0) // row.Cells[13].Text != "&nbsp;")
            //    {
            //        ddlLocationCode.SelectedItem.Text = dr["Location"].ToString(); // row.Cells[13].Text;
            //    }
            //    else
            //    {
            //        ddlLocationCode.SelectedItem.Text = "";
            //    }
            //    if (dr["code_location"] != null && dr["code_location"].ToString() != "&nbsp;" && dr["code_location"].ToString().Length > 0) //row.Cells[26].Text != "&nbsp;")
            //    {
            //        ddlLocationCode.SelectedValue = dr["code_location"].ToString(); // row.Cells[26].Text;
            //    }
            //    else
            //    {
            //        ddlLocationCode.SelectedValue = "00000000-0000-0000-0000-000000000000";
            //    }
            //    if (dr["report_position"] != null && dr["report_position"].ToString() != "&nbsp;" && dr["report_position"].ToString().Length > 0) //row.Cells[12].Text != "&nbsp;")
            //    {
            //        ddlReportPosition.SelectedItem.Text = dr["report_position"].ToString(); // row.Cells[12].Text;
            //    }
            //    else
            //    {
            //        ddlReportPosition.SelectedItem.Text = "";
            //    }

            //    txtAddress.Value = dr["address"].ToString(); //row.Cells[27].Text;
            //    txtDomicile.Value = dr["domicile"].ToString(); //row.Cells[28].Text;
            //    txtBPjamsostekNumber.Value = dr["id_bpjamsostek"].ToString(); //row.Cells[29].Text;
            //    txtBPJSKesehatanNo.Value = dr["BPJSKSNo"].ToString();
            //    txtNPWP.Value = dr["NPWPNo"].ToString();
            //    txtWorkingEmail.Value = dr["WorkingEmail"].ToString();
            //    if (dr["SourceOfHire"] != null && dr["SourceOfHire"].ToString() != "&nbsp;") //row.Cells[12].Text != "&nbsp;")
            //    {
            //        ddlSourceOfHire.SelectedItem.Text = dr["SourceOfHire"].ToString(); // row.Cells[12].Text;
            //    }
            //    else
            //    {
            //        ddlSourceOfHire.SelectedItem.Text = "";
            //    }
            //    txtBankName.Value = dr["BankName"].ToString();
            //    txtBankAccountNo.Value = dr["BankAccount"].ToString();
            //    txtBankAccountName.Value = dr["BankAccountName"].ToString();
            //    txtContractStartDate.Value = dr["ContractStartDate"].ToString();
            //    txtContractEndDate.Value = dr["ContractEndDate"].ToString();

            //    //if (dr["POB"].ToString().Length > 0) { ddlPOB.SelectedValue = dr["POB"].ToString(); } // row.Cells[12].Text; 
            //    txtPOB.Value = dr["POB"].ToString();  // row.Cells[12].Text; 
            //                                          //if (dr["Gender"].ToString().Length > 0) { ddlGender.SelectedItem.Text = dr["Gender"].ToString(); }

            //    if (dr["Gender"].ToString().Length > 0) { if (dr["Gender"].ToString().Substring(0, 4) != "<Sel") { ddlGender.SelectedItem.Text = dr["Gender"].ToString(); }; }
            //    if (dr["BloodType"].ToString().Length > 0) { if (dr["BloodType"].ToString().Substring(0, 1) != "<") { ddlBloodType.SelectedItem.Text = dr["BloodType"].ToString(); }; }

            //    if (dr["status"].ToString().Length > 0) { ddlStatus.SelectedItem.Text = dr["status"].ToString(); } //row.Cells[3].Text;
            //    if (dr["category"].ToString().Length > 0) { ddlCategory.SelectedItem.Text = dr["category"].ToString(); } //row.Cells[4].Text;
            //    if (dr["Nationality"].ToString().Length > 0) { ddlNationality.SelectedItem.Text = dr["Nationality"].ToString(); }
            //    txtPostalCode.Value = dr["PostalCode"].ToString(); // row.Cells[26].Text;
            //    hlbKTP.Value = dr["id_ktp"].ToString(); // row.Cells[14].Text;
            //    if (dr["City"].ToString().Length > 0) { ddlCity.SelectedItem.Text = dr["City"].ToString(); }
            //    if (dr["Province"].ToString().Length > 0) { ddlProvince.SelectedItem.Text = dr["Province"].ToString(); }
            //    txtMobilePhone.Value = dr["MobilePhone"].ToString();
            //    //txtEmergencyContact.Value = dr["EmergencyContactNumber"].ToString();
            //    txtPersonalEmail.Value = dr["PersonalEmail"].ToString();
            //    txtFamilyCard.Value = dr["FamilyCardNumber"].ToString();
            //    if (dr["MaritalStatus"].ToString().Length > 0) { if (dr["MaritalStatus"].ToString().Substring(0, 4) != "<Sel") { ddlMaritalStatus.SelectedItem.Text = dr["MaritalStatus"].ToString(); }; }
            //    if (dr["ReligioN"].ToString().Length > 0) { if (dr["ReligioN"].ToString().Substring(0, 4) != "<Sel") { ddlReligion.SelectedItem.Text = dr["ReligioN"].ToString(); }; }

            //    if (dr["Education1"].ToString().Length > 0) { if (dr["Education1"].ToString().Substring(0, 4) != "<Sel") { ddlEducation1.SelectedItem.Text = dr["Education1"].ToString(); }; }
            //    txtMajor1.Value = dr["Major1"].ToString();
            //    txtInstitute1.Value = dr["Institute1"].ToString();
            //    if (dr["Education2"].ToString().Length > 0) { if (dr["Education2"].ToString().Substring(0, 4) != "<Sel") { ddlEducation2.SelectedItem.Text = dr["Education2"].ToString(); }; }
            //    txtMajor2.Value = dr["Major2"].ToString();
            //    txtInstitute2.Value = dr["Institute2"].ToString();

            //    txtNameOfSpouse.Value = dr["NameOfSpouse"].ToString();
            //    txtNoIDSpouse.Value = dr["NoIDSpouse"].ToString();
            //    if (dr["DOBSpouse"].ToString().Length > 0 && Convert.ToDateTime(dr["DOBSpouse"]).Year > 1900) { txtSpouseDOB.Value = dr["DOBSpouse"].ToString(); }
            //    if (dr["POBSpouse"].ToString().Length > 0) { txtSpousePOB.Value = dr["POBSpouse"].ToString(); }

            //    txtNameOf1stChild.Value = dr["NameOf1stChild"].ToString();
            //    txtNoID1stChild.Value = dr["NoID1stChild"].ToString();
            //    if (dr["DOB1stChild"].ToString().Length > 0 && Convert.ToDateTime(dr["DOB1stChild"]).Year > 1900) { txt1stChildDOB.Value = dr["DOB1stChild"].ToString(); }
            //    if (dr["POB1stChild"].ToString().Length > 0) { txt1stChildPOB.Value = dr["POB1stChild"].ToString(); }

            //    txtNameOf2ndChild.Value = dr["NameOf2ndChild"].ToString();
            //    txtNoID2ndChild.Value = dr["NoID2ndChild"].ToString();
            //    if (dr["DOB2ndChild"].ToString().Length > 0 && Convert.ToDateTime(dr["DOB2ndChild"]).Year > 1900) { txt2ndChildDOB.Value = dr["DOB2ndChild"].ToString(); }
            //    if (dr["POB2ndChild"].ToString().Length > 0) { txt2ndChildPOB.Value = dr["POB2ndChild"].ToString(); }

            //    txtNameOf3rdChild.Value = dr["NameOf3rdChild"].ToString();
            //    txtNoID3rdChild.Value = dr["NoID3rdChild"].ToString();
            //    if (dr["DOB3rdChild"].ToString().Length > 0 && Convert.ToDateTime(dr["DOB3rdChild"]).Year > 1900) { txt3rdChildDOB.Value = dr["DOB3rdChild"].ToString(); }
            //    if (dr["POB3rdChild"].ToString().Length > 0) { txt3rdChildPOB.Value = dr["POB3rdChild"].ToString(); }
            //    //if (dr["POB3rdChild"].ToString().Length > 0) { ddl3rdChildPOB.SelectedValue = dr["POB3rdChild"].ToString(); }

            //    txtCreateBy.Value = dr["createby"].ToString(); //row.Cells[18].Text;
            //    txtCreateDate.Value = dr["create_date"].ToString(); //row.Cells[19].Text;

            //    if (dr["EffectiveEndDate"].ToString().Length > 0 && Convert.ToDateTime(dr["EffectiveEndDate"]).Year > 1900) { txtEffectiveEndDate.Value = dr["EffectiveEndDate"].ToString(); } //row.Cells[20].Text;

            //    if (dr["CareerHistory"].ToString().Length > 0) { ddlCareerHistory.SelectedItem.Text = dr["CareerHistory"].ToString(); }
            //    if (dr["EmployeeSeparation"].ToString().Length > 0) { ddlEmployementSeparation.SelectedItem.Text = dr["EmployeeSeparation"].ToString(); }

            //}

            //if (ddlCategory.SelectedItem.Text == "CONTRACT")
            //{
            //    txtContractEndDate.Visible = true;
            //    txtContractStartDate.Visible = true;

            //    txtContractEndDate.Disabled = false;
            //    txtContractStartDate.Disabled = false;
            //}
            //else
            //{
            //    txtContractEndDate.Visible = false;
            //    txtContractStartDate.Visible = false;
            //}

            //if (ddlCategory.SelectedItem.Text == "PERMANENT")
            //{

            //}
            //btnSubmit.Visible = false;
            //btnNew.Visible = false;
            ////btnUpdate.Visible = true;
            ////btnAdd.Visible = false;
            //BindDataTableEmployees();

            //BindDataTableEmergencyContact(TableEditEmergencyContact, _vid_Edit);
            //BindDataTableCareerHistory(TableEditCareerHistory, txtNIK.Value.ToString());
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "UpdateEmployee");
            sqlcomm.Parameters.AddWithValue("@id", CustomString(hlbID.Value.Trim()));
            sqlcomm.Parameters.AddWithValue("@fullname", CustomString(txtFullname.Value));
            sqlcomm.Parameters.AddWithValue("@nik", CustomString(txtNIK.Value));
            sqlcomm.Parameters.AddWithValue("@id_position", CustomString(ddlPosition.SelectedValue));
            sqlcomm.Parameters.AddWithValue("@id_division", CustomString(ddlDivision.SelectedValue));
            sqlcomm.Parameters.AddWithValue("@id_section", CustomString(ddlSection.SelectedValue));
            sqlcomm.Parameters.AddWithValue("@id_sub_section", CustomString(ddlSubSection.SelectedValue));
            sqlcomm.Parameters.AddWithValue("@report_position", CustomString(ddlReportPosition.SelectedItem.Text.ToString()));
            sqlcomm.Parameters.AddWithValue("@code_location", CustomString(ddlLocationCode.SelectedValue));
            sqlcomm.Parameters.AddWithValue("@status", CustomString(ddlStatus.SelectedItem.Text.ToString()));
            sqlcomm.Parameters.AddWithValue("@category", CustomString(ddlCategory.SelectedItem.Text.ToString()));
            sqlcomm.Parameters.AddWithValue("@join_date", CustomString(txtJoinDate.Value));
            sqlcomm.Parameters.AddWithValue("@modifiedby", CustomString(txtModifiedBy.Value));
            sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value);

            sqlcomm.Parameters.AddWithValue("@address_value", CustomString(txtAddress.Value));
            sqlcomm.Parameters.AddWithValue("@domicile_value", CustomString(txtDomicile.Value));
            sqlcomm.Parameters.AddWithValue("@date_of_birth_value", CustomString(txtBirthDate.Value)); 
            sqlcomm.Parameters.AddWithValue("@id_bpjamsostek_value", CustomString(txtBPjamsostekNumber.Value));
            sqlcomm.Parameters.AddWithValue("@id_ktp_value", CustomString(hlbKTP.Value));

            //sqlcomm.Parameters.AddWithValue("@id_institution", CustomString(ddlInstitution.SelectedValue));
            //if (txtContractStartDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractStartDate", txtContractStartDate.Value); }
            //if (txtContractEndDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractEndDate", txtContractEndDate.Value); }

            //if (ddlGender.SelectedItem.Text.Length > 0) { sqlcomm.Parameters.AddWithValue("@Gender", CustomString(ddlGender.SelectedItem.Text.ToString())); }
            //if (ddlReligion.SelectedItem.Text.Length > 0) { sqlcomm.Parameters.AddWithValue("@Religion", CustomString(ddlReligion.SelectedItem.Text.ToString())); }
            //if (ddlBloodType.SelectedItem.Text.Length > 0) { sqlcomm.Parameters.AddWithValue("@BloodType", CustomString(ddlBloodType.SelectedItem.Text.ToString())); }

            ////sqlcomm.Parameters.AddWithValue("@Religion", ddlReligion.SelectedItem.Text.ToString());
            ////sqlcomm.Parameters.AddWithValue("@BloodType", ddlBloodType.SelectedItem.Text.ToString());

            //sqlcomm.Parameters.AddWithValue("@Nationality", CustomString(ddlNationality.SelectedValue));
            //sqlcomm.Parameters.AddWithValue("@Education1", CustomString(ddlEducation1.SelectedItem.Text.ToString()));
            //sqlcomm.Parameters.AddWithValue("@Institute1", CustomString(txtInstitute1.Value));
            //sqlcomm.Parameters.AddWithValue("@Major1", CustomString(txtMajor1.Value));
            //sqlcomm.Parameters.AddWithValue("@Education2", CustomString(ddlEducation2.SelectedItem.Text.ToString()));
            //sqlcomm.Parameters.AddWithValue("@Institute2", CustomString(txtInstitute2.Value));
            //sqlcomm.Parameters.AddWithValue("@Major2", CustomString(txtMajor2.Value));
            //sqlcomm.Parameters.AddWithValue("@City", CustomString(ddlCity.SelectedValue));
            //sqlcomm.Parameters.AddWithValue("@Province", CustomString(ddlProvince.SelectedValue));
            //sqlcomm.Parameters.AddWithValue("@PostalCode", CustomString(txtPostalCode.Value));
            //sqlcomm.Parameters.AddWithValue("@MobilePhone_Value", CustomString(txtMobilePhone.Value));
            ////sqlcomm.Parameters.AddWithValue("@EmergencyContactNumber_Value", CustomString(txtEmergencyContact.Value));
            //sqlcomm.Parameters.AddWithValue("@WorkingEmail_Value", CustomString(txtWorkingEmail.Value));
            //sqlcomm.Parameters.AddWithValue("@PersonalEmail_Value", CustomString(txtPersonalEmail.Value));
            //sqlcomm.Parameters.AddWithValue("@MaritalStatus", CustomString(ddlMaritalStatus.SelectedItem.Text.ToString()));
            //sqlcomm.Parameters.AddWithValue("@POB", CustomString(txtPOB.Value));
            //sqlcomm.Parameters.AddWithValue("@FamilyCardNumber_Value", CustomString(txtFamilyCard.Value));
            //sqlcomm.Parameters.AddWithValue("@NameOfSpouse_Value", CustomString(txtNameOfSpouse.Value));
            //sqlcomm.Parameters.AddWithValue("@NoIDSpouse_Value", CustomString(txtNoIDSpouse.Value));
            //if (txtSpouseDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOBSpouse_Value", txtSpouseDOB.Value); }
            //sqlcomm.Parameters.AddWithValue("@POBSpouse_Value", CustomString(txtSpousePOB.Value));
            //sqlcomm.Parameters.AddWithValue("@NameOf1stChild_Value", CustomString(txtNameOf1stChild.Value));
            //sqlcomm.Parameters.AddWithValue("@NoID1stChild_Value", CustomString(txtNoID1stChild.Value));
            //if (txt1stChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB1stChild_Value", txt1stChildDOB.Value); }
            //sqlcomm.Parameters.AddWithValue("@POB1stChild_Value", CustomString(txt1stChildPOB.Value));
            //sqlcomm.Parameters.AddWithValue("@NameOf2ndChild_Value", CustomString(txtNameOf2ndChild.Value));
            //sqlcomm.Parameters.AddWithValue("@NoID2ndChild_Value", CustomString(txtNoID2ndChild.Value));
            //if (txt2ndChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB2ndChild_Value", txt2ndChildDOB.Value); }
            //sqlcomm.Parameters.AddWithValue("@POB2ndChild_Value", CustomString(txt2ndChildPOB.Value));
            //sqlcomm.Parameters.AddWithValue("@NameOf3rdChild_Value", CustomString(txtNameOf3rdChild.Value));
            //sqlcomm.Parameters.AddWithValue("@NoID3rdChild_Value", CustomString(txtNoID3rdChild.Value));
            //if (txt2ndChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB3rdChild_Value", txt3rdChildDOB.Value); }
            //sqlcomm.Parameters.AddWithValue("@POB3rdChild_Value", CustomString(txt3rdChildPOB.Value));
            //sqlcomm.Parameters.AddWithValue("@BPJSKSNo_Value", CustomString(txtBPJSKesehatanNo.Value));
            //sqlcomm.Parameters.AddWithValue("@NPWPNo_Value", CustomString(txtNPWP.Value));
            //sqlcomm.Parameters.AddWithValue("@BankAccount_Value", CustomString(txtBankAccountNo.Value));
            //sqlcomm.Parameters.AddWithValue("@BankAccountName_Value", CustomString(txtBankAccountName.Value));
            //sqlcomm.Parameters.AddWithValue("@BankName_Value", CustomString(txtBankName.Value));
            //sqlcomm.Parameters.AddWithValue("@SourceOfHire", CustomString(ddlSourceOfHire.SelectedItem.Text.ToString()));

            //sqlcomm.Parameters.AddWithValue("@CareerHistory", CustomString(ddlCareerHistory.SelectedItem.Text.ToString()));
            //sqlcomm.Parameters.AddWithValue("@EmployeeSeparation", CustomString(ddlEmployementSeparation.SelectedItem.Text.ToString()));

            //if (txtEffectiveEndDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@EffectiveEndDate", txtEffectiveEndDate.Value); }

            sqlcomm.ExecuteNonQuery();

            sqlcomm.Parameters.Clear();

            //update Emergency Contact
            string json = hfEmergencyContactData.Value;
            if (!string.IsNullOrEmpty(json))
            {
                var contacts = JsonConvert.DeserializeObject<List<EmergencyContact>>(json);

                // Create and fill the public DataTable
                EmergencyContactTable = new DataTable();
                EmergencyContactTable.Columns.Add("Oid");
                EmergencyContactTable.Columns.Add("ContactName");
                EmergencyContactTable.Columns.Add("ContactAddress");
                EmergencyContactTable.Columns.Add("ContactRelationship");
                EmergencyContactTable.Columns.Add("ContactNumber");
                EmergencyContactTable.Columns.Add("CreatedBy");
                EmergencyContactTable.Columns.Add("CreatedDate");
                EmergencyContactTable.Columns.Add("ModifiedBy");
                EmergencyContactTable.Columns.Add("ModifiedDate");

                foreach (var c in contacts)
                {
                    if (c.Oid != "&nbsp;")
                    {
                        EmergencyContactTable.Rows.Add(
                            c.Oid,
                            c.ContactName,
                            c.ContactAddress,
                            c.ContactRelationship,
                            c.ContactNumber,
                            c.CreatedBy,
                            c.CreatedDate,
                            c.ModifiedBy,
                            c.ModifiedDate
                        );
                    }
                }
                // EmergencyContactTable is now available as a public DataTable
            }

            foreach (DataRow row in EmergencyContactTable.Rows)
            {
                if (row["Oid"].ToString() != "-&nbsp;" && row["Oid"].ToString() != "&nbsp;")
                {
                    sqlcomm.Parameters.AddWithValue("@StatementType", "SaveEmergencyContact");
                    string _vOID = CustomString(row["Oid"].ToString());

                    if (_vOID.Substring(0, 2) != "EC")
                    {
                        sqlcomm.Parameters.AddWithValue("@Contact_Oid", _vOID);
                    }

                    if (_vOID.Substring(0, 1) == "-")
                    {
                        sqlcomm.Parameters.AddWithValue("@FlagEmergencyContact", "Delete");
                    }

                    //sqlcomm.Parameters.AddWithValue("@Contact_Oid", CustomString(row["Oid"].ToString()));
                    sqlcomm.Parameters.AddWithValue("@id", CustomString(hlbID.Value.Trim()));
                    sqlcomm.Parameters.AddWithValue("@ContactName", CustomString(row["ContactName"].ToString()));
                    sqlcomm.Parameters.AddWithValue("@ContactAddress", CustomString(row["ContactAddress"].ToString()));
                    sqlcomm.Parameters.AddWithValue("@ContactRelationship", CustomString(row["ContactRelationship"].ToString()));
                    sqlcomm.Parameters.AddWithValue("@ContactNumber", CustomString(row["ContactNumber"].ToString()));
                    sqlcomm.Parameters.AddWithValue("@CreatedBy", Session["Fullname"].ToString());
                    sqlcomm.Parameters.AddWithValue("@ModifiedBy", Session["Fullname"].ToString());
                    //sqlcomm.Parameters.AddWithValue("@ModifiedDate", row["ModifiedDate"].ToString());
                    sqlcomm.ExecuteNonQuery();
                    sqlcomm.Parameters.Clear();
                }
            }

            // Create a DataTable with the same columns as your GridView
            DataTable dtCareerHistory = new DataTable();
            dtCareerHistory.Columns.Add("id");
            dtCareerHistory.Columns.Add("nik");
            dtCareerHistory.Columns.Add("FullName");
            dtCareerHistory.Columns.Add("Title");
            dtCareerHistory.Columns.Add("Division");
            dtCareerHistory.Columns.Add("Section");
            dtCareerHistory.Columns.Add("Location");
            dtCareerHistory.Columns.Add("Category");
            dtCareerHistory.Columns.Add("ModifiedBy");
            dtCareerHistory.Columns.Add("modification_date");

            // Loop through GridView rows
            foreach (GridViewRow row in TableEditCareerHistory.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DataRow dr = dtCareerHistory.NewRow();

                    // Example: get text from cells (adjust indexes as needed)
                    dr["id"] = TableEditCareerHistory.DataKeys[row.RowIndex]?.Value?.ToString() ?? "";
                    dr["nik"] = row.Cells[1].Text.Trim();
                    dr["FullName"] = row.Cells[2].Text.Trim();
                    dr["Title"] = row.Cells[3].Text.Trim();
                    dr["Division"] = row.Cells[4].Text.Trim();
                    dr["Section"] = row.Cells[5].Text.Trim();
                    dr["Location"] = row.Cells[6].Text.Trim();

                    // If Category is a DropDownList in a TemplateField
                    DropDownList ddlCategory = row.FindControl("ddlCategory") as DropDownList;
                    dr["Category"] = ddlCategory != null ? ddlCategory.SelectedValue : row.Cells[7].Text.Trim();

                    dr["ModifiedBy"] = row.Cells[8].Text.Trim();
                    dr["modification_date"] = row.Cells[9].Text.Trim();

                    dtCareerHistory.Rows.Add(dr);
                }
            }

            foreach (DataRow _vRow in dtCareerHistory.Rows)
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "UpdateHistoryMutationEmployee");
                sqlcomm.Parameters.AddWithValue("@id_EmployeeHistory", _vRow["id"]);
                sqlcomm.Parameters.AddWithValue("@nik", _vRow["nik"]);
                sqlcomm.Parameters.AddWithValue("@CareerHistory", _vRow["Category"]); // Pass Category
                                                                                  // Add other parameters as needed
                sqlcomm.ExecuteNonQuery();
                sqlcomm.Parameters.Clear();
            }

            sqlcomm.Dispose();
            Con.Close();


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "UpdateSucces();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "Func();", true);
            BindDataTableEmployees();

            btnSubmit.Visible = false;
            btnNew.Visible = true;
            //btnUpdate.Visible = false;
            //btnAdd.Visible = false;

            txtNIK.Disabled = true;
            txtJoinDate.Disabled = true;
        }

        // Define a class to match your JSON structure
        public class CareerHistoryItem
        {
            public string id { get; set; }
            public string nik { get; set; }
            public string category { get; set; }
            // Add other properties as needed
        }

        public DataTable EmergencyContactTable { get; set; }
        public class EmergencyContact
        {
            public string Oid { get; set; }
            public string ContactName { get; set; }
            public string ContactAddress { get; set; }
            public string ContactRelationship { get; set; }
            public string ContactNumber { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedDate { get; set; }
        }

        private string CustomString(string _input, int _length = int.MaxValue)
        {
            if (string.IsNullOrEmpty(_input) || _length <= 0)
            {
                _length = 0;
                return string.Empty;
            }
            else
            {
                if (_input == "&nbsp;")
                {
                    return string.Empty;
                }
                else
                {
                    if (_input.Substring(0, 1) != "<")
                    {
                        string sanitized = _input.Replace("'", "`");
                        if (sanitized.Length <= _length)
                        {
                            return sanitized;
                        }
                        else
                        {
                            return sanitized.Substring(0, _length);
                        }
                    }
                    else { return string.Empty; }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string iDate = txtBirthDate.Value;
            //DateTime oDate = Convert.ToDateTime(iDate);
            //byte[] binaryValue = null;
            //byte[] binaryDateTime = BitConverter.GetBytes(oDate.ToBinary());
            //SqlBinary varBinaryDateTime = new SqlBinary(binaryDateTime);


            if (ddlPosition.SelectedItem.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectPosition();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
            else if (ddlDivision.SelectedItem.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectDivison();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
            else if (ddlSection.SelectedItem.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectSection();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
            //else if (ddlSubSection.SelectedItem.Text.ToString() == "<Select SubSection>")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectSubSection();", true);
            //}
            //else if (ddlReportPosition.SelectedItem.Text.ToString() == "<Select Report Position>")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectReportPosition();", true);
            //}
            else if (ddlLocationCode.SelectedItem.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectLocation();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
            else if (ddlStatus.SelectedItem.Text.ToString() == "<Select Status>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectStatus();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
            else if (ddlCategory.SelectedItem.Text.ToString() == "<Select Category>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectCategory();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
            else if (txtFullname.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "NameEmpty();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
            else if (txtNIK.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "NIKEmpty();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
            else if (hlbURLPhoto.Value == "")
            {
                btnSubmit.Visible = false;
                btnNew.Visible = true;
                //btnUpdate.Visible = false;
                divIntitution.Visible = false;
                //btnAdd.Visible = false;

                txtNIK.Disabled = true;
                txtJoinDate.Disabled = true;


                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Employees";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@id_ktp_value", CustomString(hlbKTP.Value));
                sqlcomm.Parameters.AddWithValue("@fullname", CustomString(txtFullname.Value));
                sqlcomm.Parameters.AddWithValue("@nik", CustomString(txtNIK.Value));
                sqlcomm.Parameters.AddWithValue("@id_position", CustomString(ddlPosition.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@id_division", CustomString(ddlDivision.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@id_section", CustomString(ddlSection.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@id_sub_section", CustomString(ddlSubSection.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@report_position", CustomString(ddlReportPosition.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@code_location", CustomString(ddlLocationCode.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@status", CustomString(ddlStatus.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@category", CustomString(ddlCategory.SelectedItem.Text.ToString()));
                if (txtJoinDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@join_date", txtJoinDate.Value); }
                if (txtBirthDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@date_of_birth_value", txtBirthDate.Value); }
                sqlcomm.Parameters.AddWithValue("@createby", CustomString(txtCreateBy.Value));
                //sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value);
                sqlcomm.Parameters.AddWithValue("@modifiedby", CustomString(txtModifiedBy.Value));
                //sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value);
                sqlcomm.Parameters.AddWithValue("@address_value", CustomString(txtAddress.Value));
                sqlcomm.Parameters.AddWithValue("@domicile_value", CustomString(txtDomicile.Value));
                sqlcomm.Parameters.AddWithValue("@id_bpjamsostek_value", CustomString(txtBPjamsostekNumber.Value));
                sqlcomm.Parameters.AddWithValue("@id_institution", CustomString(ddlInstitution.SelectedValue));
                if (txtContractStartDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractStartDate", txtContractStartDate.Value); }
                if (txtContractEndDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractEndDate", txtContractEndDate.Value); }
                // update 23072025
                sqlcomm.Parameters.AddWithValue("@Gender", CustomString(ddlGender.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@Religion", CustomString(ddlReligion.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@BloodType", CustomString(ddlBloodType.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@Nationality", CustomString(ddlNationality.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@Education1", CustomString(ddlEducation1.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@Institute1", CustomString(txtInstitute1.Value));
                sqlcomm.Parameters.AddWithValue("@Major1", CustomString(txtMajor1.Value));
                sqlcomm.Parameters.AddWithValue("@Education2", CustomString(ddlEducation2.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@Institute2", CustomString(txtInstitute2.Value));
                sqlcomm.Parameters.AddWithValue("@Major2", CustomString(txtMajor2.Value));
                sqlcomm.Parameters.AddWithValue("@City", CustomString(ddlCity.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@Province", CustomString(ddlProvince.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@PostalCode", CustomString(txtPostalCode.Value));
                sqlcomm.Parameters.AddWithValue("@MobilePhone_Value", CustomString(txtMobilePhone.Value));
                //sqlcomm.Parameters.AddWithValue("@EmergencyContactNumber_Value", CustomString(txtEmergencyContact.Value));
                sqlcomm.Parameters.AddWithValue("@WorkingEmail_Value", CustomString(txtWorkingEmail.Value));
                sqlcomm.Parameters.AddWithValue("@PersonalEmail_Value", CustomString(txtPersonalEmail.Value));
                sqlcomm.Parameters.AddWithValue("@MaritalStatus", CustomString(ddlMaritalStatus.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@POB", CustomString(txtPOB.Value));
                sqlcomm.Parameters.AddWithValue("@FamilyCardNumber_Value", CustomString(txtFamilyCard.Value));
                sqlcomm.Parameters.AddWithValue("@NameOfSpouse_Value", CustomString(txtNameOfSpouse.Value));
                sqlcomm.Parameters.AddWithValue("@NoIDSpouse_Value", CustomString(txtNoIDSpouse.Value));
                if (txtSpouseDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOBSpouse_Value", txtSpouseDOB.Value); }
                sqlcomm.Parameters.AddWithValue("@POBSpouse_Value", CustomString(txtSpousePOB.Value));
                sqlcomm.Parameters.AddWithValue("@NameOf1stChild_Value", CustomString(txtNameOf1stChild.Value));
                sqlcomm.Parameters.AddWithValue("@NoID1stChild_Value", CustomString(txtNoID1stChild.Value));
                if (txt1stChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB1stChild_Value", txt1stChildDOB.Value); }
                sqlcomm.Parameters.AddWithValue("@POB1stChild_Value", CustomString(txt1stChildPOB.Value));
                sqlcomm.Parameters.AddWithValue("@NameOf2ndChild_Value", CustomString(txtNameOf2ndChild.Value));
                sqlcomm.Parameters.AddWithValue("@NoID2ndChild_Value", CustomString(txtNoID2ndChild.Value));
                if (txt2ndChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB2ndChild_Value", txt2ndChildDOB.Value); }
                sqlcomm.Parameters.AddWithValue("@POB2ndChild_Value", CustomString(txt2ndChildPOB.Value));
                sqlcomm.Parameters.AddWithValue("@NameOf3rdChild_Value", CustomString(txtNameOf3rdChild.Value));
                sqlcomm.Parameters.AddWithValue("@NoID3rdChild_Value", CustomString(txtNoID3rdChild.Value));
                if (txt3rdChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB3rdChild_Value", txt3rdChildDOB.Value); }
                sqlcomm.Parameters.AddWithValue("@POB3rdChild_Value", CustomString(txt3rdChildPOB.Value));
                sqlcomm.Parameters.AddWithValue("@BPJSKSNo_Value", CustomString(txtBPJSKesehatanNo.Value));
                sqlcomm.Parameters.AddWithValue("@NPWPNo_Value", CustomString(txtNPWP.Value));
                sqlcomm.Parameters.AddWithValue("@BankAccount_Value", CustomString(txtBankAccountNo.Value));
                sqlcomm.Parameters.AddWithValue("@BankAccountName_Value", CustomString(txtBankAccountName.Value));
                sqlcomm.Parameters.AddWithValue("@BankName_Value", CustomString(txtBankName.Value));
                sqlcomm.Parameters.AddWithValue("@SourceOfHire", CustomString(ddlSourceOfHire.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@EffectiveEndDate", CustomString(txtEffectiveEndDate.Value));

                sqlcomm.Parameters.AddWithValue("@CareerHistory", CustomString(ddlCareerHistory.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@EmployeeSeparation", CustomString(ddlEmployementSeparation.SelectedItem.Text.ToString()));
                /////////////////////////////////////////
                sqlcomm.ExecuteNonQuery();

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showSucces();", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                BindDataTableEmployees();
            }
            else
            {
                btnSubmit.Visible = false;
                btnNew.Visible = true;
                //btnUpdate.Visible = false;
                divIntitution.Visible = false;
                //btnAdd.Visible = false;

                txtNIK.Disabled = true;
                txtJoinDate.Disabled = true;
                string link_image = hlbURLPhoto.Value.ToString().Substring(33, hlbURLPhoto.Value.ToString().Length - 33);
                string url = "https://drive.google.com/uc?export=download&id=" + link_image;
                if (!System.IO.Directory.Exists(Server.MapPath("~/Photos")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Photos"));
                }
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFile(new Uri(url), Server.MapPath("~/Photos/" + txtNIK.Value.ToString() + ".jpg"));
                }

                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Employees";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@id_ktp_value", CustomString(hlbKTP.Value));
                sqlcomm.Parameters.AddWithValue("@fullname", CustomString(txtFullname.Value));
                sqlcomm.Parameters.AddWithValue("@nik", CustomString(txtNIK.Value));
                sqlcomm.Parameters.AddWithValue("@id_position", CustomString(ddlPosition.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@id_division", CustomString(ddlDivision.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@id_section", CustomString(ddlSection.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@id_sub_section", CustomString(ddlSubSection.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@report_position", CustomString(ddlReportPosition.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@code_location", CustomString(ddlLocationCode.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@status", CustomString(ddlStatus.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@category", CustomString(ddlCategory.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@join_date", txtJoinDate.Value);
                sqlcomm.Parameters.AddWithValue("@date_of_birth_value", txtBirthDate.Value);
                sqlcomm.Parameters.AddWithValue("@createdby", CustomString(txtCreateBy.Value));
                //sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value);
                sqlcomm.Parameters.AddWithValue("@modifiedby", CustomString(txtModifiedBy.Value));
                //sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value);
                sqlcomm.Parameters.AddWithValue("@address_value", CustomString(txtAddress.Value));
                sqlcomm.Parameters.AddWithValue("@domicile_value", CustomString(txtDomicile.Value));
                sqlcomm.Parameters.AddWithValue("@id_bpjamsostek_value", CustomString(txtBPjamsostekNumber.Value));
                sqlcomm.Parameters.AddWithValue("@id_institution", CustomString(ddlInstitution.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@ContractStartDate", txtContractStartDate.Value);
                sqlcomm.Parameters.AddWithValue("@ContractEndDate", txtContractEndDate.Value);
                ///// 23072025
                sqlcomm.Parameters.AddWithValue("@Gender", CustomString(ddlGender.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@Religion", CustomString(ddlReligion.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@BloodType", CustomString(ddlBloodType.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@Nationality", CustomString(ddlNationality.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@Education1", CustomString(ddlEducation1.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@Institute1", CustomString(txtInstitute1.Value));
                sqlcomm.Parameters.AddWithValue("@Major1", CustomString(txtMajor1.Value));
                sqlcomm.Parameters.AddWithValue("@Education2", CustomString(ddlEducation2.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@Institute2", CustomString(txtInstitute2.Value));
                sqlcomm.Parameters.AddWithValue("@Major2", CustomString(txtMajor2.Value));
                sqlcomm.Parameters.AddWithValue("@City", CustomString(ddlCity.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@Province", CustomString(ddlProvince.SelectedValue));
                sqlcomm.Parameters.AddWithValue("@PostalCode", CustomString(txtPostalCode.Value));
                sqlcomm.Parameters.AddWithValue("@MobilePhone_Value", CustomString(txtMobilePhone.Value));
                //sqlcomm.Parameters.AddWithValue("@EmergencyContactNumber_Value", CustomString(txtEmergencyContact.Value)); 
                sqlcomm.Parameters.AddWithValue("@WorkingEmail_Value", CustomString(txtWorkingEmail.Value));
                sqlcomm.Parameters.AddWithValue("@PersonalEmail_Value", CustomString(txtPersonalEmail.Value));
                sqlcomm.Parameters.AddWithValue("@MaritalStatus", CustomString(ddlMaritalStatus.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@POB", CustomString(txtPOB.Value));
                sqlcomm.Parameters.AddWithValue("@FamilyCardNumber_Value", CustomString(txtFamilyCard.Value));
                sqlcomm.Parameters.AddWithValue("@NameOfSpouse_Value", CustomString(txtNameOfSpouse.Value));
                sqlcomm.Parameters.AddWithValue("@NoIDSpouse_Value", CustomString(txtNoIDSpouse.Value));
                if (txtSpouseDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOBSpouse_Value", txtSpouseDOB.Value); }
                sqlcomm.Parameters.AddWithValue("@POBSpouse_Value", CustomString(txtSpousePOB.Value));
                sqlcomm.Parameters.AddWithValue("@NameOf1stChild_Value", CustomString(txtNameOf1stChild.Value));
                sqlcomm.Parameters.AddWithValue("@NoID1stChild_Value", CustomString(txtNoID1stChild.Value));
                if (txt1stChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB1stChild_Value", txt1stChildDOB.Value); }
                sqlcomm.Parameters.AddWithValue("@POB1stChild_Value", CustomString(txt1stChildPOB.Value));
                sqlcomm.Parameters.AddWithValue("@NameOf2ndChild_Value", CustomString(txtNameOf2ndChild.Value));
                sqlcomm.Parameters.AddWithValue("@NoID2ndChild_Value", CustomString(txtNoID2ndChild.Value));
                if (txt2ndChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB2ndChild_Value", txt2ndChildDOB.Value); }
                sqlcomm.Parameters.AddWithValue("@POB2ndChild_Value", CustomString(txt2ndChildPOB.Value));
                sqlcomm.Parameters.AddWithValue("@NameOf3rdChild_Value", CustomString(txtNameOf3rdChild.Value));
                sqlcomm.Parameters.AddWithValue("@NoID3rdChild_Value", CustomString(txtNoID3rdChild.Value));
                if (txt3rdChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB3rdChild_Value", txt3rdChildDOB.Value); }
                sqlcomm.Parameters.AddWithValue("@POB3rdChild_Value", CustomString(txt3rdChildPOB.Value));
                sqlcomm.Parameters.AddWithValue("@BPJSKSNo_Value", CustomString(txtBPJSKesehatanNo.Value));
                sqlcomm.Parameters.AddWithValue("@NPWPNo_Value", CustomString(txtNPWP.Value));
                sqlcomm.Parameters.AddWithValue("@BankAccount_Value", CustomString(txtBankAccountNo.Value));
                sqlcomm.Parameters.AddWithValue("@BankAccountName_Value", CustomString(txtBankAccountName.Value));
                sqlcomm.Parameters.AddWithValue("@BankName_Value", CustomString(txtBankName.Value));
                sqlcomm.Parameters.AddWithValue("@SourceOfHire", CustomString(ddlSourceOfHire.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@EffectiveEndDate", CustomString(txtEffectiveEndDate.Value));
                sqlcomm.Parameters.AddWithValue("@CareerHistory", CustomString(ddlCareerHistory.SelectedItem.Text.ToString()));
                sqlcomm.Parameters.AddWithValue("@EmployeeSeparation", CustomString(ddlEmployementSeparation.SelectedItem.Text.ToString()));
                //////////////////////////
                sqlcomm.ExecuteNonQuery();

                sqlcomm.Parameters.Clear();
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showSucces();", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                BindDataTableEmployees();
            }
        }

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSubSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlLocationCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedItem.Text == "OUTSOURCED")
            {
                divIntitution.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                GetInstitutionName();
                BindDataTableEmployees();
            }
            else
            {
                divIntitution.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
                BindDataTableEmployees();
            }
        }

        protected void ddlReportPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uploadFile_OLD()
        {
            if (FileUpload.FileName.Length > 0)
            {
                string path = string.Concat(Server.MapPath("~/UploadFiles/" + FileUpload.FileName));
                FileUpload.SaveAs(path);

                #region DefineTable
                DataTable dt = new DataTable();
                dt.Columns.Add("fullname", typeof(System.String));
                dt.Columns.Add("nik", typeof(System.String));
                dt.Columns.Add("id_position", typeof(System.String));
                dt.Columns.Add("id_division", typeof(System.String));
                dt.Columns.Add("id_section", typeof(System.String));
                dt.Columns.Add("id_sub_section", typeof(System.String));
                dt.Columns.Add("report_position", typeof(System.String));
                dt.Columns.Add("code_location", typeof(System.String));
                dt.Columns.Add("status", typeof(System.String));
                dt.Columns.Add("category", typeof(System.String));
                dt.Columns.Add("join_date", typeof(System.DateTime));
                dt.Columns.Add("date_of_birth", typeof(System.DateTime));
                dt.Columns.Add("id_ktp", typeof(System.String));
                dt.Columns.Add("address", typeof(System.String));
                dt.Columns.Add("domicile", typeof(System.String));
                dt.Columns.Add("id_bpjamsostek", typeof(System.String));

                dt.Columns.Add("ContractStartDate", typeof(System.DateTime));
                dt.Columns.Add("ContractEndDate", typeof(System.DateTime));
                //dt.Columns.Add("createby", typeof(System.String));
                //dt.Columns.Add("create_date", typeof(System.String));
                //dt.Columns.Add("modifiedby", typeof(System.String));
                //dt.Columns.Add("modified_date", typeof(System.String));
                #endregion

                XLWorkbook _vWBUpload = new XLWorkbook(path);
                foreach (IXLWorksheet _vWorksheet in _vWBUpload.Worksheets)
                {
                    int _ExcelRow = 3;

                    var _tesdddd = _vWorksheet.Cell(_ExcelRow, 4).Value.ToString().Length;

                    while (_vWorksheet.Cell(_ExcelRow, 4).Value.ToString().Length > 0)
                    {

                        DataRow _dr = dt.NewRow();

                        var _testss = _vWorksheet.Cell(_ExcelRow, 20).Value.ToString().Length;

                        #region DefineValue
                        _dr["nik"] = _vWorksheet.Cell(_ExcelRow, 4).Value.ToString();
                        _dr["fullname"] = _vWorksheet.Cell(_ExcelRow, 5).Value.ToString();
                        if (_vWorksheet.Cell(_ExcelRow, 6).Value.ToString().Length > 0) { _dr["date_of_birth"] = _vWorksheet.Cell(_ExcelRow, 6).GetDateTime(); }
                        _dr["id_position"] = _vWorksheet.Cell(_ExcelRow, 7).Value.ToString();
                        _dr["id_division"] = _vWorksheet.Cell(_ExcelRow, 8).Value.ToString();
                        _dr["id_section"] = _vWorksheet.Cell(_ExcelRow, 9).Value.ToString();
                        _dr["id_sub_section"] = _vWorksheet.Cell(_ExcelRow, 10).Value.ToString();
                        _dr["category"] = _vWorksheet.Cell(_ExcelRow, 11).Value.ToString();
                        _dr["code_location"] = _vWorksheet.Cell(_ExcelRow, 12).Value.ToString();
                        _dr["report_position"] = _vWorksheet.Cell(_ExcelRow, 13).Value.ToString();
                        _dr["status"] = _vWorksheet.Cell(_ExcelRow, 14).Value.ToString();
                        _dr["join_date"] = _vWorksheet.Cell(_ExcelRow, 15).GetDateTime();
                        _dr["id_ktp"] = _vWorksheet.Cell(_ExcelRow, 16).Value.ToString();
                        _dr["address"] = _vWorksheet.Cell(_ExcelRow, 17).Value.ToString();
                        _dr["domicile"] = _vWorksheet.Cell(_ExcelRow, 18).Value.ToString();
                        _dr["id_bpjamsostek"] = _vWorksheet.Cell(_ExcelRow, 19).Value.ToString();
                        if (_vWorksheet.Cell(_ExcelRow, 20).Value.ToString().Length > 0) { _dr["ContractStartDate"] = _vWorksheet.Cell(_ExcelRow, 20).GetDateTime(); }
                        if (_vWorksheet.Cell(_ExcelRow, 21).Value.ToString().Length > 0) { _dr["ContractEndDate"] = _vWorksheet.Cell(_ExcelRow, 21).GetDateTime(); }

                        #endregion

                        dt.Rows.Add(_dr);

                        _ExcelRow = _ExcelRow + 1;
                    }

                    string pathdb = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                    SqlConnection Con = new SqlConnection(pathdb);
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "sp_HRIS_DB_Employees";
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Connection = Con;

                    Con.Open();

                    foreach (DataRow _dr1 in dt.Rows)
                    {
                        #region ParametersValue
                        sqlcomm.Parameters.AddWithValue("@StatementType", "SaveImportExisting");
                        if (CustomString(_dr1["fullname"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@fullname", CustomString(_dr1["fullname"].ToString())); }
                        if (CustomString(_dr1["nik"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@nik", CustomString(_dr1["nik"].ToString())); }
                        if (CustomString(_dr1["id_position"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_position", CustomString(_dr1["id_position"].ToString())); }
                        if (CustomString(_dr1["id_division"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_division", CustomString(_dr1["id_division"].ToString())); }
                        if (CustomString(_dr1["id_section"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_section", CustomString(_dr1["id_section"].ToString())); }
                        if (CustomString(_dr1["id_sub_section"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_sub_section", CustomString(_dr1["id_sub_section"].ToString())); }
                        if (CustomString(_dr1["report_position"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@report_position", CustomString(_dr1["report_position"].ToString())); }
                        if (CustomString(_dr1["code_location"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@code_location", CustomString(_dr1["code_location"].ToString())); }
                        if (CustomString(_dr1["status"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@status", CustomString(_dr1["status"].ToString())); }
                        if (CustomString(_dr1["category"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@category", CustomString(_dr1["category"].ToString())); }
                        if (_dr1["join_date"].ToString().Length > 0) { sqlcomm.Parameters.AddWithValue("@join_date", _dr1["join_date"]); }
                        if (_dr1["date_of_birth"].ToString().Length > 0) { sqlcomm.Parameters.AddWithValue("@date_of_birth_value", _dr1["date_of_birth"]); }
                        if (CustomString(Session["Fullname"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@createdby", CustomString(Session["Fullname"].ToString())); }
                        if (CustomString(Session["Fullname"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@modifiedby", CustomString(Session["Fullname"].ToString())); }
                        if (CustomString(_dr1["id_ktp"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_ktp_value", CustomString(_dr1["id_ktp"].ToString())); }
                        if (CustomString(_dr1["address"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@address_value", CustomString(_dr1["address"].ToString())); }
                        if (CustomString(_dr1["domicile"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@domicile_value", CustomString(_dr1["domicile"].ToString())); }
                        if (CustomString(_dr1["id_bpjamsostek"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_bpjamsostek_value", CustomString(_dr1["id_bpjamsostek"].ToString())); }

                        if (_dr1["ContractStartDate"].ToString().Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractStartDate", _dr1["ContractStartDate"]); }
                        if (_dr1["ContractEndDate"].ToString().Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractEndDate", _dr1["ContractEndDate"]); }
                        #endregion

                        sqlcomm.ExecuteNonQuery();

                        sqlcomm.Parameters.Clear();

                        BindDataTableEmployees();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "UploadSuccess();", true);


                    }
                    Con.Close();
                    File.Delete(Server.MapPath("~/UploadFiles/" + FileUpload.PostedFile.FileName));
                }
            }
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            #region Logic lama 
            //if (FileUpload.FileName.Length > 0)
            //{
            //    string path = string.Concat(Server.MapPath("~/UploadFiles/" + FileUpload.FileName));
            //    FileUpload.SaveAs(path);

            //    #region DefineTable
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("fullname", typeof(System.String));
            //    dt.Columns.Add("nik", typeof(System.String));
            //    dt.Columns.Add("id_position", typeof(System.String));
            //    dt.Columns.Add("id_division", typeof(System.String));
            //    dt.Columns.Add("id_section", typeof(System.String));
            //    dt.Columns.Add("id_sub_section", typeof(System.String));
            //    dt.Columns.Add("report_position", typeof(System.String));
            //    dt.Columns.Add("code_location", typeof(System.String));
            //    dt.Columns.Add("status", typeof(System.String));
            //    dt.Columns.Add("category", typeof(System.String));
            //    dt.Columns.Add("join_date", typeof(System.DateTime));
            //    dt.Columns.Add("date_of_birth", typeof(System.DateTime));
            //    dt.Columns.Add("id_ktp", typeof(System.String));
            //    dt.Columns.Add("address", typeof(System.String));
            //    dt.Columns.Add("domicile", typeof(System.String));
            //    dt.Columns.Add("id_bpjamsostek", typeof(System.String));

            //    dt.Columns.Add("ContractStartDate", typeof(System.DateTime));
            //    dt.Columns.Add("ContractEndDate", typeof(System.DateTime));
            //    //dt.Columns.Add("createby", typeof(System.String));
            //    //dt.Columns.Add("create_date", typeof(System.String));
            //    //dt.Columns.Add("modifiedby", typeof(System.String));
            //    //dt.Columns.Add("modified_date", typeof(System.String));
            //    #endregion

            //    XLWorkbook _vWBUpload = new XLWorkbook(path);
            //    IXLWorksheet _vWorksheet = _vWBUpload.Worksheets.First();

            //    int _ExcelRow = 3;

            //    var _tesdddd = _vWorksheet.Cell(_ExcelRow, 4).Value.ToString().Length;

            //    while (_vWorksheet.Cell(_ExcelRow, 4).Value.ToString().Length > 0)
            //    {

            //        DataRow _dr = dt.NewRow();

            //        var _testss = _vWorksheet.Cell(_ExcelRow, 20).Value.ToString().Length;

            //        #region DefineValue
            //        _dr["nik"] = _vWorksheet.Cell(_ExcelRow, 4).Value.ToString();
            //        _dr["fullname"] = _vWorksheet.Cell(_ExcelRow, 5).Value.ToString();
            //        if (_vWorksheet.Cell(_ExcelRow, 6).Value.ToString().Length > 0) { _dr["date_of_birth"] = _vWorksheet.Cell(_ExcelRow, 6).GetDateTime(); }
            //        _dr["id_position"] = _vWorksheet.Cell(_ExcelRow, 7).Value.ToString();
            //        _dr["id_division"] = _vWorksheet.Cell(_ExcelRow, 8).Value.ToString();
            //        _dr["id_section"] = _vWorksheet.Cell(_ExcelRow, 9).Value.ToString();
            //        _dr["id_sub_section"] = _vWorksheet.Cell(_ExcelRow, 10).Value.ToString();
            //        _dr["category"] = _vWorksheet.Cell(_ExcelRow, 11).Value.ToString();
            //        _dr["code_location"] = _vWorksheet.Cell(_ExcelRow, 12).Value.ToString();
            //        _dr["report_position"] = _vWorksheet.Cell(_ExcelRow, 13).Value.ToString();
            //        _dr["status"] = _vWorksheet.Cell(_ExcelRow, 14).Value.ToString();
            //        _dr["join_date"] = _vWorksheet.Cell(_ExcelRow, 15).GetDateTime();
            //        _dr["id_ktp"] = _vWorksheet.Cell(_ExcelRow, 16).Value.ToString();
            //        _dr["address"] = _vWorksheet.Cell(_ExcelRow, 17).Value.ToString();
            //        _dr["domicile"] = _vWorksheet.Cell(_ExcelRow, 18).Value.ToString();
            //        _dr["id_bpjamsostek"] = _vWorksheet.Cell(_ExcelRow, 19).Value.ToString();
            //        if (_vWorksheet.Cell(_ExcelRow, 20).Value.ToString().Length > 0) { _dr["ContractStartDate"] = _vWorksheet.Cell(_ExcelRow, 20).GetDateTime(); }
            //        if (_vWorksheet.Cell(_ExcelRow, 21).Value.ToString().Length > 0) { _dr["ContractEndDate"] = _vWorksheet.Cell(_ExcelRow, 21).GetDateTime(); }

            //        #endregion

            //        dt.Rows.Add(_dr);

            //        _ExcelRow = _ExcelRow + 1;
            //    }

            //    string pathdb = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            //    SqlConnection Con = new SqlConnection(pathdb);
            //    SqlCommand sqlcomm = new SqlCommand();
            //    sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            //    sqlcomm.CommandType = CommandType.StoredProcedure;
            //    sqlcomm.Connection = Con;

            //    Con.Open();

            //    foreach (DataRow _dr1 in dt.Rows)
            //    {
            //        #region ParametersValue
            //        sqlcomm.Parameters.AddWithValue("@StatementType", "SaveImportExisting");
            //        if (CustomString(_dr1["fullname"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@fullname", CustomString(_dr1["fullname"].ToString())); }
            //        if (CustomString(_dr1["nik"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@nik", CustomString(_dr1["nik"].ToString())); }
            //        if (CustomString(_dr1["id_position"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_position", CustomString(_dr1["id_position"].ToString())); }
            //        if (CustomString(_dr1["id_division"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_division", CustomString(_dr1["id_division"].ToString())); }
            //        if (CustomString(_dr1["id_section"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_section", CustomString(_dr1["id_section"].ToString())); }
            //        if (CustomString(_dr1["id_sub_section"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_sub_section", CustomString(_dr1["id_sub_section"].ToString())); }
            //        if (CustomString(_dr1["report_position"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@report_position", CustomString(_dr1["report_position"].ToString())); }
            //        if (CustomString(_dr1["code_location"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@code_location", CustomString(_dr1["code_location"].ToString())); }
            //        if (CustomString(_dr1["status"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@status", CustomString(_dr1["status"].ToString())); }
            //        if (CustomString(_dr1["category"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@category", CustomString(_dr1["category"].ToString())); }
            //        if (_dr1["join_date"].ToString().Length > 0) { sqlcomm.Parameters.AddWithValue("@join_date", _dr1["join_date"]); }
            //        if (_dr1["date_of_birth"].ToString().Length > 0) { sqlcomm.Parameters.AddWithValue("@date_of_birth_value", _dr1["date_of_birth"]); }
            //        if (CustomString(Session["Fullname"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@createdby", CustomString(Session["Fullname"].ToString())); }
            //        if (CustomString(Session["Fullname"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@modifiedby", CustomString(Session["Fullname"].ToString())); }
            //        if (CustomString(_dr1["id_ktp"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_ktp_value", CustomString(_dr1["id_ktp"].ToString())); }
            //        if (CustomString(_dr1["address"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@address_value", CustomString(_dr1["address"].ToString())); }
            //        if (CustomString(_dr1["domicile"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@domicile_value", CustomString(_dr1["domicile"].ToString())); }
            //        if (CustomString(_dr1["id_bpjamsostek"].ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_bpjamsostek_value", CustomString(_dr1["id_bpjamsostek"].ToString())); }

            //        if (_dr1["ContractStartDate"].ToString().Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractStartDate", _dr1["ContractStartDate"]); }
            //        if (_dr1["ContractEndDate"].ToString().Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractEndDate", _dr1["ContractEndDate"]); }
            //        #endregion

            //        sqlcomm.ExecuteNonQuery();

            //        sqlcomm.Parameters.Clear();

            //        BindDataTableEmployees();
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "UploadSuccess();", true);

            //        Con.Close();
            //        File.Delete(Server.MapPath("~/UploadFiles/" + FileUpload.PostedFile.FileName));
            //    }

            //}
            #endregion
            #region Logic Baru 

            if (!FileUpload.HasFile) return;


            string path = string.Concat(Server.MapPath("~/UploadFiles/" + FileUpload.FileName));           
           
            FileUpload.SaveAs(path);

            try
            {
                using (var wb = new XLWorkbook(path))
                {

                    string templatePath = Server.MapPath("~/TemplateImportExcel/Template Import Data Employees.xlsx");

                    var templateErrors = ValidateExcelTemplate(wb, templatePath);

                    if (templateErrors.Any())
                    {
                        Session["ImportError"] = string.Join("<br>", templateErrors);
                        Response.Redirect(Request.RawUrl, false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }


                    DataTable dtRawEmployee = ReadSheet(wb, "Employee detail");
                    DataTable dtRawPersonalinfo = ReadSheet(wb, "Personal information");
                    DataTable dtEducationinfo = ReadSheet(wb, "Education information");
                    DataTable dtRawFamilyinfo = ReadSheet(wb, "Family information");
                    DataTable dtRawEmergencyContact = ReadSheet(wb, "Emergency contact information");
                    DataTable dtRawCareerHistory = ReadSheet(wb, "Career history");
                    DataTable dtRawWarningletterHistory = ReadSheet(wb, "Warning letter history");


                    DataTable dtEmployee = MapEmployeeDetail(dtRawEmployee);
                    DataTable dtPersonalInfo = MapPersonalInfo(dtRawPersonalinfo);
                    DataTable dtEducationInfo = MapEducationInfo(dtEducationinfo);
                    DataTable dtFamilyInfo = MapFamilyInfo(dtRawFamilyinfo);
                    DataTable dtEmergencyContact = MapEmergencyContact(dtRawEmergencyContact);
                    DataTable dtCareerHistory = MapCareerHistory(dtRawCareerHistory);
                    DataTable dtWarningLetterHistory = MapWarningLetterHistory(dtRawWarningletterHistory);


                    List<string> validationErrors = new List<string>();




                    /* =========================================================
                       CEK SEMUA SHEET KOSONG
                    ========================================================= */

                    bool isAllSheetEmpty = dtEmployee.Rows.Count == 0 && dtPersonalInfo.Rows.Count == 0 && 
                        dtEducationInfo.Rows.Count == 0 && dtFamilyInfo.Rows.Count == 0 && dtEmergencyContact.Rows.Count == 0 &&
                        dtCareerHistory.Rows.Count == 0 && dtWarningLetterHistory.Rows.Count == 0;

                    if (isAllSheetEmpty)
                    {
                        Session["ImportError"] = "Import empty file, no data processed.";
                        Response.Redirect(Request.RawUrl);
                        return;
                    }


                    /* =========================================================
                       VALIDASI EMPLOYEE DETAIL
                    ========================================================= */

                    if (dtEmployee.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtEmployee.Rows)
                        {
                            string employeeId = row["EmployeeID"]?.ToString().Trim();

                            if (string.IsNullOrWhiteSpace(employeeId))
                            {
                                validationErrors.Add("Sheet Employee Detail : EmployeeID cannot be empty.");
                                continue;
                            }

                            if (!employeeId.Any(char.IsDigit))
                            {
                                validationErrors.Add($"Sheet Employee Detail : EmployeeID '{employeeId}' must contain numbers.");
                            }
                        }
                    }


                    /* =========================================================
                       VALIDASI PERSONAL INFORMATION
                    ========================================================= */

                    if (dtPersonalInfo.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtPersonalInfo.Rows)
                        {
                            string employeeId = row["EmployeeID"]?.ToString().Trim();

                            if (string.IsNullOrWhiteSpace(employeeId))
                            {
                                validationErrors.Add("Sheet Personal Information : EmployeeID cannot be empty.");
                                continue;
                            }

                            if (!employeeId.Any(char.IsDigit))
                            {
                                validationErrors.Add($"Sheet Personal Information : EmployeeID '{employeeId}' must contain numbers.");
                            }
                        }
                    }


                    /* =========================================================
                       VALIDASI EDUCATION INFORMATION
                    ========================================================= */

                    if (dtEducationInfo.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtEducationInfo.Rows)
                        {
                            string employeeId = row["EmployeeID"]?.ToString().Trim();

                            if (string.IsNullOrWhiteSpace(employeeId))
                            {
                                validationErrors.Add("Sheet Education Information : EmployeeID cannot be empty.");
                                continue;
                            }

                            if (!employeeId.Any(char.IsDigit))
                            {
                                validationErrors.Add($"Sheet Education Information : EmployeeID '{employeeId}' must contain numbers.");
                            }
                        }
                    }


                    /* =========================================================
                       VALIDASI FAMILY INFORMATION
                    ========================================================= */

                    if (dtFamilyInfo.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtFamilyInfo.Rows)
                        {
                            string employeeId = row["EmployeeID"]?.ToString().Trim();

                            if (string.IsNullOrWhiteSpace(employeeId))
                            {
                                validationErrors.Add("Sheet Family Information : EmployeeID cannot be empty.");
                                continue;
                            }

                            if (!employeeId.Any(char.IsDigit))
                            {
                                validationErrors.Add($"Sheet Family Information : EmployeeID '{employeeId}' must contain numbers.");
                            }
                        }
                    }


                    /* =========================================================
                       VALIDASI EMERGENCY CONTACT
                    ========================================================= */

                    if (dtEmergencyContact.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtEmergencyContact.Rows)
                        {
                            string employeeId = row["EmployeeID"]?.ToString().Trim();

                            if (string.IsNullOrWhiteSpace(employeeId))
                            {
                                validationErrors.Add("Sheet Emergency Contact : EmployeeID cannot be empty.");
                                continue;
                            }

                            if (!employeeId.Any(char.IsDigit))
                            {
                                validationErrors.Add($"Sheet Emergency Contact : EmployeeID '{employeeId}' must contain numbers.");
                            }
                        }
                    }


                    /* =========================================================
                       VALIDASI CAREER HISTORY
                    ========================================================= */

                    if (dtCareerHistory.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtCareerHistory.Rows)
                        {
                            string employeeId = row["EmployeeID"]?.ToString().Trim();

                            if (string.IsNullOrWhiteSpace(employeeId))
                            {
                                validationErrors.Add("Sheet Career History : EmployeeID cannot be empty.");
                                continue;
                            }

                            if (!employeeId.Any(char.IsDigit))
                            {
                                validationErrors.Add($"Sheet Career History : EmployeeID '{employeeId}' must contain numbers.");
                            }
                        }
                    }


                    /* =========================================================
                       VALIDASI WARNING LETTER
                    ========================================================= */

                    if (dtWarningLetterHistory.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtWarningLetterHistory.Rows)
                        {
                            string employeeId = row["EmployeeID"]?.ToString().Trim();

                            if (string.IsNullOrWhiteSpace(employeeId))
                            {
                                validationErrors.Add("Sheet Warning Letter : EmployeeID cannot be empty.");
                                continue;
                            }

                            if (!employeeId.Any(char.IsDigit))
                            {
                                validationErrors.Add($"Sheet Warning Letter : EmployeeID '{employeeId}' must contain numbers.");
                            }
                        }
                    }


                    /* =========================================================
                       JIKA ADA ERROR
                    ========================================================= */

                    if (validationErrors.Any())
                    {
                        Session["ImportError"] = string.Join("<br>", validationErrors.Distinct());

                        Response.Redirect(Request.RawUrl);

                        return;
                    }


                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_HRIS_DB_ImportEmployeeAll", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.CommandTimeout = 300;

                            //Employee Detail
                            var param = cmd.Parameters.AddWithValue("@EmployeeTVP", dtEmployee);
                            param.SqlDbType = SqlDbType.Structured;
                            param.TypeName = "dbo.EmployeeType";

                            //Personal Info
                            var paramPersonal = cmd.Parameters.AddWithValue("@PersonalTVP", dtPersonalInfo);
                            paramPersonal.SqlDbType = SqlDbType.Structured;
                            paramPersonal.TypeName = "dbo.PersonalType";

                            //Education Info
                            var paramEducation = cmd.Parameters.AddWithValue("@EducationTVP", dtEducationInfo);
                            paramEducation.SqlDbType = SqlDbType.Structured;
                            paramEducation.TypeName = "dbo.EducationType";

                            //Family Info
                            var paramFamily = cmd.Parameters.AddWithValue("@FamilyTVP", dtFamilyInfo);
                            paramFamily.SqlDbType = SqlDbType.Structured;
                            paramFamily.TypeName = "dbo.FamilyType";

                            //Emergency Contact
                            var paramEmergency = cmd.Parameters.AddWithValue("@EmergencyContactTVP", dtEmergencyContact);
                            paramEmergency.SqlDbType = SqlDbType.Structured;
                            paramEmergency.TypeName = "dbo.EmergencyContactType";

                            //Career History
                            var paramCareer = cmd.Parameters.AddWithValue("@CareerHistoryTVP", dtCareerHistory);
                            paramCareer.SqlDbType = SqlDbType.Structured;
                            paramCareer.TypeName = "dbo.CareerHistoryType";

                            //Warning Letter History
                            var paramWarning = cmd.Parameters.AddWithValue("@WarningLetterTVP", dtWarningLetterHistory);
                            paramWarning.SqlDbType = SqlDbType.Structured;
                            paramWarning.TypeName = "dbo.WarningLetterType";

                            cmd.Parameters.AddWithValue("@CreatedBy", Session["Fullname"].ToString());



                            con.Open();

                            DataTable dtLog = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dtLog);
                            }


                            //BindDataTableEmployees();

                            Session["ImportLog"] = dtLog;
                            Session["ImportSuccess"] = "Import successfully";

                            
                            File.Delete(path);

                            Response.Redirect(Request.RawUrl, false);
                            Context.ApplicationInstance.CompleteRequest();

                            

                            return;

                         

                            //File.Delete(path);


                            //cmd.ExecuteNonQuery();
                        }
                    }
                }



                //Response.Redirect(Request.RawUrl, false);
                //Context.ApplicationInstance.CompleteRequest();

                



            }
            catch (Exception ex)
            {
                BindDataTableEmployees();

                Session["ImportError"] = ex.Message;
                               
                Response.Redirect(Request.RawUrl); 

                return;
            }


            #endregion


        }

        private List<string> ValidateExcelTemplate( XLWorkbook uploadedWb, string templatePath)
        {
            List<string> errors = new List<string>();

            using (var templateWb = new XLWorkbook(templatePath))
            {
                foreach (var templateSheet in templateWb.Worksheets)
                {
                    string sheetName = templateSheet.Name;

                    var uploadedSheet =
                        uploadedWb.Worksheets
                        .FirstOrDefault(x =>
                            x.Name.Trim()
                            .Equals(sheetName,
                            StringComparison.OrdinalIgnoreCase));



                    /* =========================================
                       SHEET TIDAK ADA
                    ========================================= */

                    if (uploadedSheet == null)
                    {
                        errors.Add(
                            $"Sheet '{sheetName}' not found.");
                        continue;
                    }


                    /* =========================================
                       AMBIL HEADER TEMPLATE
                    ========================================= */

                    var templateHeaders =
                        templateSheet.Row(1)
                        .CellsUsed()
                        .Select(x => x.GetString().Trim())
                        .ToList();


                    /* =========================================
                       AMBIL HEADER UPLOAD
                    ========================================= */

                    var uploadedHeaders =
                        uploadedSheet.Row(1)
                        .CellsUsed()
                        .Select(x => x.GetString().Trim())
                        .ToList();

                    /* =========================================
                       HEADER COUNT BERBEDA
                    ========================================= */

                    if (templateHeaders.Count != uploadedHeaders.Count)
                    {
                        errors.Add(  $"Sheet '{sheetName}' column count mismatch.");
                    }

                    /* =========================================
                       HEADER VALIDATION
                    ========================================= */

                    foreach (string header in templateHeaders)
                    {
                        if (!uploadedHeaders.Any(x =>
                            x.Equals(header,
                            StringComparison.OrdinalIgnoreCase)))
                        {
                            errors.Add(
                                $"Sheet '{sheetName}' missing column '{header}'.");
                        }
                    }


                    /* =========================================
                       DUPLICATE HEADER
                    ========================================= */

                    var duplicates =
                        uploadedHeaders
                        .GroupBy(x => x.ToLower())
                        .Where(x => x.Count() > 1)
                        .Select(x => x.Key);

                    foreach (var dup in duplicates)
                    {
                        errors.Add(  $"Sheet '{sheetName}' duplicate column '{dup}'.");
                    }
                }
            }

            return errors;
        }

        private DataTable ReadSheet(XLWorkbook wb, string sheetName)
        {
            DataTable dt = new DataTable();
            var ws = wb.Worksheet(sheetName);

            bool firstRow = true;

            foreach (IXLRow row in ws.RowsUsed())
            {
                if (firstRow)
                {
                    foreach (IXLCell cell in row.Cells())
                        dt.Columns.Add(cell.Value.ToString().Trim());

                    firstRow = false;
                }
                else
                {
                    var newRow = dt.NewRow();
                    int i = 0;

                    foreach (IXLCell cell in row.Cells())
                    {
                        newRow[i] = cell.Value.ToString();
                        i++;
                    }

                    dt.Rows.Add(newRow);
                }
            }

            return dt;
        }




        private string GetValue(DataRow row, string col)
        {
            return row.Table.Columns.Contains(col)
                ? row[col]?.ToString().Trim()
                : "";
        }

        private DataTable MapEmployeeDetail(DataTable dtRaw)
        {
            DataTable dt = new DataTable();

            
            dt.Columns.Add("EmployeeID");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("Title");
            dt.Columns.Add("Division");
            dt.Columns.Add("Section");
            dt.Columns.Add("Subsection");
            dt.Columns.Add("EmploymentStatus");
            dt.Columns.Add("Location");
            dt.Columns.Add("ReportTo");
            dt.Columns.Add("Status");
            dt.Columns.Add("WorkingEmail");
            dt.Columns.Add("JoinDate", typeof(DateTime));
            dt.Columns.Add("ContractStartDate", typeof(DateTime));
            dt.Columns.Add("ContractEndDate", typeof(DateTime));
            dt.Columns.Add("EffectEndDate", typeof(DateTime));
            dt.Columns.Add("Sourcehire");

            foreach (DataRow row in dtRaw.Rows)
            {
                string empId = GetValue(row, "Employee ID Number");

                if (string.IsNullOrWhiteSpace(empId))
                    continue;

                DataRow dr = dt.NewRow();

                dr["EmployeeID"] = empId;
                dr["EmployeeName"] = GetValue(row, "Employee Name");
                dr["Title"] = GetValue(row, "Title");
                dr["Division"] = GetValue(row, "Division");
                dr["Section"] = GetValue(row, "Section");
                dr["Subsection"] = GetValue(row, "Subsection");
                dr["EmploymentStatus"] = GetValue(row, "Employment Status");
                dr["Location"] = GetValue(row, "Location");
                dr["ReportTo"] = GetValue(row, "Report to Position");
                dr["Status"] = GetValue(row, "Status");
                dr["Sourcehire"] = GetValue(row, "Source of Hire");
                dr["WorkingEmail"] = GetValue(row, "Working Email");

                if (DateTime.TryParse(GetValue(row, "Join Date"), out DateTime joinDate))
                    dr["JoinDate"] = joinDate;

                if (DateTime.TryParse(GetValue(row, "Contract Start Date"), out DateTime startDate))
                    dr["ContractStartDate"] = startDate;

                if (DateTime.TryParse(GetValue(row, "Contract End Date"), out DateTime endDate))
                    dr["ContractEndDate"] = endDate;

                if (DateTime.TryParse(GetValue(row, "Effective End Date"), out DateTime EffcendDate))
                    dr["EffectEndDate"] = EffcendDate;

                dt.Rows.Add(dr);
            }

            return dt;
        }


        private DataTable MapPersonalInfo(DataTable dtRaw)
        {
            DataTable dt = new DataTable();

           
            dt.Columns.Add("EmployeeID");
            dt.Columns.Add("PlaceOfBirth");
            dt.Columns.Add("DateOfBirth", typeof(DateTime));
            dt.Columns.Add("Gender");
            dt.Columns.Add("BloodType");
            dt.Columns.Add("Nationality");
            dt.Columns.Add("NationalIDNumber");
            dt.Columns.Add("Address");
            dt.Columns.Add("Domicile");
            dt.Columns.Add("City");
            dt.Columns.Add("Province");
            dt.Columns.Add("PostalCode");
            dt.Columns.Add("MobilePhone");
            dt.Columns.Add("PersonalEmail");
            dt.Columns.Add("FamilyCardNumber");
            dt.Columns.Add("MaritalStatus");
            dt.Columns.Add("Religion");
            dt.Columns.Add("BankName");
            dt.Columns.Add("BankAccountNumber");
            dt.Columns.Add("BankAccountName");
            dt.Columns.Add("BPJSTKNumber");
            dt.Columns.Add("BPJSKSNumber");
            dt.Columns.Add("NPWPNumber");

            foreach (DataRow row in dtRaw.Rows)
            {
                string empId = GetValue(row, "Employee ID Number");

             
                if (string.IsNullOrWhiteSpace(empId))
                    continue;

                DataRow dr = dt.NewRow();

                dr["EmployeeID"] = empId;

            
                dr["PlaceOfBirth"] = GetValue(row, "Place of Birth");
                dr["Gender"] = GetValue(row, "Gender");
                dr["BloodType"] = GetValue(row, "Blood Type");
                dr["Nationality"] = GetValue(row, "Nationality");
                dr["NationalIDNumber"] = GetValue(row, "National ID Number");
                dr["Address"] = GetValue(row, "Address");
                dr["Domicile"] = GetValue(row, "Domicile");
                dr["City"] = GetValue(row, "City");
                dr["Province"] = GetValue(row, "Province");
                dr["PostalCode"] = GetValue(row, "Postal Code");
                dr["MobilePhone"] = GetValue(row, "Mobile Phone");
                dr["PersonalEmail"] = GetValue(row, "Personal Email");
                dr["FamilyCardNumber"] = GetValue(row, "Family Card Number");
                dr["MaritalStatus"] = GetValue(row, "Marital Status");
                dr["Religion"] = GetValue(row, "Religion");
                dr["BankName"] = GetValue(row, "Bank Name");
                dr["BankAccountNumber"] = GetValue(row, "Bank Account Number");
                dr["BankAccountName"] = GetValue(row, "Bank Account Name");
                dr["BPJSTKNumber"] = GetValue(row, "BPJS TK Number");
                dr["BPJSKSNumber"] = GetValue(row, "BPJS KS Number");
                dr["NPWPNumber"] = GetValue(row, "NPWP Number");

               
                if (DateTime.TryParse(GetValue(row, "Date of Birth"), out DateTime dob))
                    dr["DateOfBirth"] = dob;

                dt.Rows.Add(dr);
            }

            return dt;
        }


        private DataTable MapEducationInfo(DataTable dtRaw)
        {
            DataTable dt = new DataTable();

            
            dt.Columns.Add("EmployeeID");
            dt.Columns.Add("Education1");
            dt.Columns.Add("Major1");
            dt.Columns.Add("Institute1");
            dt.Columns.Add("Education2");
            dt.Columns.Add("Major2");
            dt.Columns.Add("Institute2");

            foreach (DataRow row in dtRaw.Rows)
            {
                string empId = GetValue(row, "Employee ID Number");

                if (string.IsNullOrWhiteSpace(empId))
                    continue;

                DataRow dr = dt.NewRow();

                dr["EmployeeID"] = empId;

                
                dr["Education1"] = GetValue(row, "Education 1");
                dr["Major1"] = GetValue(row, "Major 1");
                dr["Institute1"] = GetValue(row, "University/ Institute / School 1");

               
                dr["Education2"] = GetValue(row, "Education 2");
                dr["Major2"] = GetValue(row, "Major 2");
                dr["Institute2"] = GetValue(row, "University/ Institute / School 2");

                dt.Rows.Add(dr);
            }

            return dt;
        }


        private DataTable MapFamilyInfo(DataTable dtRaw)
        {
            DataTable dt = new DataTable();

           
            dt.Columns.Add("EmployeeID");

            dt.Columns.Add("SpouseName");
            dt.Columns.Add("SpouseIDNumber");
            dt.Columns.Add("SpousePlaceOfBirth");
            dt.Columns.Add("SpouseDateOfBirth", typeof(DateTime));

            dt.Columns.Add("Child1Name");
            dt.Columns.Add("Child1IDNumber");
            dt.Columns.Add("Child1PlaceOfBirth");
            dt.Columns.Add("Child1DateOfBirth", typeof(DateTime));

            dt.Columns.Add("Child2Name");
            dt.Columns.Add("Child2IDNumber");
            dt.Columns.Add("Child2PlaceOfBirth");
            dt.Columns.Add("Child2DateOfBirth", typeof(DateTime));

            dt.Columns.Add("Child3Name");
            dt.Columns.Add("Child3IDNumber");
            dt.Columns.Add("Child3PlaceOfBirth");
            dt.Columns.Add("Child3DateOfBirth", typeof(DateTime));

            foreach (DataRow row in dtRaw.Rows)
            {
                string empId = GetValue(row, "Employee ID Number");

                if (string.IsNullOrWhiteSpace(empId))
                    continue;

                DataRow dr = dt.NewRow();

                dr["EmployeeID"] = empId;

                
                dr["SpouseName"] = GetValue(row, "Spouse Name");
                dr["SpouseIDNumber"] = GetValue(row, "Spouse ID Number");
                dr["SpousePlaceOfBirth"] = GetValue(row, "Spouse Place of Birth");

                if (DateTime.TryParse(GetValue(row, "Spouse Date of Birth"), out DateTime spouseDob))
                    dr["SpouseDateOfBirth"] = spouseDob;

                
                dr["Child1Name"] = GetValue(row, "1st Child Name");
                dr["Child1IDNumber"] = GetValue(row, "1st Child ID Number");
                dr["Child1PlaceOfBirth"] = GetValue(row, "1st Child Place of Birth");

                if (DateTime.TryParse(GetValue(row, "1st Child Date of Birth"), out DateTime c1Dob))
                    dr["Child1DateOfBirth"] = c1Dob;

                dr["Child2Name"] = GetValue(row, "2nd Child Name");
                dr["Child2IDNumber"] = GetValue(row, "2nd Child ID Number");
                dr["Child2PlaceOfBirth"] = GetValue(row, "2nd Child Place of Birth");

                if (DateTime.TryParse(GetValue(row, "2nd Child Date of Birth"), out DateTime c2Dob))
                    dr["Child2DateOfBirth"] = c2Dob;

              
                dr["Child3Name"] = GetValue(row, "3rd Child Name");
                dr["Child3IDNumber"] = GetValue(row, "3rd Child ID Number");
                dr["Child3PlaceOfBirth"] = GetValue(row, "3rd Child Place of Birth");

                if (DateTime.TryParse(GetValue(row, "3rd Child Date of Birth"), out DateTime c3Dob))
                    dr["Child3DateOfBirth"] = c3Dob;

                dt.Rows.Add(dr);
            }

            return dt;
        }


        private DataTable MapEmergencyContact(DataTable dtRaw)
        {
            DataTable dt = new DataTable();

           
            dt.Columns.Add("EmployeeID");
            dt.Columns.Add("ContactName");
            dt.Columns.Add("ContactAddress");
            dt.Columns.Add("ContactRelationship");
            dt.Columns.Add("PhoneNumber");

            foreach (DataRow row in dtRaw.Rows)
            {
                string empId = GetValue(row, "Employee ID Number");

               
                if (string.IsNullOrWhiteSpace(empId))
                    continue;

                DataRow dr = dt.NewRow();

                dr["EmployeeID"] = empId;
                dr["ContactName"] = GetValue(row, "Contact Name");
                dr["ContactAddress"] = GetValue(row, "Contact Address");
                dr["ContactRelationship"] = GetValue(row, "Contact Relationship");
                dr["PhoneNumber"] = GetValue(row, "Phone Number");

                dt.Rows.Add(dr);
            }

            return dt;
        }


        private DataTable MapCareerHistory(DataTable dtRaw)
        {
            DataTable dt = new DataTable();

           
            dt.Columns.Add("EmployeeID");
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("Title");
            dt.Columns.Add("Division");
            dt.Columns.Add("Section");
            dt.Columns.Add("Subsection");
            dt.Columns.Add("EmploymentStatus");
            dt.Columns.Add("Location");
            dt.Columns.Add("Category");
            dt.Columns.Add("EffectiveDate", typeof(DateTime));

            foreach (DataRow row in dtRaw.Rows)
            {
                string empId = GetValue(row, "Employee ID Number");

                if (string.IsNullOrWhiteSpace(empId))
                    continue;

                DataRow dr = dt.NewRow();

                dr["EmployeeID"] = empId;
                dr["EmployeeName"] = GetValue(row, "Employee Name");
                dr["Title"] = GetValue(row, "Title");
                dr["Division"] = GetValue(row, "Division");
                dr["Section"] = GetValue(row, "Section");
                dr["Subsection"] = GetValue(row, "Subsection");
                dr["EmploymentStatus"] = GetValue(row, "Employment Status");
                dr["Location"] = GetValue(row, "Location");
                dr["Category"] = GetValue(row, "Category");

                
                if (DateTime.TryParse(GetValue(row, "Effective Date"), out DateTime effDate))
                    dr["EffectiveDate"] = effDate;

                dt.Rows.Add(dr);
            }

            return dt;
        }


        private DataTable MapWarningLetterHistory(DataTable dtRaw)
        {
            DataTable dt = new DataTable();

            
            dt.Columns.Add("EmployeeID");
            dt.Columns.Add("EmployeeName");
            //dt.Columns.Add("Title");
            //dt.Columns.Add("Division");
            //dt.Columns.Add("Section");
            //dt.Columns.Add("Subsection");
            //dt.Columns.Add("EmploymentStatus");
            //dt.Columns.Add("Location");
            dt.Columns.Add("WarningType");
            dt.Columns.Add("Category");
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));

            foreach (DataRow row in dtRaw.Rows)
            {
                string empId = GetValue(row, "Employee ID Number");

                if (string.IsNullOrWhiteSpace(empId))
                    continue;

                DataRow dr = dt.NewRow();

                dr["EmployeeID"] = empId;
                dr["EmployeeName"] = GetValue(row, "Employee Name");
                //dr["Title"] = GetValue(row, "Title");
                //dr["Division"] = GetValue(row, "Division");
                //dr["Section"] = GetValue(row, "Section");
                //dr["Subsection"] = GetValue(row, "Subsection");
                //dr["EmploymentStatus"] = GetValue(row, "Employment Status");
                //dr["Location"] = GetValue(row, "Location");
                dr["WarningType"] = GetValue(row, "Type of Warning");
                dr["Category"] = GetValue(row, "Category");
               
                if (DateTime.TryParse(GetValue(row, "Start date"), out DateTime startDate))
                    dr["StartDate"] = startDate;

                if (DateTime.TryParse(GetValue(row, "End date"), out DateTime endDate))
                    dr["EndDate"] = endDate;

                dt.Rows.Add(dr);
            }

            return dt;
        }





        private void ExportImportLog(DataTable dtLog, string fileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtLog, "Import Log");

                Response.Clear();
                Response.Buffer = true;

                Response.ContentType =
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader(
                    "content-disposition",
                    $"attachment;filename={fileName}"
                );

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);

                    ms.WriteTo(Response.OutputStream);

                    Response.Flush();
                    //Response.End();
                }

                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }



        protected void TableCandidateJoin_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableCandidateJoin_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableCandidateJoin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
            BindDataTableCandidateJoined();
            BindDataTableEmployees();
            txtFullname.Value = TableCandidateJoin.SelectedRow.Cells[5].Text.ToString();
            hlbKTP.Value = TableCandidateJoin.SelectedRow.Cells[6].Text.ToString();
            txtBirthDate.Value = TableCandidateJoin.SelectedRow.Cells[16].Text.ToString();
            hlbURLPhoto.Value = TableCandidateJoin.SelectedRow.Cells[19].Text.ToString();
            txtAddress.Value = TableCandidateJoin.SelectedRow.Cells[10].Text.ToString();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("employeess_detail.aspx?NewEmployee=true");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
            //btnSubmit.Visible = true;
            ////btnAdd.Visible = true;
            //btnNew.Visible = true;
            //btnUpdate.Visible = false;
            //divIntitution.Visible = false;

            //txtNIK.Disabled = false;
            //txtJoinDate.Disabled = false;
            //txtFullname.Disabled = false;
            //txtBirthDate.Disabled = false;
            ////BindDataTableEmployees();

            //GetPositionName();
            //GetDivisionName();
            //GetSectionName();
            //GetSubSectionName();
            //GetReportPositionName();
            //GetLocation();
            //GetInstitutionName();
            //BindDataTableCandidateJoined();

            //GetMaster();

            //TableEmployees.Columns[2].Visible = false;
            //TableEmployees.Columns[22].Visible = false;
            //TableEmployees.Columns[23].Visible = false;
            //TableEmployees.Columns[24].Visible = false;
            //TableEmployees.Columns[25].Visible = false;
            //TableEmployees.Columns[26].Visible = false;
            //TableEmployees.Columns[27].Visible = false;
            //TableEmployees.Columns[28].Visible = false;
            //TableEmployees.Columns[29].Visible = false;
            //TableEmployees.Columns[30].Visible = false;

            //TableEmployees.UseAccessibleHeader = true;
            //TableEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            Response.Redirect("employeess.aspx");
        }

        protected void TableEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "50px");
            }
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("employeess.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlTableCandidateJoin').modal();", true);
            BindDataTableCandidateJoined();
            BindDataTableEmployees();
        }

        protected void btnCloseModalcandidatejoin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
            BindDataTableCandidateJoined();
            BindDataTableEmployees();
        }


        protected void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string startDate = Request.Form["startDate"];
                string endDate = Request.Form["endDate"];

                /* =========================================
                   VALIDASI
                ========================================= */

                if (string.IsNullOrWhiteSpace(startDate) && string.IsNullOrWhiteSpace(endDate))
                {
                    Session["ImportError"] = "Start Date / End Date is required";
                    Response.Redirect(Request.RawUrl, false);

                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }


                DateTime? start = null;
                DateTime? end = null;


                if (!string.IsNullOrWhiteSpace(startDate))
                {
                    start = Convert.ToDateTime(startDate);
                }

                if (!string.IsNullOrWhiteSpace(endDate))
                {
                    end = Convert.ToDateTime(endDate);
                }


                if (start.HasValue && end.HasValue && start > end )
                {
                    Session["ImportError"] = "Start Date cannot be greater than End Date";
                    Response.Redirect(Request.RawUrl, false);

                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }



                /* =========================================
                   CALL STORE PROCEDURE
                ========================================= */

                DataSet ds = new DataSet();

                using ( SqlConnection con =  new SqlConnection( ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GenerateAll", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@StartDate",(object)start ?? DBNull.Value);
                        cmd.Parameters.AddWithValue( "@EndDate", (object)end ?? DBNull.Value );
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }


                Session["ExportMasterEmployee"] = ds;
                Session["ImportSuccess"] = "Import successfully";
                Session["ImportLog"] = null;

                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();


                return;


            }
            catch (Exception ex)
            {
                Session["ImportError"] = ex.Message;
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

        }













        //protected void btnGenerateExcel_Click(object sender, EventArgs e)
        //{
        //    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
        //    SqlConnection Con = new SqlConnection(path);
        //    Con.Open();
        //    SqlCommand sqlcomm = new SqlCommand();
        //    sqlcomm.CommandText = "sp_HRIS_DB_Employees";
        //    sqlcomm.CommandType = CommandType.StoredProcedure;
        //    sqlcomm.Connection = Con;
        //    sqlcomm.Parameters.AddWithValue("@StatementType", "GenerateExcel");

        //    SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
        //    using (DataTable dt = new DataTable())
        //    {
        //        //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
        //        //sFileName = sFileName.Replace("/", "");
        //        sda.Fill(dt);
        //        string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            wb.Worksheets.Add(dt, "Data Master Employees");
        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.Charset = "";
        //            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
        //            Response.ClearContent();
        //            Response.AppendHeader("content-disposition", "attachment; filename=" + "Data Master Employees" + "_" + _vDate1 + ".xlsx");
        //            Response.ContentType = "application/excel";
        //            using (MemoryStream MyMemoryStream = new MemoryStream())
        //            {
        //                wb.SaveAs(MyMemoryStream);
        //                MyMemoryStream.WriteTo(Response.OutputStream);
        //                Response.Flush();
        //                Response.End();
        //            }
        //        }
        //    }
        //}

        //protected void btnHistoriMutasi_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlHistoryMutation').modal();", true);
        //    BindDataTableEmployees();
        //}

        //protected void btnCloseModalHistory_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("employeess.aspx");
        //}



        //protected void btnCheckHistory_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlTableHistoryMutation').modal();", true);
        //    CheckHistoryMutation();
        //    BindDataTableEmployees();
        //}

        protected void TableHistoryMutation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "50px");
            }
        }

        protected void TableHistoryMutation_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableHistoryMutation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCloseModalTableHistory_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlHistoryMutation').modal();", true);
            BindDataTableEmployees();
        }

        protected void btnChangeFoto_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
            BindDataTableEmployees();
        }

        protected void btnUploadFoto_Click(object sender, EventArgs e)
        {

            if (FileUploadFoto.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = FileUploadFoto.PostedFiles.Count();
                if (filecount <= 1)
                {
                    foreach (HttpPostedFile postfiles in FileUploadFoto.PostedFiles)
                    {
                        //Get The File Extension  
                        string filetype = Path.GetExtension(postfiles.FileName);
                        if (filetype.ToLower() == ".jpeg")
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
                                    case ".jpeg":
                                        serverfolder = Server.MapPath("~/Photos/");
                                        //check Folder avlalible or not  
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // create Folder  
                                            Directory.CreateDirectory(serverfolder);
                                        }
                                        serverpath = serverfolder + hlbDetailNIK.Text.ToString() + ".jpg";
                                        FileUploadFoto.SaveAs(serverpath);
                                        label1.Text += "[" + postfiles.FileName + "]- jpg file uploaded successfully<br/>";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                                        BindDataTableEmployees();
                                        UploadFotoOK.Value = "OK";
                                        break;

                                }

                            }
                            else
                            {

                                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                                BindDataTableEmployees();
                            }
                        }
                        else
                        {

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                            BindDataTableEmployees();
                        }
                    }
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                    BindDataTableEmployees();
                }

            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedUpload();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                BindDataTableEmployees();
            }
        }
        protected void btnCloseModalImport_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlTableDetailEmployee').modal();", true);
            BindDataTableEmployees();
        }
        protected void DownloadTemplete_Click(object sender, EventArgs e)
        {
            
            string filePath = Path.GetFileName(Server.MapPath("~/TemplateImportExcel/Template Import Data Employees.xlsx"));
            Response.ContentType = "Application/x-msexcel";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(filePath));
            Response.TransmitFile(Server.MapPath("~/TemplateImportExcel/Template Import Data Employees.xlsx"));
            Response.End();

        }
        //private void BindGridEmergencyContact()
        //{
            //string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            //SqlConnection Con = new SqlConnection(path);
            //Con.Open();
            //SqlCommand sqlcomm = new SqlCommand();
            //sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            //sqlcomm.CommandType = CommandType.StoredProcedure;
            //sqlcomm.Connection = Con;
            //sqlcomm.Parameters.AddWithValue("@StatementType", "GetEmergencyContact");
            //sqlcomm.Parameters.AddWithValue("@id_employee", hlbDetailNIK.Text.ToString());
            //SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            //using (DataTable dt = new DataTable())
            //{
            //    sda.Fill(dt);
            //    TableEditEmergencyContact.DataSource = dt;
            //    TableEditEmergencyContact.DataBind();
            //}

            //TableEditEmergencyContact.DataSource

        //}

        //protected void btnEmergencyContactUpdate_Click(object sender, EventArgs e)
        //{
        //        if (txtEmergencyContactName.Value.Length > 0)
        //        {
        //            txtEmergencyContactName.Value = txtEmergencyContactName.Value + " " + lblEmergencyContactID.Value;
        //            // Use the data as needed, e.g., populate fields or show a modal
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "Func()", true);
        //            BindDataTableEmployees();
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSave();", true);
        //            BindDataTableEmployees();
        //        }
        //}
        protected void TableEditEmergencyContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CustomSelect")
            {
                string oid = e.CommandArgument.ToString();

                // Example: Update labels or hidden fields
                lblEditHeaderEmergencyContact.Text = "Selected Emergency Contact: " + oid;
                lblEmergencyContactID.Value = oid;
            }
        }
        //protected void TableEditEmergencyContact_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //GridViewRow row = TableEditEmergencyContact.SelectedRow;
        //    //if (row != null)
        //    //{
        //        lblEditHeaderEmergencyContact.Text = "Edit Emergency Contact List";

        //        //// Example: get values from the row's cells
        //        //lblEmergencyContactID.Value = row.Cells[1].Text; // Adjust index as needed
        //        //txtEmergencyContactName.Value = row.Cells[2].Text;
        //        //txtEmergencyContactAddress.Value = row.Cells[3].Text;
        //        //txtEmergencyContactRelationship.Value = row.Cells[4].Text;
        //        //txtEmergencyContactNumber.Value = row.Cells[5].Text;

        //        //// Use the data as needed, e.g., populate fields or show a modal
        //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
        //    //}
        //}
        protected void btnEmergencyContactNew_Click(object sender, EventArgs e)
        {

        }
        protected void ddlInstitution_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlBloodType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlNationality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlSourceOfHire_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlPOB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlSpousePOB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddl1stChildPOB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddl2ndChildPOB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddl3rdChildPOB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void TableEmergencyContact_DataBound(object sender, EventArgs e)
        {

        }
        protected void TableEmergencyContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void TableEditCareerHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow && TableEditCareerHistory.EditIndex == e.Row.RowIndex)
            //{
            //    DropDownList ddlCategoryEdit = (DropDownList)e.Row.FindControl("ddlCategoryEdit");
            //    if (ddlCategoryEdit != null)
            //    {
            //        // Example: Populate dropdown with static values
            //        ddlCategoryEdit.Items.Add(new ListItem("Promotion", "Promotion"));
            //        ddlCategoryEdit.Items.Add(new ListItem("Transfer", "Transfer"));
            //        ddlCategoryEdit.Items.Add(new ListItem("Demotion", "Demotion"));
            //        // Set selected value
            //        string currentValue = DataBinder.Eval(e.Row.DataItem, "Category").ToString();
            //        ddlCategoryEdit.SelectedValue = currentValue;
            //    }
            //}

            if (e.Row.RowType == DataControlRowType.DataRow /*&& TableEditCareerHistory.EditIndex == e.Row.RowIndex*/)
            {
                string id = TableEditCareerHistory.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Attributes["data-id"] = id;

                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                string categoryValue = DataBinder.Eval(e.Row.DataItem, "Category").ToString();
                if (ddlCategory != null && !string.IsNullOrEmpty(categoryValue))
                {
                    ddlCategory.SelectedValue = categoryValue;
                }
            }
        }
        protected void TableEditCareerHistory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //TableEditCareerHistory.EditIndex = e.NewEditIndex;
            //// Rebind the data to show the edit controls
            //BindDataTableCareerHistory(TableEditCareerHistory, hlbID.Value);
        }
        protected void TableEditCareerHistory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //    // Get the row being updated
            //    GridViewRow row = TableEditCareerHistory.Rows[e.RowIndex];
            //    string id = TableEditCareerHistory.DataKeys[e.RowIndex].Value.ToString();

            //    // Get the new value from the TextBox in the EditItemTemplate
            //    TextBox txtCategory = (TextBox)row.FindControl("txtCategory");
            //    string newCategory = txtCategory.Text;

            //    // Update the database
            //    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(path))
            //    using (SqlCommand cmd = new SqlCommand("sp_HRIS_DB_Employees", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@StatementType", "UpdateCareerHistoryCategory");
            //        cmd.Parameters.AddWithValue("@id", id);
            //        cmd.Parameters.AddWithValue("@Category", newCategory);
            //        con.Open();
            //        cmd.ExecuteNonQuery();
            //    }

            //    TableEditCareerHistory.EditIndex = -1;
            //    BindDataTableCareerHistory(TableEditCareerHistory, hlbID.Value);
        }
        protected void TableEditCareerHistory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //    TableEditCareerHistory.EditIndex = -1;
            //    BindDataTableCareerHistory(TableEditCareerHistory, hlbID.Value);
            //}
        }
        protected void btnDownload_eDoc_Click(object sender, EventArgs e)
        {

        }
        protected void btnHapus_eDoc_Click(object sender, EventArgs e)
        {

        }
        protected void btnAdd_eDoc_Click(object sender, EventArgs e)
        {
            //GetDocType();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadeDocs').modal();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewEmployee').modal();", true);
            
        }
        protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileUploadEDocs.Enabled = true;
            txtDescription.Disabled = true;
            txtDescription.Value = ddlDocType.SelectedItem.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadeDocs').modal();", true);
        }
        protected void btnUploadeDoc_Click(object sender, EventArgs e)
        {
            CheckUploadDocument();
            if (lblOK.Value == "OK")
            {
                string _vPath = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection _vSQLCon = new SqlConnection(_vPath);
                SqlCommand _vSQLComm = new SqlCommand();

                _vSQLCon.Open();
                _vSQLComm.Connection = _vSQLCon;
                _vSQLComm.CommandType = CommandType.StoredProcedure;
                _vSQLComm.CommandText = "sp_HRIS_DB_EDocs";

                _vSQLComm.Parameters.AddWithValue("@Flag", "Save");
                _vSQLComm.Parameters.AddWithValue("@Module", "EmployeeDetail");
                _vSQLComm.Parameters.AddWithValue("@Description", txtDescription.Value);
                _vSQLComm.Parameters.AddWithValue("@NIK", txtNIK.Value);
                if (ddlDocType.SelectedValue.ToString()== "SP1" || ddlDocType.SelectedValue.ToString() =="SP2")
                {
                    _vSQLComm.Parameters.AddWithValue("@Category", "SP");
                }
                else
                { _vSQLComm.Parameters.AddWithValue("@Category", "Other"); }

                _vSQLComm.Parameters.AddWithValue("@DocumentType", ddlDocType.SelectedValue);
                _vSQLComm.Parameters.AddWithValue("@FileName", lblFileName.Value);
                //_vSQLComm.Parameters.AddWithValue("@Code", txtHouseRef.Value); //Session["Code"].ToString());
                _vSQLComm.Parameters.AddWithValue("@User", Session["nik"].ToString());

                _vSQLComm.ExecuteNonQuery();

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                //string ShipmentID = lblShipmentID.Value;
                string script = $@"
                                    $(document).ready(function() {{
                                        // Show Toastr notification
                                        toastr.success('Your operation was successful, Please wait to redirect the page!', 'Submit Success');

                                        // Redirect after 2 seconds (2000 milliseconds)
                                        setTimeout(function() {{
                                            window.location.href = 'employeess.aspx'; // replace with your target URL
                                        }}, 2000);
                                    }});
                                ";

                // Register the script for partial postbacks
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastrRedirect", script, true);

                _vSQLComm.Parameters.Clear();
                _vSQLComm.Dispose();
                _vSQLCon.Dispose();
                _vSQLCon.Close();
            }
            ViewEdocsFilesUpload();
        }


        //protected void Search_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox txtSearch = sender as TextBox;

        //    if (txtSearch != null)
        //    {
        //        // Determine which TextBox triggered the event and update the corresponding ViewState
        //        switch (txtSearch.ID)
        //        {
        //            case "txtSearchID":
        //                ViewState["SearchID"] = txtSearch.Text.Trim();
        //                break;
        //            case "txtSearchStatus":
        //                ViewState["SearchStatus"] = txtSearch.Text.Trim();
        //                break;
        //            case "txtSearchCategory":
        //                ViewState["SearchCategory"] = txtSearch.Text.Trim();
        //                break;
        //            case "txtSearchEmployeeID":
        //                ViewState["SearchEmployeeID"] = txtSearch.Text.Trim();
        //                break;
        //            case "txtSearchFullName":
        //                ViewState["SearchFullName"] = txtSearch.Text.Trim();
        //                break;
        //        }

        //        // Rebind the GridView with the updated search criteria
        //        //BindDataTableEmployees();
        //    }
        //}
    }
}