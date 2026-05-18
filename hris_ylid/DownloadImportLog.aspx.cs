using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Drawing.Charts;
//using DocumentFormat.OpenXml.Vml.Office;
using Newtonsoft.Json;
//using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hris_ylid
{
    public partial class DownloadImportLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ImportLog"] != null)
            {
                DataTable dtLog = (DataTable)Session["ImportLog"];

                ExportImportLog(dtLog, "ImportLog.xlsx");

                Session.Remove("ImportLog");
            }
            else if (Session["ExportMasterEmployee"] != null)
            {
                
               
                DataSet ds = (DataSet)Session["ExportMasterEmployee"];

                ExportMasterEmployee(ds);

                Session.Remove("ExportMasterEmployee");
                
            }



        }

        private void ExportImportLog(DataTable dtLog, string fileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws =  wb.Worksheets.Add(dtLog, "Import Log");
                ws.Columns().AdjustToContents();
                Response.Clear();
                Response.Buffer = true;

                //Response.ContentType ="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Charset = "";
                Response.ClearContent();

                Response.AddHeader(
                    "content-disposition",
                    $"attachment;filename={fileName}"
                );
                Response.ContentType = "application/excel";
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    //Response.End();
                }

                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

        }

        private void ExportMasterEmployee(DataSet ds)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "Employee detail";
                    var ws = wb.Worksheets.Add(ds.Tables[0]);

                    ws.Columns().AdjustToContents();
                }

                if (ds.Tables.Count > 1)
                {
                    ds.Tables[1].TableName = "Personal information";
                    var ws = wb.Worksheets.Add(ds.Tables[1]);

                    ws.Columns().AdjustToContents();
                }

                if (ds.Tables.Count > 2)
                {
                    ds.Tables[2].TableName = "Education information";
                    var ws = wb.Worksheets.Add(ds.Tables[2]);

                    ws.Columns().AdjustToContents();
                }

                if (ds.Tables.Count > 3)
                {
                    ds.Tables[3].TableName = "Family information";
                    var ws = wb.Worksheets.Add(ds.Tables[3]);

                    ws.Columns().AdjustToContents();
                }

                if (ds.Tables.Count > 4)
                {
                    ds.Tables[4].TableName =
                        "Emergency contact information";

                    var ws = wb.Worksheets.Add(ds.Tables[4]);

                    ws.Columns().AdjustToContents();
                }

                if (ds.Tables.Count > 5)
                {
                    ds.Tables[5].TableName =
                        "Career history";

                    var ws = wb.Worksheets.Add(ds.Tables[5]);

                    ws.Columns().AdjustToContents();
                }

                if (ds.Tables.Count > 6)
                {
                    ds.Tables[6].TableName =
                        "Warning letter history";

                    var ws = wb.Worksheets.Add(ds.Tables[6]);

                    ws.Columns().AdjustToContents();
                }

                string fileName = "EmployeeExport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.AddHeader(
                    "content-disposition",
                    "attachment;filename=" + fileName
                );
                Response.ContentType = "application/excel";
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.Position = 0;
                    ms.CopyTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }




    }
}