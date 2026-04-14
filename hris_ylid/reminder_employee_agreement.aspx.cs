using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class reminder_employee_agreement : System.Web.UI.Page
    {
        string _SMTPPassword = WebConfigurationManager.AppSettings["SMTPPassword"];
        string _SMTPHost = WebConfigurationManager.AppSettings["SMTPHost"];
        string _SMTPCredential = WebConfigurationManager.AppSettings["SMTPCredential"];
        int _SMTPPort = Convert.ToInt16(WebConfigurationManager.AppSettings["SMTPPort"]);
        string _SMTPServer = WebConfigurationManager.AppSettings["SMTPServer"];

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetDataEmployeeAgreementExpired30Day()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckBefore30dayExpiredEmployeeAgreement");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployeeAgreement30DayExpired.DataSource = dtb;
            TableEmployeeAgreement30DayExpired.DataBind();

            TableEmployeeAgreement30DayExpired.UseAccessibleHeader = true;
            TableEmployeeAgreement30DayExpired.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetDataEmployeeAgreementExpired60Day()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckBefore60dayExpiredEmployeeAgreement");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployeeAgreement60DayExpired.DataSource = dtb;
            TableEmployeeAgreement60DayExpired.DataBind();

            TableEmployeeAgreement60DayExpired.UseAccessibleHeader = true;
            TableEmployeeAgreement60DayExpired.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetDataEmployeeAgreementExpired90Day()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckBefore90dayExpiredEmployeeAgreement");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployeeAgreement90DayExpired.DataSource = dtb;
            TableEmployeeAgreement90DayExpired.DataBind();

            TableEmployeeAgreement90DayExpired.UseAccessibleHeader = true;
            TableEmployeeAgreement90DayExpired.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void CheckEmployeeAgreementBefore30DayExpired()
        {
            GetDataEmployeeAgreementExpired30Day();
            foreach (GridViewRow grow in TableEmployeeAgreement30DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string nik = grow.Cells[2].Text.Trim();
                string employeename = grow.Cells[3].Text.Trim();
                string joindate = grow.Cells[5].Text.Trim();
                string endofagreementdate = grow.Cells[10].Text.Trim();
                string email = grow.Cells[15].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNo}", agreementnumber);
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);
                string from = _SMTPHost;
                string to = email;
                MailMessage Msg = new MailMessage(from, to);
                Msg.Subject = "[HRIS-YLID] - AGREEMENT EXPIRATION REMINDER";
                Msg.Body = myString;
                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtp.Host = _SMTPServer;
                NetworkCredential credential = new NetworkCredential();
                credential.UserName = _SMTPCredential;
                credential.Password = _SMTPPassword;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;
                smtp.Port = _SMTPPort;
                smtp.EnableSsl = true;
                smtp.Send(Msg);
                reader.Dispose();
            }
        }

        protected void CheckEmployeeAgreementBefore60DayExpired()
        {
            GetDataEmployeeAgreementExpired60Day();
            foreach (GridViewRow grow in TableEmployeeAgreement60DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string nik = grow.Cells[2].Text.Trim();
                string employeename = grow.Cells[3].Text.Trim();
                string joindate = grow.Cells[5].Text.Trim();
                string endofagreementdate = grow.Cells[10].Text.Trim();
                string email = grow.Cells[15].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNo}", agreementnumber);
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);
                string from = _SMTPHost;
                string to = email;
                MailMessage Msg = new MailMessage(from, to);
                Msg.Subject = "[HRIS-YLID] - AGREEMENT EXPIRATION REMINDER";
                Msg.Body = myString;
                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtp.Host = _SMTPServer;
                NetworkCredential credential = new NetworkCredential();
                credential.UserName = _SMTPCredential;
                credential.Password = _SMTPPassword;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;
                smtp.Port = _SMTPPort;
                smtp.EnableSsl = true;
                smtp.Send(Msg);
                reader.Dispose();
            }
        }

        protected void CheckEmployeeAgreementBefore90DayExpired()
        {
            GetDataEmployeeAgreementExpired90Day();
            foreach (GridViewRow grow in TableEmployeeAgreement90DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string nik = grow.Cells[2].Text.Trim();
                string employeename = grow.Cells[3].Text.Trim();
                string joindate = grow.Cells[5].Text.Trim();
                string endofagreementdate = grow.Cells[10].Text.Trim();
                string email = grow.Cells[15].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNo}", agreementnumber);
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);
                string from = _SMTPHost;
                string to = email;
                MailMessage Msg = new MailMessage(from, to);
                Msg.Subject = "[HRIS-YLID] - AGREEMENT EXPIRATION REMINDER";
                Msg.Body = myString;
                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtp.Host = _SMTPServer;
                NetworkCredential credential = new NetworkCredential();
                credential.UserName = _SMTPCredential;
                credential.Password = _SMTPPassword;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;
                smtp.Port = _SMTPPort;
                smtp.EnableSsl = true;
                smtp.Send(Msg);
                reader.Dispose();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CheckEmployeeAgreementBefore30DayExpired();
            CheckEmployeeAgreementBefore60DayExpired();
            CheckEmployeeAgreementBefore90DayExpired();
            Response.Redirect("AutoClosePage.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void TableEmployeeAgreement30DayExpired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableEmployeeAgreement30DayExpired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployeeAgreement30DayExpired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableEmployeeAgreement60DayExpired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableEmployeeAgreement60DayExpired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployeeAgreement60DayExpired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableEmployeeAgreement90DayExpired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableEmployeeAgreement90DayExpired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployeeAgreement90DayExpired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}