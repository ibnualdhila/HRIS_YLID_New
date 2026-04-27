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
    public partial class subsection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
                {
                    Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }

                GetDataSubSection();
                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();
                btnNew.Visible = true;
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                
            }
        }

        protected void GetDataSubSection()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_SubSection";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableSubSection.DataSource = dtb;
            TableSubSection.DataBind();

            TableSubSection.Columns[1].Visible = false;

            TableSubSection.UseAccessibleHeader = true;
            TableSubSection.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        public bool CheckSubSectionNameDuplicate(string subsection_name)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "SELECT sub_section FROM SubSection WHERE sub_section=@sub_section";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@sub_section", subsection_name);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("sub_section", (string)dr["sub_section"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isexistsduplicate = CheckSubSectionNameDuplicate(txtSubSectionName.Value);

            if (isexistsduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateData();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewSubsection').modal();", true);
                GetDataSubSection();
            }
            else if (txtSubSectionName.Value == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "LocationEmpty();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewSubsection').modal();", true);
                GetDataSubSection();
            }
            else
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_SubSection";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@sub_section", txtSubSectionName.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
                sqlcomm.Parameters.AddWithValue("@createby", txtCreateBy.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

                sqlcomm.ExecuteNonQuery();
                GetDataSubSection();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncSave();", true);
                Con.Close();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
               
            }
                
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_SubSection";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
            sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@sub_section", txtSubSectionName.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
            sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

            sqlcomm.ExecuteNonQuery();
            GetDataSubSection();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
            Con.Close();
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            
        }

        protected void TableSubSection_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableSubSection_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("subsection.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewSubsection').modal();", true);
            GetDataSubSection();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
            
            hlbID.Value = row.Cells[1].Text.ToString();
            txtSubSectionName.Value = row.Cells[2].Text.ToString();
            ckActive.Checked = Convert.ToBoolean(row.Cells[3].Text.ToString());
            txtCreateBy.Value = row.Cells[4].Text.ToString();
            txtCreateDate.Value = row.Cells[5].Text.ToString();
            txtModifiedBy.Value = Session["Fullname"].ToString();
            txtModifiedDate.Value = DateTime.Now.ToString();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewSubsection').modal();", true);
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("subsection.aspx");
        }
    }
}