using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;


//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Drawing.Charts;
//using DocumentFormat.OpenXml.Vml.Office;
//using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
//using Stripe;

//using Stripe;
using System;
using System.CodeDom;
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
using static hris_ylid.employeess;

namespace hris_ylid
{
    public partial class employeess_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["Fullname"])))
                {
                    Response.Redirect("login.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                }

                GetPositionName();
                GetDivisionName();
                GetSectionName();
                GetSubSectionName();
                GetReportPositionName();
                GetLocation();
                GetInstitutionName();
                GetCity();
                GetProvince();
                GetNationality();



                // BindDataTableCandidateJoined();

                lblEmployeeID.Value = Request.QueryString["EmployeeID"];
                lblNIK.Value = Request.QueryString["NIK"];

                lblNewEmployee.Value = Request.QueryString["NewEmployee"];

                if (lblEmployeeID.Value.Length > 0)
                {
                    lblFlagSave.Value = "Edit Employee Information";
                    btnUpdate.Text = "Update Data";

                }
                else
                {
                    lblFlagSave.Value = "New Employee Information";
                    btnUpdate.Text = "Save Data";
                }

                txtEditHeaderEmployeeDetail.Text = lblFlagSave.Value;
                txtEditHeaderEmployeeEducation.Text = lblFlagSave.Value;
                txtEditHeaderEmployeeFamilyInfo.Text = lblFlagSave.Value;
                txtEditHeaderEmployeePersonalInfo.Text = lblFlagSave.Value;
                txtEditHeaderCareerHistory.Text = lblFlagSave.Value;
                txtEditHeaderEmergencyContact.Text = lblFlagSave.Value;
                txtEditHeaderWarningLetters.Text = "Warning Letters";

                txtCreateBy.Value = Session["Fullname"].ToString();
                txtCreateDate.Value = DateTime.Now.ToString();
                txtModifiedBy.Value = Session["Fullname"].ToString();
                txtModifiedDate.Value = DateTime.Now.ToString();

                if (lblNewEmployee.Value.ToString().ToLower() == "true")
                {
                    phAddButton.Visible = true;
                    txtFullname.Disabled = false;
                    txtNIK.Disabled = false;
                }
                else
                {
                    SetEdit();
                    ViewEdocsFilesUpload();
                    phAddButton.Visible = false;
                }
            }
        }

        private void SetEdit()
        {
            BindDataEmployeeDetail();
        }

        private void BindDataEmployeeDetail()
        {
            if (lblEmployeeID.Value.Length > 0)
            {
                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Employees";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                sqlcomm.Parameters.AddWithValue("@StatementType", "ViewPersonal");
                sqlcomm.Parameters.AddWithValue("@id", lblEmployeeID.Value);// row.Cells[2].Text);
                DataTable dtb1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(sqlcomm);
                sda1.Fill(dtb1);

                sqlcomm.Dispose();
                Con.Close();

                foreach (DataRow dr in dtb1.Rows)
                {
                    txtNIK.Value = dr["nik"].ToString();

                    //ViewEdocsFilesUpload();

                    txtFullname.Value = dr["fullname"].ToString();
                    txtJoinDate.Value = dr["join_date"].ToString();
                    txtBirthDate.Value = dr["date_of_birth"].ToString();
                    if (dr["category"].ToString() == "OUTSOURCED")//row.Cells[4].Text == "OUTSOURCED")
                    {
                        divIntitution.Visible = true;
                    }
                    else
                    {
                        divIntitution.Visible = false;
                    }

                    if (dr["institution_name"] != null && dr["institution_name"].ToString() != "&nbsp;" && dr["institution_name"].ToString().Length > 0) //row.Cells[31].Text != "&nbsp;")
                    {
                        ddlInstitution.SelectedItem.Text = dr["institution_name"].ToString();//row.Cells[31].Text;
                        divIntitution.Visible = true;
                    }
                    else
                    {
                        ddlInstitution.SelectedItem.Text = "";
                        divIntitution.Visible = false;
                    }

                    if (dr["id_institution"] != null && dr["id_institution"].ToString() != "&nbsp;" && dr["id_institution"].ToString().Length > 0) // row.Cells[30].Text != "&nbsp;")
                    {
                        ddlInstitution.SelectedValue = dr["id_institution"].ToString();//row.Cells[30].Text;
                        divIntitution.Visible = true;
                    }
                    else
                    {
                        ddlInstitution.SelectedValue = "00000000-0000-0000-0000-000000000000";
                        divIntitution.Visible = false;
                    }


                    if (dr["Position"] != null && dr["Position"].ToString() != "&nbsp;" && dr["Position"].ToString().Length > 0)  //row.Cells[8].Text != "&nbsp;")
                    {
                        ddlPosition.SelectedItem.Text = dr["Position"].ToString(); //row.Cells[8].Text;
                    }
                    else
                    {
                        ddlPosition.SelectedItem.Text = "";
                    }
                    if (dr["id_position"] != null && dr["id_position"].ToString() != "&nbsp;" && dr["id_position"].ToString().Length > 0)  //row.Cells[22].Text != "&nbsp;")
                    {
                        ddlPosition.SelectedValue = dr["id_position"].ToString(); //row.Cells[22].Text;
                    }
                    else
                    {
                        ddlPosition.SelectedValue = "00000000-0000-0000-0000-000000000000";
                    }

                    if (dr["Division"] != null || dr["Division"].ToString() != "&nbsp;" && dr["Division"].ToString().Length > 0)  //row.Cells[9].Text != "&nbsp;")
                    {
                        ddlDivision.SelectedItem.Text = dr["Division"].ToString(); //row.Cells[9].Text;
                    }
                    else
                    {
                        ddlDivision.SelectedItem.Text = "";
                    }
                    if (dr["id_division"] != null && dr["id_division"].ToString() != "&nbsp;" && dr["id_division"].ToString().Length > 0)  //row.Cells[23].Text != "&nbsp;")
                    {
                        ddlDivision.SelectedValue = dr["id_division"].ToString(); // row.Cells[23].Text;
                    }
                    else
                    {
                        ddlDivision.SelectedValue = "00000000-0000-0000-0000-000000000000";
                    }

                    if (dr["Section"] != null && dr["Section"].ToString() != "&nbsp;" && dr["Section"].ToString().Length > 0)  //row.Cells[10].Text != "&nbsp;")
                    {
                        ddlSection.SelectedItem.Text = dr["Section"].ToString(); // row.Cells[10].Text;
                    }
                    else
                    {
                        ddlSection.SelectedItem.Text = "";
                    }
                    if (dr["id_section"] != null && dr["id_section"].ToString() != "&nbsp;" && dr["id_section"].ToString().Length > 0)  //row.Cells[24].Text != "&nbsp;")
                    {
                        ddlSection.SelectedValue = dr["id_section"].ToString(); // row.Cells[24].Text;
                    }
                    else
                    {
                        ddlSection.SelectedValue = "00000000-0000-0000-0000-000000000000";
                    }

                    if (dr["SubSection"] != null && dr["SubSection"].ToString() != "&nbsp;" && dr["SubSection"].ToString().Length > 0)  //row.Cells[11].Text != "&nbsp;")
                    {
                        ddlSubSection.SelectedItem.Text = dr["SubSection"].ToString(); // row.Cells[11].Text;
                    }
                    else
                    {
                        ddlSubSection.SelectedItem.Text = "";
                    }
                    if (dr["id_sub_section"] != null && dr["id_sub_section"].ToString() != "&nbsp;" && dr["id_sub_section"].ToString().Length > 0)  //row.Cells[25].Text != "&nbsp;")
                    {
                        ddlSubSection.SelectedValue = dr["id_sub_section"].ToString(); //row.Cells[25].Text;
                    }
                    else
                    {
                        ddlSubSection.SelectedValue = "00000000-0000-0000-0000-000000000000";
                    }

                    if (dr["Location"] != null && dr["Location"].ToString() != "&nbsp;" && dr["Location"].ToString().Length > 0) // row.Cells[13].Text != "&nbsp;")
                    {
                        ddlLocationCode.SelectedItem.Text = dr["Location"].ToString(); // row.Cells[13].Text;
                    }
                    else
                    {
                        ddlLocationCode.SelectedItem.Text = "";
                    }
                    if (dr["code_location"] != null && dr["code_location"].ToString() != "&nbsp;" && dr["code_location"].ToString().Length > 0) //row.Cells[26].Text != "&nbsp;")
                    {
                        ddlLocationCode.SelectedValue = dr["code_location"].ToString(); // row.Cells[26].Text;
                    }
                    else
                    {
                        ddlLocationCode.SelectedValue = "00000000-0000-0000-0000-000000000000";
                    }
                    if (dr["report_position"] != null && dr["report_position"].ToString() != "&nbsp;" && dr["report_position"].ToString().Length > 0) //row.Cells[12].Text != "&nbsp;")
                    {
                        ddlReportPosition.SelectedItem.Text = dr["report_position"].ToString(); // row.Cells[12].Text;
                    }
                    else
                    {
                        ddlReportPosition.SelectedItem.Text = "";
                    }

                    if (dr["Id_nationality"] != null && dr["Id_nationality"].ToString() != "&nbsp;" && dr["Id_nationality"].ToString().Length > 0)  //row.Cells[23].Text != "&nbsp;")
                    {
                        ddlNationality.SelectedValue = dr["Id_nationality"].ToString(); // row.Cells[23].Text;
                    }
                    else
                    {
                        ddlNationality.SelectedValue = "00000000-0000-0000-0000-000000000000";
                    }



                    if (dr["Id_Province"] != null && dr["Id_Province"].ToString() != "&nbsp;" && dr["Id_Province"].ToString().Length > 0)  //row.Cells[23].Text != "&nbsp;")
                    {
                        ddlProvince.SelectedValue = dr["Id_Province"].ToString(); // row.Cells[23].Text;
                    }
                    else
                    {
                        ddlProvince.SelectedValue = "00000000-0000-0000-0000-000000000000";
                    }


                    if (dr["Id_City"] != null && dr["Id_City"].ToString() != "&nbsp;" && dr["Id_City"].ToString().Length > 0)  //row.Cells[23].Text != "&nbsp;")
                    {
                        ddlCity.SelectedValue = dr["Id_City"].ToString(); // row.Cells[23].Text;
                    }
                    else
                    {
                        ddlCity.SelectedValue = "00000000-0000-0000-0000-000000000000";
                    }



                    if (dr["City"] != null && dr["City"].ToString() != "&nbsp;" &&  dr["City"].ToString().Length > 0) {
                        
                        ddlCity.SelectedItem.Text = dr["City"].ToString(); 
                    
                    }
                    else
                    {
                        ddlCity.SelectedItem.Text = "";
                    }
                    if (dr["Province"] != null && dr["Province"].ToString() != "&nbsp;" &&  dr["Province"].ToString().Length > 0) {

                        ddlProvince.SelectedItem.Text = dr["Province"].ToString();

                    }
                    else 
                    {                        
                        ddlProvince.SelectedItem.Text = "";
                    
                    }
                    if (dr["Nationality"] != null && dr["Nationality"].ToString() != "&nbsp;" && dr["Nationality"].ToString().Length > 0)
                    {

                        ddlNationality.SelectedItem.Text = dr["Nationality"].ToString();

                    }else
                    {
                        ddlNationality.SelectedItem.Text = "";
                    }


                    txtAddress.Value = dr["address"].ToString(); //row.Cells[27].Text;
                    txtDomicile.Value = dr["domicile"].ToString(); //row.Cells[28].Text;
                    txtBPjamsostekNumber.Value = dr["id_bpjamsostek"].ToString(); //row.Cells[29].Text;
                    txtBPJSKesehatanNo.Value = dr["BPJSKSNo"].ToString();
                    txtNPWP.Value = dr["NPWPNo"].ToString();
                    txtWorkingEmail.Value = dr["WorkingEmail"].ToString();

                    //if (dr["SourceOfHire"] != null && dr["SourceOfHire"].ToString() != "&nbsp;") //row.Cells[12].Text != "&nbsp;")
                    //{
                    //    ddlSourceOfHire.SelectedItem.Text = dr["SourceOfHire"].ToString(); // row.Cells[12].Text;
                    //}
                    //else
                    //{
                    //    ddlSourceOfHire.SelectedItem.Text = "";
                    //}


                    string SourceOfHire = dr["SourceOfHire"].ToString();

                    ListItem SourceOfHireitem = ddlSourceOfHire.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(SourceOfHire, StringComparison.OrdinalIgnoreCase));

                    if (SourceOfHireitem != null)
                    {
                        ddlSourceOfHire.ClearSelection();
                        SourceOfHireitem.Selected = true;
                    }
                    else
                    {
                        ddlSourceOfHire.SelectedIndex = 0;
                    }





                    txtBankName.Value = dr["BankName"].ToString();
                    txtBankAccountNo.Value = dr["BankAccount"].ToString();
                    txtBankAccountName.Value = dr["BankAccountName"].ToString();
                    txtContractStartDate.Value = dr["ContractStartDate"].ToString();
                    txtContractEndDate.Value = dr["ContractEndDate"].ToString();

                    //if (dr["POB"].ToString().Length > 0) { ddlPOB.SelectedValue = dr["POB"].ToString(); } // row.Cells[12].Text; 
                    txtPOB.Value = dr["POB"].ToString();  // row.Cells[12].Text; 
                                                          //if (dr["Gender"].ToString().Length > 0) { ddlGender.SelectedItem.Text = dr["Gender"].ToString(); }

                    //if (dr["Gender"].ToString().Length > 0) { if (dr["Gender"].ToString().Substring(0, 4) != "<Sel") { ddlGender.SelectedItem.Text = dr["Gender"].ToString(); }; }

                    string gender = dr["Gender"].ToString();

                    ListItem item = ddlGender.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(gender, StringComparison.OrdinalIgnoreCase));

                    if (item != null)
                    {
                        ddlGender.ClearSelection();
                        item.Selected = true;
                    }
                    else
                    {
                        ddlGender.SelectedIndex = 0;
                    }



                    //if (dr["BloodType"].ToString().Length > 0) { if (dr["BloodType"].ToString().Substring(0, 1) != "<") { ddlBloodType.SelectedItem.Text = dr["BloodType"].ToString(); }; }


                    string blood = dr["BloodType"].ToString();

                    ListItem blooditem = ddlBloodType.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(blood, StringComparison.OrdinalIgnoreCase));

                    if (blooditem != null)
                    {
                        ddlBloodType.ClearSelection();
                        blooditem.Selected = true;
                    }
                    else
                    {
                        ddlBloodType.SelectedIndex = 0;
                    }

                    //if (dr["status"].ToString().Length > 0) { ddlStatus.SelectedItem.Text = dr["status"].ToString(); } //row.Cells[3].Text;

                    string sts = dr["status"].ToString();
                    ListItem stsitem = ddlStatus.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(sts, StringComparison.OrdinalIgnoreCase));

                    if (stsitem != null)
                    {
                        ddlStatus.ClearSelection();
                        stsitem.Selected = true;
                    }
                    else
                    {
                        ddlStatus.SelectedIndex = 0;
                    }


                    //if (dr["category"].ToString().Length > 0) { ddlCategory.SelectedItem.Text = dr["category"].ToString(); } //row.Cells[4].Text;
                    string category = dr["category"].ToString();
                    ListItem categoryitem = ddlCategory.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(category, StringComparison.OrdinalIgnoreCase));

                    if (categoryitem != null)
                    {
                        ddlCategory.ClearSelection();
                        categoryitem.Selected = true;
                    }
                    else
                    {
                        ddlCategory.SelectedIndex = 0;
                    }

                    txtPostalCode.Value = dr["PostalCode"].ToString(); // row.Cells[26].Text;
                    hlbKTP.Value = dr["id_ktp"].ToString(); // row.Cells[14].Text;
                   
                    txtMobilePhone.Value = dr["MobilePhone"].ToString();
                    //txtEmergencyContact.Value = dr["EmergencyContactNumber"].ToString();
                    txtPersonalEmail.Value = dr["PersonalEmail"].ToString();
                    txtFamilyCard.Value = dr["FamilyCardNumber"].ToString();

                    //if (dr["MaritalStatus"].ToString().Length > 0) { if (dr["MaritalStatus"].ToString().Substring(0, 4) != "<Sel") { ddlMaritalStatus.SelectedItem.Text = dr["MaritalStatus"].ToString(); }; }

                    string martial = dr["MaritalStatus"].ToString();

                    ListItem martialitem = ddlMaritalStatus.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(martial, StringComparison.OrdinalIgnoreCase));

                    if (martialitem != null)
                    {
                        ddlMaritalStatus.ClearSelection();
                        martialitem.Selected = true;
                    }
                    else
                    {
                        ddlMaritalStatus.SelectedIndex = 0;
                    }
                    //if (dr["ReligioN"].ToString().Length > 0) { if (dr["ReligioN"].ToString().Substring(0, 4) != "<Sel") { ddlReligion.SelectedItem.Text = dr["ReligioN"].ToString(); }; }

                    string religi = dr["ReligioN"].ToString();

                    ListItem religiitem = ddlReligion.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(religi, StringComparison.OrdinalIgnoreCase));

                    if (religiitem != null)
                    {
                        ddlReligion.ClearSelection();
                        religiitem.Selected = true;
                    }
                    else
                    {
                        ddlReligion.SelectedIndex = 0;
                    }



                    //if (dr["Education1"].ToString().Length > 0) { if (dr["Education1"].ToString().Substring(0, 4) != "<Sel") { ddlEducation1.SelectedItem.Text = dr["Education1"].ToString(); }; }
                    string Education1 = dr["Education1"].ToString();

                    ListItem Education1itm = ddlEducation1.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(Education1, StringComparison.OrdinalIgnoreCase));

                    if (Education1itm != null)
                    {
                        ddlEducation1.ClearSelection();
                        Education1itm.Selected = true;
                    }
                    else
                    {
                        ddlEducation1.SelectedIndex = 0;
                    }

                    txtMajor1.Value = dr["Major1"].ToString();
                    txtInstitute1.Value = dr["Institute1"].ToString();
                    
                    
                    if (dr["Education2"].ToString().Length > 0) { if (dr["Education2"].ToString().Substring(0, 4) != "<Sel") { ddlEducation2.SelectedItem.Text = dr["Education2"].ToString(); }; }

                    string Education2 = dr["Education2"].ToString();

                    ListItem Education2itm = ddlEducation2.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(Education2, StringComparison.OrdinalIgnoreCase));

                    if (Education2itm != null)
                    {
                        ddlEducation2.ClearSelection();
                        Education2itm.Selected = true;
                    }
                    else
                    {
                        ddlEducation2.SelectedIndex = 0;
                    }



                    txtMajor2.Value = dr["Major2"].ToString();
                    txtInstitute2.Value = dr["Institute2"].ToString();

                    txtNameOfSpouse.Value = dr["NameOfSpouse"].ToString();
                    txtNoIDSpouse.Value = dr["NoIDSpouse"].ToString();
                    if (dr["DOBSpouse"].ToString().Length > 0 && Convert.ToDateTime(dr["DOBSpouse"]).Year > 1900) { txtSpouseDOB.Value = dr["DOBSpouse"].ToString(); }
                    if (dr["POBSpouse"].ToString().Length > 0) { txtSpousePOB.Value = dr["POBSpouse"].ToString(); }

                    txtNameOf1stChild.Value = dr["NameOf1stChild"].ToString();
                    txtNoID1stChild.Value = dr["NoID1stChild"].ToString();
                    if (dr["DOB1stChild"].ToString().Length > 0 && Convert.ToDateTime(dr["DOB1stChild"]).Year > 1900) { txt1stChildDOB.Value = dr["DOB1stChild"].ToString(); }
                    if (dr["POB1stChild"].ToString().Length > 0) { txt1stChildPOB.Value = dr["POB1stChild"].ToString(); }

                    txtNameOf2ndChild.Value = dr["NameOf2ndChild"].ToString();
                    txtNoID2ndChild.Value = dr["NoID2ndChild"].ToString();
                    if (dr["DOB2ndChild"].ToString().Length > 0 && Convert.ToDateTime(dr["DOB2ndChild"]).Year > 1900) { txt2ndChildDOB.Value = dr["DOB2ndChild"].ToString(); }
                    if (dr["POB2ndChild"].ToString().Length > 0) { txt2ndChildPOB.Value = dr["POB2ndChild"].ToString(); }

                    txtNameOf3rdChild.Value = dr["NameOf3rdChild"].ToString();
                    txtNoID3rdChild.Value = dr["NoID3rdChild"].ToString();
                    if (dr["DOB3rdChild"].ToString().Length > 0 && Convert.ToDateTime(dr["DOB3rdChild"]).Year > 1900) { txt3rdChildDOB.Value = dr["DOB3rdChild"].ToString(); }
                    if (dr["POB3rdChild"].ToString().Length > 0) { txt3rdChildPOB.Value = dr["POB3rdChild"].ToString(); }
                    //if (dr["POB3rdChild"].ToString().Length > 0) { ddl3rdChildPOB.SelectedValue = dr["POB3rdChild"].ToString(); }

                    txtCreateBy.Value = dr["createby"].ToString(); //row.Cells[18].Text;
                    txtCreateDate.Value = dr["create_date"].ToString(); //row.Cells[19].Text;

                    if (dr["EffectiveEndDate"].ToString().Length > 0 && Convert.ToDateTime(dr["EffectiveEndDate"]).Year > 1900) { txtEffectiveEndDate.Value = dr["EffectiveEndDate"].ToString(); } //row.Cells[20].Text;

                    //if (dr["CareerHistory"].ToString().Length > 0) { ddlCareerHistory.SelectedItem.Text = dr["CareerHistory"].ToString(); }
                    string CareerHistory = dr["CareerHistory"].ToString();

                    ListItem CareerHistoryitem = ddlCareerHistory.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(CareerHistory, StringComparison.OrdinalIgnoreCase));

                    if (CareerHistoryitem != null)
                    {
                        ddlCareerHistory.ClearSelection();
                        CareerHistoryitem.Selected = true;
                    }
                    else
                    {
                        ddlCareerHistory.SelectedIndex = 0;
                    }


                    //if (dr["EmployeeSeparation"].ToString().Length > 0) { ddlEmployementSeparation.SelectedItem.Text = dr["EmployeeSeparation"].ToString(); }

                    string EmployeeSeparation = dr["EmployeeSeparation"].ToString();

                    ListItem EmployeeSeparationitem = ddlEmployementSeparation.Items
                        .Cast<ListItem>()
                        .FirstOrDefault(x =>
                            x.Text.Equals(EmployeeSeparation, StringComparison.OrdinalIgnoreCase));

                    if (EmployeeSeparationitem != null)
                    {
                        ddlEmployementSeparation.ClearSelection();
                        EmployeeSeparationitem.Selected = true;
                    }
                    else
                    {
                        ddlEmployementSeparation.SelectedIndex = 0;
                    }

                    // Replace this block:
                    //string Fromfile = "~/Photos/" + dr["Photo"].ToString() + ".jpeg";
                    //PhotoUser.ImageUrl = Fromfile;

                    // With this:
                    string photoFileName = dr["Photo"].ToString();
                    if (photoFileName.Length > 0)
                    {
                        string photoJpegPath = "~/Photos/" + photoFileName + ".jpeg";
                        string photoJpgPath = "~/Photos/" + photoFileName + ".jpg";
                        if (File.Exists(Server.MapPath(photoJpegPath)))
                        {
                            PhotoUser.ImageUrl = photoJpegPath;
                        }
                        else if (File.Exists(Server.MapPath(photoJpgPath)))
                        {
                            PhotoUser.ImageUrl = photoJpgPath;
                        }
                        else
                        {
                            PhotoUser.ImageUrl = ""; // or set to a default image path if desired
                        }
                        lblPhoto.Value = photoFileName;

                    }
                }

                if (ddlCategory.SelectedItem.Text == "CONTRACT")
                {
                    txtContractEndDate.Visible = true;
                    txtContractStartDate.Visible = true;

                    txtContractEndDate.Disabled = false;
                    txtContractStartDate.Disabled = false;
                }
                else
                {
                    txtContractEndDate.Visible = false;
                    txtContractStartDate.Visible = false;
                }

                if (ddlCategory.SelectedItem.Text == "PERMANENT")
                {

                }
                //btnSubmit.Visible = false;
                //btnNew.Visible = false;
                ////btnUpdate.Visible = true;
                ////btnAdd.Visible = false;
                //BindDataTableEmployees();

                BindDataTableEmergencyContact(TableEditEmergencyContactNew, lblEmployeeID.Value);

                BindDataTableCareerHistory(TableEditCareerHistory, txtNIK.Value.ToString());
                BindDataTableHistoryTrainingEmployee(TableViewUserTraining, lblNIK.Value);

                BindDataTableSuratPeringatan(TableEditSuratPeringatan, lblEmployeeID.Value);

            }
        }
        protected void GetMaster()
        {
            ddlCity.Items.Clear();
            ddlProvince.Items.Clear();
            ddlNationality.Items.Clear();
            //ddlPOB.Items.Clear();
            //ddl1stChildPOB.Items.Clear();
            //ddl2ndChildPOB.Items.Clear();
            //ddl3rdChildPOB.Items.Clear();
            //ddlSpousePOB.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();

            sqlcomm.CommandText = "sp_HRIS_DB_Master";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            Con.Open();
            // Country
            try
            {
                //if (_Parameter == "Country")
                //{

                sqlcomm.Parameters.Clear();
                sqlcomm.Parameters.AddWithValue("@StatementType", "Country");

                SqlDataReader dr;

                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlNationality.Items.Add(newItem);

                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["Name"].ToString();
                    newItem.Value = dr["Oid"].ToString();
                    ddlNationality.Items.Add(newItem);
                }
                dr.Close();
                //}
                //if (_Parameter == "City")
                //{

                sqlcomm.Parameters.Clear();
                sqlcomm.Parameters.AddWithValue("@StatementType", "City");

                SqlDataReader dr1;

                ListItem newItem1 = new ListItem();
                newItem1.Text = "";
                newItem1.Value = "00000000-0000-0000-0000-000000000000";
                ddlCity.Items.Add(newItem1);
                //ddl1stChildPOB.Items.Add(newItem1);
                //ddl2ndChildPOB.Items.Add(newItem1);
                //ddl3rdChildPOB.Items.Add(newItem1);
                //ddlSpousePOB.Items.Add(newItem1);
                //ddlPOB.Items.Add(newItem1);

                dr1 = sqlcomm.ExecuteReader();

                while (dr1.Read())
                {
                    newItem1 = new ListItem();
                    newItem1.Text = dr1["Name"].ToString();
                    newItem1.Value = dr1["Oid"].ToString();
                    ddlCity.Items.Add(newItem1);
                    //ddlPOB.Items.Add(newItem1);
                    //ddl1stChildPOB.Items.Add(newItem1);
                    //ddl2ndChildPOB.Items.Add(newItem1);
                    //ddl3rdChildPOB.Items.Add(newItem1);
                    //ddlSpousePOB.Items.Add(newItem1);
                }
                dr1.Close();
                //}
                //if (_Parameter == "Province")
                //{

                sqlcomm.Parameters.Clear();
                sqlcomm.Parameters.AddWithValue("@StatementType", "Province");

                SqlDataReader dr2;

                ListItem newItem2 = new ListItem();
                newItem2.Text = "";
                newItem2.Value = "00000000-0000-0000-0000-000000000000";
                ddlProvince.Items.Add(newItem2);

                dr2 = sqlcomm.ExecuteReader();

                while (dr2.Read())
                {
                    newItem2 = new ListItem();
                    newItem2.Text = dr2["Name"].ToString();
                    newItem2.Value = dr2["Oid"].ToString();
                    ddlProvince.Items.Add(newItem2);
                }
                dr2.Close();
                //}
                Con.Close();
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
        protected void GetLocation()
        {
            ddlLocationCode.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddLocation");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlLocationCode.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["branch_name"].ToString();
                    newItem.Value = dr["code_branch"].ToString();
                    ddlLocationCode.Items.Add(newItem);
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
        protected void GetReportPositionName()
        {
            ddlReportPosition.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddPositionName");

            SqlDataReader dr;

            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlReportPosition.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["position"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlReportPosition.Items.Add(newItem);
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
            ddlSubSection.Items.Clear();
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
                ddlSubSection.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["sub_section"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlSubSection.Items.Add(newItem);
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
        protected void GetSectionName()
        {
            ddlSection.Items.Clear();
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
                ddlSection.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["section"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlSection.Items.Add(newItem);
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



        protected void GetCity()
        {
            ddlCity.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "City");
            SqlDataReader dr;

            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlCity.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["Name"].ToString();
                    newItem.Value = dr["Oid"].ToString();
                    ddlCity.Items.Add(newItem);
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



        protected void GetNationality()
        {
            ddlNationality.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Country");
            SqlDataReader dr;
            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlNationality.Items.Add(newItem);
                Con.Open();
                dr = sqlcomm.ExecuteReader();
                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["Name"].ToString();
                    newItem.Value = dr["Oid"].ToString();
                    ddlNationality.Items.Add(newItem);
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


        protected void GetProvince()
        {
            ddlProvince.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "Province");
            SqlDataReader dr;
            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlProvince.Items.Add(newItem);
                Con.Open();
                dr = sqlcomm.ExecuteReader();
                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["Name"].ToString();
                    newItem.Value = dr["Oid"].ToString();
                    ddlProvince.Items.Add(newItem);
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





        protected void GetPositionName()
        {
            ddlPosition.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Employees";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddPositionName");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlPosition.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["position"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlPosition.Items.Add(newItem);
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
        protected void GetInstitutionName()
        {
            ddlInstitution.Items.Clear();
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);

            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Institutions";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "AddInstitutionname");

            SqlDataReader dr;


            try
            {
                ListItem newItem = new ListItem();
                newItem.Text = "";
                newItem.Value = "00000000-0000-0000-0000-000000000000";
                ddlInstitution.Items.Add(newItem);

                Con.Open();
                dr = sqlcomm.ExecuteReader();

                while (dr.Read())
                {
                    newItem = new ListItem();
                    newItem.Text = dr["institution_name"].ToString();
                    newItem.Value = dr["id"].ToString();
                    ddlInstitution.Items.Add(newItem);
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

        public DataTable EmergencyContactTable { get; set; }
        public class EmergencyContact
        {
            public string Oid { get; set; }
            public string ContactName { get; set; }
            public string ContactAddress { get; set; }
            public string ContactRelationship { get; set; }
            public string ContactNumber { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedDate { get; set; }
        }

        private string CustomString(string _input, int _length = int.MaxValue)
        {
            if (string.IsNullOrEmpty(_input) || _length <= 0)
            {
                _length = 0;
                return string.Empty;
            }
            else
            {
                if (_input == "&nbsp;")
                {
                    return string.Empty;
                }
                else
                {
                    if (_input.Substring(0, 1) != "<")
                    {
                        string sanitized = _input.Replace("'", "`");
                        if (sanitized.Length <= _length)
                        {
                            return sanitized;
                        }
                        else
                        {
                            return sanitized.Substring(0, _length);
                        }
                    }
                    else { return string.Empty; }
                }
            }
        }

        //protected void BindDataTableCandidateJoined()
        //{
        //    string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
        //    SqlConnection Con = new SqlConnection(path);
        //    Con.Open();
        //    SqlCommand sqlcomm = new SqlCommand();
        //    sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
        //    sqlcomm.CommandType = CommandType.StoredProcedure;
        //    sqlcomm.Connection = Con;
        //    sqlcomm.Parameters.AddWithValue("@StatementType", "ViewCandidateJoined");

        //    DataTable dtb = new DataTable();
        //    SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

        //    sda.Fill(dtb);
        //    ViewState["myViewState"] = dtb;
        //    if (dtb.Rows.Count <= 0)
        //    {
        //        dtb.Rows.Add(dtb.NewRow());
        //    }
        //    TableCandidateJoin.DataSource = dtb;
        //    TableCandidateJoin.DataBind();

        //    TableCandidateJoin.UseAccessibleHeader = true;
        //    TableCandidateJoin.HeaderRow.TableSection = TableRowSection.TableHeader;

        //    Con.Close();

        //}

        private void BindDataTableCareerHistory(GridView _Grid1, string _vNIK)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_Employees", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "HistoryMutationEmployee");
                sqlcomm.Parameters.AddWithValue("@nik", _vNIK);

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["myViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }
                    _Grid1.DataSource = dtb;
                    _Grid1.DataBind();

                    _Grid1.UseAccessibleHeader = true;
                    _Grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    _Grid1.Columns[0].Visible = false;
                }
            }
        }
                protected void BindDataTableCandidateJoined()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection Con = new SqlConnection(path);
            Con.Open();
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.CommandText = "sp_HRIS_DB_Personal_information";
            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Connection = Con;
            sqlcomm.Parameters.AddWithValue("@StatementType", "ViewCandidateJoined");

            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);

            sda.Fill(dtb);
            ViewState["myViewState"] = dtb;
            if (dtb.Rows.Count <= 0)
            {
                dtb.Rows.Add(dtb.NewRow());
            }
            TableCandidateJoin.DataSource = dtb;
            TableCandidateJoin.DataBind();

            TableCandidateJoin.UseAccessibleHeader = true;
            TableCandidateJoin.HeaderRow.TableSection = TableRowSection.TableHeader;

            Con.Close();

        }
        private void BindDataTableSuratPeringatan(GridView _Grid1, string _vID)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_Employees", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "ViewSuratPeringatan");
                sqlcomm.Parameters.AddWithValue("@id", _vID);

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["SuratPeringatanViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }
                    _Grid1.DataSource = dtb;
                    _Grid1.DataBind();
                    _Grid1.UseAccessibleHeader = true;
                    _Grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //_Grid1.Columns[1].Visible = false;

                }
            }
        }
        private void BindDataTableEmergencyContact(GridView _Grid1, string _vID)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_Employees", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "ViewEmergencyContact");
                sqlcomm.Parameters.AddWithValue("@id", _vID);

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["EmergencyContactViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }
                    _Grid1.DataSource = dtb;
                    _Grid1.DataBind();
                    _Grid1.UseAccessibleHeader = true;
                    _Grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //_Grid1.Columns[1].Visible = false;

                }
            }
        }
        protected void BindDataTableHistoryTrainingEmployee(GridView _GridTraining, string _NIK)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_IndividualTraining", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "ViewParticipantTrainingDetail");
                sqlcomm.Parameters.AddWithValue("@nik_participant", _NIK);

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["myViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }
                    _GridTraining.DataSource = dtb;
                    _GridTraining.DataBind();

                    _GridTraining.UseAccessibleHeader = true;
                    _GridTraining.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void BindDataTableHistoryMutationEmployee(GridView _GridTraining)
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_Employees", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "HistoryMutationEmployee");
                sqlcomm.Parameters.AddWithValue("@nik", lblNIK.Value);

                using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    ViewState["myViewState"] = dtb;
                    if (dtb.Rows.Count <= 0)
                    {
                        dtb.Rows.Add(dtb.NewRow());
                    }

                    _GridTraining.DataSource = dtb;
                    _GridTraining.DataBind();

                    _GridTraining.UseAccessibleHeader = true;
                    _GridTraining.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }
        protected void UpdatePhoto()
        {
            string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(path))
            using (SqlCommand sqlcomm = new SqlCommand("sp_HRIS_DB_Employees", Con))
            {
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddWithValue("@StatementType", "SavePhoto");
                sqlcomm.Parameters.AddWithValue("@nik", lblNIK.Value);
                sqlcomm.Parameters.AddWithValue("@Photo", lblPhoto.Value);

                Con.Open();
                sqlcomm.ExecuteNonQuery();
                sqlcomm.Parameters.Clear();
            }
        }
        protected void ViewEdocsFilesUpload()
        {
            //string _vShipmentID = Request.QueryString["ShipmentID"];
            //string _vMasterRef = txtMAWBNo.Value;

            string _vPath = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
            SqlConnection _vSQLCon = new SqlConnection(_vPath);
            SqlCommand _vSQLComm = new SqlCommand();

            _vSQLCon.Open();
            _vSQLComm.Connection = _vSQLCon;
            _vSQLComm.CommandType = CommandType.StoredProcedure;
            _vSQLComm.CommandText = "sp_HRIS_eDocs";

            _vSQLComm.Parameters.AddWithValue("@Flag", "ViewSuratPeringatan");
            _vSQLComm.Parameters.AddWithValue("@NIK", lblNIK.Value);

            DataTable _dt = new DataTable();
            SqlDataReader _dr = _vSQLComm.ExecuteReader();
            _dt.Load(_dr);

            TableeDocsFiles.DataSource = _dt;
            TableeDocsFiles.DataBind();
            TableeDocsFiles.UseAccessibleHeader = true;
            TableeDocsFiles.HeaderRow.TableSection = TableRowSection.TableHeader;

            TableeDocsFiles.Columns[0].Visible = false;
            TableeDocsFiles.Columns[6].Visible = false;
            TableeDocsFiles.Columns[7].Visible = false;

            _dt.Dispose();
            _vSQLComm.Parameters.Clear();

            _vSQLComm.Parameters.AddWithValue("@Flag", "ViewOther");
            _vSQLComm.Parameters.AddWithValue("@NIK", lblNIK.Value);

            DataTable _dt1 = new DataTable();
            SqlDataReader _dr1 = _vSQLComm.ExecuteReader();
            _dt1.Load(_dr1);

            TableeDocsFiles_Other.DataSource = _dt1;
            TableeDocsFiles_Other.DataBind();
            TableeDocsFiles_Other.UseAccessibleHeader = true;
            TableeDocsFiles_Other.HeaderRow.TableSection = TableRowSection.TableHeader;

            TableeDocsFiles_Other.Columns[0].Visible = false;
            TableeDocsFiles_Other.Columns[6].Visible = false;
            TableeDocsFiles_Other.Columns[7].Visible = false;


            _dt1.Dispose();
            _vSQLCon.Close();
            _vSQLComm.Dispose();
            _vSQLCon.Dispose();

            _vSQLCon.Close();
        }

        protected void CheckUploadDocument()
        {
            if (FileUploadEDocs.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                lbErrorUploadNotif.InnerText = ""; // Clear previous messages

                // Check the number of selected files
                filecount = FileUploadEDocs.PostedFiles.Count();
                string[] allowedExtensions = { ".pdf", ".docx", ".xlsx", ".jpg", ".jpeg", ".png" };

                if (filecount <= 1)
                {
                    foreach (HttpPostedFile postfiles in FileUploadEDocs.PostedFiles)
                    {
                        // Get the File Extension
                        string filetype = Path.GetExtension(postfiles.FileName);

                        if (IsValidFileType(filetype, allowedExtensions))
                        {
                            // Get the File Size In Byte
                            double filesize = postfiles.ContentLength;

                            if (filesize < (2048576))
                            {
                                fileuploadcount++;
                                string serverfolder = string.Empty;
                                string serverpath = string.Empty;

                                // Adding File Into Specific Folder Depend On his Extension
                                switch (filetype.ToLower())
                                {
                                    case ".pdf":
                                    case ".docx":
                                    case ".xlsx":
                                    case ".jpg":
                                    case ".jpeg":
                                    case ".png":
                                        string _vNIK = txtNIK.Value;
                                        //string _vCode = txtMAWBNo.Value; //Session["Code"].ToString();
                                        string _vDocType = string.Empty; //ddlDocType.SelectedItem.Text;

                                        if (ddlDocType.SelectedItem.Text.Substring(0, 2) == "SP")
                                        { _vDocType = "SuratPeringatan"; }
                                        else
                                        { _vDocType = "Others"; }

                                        serverfolder = Server.MapPath("~/eDocs_Files/Employee" + "/" + _vNIK + "/" + _vDocType + "/");

                                        // Ensure the folder is available
                                        if (!Directory.Exists(serverfolder))
                                        {
                                            // Create the folder
                                            Directory.CreateDirectory(serverfolder);
                                        }

                                        // Append a unique identifier to avoid overwriting
                                        string uniqueFileName = Path.GetFileNameWithoutExtension(postfiles.FileName)
                                            + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") // Add timestamp
                                            + Path.GetExtension(postfiles.FileName);

                                        serverpath = Path.Combine(serverfolder, uniqueFileName);
                                        postfiles.SaveAs(serverpath);
                                        lbErrorUploadNotif.InnerText += "[" + uniqueFileName + "]- " + filetype + " file uploaded successfully";
                                        lblOK.Value = "OK";
                                        lblFileName.Value = uniqueFileName;
                                        break;
                                }
                            }
                            else
                            {
                                //lbErrorUploadNotif.InnerText += "[" + postfiles.FileName + "]- File not uploaded; size is greater than 2MB. Your File Size is " + (filesize / (1024 * 1034)) + " MB";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "toastr.error('Save failed!,files not uploded size is greater then(2)MB');", true);
                            }
                        }
                        else
                        {
                            //lbErrorUploadNotif.InnerText += "[" + postfiles.FileName + "]- Invalid file type. Allowed types are pdf/docx/xlsx/jpg/jpeg/png";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "toastr.error('Save failed!,file type must be .png or .xlsx or .doc or .pdf and .jpg');", true);
                        }
                    }
                }
                else
                {
                    //lbErrorUploadNotif.InnerText = "You have selected " + filecount + " files. Please select a maximum of 1 files." ;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "toastr.error('Save failed!,Please select Maximum (6) files');", true);
                }
            }
            else
            {
                lbErrorUploadNotif.InnerText = "No Files Upload.";
            }
        }
        private bool IsValidFileType(string fileType, string[] allowedExtensions)
        {
            return Array.Exists(allowedExtensions, ext => ext.Equals(fileType, StringComparison.OrdinalIgnoreCase));
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlTableCandidateJoin').modal();", true);
            BindDataTableCandidateJoined();
            //BindDataTableEmployees();
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnChangeFoto_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
            BindDataEmployeeDetail();
        }

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSubSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlReportPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlLocationCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlInstitution_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSourceOfHire_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBloodType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlNationality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void TableEditEmergencyContact_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void TableEditEmergencyContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        // Pseudocode:
        // 1. In your GridView, use a TemplateField with a DropDownList for the column you want to bind.
        // 2. In the GridView's RowDataBound event, find the DropDownList control.
        // 3. Bind the DropDownList's items (e.g., from a data source or static list).
        // 4. Set the DropDownList's SelectedValue property to the value from the DataRow.

        // Example for GridView RowDataBound event handler:
        protected void TableEditCareerHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the DropDownList in the current row
                DropDownList ddlCategory = e.Row.FindControl("ddlCategory") as DropDownList;
                if (ddlCategory != null)
                {
                    // Example: bind items (replace with your actual data source)
                    ddlCategory.Items.Clear();
                    ddlCategory.Items.Add(new ListItem("", ""));
                    ddlCategory.Items.Add(new ListItem("Promotion", "Promotion"));
                    ddlCategory.Items.Add(new ListItem("Mutation", "Mutation"));
                    ddlCategory.Items.Add(new ListItem("Rotation", "Rotation"));
                    ddlCategory.Items.Add(new ListItem("Demotion", "Demotion"));

                    // Get the value from the data item (DataRowView)
                    string categoryValue = DataBinder.Eval(e.Row.DataItem, "Category")?.ToString();
                    if (!string.IsNullOrEmpty(categoryValue))
                    {
                        // Set the selected value
                        ListItem item = ddlCategory.Items.FindByValue(categoryValue);
                        if (item != null)
                        {
                            ddlCategory.ClearSelection();
                            item.Selected = true;
                        }
                    }
                }

                TextBox txtEffectiveDate = (TextBox)e.Row.FindControl("txtEffectiveDate");

                if (txtEffectiveDate != null)
                {
                    object effectiveDateObj = DataBinder.Eval(e.Row.DataItem, "EffectiveDate");

                    if (effectiveDateObj != DBNull.Value && effectiveDateObj != null)
                    {
                        DateTime effectiveDate = Convert.ToDateTime(effectiveDateObj);

                        // wajib format yyyy-MM-dd untuk input type=date
                        txtEffectiveDate.Text = effectiveDate.ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIK.Value) && !string.IsNullOrEmpty(txtFullname.Value))
            {
                //string path1 = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                //SqlConnection Con1 = new SqlConnection(path1);
                //Con1.Open();
                //SqlCommand sqlcomm1 = new SqlCommand();
                //sqlcomm1.CommandText = "sp_HRIS_DB_Employees";
                //sqlcomm1.CommandType = CommandType.StoredProcedure;
                //sqlcomm1.Connection = Con1;
                //sqlcomm1.Parameters.AddWithValue("@StatementType", "CheckNIK");
                //sqlcomm1.Parameters.AddWithValue("@nik", CustomString(txtNIK.Value));
                //sqlcomm1.Parameters.Add("@FlagCheck_Out", SqlDbType.Int).Direction = ParameterDirection.Output;
                //sqlcomm1.ExecuteNonQuery();
                //String _flagcheck = sqlcomm1.Parameters["@FlagCheck_Out"].Value?.ToString() ?? "";

                //sqlcomm1.Dispose();
                //Con1.Close();

                //if (_flagcheck.Length > 0 && _flagcheck.ToString() == "1")
                //{
                if (!string.IsNullOrEmpty(hlbURLPhoto.Value))
                {
                    string link_image = hlbURLPhoto.Value.ToString().Substring(33, hlbURLPhoto.Value.ToString().Length - 33);
                    string url = "https://drive.google.com/uc?export=download&id=" + link_image;
                    if (!System.IO.Directory.Exists(Server.MapPath("~/Photos")))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Photos"));
                    }
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        client.DownloadFile(new Uri(url), Server.MapPath("~/Photos/" + txtNIK.Value.ToString() + ".jpg"));
                    }

                    lblPhoto.Value = txtNIK.Value.ToString();
                }

                string path = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection Con = new SqlConnection(path);
                Con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "sp_HRIS_DB_Employees";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = Con;
                if (lblNewEmployee.Value == "true")
                {
                    sqlcomm.Parameters.AddWithValue("@StatementType", "Save");
                }
                else
                {
                    sqlcomm.Parameters.AddWithValue("@StatementType", "UpdateEmployee");
                }
                if (CustomString(lblEmployeeID.Value.Trim()).Length > 0) { sqlcomm.Parameters.AddWithValue("@id", CustomString(lblEmployeeID.Value.Trim())); }
                sqlcomm.Parameters.AddWithValue("@fullname", CustomString(txtFullname.Value));
                sqlcomm.Parameters.AddWithValue("@nik", CustomString(txtNIK.Value));
                if (CustomString(ddlPosition.SelectedValue).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_position", CustomString(ddlPosition.SelectedValue)); }
                if (CustomString(ddlDivision.SelectedValue).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_division", CustomString(ddlDivision.SelectedValue)); }
                if (CustomString(ddlSection.SelectedValue).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_section", CustomString(ddlSection.SelectedValue)); }
                if (CustomString(ddlSubSection.SelectedValue).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_sub_section", CustomString(ddlSubSection.SelectedValue)); }
                if (CustomString(ddlReportPosition.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@report_position", CustomString(ddlReportPosition.SelectedItem.Text.ToString())); }
                if (CustomString(ddlLocationCode.SelectedValue).Length > 0) { sqlcomm.Parameters.AddWithValue("@code_location", CustomString(ddlLocationCode.SelectedValue)); }
                if (CustomString(ddlStatus.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@status", CustomString(ddlStatus.SelectedItem.Text.ToString())); }
                if (CustomString(ddlCategory.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@category", CustomString(ddlCategory.SelectedItem.Text.ToString())); }
                sqlcomm.Parameters.AddWithValue("@join_date", CustomString(txtJoinDate.Value));
                if (CustomString(txtModifiedBy.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@modifiedby", CustomString(txtModifiedBy.Value)); }
                if (CustomString(txtCreateDate.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@createdby", CustomString(txtCreateDate.Value)); }
                //sqlcomm.Parameters.AddWithValue("@modified_date", txtModifiedDate.Value);
                //sqlcomm.Parameters.Add("@modified_date", SqlDbType.DateTime).Value = Convert.ToDateTime(txtModifiedDate.Value);


            

                if (CustomString(txtAddress.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@address_value", CustomString(txtAddress.Value)); }
                if (CustomString(txtDomicile.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@domicile_value", CustomString(txtDomicile.Value)); }
                sqlcomm.Parameters.AddWithValue("@date_of_birth_value", CustomString(txtBirthDate.Value));
                if (CustomString(txtBPjamsostekNumber.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_bpjamsostek_value", CustomString(txtBPjamsostekNumber.Value)); }
                if (CustomString(hlbKTP.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@id_ktp_value", CustomString(hlbKTP.Value)); }

                sqlcomm.Parameters.AddWithValue("@id_institution", CustomString(ddlInstitution.SelectedValue));
                if (txtContractStartDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractStartDate", txtContractStartDate.Value); }
                if (txtContractEndDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@ContractEndDate", txtContractEndDate.Value); }

                if (ddlGender.SelectedItem.Text.Length > 0) { sqlcomm.Parameters.AddWithValue("@Gender", CustomString(ddlGender.SelectedItem.Text.ToString())); }
                if (ddlReligion.SelectedItem.Text.Length > 0) { sqlcomm.Parameters.AddWithValue("@Religion", CustomString(ddlReligion.SelectedItem.Text.ToString())); }
                if (ddlBloodType.SelectedItem.Text.Length > 0) { sqlcomm.Parameters.AddWithValue("@BloodType", CustomString(ddlBloodType.SelectedItem.Text.ToString())); }

                if (CustomString(ddlNationality.SelectedValue).Length > 0) { sqlcomm.Parameters.AddWithValue("@Nationality", CustomString(ddlNationality.SelectedValue)); }
                if (CustomString(ddlEducation1.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@Education1", CustomString(ddlEducation1.SelectedItem.Text.ToString())); }
                if (CustomString(txtInstitute1.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@Institute1", CustomString(txtInstitute1.Value)); }
                if (CustomString(txtMajor1.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@Major1", CustomString(txtMajor1.Value)); }
                if (CustomString(ddlEducation2.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@Education2", CustomString(ddlEducation2.SelectedItem.Text.ToString())); }
                if (CustomString(txtInstitute2.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@Institute2", CustomString(txtInstitute2.Value)); }
                if (CustomString(txtMajor2.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@Major2", CustomString(txtMajor2.Value)); }
                if (CustomString(ddlCity.SelectedValue).Length > 0) { sqlcomm.Parameters.AddWithValue("@City", CustomString(ddlCity.SelectedValue)); }
                if (CustomString(ddlProvince.SelectedValue).Length > 0) { sqlcomm.Parameters.AddWithValue("@Province", CustomString(ddlProvince.SelectedValue)); }
                if (CustomString(txtPostalCode.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@PostalCode", CustomString(txtPostalCode.Value)); }
                if (CustomString(txtMobilePhone.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@MobilePhone_Value", CustomString(txtMobilePhone.Value)); }
                if (CustomString(txtWorkingEmail.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@WorkingEmail_Value", CustomString(txtWorkingEmail.Value)); }
                if (CustomString(txtPersonalEmail.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@PersonalEmail_Value", CustomString(txtPersonalEmail.Value)); }
                if (CustomString(ddlMaritalStatus.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@MaritalStatus", CustomString(ddlMaritalStatus.SelectedItem.Text.ToString())); }
                if (CustomString(txtPOB.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@POB", CustomString(txtPOB.Value)); }
                if (CustomString(txtFamilyCard.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@FamilyCardNumber_Value", CustomString(txtFamilyCard.Value)); }
                if (CustomString(txtNameOfSpouse.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NameOfSpouse_Value", CustomString(txtNameOfSpouse.Value)); }
                if (CustomString(txtNoIDSpouse.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NoIDSpouse_Value", CustomString(txtNoIDSpouse.Value)); }
                if (txtSpouseDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOBSpouse_Value", txtSpouseDOB.Value); }
                if (CustomString(txtSpousePOB.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@POBSpouse_Value", CustomString(txtSpousePOB.Value)); }
                if (CustomString(txtNameOf1stChild.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NameOf1stChild_Value", CustomString(txtNameOf1stChild.Value)); }
                if (CustomString(txtNoID1stChild.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NoID1stChild_Value", CustomString(txtNoID1stChild.Value)); }
                if (txt1stChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB1stChild_Value", txt1stChildDOB.Value); }
                if (CustomString(txt1stChildPOB.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@POB1stChild_Value", CustomString(txt1stChildPOB.Value)); }
                if (CustomString(txtNameOf2ndChild.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NameOf2ndChild_Value", CustomString(txtNameOf2ndChild.Value)); }
                if (CustomString(txtNoID2ndChild.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NoID2ndChild_Value", CustomString(txtNoID2ndChild.Value)); }
                if (txt2ndChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB2ndChild_Value", txt2ndChildDOB.Value); }
                if (CustomString(txt2ndChildPOB.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@POB2ndChild_Value", CustomString(txt2ndChildPOB.Value)); }
                if (CustomString(txtNameOf3rdChild.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NameOf3rdChild_Value", CustomString(txtNameOf3rdChild.Value)); }
                if (CustomString(txtNoID3rdChild.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NoID3rdChild_Value", CustomString(txtNoID3rdChild.Value)); }
                if (txt3rdChildDOB.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@DOB3rdChild_Value", txt3rdChildDOB.Value); }
                if (CustomString(txt3rdChildPOB.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@POB3rdChild_Value", CustomString(txt3rdChildPOB.Value)); }
                if (CustomString(txtBPJSKesehatanNo.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@BPJSKSNo_Value", CustomString(txtBPJSKesehatanNo.Value)); }
                if (CustomString(txtNPWP.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@NPWPNo_Value", CustomString(txtNPWP.Value)); }
                if (CustomString(txtBankAccountNo.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@BankAccount_Value", CustomString(txtBankAccountNo.Value)); }
                if (CustomString(txtBankAccountName.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@BankAccountName_Value", CustomString(txtBankAccountName.Value)); }
                if (CustomString(txtBankName.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@BankName_Value", CustomString(txtBankName.Value)); }
                
                if (CustomString(ddlSourceOfHire.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@SourceOfHire", CustomString(ddlSourceOfHire.SelectedItem.Text.ToString())); }



                if (CustomString(ddlCareerHistory.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@CareerHistory", CustomString(ddlCareerHistory.SelectedItem.Text.ToString())); }
                if (CustomString(ddlEmployementSeparation.SelectedItem.Text.ToString()).Length > 0) { sqlcomm.Parameters.AddWithValue("@EmployeeSeparation", CustomString(ddlEmployementSeparation.SelectedItem.Text.ToString())); }

                if (txtEffectiveEndDate.Value.Length > 0) { sqlcomm.Parameters.AddWithValue("@EffectiveEndDate", txtEffectiveEndDate.Value); }
                if (CustomString(lblPhoto.Value).Length > 0) { sqlcomm.Parameters.AddWithValue("@Photo", CustomString(lblPhoto.Value)); }

                sqlcomm.Parameters.Add("@EmployeeID_Out", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;

                //foreach(SqlParameter p in sqlcomm.Parameters)
                //{
                //    Response.Write(p.ParameterName + " = " + p.Value + "<br>");
                //}

                //Response.End();

                sqlcomm.ExecuteNonQuery();

                if (lblNewEmployee.Value == "true")
                { lblEmployeeID.Value = sqlcomm.Parameters["@EmployeeID_Out"].Value?.ToString(); }

                sqlcomm.Parameters.Clear();

                //update Emergency Contact

                if (lblNewEmployee.Value == "false")
                {
                    if (TableEditEmergencyContactNew.Rows.Count > 0)
                    {
                        DataTable dtEmergencyContact = new DataTable();
                        dtEmergencyContact.Columns.Add("Oid");
                        dtEmergencyContact.Columns.Add("ContactName");
                        dtEmergencyContact.Columns.Add("ContactAddress");
                        dtEmergencyContact.Columns.Add("ContactRelationship");
                        dtEmergencyContact.Columns.Add("ContactNumber");
                        dtEmergencyContact.Columns.Add("Status");

                        var _count = TableEditEmergencyContactNew.Rows.Count;

                        foreach (GridViewRow _RowEC in TableEditEmergencyContactNew.Rows)
                        {
                            if (_RowEC.RowType == DataControlRowType.DataRow)
                            {
                                DataRow _dtRowEC = dtEmergencyContact.NewRow();

                                _dtRowEC["Oid"] = TableEditEmergencyContactNew.DataKeys[_RowEC.RowIndex]?.Values["Oid"]?.ToString() ?? "";
                                _dtRowEC["ContactName"] = CustomString(_RowEC.Cells[2].Text.Trim());
                                _dtRowEC["ContactAddress"] = _RowEC.Cells[3].Text.Trim();
                                _dtRowEC["ContactRelationship"] = _RowEC.Cells[4].Text.Trim();
                                _dtRowEC["ContactNumber"] = CustomString(_RowEC.Cells[5].Text.Trim());
                                _dtRowEC["Status"] = _RowEC.Cells[6].Text.Trim();

                                dtEmergencyContact.Rows.Add(_dtRowEC);
                            }
                        }
                        foreach (DataRow _drSP in dtEmergencyContact.Rows)
                        {
                            if (_drSP["Oid"].ToString().Length > 0)
                            {
                                sqlcomm.CommandType = CommandType.StoredProcedure;
                                sqlcomm.Parameters.AddWithValue("@StatementType", "SaveEmergencyContact");

                                sqlcomm.Parameters.AddWithValue("@Contact_Oid", _drSP["Oid"]);
                                sqlcomm.Parameters.AddWithValue("@id", CustomString(lblEmployeeID.Value));
                                sqlcomm.Parameters.AddWithValue("@ContactName", CustomString(_drSP["ContactName"].ToString()));
                                sqlcomm.Parameters.AddWithValue("@ContactAddress", CustomString(_drSP["ContactAddress"].ToString()));
                                sqlcomm.Parameters.AddWithValue("@ContactNumber", CustomString(_drSP["ContactNumber"].ToString()));
                                sqlcomm.Parameters.AddWithValue("@ContactRelationship", _drSP["ContactRelationship"]);
                                sqlcomm.Parameters.AddWithValue("@Contact_Status", _drSP["Status"]);
                                sqlcomm.Parameters.AddWithValue("@createdby", CustomString(txtCreateBy.Value));
                                sqlcomm.Parameters.AddWithValue("@modifiedby", CustomString(txtModifiedBy.Value));

                                sqlcomm.ExecuteNonQuery();
                                sqlcomm.Parameters.Clear();
                            }
                        }
                    }

                    if (TableEditSuratPeringatan.Rows.Count > 0)
                    {
                        DataTable dtSuratPeringatan = new DataTable();
                        dtSuratPeringatan.Columns.Add("oid");
                        dtSuratPeringatan.Columns.Add("SuratPeringatan");
                        dtSuratPeringatan.Columns.Add("StartDate");
                        dtSuratPeringatan.Columns.Add("EndDate");
                        dtSuratPeringatan.Columns.Add("Remarks");
                        dtSuratPeringatan.Columns.Add("ActiveSP");
                        dtSuratPeringatan.Columns.Add("Status");

                        foreach (GridViewRow _RowSp in TableEditSuratPeringatan.Rows)
                        {
                            if (_RowSp.RowType == DataControlRowType.DataRow)
                            {
                                DataRow _dtRowSP = dtSuratPeringatan.NewRow();
                                _dtRowSP["oid"] = TableEditSuratPeringatan.DataKeys[_RowSp.RowIndex]?.Values["oid"]?.ToString() ?? "";
                                _dtRowSP["SuratPeringatan"] = CustomString(_RowSp.Cells[2].Text.Trim());
                                _dtRowSP["StartDate"] = _RowSp.Cells[3].Text.Trim();
                                _dtRowSP["EndDate"] = _RowSp.Cells[4].Text.Trim();
                                _dtRowSP["Remarks"] = CustomString(_RowSp.Cells[5].Text.Trim());
                                _dtRowSP["ActiveSP"] = _RowSp.Cells[6].Text.Trim();
                                _dtRowSP["Status"] = TableEditSuratPeringatan.DataKeys[_RowSp.RowIndex]?.Values["Status"]?.ToString() ?? "";

                                dtSuratPeringatan.Rows.Add(_dtRowSP);
                            }
                        }
                        foreach (DataRow _drSP in dtSuratPeringatan.Rows)
                        {
                            if (_drSP["oid"].ToString().Length > 0)
                            {
                                sqlcomm.CommandType = CommandType.StoredProcedure;
                                sqlcomm.Parameters.AddWithValue("@StatementType", "SaveSuratPeringatan");

                                string _test1 = _drSP["oid"].ToString();
                                string _test2 = CustomString(lblEmployeeID.Value.Trim());
                                string _test3 = CustomString(lblNIK.Value.Trim());
                                string _test4 = _drSP["ActiveSP"].ToString();

                                sqlcomm.Parameters.AddWithValue("@OidSP", _drSP["oid"]);
                                sqlcomm.Parameters.AddWithValue("@id", CustomString(lblEmployeeID.Value.Trim()));
                                sqlcomm.Parameters.AddWithValue("@SuratPeringatan", _drSP["SuratPeringatan"]);
                                sqlcomm.Parameters.AddWithValue("@StartDateSP", _drSP["StartDate"]);
                                sqlcomm.Parameters.AddWithValue("@EndDateSP", _drSP["StartDate"]);
                                sqlcomm.Parameters.AddWithValue("@RemarkSP", _drSP["Remarks"]);
                                sqlcomm.Parameters.AddWithValue("@ActiveSP", _drSP["ActiveSP"]);
                                sqlcomm.Parameters.AddWithValue("@createdby", CustomString(txtCreateBy.Value));
                                sqlcomm.Parameters.AddWithValue("@modifiedby", CustomString(txtModifiedBy.Value));
                                sqlcomm.Parameters.AddWithValue("@NIK", CustomString(lblNIK.Value.Trim()));

                                sqlcomm.ExecuteNonQuery();
                                sqlcomm.Parameters.Clear();
                            }
                        }
                    }
                }

                // Create a DataTable with the same columns as your GridView
                DataTable dtCareerHistory = new DataTable();
                dtCareerHistory.Columns.Add("id");
                dtCareerHistory.Columns.Add("nik");
                dtCareerHistory.Columns.Add("FullName");
                dtCareerHistory.Columns.Add("Title");
                dtCareerHistory.Columns.Add("Division");
                dtCareerHistory.Columns.Add("Section");
                dtCareerHistory.Columns.Add("Location");
                dtCareerHistory.Columns.Add("Category");
                dtCareerHistory.Columns.Add("ModifiedBy");
                dtCareerHistory.Columns.Add("modification_date");
                dtCareerHistory.Columns.Add("Effectivedate");

                // Loop through GridView rows
                foreach (GridViewRow row in TableEditCareerHistory.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DataRow dr = dtCareerHistory.NewRow();

                        // Example: get text from cells (adjust indexes as needed)
                        dr["id"] = TableEditCareerHistory.DataKeys[row.RowIndex]?.Value?.ToString() ?? "";
                        dr["nik"] = row.Cells[1].Text.Trim();
                        dr["FullName"] = row.Cells[2].Text.Trim();
                        dr["Title"] = row.Cells[3].Text.Trim();
                        dr["Division"] = row.Cells[4].Text.Trim();
                        dr["Section"] = row.Cells[5].Text.Trim();
                        dr["Location"] = row.Cells[6].Text.Trim();


                        // If Category is a DropDownList in a TemplateField
                        //DropDownList ddlCategory = row.FindControl("ddlCategory") as DropDownList;
                        //dr["Category"] = ddlCategory != null ? ddlCategory.SelectedValue : row.Cells[7].Text.Trim();

                        DropDownList ddlCategory = row.FindControl("ddlCategory") as DropDownList;
                        if (ddlCategory != null)
                        {
                            dr["Category"] = ddlCategory.SelectedValue;
                        }
                        else
                        {
                            // Optionally handle the case where the control is missing
                            dr["Category"] = string.Empty;
                        }
                        TextBox txtEffectiveDate = row.FindControl("txtEffectiveDate") as TextBox;

                        if (txtEffectiveDate != null)
                        {
                            dr["Effectivedate"] = txtEffectiveDate.Text;
                        }
                        else
                        {
                            dr["Effectivedate"] = DBNull.Value;
                        }
                        dr["ModifiedBy"] = row.Cells[8].Text.Trim();
                        dr["modification_date"] = row.Cells[9].Text.Trim();
                        

                        dtCareerHistory.Rows.Add(dr);
                    }
                }

                foreach (DataRow _vRow in dtCareerHistory.Rows)
                {
                    if (_vRow["id"].ToString().Length > 0)
                    {
                        sqlcomm.CommandType = CommandType.StoredProcedure;
                        sqlcomm.Parameters.AddWithValue("@StatementType", "UpdateHistoryMutationEmployee");
                        sqlcomm.Parameters.AddWithValue("@id_EmployeeHistory", _vRow["id"]);
                        sqlcomm.Parameters.AddWithValue("@nik", _vRow["nik"]);
                        sqlcomm.Parameters.AddWithValue("@CareerHistory", _vRow["Category"]);// Pass Category
                                                                                             // Add other parameters as needed
                        sqlcomm.Parameters.AddWithValue("@EffectiveDate", string.IsNullOrEmpty(_vRow["Effectivedate"].ToString()) ? (object)DBNull.Value : Convert.ToDateTime(_vRow["Effectivedate"]));
                        sqlcomm.ExecuteNonQuery();
                        sqlcomm.Parameters.Clear();
                    }
                }

                sqlcomm.Dispose();
                Con.Close();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "Func();", true);

                //}
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedSave();", true);
                //}

                BindDataEmployeeDetail();
            }
        }

        protected void btnAdd_eDoc_Click(object sender, EventArgs e)
        { 
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadeDocs').modal();", true);
            // Ensure the e-Docs tab remains open after postback
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "activateEDocTab", "$('#myTabEmployeeDetail a[href=\"#tabeDoc\"]').tab('show');", true);
        }
        protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileUploadEDocs.Enabled = true;
            txtDescription.Value = ddlDocType.SelectedItem.Value;
            txtDescription.Disabled = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadeDocs').modal();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "activateEDocTab", "$('#myTabEmployeeDetail a[href=\"#tabeDoc\"]').tab('show');", true);
        }

        protected void btnDownload_eDoc_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the index of the clicked row
                int dataIndex = Convert.ToInt32((sender as LinkButton).CommandArgument);
                GridViewRow clickedRow = TableeDocsFiles.Rows[dataIndex];

                // Extract file details from the clicked row
                //string module = clickedRow.Cells[2].Text;
                string NIK = lblNIK.Value; //clickedRow.Cells[7].Text;
                //string docType = clickedRow.Cells[3].Text;
                string fileName = clickedRow.Cells[5].Text;

                // Construct the file path

                //serverfolder = Server.MapPath("~/eDocs_Files/Employee" + "/" + _vNIK + "/" + _vDocType + "/");

                string serverFolderPath = Path.Combine(Server.MapPath("~/eDocs_Files/Employee"), NIK, "SuratPeringatan");
                string filePath = Path.Combine(serverFolderPath, fileName);

                // Check if the file exists and handle the download
                if (File.Exists(filePath))
                {
                    Response.Clear();
                    Response.ContentType = MimeMapping.GetMimeMapping(fileName); // Dynamically determine MIME type
                    Response.AppendHeader("Content-Disposition", $"attachment; filename=\"{fileName}\"");
                    Response.TransmitFile(filePath);
                    Response.End();
                    ViewEdocsFilesUpload();
                }
                else
                {
                    // Notify the user if the file does not exist
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "toastr.error('Download failed, File not found!');", true);
                    ViewEdocsFilesUpload();
                }
            }
            catch (Exception ex)
            {
                // Log the error and notify the user
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", $"toastr.error('An error occurred: {ex.Message}');", true);
                ViewEdocsFilesUpload();
            }
        }

        protected void btnHapus_eDoc_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the index of the clicked row
                int dataIndex = Convert.ToInt32((sender as LinkButton).CommandArgument);
                GridViewRow clickedRow = TableeDocsFiles.Rows[dataIndex];

                // Extract necessary values from the clicked row
                string oid = TableeDocsFiles.DataKeys[dataIndex]?.Value?.ToString(); //clickedRow.Cells[0].Text;
                string user = Session["nik"]?.ToString();

                // Ensure the connection string is valid
                string connectionString = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;

                // Use 'using' statements for proper resource disposal
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand sqlCommand = new SqlCommand("sp_HRIS_eDocs", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    sqlCommand.Parameters.AddWithValue("@Flag", "Delete");
                    sqlCommand.Parameters.AddWithValue("@eDoc_ID", oid);
                    sqlCommand.Parameters.AddWithValue("@User", user);

                    // Open connection and execute the command
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }

                // Notify the user of success and redirect
                //string script = $@"
                //                $(document).ready(function() {{
                //                    toastr.success('Your operation was successful !', 'Submit Success');
                //                    setTimeout(function() {{
                //                        window.location.href = 'ffis_aff_cs_detail.aspx?ShipmentID={shipmentID}';
                //                    }}, 2000);
                //                }});
                //            ";
                ViewEdocsFilesUpload();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastrRedirect", script, true);
            }
            catch (Exception ex)
            {
                // Log the error and notify the user
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", $"toastr.error('An error occurred: {ex.Message}');", true);
                ViewEdocsFilesUpload();
            }
        }

        protected void btnCloseModalImport_Click(object sender, EventArgs e)
        {

        }

        protected void btnUploadFoto_Click(object sender, EventArgs e)
        {
            
            if (FileUploadFoto.HasFiles)
            {
                if (lblNIK.Value.Length > 0)
                {
                    int filecount = 0;
                    int fileuploadcount = 0;

                    string _PhotoName = string.Empty;

                    if (lblPhoto.Value.Length > 0) { lblPhoto.Value = lblNIK.Value + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"); }

                    //check No of Files Selected  
                    filecount = FileUploadFoto.PostedFiles.Count();
                    if (filecount <= 1)
                    {
                        foreach (HttpPostedFile postfiles in FileUploadFoto.PostedFiles)
                        {
                            ////Get The File Extension  
                            ////string filetype = Path.GetExtension(postfiles.FileName);
                            //string filetype = Path.GetExtension(postfiles.FileName).ToLower();
                            //if (filetype == ".jpeg" || filetype == ".jpg" || filetype == ".png")
                            //{
                            //    //Get The File Size In Bite  
                            //    double filesize = postfiles.ContentLength;
                            //    if (filesize < (2048576))
                            //    {
                            //        fileuploadcount++;
                            //        string serverfolder = string.Empty;
                            //        string serverpath = string.Empty;
                            //        // Adding File Into Scecific Folder Depend On his Extension
                            //        switch (filetype)
                            //        {
                            //            case ".png":
                            //            case ".jpeg":
                            //            case ".jpg":
                            //                serverfolder = Server.MapPath("~/Photos/");
                            //                //check Folder avlalible or not  
                            //                if (!Directory.Exists(serverfolder))
                            //                {
                            //                    // create Folder  
                            //                    Directory.CreateDirectory(serverfolder);
                            //                }
                            //                serverpath = serverfolder + lblPhoto.Value + filetype;
                            //                FileUploadFoto.SaveAs(serverpath);
                            //                label1.Text += "[" + postfiles.FileName + "]- jpeg/jpg file uploaded successfully<br/>";
                            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                            //                //BindDataTableEmployees();
                            //                UploadFotoOK.Value = "OK";

                            //                UpdatePhoto();

                            //                string Fromfile1 = "~/Photos/" + lblPhoto.Value.ToString() + filetype;
                            //                PhotoUser.ImageUrl = Fromfile1;

                            //                break;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaxSize();", true);
                            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                            //        //BindDataTableEmployees();
                            //    }
                            //}
                            //else
                            //{
                            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMustBePDF();", true);
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                            //    //BindDataTableEmployees();
                            //}

                            string filetype = Path.GetExtension(postfiles.FileName).ToLower();

                            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                            if (allowedExtensions.Contains(filetype))
                            {
                                double filesize = postfiles.ContentLength;

                                if (filesize < 2048576) // 2 MB
                                {
                                    string serverfolder = Server.MapPath("~/Photos/");

                                    if (!Directory.Exists(serverfolder))
                                    {
                                        Directory.CreateDirectory(serverfolder);
                                    }

                                    string serverpath = Path.Combine(serverfolder, lblPhoto.Value + filetype);

                                    FileUploadFoto.SaveAs(serverpath);

                                    PhotoUser.ImageUrl = "~/Photos/" + lblPhoto.Value + filetype;

                                    UploadFotoOK.Value = "OK";
                                    UpdatePhoto();
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(
                                        this.GetType(), "text", "FailedMaxSize();", true);
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(
                                    this.GetType(), "text", "FailedMustBeImage();", true);
                            }
                        }
                    }
                    else
                    {

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedMaximumUpload();", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                        //BindDataTableEmployees();
                    }

                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "text", "FailedUpload();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal", "$('#mdlUploadFoto').modal();", true);
                    //BindDataTableEmployees();
                }
            }
        }



        protected void btnUploadeDoc_Click(object sender, EventArgs e)
        {
            CheckUploadDocument();
            if (lblOK.Value == "OK")
            {
                string _Category = string.Empty;
                if (ddlDocType.SelectedItem.Text == "SP" )
                {
                    _Category = "SuratPeringatan";
                }
                else
                { 
                    _Category = "OtherDocument";
                }


                string _vPath = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;
                SqlConnection _vSQLCon = new SqlConnection(_vPath);
                SqlCommand _vSQLComm = new SqlCommand();

                _vSQLCon.Open();
                _vSQLComm.Connection = _vSQLCon;
                _vSQLComm.CommandType = CommandType.StoredProcedure;
                _vSQLComm.CommandText = "sp_HRIS_eDocs";

                _vSQLComm.Parameters.AddWithValue("@Flag", "Save");
                _vSQLComm.Parameters.AddWithValue("@Module", "EmployeeDetail");
                _vSQLComm.Parameters.AddWithValue("@Description", txtDescription.Value);
                _vSQLComm.Parameters.AddWithValue("@NIK", lblNIK.Value);
                _vSQLComm.Parameters.AddWithValue("@Category", _Category);
                _vSQLComm.Parameters.AddWithValue("@DocumentType", ddlDocType.SelectedValue);
                _vSQLComm.Parameters.AddWithValue("@FileName", lblFileName.Value);
                _vSQLComm.Parameters.AddWithValue("@User", Session["nik"].ToString());

                _vSQLComm.ExecuteNonQuery();

                _vSQLComm.Parameters.Clear();
                _vSQLComm.Dispose();
                _vSQLCon.Dispose();
                _vSQLCon.Close();
            }
            ViewEdocsFilesUpload();
        }

        protected void btnHapus_eDoc_Other_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the index of the clicked row
                int dataIndex = Convert.ToInt32((sender as LinkButton).CommandArgument);
                GridViewRow clickedRow = TableeDocsFiles_Other.Rows[dataIndex];

                // Extract necessary values from the clicked row
                string oid = TableeDocsFiles_Other.DataKeys[dataIndex]?.Value?.ToString(); //clickedRow.Cells[0].Text;
                string user = Session["nik"]?.ToString();

                // Ensure the connection string is valid
                string connectionString = ConfigurationManager.ConnectionStrings["dbpath"].ConnectionString;

                // Use 'using' statements for proper resource disposal
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand sqlCommand = new SqlCommand("sp_HRIS_eDocs", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    sqlCommand.Parameters.AddWithValue("@Flag", "Delete");
                    sqlCommand.Parameters.AddWithValue("@eDoc_ID", oid);
                    sqlCommand.Parameters.AddWithValue("@User", user);

                    // Open connection and execute the command
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.Parameters.Clear();
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }

                // Notify the user of success and redirect
                //string script = $@"
                //                $(document).ready(function() {{
                //                    toastr.success('Your operation was successful !', 'Submit Success');
                //                    setTimeout(function() {{
                //                        window.location.href = 'ffis_aff_cs_detail.aspx?ShipmentID={shipmentID}';
                //                    }}, 2000);
                //                }});
                //            ";
                ViewEdocsFilesUpload();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ToastrRedirect", script, true);
            }
            catch (Exception ex)
            {
                // Log the error and notify the user
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", $"toastr.error('An error occurred: {ex.Message}');", true);
                ViewEdocsFilesUpload();
            }
        }

        protected void btnDownload_eDoc_Other_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the index of the clicked row
                int dataIndex = Convert.ToInt32((sender as LinkButton).CommandArgument);
                GridViewRow clickedRow = TableeDocsFiles_Other.Rows[dataIndex];

                // Extract file details from the clicked row
                //string module = clickedRow.Cells[2].Text;
                string NIK = lblNIK.Value; //clickedRow.Cells[7].Text;
                //string docType = clickedRow.Cells[3].Text;
                string fileName = clickedRow.Cells[5].Text;

                // Construct the file path

                //serverfolder = Server.MapPath("~/eDocs_Files/Employee" + "/" + _vNIK + "/" + _vDocType + "/");

                string serverFolderPath = Path.Combine(Server.MapPath("~/eDocs_Files/Employee"), NIK, "Others");
                string filePath = Path.Combine(serverFolderPath, fileName);

                // Check if the file exists and handle the download
                if (File.Exists(filePath))
                {
                    Response.Clear();
                    Response.ContentType = MimeMapping.GetMimeMapping(fileName); // Dynamically determine MIME type
                    Response.AppendHeader("Content-Disposition", $"attachment; filename=\"{fileName}\"");
                    Response.TransmitFile(filePath);
                    Response.End();
                    ViewEdocsFilesUpload();
                }
                else
                {
                    // Notify the user if the file does not exist
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "toastr.error('Download failed, File not found!');", true);
                    ViewEdocsFilesUpload();
                }
            }
            catch (Exception ex)
            {
                // Log the error and notify the user
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", $"toastr.error('An error occurred: {ex.Message}');", true);
                ViewEdocsFilesUpload();
            }
        }

        protected void TableEditSuratPeringatan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string oid = e.CommandArgument.ToString();
            if (e.CommandName == "EditRow")
            {
                // Find the row index by OID
                DataTable dt = ViewState["SuratPeringatanViewState"] as DataTable;
                if (dt != null)
                {
                    DataRow[] rows = dt.Select($"Oid = '{oid}'");
                    if (rows.Length > 0)
                    {
                        DataRow row = rows[0];
                        // Set the form fields
                        lblSuratPeringatanID.Value = row["Oid"].ToString();

                        // Set DropDownList by value (prefer Value over Text for reliability)
                        ListItem item = ddlSuratPeringatan.Items.FindByText(row["SuratPeringatan"].ToString());
                        if (item != null)
                            ddlSuratPeringatan.SelectedValue = item.Value;
                        else
                            ddlSuratPeringatan.SelectedIndex = 0;

                        txtStartDateSP.Value = row["StartDate"].ToString();
                        txtEndDateSP.Value = row["EndDate"].ToString();
                        txtRemarkSP.Value = row["Remarks"].ToString();

                        // Set ActiveSP dropdown if present
                        if (row.Table.Columns.Contains("ActiveSP"))
                        {
                            ListItem activeItem = ddlActiveSP.Items.FindByText(row["ActiveSP"].ToString());
                            if (activeItem != null)
                                ddlActiveSP.SelectedValue = activeItem.Value;
                            else
                                ddlActiveSP.SelectedIndex = 0;
                        }

                     
                    }
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                DataTable dt = ViewState["SuratPeringatanViewState"] as DataTable;
                if (dt != null)
                {
                    DataRow[] rows = dt.Select($"Oid = '{oid}'");
                    if (rows.Length > 0)
                    {
                        DataRow row = rows[0];
                        // Update the "status" column to "delete"
                        if (dt.Columns.Contains("status") && row["status"].ToString() == "new")
                        {
                            dt.Rows.Remove(row); // Remove new rows entirely
                        }
                        else
                        {
                            row["ActiveSP"] = "Non Active"; // Mark existing rows for deletion
                        }

                       
                    }
                    // Save and rebind
                    ViewState["SuratPeringatanViewState"] = dt;
                    TableEditSuratPeringatan.DataSource = dt;
                    TableEditSuratPeringatan.DataBind();


                }
            }

            // Ensure the tabWarningLetters stays open after postback
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "activateTabWarningLetters",
                "$('#myTabEmployeeDetail a[href=\"#tabWarningLetters\"]').tab('show');",
                true
            );

          
        }
        private bool GetActiveSPValue()
        {
            // Example: adjust according to your dropdown values
            // If ddlActiveSP.SelectedItem.Text == "Active", return true; else false
            return ddlActiveSP.SelectedItem.Text.Equals("Active", StringComparison.OrdinalIgnoreCase);
        }
        protected void btnUpdateSP_Click(object sender, EventArgs e)
        {
            // 1. Get the current DataTable from ViewState or create a new one


            DataTable dt;
            if (ViewState["SuratPeringatanViewState"] != null)
            {
                dt = (DataTable)ViewState["SuratPeringatanViewState"];
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("Oid");
                dt.Columns.Add("SuratPeringatan");
                dt.Columns.Add("StartDate");
                dt.Columns.Add("EndDate");
                dt.Columns.Add("Remarks");
                dt.Columns.Add("ActiveSP");
                dt.Columns.Add("CreatedBy");
                dt.Columns.Add("CreatedDate");
                dt.Columns.Add("modifiedBy");
                dt.Columns.Add("modifiedDate");
            }

            string oid = lblSuratPeringatanID.Value;
            bool isUpdate = !string.IsNullOrEmpty(oid);

            if (isUpdate)
            {
                // Try to find the row with the same Oid
                DataRow[] foundRows = dt.Select($"Oid = '{oid}'");
                if (foundRows.Length > 0)
                {
                    DataRow row = foundRows[0];
                    row["SuratPeringatan"] = ddlSuratPeringatan.SelectedItem.Text;
                    row["StartDate"] = txtStartDateSP.Value;
                    row["EndDate"] = txtEndDateSP.Value;
                    row["Remarks"] = txtRemarkSP.Value;
                    row["ActiveSP"] = ddlActiveSP.SelectedItem.Text; //GetActiveSPValue();
                    row["modifiedBy"] = Session["Fullname"]?.ToString();
                    row["modifiedDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                }
                else
                {
                    // Oid not found, treat as new
                    DataRow newRow = dt.NewRow();
                    newRow["Oid"] = Guid.NewGuid().ToString();
                    newRow["SuratPeringatan"] = ddlSuratPeringatan.SelectedItem.Text;
                    newRow["StartDate"] = txtStartDateSP.Value;
                    newRow["EndDate"] = txtEndDateSP.Value;
                    newRow["Remarks"] = txtRemarkSP.Value;
                    newRow["ActiveSP"] = ddlActiveSP.SelectedItem.Text;  //GetActiveSPValue();
                    newRow["CreatedBy"] = Session["Fullname"]?.ToString();
                    newRow["CreatedDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    newRow["modifiedBy"] = Session["Fullname"]?.ToString();
                    newRow["modifiedDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    newRow["status"] = "new"; // Mark as new
                    dt.Rows.InsertAt(newRow, 0);
                }
            }
            else
            {
                // Insert as new
                DataRow newRow = dt.NewRow();
                newRow["Oid"] = Guid.NewGuid().ToString();
                newRow["SuratPeringatan"] = ddlSuratPeringatan.SelectedItem.Text;
                newRow["StartDate"] = txtStartDateSP.Value;
                newRow["EndDate"] = txtEndDateSP.Value;
                newRow["Remarks"] = txtRemarkSP.Value;
                newRow["ActiveSP"] = ddlActiveSP.SelectedItem.Text;// GetActiveSPValue();
                newRow["CreatedBy"] = Session["Fullname"]?.ToString();
                newRow["CreatedDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                newRow["modifiedBy"] = Session["Fullname"]?.ToString();
                newRow["modifiedDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                newRow["status"] = "new"; // Mark as new
                dt.Rows.InsertAt(newRow, 0);
            }

            // Remove empty row if present (e.g., Oid is null or empty)
            foreach (DataRow dr in dt.Select("ISNULL(Oid, '') = ''"))
            {
                dt.Rows.Remove(dr);
            }

            // 3. Save back to ViewState and rebind
            ViewState["SuratPeringatanViewState"] = dt;
            TableEditSuratPeringatan.DataSource = dt;
            TableEditSuratPeringatan.DataBind();

            // Optionally, clear input fields after adding/updating
            lblSuratPeringatanID.Value = "";
            ddlSuratPeringatan.SelectedIndex = 0;
            txtStartDateSP.Value = "";
            txtEndDateSP.Value = "";
            txtRemarkSP.Value = "";
            ddlActiveSP.SelectedIndex = 0;

            // Ensure the tabWarningLetters stays open after postback
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "activateTabWarningLetters",
                "$('#myTabEmployeeDetail a[href=\"#tabWarningLetters\"]').tab('show');",
                true
            );




        }

        protected void btnNewSP_Click(object sender, EventArgs e)
        {
            lblSuratPeringatanID.Value = "";
            ddlSuratPeringatan.SelectedIndex = 0;
            ddlActiveSP.SelectedIndex = 0;
            txtStartDateSP.Value = "";
            txtEndDateSP.Value = "";
            txtRemarkSP.Value = "";
        }

        protected void btnNewEC_Click(object sender, EventArgs e)
        {
            lblEmergencyContactID.Value = "";
            txtEmergencyContactName.Value = "";
            txtEmergencyContactRelationship.Value = "";
            txtEmergencyContactNumber.Value = "";
            txtEmergencyContactAddress.Value = "";

            ViewState["EmergencyContactOid"] = null;
        }

        protected void btnUpdateEC_Click(object sender, EventArgs e)
        {
            // 1. Get or create the DataTable for Emergency Contacts
            DataTable dt;
            if (ViewState["EmergencyContactViewState"] != null)
            {
                dt = (DataTable)ViewState["EmergencyContactViewState"];
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("Oid");
                dt.Columns.Add("ContactName");
                dt.Columns.Add("ContactRelationship");
                dt.Columns.Add("ContactNumber");
                dt.Columns.Add("ContactAddress");
                dt.Columns.Add("status");
            }

            string oid = lblEmergencyContactID.Value;
            if (string.IsNullOrEmpty(oid) && ViewState["EmergencyContactOid"] != null)
            {
                oid = ViewState["EmergencyContactOid"].ToString();
            };
            bool isUpdate = !string.IsNullOrEmpty(oid);

            if (isUpdate)
            {
                // Try to find the row with the same Oid
                DataRow[] foundRows = dt.Select($"Oid = '{oid}'");
                if (foundRows.Length > 0)
                {
                    DataRow row = foundRows[0];
                    row["ContactName"] = txtEmergencyContactName.Value;
                    row["ContactRelationship"] = txtEmergencyContactRelationship.Value;
                    row["ContactNumber"] = txtEmergencyContactNumber.Value;
                    row["ContactAddress"] = txtEmergencyContactAddress.Value;
                    row["status"] = "updated";
                }
                else
                {
                    // Oid not found, treat as new
                    DataRow newRow = dt.NewRow();
                    newRow["Oid"] = oid;
                    newRow["ContactName"] = txtEmergencyContactName.Value;
                    newRow["ContactRelationship"] = txtEmergencyContactRelationship.Value;
                    newRow["ContactNumber"] = txtEmergencyContactNumber.Value;
                    newRow["ContactAddress"] = txtEmergencyContactAddress.Value;
                    newRow["status"] = "new";
                    dt.Rows.InsertAt(newRow, 0);
                }
            }
            else
            {
                // Insert as new
                DataRow newRow = dt.NewRow();
                newRow["Oid"] = Guid.NewGuid().ToString();
                newRow["ContactName"] = txtEmergencyContactName.Value;
                newRow["ContactRelationship"] = txtEmergencyContactRelationship.Value;
                newRow["ContactNumber"] = txtEmergencyContactNumber.Value;
                newRow["ContactAddress"] = txtEmergencyContactAddress.Value;
                newRow["status"] = "new";
                dt.Rows.InsertAt(newRow, 0);

              

            }

            // Remove empty row if present (e.g., Oid is null or empty)
            foreach (DataRow dr in dt.Select("ISNULL(Oid, '') = ''"))
            {
                dt.Rows.Remove(dr);
            }

            // 3. Save back to ViewState and rebind the GridView
            ViewState["EmergencyContactViewState"] = dt;
            TableEditEmergencyContactNew.DataSource = dt;
            TableEditEmergencyContactNew.DataBind();



            // 4. Optionally, clear the input fields
            lblEmergencyContactID.Value = "";
            txtEmergencyContactName.Value = "";
            txtEmergencyContactRelationship.Value = "";
            txtEmergencyContactNumber.Value = "";
            txtEmergencyContactAddress.Value = "";

            // 5. Keep the Emergency Contact tab open
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "activatetabEmergencyContactNew",
                "$('#myTabEmployeeDetail a[href=\"#tabEmergencyContactNew\"]').tab('show');",
                true
            );

            
        }

        protected void TableEditEmergencyContactNew_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string oid = e.CommandArgument.ToString();
            if (e.CommandName == "EditRow")
            {
                // Get the DataTable from ViewState
                DataTable dt = ViewState["EmergencyContactViewState"] as DataTable;
                if (dt != null)
                {
                    DataRow[] rows = dt.Select($"Oid = '{oid}'");
                    if (rows.Length > 0)
                    {
                        DataRow row = rows[0];
                        // Copy data to controls
                        lblEmergencyContactID.Value = row["Oid"].ToString();

                        ViewState["EmergencyContactOid"] = row["Oid"].ToString();

                        txtEmergencyContactName.Value = row["ContactName"].ToString();
                        txtEmergencyContactRelationship.Value = row["ContactRelationship"].ToString();
                        txtEmergencyContactNumber.Value = row["ContactNumber"].ToString();
                        txtEmergencyContactAddress.Value = row["ContactAddress"].ToString();
                    }
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                DataTable dt = ViewState["EmergencyContactViewState"] as DataTable;
                if (dt != null)
                {
                    DataRow[] rows = dt.Select($"Oid = '{oid}'");
                    if (rows.Length > 0)
                    {
                        DataRow row = rows[0];
                        // Update the "Status" column to "Delete"
                        if (dt.Columns.Contains("Status"))
                        {
                            row["Status"] = "Delete";
                        }
                        else
                        {
                            dt.Columns.Add("Status", typeof(string));
                            row["Status"] = "Delete";
                        }
                    }
                    // Save and rebind
                    ViewState["EmergencyContactViewState"] = dt;
                    TableEditEmergencyContactNew.DataSource = dt;
                    TableEditEmergencyContactNew.DataBind();
                }
            }
            // Ensure the Emergency Contact tab stays open after postback
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "activatetabEmergencyContactNew",
                "$('#myTabEmployeeDetail a[href=\"#tabEmergencyContactNew\"]').tab('show');",
                true
            );
        }

        protected void TableEditEmergencyContactNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the DataRowView for this row
                var drv = e.Row.DataItem as System.Data.DataRowView;
                if (drv != null && drv.DataView.Table.Columns.Contains("Status"))
                {
                    string status = drv["Status"]?.ToString();
                    if (!string.IsNullOrEmpty(status) && status.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set background color to dark orange
                        e.Row.BackColor = System.Drawing.Color.DarkOrange;
                    }
                }

                TableEditEmergencyContactNew.UseAccessibleHeader = true;
                TableEditEmergencyContactNew.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        protected void btnCloseModalcandidatejoin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this,this.GetType(),"closeCandidateJoinModal","$('#mdlTableCandidateJoin').modal('hide');",true);
        }
        protected void TableCandidateJoin_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void TableCandidateJoin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeCandidateJoinModal", "$('#mdlTableCandidateJoin').modal('hide');", true);
            BindDataTableCandidateJoined();
            //BindDataTableEmployees();
            txtFullname.Value = TableCandidateJoin.SelectedRow.Cells[5].Text.ToString();
            hlbKTP.Value = TableCandidateJoin.SelectedRow.Cells[6].Text.ToString();
            txtBirthDate.Value = TableCandidateJoin.SelectedRow.Cells[16].Text.ToString();
            hlbURLPhoto.Value = TableCandidateJoin.SelectedRow.Cells[19].Text.ToString();
            txtAddress.Value = TableCandidateJoin.SelectedRow.Cells[10].Text.ToString();
        }

        protected void TableCandidateJoin_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
    
}