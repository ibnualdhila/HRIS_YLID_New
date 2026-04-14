using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class branch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["nik"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                GetDataBranch();
                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();
                btnNew.Visible = true;
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                btnRefresh.Visible = true;
            }
        }

        protected void GetDataBranch()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Branch";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableBranch.DataSource = dtb;
            TableBranch.DataBind();

            TableBranch.Columns[1].Visible = false;

            TableBranch.UseAccessibleHeader = true;
            TableBranch.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        public bool CheckBranchNameDuplicate(string branch_name)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "SELECT branch_name FROM Branch WHERE branch_name=@branch_name";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@branch_name", branch_name);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("branch_name", (string)dr["branch_name"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckBranchCodeDuplicate(string code_branch)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "SELECT code_branch FROM Branch WHERE code_branch=@code_branch";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@code_branch", code_branch);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("code_branch", (string)dr["code_branch"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isbranch_name_duplicate = CheckBranchNameDuplicate(txtBranchName.Value);
            bool isbranch_code_duplicate = CheckBranchCodeDuplicate(txtBranchCode.Value);

            if (isbranch_name_duplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateDataBranchName();", true);
            }
            else if (isbranch_code_duplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateDataBranchCode();", true);
            }
            else
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Branch";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@code_branch", txtBranchCode.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@branch_name", txtBranchName.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
                sqlcomm.Parameters.AddWithValue("@createby", txtCreateBy.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

                sqlcomm.ExecuteNonQuery();
                GetDataBranch();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                Con.Close();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                btnRefresh.Visible = true;
            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Branch";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
            sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@code_branch", txtBranchCode.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@branch_name", txtBranchName.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
            sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

            sqlcomm.ExecuteNonQuery();
            GetDataBranch();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
            Con.Close();
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            btnRefresh.Visible = true;

        }

        protected void TableBranch_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

       

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
            btnRefresh.Visible = true;
            hlbID.Value = row.Cells[1].Text.ToString();
            txtBranchCode.Value = row.Cells[2].Text.ToString();
            txtBranchName.Value = row.Cells[3].Text.ToString();
            ckActive.Checked = Convert.ToBoolean(row.Cells[4].Text.ToString());
            txtCreateBy.Value = row.Cells[5].Text.ToString();
            txtCreateDate.Value = row.Cells[6].Text.ToString();
            txtModifiedBy.Value = Session["Fullname"].ToString();
            txtModifiedDate.Value = DateTime.Now.ToString();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("branch.aspx");
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewLocation').modal();", true);
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            btnRefresh.Visible = true;
            GetDataBranch();
        }

    }
}