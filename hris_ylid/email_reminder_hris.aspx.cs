using Microsoft.Identity.Client;
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

namespace hris_ylid
{
    public partial class email_reminder_hris : System.Web.UI.Page
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

        }

        #region CompanyAgreement
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

        private async Task CheckCompanyAgreementBefore30DayExpired()
        {
            GetDataCompanyAgreementExpired30Day();
            foreach (GridViewRow grow in TableCompanyAgreement30DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string agreementname = grow.Cells[2].Text.Trim();
                string effectivedate = grow.Cells[7].Text.Trim();
                string expirationdate = grow.Cells[8].Text.Trim();
                string email = grow.Cells[19].Text.Trim();
                string email2 = grow.Cells[20].Text.Trim();
                string email3 = grow.Cells[21].Text.Trim();

                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNumber}", agreementnumber);
                myString = myString.Replace("{AgreementName}", agreementname);
                myString = myString.Replace("{EffectiveDate}", effectivedate);
                myString = myString.Replace("{ExpirationDate}", expirationdate);

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
                                subject = "[HRIS-YLID] - COMPANY AGREEMENT EXPIRATION REMINDER",
                                body = new
                                {
                                    contentType = "HTML",
                                    content = myString
                                },
                                toRecipients = new[] { new { emailAddress = new { address = email } } },
                                ccRecipients = new[] { new { emailAddress = new { address = email2 } }, new { emailAddress = new { address = email3 } } }
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
        }

        //protected void CheckCompanyAgreementBefore30DayExpired()
        //{
        //    GetDataCompanyAgreementExpired30Day();
        //    foreach (GridViewRow grow in TableCompanyAgreement30DayExpired.Rows)
        //    {
        //        string agreementnumber = grow.Cells[1].Text.Trim();
        //        string agreementname = grow.Cells[2].Text.Trim();
        //        string effectivedate = grow.Cells[7].Text.Trim();
        //        string expirationdate = grow.Cells[8].Text.Trim();
        //        string email = grow.Cells[19].Text.Trim();
        //        string email2 = grow.Cells[20].Text.Trim();
        //        string email3 = grow.Cells[21].Text.Trim();
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{AgreementNumber}", agreementnumber);
        //        myString = myString.Replace("{AgreementName}", agreementname);
        //        myString = myString.Replace("{EffectiveDate}", effectivedate);
        //        myString = myString.Replace("{ExpirationDate}", expirationdate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress(email2));
        //        Msg.CC.Add(new MailAddress(email3));
        //        Msg.Subject = "[HRIS-YLID] - COMPANY AGREEMENT EXPIRATION REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}

        private async Task CheckCompanyAgreementBefore60DayExpired()
        {
            GetDataCompanyAgreementExpired60Day();
            foreach (GridViewRow grow in TableCompanyAgreement60DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string agreementname = grow.Cells[2].Text.Trim();
                string effectivedate = grow.Cells[7].Text.Trim();
                string expirationdate = grow.Cells[8].Text.Trim();
                string email = grow.Cells[19].Text.Trim();
                string email2 = grow.Cells[20].Text.Trim();
                string email3 = grow.Cells[21].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNumber}", agreementnumber);
                myString = myString.Replace("{AgreementName}", agreementname);
                myString = myString.Replace("{EffectiveDate}", effectivedate);
                myString = myString.Replace("{ExpirationDate}", expirationdate);

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
                                subject = "[HRIS-YLID] - COMPANY AGREEMENT EXPIRATION REMINDER",
                                body = new
                                {
                                    contentType = "HTML",
                                    content = myString
                                },
                                toRecipients = new[] { new { emailAddress = new { address = email } } },
                                ccRecipients = new[] { new { emailAddress = new { address = email2 } }, new { emailAddress = new { address = email3 } } }
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
        }

        //protected void CheckCompanyAgreementBefore60DayExpired()
        //{
        //    GetDataCompanyAgreementExpired60Day();
        //    foreach (GridViewRow grow in TableCompanyAgreement60DayExpired.Rows)
        //    {
        //        string agreementnumber = grow.Cells[1].Text.Trim();
        //        string agreementname = grow.Cells[2].Text.Trim();
        //        string effectivedate = grow.Cells[7].Text.Trim();
        //        string expirationdate = grow.Cells[8].Text.Trim();
        //        string email = grow.Cells[19].Text.Trim();
        //        string email2 = grow.Cells[20].Text.Trim();
        //        string email3 = grow.Cells[21].Text.Trim();
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{AgreementNumber}", agreementnumber);
        //        myString = myString.Replace("{AgreementName}", agreementname);
        //        myString = myString.Replace("{EffectiveDate}", effectivedate);
        //        myString = myString.Replace("{ExpirationDate}", expirationdate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress(email2));
        //        Msg.CC.Add(new MailAddress(email3));
        //        Msg.Subject = "[HRIS-YLID] - COMPANY AGREEMENT EXPIRATION REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}

        private async Task CheckCompanyAgreementBefore90DayExpired()
        {
            GetDataCompanyAgreementExpired90Day();
            foreach (GridViewRow grow in TableCompanyAgreement90DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string agreementname = grow.Cells[2].Text.Trim();
                string effectivedate = grow.Cells[7].Text.Trim();
                string expirationdate = grow.Cells[8].Text.Trim();
                string email = grow.Cells[19].Text.Trim();
                string email2 = grow.Cells[20].Text.Trim();
                string email3 = grow.Cells[21].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNumber}", agreementnumber);
                myString = myString.Replace("{AgreementName}", agreementname);
                myString = myString.Replace("{EffectiveDate}", effectivedate);
                myString = myString.Replace("{ExpirationDate}", expirationdate);

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
                                subject = "[HRIS-YLID] - COMPANY AGREEMENT EXPIRATION REMINDER",
                                body = new
                                {
                                    contentType = "HTML",
                                    content = myString
                                },
                                toRecipients = new[] { new { emailAddress = new { address = email } } },
                                ccRecipients = new[] { new { emailAddress = new { address = email2 } }, new { emailAddress = new { address = email3 } } }
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
        }

        //protected void CheckCompanyAgreementBefore90DayExpired()
        //{
        //    GetDataCompanyAgreementExpired90Day();
        //    foreach (GridViewRow grow in TableCompanyAgreement90DayExpired.Rows)
        //    {
        //        string agreementnumber = grow.Cells[1].Text.Trim();
        //        string agreementname = grow.Cells[2].Text.Trim();
        //        string effectivedate = grow.Cells[7].Text.Trim();
        //        string expirationdate = grow.Cells[8].Text.Trim();
        //        string email = grow.Cells[19].Text.Trim();
        //        string email2 = grow.Cells[20].Text.Trim();
        //        string email3 = grow.Cells[21].Text.Trim();
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_company_agreement.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{AgreementNumber}", agreementnumber);
        //        myString = myString.Replace("{AgreementName}", agreementname);
        //        myString = myString.Replace("{EffectiveDate}", effectivedate);
        //        myString = myString.Replace("{ExpirationDate}", expirationdate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress(email2));
        //        Msg.CC.Add(new MailAddress(email3));
        //        Msg.Subject = "[HRIS-YLID] - COMPANY AGREEMENT EXPIRATION REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}
        #endregion

        #region EmployeeAgreement
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

        protected void GetDataEmployeeAgreementExpired180Day()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_EmployeeAgreement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckBefore180dayExpiredEmployeeAgreement");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableEmployeeAgreement180DayExpired.DataSource = dtb;
            TableEmployeeAgreement180DayExpired.DataBind();

            TableEmployeeAgreement180DayExpired.UseAccessibleHeader = true;
            TableEmployeeAgreement180DayExpired.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        private async Task CheckEmployeeAgreementBefore30DayExpired()
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
                string email2 = grow.Cells[21].Text.Trim();
                string email3 = grow.Cells[22].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNo}", agreementnumber);
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);

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
                                subject = "[HRIS-YLID] - EMPLOYEE AGREEMENT EXPIRATION REMINDER",
                                body = new
                                {
                                    contentType = "HTML",
                                    content = myString
                                },
                                toRecipients = new[] { new { emailAddress = new { address = email } } },
                                ccRecipients = new[] { new { emailAddress = new { address = email2 } }, new { emailAddress = new { address = email3 } } }
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
        }

        //protected void CheckEmployeeAgreementBefore30DayExpired()
        //{
        //    GetDataEmployeeAgreementExpired30Day();
        //    foreach (GridViewRow grow in TableEmployeeAgreement30DayExpired.Rows)
        //    {
        //        string agreementnumber = grow.Cells[1].Text.Trim();
        //        string nik = grow.Cells[2].Text.Trim();
        //        string employeename = grow.Cells[3].Text.Trim();
        //        string joindate = grow.Cells[5].Text.Trim();
        //        string endofagreementdate = grow.Cells[10].Text.Trim();
        //        string email = grow.Cells[15].Text.Trim();
        //        string email2 = grow.Cells[21].Text.Trim();
        //        string email3 = grow.Cells[22].Text.Trim();
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{AgreementNo}", agreementnumber);
        //        myString = myString.Replace("{NIK}", nik);
        //        myString = myString.Replace("{EmployeeName}", employeename);
        //        myString = myString.Replace("{JoinDate}", joindate);
        //        myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress(email2));
        //        Msg.CC.Add(new MailAddress(email3));
        //        Msg.Subject = "[HRIS-YLID] - EMPLOYEE AGREEMENT EXPIRATION REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}

        private async Task CheckEmployeeAgreementBefore60DayExpired()
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
                string email2 = grow.Cells[21].Text.Trim();
                string email3 = grow.Cells[22].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNo}", agreementnumber);
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);

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
                                subject = "[HRIS-YLID] - EMPLOYEE AGREEMENT EXPIRATION REMINDER",
                                body = new
                                {
                                    contentType = "HTML",
                                    content = myString
                                },
                                toRecipients = new[] { new { emailAddress = new { address = email } } },
                                ccRecipients = new[] { new { emailAddress = new { address = email2 } }, new { emailAddress = new { address = email3 } } }
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
        }

        //protected void CheckEmployeeAgreementBefore60DayExpired()
        //{
        //    GetDataEmployeeAgreementExpired60Day();
        //    foreach (GridViewRow grow in TableEmployeeAgreement60DayExpired.Rows)
        //    {
        //        string agreementnumber = grow.Cells[1].Text.Trim();
        //        string nik = grow.Cells[2].Text.Trim();
        //        string employeename = grow.Cells[3].Text.Trim();
        //        string joindate = grow.Cells[5].Text.Trim();
        //        string endofagreementdate = grow.Cells[10].Text.Trim();
        //        string email = grow.Cells[15].Text.Trim();
        //        string email2 = grow.Cells[21].Text.Trim();
        //        string email3 = grow.Cells[22].Text.Trim();
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{AgreementNo}", agreementnumber);
        //        myString = myString.Replace("{NIK}", nik);
        //        myString = myString.Replace("{EmployeeName}", employeename);
        //        myString = myString.Replace("{JoinDate}", joindate);
        //        myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress(email2));
        //        Msg.CC.Add(new MailAddress(email3));
        //        Msg.Subject = "[HRIS-YLID] - EMPLOYEE AGREEMENT EXPIRATION REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}

        private async Task CheckEmployeeAgreementBefore90DayExpired()
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
                string email2 = grow.Cells[21].Text.Trim();
                string email3 = grow.Cells[22].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNo}", agreementnumber);
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);

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
                                subject = "[HRIS-YLID] - EMPLOYEE AGREEMENT EXPIRATION REMINDER",
                                body = new
                                {
                                    contentType = "HTML",
                                    content = myString
                                },
                                toRecipients = new[] { new { emailAddress = new { address = email } } },
                                ccRecipients = new[] { new { emailAddress = new { address = email2 } }, new { emailAddress = new { address = email3 } } }
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
        }

        //protected void CheckEmployeeAgreementBefore90DayExpired()
        //{
        //    GetDataEmployeeAgreementExpired90Day();
        //    foreach (GridViewRow grow in TableEmployeeAgreement90DayExpired.Rows)
        //    {
        //        string agreementnumber = grow.Cells[1].Text.Trim();
        //        string nik = grow.Cells[2].Text.Trim();
        //        string employeename = grow.Cells[3].Text.Trim();
        //        string joindate = grow.Cells[5].Text.Trim();
        //        string endofagreementdate = grow.Cells[10].Text.Trim();
        //        string email = grow.Cells[15].Text.Trim();
        //        string email2 = grow.Cells[21].Text.Trim();
        //        string email3 = grow.Cells[22].Text.Trim();
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{AgreementNo}", agreementnumber);
        //        myString = myString.Replace("{NIK}", nik);
        //        myString = myString.Replace("{EmployeeName}", employeename);
        //        myString = myString.Replace("{JoinDate}", joindate);
        //        myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress(email2));
        //        Msg.CC.Add(new MailAddress(email3));
        //        Msg.Subject = "[HRIS-YLID] - EMPLOYEE AGREEMENT EXPIRATION REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}

        private async Task CheckEmployeeAgreementBefore180DayExpired()
        {
            GetDataEmployeeAgreementExpired180Day();
            foreach (GridViewRow grow in TableEmployeeAgreement180DayExpired.Rows)
            {
                string agreementnumber = grow.Cells[1].Text.Trim();
                string nik = grow.Cells[2].Text.Trim();
                string employeename = grow.Cells[3].Text.Trim();
                string joindate = grow.Cells[5].Text.Trim();
                string endofagreementdate = grow.Cells[10].Text.Trim();
                string email = grow.Cells[15].Text.Trim();
                string email2 = grow.Cells[21].Text.Trim();
                string email3 = grow.Cells[22].Text.Trim();
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{AgreementNo}", agreementnumber);
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);

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
                                subject = "[HRIS-YLID] - EMPLOYEE AGREEMENT EXPIRATION REMINDER",
                                body = new
                                {
                                    contentType = "HTML",
                                    content = myString
                                },
                                toRecipients = new[] { new { emailAddress = new { address = email } } },
                                ccRecipients = new[] { new { emailAddress = new { address = email2 } }, new { emailAddress = new { address = email3 } } }
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
        }

        //protected void CheckEmployeeAgreementBefore180DayExpired()
        //{
        //    GetDataEmployeeAgreementExpired180Day();
        //    foreach (GridViewRow grow in TableEmployeeAgreement180DayExpired.Rows)
        //    {
        //        string agreementnumber = grow.Cells[1].Text.Trim();
        //        string nik = grow.Cells[2].Text.Trim();
        //        string employeename = grow.Cells[3].Text.Trim();
        //        string joindate = grow.Cells[5].Text.Trim();
        //        string endofagreementdate = grow.Cells[10].Text.Trim();
        //        string email = grow.Cells[15].Text.Trim();
        //        string email2 = grow.Cells[21].Text.Trim();
        //        string email3 = grow.Cells[22].Text.Trim();
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_agreement.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{AgreementNo}", agreementnumber);
        //        myString = myString.Replace("{NIK}", nik);
        //        myString = myString.Replace("{EmployeeName}", employeename);
        //        myString = myString.Replace("{JoinDate}", joindate);
        //        myString = myString.Replace("{EndOfAgreementDate}", endofagreementdate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress(email2));
        //        Msg.CC.Add(new MailAddress(email3));
        //        Msg.Subject = "[HRIS-YLID] - EMPLOYEE AGREEMENT EXPIRATION REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}
        #endregion

        #region Retirement
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

        private async Task CheckEmployeeWillRetired()
        {
            BindDataTableEmployees();
            foreach (GridViewRow grow in TableEmployeeWillRetired.Rows)
            {
                string nik = grow.Cells[3].Text.Trim();
                string employeename = grow.Cells[2].Text.Trim();
                string birthdate = grow.Cells[13].Text.Trim();
                string joindate = grow.Cells[12].Text.Trim();
                string retireddate = grow.Cells[14].Text.Trim();
                string email = "yohanes.adhi@id.yusen-logistics.com";
                //string email = "ibnu.aldhila@id.yusen-logistics.com";
                StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_will_retired.html"));
                string readFile = reader.ReadToEnd();
                string myString = readFile;
                myString = myString.Replace("{NIK}", nik);
                myString = myString.Replace("{EmployeeName}", employeename);
                myString = myString.Replace("{BirthDate}", birthdate);
                myString = myString.Replace("{JoinDate}", joindate);
                myString = myString.Replace("{RetiredDate}", retireddate);

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
                                subject = "[HRIS-YLID] - RETIREMENT EMPLOYEE REMINDER",
                                body = new
                                {
                                    contentType = "HTML",
                                    content = myString
                                },
                                toRecipients = new[] { new { emailAddress = new { address = email } } },
                                //ccRecipients = new[] { new { emailAddress = new { address = "hasan.bawawi@id.yusen-logistics.com" } }, new { emailAddress = new { address = "agung.prasetiyo@id.yusen-logistics.com" } } }
                                ccRecipients = new[] { new { emailAddress = new { address = "meilva.pohan@id.yusen-logistics.com" } }, new { emailAddress = new { address = "siti.tsauroh@id.yusen-logistics.com" } } }
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
        }

        //protected void CheckEmployeeWillRetired()
        //{
        //    BindDataTableEmployees();
        //    foreach (GridViewRow grow in TableEmployeeWillRetired.Rows)
        //    {
        //        string nik = grow.Cells[3].Text.Trim();
        //        string employeename = grow.Cells[2].Text.Trim();
        //        string birthdate = grow.Cells[13].Text.Trim();
        //        string joindate = grow.Cells[12].Text.Trim();
        //        string retireddate = grow.Cells[14].Text.Trim();
        //        string email = "yohanes.adhi@id.yusen-logistics.com";
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_will_retired.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{NIK}", nik);
        //        myString = myString.Replace("{EmployeeName}", employeename);
        //        myString = myString.Replace("{BirthDate}", birthdate);
        //        myString = myString.Replace("{JoinDate}", joindate);
        //        myString = myString.Replace("{RetiredDate}", retireddate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress("meilva.pohan@id.yusen-logistics.com"));
        //        Msg.CC.Add(new MailAddress("siti.tsauroh@id.yusen-logistics.com"));
        //        Msg.Subject = "[HRIS-YLID] - RETIREMENT EMPLOYEE REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}
        #endregion

        #region ValidityWillExpired
        //protected void BindDataTableValidityWillExpired()
        //{
        //    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
        //    SqlConnection Con = new SqlConnection(path);
        //    Con.Open();
        //    SqlCommand sqlcomm = new SqlCommand();
        //    sqlcomm.CommandText = "sp_HRIS_DB_Training";
        //    sqlcomm.CommandType = CommandType.StoredProcedure;
        //    sqlcomm.Connection = Con;
        //    sqlcomm.Parameters.AddWithValue("@StatementType", "DetailTrainingValidityWillBeExpired");

        //    DataTable dtb = new DataTable();
        //    SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

        //    sda.Fill(dtb);
        //    ViewState["myViewState"] = dtb;
        //    TableWillExpired.DataSource = dtb;
        //    TableWillExpired.DataBind();

        //    TableWillExpired.UseAccessibleHeader = true;
        //    TableWillExpired.HeaderRow.TableSection = TableRowSection.TableHeader;

        //    Con.Close();

        //}

        //protected void CheckValidityWillExpired()
        //{
        //    BindDataTableValidityWillExpired();
        //    foreach (GridViewRow grow in TableWillExpired.Rows)
        //    {
        //        string nik = grow.Cells[3].Text.Trim();
        //        string employeename = grow.Cells[2].Text.Trim();
        //        string birthdate = grow.Cells[13].Text.Trim();
        //        string joindate = grow.Cells[12].Text.Trim();
        //        string retireddate = grow.Cells[14].Text.Trim();
        //        string email = "yohanes.adhi@id.yusen-logistics.com";
        //        StreamReader reader = new StreamReader(Server.MapPath("~/templete_email_reminder_employee_will_retired.html"));
        //        string readFile = reader.ReadToEnd();
        //        string myString = readFile;
        //        myString = myString.Replace("{NIK}", nik);
        //        myString = myString.Replace("{EmployeeName}", employeename);
        //        myString = myString.Replace("{BirthDate}", birthdate);
        //        myString = myString.Replace("{JoinDate}", joindate);
        //        myString = myString.Replace("{RetiredDate}", retireddate);
        //        string from = _SMTPHost;
        //        string to = email;
        //        MailMessage Msg = new MailMessage(from, to);
        //        Msg.CC.Add(new MailAddress("meilva.pohan@id.yusen-logistics.com"));
        //        Msg.CC.Add(new MailAddress("siti.tsauroh@id.yusen-logistics.com"));
        //        Msg.Subject = "[HRIS-YLID] - RETIREMENT EMPLOYEE REMINDER";
        //        Msg.Body = myString;
        //        Msg.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = smtp.Host = _SMTPServer;
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = _SMTPCredential;
        //        credential.Password = _SMTPPassword;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = _SMTPPort;
        //        smtp.EnableSsl = true;
        //        smtp.Send(Msg);
        //        reader.Dispose();
        //    }
        //}
        #endregion

        #region TableReminder
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

        protected void TableEmployeeWillRetired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableEmployeeWillRetired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployeeWillRetired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableEmployeeAgreement120DayExpired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableEmployeeAgreement120DayExpired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployeeAgreement120DayExpired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableEmployeeAgreement180DayExpired_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableEmployeeAgreement180DayExpired_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableEmployeeAgreement180DayExpired_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            await CheckCompanyAgreementBefore30DayExpired();
            await CheckCompanyAgreementBefore60DayExpired();
            await CheckCompanyAgreementBefore90DayExpired();

            await CheckEmployeeAgreementBefore30DayExpired();
            await CheckEmployeeAgreementBefore60DayExpired();
            await CheckEmployeeAgreementBefore90DayExpired();
            await CheckEmployeeAgreementBefore180DayExpired();

            await CheckEmployeeWillRetired();

            Response.Redirect("AutoClosePage.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}