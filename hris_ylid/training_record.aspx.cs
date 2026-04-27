using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class training_record : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
                {
                    Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }

                //GetDataTrainingRecord();
                GetTrainingCode();
                
            }
        }

        protected void GetTrainingCode()
        {
            ddlTrainingCode.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "GetTrainingCode");

            SqlDataReader dr;

            try
            {

                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "0";
                ddlTrainingCode.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["training_code"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlTrainingCode.Items.Add(newItem);
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

        protected void GetDataTrainingRecord()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewTrainingRecord");
            sqlcomm.Parameters.AddWithValue("@training_code", ddlTrainingCode.SelectedItem.Text.ToString());
            //sqlcomm.Parameters.AddWithValue("@training_start_date", txtTrainingStartDate.Value.ToString());
            //sqlcomm.Parameters.AddWithValue("@training_end_date", txtTrainingEndDate.Value.ToString());
            //sqlcomm.Parameters.AddWithValue("@training_type", ddlTrainingType.SelectedItem.Text.ToString());
            //sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerType.SelectedItem.Text.ToString());
            //sqlcomm.Parameters.AddWithValue("@method", ddlMethod.SelectedItem.Text.ToString());
            //sqlcomm.Parameters.AddWithValue("@training_status", ddlTrainingStatus.SelectedItem.Text.ToString());
            //sqlcomm.Parameters.AddWithValue("@training_result", ddlTrainingResult.SelectedItem.Text.ToString());
            //sqlcomm.Parameters.AddWithValue("@Participant", txtParticipant.Value.ToString());

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableIndividualTraining.DataSource = dtb;
            TableIndividualTraining.DataBind();

            //TableIndividualTraining.Columns[0].Visible = false;
            //TableParticipant.Columns[2].Visible = false;


            TableIndividualTraining.UseAccessibleHeader = true;
            TableIndividualTraining.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetDataTrainingRecordALL()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewTrainingRecordAll");
            
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableIndividualTraining.DataSource = dtb;
            TableIndividualTraining.DataBind();

            //TableIndividualTraining.Columns[0].Visible = false;
            //TableParticipant.Columns[2].Visible = false;


            TableIndividualTraining.UseAccessibleHeader = true;
            TableIndividualTraining.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void GetDataTrainingRecordALLWithDateRange()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewTrainingRecordAllWithRangeDate");
            //sqlcomm.Parameters.AddWithValue("@training_start_date", txtTrainingStartDate.Value);
            //sqlcomm.Parameters.AddWithValue("@training_end_date", txtTrainingEndDate.Value);
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableIndividualTraining.DataSource = dtb;
            TableIndividualTraining.DataBind();

            //TableIndividualTraining.Columns[0].Visible = false;
            //TableParticipant.Columns[2].Visible = false;


            TableIndividualTraining.UseAccessibleHeader = true;
            TableIndividualTraining.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void ddlTrainingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTrainingResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTrainingCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTrainingType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTrainerType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GetDataTrainingRecordFilter()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewTrainingRecordFilter");
            sqlcomm.Parameters.AddWithValue("@training_code", ddlTrainingCode.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@training_title", txtTrainingTitle.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerOrganizer.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@Participant", txtParticipant.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@nik_participant", txtNIK.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@training_status", ddlStatus.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@training_result", ddlTrainingResult.SelectedItem.Text.ToString());

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            TableIndividualTraining.DataSource = dtb;
            TableIndividualTraining.DataBind();

            //TableIndividualTraining.Columns[0].Visible = false;
            //TableParticipant.Columns[2].Visible = false;


            TableIndividualTraining.UseAccessibleHeader = true;
            TableIndividualTraining.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetDataTrainingRecordFilter();
            btnGenExcel.Visible = true;
            //if (ddlTrainingCode.SelectedItem.Text.ToString() == "<All>")
            //{
            //    GetDataTrainingRecordALL();
            //    btnGenExcel.Visible = true;
            //}
            //else
            //{
            //    GetDataTrainingRecord();
            //    btnGenExcel.Visible = true;
            //}
        }

        protected void btnGenExcelNew_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewTrainingRecordFilter");
            sqlcomm.Parameters.AddWithValue("@training_code", ddlTrainingCode.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@training_title", txtTrainingTitle.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerOrganizer.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@Participant", txtParticipant.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@nik_participant", txtNIK.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@training_status", ddlStatus.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@training_result", ddlTrainingResult.SelectedItem.Text.ToString());

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            using (DataTable dt = new DataTable())
            {
                //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                //sFileName = sFileName.Replace("/", "");
                sda.Fill(dt);
                string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Training Record");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Training Record" + "_" + _vDate1 + ".xlsx");
                    Response.ContentType = "application/excel";
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }

        protected void TableIndividualTraining_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableIndividualTraining_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void TableIndividualTraining_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGenExcel_Click(object sender, EventArgs e)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_IndividualTraining";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewTrainingRecordFilter");
            sqlcomm.Parameters.AddWithValue("@training_code", ddlTrainingCode.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@training_title", txtTrainingTitle.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@trainer_type", ddlTrainerOrganizer.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@Participant", txtParticipant.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@nik_participant", txtNIK.Value.ToString());
            sqlcomm.Parameters.AddWithValue("@training_status", ddlStatus.SelectedItem.Text.ToString());
            sqlcomm.Parameters.AddWithValue("@training_result", ddlTrainingResult.SelectedItem.Text.ToString());

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            using (DataTable dt = new DataTable())
            {
                //string sFileName = "JobListExport-" + System.DateTime.Now.Date + ".xlsx";
                //sFileName = sFileName.Replace("/", "");
                sda.Fill(dt);
                string _vDate1 = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Training Record");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment; filename=JoblistExport.xlsx");
                    Response.ClearContent();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Training Record" + "_" + _vDate1 + ".xlsx");
                    Response.ContentType = "application/excel";
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTrainerOrganizer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTrainingResult_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }


    }
}