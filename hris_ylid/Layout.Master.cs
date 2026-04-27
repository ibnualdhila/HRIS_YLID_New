using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Employees"] == null)
            {
                Session.Clear();
                Session.RemoveAll();
                //Response.Redirect("ExpiredPage.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                //Response.Redirect("ExpiredPage.aspx?url=" + Request.Url.AbsolutePath.ToString());
                Response.Redirect("expired_page.aspx?url=" + Request.Url.AbsoluteUri.ToString());

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetDataUser();
            txtFullname.InnerText = Session["Fullname"].ToString();
            //lblLvlUser.Text = Session["GroupName"].ToString();
            lbNIK.Text= Session["nik"].ToString();
            lblOid.Text = Session["Oid"].ToString();
            lblOidEmployees.Text = Session["Employees"].ToString();
            lblDivision.Text = Session["Division"].ToString();
            lblSection.Text = Session["Section"].ToString();
            lblPosition.Text = Session["Position"].ToString();
            lblLocation.Text = Session["Location"].ToString();
            //lblGroupChart.Text = Session["GroupChart"].ToString();

            if (!IsPostBack)
            {
                //String lvluser = lblLvlUser.Text;

                CheckUserNav(Session["Oid"].ToString());
                #region OLD
                //if (lvluser.Contains("Admin"))
                //{

                //    home.Visible = true;
                //    masterdata.Visible = true;
                //    administrator.Visible = true;
                //    registration.Visible = true;
                //    //manageroles.Visible = true;
                //    master_organization.Visible = true;
                //    location.Visible = true;
                //    division.Visible = true;
                //    position.Visible = true;
                //    section.Visible = true;
                //    subsection.Visible = true;
                //    master_hr_general.Visible = true;
                //    employees.Visible = true;
                //    //vendor_general.Visible = true;
                //    master_hr_training.Visible = true;
                //    data_vendor.Visible = true;
                //    data_institution.Visible = true;
                //    data_training.Visible = true;
                //    recruitment.Visible = true;
                //    personal_info.Visible = true;
                //    hr_general.Visible = true;
                //    company_document.Visible = true;
                //    employee_agreement.Visible = true;
                //    //hr_training.Visible = true;
                //    //CheckUserNav(hr_training);

                //    individual_training.Visible = true;
                //    training_record.Visible = true;

                //}
                //else if (lvluser.Contains("HR General"))
                //{
                //    CheckUserNav(Session["Oid"].ToString());
                //    //home.Visible = true;                //1
                //    //masterdata.Visible = true;          //1
                //    //administrator.Visible = false;
                //    //registration.Visible = true;
                //    ////manageroles.Visible = true;
                //    //master_organization.Visible = true;
                //    //location.Visible = true;
                //    //division.Visible = true;
                //    //position.Visible = true;
                //    //section.Visible = true;
                //    //subsection.Visible = true;
                //    //master_hr_general.Visible = true;
                //    //employees.Visible = true;
                //    //recruitment.Visible = true;
                //    //personal_info.Visible = true;
                //    //hr_general.Visible = true;
                //    //company_document.Visible = true;
                //    //employee_agreement.Visible = true;
                //    //data_institution.Visible = true;
                //    ////vendor_general.Visible = true;

                //    ////HR Training Role
                //    //master_hr_training.Visible = false;   //1
                //    //data_vendor.Visible = false;            //1
                //    //data_training.Visible = false;          //1
                //    //hr_training.Visible = false;            //1
                //    //individual_training.Visible = false;    //1
                //    //training_record.Visible = false;        //1

                //}
                //else if (lvluser.Contains("HR Training"))
                //{

                //    CheckUserNav(Session["Oid"].ToString());

                //    //home.Visible = true;
                //    //masterdata.Visible = true;
                //    //master_hr_training.Visible = true;
                //    //data_vendor.Visible = true;
                //    //data_training.Visible = true;
                //    //hr_training.Visible = true;
                //    //individual_training.Visible = true;
                //    //training_record.Visible = true;

                //    ////HR General Role
                //    //administrator.Visible = false;
                //    //registration.Visible = false;
                //    ////manageroles.Visible = false;
                //    //master_organization.Visible = false;
                //    //location.Visible = false;
                //    //division.Visible = false;
                //    //position.Visible = false;
                //    //section.Visible = false;
                //    //subsection.Visible = false;
                //    //master_hr_general.Visible = false;
                //    //employees.Visible = false;
                //    //recruitment.Visible = false;
                //    //personal_info.Visible = false;
                //    //hr_general.Visible = false;
                //    //company_document.Visible = false;
                //    //employee_agreement.Visible = false;
                //    //data_institution.Visible = false;
                //    ////vendor_general.Visible = false;
                //}
                //else if (lvluser.Contains("HR Recruitment"))
                //{
                //    home.Visible = false;
                //    masterdata.Visible = false;
                //    administrator.Visible = false;
                //    registration.Visible = false;
                //    //manageroles.Visible = false;
                //    master_organization.Visible = false;
                //    location.Visible = false;
                //    division.Visible = false;
                //    position.Visible = false;
                //    section.Visible = false;
                //    subsection.Visible = false;
                //    master_hr_general.Visible = false;
                //    employees.Visible = false;
                //    //vendor_general.Visible = false;
                //    master_hr_training.Visible = false;
                //    data_vendor.Visible = false;
                //    data_institution.Visible = false;
                //    data_training.Visible = false;
                //    recruitment.Visible = true;
                //    personal_info.Visible = true;
                //    hr_general.Visible = false;
                //    company_document.Visible = false;
                //    employee_agreement.Visible = false;
                //    hr_training.Visible = false;
                //    individual_training.Visible = false;
                //    training_record.Visible = false;
                //}
                //else
                //{
                //    home.Visible = true;
                //    masterdata.Visible = false;
                //    administrator.Visible = false;
                //    registration.Visible = false;
                //    //manageroles.Visible = false;
                //    master_organization.Visible = false;
                //    location.Visible = false;
                //    division.Visible = false;
                //    position.Visible = false;
                //    section.Visible = false;
                //    subsection.Visible = false;
                //    master_hr_general.Visible = false;
                //    employees.Visible = false;
                //    //vendor_general.Visible = false;
                //    master_hr_training.Visible = false;
                //    data_vendor.Visible = false;
                //    data_institution.Visible = false;
                //    data_training.Visible = false;
                //    recruitment.Visible = false;
                //    personal_info.Visible = false;
                //    hr_general.Visible = false;
                //    company_document.Visible = false;
                //    employee_agreement.Visible = false;
                //    hr_training.Visible = false;
                //    individual_training.Visible = false;
                //    training_record.Visible = false;
                //}
                #endregion
            }
        }

        private void CheckUserNav(string vNIK)
        {
            masterdata.Visible = false;
            administrator.Visible = false;
            registration.Visible = false;
            master_organization.Visible = false;
            location.Visible = false;
            division.Visible = false;
            position.Visible = false;
            section.Visible = false;
            subsection.Visible = false;
            master_hr_general.Visible = false;
            employees.Visible = false;
            recruitment.Visible = false;
            personal_info.Visible = false;
            hr_general.Visible = false;
            company_document.Visible = false;
            employee_agreement.Visible = false;
            data_institution.Visible = false;
            master_hr_training.Visible = false;
            data_vendor.Visible = false;
            data_training.Visible = false;
            hr_training.Visible = false;
            individual_training.Visible = false;
            training_record.Visible = false;
            registration_group.Visible = false;
            registration_module.Visible = false;
            organization_chart_group.Visible = false;
            organization_chart_head.Visible = false;

            string vUserCode = vNIK;

            string vPath = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection vCon = new SqlConnection(vPath);
            SqlCommand vSQLComm = new SqlCommand();
            vSQLComm.Connection = vCon;

            vCon.Open();

            vSQLComm.CommandText = "sp_HRIS_DB_UserControl";
            vSQLComm.CommandType = CommandType.StoredProcedure; 
            vSQLComm.Parameters.AddWithValue("@Code", vUserCode);
            vSQLComm.Parameters.AddWithValue("@Flag", "ModuleNav");

            SqlDataReader vDR = vSQLComm.ExecuteReader();
            if (vDR.HasRows)
            {
                while (vDR.Read())
                {
                    //HtmlGenericControl vModuleName = new HtmlGenericControl(); //Convert. vDR["ModuleName"]; 
                    //vModuleName = vDR["ModuleName"].ToString();
                    //Int16  vRoleNav = Convert.ToInt16(vDR["RoleNavigation"]);
                    //ChangeUserNav(vModuleName, vRoleNav);
                    //ChangeUserNav(hr_training,1);

                    if (vDR["ModuleName"].ToString().Trim() == "masterdata") { masterdata.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "administrator") { administrator.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "registration") { registration.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "master_organization") { master_organization.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "location") { location.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "division") { division.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "position") { position.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "section") { section.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "subsection") { subsection.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "master_hr_general") { master_hr_general.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "employees") { employees.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "recruitment") { recruitment.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "personal_info") { personal_info.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "hr_general") { hr_general.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "company_document") { company_document.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "employee_agreement") { employee_agreement.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "data_institution") { data_institution.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "master_hr_training") { master_hr_training.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "data_vendor") { data_vendor.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "data_training") { data_training.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "hr_training") { hr_training.Visible = Convert.ToBoolean(vDR["RoleNavigation"]) ; }
                    if (vDR["ModuleName"].ToString().Trim() == "individual_training") { individual_training.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "training_record") { training_record.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "registration_group") { registration_group.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "registration_module") { registration_module.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "organization_chart_group") { organization_chart_group.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }
                    if (vDR["ModuleName"].ToString().Trim() == "organization_chart_head") { organization_chart_head.Visible = Convert.ToBoolean(vDR["RoleNavigation"]); }

                }
            }
            //foreach (Row _Row in dr)
        }

        protected void GetDataUser()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckUser");
            sqlcomm.Parameters.AddWithValue("@id", Session["Employees"].ToString());
            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Fullname", (string)dr["Fullname"]);
            }
            else
            {

            }
        }

        protected void Logout(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }
    }
}