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
    public partial class division : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
            {
                Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                GetDataDivision();
                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;

            }
        }

        protected void GetDataDivision()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Division";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "View");
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableDivision.DataSource = dtb;
            TableDivision.DataBind();

            TableDivision.Columns[1].Visible = false;

            TableDivision.UseAccessibleHeader = true;
            TableDivision.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void TableDivision_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public bool CheckDivisionNameDuplicate(string div_name)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            string sqlquery = "SELECT division_name FROM Division WHERE division_name=@div_name";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, Con);
            sqlcomm.Parameters.AddWithValue("@div_name", div_name);

            SqlDataReader dr = null;
            dr = sqlcomm.ExecuteReader();

            if (dr.Read())
            {

                Session.Add("division_name", (string)dr["division_name"]);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isexistsduplicate = CheckDivisionNameDuplicate(txtDivName.Value);

            if (isexistsduplicate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DuplicateData();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewDivision').modal();", true);
                GetDataDivision();
            }
            else if (txtDivName.Value=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "DivisionEmpty();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewDivision').modal();", true);
                GetDataDivision();
            }
            else
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Division";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                sqlcomm.Parameters.AddWithValue("@division_name", txtDivName.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
                sqlcomm.Parameters.AddWithValue("@createby", txtCreateBy.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@create_date", txtCreateDate.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
                sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

                sqlcomm.ExecuteNonQuery();

                GetDataDivision();
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
            sqlcomm.CommandText = "sp_HRIS_DB_Division";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Update");
            sqlcomm.Parameters.AddWithValue("@id", hlbID.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@division_name", txtDivName.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@active", ckActive.Checked);
            sqlcomm.Parameters.AddWithValue("@modifiedby", txtModifiedBy.Value.Trim());
            sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value.Trim());

            sqlcomm.ExecuteNonQuery();
            GetDataDivision();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FuncUpdate();", true);
            Con.Close();
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewDivision').modal();", true);
            GetDataDivision();

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            btnSubmit.Visible = false;
            btnUpdate.Visible = true;


            hlbID.Value = row.Cells[1].Text.ToString();
            txtDivName.Value = row.Cells[2].Text.ToString();
            ckActive.Checked = Convert.ToBoolean(row.Cells[3].Text.ToString());
            txtCreateBy.Value = row.Cells[4].Text.ToString();
            txtCreateDate.Value = row.Cells[5].Text.ToString();
            txtModifiedBy.Value = Session["Fullname"].ToString();
            txtModifiedDate.Value = DateTime.Now.ToString();

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlAddNewDivision').modal();", true);
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnCloseModalNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("division.aspx");
        }
    }
}