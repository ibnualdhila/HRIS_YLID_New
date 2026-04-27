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
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class index : System.Web.UI.Page
    {
        string GROUP_FIN_ACC_MANAGER = WebConfigurationManager.AppSettings["GROUP_FIN_ACC_MANAGER"];
        string GROUP_FIN_ACC_SENIOR_MANAGER = WebConfigurationManager.AppSettings["GROUP_FIN_ACC_SENIOR_MANAGER"];

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            //{
            //    Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            //}
            hlbOidEmployees.Value = Session["Oid"].ToString();
            if (!IsPostBack)
            {
                TableEmployees.Visible = false;
                TablePersonalInformation.Visible = false;
                TableCompanyAgreement.Visible = false;
                TableEmployeeWillRetired.Visible = false;
                TableListEmployees.Visible = false;

                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_AccessGroup";
                sqlcomm.CommandType = CommandType.StoredProcedure;

                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "View");
                sqlcomm.Parameters.AddWithValue("@Oid_usermanagement", hlbOidEmployees.Value);

                SqlDataReader dr = null;
                dr = sqlcomm.ExecuteReader();

                if (dr.Read())
                {
                    Session.Add("Oid_UserManagement_AccessGroup", (Guid)dr["Oid_UserManagement_AccessGroup"]);
                    Session.Add("Oid_UserManagement", (Guid)dr["Oid_UserManagement"]);
                    Session.Add("EmployeeIDNumber_UserManagement_AccessGroup", (string)dr["EmployeeIDNumber_UserManagement_AccessGroup"]);
                    Session.Add("Fullname_UserManagement_AccessGroup", (string)dr["Fullname_UserManagement_AccessGroup"]);
                    Session.Add("IsActive", (string)dr["IsActive"].ToString());
                    Session.Add("GroupName", (string)dr["GroupName"]);
                    Session.Add("UserManagement_Group", (Guid)dr["UserManagement_Group"]);
                }
                else
                {

                }

                hlbLvlUser.Value = Session["GroupName"].ToString();

                String lvluser = hlbLvlUser.Value;
                if (lvluser.Contains("HR General"))
                {
                    ViewHRGeneral.Visible = true;
                    ViewHRTraining.Visible = false;
                    divListEmployees.Visible = false;

                    GetDataTotalNewEmployee();
                    lbTotalEmployees.Text = Session["Total Employee"].ToString();

                    GetDataTotalOutSourcedEmployee();
                    lbTotalOutsourcedEmployees.Text = Session["OutSourcedEmployee"].ToString();

                    GetDataTotalJobApplication();
                    lbTotalJobApplication.Text = Session["Apply Total"].ToString();

                    GetDataTotalCompanyAgreementWillExpired();
                    lbTotalCompanyAgreementWillExpired.Text = Session["Total Agreement"].ToString();

                    GetDataTotalEmployeeAgreementWillExpired();
                    lbTotalEmployeeAgreementWillExpired.Text = Session["Total Agreement"].ToString();

                    GetDataTotalEmployeeWillRetired();
                    lbTotalEmployeeWillRetired.Text = Session["Total Employee Retired"].ToString();

                    //GetDataNotifCompanyAgreementWillBeExpired();
                    //GetDataNotifEmployeeAgreementWillBeExpired();

                }
                else if (lvluser.Contains("HR Training"))
                {
                    ViewHRTraining.Visible = true;
                    ViewHRGeneral.Visible = false;
                    divListEmployees.Visible = false;

                    GetDataTotalTraining();
                    lbTotalTraining.Text = Session["Total Training"].ToString();

                    GetDataTotalParticipantTraining();
                    lbTotalParticipant.Text = Session["Total Participant"].ToString();

                    GetDataTrainingWilBeExpired();
                    lbWillbeExpired.Text = Session["TotalValidityWillBeExpired"].ToString();

                    GetDataTrainingExpired();
                    lbExpired.Text = Session["TotalValidityExpired"].ToString();

                    GetDataTrainingResult();
                    if (Session["Pass"] != null) { lblPass.Text = Session["Pass"].ToString(); } else { lblPass.Text = "0"; }
                    if (Session["Fail"] != null) { lblFail.Text = Session["Fail"].ToString(); } else { lblFail.Text = "0"; }

                    //Convert % TrainingResult
                    if (lblPass.Text == "0" && lblFail.Text == "0")
                    {
                        lblPersenPass.Text = "(0 %)";
                        lblPersenFail.Text = "(0 %)";
                    }
                    else
                    {
                        int valuePass = Convert.ToInt32(lblPass.Text);
                        int valueFail = Convert.ToInt32(lblFail.Text);
                        int totalValue = valuePass + valueFail;

                        double percentagePass = (((double)valuePass / (double)totalValue) * 100);
                        double percentageFail = (((double)valueFail / (double)totalValue) * 100);
                        lblPersenPass.Text = "(" + percentagePass.ToString("0.00") + "%" + ")";
                        lblPersenFail.Text = "(" + percentageFail.ToString("0.00") + "%" + ")";
                    }

                }
                else if (lvluser.Contains("User"))
                {
                    TableListEmployees.Visible = true;
                    ViewTableDetail.Visible = true;
                    divListEmployees.Visible = true;
                    ViewHRTraining.Visible = false;
                    ViewHRGeneral.Visible = false;

                    TableEmployees.Visible = false;
                    TablePersonalInformation.Visible = false;
                    TableCompanyAgreement.Visible = false;
                    TableEmployeeAgreement.Visible = false;
                    TableEmployeeWillRetired.Visible = false;
                    TableTrainingActiveTotal.Visible = false;
                    TableTrainingParticipant.Visible = false;
                    TableParticipantTrainingPass.Visible = false;
                    TableParticipantTrainingFail.Visible = false;
                    lbNameTable.Text = "List of Employees";

                    GetChartGroupDetail();

                    if (hlbNameChartGroup.Value == GROUP_FIN_ACC_MANAGER)
                    {
                        GetListEmployeesFIN_ACC_MANAGER();
                    }
                    else if (hlbNameChartGroup.Value == GROUP_FIN_ACC_SENIOR_MANAGER)
                    {
                        GetListEmployeesFIN_ACC_SENIOR_MANAGER();
                    }
                    else
                    {
                        GetListEmployees();
                    }
                    
                }
                else if (lvluser.Contains("HR Recruitment"))
                {
                    TableListEmployees.Visible = false;
                    ViewTableDetail.Visible = false;
                    ViewHRTraining.Visible = false;
                    ViewHRGeneral.Visible = false;
                    divListEmployees.Visible = false;

                    TableEmployees.Visible = false;
                    TablePersonalInformation.Visible = false;
                    TableCompanyAgreement.Visible = false;
                    TableEmployeeAgreement.Visible = false;
                    TableEmployeeWillRetired.Visible = false;
                    TableTrainingActiveTotal.Visible = false;
                    TableTrainingParticipant.Visible = false;
                    TableParticipantTrainingPass.Visible = false;
                    TableParticipantTrainingFail.Visible = false;
                    lbNameTable.Text = "List of Employees";

                    //GetListEmployees();

                }
                else if (lvluser.Contains("Admin"))
                {
                    ViewHRGeneral.Visible = true;
                    ViewHRTraining.Visible = true;
                    divListEmployees.Visible = false;
                    GetDataTotalNewEmployee();
                    lbTotalEmployees.Text = Session["Total Employee"].ToString();

                    GetDataTotalOutSourcedEmployee();
                    lbTotalOutsourcedEmployees.Text = Session["OutSourcedEmployee"].ToString();

                    GetDataTotalJobApplication();
                    lbTotalJobApplication.Text = Session["Apply Total"].ToString();

                    GetDataTotalCompanyAgreementWillExpired();
                    lbTotalCompanyAgreementWillExpired.Text = Session["Total Agreement"].ToString();

                    GetDataTotalEmployeeAgreementWillExpired();
                    lbTotalEmployeeAgreementWillExpired.Text = Session["Total Agreement"].ToString();

                    GetDataTotalTraining();
                    lbTotalTraining.Text = Session["Total Training"].ToString();

                    GetDataTotalParticipantTraining();
                    lbTotalParticipant.Text = Session["Total Participant"].ToString();

                    GetDataTotalEmployeeWillRetired();
                    lbTotalEmployeeWillRetired.Text = Session["Total Employee Retired"].ToString();

                    GetDataTrainingWilBeExpired();
                    lbWillbeExpired.Text = Session["TotalValidityWillBeExpired"].ToString();

                    GetDataTrainingExpired();
                    lbExpired.Text = Session["TotalValidityExpired"].ToString();

                    GetDataTrainingResult();
                    if (Session["Pass"] != null) { lblPass.Text = Session["Pass"].ToString(); } else { lblPass.Text = "0"; }
                    if (Session["Fail"] != null) { lblFail.Text = Session["Fail"].ToString(); } else { lblFail.Text = "0"; }

                    //Convert % TrainingResult
                    if (lblPass.Text == "0" && lblFail.Text == "0")
                    {
                        lblPersenPass.Text = "(0 %)";
                        lblPersenFail.Text = "(0 %)";
                    }
                    else
                    {
                        int valuePass = Convert.ToInt32(lblPass.Text);
                        int valueFail = Convert.ToInt32(lblFail.Text);
                        int totalValue = valuePass + valueFail;

                        double percentagePass = (((double)valuePass / (double)totalValue) * 100);
                        double percentageFail = (((double)valueFail / (double)totalValue) * 100);
                        lblPersenPass.Text = "(" + percentagePass.ToString("0.00") + "%" + ")";
                        lblPersenFail.Text = "(" + percentageFail.ToString("0.00") + "%" + ")";
                    }

                    //GetDataNotifCompanyAgreementWillBeExpired();
                    //GetDataNotifEmployeeAgreementWillBeExpired();

                }
                else if (lvluser.Contains("IT Development"))
                {
                    TableListEmployees.Visible = false;
                    ViewTableDetail.Visible = false;
                    divListEmployees.Visible = false;
                    ViewHRTraining.Visible = false;
                    ViewHRGeneral.Visible = false;

                    TableEmployees.Visible = false;
                    TablePersonalInformation.Visible = false;
                    TableCompanyAgreement.Visible = false;
                    TableEmployeeAgreement.Visible = false;
                    TableEmployeeWillRetired.Visible = false;
                    TableTrainingActiveTotal.Visible = false;
                    TableTrainingParticipant.Visible = false;
                    TableParticipantTrainingPass.Visible = false;
                    TableParticipantTrainingFail.Visible = false;
                    lbNameTable.Visible = false;
                }
                else
                {
                    TableListEmployees.Visible = true;
                    ViewTableDetail.Visible = true;
                    divListEmployees.Visible = true;
                    ViewHRTraining.Visible = false;
                    ViewHRGeneral.Visible = false;

                    TableEmployees.Visible = false;
                    TablePersonalInformation.Visible = false;
                    TableCompanyAgreement.Visible = false;
                    TableEmployeeAgreement.Visible = false;
                    TableEmployeeWillRetired.Visible = false;
                    TableTrainingActiveTotal.Visible = false;
                    TableTrainingParticipant.Visible = false;
                    TableParticipantTrainingPass.Visible = false;
                    TableParticipantTrainingFail.Visible = false;
                    //lbNameTable.Visible = false;
                }
            }
        }

        protected void GetChartGroupDetail()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup_Detail";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewOrganizationAccess");
            sqlcomm.Parameters.AddWithValue("@Oid", hlbOidEmployees.Value);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    hlbNameChartGroup.Value = (string)dr["GroupName"];

                }

            }
            else
            {
                Con.Close();
            }
        }

        protected void GetListEmployees()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_RolesViewOrganizationChart";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@Employee", Session["Employees"].ToString());

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableListEmployees.DataSource = dtb;
            TableListEmployees.DataBind();

            TableListEmployees.UseAccessibleHeader = true;
            TableListEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        protected void GetListEmployeesFIN_ACC_MANAGER()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_RolesViewOrganizationChart_FIN_ACC_MANAGER";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@Employee", Session["Employees"].ToString());

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableListEmployees.DataSource = dtb;
            TableListEmployees.DataBind();

            TableListEmployees.UseAccessibleHeader = true;
            TableListEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        protected void GetListEmployeesFIN_ACC_SENIOR_MANAGER()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_RolesViewOrganizationChart_FIN_ACC_SENIOR_MANAGER";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@Employee", Session["Employees"].ToString());

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableListEmployees.DataSource = dtb;
            TableListEmployees.DataBind();

            TableListEmployees.UseAccessibleHeader = true;
            TableListEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        protected void GetDataTotalNewEmployee()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "TotalEmployee");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Total Employee", (Int32)dr["Total Employee"]);
            }
            else
            {

            }
        }

        protected void GetDataTotalOutSourcedEmployee()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "TotalOutSourcedEmployee");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("OutSourcedEmployee", (Int32)dr["OutSourcedEmployee"]);
            }
            else
            {

            }
        }

        protected void GetDataTotalJobApplication()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "JobApplication");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Apply Total", (Int32)dr["Apply Total"]);
            }
            else
            {

            }
        }

        protected void GetDataTotalCompanyAgreementWillExpired()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "WillBeExpired");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Total Agreement", (Int32)dr["Total Agreement"]);
            }
            else
            {

            }
        }

        protected void GetDataTotalEmployeeAgreementWillExpired()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "WillBeEndOff");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Total Agreement", (Int32)dr["Total Agreement"]);
            }
            else
            {

            }
        }

        protected void GetDataTotalEmployeeWillRetired()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "TotalEmployeeWillRetired");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Total Employee Retired", (Int32)dr["Total Employee Retired"]);
            }
            else
            {

            }
        }

        protected void GetDataTotalTraining()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "TrainingActive");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Total Training", (Int32)dr["Total Training"]);
            }
            else
            {

            }
        }

        protected void GetDataTotalParticipantTraining()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ParticipantTraining");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Total Participant", (Int32)dr["Total Participant"]);
            }
            else
            {

            }
        }

        protected void GetDataTrainingResult()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "TrainingResult");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Pass", (Int32)dr["Pass"]);
                Session.Add("Fail", (Int32)dr["Fail"]);
            }
            else
            {

            }
        }

        protected void GetDataTrainingWilBeExpired()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "TotalValidityWillBeExpired");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("TotalValidityWillBeExpired", (Int32)dr["TotalValidityWillBeExpired"]);
            }
            else
            {

            }
        }

        protected void GetDataTrainingExpired()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "TotalValidityExpired");

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("TotalValidityExpired", (Int32)dr["TotalValidityExpired"]);
            }
            else
            {

            }
        }

        protected void ViewDetailEmployees_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TableEmployees.Visible = true;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible=false;
            lbNameTable.Text = "List of Employees";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailTotalEmployeeActive");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployees.DataSource = dtb;
            TableEmployees.DataBind();

            TableEmployees.UseAccessibleHeader = true;
            TableEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void ViewDetailOutsourcedEmployees_Click(object sender, EventArgs e)
        {

            ViewTableDetail.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible = true;
            lbNameTable.Text = "List of Outsourced Employees";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailTotalOutsourcedEmployeeActive");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableOutsourcedEmployees.DataSource = dtb;
            TableOutsourcedEmployees.DataBind();

            TableOutsourcedEmployees.UseAccessibleHeader = true;
            TableOutsourcedEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void ViewDetailJobApplication_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TablePersonalInformation.Visible = true;
            TableEmployees.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible = false;
            lbNameTable.Text = "List of Job Applications";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailJobApplication");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TablePersonalInformation.DataSource = dtb;
            TablePersonalInformation.DataBind();

            TablePersonalInformation.UseAccessibleHeader = true;
            TablePersonalInformation.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void ViewDetailCompanyAgreementWillExpired_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TableCompanyAgreement.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible = false;
            lbNameTable.Text = "List of Company Agreements";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailCompanyAgreementWillBeExpired");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableCompanyAgreement.DataSource = dtb;
            TableCompanyAgreement.DataBind();

            TableCompanyAgreement.UseAccessibleHeader = true;
            TableCompanyAgreement.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void ViewDetailEmployeeAgreementWillExpired_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TableEmployeeAgreement.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible = false;
            lbNameTable.Text = "List of Employee Agreements";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailEmployeeAgreementWillBeEndOff");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployeeAgreement.DataSource = dtb;
            TableEmployeeAgreement.DataBind();

            TableEmployeeAgreement.UseAccessibleHeader = true;
            TableEmployeeAgreement.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void ViewDetailTotalTraining_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TableTrainingActiveTotal.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible = false;
            lbNameTable.Text = "List of Trainings";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailTrainingActive");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableTrainingActiveTotal.DataSource = dtb;
            TableTrainingActiveTotal.DataBind();

            TableTrainingActiveTotal.UseAccessibleHeader = true;
            TableTrainingActiveTotal.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void ViewDetailTotalParticipant_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = true;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible = false;
            lbNameTable.Text = "List of Training Participants";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailParticipantTraining");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableTrainingParticipant.DataSource = dtb;
            TableTrainingParticipant.DataBind();

            TableTrainingParticipant.UseAccessibleHeader = true;
            TableTrainingParticipant.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void DetailPass_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TableParticipantTrainingPass.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible = false;
            lbNameTable.Text = "List of Participant Passed The Training";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewDetailParticipantPass");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableParticipantTrainingPass.DataSource = dtb;
            TableParticipantTrainingPass.DataBind();

            TableParticipantTrainingPass.UseAccessibleHeader = true;
            TableParticipantTrainingPass.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void DetailFail_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = true;
            TableOutsourcedEmployees.Visible = false;
            lbNameTable.Text = "List of Participants Who Failed a Training";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewDetailParticipantFail");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableParticipantTrainingFail.DataSource = dtb;
            TableParticipantTrainingFail.DataBind();

            TableParticipantTrainingFail.UseAccessibleHeader = true;
            TableParticipantTrainingFail.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void ViewDetailEmployeeWillRetired_Click(object sender, EventArgs e)
        {
            
            ViewTableDetail.Visible = true;
            TableEmployeeWillRetired.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = false;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            TableOutsourcedEmployees.Visible = false;
            lbNameTable.Text = "List of Retiring Employees";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewDetailEmployeeBefore3MonthRetired");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployeeWillRetired.DataSource = dtb;
            TableEmployeeWillRetired.DataBind();

            TableEmployeeWillRetired.UseAccessibleHeader = true;
            TableEmployeeWillRetired.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void GetDataTableTrainingParticipanTotal()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailParticipantTraining");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableTrainingParticipant.DataSource = dtb;
            TableTrainingParticipant.DataBind();

            TableTrainingParticipant.UseAccessibleHeader = true;
            TableTrainingParticipant.HeaderRow.TableSection = TableRowSection.TableHeader;
            Con.Close();
        }

        protected void btnViewTraining_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            GetDataTableTrainingParticipanTotal();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewTraining').modal();", true);

            ViewTableDetail.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = true;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            lbNameTable.Text = "List of Training Participants";
            hlbNIKParticipant.Value= row.Cells[1].Text.ToString();

            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewParticipantTrainingDetail");
            sqlcomm.Parameters.AddWithValue("@nik_participant", row.Cells[1].Text.ToString());

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableViewTraining.DataSource = dtb;
            TableViewTraining.DataBind();

            TableViewTraining.UseAccessibleHeader = true;
            TableViewTraining.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void TableViewTraining_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableViewTraining_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableViewTraining_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableCompanyAgreement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Int32 daytoexpired;
                daytoexpired = Convert.ToInt32(e.Row.Cells[8].Text.ToString());
                if (daytoexpired > 0)
                {
                    e.Row.Cells[8].Text = daytoexpired + " " + "Days before expiry date!";
                }
                else
                {
                    e.Row.Cells[8].Text = "has expired";
                    e.Row.Cells[8].ForeColor = Color.Red;
                    e.Row.Cells[0].ForeColor = Color.Red;
                    e.Row.Cells[1].ForeColor = Color.Red;
                    e.Row.Cells[2].ForeColor = Color.Red;
                    e.Row.Cells[3].ForeColor = Color.Red;
                    e.Row.Cells[4].ForeColor = Color.Red;
                    e.Row.Cells[5].ForeColor = Color.Red;
                    e.Row.Cells[6].ForeColor = Color.Red;
                    e.Row.Cells[7].ForeColor = Color.Red;
                    e.Row.Cells[9].ForeColor = Color.Red;
                    e.Row.Cells[10].ForeColor = Color.Red;
                    e.Row.Cells[11].ForeColor = Color.Red;
                    e.Row.Cells[13].ForeColor = Color.Red;
                    e.Row.Cells[14].ForeColor = Color.Red;
                    
                }
            }
        }

        protected void TableEmployeeAgreement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Int32 daytoexpired;
                daytoexpired = Convert.ToInt32(e.Row.Cells[11].Text.ToString());
                if (daytoexpired > 0)
                {
                    e.Row.Cells[11].Text = daytoexpired + " " + "Days before expiry date!";
                }
                else
                {
                    e.Row.Cells[11].Text = "has expired";
                    e.Row.Cells[11].ForeColor = Color.Red;
                    e.Row.Cells[0].ForeColor = Color.Red;
                    e.Row.Cells[1].ForeColor = Color.Red;
                    e.Row.Cells[2].ForeColor = Color.Red;
                    e.Row.Cells[3].ForeColor = Color.Red;
                    e.Row.Cells[4].ForeColor = Color.Red;
                    e.Row.Cells[5].ForeColor = Color.Red;
                    e.Row.Cells[6].ForeColor = Color.Red;
                    e.Row.Cells[7].ForeColor = Color.Red;
                    e.Row.Cells[8].ForeColor = Color.Red;
                    e.Row.Cells[10].ForeColor = Color.Red;
                    e.Row.Cells[9].ForeColor = Color.Red;
                    e.Row.Cells[12].ForeColor = Color.Red;
                    e.Row.Cells[13].ForeColor = Color.Red;
                    e.Row.Cells[15].ForeColor = Color.Red;
                    
                }
            }
        }

        protected void TableEmployeeWillRetired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnViewDetail_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewUserTraining').modal();", true);
            GetListEmployees();

            TableListEmployees.Visible = true;
            ViewTableDetail.Visible = true;
            lblNIK.Value= row.Cells[1].Text.ToString();

            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewParticipantTrainingDetail");
            sqlcomm.Parameters.AddWithValue("@nik_participant", row.Cells[1].Text.ToString());

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableViewUserTraining.DataSource = dtb;
            TableViewUserTraining.DataBind();

            TableViewUserTraining.UseAccessibleHeader = true;
            TableViewUserTraining.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnCloseModalViewDetail_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btnCloseModalViewTraining_Click(object sender, EventArgs e)
        {
            ViewTableDetail.Visible = true;
            TableEmployees.Visible = false;
            TablePersonalInformation.Visible = false;
            TableCompanyAgreement.Visible = false;
            TableEmployeeAgreement.Visible = false;
            TableEmployeeWillRetired.Visible = false;
            TableTrainingActiveTotal.Visible = false;
            TableTrainingParticipant.Visible = true;
            TableParticipantTrainingPass.Visible = false;
            TableParticipantTrainingFail.Visible = false;
            lbNameTable.Text = "List of Training Participant";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewDetail').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailParticipantTraining");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableTrainingParticipant.DataSource = dtb;
            TableTrainingParticipant.DataBind();

            TableTrainingParticipant.UseAccessibleHeader = true;
            TableTrainingParticipant.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnCloseModalDetailUserTraining_Click(object sender, EventArgs e)
        {
            GetListEmployees();
        }

        protected void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "GenerateExcel");
            sqlcomm.Parameters.AddWithValue("@nik_participant", lblNIK.Value);

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            using (DataTable dt = new DataTable())
            {
                //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                //sFileName = sFileName.Replace("/", "");
                sda.Fill(dt);
                string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Data Individual Training");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Data Individual Training" + "_" + _vDate1 + ".xlsx");
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

        protected void btnDownloadCertificate_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hlbCerti.Value = row.Cells[17].Text.ToString();

            if (row.Cells[17].Text.ToString() == "NO")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "CertiEmpty();", true);
            }
            else
            {
                string zipFileName = row.Cells[1].Text + ".zip";
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "fileName=" + zipFileName);
                byte[] buffer = new byte[4096];
                ZipOutputStream zipOutputStream = new ZipOutputStream(Response.OutputStream);
                zipOutputStream.SetLevel(3);
                try
                {
                    DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/UploadDocumentIndividualTraining/Certificate" + "/" + row.Cells[3].Text + "/" + row.Cells[1].Text + "/"));
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

            
        }

        protected void btnGenerateExcelTraining_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "GenerateExcel");
            sqlcomm.Parameters.AddWithValue("@nik_participant", hlbNIKParticipant.Value);

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            using (DataTable dt = new DataTable())
            {
                //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                //sFileName = sFileName.Replace("/", "");
                sda.Fill(dt);
                string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Data Individual Training");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Data Individual Training" + "_" + _vDate1 + ".xlsx");
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

        protected void DetailWillBeExpired_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewUserValidityWillExpired').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailTrainingValidityWillBeExpired");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableWillExpired.DataSource = dtb;
            TableWillExpired.DataBind();

            TableWillExpired.UseAccessibleHeader = true;
            TableWillExpired.HeaderRow.TableSection = TableRowSection.TableHeader;
            Con.Close();

        }

        protected void DetailExpired_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViewUserValidityExpired').modal();", true);
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailTrainingValidityExpired");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableExpired.DataSource = dtb;
            TableExpired.DataBind();

            TableExpired.UseAccessibleHeader = true;
            TableExpired.HeaderRow.TableSection = TableRowSection.TableHeader;
            Con.Close();
        }

        protected void btnGenerateExcelWillExpired_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailTrainingValidityWillBeExpired");

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            using (DataTable dt = new DataTable())
            {
                //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                //sFileName = sFileName.Replace("/", "");
                sda.Fill(dt);
                string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Data Training");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Data Training" + "_" + _vDate1 + ".xlsx");
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

        protected void btnCloseModalExpired_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btnCloseModalWillExpired_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btnGenerateExcelExpired_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Training";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "DetailTrainingValidityExpired");

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            using (DataTable dt = new DataTable())
            {
                //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                //sFileName = sFileName.Replace("/", "");
                sda.Fill(dt);
                string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Data Training");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Data Training" + "_" + _vDate1 + ".xlsx");
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

        //protected void btnCloseDetailTraining_Click(object sender, EventArgs e)
        //{

        //}

        //protected void GetDataNotifCompanyAgreementWillBeExpired()
        //{
        //    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
        //    SqlConnection Con = new SqlConnection(path);
        //    Con.Open();
        //    SqlCommand sqlcomm = new SqlCommand();
        //    sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
        //    sqlcomm.CommandType = CommandType.StoredProcedure;
        //    sqlcomm.Connection = Con;
        //    sqlcomm.Parameters.AddWithValue("@StatementType", "NotifCompanyAgreementWillBeExpired");

        //    DataTable dtb = new DataTable();
        //    SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

        //    sda.Fill(dtb);
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<table header-border>");

        //    foreach (DataRow dr in dtb.Rows)
        //    {
        //        sb.Append("<tr>");
        //        foreach (DataColumn dc in dtb.Columns)
        //        {
        //            sb.Append("<th>");
        //            sb.Append(dr[dc.ColumnName].ToString());
        //            sb.Append("</th>");
        //        }
        //        sb.Append("</tr>");
        //    }
        //    sb.Append("</table>");
        //    PanelNotifCompanyAgreementWillExpired.Controls.Add(new Label { Text = sb.ToString() });
        //    Con.Close();

        //}

        //protected void GetDataNotifEmployeeAgreementWillBeExpired()
        //{
        //    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
        //    SqlConnection Con = new SqlConnection(path);
        //    Con.Open();
        //    SqlCommand sqlcomm = new SqlCommand();
        //    sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
        //    sqlcomm.CommandType = CommandType.StoredProcedure;
        //    sqlcomm.Connection = Con;
        //    sqlcomm.Parameters.AddWithValue("@StatementType", "NotifEmployeeAgreementWillBeExpired");

        //    DataTable dtb = new DataTable();
        //    SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

        //    sda.Fill(dtb);
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<table header-border>");

        //    foreach (DataRow dr in dtb.Rows)
        //    {
        //        sb.Append("<tr>");
        //        foreach (DataColumn dc in dtb.Columns)
        //        {
        //            sb.Append("<th>");
        //            sb.Append(dr[dc.ColumnName].ToString());
        //            sb.Append("</th>");
        //        }
        //        sb.Append("</tr>");
        //    }
        //    sb.Append("</table>");
        //    PanelNotifEmployeeAgreementWillExpired.Controls.Add(new Label { Text = sb.ToString() });
        //    Con.Close();

        //}
    }
}