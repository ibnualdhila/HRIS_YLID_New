using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class organization_chart_head : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                GetDataOrganizationChartHead();
            }
        }

        protected void GetDataOrganizationChartHead()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewUserManagementSuperior");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableOrganizationChartHead.DataSource = dtb;
            TableOrganizationChartHead.DataBind();

            TableOrganizationChartHead.Columns[1].Visible = false;
            //TableOrganizationChartHead.Columns[8].Visible = false;

            TableOrganizationChartHead.UseAccessibleHeader = true;
            TableOrganizationChartHead.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetDataAccessGroupOrganization()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup_Detail";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewOrganizationAccess");
            sqlcomm.Parameters.AddWithValue("@Oid", hlbOid.Value);
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableOrganizationAccess.DataSource = dtb;
            TableOrganizationAccess.DataBind();

            TableOrganizationAccess.Columns[1].Visible = false;
            //TableOrganizationChartHead.Columns[8].Visible = false;

            TableOrganizationAccess.UseAccessibleHeader = true;
            TableOrganizationAccess.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hlbOid.Value = row.Cells[1].Text.ToString();
            txtNIK.Value = row.Cells[2].Text.ToString();
            txtFullname.Value = row.Cells[3].Text.ToString();
            txtPosition.Value = row.Cells[4].Text.ToString();
            txtSection.Value = row.Cells[6].Text.ToString();
            GetDataOrganizationChartHead();
            GetDataAccessGroupOrganization();
        }

        protected void GetGroupAccess()
        {
            ddlGroupAccess.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup";
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlGroupAccessRole').modal();", true);
            GetDataOrganizationChartHead();
            GetDataAccessGroupOrganization();
            GetGroupAccess();
        }

        protected void btnCloseModalView_Click(object sender, EventArgs e)
        {
            Response.Redirect("organization_chart_head.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GetDataOrganizationChartHead();

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_OrganizationChartGroup";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Delete");
            sqlcomm.Parameters.AddWithValue("@Oid", row.Cells[1].Text.ToString());

            sqlcomm.ExecuteNonQuery();
            Con.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncRemoveGroup();", true);
            GetGroupAccess();
            GetDataAccessGroupOrganization();
        }

        protected void ddlGroupAccess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool CheckGroupAccessDuplicate(string UserManagement, string group)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_OrganizationChartGroup";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckDuplicateAccess");
            sqlcomm.Parameters.AddWithValue("@UserManagement", UserManagement);
            sqlcomm.Parameters.AddWithValue("@UserManagement_ChartGroup", group);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("UserManagement", (Guid)dr["UserManagement"]);
                Session.Add("UserManagement_ChartGroup", (Guid)dr["UserManagement_ChartGroup"]);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSubmitGroup_Click(object sender, EventArgs e)
        {
            bool isexistsduplicate = CheckGroupAccessDuplicate(hlbOid.Value,ddlGroupAccess.SelectedValue);
            if (isexistsduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "Savedfailed();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);
                GetDataOrganizationChartHead();
                GetDataAccessGroupOrganization();
                GetGroupAccess();
            }
            else if (ddlGroupAccess.SelectedValue == "00000000-0000-0000-0000-000000000000")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectModule();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);
                GetDataOrganizationChartHead();
                GetDataAccessGroupOrganization();
                GetGroupAccess();
            }
            else
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_OrganizationChartGroup";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@CreateBy", Session["nik"].ToString());
                sqlcomm.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@ModifiedBy", Session["nik"].ToString());
                sqlcomm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@UserManagement", hlbOid.Value);
                sqlcomm.Parameters.AddWithValue("@UserManagement_ChartGroup", ddlGroupAccess.SelectedValue);

                sqlcomm.ExecuteNonQuery();
                Con.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSubmitGroup();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlViews').modal();", true);
                GetDataOrganizationChartHead();
                GetDataAccessGroupOrganization();
                GetGroupAccess();
            }
            
        }

        protected void btnCloseModalGroupAccess_Click(object sender, EventArgs e)
        {
            Response.Redirect("organization_chart_head.aspx");
        }
    }
}