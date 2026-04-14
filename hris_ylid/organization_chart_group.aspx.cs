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
    public partial class organization_chart_group : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                GetDataGroup();
                btnRemove_GroupAccess.Attributes.Add("onclick", "javascript:return DeleteConfirm()");
            }
        }

        protected void GetDataGroup()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableGroup.DataSource = dtb;
            TableGroup.DataBind();

            TableGroup.Columns[2].Visible = false;
            TableGroup.Columns[3].Visible = false;
            TableGroup.Columns[11].Visible = false;

            TableGroup.UseAccessibleHeader = true;
            TableGroup.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewGroup').modal();", true);
            GetDataGroup();
            GetDivisionName();
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            int a;
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string query = "select COUNT(Code) from UserManagement_ChartGroup";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    hlbCode.Value = "Group01";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    hlbCode.Value = "Group0" + a.ToString();
                }
            }
        }

        protected void TableGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewGroup').modal();", true);
            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
            GetDataGroup();
            GetDivisionName();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            hlbCode.Value = row.Cells[3].Text.ToString();
            txtGroupName.Value = row.Cells[4].Text.ToString();
            hlbOid.Value = row.Cells[2].Text.ToString();
            ckActive.Checked = Convert.ToBoolean(row.Cells[6].Text.ToString());
            ddlDivision.SelectedItem.Text = row.Cells[5].Text;
            ddlDivision.SelectedValue = row.Cells[11].Text;
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("organization_chart_group.aspx");
        }

        public bool CheckGroupNameDuplicate(string group_name)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "SELECT GroupName FROM UserManagement_ChartGroup WHERE GroupName=@group_name";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@group_name", group_name);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("GroupName", (string)dr["GroupName"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isexistsduplicate = CheckGroupNameDuplicate(txtGroupName.Value);

            if (txtGroupName.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "ErorrEmptyGroupname();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewGroup').modal();", true);
                GetDataGroup();
                GetDivisionName();
            }
            else if (isexistsduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateGroupName();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewGroup').modal();", true);
                GetDataGroup();
                GetDivisionName();
            }
            else if (ddlDivision.SelectedItem.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DivisionEmpty();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewGroup').modal();", true);
                GetDataGroup();
                GetDivisionName();
            }
            else
            {

                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@CreateBy", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@ModifiedBy", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@Code", hlbCode.Value);
                sqlcomm.Parameters.AddWithValue("@GroupName", txtGroupName.Value);
                sqlcomm.Parameters.AddWithValue("@Division", ddlDivision.SelectedValue);
                sqlcomm.Parameters.AddWithValue("@IsActive", ckActive.Checked);

                sqlcomm.ExecuteNonQuery();
                Con.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                GetDataGroup();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
            sqlcomm.Parameters.AddWithValue("@Oid", hlbOid.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@ModifiedBy", Session["Fullname"].ToString());
            sqlcomm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
            sqlcomm.Parameters.AddWithValue("@GroupName", txtGroupName.Value);
            sqlcomm.Parameters.AddWithValue("@IsActive", ckActive.Checked);
            sqlcomm.Parameters.AddWithValue("@Division", ddlDivision.SelectedValue);

            sqlcomm.ExecuteNonQuery();
            Con.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
            GetDataGroup();
        }

        protected void btnShowAccessDetail_Click(object sender, EventArgs e)
        {
            btnSubmit_GroupAccess.Visible = true;
            btnUpdate_GroupAccess.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewNavigationRole').modal();", true);
            GetDataGroup();
            
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            txtGroupName_Navigation.Value = row.Cells[4].Text.ToString();
            hlbOid.Value = row.Cells[2].Text.ToString();
            GetDataGroupAccessDetail();
            GetSubSectionName();
            GetSectionName();
        }

        protected void btnCloseModalNew_Navigation_Click(object sender, EventArgs e)
        {
            Response.Redirect("organization_chart_group.aspx");
        }

        protected void GetSectionName()
        {
            ddlSectionName.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddSectionName");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlSectionName.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["section"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlSectionName.Items.Add(newItem);
                }
                dr.Close();
            }
            catch (Exception err)
            {
                string _ErrorMsg = err.Message;
            }
            finally
            {
                Con.Close();
            }
        }

        protected void GetSubSectionName()
        {
            ddlSubSectionName.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddSubSectionName");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlSubSectionName.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["sub_section"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlSubSectionName.Items.Add(newItem);
                }
                dr.Close();
            }
            catch (Exception err)
            {
                string _ErrorMsg = err.Message;
            }
            finally
            {
                Con.Close();
            }
        }

        protected void GetDataGroupAccessDetail()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup_Detail";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            sqlcomm.Parameters.AddWithValue("@UserManagement_ChartGroup", hlbOid.Value);
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableRoleGroupAccess.DataSource = dtb;
            TableRoleGroupAccess.DataBind();

            TableRoleGroupAccess.Columns[1].Visible = false;
            TableRoleGroupAccess.Columns[6].Visible = false;
            TableRoleGroupAccess.Columns[7].Visible = false;

            TableRoleGroupAccess.UseAccessibleHeader = true;
            TableRoleGroupAccess.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void ddlSectionName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool CheckGroupAccessDuplicate(string section, string subsection, string group)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup_Detail";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "CheckDuplicateAccess");
            sqlcomm.Parameters.AddWithValue("@SubSection", section); 
            sqlcomm.Parameters.AddWithValue("@Section", subsection);
            sqlcomm.Parameters.AddWithValue("@UserManagement_ChartGroup", group);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {
                Session.Add("Section", (Guid)dr["Section"]);
                Session.Add("SubSection", (Guid)dr["SubSection"]);
                Session.Add("UserManagement_ChartGroup", (Guid)dr["UserManagement_ChartGroup"]);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSubmit_GroupAccess_Click(object sender, EventArgs e)
        {
            bool isexistsduplicate = CheckGroupAccessDuplicate(ddlSectionName.SelectedValue, ddlSubSectionName.SelectedValue, hlbOid.Value);
            if (isexistsduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "Savedfailed();", true);
            }
            else if (ddlSubSectionName.SelectedValue == "00000000-0000-0000-0000-000000000000")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "SelectModule();", true);
            }
            else
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup_Detail";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@CreateBy", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@ModifiedBy", Session["Fullname"].ToString());
                sqlcomm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
                sqlcomm.Parameters.AddWithValue("@SubSection", ddlSubSectionName.SelectedValue);
                sqlcomm.Parameters.AddWithValue("@Section", ddlSectionName.SelectedValue);
                sqlcomm.Parameters.AddWithValue("@UserManagement_ChartGroup", hlbOid.Value);
                sqlcomm.Parameters.AddWithValue("@IsActive", ckActive_Status.Checked);

                sqlcomm.ExecuteNonQuery();
                Con.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSaveNavigation();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewNavigationRole').modal();", true);
                GetDataGroupAccessDetail();
                GetSubSectionName();
                GetSectionName();
                GetDataGroup();
            }
        }

        protected void btnUpdate_GroupAccess_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup_Detail";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
            sqlcomm.Parameters.AddWithValue("@ModifiedBy", Session["Fullname"].ToString());
            sqlcomm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
            sqlcomm.Parameters.AddWithValue("@Oid", hlbOidModule.Value);
            sqlcomm.Parameters.AddWithValue("@SubSection", ddlSubSectionName.SelectedValue);
            sqlcomm.Parameters.AddWithValue("@Section", ddlSectionName.SelectedValue);
            sqlcomm.Parameters.AddWithValue("@IsActive", ckActive_Status.Checked);

            sqlcomm.ExecuteNonQuery();
            Con.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdateNavigation();", true);
        }

        protected void DeleteRecord(string oid)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_UserManagement_ChartGroup_Detail";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Delete");
            sqlcomm.Parameters.AddWithValue("@Oid", oid);

            sqlcomm.ExecuteNonQuery();
            Con.Close();
        }

        protected void btnRemove_GroupAccess_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow grow in TableRoleGroupAccess.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkSelect");
                if (chkdel.Checked)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                foreach (GridViewRow grow in TableRoleGroupAccess.Rows)
                {
                    CheckBox chkdel = (CheckBox)grow.FindControl("chkSelect");
                    if (chkdel.Checked)
                    {
                        string oid = grow.Cells[1].Text;
                        DeleteRecord(oid);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncDeleteNavigation();", true);
                    }
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "Deletefailed();", true);
            }
        }

        protected void TableRoleGroupAccess_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableRoleGroupAccess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEditSection_Click(object sender, EventArgs e)
        {
            btnSubmit_GroupAccess.Visible = false;
            btnUpdate_GroupAccess.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewNavigationRole').modal();", true);
            GetDataGroup();
            GetSubSectionName();
            GetSectionName();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            hlbOidModule.Value = row.Cells[1].Text.ToString();

            if (row.Cells[3].Text != "&nbsp;")
            {
                ddlSectionName.SelectedItem.Text = row.Cells[3].Text;
            }
            else
            {
                ddlSectionName.SelectedItem.Text = "";
            }
            if (row.Cells[6].Text != "&nbsp;")
            {
                ddlSectionName.SelectedValue = row.Cells[6].Text;
            }
            else
            {
                ddlSectionName.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }

            if (row.Cells[4].Text != "&nbsp;")
            {
                ddlSubSectionName.SelectedItem.Text = row.Cells[4].Text;
            }
            else
            {
                ddlSubSectionName.SelectedItem.Text = "";
            }
            if (row.Cells[7].Text != "&nbsp;")
            {
                ddlSubSectionName.SelectedValue = row.Cells[7].Text;
            }
            else
            {
                ddlSubSectionName.SelectedValue = "00000000-0000-0000-0000-000000000000";
            }

            ckActive_Status.Checked = Convert.ToBoolean(row.Cells[5].Text.ToString());
            GetDataGroupAccessDetail();
        }

        protected void GetDivisionName()
        {
            ddlDivision.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddDivisionName");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlDivision.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["division_name"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlDivision.Items.Add(newItem);
                }
                dr.Close();
            }
            catch (Exception err)
            {
                string _ErrorMsg = err.Message;
            }
            finally
            {
                Con.Close();
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSubSectionName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}