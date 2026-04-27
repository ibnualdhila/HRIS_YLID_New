<%@ Page Title="Company Agreement" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="company_agreement.aspx.cs" Inherits="hris_ylid.company_agreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function myPopup(myURL, title, myWidth, myHeight) {
            var left = (screen.width - myWidth) / 2;
            var top = (screen.height - myHeight) / 4;
            var myWindow = window.open(myURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + myWidth + ', height=' + myHeight + ', top=' + top + ', left=' + left);
        }
    </script>

    <script type="text/javascript" src="date/JS/jquery-1.10.2.min.js"></script>
    <link href="Content/Ionicons/css/ionicons.min.css" rel="stylesheet" />
    <link href="vendors/sweetalert.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <script src="quixlab/plugins/tables/js/datatable/dataTables.fixedColumns.min.js"></script>
    <script src="quixlab/plugins/tables/js/datatable/dataTables.fixedColumns.js"></script>
    <script src="quixlab/plugins/timepicker/bootstrap-timepicker.min.js"></script>
    <script src="quixlab/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>
    <script src="quixlab/plugins/moment/moment.js"></script>
    <script src="quixlab/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>
    <link href="quixlab/plugins/bootstrap4-datetimepicker/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="quixlab/plugins/bootstrap4-datetimepicker/bootstrap-datetimepicker.min.js"></script>
    <link href="vendor/toastr/toastr.min.css" rel="stylesheet" />
    <script src="vendor/toastr/toastr.min.js"></script>
    <script type="text/javascript">
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "2000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    </script>
    <style>
        fieldset.scheduler-border {
            border: 1px groove #ff6d10 !important;
            padding: 0 1.4em 1.4em 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }

        legend.scheduler-border {
            width: inherit; /* Or auto */
            padding: 0 10px; /* To give a bit of padding on the left and right */
            border-bottom: none;
            color: #06183d;
        }

        .buttonColor {
            background-color: #06183d;
            color: white;
        }

            .buttonColor:hover {
                background-color: #ff6d10;
                color: white;
            }

        .buttonColorGridview {
            background-color: #ff6d10;
            color: white;
        }

            .buttonColorGridview:hover {
                background-color: #06183d;
                color: white;
            }
    </style>
    <link href="vendors/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript">
        function FuncUpdate() {
            swal({
                title: 'Update Success',
                text: 'Company agreement Successfully Updated',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'company_agreement.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncSave() {
            swal({
                title: 'Save Success',
                text: 'Company agreement Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'company_agreement.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function DownloadFailed() {
            swal({
                title: 'Download Failed',
                text: 'FIle or folder not found',
                timer: '2000',
                type: 'error',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'company_agreement.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FailedMustBePDF() {
            swal('Save Failed!', 'File type must be jpeg', 'error');
        }
    </script>

    <script type="text/javascript">
        function FieldAgreementNumber() {
            swal('Save Failed!', 'Please field Agreement Number', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldNameAgreement() {
            swal('Save Failed!', 'Please field Name of Agreement', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldInstitutionServices() {
            swal('Save Failed!', 'Please field Institution Services', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldEffective() {
            swal('Save Failed!', 'Please field Effective Date', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldExpiration() {
            swal('Save Failed!', 'Please field Expiration Date', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldRemark() {
            swal('Save Failed!', 'Please field Remark', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldEmailAddress1() {
            swal('Save Failed!', 'Please field Email Address 1', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldEmailAddress2() {
            swal('Save Failed!', 'Please field Email Address 2', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldEmailAddress3() {
            swal('Save Failed!', 'Please field Email Address 3', 'error');
        }
    </script>

    <script type="text/javascript">
        function FailedCancel() {
            swal('Cancel Failed!', 'This form already completed / canceled / approved / rejected. Please try another form!', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedMaximumUpload() {
            swal('Save Failed!', 'Please select Maximum (6) files', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedMustBePDF() {
            swal('Save Failed!', 'File type must be pdf', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedMaxSize() {
            swal('Save Failed!', 'files not uploded size is greater then(2)MB', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedDownload() {
            swal('Download Failed!', 'files not found', 'error');
        }
    </script>
    <script type="text/javascript">
        function CheckVendor() {
            swal('Save Failed!', 'Please select Vendor first', 'error');
        }
    </script>
    <script type="text/javascript">
        function CheckPIC() {
            swal('Save Failed!', 'Please select PIC first', 'error');
        }
    </script>
    <script type="text/javascript">
        function CheckReminder() {
            swal('Save Failed!', 'Please select reminder time first', 'error');
        }
    </script>
    <script type="text/javascript">
        function CheckNotifEmail() {
            swal('Save Failed!', 'Please select start notification to email first', 'error');
        }
    </script>
    <style>
        .page-head {
            background-color: #06183d; /* Change this to your desired background color */
            color: white; /* Text color for the header */
            padding: 1px 2.938rem; /* Optional padding for the header */
            height: 182px;
            position: relative;
            margin-top: 0px; /* Adjust top margin to move the header down */
            margin-left: 0px; /* Adjust left margin to move the header right */
        }

        .custom-card-body {
            position: relative;
            top: -99px; /* Adjust the top position to move the card body down */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-head">
        <div id="page-title">
            <h1 class="page-header text-overflow" style="color: white;">Company Agreement</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;HR Agreement</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Company Agreement</li>
        </ol>
    </div>


    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Company Agreements</legend>
                                    <div class="control-group">
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <button type="button" onclick="<%=btnNew.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                    New
											<span class="btn-icon-right"><i class="fa fa-plus-circle"></i></span>
                                                </button>
                                                <asp:Button runat="server" Style="display: none;" ID="btnNew" OnClick="btnNew_Click"></asp:Button>
                                                <button type="button" onclick="<%=btnGenExcel.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                    Generate .xlsx
											<span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span>
                                                </button>
                                                <asp:Button runat="server" Style="display: none;" ID="btnGenExcel" OnClick="btnGenExcel_Click"></asp:Button>
                                            </div>

                                            <div class='col-sm-12'>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="TableCompanyAgreement" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableCompanyAgreement_RowDataBound" OnRowCommand="TableCompanyAgreement_RowCommand" OnSelectedIndexChanged="TableCompanyAgreement_SelectedIndexChanged">
                                                        <HeaderStyle BackColor="#06183d" ForeColor="White" />

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnEdit" CommandName="Ubah" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnEdit_Click" ToolTip="Edit"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Download Document">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDownload" CommandName="Download" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnDownload_Click" ToolTip="Download"><i class="fa fa-download"></i> Download</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="id" HeaderText="ID" />
                                                            <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                                                            <asp:BoundField DataField="agreement_name" HeaderText="Name of Agreement" />
                                                            <asp:BoundField DataField="id_institution" HeaderText="id_institution" />
                                                            <asp:BoundField DataField="Institution" HeaderText="Institution" />
                                                            <asp:BoundField DataField="institution_services" HeaderText="Institution Service" />
                                                            <asp:BoundField DataField="nik_pic" HeaderText="Employee ID Number of PIC" />
                                                            <asp:BoundField DataField="PIC" HeaderText="Name of PIC" />
                                                            <asp:BoundField DataField="effective_date" HeaderText="Effective Date" />
                                                            <asp:BoundField DataField="expiration_date" HeaderText="Expiration Date" />
                                                            <asp:BoundField DataField="DayToExpired" HeaderText="Days until Expiration Date" />
                                                            <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                                                            <asp:BoundField DataField="notification_to_email" HeaderText="Start Email Notification" />
                                                            <asp:BoundField DataField="email" HeaderText="Email Address" />
                                                            <asp:BoundField DataField="remaks" HeaderText="Remark" />
                                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                                            <asp:BoundField DataField="createby" HeaderText="Created by" />
                                                            <asp:BoundField DataField="create_date" HeaderText="Creation Date" />
                                                            <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                                                            <asp:BoundField DataField="modified_date" HeaderText="Modification Date" />
                                                            <asp:BoundField DataField="id location" HeaderText="id location" />
                                                            <asp:BoundField DataField="email2" HeaderText="Email Address 2" />
                                                            <asp:BoundField DataField="email3" HeaderText="Email Address 3" />
                                                        </Columns>

                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal New
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlAddNewCompanyAgreement" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1q">Add New Company Agreement</h4>
                    <button type="button" onclick="<%=btnCloseModalNew.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalNew" OnClick="btnCloseModalNew_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
					</button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-6'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Detailed Company Agreement</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <asp:HiddenField ID="hlbID" runat="server" />
                                        <div class='col-sm-6'>
                                            Agreement Number
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtAgreementNumber" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type agreement number...">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Name of Agreement
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtNameAggreement" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type name of agreement...">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Institution
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlVendor" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class='col-sm-6'>
                                            PIC
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlPIC" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlPIC_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'>
                                            Institution Service
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtVendorServices" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type institution of services...">
                                                </div>
                                            </div>
                                        </div>

                                        <div class='col-sm-6'>
                                            Effective Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtEffectiveDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Effective Date">
                                                    <%--<input type="text" class="form-control datepicker1" id="txtEffectiveDate" runat="server" placeholder="mm/dd/yyyy" > <span class="input-group-append"><span class="input-group-text"><i class="mdi mdi-calendar-check"></i></span></span>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Expiration Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtExpirationDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Expiration Date">
                                                    <%--<input type="text" class="form-control datepicker1" id="txtExpirationDate" runat="server" placeholder="mm/dd/yyyy" > <span class="input-group-append"><span class="input-group-text"><i class="mdi mdi-calendar-check"></i></span></span>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'>
                                            Remark
											<div class="form-group">
                                                <div class='input-group'>
                                                    <textarea class="form-control h-150px" rows="2" id="txtRemaks" runat="server" placeholder="Type remark..."></textarea>
                                                </div>
                                            </div>
                                        </div>

                                        <div class='col-sm-6'>
                                            Location
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlLocationCode" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlLocationCode_SelectedIndexChanged">
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Upload Document
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:FileUpload ID="FileUploadDocument" AllowMultiple="true" runat="server" />
                                                    <asp:Label ID="label1" runat="server" ForeColor="Green"></asp:Label>
                                                    <asp:HiddenField ID="hlbUploadOK" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6' id="divStatus" runat="server" visible="false">
                                            Status
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlStatus" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="false">
                                                        <asp:ListItem Enabled="true" Text="<Select Type>" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="ACTIVE" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="NON ACTIVE" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="CANCEL" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                        <div class='col-sm-6'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Set Reminder</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <asp:HiddenField ID="hlbDayToExpired" runat="server" />
                                        <div class='col-sm-6'>
                                            Recurring Reminder
											<div class="form-group">
                                                <div class='input-group'>
                                                    <%--<asp:DropDownList ID="ddlReminder" class="selectpicker form-control" data-show-subtext ="true" data-live-search ="true" AppendDataBoundItems="true"
													runat="server" OnSelectedIndexChanged="ddlReminder_SelectedIndexChanged">
														<asp:ListItem Enabled="true" Text="<Select Reminder>" Value="-1"></asp:ListItem>
														<asp:ListItem Text="DAILY" Value="1"></asp:ListItem>
														<asp:ListItem Text="WEEKLY" Value="2"></asp:ListItem>
														<asp:ListItem Text="MONTHLY" Value="3"></asp:ListItem>
													</asp:DropDownList>--%>
                                                    <input runat="server" id="txtReminderTime" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Recurring Reminder" value="WEEKLY" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Start Email Notification
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlStartNotificationEmail" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlStartNotificationEmail_SelectedIndexChanged">
                                                        <asp:ListItem Enabled="true" Text="<Select Start>" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="1 Month" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="2 Months" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'>
                                            Email To
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtEmailAddress" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type email to (xxxx.xxxx@id.yusen-logistics.com)">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'>
                                            Email Cc - 1
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtEmailAddress2" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type email cc - 1 (xxxx.xxxx@id.yusen-logistics.com)">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'>
                                            Email Cc - 2
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtEmailAddress3" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type email cc - 2 (xxxx.xxxx@id.yusen-logistics.com)">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'>
                                            &nbsp;
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:Button runat="server" Visible="false" ID="btnSubmit" CssClass="btn buttonColor" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
                                                    <asp:Button runat="server" Visible="false" ID="btnUpdate" CssClass="btn buttonColor" Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Reff Records</legend>
                                    <div class="control-group">
                                        <div class="row">
                                            <div class='col-sm-6'>
                                                Created by
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtCreateBy" data-validate-length-range="5,15" type="text" class="form-control input-rounded" disabled>
                                                </div>
                                            </div>
                                            </div>
                                            <div class='col-sm-6'>
                                                Creation Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtCreateDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded" disabled>
                                                </div>
                                            </div>
                                            </div>
                                            <div class='col-sm-6'>
                                                Modified by
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtModifiedBy" data-validate-length-range="5,15" type="text" class="form-control input-rounded" disabled>
                                                </div>
                                            </div>
                                            </div>
                                            <div class='col-sm-6'>
                                                Modification Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtModifiedDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded" disabled>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="modal-footer">


                    <%--<button type="button" data-dismiss="modal" class="btn mb-1 buttonColor">Close <span class="btn-icon-right">
					<i class="fa fa-close"></i></span></button>--%>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal Generate Range Date
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlGenerateExcel" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel112">Input Date Range</h4>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
			</button>--%>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class='col-sm-12'>
                                Date Range
					<div class="form-group">
                        <div class='input-group'>
                            <input type="text" id="txtStartDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Start Date">
                            <input type="text" id="txtEndDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="End Date">
                        </div>
                    </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" onclick="<%=btnGenerate.ClientID %>.click()" class="btn mb-1 buttonColor">Generate
			<span class="btn-icon-right"><i class="fa fa-check-circle-o"></i></span></button>
			<asp:Button runat="server" Style="display: none;" ID="btnGenerate" CausesValidation="False" OnClick="btnGenerate_Click"></asp:Button>--%>
                    <asp:LinkButton runat="server" ID="btnSubmitGenerate" CssClass="btn mb-1 buttonColor" OnClick="btnSubmitGenerate_Click"><i class="fa fa-check-circle-o"></i> Generate</asp:LinkButton>
                    <button type="button" onclick="<%=btnCloseModalGenerateRange.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Close <span class="btn-icon-right">
                            <i class="fa fa-close"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalGenerateRange" OnClick="btnCloseModalGenerateRange_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="vendors/date/JS/jquery-1.10.2.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.js"></script>

    <script src="./plugins/timepicker/bootstrap-timepicker.min.js"></script>
    <script src="./plugins/bootstrap-daterangepicker/daterangepicker.js"></script>
    <script src="./plugins/moment/moment.js"></script>
    <script src="./plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".grid").DataTable(
                {
                    scrollY: "500px",
                    scrollX: true,
                    scrollCollapse: true,
                    paging: true
                    //fixedColumns: {
                    //	left: 2
                    //}

                });
        });
    </script>
    <script>
        $(document).ready(function () {
            $(".datepicker1").datepicker({ format: 'yyyy/mm/dd', autoclose: true, todayBtn: 'linked' })
        });
    </script>
</asp:Content>
