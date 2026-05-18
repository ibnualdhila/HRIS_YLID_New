<%@ Page Title="Employment Agreement" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="employee_agreement.aspx.cs" Inherits="hris_ylid.employee_agreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

            
        .dataTables_filter input {
            border: 3px solid darkblue !important;
            background-color: #f0f8ff !important;
            padding: 6px 10px !important;
            font-weight: bold !important;
            height:20px;
        }
     
        /* Tambahan efek saat fokus */
        .dataTables_filter input:focus {
            outline: none;
            border-color: midnightblue !important; /* biru terang */
            box-shadow: 0 0 5px rgba(0,123,255,0.5);
        }

        /* Styling dropdown "Show entries" */
    .dataTables_length select {
        border: 3px solid darkblue !important;
        background-color: #f0f8ff !important;
        padding: 4px 8px !important;
        font-weight: bold !important;
        height: 32px;
        border-radius: 4px;
        cursor: pointer;
    }

    /* Efek saat fokus */
    .dataTables_length select:focus {
        outline: none;
        border-color: midnightblue !important;
        box-shadow: 0 0 5px rgba(0,123,255,0.5);
    }


    .dataTables_filter input,
    .dataTables_length select {
        height: 32px;
    }

    </style>

    <link href="vendors/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript" src="vendors/date/JS/jquery-1.10.2.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.js"></script>

    <script type="text/javascript">
        function FuncUpdate() {
            swal({
                title: 'Update Success',
                text: 'Employee agreement Successfully Updated',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'employee_agreement.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncSave() {
            swal({
                title: 'Save Success',
                text: 'Employee agreement Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'employee_agreement.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncCancel() {
            swal({
                title: 'Cancel Success',
                text: 'Request form has been canceled',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'employee_agreement.aspx';
                }
            );
        }
    </script>

    <script type="text/javascript">
        function FieldAgreementNumber() {
            swal('Save Failed!', 'Please field Agreement Number', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldAgreementDate() {
            swal('Save Failed!', 'Please field Agreement Date', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldDescription() {
            swal('Save Failed!', 'Please field Description', 'error');
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
            <h1 class="page-header text-overflow" style="color: white;">Employment Agreement</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;HR Agreement</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Employment Agreement</li>
        </ol>
    </div>

    <asp:HiddenField ID="hlbNIK" runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Employment Agreements</legend>
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
                                                <%--<asp:LinkButton runat="server" ID="btnGenExcel" CssClass="btn mb-1 buttonColor" OnClick="btnGenExcel_Click"><i class="fa fa-file-excel-o"></i> Generate .xlsx</asp:LinkButton>--%>
                                            </div>
                                            <div class='col-sm-12'>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="TableEmployeeAgreement" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnSelectedIndexChanged="TableEmployeeAgreement_SelectedIndexChanged" OnRowDataBound="TableEmployeeAgreement_RowDataBound">
                                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
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
                                                            <asp:BoundField DataField="nik_employee" HeaderText="Employee ID Number" />
                                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name of Employee" />
                                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                                            <asp:BoundField DataField="JoinDate" HeaderText="Join Date" />
                                                            <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />
                                                            <asp:BoundField DataField="WorkingStatus" HeaderText="Employment Status" />
                                                            <asp:BoundField DataField="nik_pic" HeaderText="PIC Employee Number" />
                                                            <asp:BoundField DataField="PIC" HeaderText="Name of PIC" />
                                                            <asp:BoundField DataField="end_of_agreement_date" HeaderText="End of Agreement" />
                                                            <asp:BoundField DataField="description" HeaderText="Description" />
                                                            <asp:BoundField DataField="DayToEndOfAgreement" HeaderText="Days until End of Agreement" />
                                                            <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                                                            <asp:BoundField DataField="notification_to_email" HeaderText="Start Email Notification" />
                                                            <asp:BoundField DataField="email" HeaderText="Email Address" />
                                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                                            <asp:BoundField DataField="createby" HeaderText="Created by" />
                                                            <asp:BoundField DataField="create_date" HeaderText="Creation Date" />
                                                            <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                                                            <asp:BoundField DataField="modified_date" HeaderText="Modification Date" />
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
    <div class="modal fade bs-example-modal-lg" id="mdlAddNewEmployeeAgreement" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1q">Add New Employment Agreement</h4>
                    <button type="button" onclick="<%=btnCloseModalNew.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalNew" OnClick="btnCloseModalNew_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
					</button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-6'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Detailed Employment Agreement</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <asp:HiddenField ID="hlbID" runat="server" />
                                        <asp:HiddenField ID="hlbCodeLocation" runat="server" />
                                        <div class='col-sm-6'>
                                            Agreement Number
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtAgreementNumber" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type agreement number...">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Name of Employee
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlEmployee" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Employee Status
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtEmployeeStatus" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Employee Status" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Employment Status
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtWorkingStatus" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Employment Status" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Location
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtLocation" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Location" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Join Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtJoinDate" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Join Date" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            End of Agreement
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtEndOfAgreementDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="End of Agreement Date">
                                                    <%--<input type="text" class="form-control datepicker1" id="txtEndOfAgreementDate" runat="server" placeholder="mm/dd/yyyy"> <span class="input-group-append"><span class="input-group-text"><i class="mdi mdi-calendar-check"></i></span></span>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Name of PIC
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
                                            Description
											<div class="form-group">
                                                <div class='input-group'>
                                                    <textarea class="form-control h-150px" rows="2" id="txtDescription" runat="server" placeholder="Type description..."></textarea>
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
                                        <div class='col-sm-6' id="divStatusAgreement" runat="server" visible="false">
                                            Status Agreement
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlStatus" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="false">
                                                        <asp:ListItem Enabled="true" Text="<Select Status>" Value="-1"></asp:ListItem>
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
                                                        <asp:ListItem Text="6 Months" Value="4"></asp:ListItem>
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
