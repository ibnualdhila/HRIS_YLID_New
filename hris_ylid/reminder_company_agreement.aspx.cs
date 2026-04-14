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
    public partial class reminder_company_agreement : System.Web.UI.Page
    {
        string _SMTPPassword = WebConfigurationManager.AppSettings["SMTPPassword"];
        string _SMTPHost = WebConfigurationManager.AppSettings["SMTPHost"];
        string _SMTPCredential = WebConfigurationManager.AppSettings["SMTPCredential"];
        int _SMTPPort = Convert.ToInt16(WebConfigurationManager.AppSettings["SMTPPort"]);
        string _SMTPServer = WebConfigurationManager.AppSettings["SMTPServer"];

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetDataCompanyAgreementExpired30Day()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckBefore30dayExpiredCompanyAgreement");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableCompanyAgreement30DayExpired.DataSource = dtb;
            TableCompanyAgreement30DayExpired.DataBind();

            TableCompanyAgreement30DayExpired.UseAccessibleHeader = true;
            TableCompanyAgreement30DayExpired.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetDataCompanyAgreementExpired60Day()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckBefore60dayExpiredCompanyAgreement");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableCompanyAgreement60DayExpired.DataSource = dtb;
            TableCompanyAgreement60DayExpired.DataBind();

            TableCompanyAgreement60DayExpired.UseAccessibleHeader = true;
            TableCompanyAgreement60DayExpired.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetDataCompanyAgreementExpired90Day()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_CompanyAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckBefore90dayExpiredCompanyAgreement");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableCompanyAgreement90DayExpired.DataSource = dtb;
            TableCompanyAgreement90DayExpired.DataBind();

            TableCompanyAgreement90DayExpired.UseAccessibleHeader = true;
            TableCompanyAgreement90DayExpired.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void CheckCompanyAgreementBefore30DayExpired()
        {
            GetDataCompanyAgreementExpired30Day();
            foreach (GridViewRow grow in TableCompanyAgreement30DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string agreementname = grow.Cells[2].Text.Trim();
                string effectivedate = grow.Cells[7].Text.Trim();
                string expirationdate = grow.Cells[8].Text.Trim();
                string email = grow.Cells[19].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNumber}", agreementnumber);
                myString = myString.Replace("{AgreementName}", agreementname);
                myString = myString.Replace("{EffectiveDate}", effectivedate);
                myString = myString.Replace("{ExpirationDate}", expirationdate);
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

        protected void CheckCompanyAgreementBefore60DayExpired()
        {
            GetDataCompanyAgreementExpired60Day();
            foreach (GridViewRow grow in TableCompanyAgreement60DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string agreementname = grow.Cells[2].Text.Trim();
                string effectivedate = grow.Cells[7].Text.Trim();
                string expirationdate = grow.Cells[8].Text.Trim();
                string email = grow.Cells[19].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNumber}", agreementnumber);
                myString = myString.Replace("{AgreementName}", agreementname);
                myString = myString.Replace("{EffectiveDate}", effectivedate);
                myString = myString.Replace("{ExpirationDate}", expirationdate);
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

        protected void CheckCompanyAgreementBefore90DayExpired()
        {
            GetDataCompanyAgreementExpired90Day();
            foreach (GridViewRow grow in TableCompanyAgreement90DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string agreementname = grow.Cells[2].Text.Trim();
                string effectivedate = grow.Cells[7].Text.Trim();
                string expirationdate = grow.Cells[8].Text.Trim();
                string email = grow.Cells[19].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNumber}", agreementnumber);
                myString = myString.Replace("{AgreementName}", agreementname);
                myString = myString.Replace("{EffectiveDate}", effectivedate);
                myString = myString.Replace("{ExpirationDate}", expirationdate);
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
            CheckCompanyAgreementBefore30DayExpired();
            CheckCompanyAgreementBefore60DayExpired();
            CheckCompanyAgreementBefore90DayExpired();
            Response.Redirect("AutoClosePage.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void TableCompanyAgreement30DayExpired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableCompanyAgreement30DayExpired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableCompanyAgreement30DayExpired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableCompanyAgreement60DayExpired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableCompanyAgreement60DayExpired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableCompanyAgreement60DayExpired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableCompanyAgreement90DayExpired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableCompanyAgreement90DayExpired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableCompanyAgreement90DayExpired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}