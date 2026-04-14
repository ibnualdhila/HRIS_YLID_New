using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                GetDataRegister();
                GetEmployees();
                
            }
        }

        protected void GetEmployees()
        {
            ddlEmployees.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "GetEmployees");

            SqlDataReader dr;

            try
            {

                ListItem newItem = new ListItem();
                newItem.Text = "<Select Name>";
                newItem.Value = "0";
                ddlEmployees.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["fullname"].ToString();
                    newItem.Value = dr["id"].ToString();
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

        protected void GetGroupAccess()
        {
            ddlGroupAccess.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewGroup");

            SqlDataReader dr;

            try
            {

                ListItem newItem = new ListItem();
                newItem.Text = "<Select Group>";
                newItem.Value = "0";
                ddlGroupAccess.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["GroupName"].ToString();
                    newItem.Value = dr["Oid"].ToString();
                    ddlGroupAccess.Items.Add(newItem);
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

        protected void GetDataRegister()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableUserRegister.DataSource = dtb;
            TableUserRegister.DataBind();

            TableUserRegister.Columns[5].Visible = false;
            TableUserRegister.Columns[8].Visible = false;

            TableUserRegister.UseAccessibleHeader = true;
            TableUserRegister.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetDataUserAccessGroupRole()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewAccessGroupRole");
            sqlcomm.Parameters.AddWithValue("@Oid_usermanagement", hlbOid.Value.ToString());
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableAccessGroupRole.DataSource = dtb;
            TableAccessGroupRole.DataBind();

            TableAccessGroupRole.Columns[1].Visible = false;
            TableAccessGroupRole.Columns[2].Visible = false;
            TableAccessGroupRole.Columns[7].Visible = false;

            TableAccessGroupRole.UseAccessibleHeader = true;
            TableAccessGroupRole.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void TableUserRegister_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableUserRegister_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDetailEmployee();
            hlbNIK.Value = Session["nik_detail"].ToString();
        }

        protected void ddlUserAccesss_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GetDetailEmployee()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "GetDetailEmployees");
            sqlcomm.Parameters.AddWithValue("@id", ddlEmployees.SelectedValue);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("nik_detail", (string)dr["nik_detail"]);
                Session.Add("fullname_detail", (string)dr["fullname_detail"]);
                //Session.Add("training_title", (string)dr["training_title"]);
                //Session.Add("training_start_date", (DateTime)dr["training_start_date"]);
                //Session.Add("training_end_date", (DateTime)dr["training_end_date"]);
                //Session.Add("trainer_type", (string)dr["trainer_type"]);
                //Session.Add("Vendor Trainer", (string)dr["Vendor Trainer"].ToString());
                //Session.Add("Internal Trainer", (string)dr["Internal Trainer"].ToString());
                //Session.Add("location", (string)dr["location"]);
                //Session.Add("method", (string)dr["method"]);
                //Session.Add("certificate", (string)dr["certificate"]);
                //Session.Add("training_type", (string)dr["training_type"]);
                //Session.Add("regulation", (string)dr["regulation"].ToString());
                //Session.Add("government_institution", (string)dr["government_institution"].ToString());
                //Session.Add("validity_period", (string)dr["validity_period"].ToString());
                //Session.Add("cost", (string)dr["cost"]);
                //Session.Add("status", (string)dr["status"]);
                //Session.Add("createby", (string)dr["createby"]);
                //Session.Add("create_date", (DateTime)dr["create_date"]);
                //Session.Add("modifiedby", (string)dr["modifiedby"]);
                //Session.Add("modified_date", (DateTime)dr["modified_date"]);
            }
            else
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Value != txtConfirmationPassword.Value)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "ErorrPasswordDoesntMacth();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewUser').modal();", true);
                GetDataRegister();
            }
            else if (ddlEmployees.SelectedItem.Text == "<Select Name>")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectEmployee();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewUser').modal();", true);
                GetDataRegister();
            }
            else if (txtPassword.Value=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "passwordfield();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewUser').modal();", true);
                GetDataRegister();
            }
            else if (txtConfirmationPassword.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "Confirmpasswordfield();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewUser').modal();", true);
                GetDataRegister();
            }
            else
            {
                
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@CreateBy", Session["nik"].ToString());
                sqlcomm.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@ModifiedBy", Session["nik"].ToString());
                sqlcomm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@Employees", ddlEmployees.SelectedValue);
                sqlcomm.Parameters.AddWithValue("@IsActive", ckActive.Checked);
                sqlcomm.Parameters.AddWithValue("@UserName", txtUsername.Value.ToString().Trim());
                sqlcomm.Parameters.AddWithValue("@StoredPassword", Encrypt(txtPassword.Value.Trim()));
                sqlcomm.Parameters.AddWithValue("@UserID_AD", "YLID-" + hlbNIK.Value.ToString());
                //sqlcomm.Parameters.AddWithValue("@nik", ddlEmployees.SelectedValue);
                //sqlcomm.Parameters.AddWithValue("@password", Encrypt(txtPassword.Value.Trim()));
                //sqlcomm.Parameters.AddWithValue("@username_AD", "YLID-" + ddlEmployees.SelectedValue);
                //sqlcomm.Parameters.AddWithValue("@username", txtUsername.Value.ToString().Trim());
                //sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);

                sqlcomm.ExecuteNonQuery();
                Con.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                GetDataRegister();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewUser').modal();", true);
            GetDataRegister();
            GetEmployees();
           
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
            sqlcomm.Parameters.AddWithValue("@Oid", hlbOid.Value);
            sqlcomm.Parameters.AddWithValue("@IsActive", ckActiveUpdate.Checked);

            sqlcomm.ExecuteNonQuery();
            Con.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
            GetDataRegister();
        }

        protected void ddlUserAccesssEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);

            GetDataRegister();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            txtFullname.Value = row.Cells[2].Text.ToString();
            txtNIK.Value = row.Cells[1].Text.ToString();
            txtUsernameEdit.Value = row.Cells[4].Text.ToString();
            hlbOid.Value = row.Cells[8].Text.ToString();


            ckActiveUpdate.Checked = Convert.ToBoolean(row.Cells[7].Text.ToString());
            GetDataUserAccessGroupRole();
        }

        protected void btnCloseModalView_Click(object sender, EventArgs e)
        {
            Response.Redirect("registration.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);
            GetDataRegister();
            GetDataUserAccessGroupRole();
            btnUpdate.Visible = true;
            ckActiveUpdate.Enabled = true;
        }

        protected void btnAccessRole_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlGroupAccessRole').modal();", true);
            GetGroupAccess();
            GetDataRegister();
            GetDataUserAccessGroupRole();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GetDataRegister();
            
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_AccessGroup";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Delete");
            sqlcomm.Parameters.AddWithValue("@Oid", row.Cells[1].Text.ToString());

            sqlcomm.ExecuteNonQuery();
            Con.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncRemoveGroup();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);
            GetGroupAccess();
            GetDataUserAccessGroupRole();
        }

        protected void TableAccessGroupRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableAccessGroupRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlGroupAccess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmitGroup_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_AccessGroup";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
            sqlcomm.Parameters.AddWithValue("@CreateBy", Session["nik"].ToString());
            sqlcomm.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString());
            sqlcomm.Parameters.AddWithValue("@ModifiedBy", Session["nik"].ToString());
            sqlcomm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
            sqlcomm.Parameters.AddWithValue("@Oid_UserManagement", hlbOid.Value);
            sqlcomm.Parameters.AddWithValue("@GroupName", ddlGroupAccess.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@Oid_UserManagement_Group", ddlGroupAccess.SelectedValue);

            sqlcomm.ExecuteNonQuery();
            Con.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSubmitGroup();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);
            GetDataRegister();
            GetDataUserAccessGroupRole();
        }

        protected void btnCloseModalGroupAccess_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);
            GetDataRegister();
            GetDataUserAccessGroupRole();
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("registration.aspx");
        }
    }
}