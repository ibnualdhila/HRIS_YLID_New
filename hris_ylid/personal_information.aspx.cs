using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace hris_ylid
{
    public partial class personal_information : System.Web.UI.Page
    {
        //string _SMTPPassword = WebConfigurationManager.AppSettings["SMTPPassword"];
        //string _SMTPHost = WebConfigurationManager.AppSettings["SMTPHost"];
        //string _SMTPCredential = WebConfigurationManager.AppSettings["SMTPCredential"];
        //int _SMTPPort = Convert.ToInt16(WebConfigurationManager.AppSettings["SMTPPort"]);
        //string _SMTPServer = WebConfigurationManager.AppSettings["SMTPServer"];

        string _clientId = WebConfigurationManager.AppSettings["clientId"];
        string _clientSecret = WebConfigurationManager.AppSettings["clientSecret"];
        string _tenantId = WebConfigurationManager.AppSettings["tenantId"];
        string itsystem_endpoint = WebConfigurationManager.AppSettings["itsystem_endpoint"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                BindDataTablePersonalInformation();
                BindDataTableLogEmailSend();
            }
        }

        protected void BindDataTablePersonalInformation()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");

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

        protected void BindDataTableLogEmailSend()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Compose_email";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            sqlcomm.Parameters.AddWithValue("@id_ktp", lbKtp.Text);

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableLogEmailSend.DataSource = dtb;
            TableLogEmailSend.DataBind();

            TableLogEmailSend.UseAccessibleHeader = true;
            TableLogEmailSend.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();

        }

        private string CustomString(string _input, int _length)
        {
            if (string.IsNullOrEmpty(_input) || _length <= 0)
                return string.Empty;

            return _input.Length <= _length ? _input.Replace("'", "`") : _input.Replace("'", "`").Substring(0, _length);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string path = string.Concat(Server.MapPath("~/UploadFiles/" + FileUpload.FileName));
            FileUpload.SaveAs(path);

            #region DefineTable
            DataTable dt = new DataTable();
            //dt.Columns.Add("id", typeof(System.Int32));
            dt.Columns.Add("time_stamp", typeof(System.DateTime));
            dt.Columns.Add("candidate_job", typeof(System.String));
            dt.Columns.Add("email", typeof(System.String));
            dt.Columns.Add("id_ktp", typeof(System.String));
            dt.Columns.Add("fullname", typeof(System.String));
            dt.Columns.Add("nickname", typeof(System.String));
            dt.Columns.Add("sex", typeof(System.String));
            dt.Columns.Add("home_address", typeof(System.String));
            dt.Columns.Add("zip_code", typeof(System.String));
            dt.Columns.Add("phone", typeof(System.String));
            dt.Columns.Add("nationality", typeof(System.String));
            dt.Columns.Add("religion", typeof(System.String));
            dt.Columns.Add("place_of_birth", typeof(System.String));
            dt.Columns.Add("date_of_birth", typeof(System.DateTime));
            dt.Columns.Add("marital_status", typeof(System.String));
            dt.Columns.Add("other_id_card", typeof(System.String));
            dt.Columns.Add("photo", typeof(System.String));
            dt.Columns.Add("name_of_partner", typeof(System.String));
            dt.Columns.Add("age_of_partner", typeof(System.String));
            dt.Columns.Add("occupation_of_partner", typeof(System.String));
            dt.Columns.Add("name_children_1", typeof(System.String));
            dt.Columns.Add("sex_children_1", typeof(System.String));
            dt.Columns.Add("age_children_1", typeof(System.String));
            dt.Columns.Add("education_children_1", typeof(System.String));
            dt.Columns.Add("name_children_2", typeof(System.String));
            dt.Columns.Add("sex_children_2", typeof(System.String));
            dt.Columns.Add("age_children_2", typeof(System.String));
            dt.Columns.Add("education_children_2", typeof(System.String));
            dt.Columns.Add("name_children_3", typeof(System.String));
            dt.Columns.Add("sex_children_3", typeof(System.String));
            dt.Columns.Add("age_children_3", typeof(System.String));
            dt.Columns.Add("education_children_3", typeof(System.String));
            dt.Columns.Add("name_children_4", typeof(System.String));
            dt.Columns.Add("sex_children_4", typeof(System.String));
            dt.Columns.Add("age_children_4", typeof(System.String));
            dt.Columns.Add("education_children_4", typeof(System.String));
            dt.Columns.Add("name_children_5", typeof(System.String));
            dt.Columns.Add("sex_children_5", typeof(System.String));
            dt.Columns.Add("age_children_5", typeof(System.String));
            dt.Columns.Add("education_children_5", typeof(System.String));
            dt.Columns.Add("name_father", typeof(System.String));
            dt.Columns.Add("name_mother", typeof(System.String));
            dt.Columns.Add("name_sister_brother_1", typeof(System.String));
            dt.Columns.Add("name_sister_brother_2", typeof(System.String));
            dt.Columns.Add("name_sister_brother_3", typeof(System.String));
            dt.Columns.Add("name_sister_brother_4", typeof(System.String));
            dt.Columns.Add("name_sister_brother_5", typeof(System.String));
            dt.Columns.Add("age_father", typeof(System.String));
            dt.Columns.Add("age_mother", typeof(System.String));
            dt.Columns.Add("age_sister_brother_1", typeof(System.String));
            dt.Columns.Add("age_sister_brother_2", typeof(System.String));
            dt.Columns.Add("age_sister_brother_3", typeof(System.String));
            dt.Columns.Add("age_sister_brother_4", typeof(System.String));
            dt.Columns.Add("age_sister_brother_5", typeof(System.String));
            dt.Columns.Add("education_father", typeof(System.String));
            dt.Columns.Add("education_mother", typeof(System.String));
            dt.Columns.Add("education_sister_brother_1", typeof(System.String));
            dt.Columns.Add("education_sister_brother_2", typeof(System.String));
            dt.Columns.Add("education_sister_brother_3", typeof(System.String));
            dt.Columns.Add("education_sister_brother_4", typeof(System.String));
            dt.Columns.Add("education_sister_brother_5", typeof(System.String));
            dt.Columns.Add("occupation_father", typeof(System.String));
            dt.Columns.Add("occupation_mother", typeof(System.String));
            dt.Columns.Add("occupation_sister_brother_1", typeof(System.String));
            dt.Columns.Add("occupation_sister_brother_2", typeof(System.String));
            dt.Columns.Add("occupation_sister_brother_3", typeof(System.String));
            dt.Columns.Add("occupation_sister_brother_4", typeof(System.String));
            dt.Columns.Add("occupation_sister_brother_5", typeof(System.String));
            dt.Columns.Add("name_elementary_school", typeof(System.String));
            dt.Columns.Add("place_elementary_school", typeof(System.String));
            dt.Columns.Add("period_elementary_school", typeof(System.String));
            dt.Columns.Add("name_junior_high_school", typeof(System.String));
            dt.Columns.Add("place_junior_high_school", typeof(System.String));
            dt.Columns.Add("period_junior_high_school", typeof(System.String));
            dt.Columns.Add("name_senior_high_school", typeof(System.String));
            dt.Columns.Add("place_senior_high_school", typeof(System.String));
            dt.Columns.Add("period_senior_high_school", typeof(System.String));
            dt.Columns.Add("name_diploma_school", typeof(System.String));
            dt.Columns.Add("place_diploma_school", typeof(System.String));
            dt.Columns.Add("period_diploma_school", typeof(System.String));
            dt.Columns.Add("name_bachelor_school", typeof(System.String));
            dt.Columns.Add("place_bachelor_school", typeof(System.String));
            dt.Columns.Add("period_bachelor_school", typeof(System.String));
            dt.Columns.Add("name_master_school", typeof(System.String));
            dt.Columns.Add("place_master_school", typeof(System.String));
            dt.Columns.Add("period_master_school", typeof(System.String));
            dt.Columns.Add("name_doctoral_school", typeof(System.String));
            dt.Columns.Add("place_doctoral_school", typeof(System.String));
            dt.Columns.Add("period_doctoral_school", typeof(System.String));
            dt.Columns.Add("kind_of_course_1", typeof(System.String));
            dt.Columns.Add("name_of_school_1", typeof(System.String));
            dt.Columns.Add("period_of_course_1", typeof(System.String));
            dt.Columns.Add("kind_of_course_2", typeof(System.String));
            dt.Columns.Add("name_of_school_2", typeof(System.String));
            dt.Columns.Add("period_of_course_2", typeof(System.String));
            dt.Columns.Add("kind_of_course_3", typeof(System.String));
            dt.Columns.Add("name_of_school_3", typeof(System.String));
            dt.Columns.Add("period_of_course_3", typeof(System.String));
            dt.Columns.Add("language_english", typeof(System.String));
            dt.Columns.Add("language_mandarin", typeof(System.String));
            dt.Columns.Add("language_japanese", typeof(System.String));
            dt.Columns.Add("language_french", typeof(System.String));
            dt.Columns.Add("language_dutch", typeof(System.String));
            dt.Columns.Add("language_deutsche", typeof(System.String));
            dt.Columns.Add("operate_of_computer", typeof(System.String));
            dt.Columns.Add("controlled_software_1", typeof(System.String));
            dt.Columns.Add("controlled_software_2", typeof(System.String));
            dt.Columns.Add("controlled_software_3", typeof(System.String));
            dt.Columns.Add("controlled_software_4", typeof(System.String));
            dt.Columns.Add("controlled_software_5", typeof(System.String));
            dt.Columns.Add("controlled_software_6", typeof(System.String));
            dt.Columns.Add("controlled_software_7", typeof(System.String));
            dt.Columns.Add("controlled_software_8", typeof(System.String));
            dt.Columns.Add("computer_programming", typeof(System.String));
            dt.Columns.Add("ever_taken_psikotes", typeof(System.String));
            dt.Columns.Add("place_psikotes", typeof(System.String));
            dt.Columns.Add("time_psikotes", typeof(System.String));
            dt.Columns.Add("purpose_psikotes", typeof(System.String));
            dt.Columns.Add("company_job_experience_1", typeof(System.String));
            dt.Columns.Add("period_job_experience_1", typeof(System.String));
            dt.Columns.Add("position_job_experience_1", typeof(System.String));
            dt.Columns.Add("salary_job_experience_1", typeof(System.String));
            dt.Columns.Add("reason_leaving_company_1", typeof(System.String));
            dt.Columns.Add("company_job_experience_2", typeof(System.String));
            dt.Columns.Add("period_job_experience_2", typeof(System.String));
            dt.Columns.Add("position_job_experience_2", typeof(System.String));
            dt.Columns.Add("salary_job_experience_2", typeof(System.String));
            dt.Columns.Add("reason_leaving_company_2", typeof(System.String));
            dt.Columns.Add("company_job_experience_3", typeof(System.String));
            dt.Columns.Add("period_job_experience_3", typeof(System.String));
            dt.Columns.Add("position_job_experience_3", typeof(System.String));
            dt.Columns.Add("salary_job_experience_3", typeof(System.String));
            dt.Columns.Add("reason_leaving_company_3", typeof(System.String));
            dt.Columns.Add("company_job_experience_4", typeof(System.String));
            dt.Columns.Add("period_job_experience_4", typeof(System.String));
            dt.Columns.Add("position_job_experience_4", typeof(System.String));
            dt.Columns.Add("salary_job_experience_4", typeof(System.String));
            dt.Columns.Add("reason_leaving_company_4", typeof(System.String));
            dt.Columns.Add("explain_responsibility_last_position", typeof(System.String));
            dt.Columns.Add("company_organization_structure", typeof(System.String));
            dt.Columns.Add("height", typeof(System.String));
            dt.Columns.Add("weight", typeof(System.String));
            dt.Columns.Add("color_blind", typeof(System.String));
            dt.Columns.Add("spectacles", typeof(System.String));
            dt.Columns.Add("surgery_or_been_hospitalized", typeof(System.String));
            dt.Columns.Add("illnes", typeof(System.String));
            dt.Columns.Add("name_of_hospital", typeof(System.String));
            dt.Columns.Add("money_spend_per_month", typeof(System.String));
            dt.Columns.Add("salary_present_company", typeof(System.String));
            dt.Columns.Add("present_company_fasilities", typeof(System.String));
            dt.Columns.Add("salary_expect", typeof(System.String));
            dt.Columns.Add("present_company_loan", typeof(System.String));
            dt.Columns.Add("total_loan", typeof(System.String));
            dt.Columns.Add("fasilities_expect", typeof(System.String));
            dt.Columns.Add("start_working", typeof(System.DateTime));
            dt.Columns.Add("friend_in_company", typeof(System.String));
            dt.Columns.Add("name_friend_in_company", typeof(System.String));
            dt.Columns.Add("business_trip", typeof(System.String));
            dt.Columns.Add("name_reference_1", typeof(System.String));
            dt.Columns.Add("address_reference_1", typeof(System.String));
            dt.Columns.Add("phone_reference_1", typeof(System.String));
            dt.Columns.Add("occupation_reference_1", typeof(System.String));
            dt.Columns.Add("relation_reference_1", typeof(System.String));
            dt.Columns.Add("name_reference_2", typeof(System.String));
            dt.Columns.Add("address_reference_2", typeof(System.String));
            dt.Columns.Add("phone_reference_2", typeof(System.String));
            dt.Columns.Add("occupation_reference_2", typeof(System.String));
            dt.Columns.Add("relation_reference_2", typeof(System.String));
            dt.Columns.Add("name_reference_3", typeof(System.String));
            dt.Columns.Add("address_reference_3", typeof(System.String));
            dt.Columns.Add("phone_reference_3", typeof(System.String));
            dt.Columns.Add("occupation_reference_3", typeof(System.String));
            dt.Columns.Add("relation_reference_3", typeof(System.String));
            dt.Columns.Add("status_recruitment", typeof(System.String));
            dt.Columns.Add("pic_recruitment", typeof(System.String));
            dt.Columns.Add("status_record", typeof(System.String));
            dt.Columns.Add("name_of_housemate_1", typeof(System.String));
            dt.Columns.Add("address_housemate_1", typeof(System.String));
            dt.Columns.Add("relationship_housemate_1", typeof(System.String));
            dt.Columns.Add("phone_housemate_1", typeof(System.String));
            dt.Columns.Add("name_of_housemate_2", typeof(System.String));
            dt.Columns.Add("address_housemate_2", typeof(System.String));
            dt.Columns.Add("relationship_housemate_2", typeof(System.String));
            dt.Columns.Add("phone_housemate_2", typeof(System.String));

            dt.Columns.Add("HandPhone", typeof(System.String));
            dt.Columns.Add("BloodType", typeof(System.String));
            dt.Columns.Add("PreviousOrganization", typeof(System.String));
            dt.Columns.Add("PositionOnPreviousOrganization", typeof(System.String));
            dt.Columns.Add("SalaryPrevCompanyNettGross", typeof(System.String));
            #endregion

            XLWorkbook _vWBUpload = new XLWorkbook(path);
            foreach (IXLWorksheet _vWorksheet in _vWBUpload.Worksheets)
            {
                int _ExcelRow = 2;

                while (_vWorksheet.Cell(_ExcelRow, 2).Value.ToString().Length > 0)
                {

                    DataRow _dr = dt.NewRow();

                    #region DefineValue

                    //_dr["id"] = Convert.ToInt32(_vWorksheet.Cell(2, 1).Value.ToString());
                    _dr["time_stamp"] = Convert.ToDateTime(_vWorksheet.Cell(_ExcelRow, 2).Value);
                    _dr["candidate_job"] = _vWorksheet.Cell(_ExcelRow, 3).Value.ToString();
                    _dr["email"] = _vWorksheet.Cell(_ExcelRow, 4).Value.ToString();
                    _dr["id_ktp"] = _vWorksheet.Cell(_ExcelRow, 5).Value.ToString();
                    _dr["fullname"] = _vWorksheet.Cell(_ExcelRow, 6).Value.ToString();
                    _dr["nickname"] = _vWorksheet.Cell(_ExcelRow, 7).Value.ToString();
                    _dr["sex"] = _vWorksheet.Cell(_ExcelRow, 8).Value.ToString();
                    _dr["home_address"] = _vWorksheet.Cell(_ExcelRow, 9).Value.ToString();
                    _dr["zip_code"] = _vWorksheet.Cell(_ExcelRow, 10).Value.ToString();
                    //_dr["phone"] = _vWorksheet.Cell(_ExcelRow, 11).Value.ToString();
                    _dr["nationality"] = _vWorksheet.Cell(_ExcelRow, 12).Value.ToString();
                    _dr["religion"] = _vWorksheet.Cell(_ExcelRow, 13).Value.ToString();
                    _dr["place_of_birth"] = _vWorksheet.Cell(_ExcelRow, 14).Value.ToString();
                    _dr["date_of_birth"] = Convert.ToDateTime(_vWorksheet.Cell(_ExcelRow, 15).Value);
                    _dr["marital_status"] = _vWorksheet.Cell(_ExcelRow, 16).Value.ToString();
                    _dr["other_id_card"] = _vWorksheet.Cell(_ExcelRow, 17).Value.ToString();
                    _dr["photo"] = _vWorksheet.Cell(_ExcelRow, 18).Value.ToString();
                    _dr["name_of_partner"] = _vWorksheet.Cell(_ExcelRow, 19).Value.ToString();
                    _dr["age_of_partner"] = _vWorksheet.Cell(_ExcelRow, 20).Value.ToString();
                    _dr["occupation_of_partner"] = _vWorksheet.Cell(_ExcelRow, 21).Value.ToString();
                    _dr["name_children_1"] = _vWorksheet.Cell(_ExcelRow, 22).Value.ToString();
                    _dr["sex_children_1"] = _vWorksheet.Cell(_ExcelRow, 23).Value.ToString();
                    _dr["age_children_1"] = _vWorksheet.Cell(_ExcelRow, 24).Value.ToString();
                    _dr["education_children_1"] = _vWorksheet.Cell(_ExcelRow, 25).Value.ToString();
                    _dr["name_children_2"] = _vWorksheet.Cell(_ExcelRow, 26).Value.ToString();
                    _dr["sex_children_2"] = _vWorksheet.Cell(_ExcelRow, 27).Value.ToString();
                    _dr["age_children_2"] = _vWorksheet.Cell(_ExcelRow, 28).Value.ToString();
                    _dr["education_children_2"] = _vWorksheet.Cell(_ExcelRow, 29).Value.ToString();
                    _dr["name_children_3"] = _vWorksheet.Cell(_ExcelRow, 30).Value.ToString();
                    _dr["sex_children_3"] = _vWorksheet.Cell(_ExcelRow, 31).Value.ToString();
                    _dr["age_children_3"] = _vWorksheet.Cell(_ExcelRow, 32).Value.ToString();
                    _dr["education_children_3"] = _vWorksheet.Cell(_ExcelRow, 33).Value.ToString();
                    _dr["name_children_4"] = _vWorksheet.Cell(_ExcelRow, 34).Value.ToString();
                    _dr["sex_children_4"] = _vWorksheet.Cell(_ExcelRow, 35).Value.ToString();
                    _dr["age_children_4"] = _vWorksheet.Cell(_ExcelRow, 36).Value.ToString();
                    _dr["education_children_4"] = _vWorksheet.Cell(_ExcelRow, 37).Value.ToString();
                    _dr["name_children_5"] = _vWorksheet.Cell(_ExcelRow, 38).Value.ToString();
                    _dr["sex_children_5"] = _vWorksheet.Cell(_ExcelRow, 39).Value.ToString();
                    _dr["age_children_5"] = _vWorksheet.Cell(_ExcelRow, 40).Value.ToString();
                    _dr["education_children_5"] = _vWorksheet.Cell(_ExcelRow, 41).Value.ToString();
                    _dr["name_father"] = _vWorksheet.Cell(_ExcelRow, 42).Value.ToString();
                    _dr["name_mother"] = _vWorksheet.Cell(_ExcelRow, 43).Value.ToString();
                    _dr["name_sister_brother_1"] = _vWorksheet.Cell(_ExcelRow, 44).Value.ToString();
                    _dr["name_sister_brother_2"] = _vWorksheet.Cell(_ExcelRow, 45).Value.ToString();
                    _dr["name_sister_brother_3"] = _vWorksheet.Cell(_ExcelRow, 46).Value.ToString();
                    _dr["name_sister_brother_4"] = _vWorksheet.Cell(_ExcelRow, 47).Value.ToString();
                    _dr["name_sister_brother_5"] = _vWorksheet.Cell(_ExcelRow, 48).Value.ToString();
                    _dr["age_father"] = _vWorksheet.Cell(_ExcelRow, 49).Value.ToString();
                    _dr["age_mother"] = _vWorksheet.Cell(_ExcelRow, 50).Value.ToString();
                    _dr["age_sister_brother_1"] = _vWorksheet.Cell(_ExcelRow, 51).Value.ToString();
                    _dr["age_sister_brother_2"] = _vWorksheet.Cell(_ExcelRow, 52).Value.ToString();
                    _dr["age_sister_brother_3"] = _vWorksheet.Cell(_ExcelRow, 53).Value.ToString();
                    _dr["age_sister_brother_4"] = _vWorksheet.Cell(_ExcelRow, 54).Value.ToString();
                    _dr["age_sister_brother_5"] = _vWorksheet.Cell(_ExcelRow, 55).Value.ToString();
                    _dr["education_father"] = _vWorksheet.Cell(_ExcelRow, 56).Value.ToString();
                    _dr["education_mother"] = _vWorksheet.Cell(_ExcelRow, 57).Value.ToString();
                    _dr["education_sister_brother_1"] = _vWorksheet.Cell(_ExcelRow, 58).Value.ToString();
                    _dr["education_sister_brother_2"] = _vWorksheet.Cell(_ExcelRow, 59).Value.ToString();
                    _dr["education_sister_brother_3"] = _vWorksheet.Cell(_ExcelRow, 60).Value.ToString();
                    _dr["education_sister_brother_4"] = _vWorksheet.Cell(_ExcelRow, 61).Value.ToString();
                    _dr["education_sister_brother_5"] = _vWorksheet.Cell(_ExcelRow, 62).Value.ToString();
                    _dr["occupation_father"] = _vWorksheet.Cell(_ExcelRow, 63).Value.ToString();
                    _dr["occupation_mother"] = _vWorksheet.Cell(_ExcelRow, 64).Value.ToString();
                    _dr["occupation_sister_brother_1"] = _vWorksheet.Cell(_ExcelRow, 65).Value.ToString();
                    _dr["occupation_sister_brother_2"] = _vWorksheet.Cell(_ExcelRow, 66).Value.ToString();
                    _dr["occupation_sister_brother_3"] = _vWorksheet.Cell(_ExcelRow, 67).Value.ToString();
                    _dr["occupation_sister_brother_4"] = _vWorksheet.Cell(_ExcelRow, 68).Value.ToString();
                    _dr["occupation_sister_brother_5"] = _vWorksheet.Cell(_ExcelRow, 69).Value.ToString();
                    _dr["name_elementary_school"] = _vWorksheet.Cell(_ExcelRow, 70).Value.ToString();
                    _dr["place_elementary_school"] = _vWorksheet.Cell(_ExcelRow, 71).Value.ToString();
                    _dr["period_elementary_school"] = _vWorksheet.Cell(_ExcelRow, 72).Value.ToString();
                    _dr["name_junior_high_school"] = _vWorksheet.Cell(_ExcelRow, 73).Value.ToString();
                    _dr["place_junior_high_school"] = _vWorksheet.Cell(_ExcelRow, 74).Value.ToString();
                    _dr["period_junior_high_school"] = _vWorksheet.Cell(_ExcelRow, 75).Value.ToString();
                    _dr["name_senior_high_school"] = _vWorksheet.Cell(_ExcelRow, 76).Value.ToString();
                    _dr["place_senior_high_school"] = _vWorksheet.Cell(_ExcelRow, 77).Value.ToString();
                    _dr["period_senior_high_school"] = _vWorksheet.Cell(_ExcelRow, 78).Value.ToString();
                    _dr["name_diploma_school"] = _vWorksheet.Cell(_ExcelRow, 79).Value.ToString();
                    _dr["place_diploma_school"] = _vWorksheet.Cell(_ExcelRow, 80).Value.ToString();
                    _dr["period_diploma_school"] = _vWorksheet.Cell(_ExcelRow, 81).Value.ToString();
                    _dr["name_bachelor_school"] = _vWorksheet.Cell(_ExcelRow, 82).Value.ToString();
                    _dr["place_bachelor_school"] = _vWorksheet.Cell(_ExcelRow, 83).Value.ToString();
                    _dr["period_bachelor_school"] = _vWorksheet.Cell(_ExcelRow, 84).Value.ToString();
                    _dr["name_master_school"] = _vWorksheet.Cell(_ExcelRow, 85).Value.ToString();
                    _dr["place_master_school"] = _vWorksheet.Cell(_ExcelRow, 86).Value.ToString();
                    _dr["period_master_school"] = _vWorksheet.Cell(_ExcelRow, 87).Value.ToString();
                    _dr["name_doctoral_school"] = _vWorksheet.Cell(_ExcelRow, 88).Value.ToString();
                    _dr["place_doctoral_school"] = _vWorksheet.Cell(_ExcelRow, 89).Value.ToString();
                    _dr["period_doctoral_school"] = _vWorksheet.Cell(_ExcelRow, 90).Value.ToString();
                    _dr["kind_of_course_1"] = _vWorksheet.Cell(_ExcelRow, 91).Value.ToString();
                    _dr["name_of_school_1"] = _vWorksheet.Cell(_ExcelRow, 92).Value.ToString();
                    _dr["period_of_course_1"] = _vWorksheet.Cell(_ExcelRow, 93).Value.ToString();
                    _dr["kind_of_course_2"] = _vWorksheet.Cell(_ExcelRow, 94).Value.ToString();
                    _dr["name_of_school_2"] = _vWorksheet.Cell(_ExcelRow, 95).Value.ToString();
                    _dr["period_of_course_2"] = _vWorksheet.Cell(_ExcelRow, 96).Value.ToString();
                    _dr["kind_of_course_3"] = _vWorksheet.Cell(_ExcelRow, 97).Value.ToString();
                    _dr["name_of_school_3"] = _vWorksheet.Cell(_ExcelRow, 98).Value.ToString();
                    _dr["period_of_course_3"] = _vWorksheet.Cell(_ExcelRow, 99).Value.ToString();
                    _dr["language_english"] = _vWorksheet.Cell(_ExcelRow, 100).Value.ToString();
                    _dr["language_mandarin"] = _vWorksheet.Cell(_ExcelRow, 101).Value.ToString();
                    _dr["language_japanese"] = _vWorksheet.Cell(_ExcelRow, 102).Value.ToString();
                    _dr["language_french"] = _vWorksheet.Cell(_ExcelRow, 103).Value.ToString();
                    _dr["language_dutch"] = _vWorksheet.Cell(_ExcelRow, 104).Value.ToString();
                    _dr["language_deutsche"] = _vWorksheet.Cell(_ExcelRow, 105).Value.ToString();
                    _dr["operate_of_computer"] = _vWorksheet.Cell(_ExcelRow, 106).Value.ToString();
                    _dr["controlled_software_1"] = _vWorksheet.Cell(_ExcelRow, 107).Value.ToString();
                    _dr["controlled_software_2"] = _vWorksheet.Cell(_ExcelRow, 108).Value.ToString();
                    _dr["controlled_software_3"] = _vWorksheet.Cell(_ExcelRow, 109).Value.ToString();
                    _dr["controlled_software_4"] = _vWorksheet.Cell(_ExcelRow, 110).Value.ToString();
                    _dr["controlled_software_5"] = _vWorksheet.Cell(_ExcelRow, 111).Value.ToString();
                    _dr["controlled_software_6"] = _vWorksheet.Cell(_ExcelRow, 112).Value.ToString();
                    _dr["controlled_software_7"] = _vWorksheet.Cell(_ExcelRow, 113).Value.ToString();
                    _dr["controlled_software_8"] = _vWorksheet.Cell(_ExcelRow, 114).Value.ToString();
                    _dr["computer_programming"] = _vWorksheet.Cell(_ExcelRow, 115).Value.ToString();
                    _dr["ever_taken_psikotes"] = _vWorksheet.Cell(_ExcelRow, 116).Value.ToString();
                    _dr["place_psikotes"] = _vWorksheet.Cell(_ExcelRow, 117).Value.ToString();
                    _dr["time_psikotes"] = _vWorksheet.Cell(_ExcelRow, 118).Value.ToString();
                    _dr["purpose_psikotes"] = _vWorksheet.Cell(_ExcelRow, 119).Value.ToString();
                    _dr["company_job_experience_1"] = _vWorksheet.Cell(_ExcelRow, 120).Value.ToString();
                    _dr["period_job_experience_1"] = _vWorksheet.Cell(_ExcelRow, 121).Value.ToString();
                    _dr["position_job_experience_1"] = _vWorksheet.Cell(_ExcelRow, 122).Value.ToString();
                    _dr["salary_job_experience_1"] = _vWorksheet.Cell(_ExcelRow, 123).Value.ToString();
                    _dr["reason_leaving_company_1"] = _vWorksheet.Cell(_ExcelRow, 124).Value.ToString();
                    _dr["company_job_experience_2"] = _vWorksheet.Cell(_ExcelRow, 125).Value.ToString();
                    _dr["period_job_experience_2"] = _vWorksheet.Cell(_ExcelRow, 126).Value.ToString();
                    _dr["position_job_experience_2"] = _vWorksheet.Cell(_ExcelRow, 127).Value.ToString();
                    _dr["salary_job_experience_2"] = _vWorksheet.Cell(_ExcelRow, 128).Value.ToString();
                    _dr["reason_leaving_company_2"] = _vWorksheet.Cell(_ExcelRow, 129).Value.ToString();
                    _dr["company_job_experience_3"] = _vWorksheet.Cell(_ExcelRow, 130).Value.ToString();
                    _dr["period_job_experience_3"] = _vWorksheet.Cell(_ExcelRow, 131).Value.ToString();
                    _dr["position_job_experience_3"] = _vWorksheet.Cell(_ExcelRow, 132).Value.ToString();
                    _dr["salary_job_experience_3"] = _vWorksheet.Cell(_ExcelRow, 133).Value.ToString();
                    _dr["reason_leaving_company_3"] = _vWorksheet.Cell(_ExcelRow, 134).Value.ToString();
                    _dr["company_job_experience_4"] = _vWorksheet.Cell(_ExcelRow, 135).Value.ToString();
                    _dr["period_job_experience_4"] = _vWorksheet.Cell(_ExcelRow, 136).Value.ToString();
                    _dr["position_job_experience_4"] = _vWorksheet.Cell(_ExcelRow, 137).Value.ToString();
                    _dr["salary_job_experience_4"] = _vWorksheet.Cell(_ExcelRow, 138).Value.ToString();
                    _dr["reason_leaving_company_4"] = _vWorksheet.Cell(_ExcelRow, 139).Value.ToString();
                    _dr["explain_responsibility_last_position"] = _vWorksheet.Cell(_ExcelRow, 140).Value.ToString();
                    _dr["company_organization_structure"] = _vWorksheet.Cell(_ExcelRow, 141).Value.ToString();
                    _dr["height"] = _vWorksheet.Cell(_ExcelRow, 142).Value.ToString();
                    _dr["weight"] = _vWorksheet.Cell(_ExcelRow, 143).Value.ToString();
                    _dr["color_blind"] = _vWorksheet.Cell(_ExcelRow, 144).Value.ToString();
                    _dr["spectacles"] = _vWorksheet.Cell(_ExcelRow, 145).Value.ToString();
                    _dr["surgery_or_been_hospitalized"] = _vWorksheet.Cell(_ExcelRow, 146).Value.ToString();
                    _dr["illnes"] = _vWorksheet.Cell(_ExcelRow, 147).Value.ToString();
                    _dr["name_of_hospital"] = _vWorksheet.Cell(_ExcelRow, 148).Value.ToString();
                    _dr["money_spend_per_month"] = _vWorksheet.Cell(_ExcelRow, 149).Value.ToString();
                    _dr["salary_present_company"] = _vWorksheet.Cell(_ExcelRow, 150).Value.ToString();
                    _dr["present_company_fasilities"] = _vWorksheet.Cell(_ExcelRow, 151).Value.ToString();
                    _dr["total_loan"] = _vWorksheet.Cell(_ExcelRow, 152).Value.ToString();
                    _dr["present_company_loan"] = _vWorksheet.Cell(_ExcelRow, 153).Value.ToString();
                    _dr["salary_expect"] = _vWorksheet.Cell(_ExcelRow, 154).Value.ToString();
                    _dr["fasilities_expect"] = _vWorksheet.Cell(_ExcelRow, 155).Value.ToString();
                    _dr["start_working"] = Convert.ToDateTime(_vWorksheet.Cell(_ExcelRow, 156).Value);
                    _dr["friend_in_company"] = _vWorksheet.Cell(_ExcelRow, 157).Value.ToString();
                    _dr["name_friend_in_company"] = _vWorksheet.Cell(_ExcelRow, 158).Value.ToString();
                    _dr["business_trip"] = _vWorksheet.Cell(_ExcelRow, 159).Value.ToString();
                    _dr["name_reference_1"] = _vWorksheet.Cell(_ExcelRow, 160).Value.ToString();
                    _dr["address_reference_1"] = _vWorksheet.Cell(_ExcelRow, 161).Value.ToString();
                    _dr["phone_reference_1"] = _vWorksheet.Cell(_ExcelRow, 162).Value.ToString();
                    _dr["occupation_reference_1"] = _vWorksheet.Cell(_ExcelRow, 163).Value.ToString();
                    _dr["relation_reference_1"] = _vWorksheet.Cell(_ExcelRow, 164).Value.ToString();
                    _dr["name_reference_2"] = _vWorksheet.Cell(_ExcelRow, 165).Value.ToString();
                    _dr["address_reference_2"] = _vWorksheet.Cell(_ExcelRow, 166).Value.ToString();
                    _dr["phone_reference_2"] = _vWorksheet.Cell(_ExcelRow, 167).Value.ToString();
                    _dr["occupation_reference_2"] = _vWorksheet.Cell(_ExcelRow, 168).Value.ToString();
                    _dr["relation_reference_2"] = _vWorksheet.Cell(_ExcelRow, 169).Value.ToString();
                    _dr["name_reference_3"] = _vWorksheet.Cell(_ExcelRow, 170).Value.ToString();
                    _dr["address_reference_3"] = _vWorksheet.Cell(_ExcelRow, 171).Value.ToString();
                    _dr["phone_reference_3"] = _vWorksheet.Cell(_ExcelRow, 172).Value.ToString();
                    _dr["occupation_reference_3"] = _vWorksheet.Cell(_ExcelRow, 173).Value.ToString();
                    _dr["relation_reference_3"] = _vWorksheet.Cell(_ExcelRow, 174).Value.ToString();
                    _dr["status_recruitment"] = _vWorksheet.Cell(_ExcelRow, 175).Value.ToString();
                    _dr["pic_recruitment"] = _vWorksheet.Cell(_ExcelRow, 176).Value.ToString();
                    _dr["status_record"] = _vWorksheet.Cell(_ExcelRow, 177).Value.ToString();
                    _dr["name_of_housemate_1"] = _vWorksheet.Cell(_ExcelRow, 178).Value.ToString();
                    _dr["address_housemate_1"] = _vWorksheet.Cell(_ExcelRow, 179).Value.ToString();
                    _dr["relationship_housemate_1"] = _vWorksheet.Cell(_ExcelRow, 180).Value.ToString();
                    _dr["phone_housemate_1"] = _vWorksheet.Cell(_ExcelRow, 181).Value.ToString();
                    _dr["name_of_housemate_2"] = _vWorksheet.Cell(_ExcelRow, 182).Value.ToString();
                    _dr["address_housemate_2"] = _vWorksheet.Cell(_ExcelRow, 183).Value.ToString();
                    _dr["relationship_housemate_2"] = _vWorksheet.Cell(_ExcelRow, 184).Value.ToString();
                    _dr["phone_housemate_2"] = _vWorksheet.Cell(_ExcelRow, 185).Value.ToString();

                    _dr["HandPhone"] = _vWorksheet.Cell(_ExcelRow, 11).Value.ToString();
                    _dr["BloodType"] = _vWorksheet.Cell(_ExcelRow, 186).Value.ToString();
                    _dr["SalaryPrevCompanyNettGross"] = _vWorksheet.Cell(_ExcelRow, 187).Value.ToString();
                    _dr["PreviousOrganization"] = _vWorksheet.Cell(_ExcelRow, 188).Value.ToString();
                    _dr["PositionOnPreviousOrganization"] = _vWorksheet.Cell(_ExcelRow, 189).Value.ToString();
                    

                    #endregion

                    dt.Rows.Add(_dr);

                    _ExcelRow = _ExcelRow + 1;
                }

                string pathdb = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(pathdb);
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;

                Con.Open();

                foreach (DataRow _dr1 in dt.Rows)
                {
                    #region ParametersValue
                    sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                    sqlcomm.Parameters.AddWithValue("@time_stamp", _dr1["time_stamp"]);
                    sqlcomm.Parameters.AddWithValue("@candidate_job", CustomString(_dr1["candidate_job"].ToString(), 200)); //_dr1["candidate_job"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@email", CustomString(_dr1["email"].ToString(), 50)); //_dr1["email"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@id_ktp", CustomString(_dr1["id_ktp"].ToString(), 50)); // _dr1["id_ktp"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@fullname", CustomString(_dr1["fullname"].ToString(), 50)); // _dr1["fullname"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@nickname", CustomString(_dr1["nickname"].ToString(), 50)); //  _dr1["nickname"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@sex", CustomString(_dr1["sex"].ToString(), 50)); //  _dr1["sex"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@home_address", _dr1["home_address"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@zip_code", CustomString(_dr1["zip_code"].ToString(), 50)); //  _dr1["zip_code"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@phone", CustomString(_dr1["phone"].ToString(), 50)); //  _dr1["phone"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@nationality", CustomString(_dr1["nationality"].ToString(), 50)); //  _dr1["nationality"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@religion", CustomString(_dr1["religion"].ToString(), 50)); //  _dr1["religion"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@place_of_birth", CustomString(_dr1["place_of_birth"].ToString(), 50)); //  _dr1["place_of_birth"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@date_of_birth", _dr1["date_of_birth"]);
                    sqlcomm.Parameters.AddWithValue("@marital_status", CustomString(_dr1["marital_status"].ToString(), 50)); //  _dr1["marital_status"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@other_id_card", CustomString(_dr1["other_id_card"].ToString(), 50)); //  _dr1["other_id_card"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@photo", _dr1["photo"]);

                    sqlcomm.Parameters.AddWithValue("@name_of_partner", CustomString(_dr1["name_of_partner"].ToString(), 50)); //   _dr1["name_of_partner"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_of_partner", CustomString(_dr1["age_of_partner"].ToString(), 50)); //   _dr1["age_of_partner"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_of_partner", CustomString(_dr1["occupation_of_partner"].ToString(), 50)); //   _dr1["occupation_of_partner"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_children_1", CustomString(_dr1["name_children_1"].ToString(), 50)); //   _dr1["name_children_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@sex_children_1", CustomString(_dr1["sex_children_1"].ToString(), 50)); //   _dr1["sex_children_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_children_1", CustomString(_dr1["age_children_1"].ToString(), 50)); //   _dr1["age_children_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_children_1", CustomString(_dr1["education_children_1"].ToString(), 50)); //   _dr1["education_children_1"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_children_2", CustomString(_dr1["name_children_2"].ToString(), 50)); //   _dr1["name_children_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@sex_children_2", CustomString(_dr1["sex_children_2"].ToString(), 50)); //   _dr1["sex_children_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_children_2", CustomString(_dr1["age_children_2"].ToString(), 50)); //   _dr1["age_children_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_children_2", CustomString(_dr1["education_children_2"].ToString(), 50)); //   _dr1["education_children_2"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_children_3", CustomString(_dr1["name_children_3"].ToString(), 50)); //   _dr1["name_children_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@sex_children_3", CustomString(_dr1["sex_children_3"].ToString(), 50)); //   _dr1["sex_children_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_children_3", CustomString(_dr1["age_children_3"].ToString(), 50)); //   _dr1["age_children_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_children_3", CustomString(_dr1["education_children_3"].ToString(), 50)); //   _dr1["education_children_3"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_children_4", CustomString(_dr1["name_children_4"].ToString(), 50)); //   _dr1["name_children_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@sex_children_4", CustomString(_dr1["sex_children_4"].ToString(), 50)); //   _dr1["sex_children_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_children_4", CustomString(_dr1["age_children_4"].ToString(), 50)); //   _dr1["age_children_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_children_4", CustomString(_dr1["education_children_4"].ToString(), 50)); //   _dr1["education_children_4"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_children_5", CustomString(_dr1["name_children_5"].ToString(), 50)); //   _dr1["name_children_5"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@sex_children_5", CustomString(_dr1["sex_children_5"].ToString(), 50)); //   _dr1["sex_children_5"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_children_5", CustomString(_dr1["age_children_5"].ToString(), 50)); //   _dr1["age_children_5"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_children_5", CustomString(_dr1["education_children_5"].ToString(), 50)); //   _dr1["education_children_5"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_father", CustomString(_dr1["name_father"].ToString(), 50)); // _dr1["name_father"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@name_mother", CustomString(_dr1["name_mother"].ToString(), 50)); // _dr1["name_mother"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@name_sister_brother_1", CustomString(_dr1["name_sister_brother_1"].ToString(), 50)); // _dr1["name_sister_brother_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@name_sister_brother_2", CustomString(_dr1["name_sister_brother_2"].ToString(), 50)); // _dr1["name_sister_brother_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@name_sister_brother_3", CustomString(_dr1["name_sister_brother_3"].ToString(), 50)); // _dr1["name_sister_brother_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@name_sister_brother_4", CustomString(_dr1["name_sister_brother_4"].ToString(), 50)); // _dr1["name_sister_brother_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@name_sister_brother_5", CustomString(_dr1["name_sister_brother_5"].ToString(), 50)); // _dr1["name_sister_brother_5"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_father", CustomString(_dr1["age_father"].ToString(), 50)); // _dr1["age_father"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_mother", CustomString(_dr1["age_mother"].ToString(), 50)); // _dr1["age_mother"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_sister_brother_1", CustomString(_dr1["age_sister_brother_1"].ToString(), 50)); // _dr1["age_sister_brother_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_sister_brother_2", CustomString(_dr1["age_sister_brother_2"].ToString(), 50)); // _dr1["age_sister_brother_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_sister_brother_3", CustomString(_dr1["age_sister_brother_3"].ToString(), 50)); // _dr1["age_sister_brother_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_sister_brother_4", CustomString(_dr1["age_sister_brother_4"].ToString(), 50)); // _dr1["age_sister_brother_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@age_sister_brother_5", CustomString(_dr1["age_sister_brother_5"].ToString(), 50)); // _dr1["age_sister_brother_5"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_father", CustomString(_dr1["education_father"].ToString(), 50)); //_dr1["education_father"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_mother", CustomString(_dr1["education_mother"].ToString(), 50)); // _dr1["education_mother"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_sister_brother_1", CustomString(_dr1["education_sister_brother_1"].ToString(), 50)); // _dr1["education_sister_brother_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_sister_brother_2", CustomString(_dr1["education_sister_brother_2"].ToString(), 50)); // _dr1["education_sister_brother_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_sister_brother_3", CustomString(_dr1["education_sister_brother_3"].ToString(), 50)); // _dr1["education_sister_brother_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_sister_brother_4", CustomString(_dr1["education_sister_brother_4"].ToString(), 50)); // _dr1["education_sister_brother_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@education_sister_brother_5", CustomString(_dr1["education_sister_brother_5"].ToString(), 50)); // _dr1["education_sister_brother_5"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_father", CustomString(_dr1["occupation_father"].ToString(), 50)); // _dr1["occupation_father"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_mother", CustomString(_dr1["occupation_mother"].ToString(), 50)); // _dr1["occupation_mother"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_sister_brother_1", CustomString(_dr1["occupation_sister_brother_1"].ToString(), 50)); // _dr1["occupation_sister_brother_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_sister_brother_2", CustomString(_dr1["occupation_sister_brother_2"].ToString(), 50)); // _dr1["occupation_sister_brother_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_sister_brother_3", CustomString(_dr1["occupation_sister_brother_3"].ToString(), 50)); // _dr1["occupation_sister_brother_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_sister_brother_4", CustomString(_dr1["occupation_sister_brother_4"].ToString(), 50)); // _dr1["occupation_sister_brother_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_sister_brother_5", CustomString(_dr1["occupation_sister_brother_5"].ToString(), 50)); // _dr1["occupation_sister_brother_5"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_elementary_school", CustomString(_dr1["name_elementary_school"].ToString(), 200)); // _dr1["name_elementary_school"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@place_elementary_school", _dr1["place_elementary_school"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@period_elementary_school", CustomString(_dr1["period_elementary_school"].ToString(), 50)); // _dr1["period_elementary_school"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_junior_high_school", CustomString(_dr1["name_junior_high_school"].ToString(), 200)); // _dr1["name_junior_high_school"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@place_junior_high_school", _dr1["place_junior_high_school"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@period_junior_high_school", CustomString(_dr1["period_junior_high_school"].ToString(), 50)); // _dr1["period_junior_high_school"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_senior_high_school", CustomString(_dr1["name_senior_high_school"].ToString(), 200)); // _dr1["name_senior_high_school"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@place_senior_high_school", _dr1["place_senior_high_school"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@period_senior_high_school", CustomString(_dr1["period_senior_high_school"].ToString(), 50)); // _dr1["period_senior_high_school"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_diploma_school", CustomString(_dr1["name_diploma_school"].ToString(), 200)); // _dr1["name_diploma_school"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@place_diploma_school", _dr1["place_diploma_school"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@period_diploma_school", CustomString(_dr1["period_diploma_school"].ToString(), 50)); // _dr1["period_diploma_school"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_bachelor_school", CustomString(_dr1["name_bachelor_school"].ToString(), 200)); // _dr1["name_bachelor_school"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@place_bachelor_school", _dr1["place_bachelor_school"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@period_bachelor_school", CustomString(_dr1["period_bachelor_school"].ToString(), 50)); // _dr1["period_bachelor_school"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_master_school", CustomString(_dr1["name_master_school"].ToString(), 200)); // _dr1["name_master_school"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@place_master_school", _dr1["place_master_school"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@period_master_school", CustomString(_dr1["period_master_school"].ToString(), 50)); // _dr1["period_master_school"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_doctoral_school", CustomString(_dr1["name_doctoral_school"].ToString(), 200)); // _dr1["name_doctoral_school"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@place_doctoral_school", _dr1["place_doctoral_school"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@period_doctoral_school", CustomString(_dr1["period_doctoral_school"].ToString(), 50)); // _dr1["period_doctoral_school"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@kind_of_course_1", CustomString(_dr1["kind_of_course_1"].ToString(), 100)); //_dr1["kind_of_course_1"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@name_of_school_1", CustomString(_dr1["name_of_school_1"].ToString(), 100)); //_dr1["name_of_school_1"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@period_of_course_1", CustomString(_dr1["period_of_course_1"].ToString(), 50)); //_dr1["period_of_course_1"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@kind_of_course_2", CustomString(_dr1["kind_of_course_2"].ToString(), 100)); //_dr1["kind_of_course_2"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@name_of_school_2", CustomString(_dr1["name_of_school_2"].ToString(), 100)); //_dr1["name_of_school_2"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@period_of_course_2", CustomString(_dr1["period_of_course_2"].ToString(), 50)); //_dr1["period_of_course_2"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@kind_of_course_3", CustomString(_dr1["kind_of_course_3"].ToString(), 100)); //_dr1["kind_of_course_3"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@name_of_school_3", CustomString(_dr1["name_of_school_3"].ToString(), 100)); //_dr1["name_of_school_3"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@period_of_course_3", CustomString(_dr1["period_of_course_3"].ToString(), 50)); //_dr1["period_of_course_3"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@language_english", CustomString(_dr1["language_english"].ToString(), 50)); //_dr1["language_english"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@language_mandarin", CustomString(_dr1["language_mandarin"].ToString(), 50)); //_dr1["language_mandarin"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@language_japanese", CustomString(_dr1["language_japanese"].ToString(), 50)); //_dr1["language_japanese"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@language_french", CustomString(_dr1["language_french"].ToString(), 50)); //_dr1["language_french"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@language_dutch", CustomString(_dr1["language_dutch"].ToString(), 50)); //_dr1["language_dutch"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@language_deutsche", CustomString(_dr1["language_deutsche"].ToString(), 50)); //_dr1["language_deutsche"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@operate_of_computer", CustomString(_dr1["operate_of_computer"].ToString(), 50)); //_dr1["operate_of_computer"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@controlled_software_1", CustomString(_dr1["controlled_software_1"].ToString(), 100)); // _dr1["controlled_software_1"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@controlled_software_2", CustomString(_dr1["controlled_software_2"].ToString(), 100)); // _dr1["controlled_software_2"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@controlled_software_3", CustomString(_dr1["controlled_software_3"].ToString(), 100)); // _dr1["controlled_software_3"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@controlled_software_4", CustomString(_dr1["controlled_software_4"].ToString(), 100)); // _dr1["controlled_software_4"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@controlled_software_5", CustomString(_dr1["controlled_software_5"].ToString(), 100)); // _dr1["controlled_software_5"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@controlled_software_6", CustomString(_dr1["controlled_software_6"].ToString(), 100)); // _dr1["controlled_software_6"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@controlled_software_7", CustomString(_dr1["controlled_software_7"].ToString(), 100)); // _dr1["controlled_software_7"].ToString().Replace("'", "`").Substring(0, 100));
                    sqlcomm.Parameters.AddWithValue("@controlled_software_8", CustomString(_dr1["controlled_software_8"].ToString(), 100)); // _dr1["controlled_software_8"].ToString().Replace("'", "`").Substring(0, 100));

                    sqlcomm.Parameters.AddWithValue("@computer_programming", CustomString(_dr1["computer_programming"].ToString(), 50)); //  _dr1["computer_programming"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@ever_taken_psikotes", CustomString(_dr1["ever_taken_psikotes"].ToString(), 50)); //  _dr1["ever_taken_psikotes"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@place_psikotes", CustomString(_dr1["place_psikotes"].ToString(), 200)); //  _dr1["place_psikotes"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@time_psikotes", CustomString(_dr1["time_psikotes"].ToString(), 50)); //  _dr1["time_psikotes"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@purpose_psikotes", _dr1["purpose_psikotes"].ToString().Replace("'", "`"));

                    sqlcomm.Parameters.AddWithValue("@company_job_experience_1", CustomString(_dr1["company_job_experience_1"].ToString(), 200)); // _dr1["company_job_experience_1"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@period_job_experience_1", CustomString(_dr1["period_job_experience_1"].ToString(), 50)); // _dr1["period_job_experience_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@position_job_experience_1", CustomString(_dr1["position_job_experience_1"].ToString(), 50)); // _dr1["position_job_experience_1"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@salary_job_experience_1", CustomString(_dr1["salary_job_experience_1"].ToString(), 250)); // _dr1["salary_job_experience_1"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@reason_leaving_company_1",_dr1["reason_leaving_company_1"].ToString().Replace("'", "`"));

                    sqlcomm.Parameters.AddWithValue("@company_job_experience_2", CustomString(_dr1["company_job_experience_2"].ToString(), 200)); // _dr1["company_job_experience_2"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@period_job_experience_2", CustomString(_dr1["period_job_experience_2"].ToString(), 50)); // _dr1["period_job_experience_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@position_job_experience_2", CustomString(_dr1["position_job_experience_2"].ToString(), 250)); // _dr1["position_job_experience_2"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@salary_job_experience_2", CustomString(_dr1["salary_job_experience_2"].ToString(), 50)); // _dr1["salary_job_experience_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@reason_leaving_company_2", _dr1["reason_leaving_company_2"].ToString().Replace("'", "`"));
                    
                    sqlcomm.Parameters.AddWithValue("@company_job_experience_3", CustomString(_dr1["company_job_experience_3"].ToString(), 200)); // _dr1["company_job_experience_3"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@period_job_experience_3", CustomString(_dr1["period_job_experience_3"].ToString(), 50)); // _dr1["period_job_experience_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@position_job_experience_3", CustomString(_dr1["position_job_experience_3"].ToString(), 250)); // _dr1["position_job_experience_3"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@salary_job_experience_3", CustomString(_dr1["salary_job_experience_3"].ToString(), 50)); // _dr1["salary_job_experience_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@reason_leaving_company_3",_dr1["reason_leaving_company_3"].ToString().Replace("'", "`"));

                    sqlcomm.Parameters.AddWithValue("@company_job_experience_4", CustomString(_dr1["company_job_experience_4"].ToString(), 200)); // _dr1["company_job_experience_4"].ToString().Replace("'", "`").Substring(0, 200));
                    sqlcomm.Parameters.AddWithValue("@period_job_experience_4", CustomString(_dr1["period_job_experience_4"].ToString(), 50)); // _dr1["period_job_experience_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@position_job_experience_4", CustomString(_dr1["position_job_experience_4"].ToString(), 250)); // _dr1["position_job_experience_4"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@salary_job_experience_4", CustomString(_dr1["salary_job_experience_4"].ToString(), 50)); // _dr1["salary_job_experience_4"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@reason_leaving_company_4", _dr1["reason_leaving_company_4"].ToString().Replace("'", "`"));

                    sqlcomm.Parameters.AddWithValue("@explain_responsibility_last_position", _dr1["explain_responsibility_last_position"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@company_organization_structure", _dr1["company_organization_structure"].ToString().Replace("'", "`"));

                    sqlcomm.Parameters.AddWithValue("@height", CustomString(_dr1["height"].ToString(), 50)); // _dr1["height"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@weight", CustomString(_dr1["weight"].ToString(), 50)); // _dr1["weight"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@color_blind", CustomString(_dr1["color_blind"].ToString(), 50)); // _dr1["color_blind"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@spectacles", CustomString(_dr1["spectacles"].ToString(), 50)); // _dr1["spectacles"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@surgery_or_been_hospitalized", CustomString(_dr1["surgery_or_been_hospitalized"].ToString(), 50)); // _dr1["surgery_or_been_hospitalized"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@illnes", CustomString(_dr1["illnes"].ToString(), 250)); // _dr1["illnes"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@name_of_hospital", CustomString(_dr1["name_of_hospital"].ToString(), 200)); // _dr1["name_of_hospital"].ToString().Replace("'", "`").Substring(0, 200));

                    sqlcomm.Parameters.AddWithValue("@money_spend_per_month", _dr1["money_spend_per_month"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@salary_present_company", _dr1["salary_present_company"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@present_company_fasilities", _dr1["present_company_fasilities"].ToString().Replace("'", "`"));

                    sqlcomm.Parameters.AddWithValue("@total_loan", CustomString(_dr1["total_loan"].ToString(), 50)); // _dr1["total_loan"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@present_company_loan", CustomString(_dr1["present_company_loan"].ToString(), 50)); // _dr1["present_company_loan"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@salary_expect", _dr1["salary_expect"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@fasilities_expect", _dr1["fasilities_expect"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@start_working", _dr1["start_working"]);
                    sqlcomm.Parameters.AddWithValue("@friend_in_company", CustomString(_dr1["friend_in_company"].ToString(), 50)); // _dr1["friend_in_company"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@name_friend_in_company", CustomString(_dr1["name_friend_in_company"].ToString(), 250)); // _dr1["name_friend_in_company"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@business_trip", _dr1["business_trip"]);

                    sqlcomm.Parameters.AddWithValue("@name_reference_1", CustomString(_dr1["name_reference_1"].ToString(), 250)); // _dr1["name_reference_1"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@address_reference_1", CustomString(_dr1["address_reference_1"].ToString(), 250)); // _dr1["address_reference_1"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@phone_reference_1", CustomString(_dr1["phone_reference_1"].ToString(), 50)); // _dr1["phone_reference_1"].ToString().Replace("'","`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_reference_1", CustomString(_dr1["occupation_reference_1"].ToString(), 250)); // _dr1["occupation_reference_1"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@relation_reference_1", CustomString(_dr1["relation_reference_1"].ToString(), 250)); // _dr1["relation_reference_1"].ToString().Replace("'", "`").Substring(0, 250));

                    sqlcomm.Parameters.AddWithValue("@name_reference_2", CustomString(_dr1["name_reference_2"].ToString(), 250)); // _dr1["name_reference_2"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@address_reference_2", CustomString(_dr1["address_reference_2"].ToString(), 250)); // _dr1["address_reference_2"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@phone_reference_2", CustomString(_dr1["phone_reference_2"].ToString(), 50)); // _dr1["phone_reference_2"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_reference_2", CustomString(_dr1["occupation_reference_2"].ToString(), 250)); // _dr1["occupation_reference_2"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@relation_reference_2", CustomString(_dr1["relation_reference_2"].ToString(), 250)); // _dr1["relation_reference_2"].ToString().Replace("'", "`").Substring(0, 250));

                    sqlcomm.Parameters.AddWithValue("@name_reference_3", CustomString(_dr1["name_reference_3"].ToString(), 250)); // _dr1["name_reference_3"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@address_reference_3", CustomString(_dr1["address_reference_3"].ToString(), 250)); // _dr1["address_reference_3"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@phone_reference_3", CustomString(_dr1["phone_reference_3"].ToString(), 50)); // _dr1["phone_reference_3"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@occupation_reference_3", CustomString(_dr1["occupation_reference_3"].ToString(), 250)); // _dr1["occupation_reference_3"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@relation_reference_3", CustomString(_dr1["relation_reference_3"].ToString(), 250)); // _dr1["relation_reference_3"].ToString().Replace("'", "`").Substring(0, 250));

                    sqlcomm.Parameters.AddWithValue("@status_recruitment", CustomString(_dr1["status_recruitment"].ToString(), 50)); // _dr1["status_recruitment"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@pic_recruitment", CustomString(_dr1["pic_recruitment"].ToString(), 50)); // _dr1["pic_recruitment"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@status_record", CustomString(_dr1["status_record"].ToString(), 50)); // _dr1["status_record"].ToString().Replace("'", "`").Substring(0, 50));

                    //string _test111 = _dr1["name_of_housemate_1"].ToString().Replace("'", "`");

                    sqlcomm.Parameters.AddWithValue("@name_of_housemate_1", CustomString(_dr1["name_of_housemate_1"].ToString(), 250)); // _dr1["name_of_housemate_1"].ToString().Replace("'", "`").Substring(0,250));
                    sqlcomm.Parameters.AddWithValue("@address_housemate_1", _dr1["address_housemate_1"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@relationship_housemate_1", CustomString(_dr1["relationship_housemate_1"].ToString(), 250)); // _dr1["relationship_housemate_1"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@phone_housemate_1", CustomString(_dr1["phone_housemate_1"].ToString(), 50)); // _dr1["phone_housemate_1"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@name_of_housemate_2", CustomString(_dr1["name_of_housemate_2"].ToString(), 250)); // _dr1["name_of_housemate_2"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@address_housemate_2", _dr1["address_housemate_2"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@relationship_housemate_2", CustomString(_dr1["relationship_housemate_2"].ToString(), 250)); // _dr1["relationship_housemate_2"].ToString().Replace("'", "`").Substring(0, 250));
                    sqlcomm.Parameters.AddWithValue("@phone_housemate_2", CustomString(_dr1["phone_housemate_2"].ToString(), 50)); // _dr1["phone_housemate_2"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@HandPhone", CustomString(_dr1["HandPhone"].ToString(), 20)); // _dr1["HandPhone"].ToString().Replace("'", "`").Substring(0, 20));
                    sqlcomm.Parameters.AddWithValue("@BloodType", CustomString(_dr1["BloodType"].ToString(), 50)); // _dr1["BloodType"].ToString().Replace("'", "`").Substring(0, 50));

                    sqlcomm.Parameters.AddWithValue("@SalaryPrevCompanyNettGross", CustomString(_dr1["SalaryPrevCompanyNettGross"].ToString(), 50)); // _dr1["SalaryPrevCompanyNettGross"].ToString().Replace("'", "`").Substring(0, 50));
                    sqlcomm.Parameters.AddWithValue("@PreviousOrganization", _dr1["PreviousOrganization"].ToString().Replace("'", "`"));
                    sqlcomm.Parameters.AddWithValue("@PositionOnPreviousOrganization", _dr1["PositionOnPreviousOrganization"].ToString().Replace("'", "`"));

                    #endregion

                    //string _test2 = _dr1["id_ktp"].ToString();

                    sqlcomm.ExecuteNonQuery();

                    sqlcomm.Parameters.Clear();

                    BindDataTablePersonalInformation();
                    lblMessage.Text = "Your file uploaded successfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;


                }
                Con.Close();
                File.Delete(Server.MapPath("~/UploadFiles/" + FileUpload.PostedFile.FileName));
            }



            #region Old

            //string conString = string.Empty;
            //string extension = Path.GetExtension(FileUpload.PostedFile.FileName);
            //switch (extension)
            //{
            //    case ".xls": //Excel 97-03
            //        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
            //        break;
            //    case ".xlsx": //Excel 07 or higher
            //        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
            //        break;

            //}
            //conString = string.Format(conString, path);
            //using (OleDbConnection excel_con = new OleDbConnection(conString))
            //{
            //    excel_con.Open();
            //    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
            //    DataTable dtExcelData = new DataTable();

            //    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
            //    {
            //        oda.Fill(dtExcelData);
            //    }



            //    string consString = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(consString))
            //    {
            //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            //        {

            //            sqlBulkCopy.DestinationTableName = "dbo.PersonalInformations";

            //            con.Open();
            //            sqlBulkCopy.WriteToServer(dtExcelData);
            //            con.Close();
            //            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func2()", true);
            //            BindDataTablePersonalInformation();
            //            lblMessage.Text = "Your file uploaded successfully";
            //            lblMessage.ForeColor = System.Drawing.Color.Green;
            //            File.Delete(Server.MapPath("~/UploadFiles/" + FileUpload.PostedFile.FileName));
            //        }
            //        excel_con.Close();
            //    }
            //}
            #endregion
        }

        protected void SendEmail_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            lbKtp.Text = row.Cells[8].Text;
            txtEmailTo.Value = row.Cells[9].Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlComposeEmail').modal();", true);
            BindDataTablePersonalInformation();
        }

        protected void SaveLogSendEmail()
        {

            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Compose_email";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
            sqlcomm.Parameters.AddWithValue("@create_date", DateTime.Now.ToString());
            sqlcomm.Parameters.AddWithValue("@id_ktp", lbKtp.Text.Trim());
            sqlcomm.Parameters.AddWithValue("@email_recipient", lbEmailCandidate.Text.Trim());
            sqlcomm.Parameters.AddWithValue("@status_recruitment", ddlStatus.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@create_by", Session["nik"].ToString());

            sqlcomm.ExecuteNonQuery();
            Con.Close();
        }

        private string PopulateBodyOnprocess(string candidate_name, string job_position)
        {

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_personal_info_OnProcess.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CandidateName}", candidate_name);
            body = body.Replace("{JobPosition}", job_position);
            //body = body.Replace("{Tittle}", tittle);
            //body = body.Replace("{Branch}", branch);

            return body;
        }

        private async Task SendEmailOnProcess()
        {
            string body = this.PopulateBodyOnprocess
            (txtCandidateName.Value, txtJobApplied.Value);

            try
            {
                // Konfigurasi autentikasi
                var authority = $"https://login.microsoftonline.com/{_tenantId}";
                var scopes = new[] { "https://graph.microsoft.com/.default" };

                var confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(_clientId)
                    .WithClientSecret(_clientSecret)
                    .WithAuthority(authority)
                    .Build();

                // Mendapatkan token akses
                var authResult = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();
                var accessToken = authResult.AccessToken;

                // Create HttpClient with Authorization Header
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Create email content with HTML body
                    var emailBody = new
                    {
                        message = new
                        {
                            subject = "Personal Information Form Received",
                            body = new
                            {   
                                contentType = "HTML",
                                content = body
                            },
                            toRecipients = new[] { new { emailAddress = new { address = lbEmailCandidate.Text.Trim() } } },
                            ccRecipients = new[] { new { emailAddress = new { address = "meilva.pohan@id.yusen-logistics.com" } }, new { emailAddress = new { address = "yohanes.adhi@id.yusen-logistics.com" } }, new { emailAddress = new { address = "yovitapaska.adhisetyarini@id.yusen-logistics.com" } } }
                        },
                        saveToSentItems = true
                    };

                    var emailBodyJson = JsonConvert.SerializeObject(emailBody);
                    var content = new StringContent(emailBodyJson, Encoding.UTF8, "application/json");

                    // Send HTTP request to send email
                    var response = await httpClient.PostAsync(itsystem_endpoint, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Handle response
                    if (response.IsSuccessStatusCode)
                    {
                        // Assuming 'FuncSave()' and 'FailedSend()' are JavaScript functions on the client side
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                    }
                    else
                    {
                        Response.Write(responseContent.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSend();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSend();", true);
            }
        }

        //private void SendHtmlFormattedEmailOnProcess(string recepientEmail, string subject, string body)
        //{
        //    using (MailMessage mm = new MailMessage(_SMTPHost, lbEmailCandidate.Text.Trim()))
        //    {
        //        mm.CC.Add(new MailAddress("meilva.pohan@id.yusen-logistics.com"));
        //        mm.CC.Add(new MailAddress("mirna.dewi@id.yusen-logistics.com"));
        //        mm.CC.Add(new MailAddress("yohanes.adhi@id.yusen-logistics.com"));
        //        mm.Subject = "Personal Information Form Received";
        //        //mm.Attachments.Add(new Attachment(Server.MapPath("~/Prints/" + FileName)));
        //        mm.Body = body;
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(mm);
        //    }
        //}

        //protected void SendEmailOnProcess()
        //{

        //    string body = this.PopulateBodyOnprocess
        //    (txtCandidateName.Value, txtJobApplied.Value);
        //    this.SendHtmlFormattedEmailOnProcess("widhitech@gmail.com", "New article published!", body);

        //}

        private string PopulateBodySubmit(string candidate_name, string job_position)
        {

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_personal_info_submit.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CandidateName}", candidate_name);
            body = body.Replace("{JobPosition}", job_position);
            //body = body.Replace("{Tittle}", tittle);
            //body = body.Replace("{Branch}", branch);

            return body;
        }

        private async Task SendEmailSubmit()
        {
            string body = this.PopulateBodySubmit
            (txtCandidateName.Value, txtJobApplied.Value);

            try
            {
                // Konfigurasi autentikasi
                var authority = $"https://login.microsoftonline.com/{_tenantId}";
                var scopes = new[] { "https://graph.microsoft.com/.default" };

                var confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(_clientId)
                    .WithClientSecret(_clientSecret)
                    .WithAuthority(authority)
                    .Build();

                // Mendapatkan token akses
                var authResult = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();
                var accessToken = authResult.AccessToken;

                // Create HttpClient with Authorization Header
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Create email content with HTML body
                    var emailBody = new
                    {
                        message = new
                        {
                            subject = "Application Received",
                            body = new
                            {
                                contentType = "HTML",
                                content = body
                            },
                            toRecipients = new[] { new { emailAddress = new { address = lbEmailCandidate.Text.Trim() } } },
                            ccRecipients = new[] { new { emailAddress = new { address = "meilva.pohan@id.yusen-logistics.com" } }, new { emailAddress = new { address = "yohanes.adhi@id.yusen-logistics.com" } }, new { emailAddress = new { address = "yovitapaska.adhisetyarini@id.yusen-logistics.com" } } }
                        },
                        saveToSentItems = true
                    };

                    var emailBodyJson = JsonConvert.SerializeObject(emailBody);
                    var content = new StringContent(emailBodyJson, Encoding.UTF8, "application/json");

                    // Send HTTP request to send email
                    var response = await httpClient.PostAsync(itsystem_endpoint, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Handle response
                    if (response.IsSuccessStatusCode)
                    {
                        // Assuming 'FuncSave()' and 'FailedSend()' are JavaScript functions on the client side
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                    }
                    else
                    {
                        Response.Write(responseContent.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSend();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSend();", true);
            }
        }

        //private void SendHtmlFormattedEmailSubmit(string recepientEmail, string subject, string body)
        //{
        //    using (MailMessage mm = new MailMessage(_SMTPHost, lbEmailCandidate.Text.Trim()))
        //    {
        //        mm.CC.Add(new MailAddress("meilva.pohan@id.yusen-logistics.com"));
        //        mm.CC.Add(new MailAddress("mirna.dewi@id.yusen-logistics.com"));
        //        mm.CC.Add(new MailAddress("yohanes.adhi@id.yusen-logistics.com"));
        //        mm.Subject = "Application Received";
        //        //mm.Attachments.Add(new Attachment(Server.MapPath("~/Prints/" + FileName)));
        //        mm.Body = body;
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(mm);
        //    }
        //}

        //protected void SendEmailSubmit()
        //{

        //    string body = this.PopulateBodySubmit
        //    (txtCandidateName.Value, txtJobApplied.Value);
        //    this.SendHtmlFormattedEmailSubmit("widhitech@gmail.com", "New article published!", body);

        //}

        private string PopulateBodyHold(string candidate_name, string job_position)
        {

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_personal_info_hold.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CandidateName}", candidate_name);
            body = body.Replace("{JobPosition}", job_position);
            //body = body.Replace("{Tittle}", tittle);
            //body = body.Replace("{Branch}", branch);

            return body;
        }

        private async Task SendEmailHold()
        {
            string body = this.PopulateBodyHold
            (txtCandidateName.Value, txtJobApplied.Value);

            try
            {
                // Konfigurasi autentikasi
                var authority = $"https://login.microsoftonline.com/{_tenantId}";
                var scopes = new[] { "https://graph.microsoft.com/.default" };

                var confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(_clientId)
                    .WithClientSecret(_clientSecret)
                    .WithAuthority(authority)
                    .Build();

                // Mendapatkan token akses
                var authResult = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();
                var accessToken = authResult.AccessToken;

                // Create HttpClient with Authorization Header
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Create email content with HTML body
                    var emailBody = new
                    {
                        message = new
                        {
                            subject = "Thank You for Applying",
                            body = new
                            {
                                contentType = "HTML",
                                content = body
                            },
                            toRecipients = new[] { new { emailAddress = new { address = lbEmailCandidate.Text.Trim() } } },
                            ccRecipients = new[] { new { emailAddress = new { address = "meilva.pohan@id.yusen-logistics.com" } }, new { emailAddress = new { address = "yohanes.adhi@id.yusen-logistics.com" } }, new { emailAddress = new { address = "yovitapaska.adhisetyarini@id.yusen-logistics.com" } } }
                        },
                        saveToSentItems = true
                    };

                    var emailBodyJson = JsonConvert.SerializeObject(emailBody);
                    var content = new StringContent(emailBodyJson, Encoding.UTF8, "application/json");

                    // Send HTTP request to send email
                    var response = await httpClient.PostAsync(itsystem_endpoint, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Handle response
                    if (response.IsSuccessStatusCode)
                    {
                        // Assuming 'FuncSave()' and 'FailedSend()' are JavaScript functions on the client side
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                    }
                    else
                    {
                        Response.Write(responseContent.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSend();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSend();", true);
            }
        }

        //private void SendHtmlFormattedEmailHold(string recepientEmail, string subject, string body)
        //{
        //    using (MailMessage mm = new MailMessage(_SMTPHost, lbEmailCandidate.Text.Trim()))
        //    {
        //        mm.CC.Add(new MailAddress("meilva.pohan@id.yusen-logistics.com"));
        //        mm.CC.Add(new MailAddress("mirna.dewi@id.yusen-logistics.com"));
        //        mm.CC.Add(new MailAddress("yohanes.adhi@id.yusen-logistics.com"));
        //        mm.Subject = "Thank You for Applying";
        //        //mm.Attachments.Add(new Attachment(Server.MapPath("~/Prints/" + FileName)));
        //        mm.Body = body;
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(mm);
        //    }
        //}

        //protected void SendEmailHold()
        //{

        //    string body = this.PopulateBodyHold
        //    (txtCandidateName.Value, txtJobApplied.Value);
        //    this.SendHtmlFormattedEmailHold("widhitech@gmail.com", "New article published!", body);

        //}

        private string PopulateBodyReject(string candidate_name, string job_position)
        {

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_personal_info_reject.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CandidateName}", candidate_name);
            body = body.Replace("{JobPosition}", job_position);
            //body = body.Replace("{Tittle}", tittle);
            //body = body.Replace("{Branch}", branch);

            return body;
        }

        private async Task SendEmailReject()
        {
            string body = this.PopulateBodyReject
            (txtCandidateName.Value, txtJobApplied.Value);

            try
            {
                // Konfigurasi autentikasi
                var authority = $"https://login.microsoftonline.com/{_tenantId}";
                var scopes = new[] { "https://graph.microsoft.com/.default" };

                var confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(_clientId)
                    .WithClientSecret(_clientSecret)
                    .WithAuthority(authority)
                    .Build();

                // Mendapatkan token akses
                var authResult = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();
                var accessToken = authResult.AccessToken;

                // Create HttpClient with Authorization Header
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Create email content with HTML body
                    var emailBody = new
                    {
                        message = new
                        {
                            subject = "Thank You for Joining the Recruitment Process",
                            body = new
                            {
                                contentType = "HTML",
                                content = body
                            },
                            toRecipients = new[] { new { emailAddress = new { address = lbEmailCandidate.Text.Trim() } } },
                            ccRecipients = new[] { new { emailAddress = new { address = "meilva.pohan@id.yusen-logistics.com" } }, new { emailAddress = new { address = "yohanes.adhi@id.yusen-logistics.com" } }, new { emailAddress = new { address = "yovitapaska.adhisetyarini@id.yusen-logistics.com" } } }
                            //toRecipients = new[] { new { emailAddress = new { address = lbEmailCandidate.Text.Trim() } } },
                            //toRecipients = new[] { new { emailAddress = new { address = "widhitech@gmail.com" } } },
                            //ccRecipients = new[] { new { emailAddress = new { address = "widhi.kusuma@id.yusen-logistics.com" } } }
                        },
                        saveToSentItems = true
                    };

                    var emailBodyJson = JsonConvert.SerializeObject(emailBody);
                    var content = new StringContent(emailBodyJson, Encoding.UTF8, "application/json");

                    // Send HTTP request to send email
                    var response = await httpClient.PostAsync(itsystem_endpoint, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Handle response
                    if (response.IsSuccessStatusCode)
                    {
                        // Assuming 'FuncSave()' and 'FailedSend()' are JavaScript functions on the client side
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                    }
                    else
                    {
                        Response.Write(responseContent.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSend();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write(ex.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSend();", true);
            }
        }

        //private void SendHtmlFormattedEmailReject(string recepientEmail, string subject, string body)
        //{
        //    using (MailMessage mm = new MailMessage(_SMTPHost, lbEmailCandidate.Text.Trim()))
        //    {
        //        mm.CC.Add(new MailAddress("meilva.pohan@id.yusen-logistics.com"));
        //        mm.CC.Add(new MailAddress("mirna.dewi@id.yusen-logistics.com"));
        //        mm.CC.Add(new MailAddress("yohanes.adhi@id.yusen-logistics.com"));
        //        mm.Subject = "Thank You for Joining the Recruitment Process";
        //        //mm.Attachments.Add(new Attachment(Server.MapPath("~/Prints/" + FileName)));
        //        mm.Body = body;
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(mm);
        //    }
        //}

        //protected void SendEmailReject()
        //{

        //    string body = this.PopulateBodyReject
        //    (txtCandidateName.Value, txtJobApplied.Value);
        //    this.SendHtmlFormattedEmailReject("widhitech@gmail.com", "New article published!", body);

        //}

        protected void TablePersonalInformation_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TablePersonalInformation_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TablePersonalInformation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            lbKtp.Text = row.Cells[8].Text;
            lbEmailCandidate.Text = row.Cells[9].Text;
            txtTimeStamp.Value = row.Cells[5].Text;
            txtJobApplied.Value = row.Cells[4].Text;
            txtGender.Value = row.Cells[11].Text;
            txtCandidateName.Value = row.Cells[3].Text;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlPersonalInformation').modal();", true);
            BindDataTableLogEmailSend();
            BindDataTablePersonalInformation();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "[sp_HRIS_DB_Print_Detail_Personal_Information]";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddWithValue("@id_ktp", row.Cells[8].Text);

            sqlcomm.Connection = Con;
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ReportViewerPrintPersonalInformation.ProcessingMode = ProcessingMode.Local;
            if (row.Cells[22].Text.ToString()== "&nbsp;")
            {
                string link_image = row.Cells[21].Text.ToString().Substring(33, row.Cells[21].Text.ToString().Length - 33);
                string url = "https://drive.google.com/uc?export=download&id=" + link_image;
               
                if (!System.IO.Directory.Exists(Server.MapPath("~/Photos")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Photos"));
                }
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFile(new Uri(url), Server.MapPath("~/Photos/photo.jpg"));
                }
                //var url = "https://drive.google.com/uc?export=download&id=" + link_image;
                //var imagepath = (new WebClient()).DownloadString(url);
                //string imagePath = new Uri(Server.MapPath("~/Photos/photo.png")).AbsoluteUri;
                //ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                //ReportViewerPrintPersonalInformation.LocalReport.SetParameters(parameter);
                ReportViewerPrintPersonalInformation.LocalReport.ReportPath = Server.MapPath("~/Prints/FormPersonalInformation.rdlc");
                ReportViewerPrintPersonalInformation.LocalReport.DataSources.Clear();
                ReportViewerPrintPersonalInformation.LocalReport.DataSources.Add(new ReportDataSource("DataSetDetailPersonalInformation_new", dtb));
                ReportViewerPrintPersonalInformation.LocalReport.EnableExternalImages = true;
                ReportViewerPrintPersonalInformation.LocalReport.Refresh();

                string FileName = "Form Personal Information - " + row.Cells[3].Text + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                Byte[] mybytes = ReportViewerPrintPersonalInformation.LocalReport.Render("PDF", null,
                              out extension, out encoding,
                              out mimeType, out streams, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                Response.Flush();
                BindDataTablePersonalInformation();
                //File.Delete(Server.MapPath("~/Photos/photo.jpg"));
                //File.Delete(Server.MapPath("~/Photos/organization.jpg"));
            }
            else
            {
                string link_image = row.Cells[21].Text.ToString().Substring(33, row.Cells[21].Text.ToString().Length - 33);
                string link_stucture_organization = row.Cells[22].Text.ToString().Substring(33, row.Cells[22].Text.ToString().Length - 33);
                string url = "https://drive.google.com/uc?export=download&id=" + link_image;
                string url_stucture = "https://drive.google.com/uc?export=download&id=" + link_stucture_organization;
                if (!System.IO.Directory.Exists(Server.MapPath("~/Photos")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Photos"));
                }
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFile(new Uri(url), Server.MapPath("~/Photos/photo.jpg"));
                    client.DownloadFile(new Uri(url_stucture), Server.MapPath("~/Photos/organization.jpg"));
                }
                //var url = "https://drive.google.com/uc?export=download&id=" + link_image;
                //var imagepath = (new WebClient()).DownloadString(url);
                //string imagePath = new Uri(Server.MapPath("~/Photos/photo.png")).AbsoluteUri;
                //ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
                //ReportViewerPrintPersonalInformation.LocalReport.SetParameters(parameter);
                ReportViewerPrintPersonalInformation.LocalReport.ReportPath = Server.MapPath("~/Prints/FormPersonalInformation.rdlc");
                ReportViewerPrintPersonalInformation.LocalReport.DataSources.Clear();
                ReportViewerPrintPersonalInformation.LocalReport.DataSources.Add(new ReportDataSource("DataSetDetailPersonalInformation_new", dtb));
                ReportViewerPrintPersonalInformation.LocalReport.EnableExternalImages = true;
                ReportViewerPrintPersonalInformation.LocalReport.Refresh();

                string FileName = "Form Personal Information - " + row.Cells[3].Text + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                Byte[] mybytes = ReportViewerPrintPersonalInformation.LocalReport.Render("PDF", null,
                              out extension, out encoding,
                              out mimeType, out streams, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                Response.Flush();
                BindDataTablePersonalInformation();
                File.Delete(Server.MapPath("~/Photos/photo.jpg"));
                File.Delete(Server.MapPath("~/Photos/organization.jpg"));
            }
            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlDelete').modal();", true);
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            lblIDKTP.Text = row.Cells[8].Text;
            BindDataTablePersonalInformation();
        }

        protected void ddlPICRecruit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableLogEmailSend_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableLogEmailSend_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "UpdateStatusRecruitment");
            sqlcomm.Parameters.AddWithValue("@id_ktp", lbKtp.Text.Trim());
            sqlcomm.Parameters.AddWithValue("@status_recruitment", ddlStatus.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@pic_recruitment", ddlPICRecruit.SelectedItem.Text.ToString());

            sqlcomm.ExecuteNonQuery();

            if (ddlStatus.SelectedItem.Text.ToString() == "On Process")
            {
                await SendEmailOnProcess();
            }
            else if (ddlStatus.SelectedItem.Text.ToString() == "Hold")
            {
                await SendEmailHold();
            }
            else if (ddlStatus.SelectedItem.Text.ToString() == "Reject")
            {
                await SendEmailReject();
            }
            else if (ddlStatus.SelectedItem.Text.ToString() == "Submit")
            {
                await SendEmailSubmit();
            }

            SaveLogSendEmail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
            BindDataTablePersonalInformation();
            BindDataTableLogEmailSend();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlPersonalInformation').modal();", true);
        }

        protected void btnSending_Click(object sender, EventArgs e)
        {

        }

        protected void btnYesDelete_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "UpdateStatusRecord");
            sqlcomm.Parameters.AddWithValue("@id_ktp", lblIDKTP.Text.Trim());

            sqlcomm.ExecuteNonQuery();
            BindDataTablePersonalInformation();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncDelete();", true);
        }

    }
}