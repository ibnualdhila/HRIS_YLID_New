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
    public partial class reminder_employee_will_retired : System.Web.UI.Page
    {
        string _SMTPPassword = WebConfigurationManager.AppSettings["SMTPPassword"];
        string _SMTPHost = WebConfigurationManager.AppSettings["SMTPHost"];
        string _SMTPCredential = WebConfigurationManager.AppSettings["SMTPCredential"];
        int _SMTPPort = Convert.ToInt16(WebConfigurationManager.AppSettings["SMTPPort"]);
        string _SMTPServer = WebConfigurationManager.AppSettings["SMTPServer"];

        protected void Page_Load(object sender, EventArgs e)
        {

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
            sqlcomm.Parameters.AddWithValue("@StatementType", "ReminderEmployeeBefore3MonthRetired");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployeeWillRetired.DataSource = dtb;
            TableEmployeeWillRetired.DataBind();

            TableEmployeeWillRetired.UseAccessibleHeader = true;
            TableEmployeeWillRetired.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();

        }

        protected void CheckEmployeeWillRetired()
        {
            BindDataTableEmployees();
            foreach (GridViewRow grow in TableEmployeeWillRetired.Rows)
            {
                string nik = grow.Cells[3].Text.Trim();
                string employeename = grow.Cells[2].Text.Trim();
                string birthdate = grow.Cells[13].Text.Trim();
                string joindate = grow.Cells[12].Text.Trim();
                string retireddate = grow.Cells[14].Text.Trim();
                string email = "widhi.kusuma@id.yusen-logistics.com";
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_will_retired.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{BirthDate}", birthdate);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{RetiredDate}", retireddate);
                string from = _SMTPHost;
                string to = email;
                MailMessage Msg = new MailMessage(from, to);
                Msg.Subject = "[HRIS-YLID] - RETIREMENT EMPLOYEE REMINDER";
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
            CheckEmployeeWillRetired();
            Response.Redirect("AutoClosePage.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void TableEmployeeWillRetired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableEmployeeWillRetired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployeeWillRetired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}