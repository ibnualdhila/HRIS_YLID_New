<%@ Page Title="Employees Detail" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="employeess_detail.aspx.cs" Inherits="hris_ylid.employeess_detail" %>

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
        #TableEditCareerHistory {
            table-layout: fixed;
            width: 100%;
        }

            #TableEditCareerHistory th,
            #TableEditCareerHistory td {
                white-space: nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
                padding: 4px 8px;
                font-size: 13px;
                vertical-align: middle;
                height: 36px; /* reasonable for dropdowns */
            }

            #TableEditCareerHistory select {
                width: 100%;
                min-width: 100px;
                height: 28px;
                font-size: 13px;
                padding: 2px 6px;
                box-sizing: border-box;
            }

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
    <script>
        function showSucces() {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "progressBar": true,
                "preventDuplicates": false,
                "positionClass": "toast-bottom-right",
                "showDuration": "400",
                "hideDuration": "1000",
                "timeOut": "7000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr["success"]("SAVE SUCCESS, New Employee Successfully Submited");

        }
    </script>
    <script>
        function UpdateSucces() {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "progressBar": true,
                "preventDuplicates": false,
                "positionClass": "toast-bottom-right",
                "showDuration": "400",
                "hideDuration": "1000",
                "timeOut": "7000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr["success"]("UPDATE SUCCESS, Employee Successfully Updated");

        }
    </script>
    <link href="vendors/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript">
        function Func() {
            swal({
                title: 'Update Success',
                text: 'Data Employee Successfully Updated',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'employeess.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncSave() {
            swal({
                title: 'Save Success',
                text: 'Data Employee Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'employeess.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function UploadSuccess() {
            swal({
                title: 'Upload Success',
                text: 'Data Employee Successfully Uploaded',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'employeess.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function SelectPosition() {
            swal('Save Failed!', 'Position not yet selected, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectDivison() {
            swal('Save Failed!', 'Division not yet selected, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectSection() {
            swal('Save Failed!', 'Section not yet selected, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectSubSection() {
            swal('Save Failed!', 'Sub Section not yet selected, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectReportPosition() {
            swal('Save Failed!', 'Report Position not yet selected, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectLocation() {
            swal('Save Failed!', 'Location not yet selected, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectStatus() {
            swal('Save Failed!', 'Status not yet selected, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectCategory() {
            swal('Save Failed!', 'Category not yet selected, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function NameEmpty() {
            swal('Save Failed!', 'Fullname cannot be empty, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function NIKEmpty() {
            swal('Save Failed!', 'NIK cannot be empty, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedUpload() {
            swal('Save Failed!', 'Please select the file for upload, please try again', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedMaximumUpload() {
            swal('Save Failed!', 'Please select Maximum (1) files', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedMustBePDF() {
            swal('Save Failed!', 'File type must be jpeg', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedMaxSize() {
            swal('Save Failed!', 'files not uploded size is greater then(2)MB', 'error');
        }
    </script>

    <script type="text/javascript">
        function FailedSave() {
            swal('Save Failed!', 'No data is saved', 'error');
        }
    </script>

<%--    <script type="text/javascript">
        function fillSuratPeringatanFields(oid, name, address, relationship, phone) {
            if (!oid) {
                return;
            }
            document.getElementById('<%= lblSuratPeringatanID.ClientID %>').value = oid;
            document.getElementById('<%= ddlSuratPeringatan.ClientID %>').value = SuratPeringatan;
            document.getElementById('<%= txtStartDateSP.ClientID %>').value = StartDate;
            document.getElementById('<%= txtEndDateSP.ClientID %>').value = EndDate;
            document.getElementById('<%= txtRemarkSP.ClientID %>').value = Remarks;
            document.getElementById('<%= ddlActiveSP.ClientID %>').value = ActiveSP;

            document.getElementById("<%= txtEditHeaderWarningLetters.ClientID %>").innerText = "Edit Surat Peringatan";
        }
    </script>--%>

<%--    <script type="text/javascript">
        function fillEmergencyContactFields(oid, name, address, relationship, phone) {
            if (!oid) {
                return;
            }
            document.getElementById('<%= lblEmergencyContactID.ClientID %>').value = oid;
            document.getElementById('<%= txtEmergencyContactName.ClientID %>').value = name;
            document.getElementById('<%= txtEmergencyContactAddress.ClientID %>').value = address;
            document.getElementById('<%= txtEmergencyContactRelationship.ClientID %>').value = relationship;
            document.getElementById('<%= txtEmergencyContactNumber.ClientID %>').value = phone;

            document.getElementById("<%= txtEditHeaderEmergencyContact.ClientID %>").innerText = "Edit Emergency Contact List ";
        }
    </script>--%>

<%--    <script type="text/javascript">
        function clearSuratPeringatanFields() {
            document.getElementById('<%= lblSuratPeringatanID.ClientID %>').value = '';
            document.getElementById('<%= ddlSuratPeringatan.ClientID %>').value = '';
            document.getElementById('<%= txtStartDateSP.ClientID %>').value = '';
            document.getElementById('<%= txtEndDateSP.ClientID %>').value = '';
            document.getElementById('<%= txtRemarkSP.ClientID %>').value = '';
            document.getElementById('<%= ddlActiveSP.ClientID %>').value = '';

            document.getElementById("<%= txtEditHeaderWarningLetters.ClientID %>").innerText = "Add Surat Peringatan";
            // Show the modal if needed
            //$('#mdlAddNewEmployee').modal();
        }
    </script>--%>

<%--    <script type="text/javascript">
        function clearEmergencyContactFields() {
            document.getElementById('<%= lblEmergencyContactID.ClientID %>').value = '';
            document.getElementById('<%= txtEmergencyContactName.ClientID %>').value = '';
            document.getElementById('<%= txtEmergencyContactAddress.ClientID %>').value = '';
            document.getElementById('<%= txtEmergencyContactRelationship.ClientID %>').value = '';
            document.getElementById('<%= txtEmergencyContactNumber.ClientID %>').value = '';

            document.getElementById("<%= txtEditHeaderEmergencyContact.ClientID %>").innerText = "Add Emergency Contact List ";
            // Show the modal if needed
            //$('#mdlAddNewEmployee').modal();
        }
    </script>--%>

    <script type="text/javascript">
        var createdBy = '<%= Session["Fullname"] != null ? Session["Fullname"].ToString().Replace("'", "\\'") : "" %>';
        var createdDate = '<%= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %>';

        var modifiedBy = '<%= Session["Fullname"] != null ? Session["Fullname"].ToString().Replace("'", "\\'") : "" %>';
        var ModifiedDate = '<%= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %>';
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
            <h1 class="page-header text-overflow" style="color: white;">Employees</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Master Data</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;HR General</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Employees</li>
        </ol>
    </div>

    <div hidden="hidden">
        <asp:HiddenField ID="hlbKTPhiden" runat="server" />
        <asp:HiddenField ID="hlbURLPhoto" runat="server" />
        <asp:HiddenField ID="lblEmergencyContactID" runat="server" />

        <asp:HiddenField ID="lblEmergencyContactID_2" runat="server" />

        <asp:HiddenField ID="lblSuratPeringatanID" runat="server" />
        <asp:HiddenField ID="UserName" runat="server" />

        <asp:HiddenField ID="hfEmergencyContactData" runat="server" />
        <asp:HiddenField ID="hfSuratPeringatanData" runat="server" />
        <asp:HiddenField ID="hfCareerHistoryData" runat="server" />

        <asp:HiddenField ID="lblOK" runat="server" />
        <asp:HiddenField ID="lblCode" runat="server" />
        <asp:HiddenField ID="lblFileName" runat="server" />

        <asp:HiddenField ID="lblPhoto" runat="server" />
        <asp:HiddenField ID="lblNIK" runat="server" />
        <asp:HiddenField ID="lblEmployeeID" runat="server" />
        <asp:HiddenField ID="lblFlagSave" runat="server" />
        <asp:HiddenField ID="lblNewEmployee" runat="server" />


    </div>

    <asp:HiddenField ID="hlbID" runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <a href="employeess.aspx" style="float: right;" class="close" title="Close"><span>x</span></a>
                            </div>
                            <div class='col-sm-12'>
                                <div class="default-tab">
                                    <ul class="nav nav-tabs mb-3" id="myTabEmployeeDetail" role="tablist">
                                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#tabEmployeeDetail">Employee Detail</a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tabEmployeePersonalInformation">Employee Personal Information</a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tabEmployeeEducationInformation">Employee Education Information</a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tabEmployeeFamilyInformation">Employee Family Information</a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tabEmergencyContactNew">Emergency Contact Information</a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tabCareerHistory">Career History</a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tabTrainingHistory">Training History</a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tabWarningLetters">Warning Letters</a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tabeDoc">e-Docs</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class='col-sm-12'>
                                <div class="tab-content">
                                    <div class="tab-pane fade" id="tabWarningLetters" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">
                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">
                                                            <asp:Label ID="txtEditHeaderWarningLetters" runat="server" Text=""></asp:Label>
                                                        </legend>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <br />
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Surat Peringatan
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlSuratPeringatan" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true" runat="server">
                                                                                <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Surat Peringatan 1" Value="SP1"></asp:ListItem>
                                                                                <asp:ListItem Text="Surat Peringatan 2" Value="SP2"></asp:ListItem>
                                                                                <asp:ListItem Text="Surat Peringatan 3" Value="SP3"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Start Date
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txtStartDateSP" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="From Date">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    End Date
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txtEndDateSP" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="End Date">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-4'>
                                                                    Remark
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtRemarkSP" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Remark">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Active
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlActiveSP" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true" runat="server">
                                                                                <asp:ListItem Enabled="true" Text="Non Active" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <div class="form-group d-flex gap-2" style="display: flex; gap: 10px;">
                                                                        <asp:Button runat="server" ID="btnNewSP" CssClass="btn buttonColor" Text="New" OnClick="btnNewSP_Click" Style="min-width: 120px;" />
                                                                        <asp:Button runat="server" ID="btnUpdateSP" CssClass="btn buttonColor" Text="Update" OnClick="btnUpdateSP_Click" Style="min-width: 120px;" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <div class="form-group">
                                                                        <button type="button" class="btn mb-1 buttonColor" onclick="clearSuratPeringatanFields()">
                                                                            New
                                                                                <span class="btn-icon-right"><i class="fa fa-plus-circle"></i></span>
                                                                        </button>
                                                                        <button type="button" class="btn mb-1 buttonColor" onclick="addSuratPeringatanRow()">
                                                                            Save
                                                                                <span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span>
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                    </fieldset>
                                                    <fieldset class="scheduler-border">
                                                        <div class="row">
                                                            <div class='col-sm-12'>
                                                                <br />
                                                            </div>
                                                            <div class='col-sm-12'>
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="TableEditSuratPeringatan" runat="server"
                                                                        CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid"
                                                                        AutoGenerateColumns="False"
                                                                        Style="width: 100%"
                                                                        ShowHeaderWhenEmpty="true"
                                                                        EmptyDataText="No Record Found"
                                                                        DataKeyNames="oid,Status"
                                                                        OnRowCommand="TableEditSuratPeringatan_RowCommand"
                                                                        ClientIDMode="Static">
                                                                        <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                                        <Columns>
                                                                            
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditRow" CommandArgument='<%# Eval("oid") %>' CssClass="btn btn-sm buttonColorGridview" Text="Edit" />
                                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("oid") %>' CssClass="btn btn-sm btn-danger" Text="Non Active" OnClientClick="return confirm('Are you sure you want to delete this row?');" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:BoundField DataField="oid" HeaderText="oid" visible="false"/>
                                                                            <asp:BoundField DataField="SuratPeringatan" HeaderText="Surat Peringatan" />
                                                                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                                                                            <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                                            <asp:BoundField DataField="ActiveSP" HeaderText="Active" />
                                                                            <asp:BoundField DataField="Status" HeaderText="Status" Visible="false" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade show active" id="tabEmployeeDetail" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">
                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">
                                                            <asp:Label ID="txtEditHeaderEmployeeDetail" runat="server" Text=""></asp:Label>
                                                        </legend>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <br />
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="media align-items-center mb-4">
                                                                        <asp:Image ID="PhotoUser" class="mr-3 img-fluid" ImageUrl="~/images/img.png" runat="server" Width="108" Height="158" Style="object-fit: cover; border-radius: 8px;" />
                                                                        <%--<asp:Image ID="PhotoUser" class="mr-3" ImageUrl="~/images/img.png" runat="server" Width="80" Height="80" />--%>
                                                                        <div class="media-body">
                                                                            <asp:HiddenField ID="sds" runat="server" />
                                                                            <h3 class="mb-0">
                                                                                <asp:Label ID="lbDetailFullname" runat="server"></asp:Label></h3>
                                                                            <p class="text-muted mb-0">
                                                                                <asp:Label ID="lbDetailPosition" runat="server"></asp:Label>
                                                                            </p>
                                                                            <p class="text-muted mb-0">
                                                                                <asp:Label ID="hlbDetailNIK" runat="server"></asp:Label>
                                                                            </p>
                                                                            <asp:LinkButton ID="btnChangeFoto" runat="server" class="badge badge-pill badge-warning" OnClick="btnChangeFoto_Click" Text="">Change Photo</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                    <br />
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Full Name
				                                                    <div class="input-group">
                                                                        <input runat="server" id="txtFullname" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type full name or add from recruitment.." disabled>
                                                                        <asp:PlaceHolder ID="phAddButton" runat="server">
                                                                            <div class="input-group-append">
                                                                                <button type="button" class="btn buttonColor" onclick="<%=btnAdd.ClientID %>.click()">
                                                                                    <i class="fa fa-plus-circle"></i>Add</button>
                                                                                <asp:Button runat="server" Style="display: none;" ID="btnAdd" OnClick="btnAdd_Click"></asp:Button>
                                                                            </div>
                                                                        </asp:PlaceHolder>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Employee ID No.
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNIK" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Employee ID No." disabled>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Join Date
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txtJoinDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Join Date">
                                                                            <%--<input type="text" class="form-control datepicker1" id="txtJoinDate" runat="server" placeholder="mm/dd/yyyy"> <span class="input-group-append"><span class="input-group-text"><i class="mdi mdi-calendar-check"></i></span></span>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Status
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlStatus" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                                                <asp:ListItem Text="ACTIVE" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="NON ACTIVE" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                    <%--<br />--%>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Title
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlPosition" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Division/Branch
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlDivision" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Section
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlSection" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Subsection
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlSubSection" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlSubSection_SelectedIndexChanged">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                    <%--                                                                    <br />--%>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Report to Position
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlReportPosition" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlReportPosition_SelectedIndexChanged">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
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
                                                                <div class='col-sm-3'>
                                                                    Working Email
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtWorkingEmail" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Working Email">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3' id="divIntitution" runat="server">
                                                                    Institution Name
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlInstitution" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlInstitution_SelectedIndexChanged">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                    <%--<br />--%>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Source of Hire
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlSourceOfHire" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlSourceOfHire_SelectedIndexChanged">
                                                                                <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Job Boards" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Company Career Page" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Employee Referrals" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Headhunters" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Internal Hires" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="Campus Hiring" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="Social Media" Value="7"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Contract Start Date
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txtContractStartDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Contract Start Date">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Contract End Date
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txtContractEndDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Contract End Date">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Employment Status
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlCategory" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="CONTRACT" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="INTERN" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="OUTSOURCED" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="PROBATIONARY" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="PERMANENT" Value="5"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                    <%--<br />--%>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Career History
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlCareerHistory" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server">
                                                                                <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Promotion" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Mutation" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Rotation" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Demotion" Value="4"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Employement Separation
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlEmployementSeparation" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server">
                                                                                <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Resigned" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Not Passed Probation" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Pension" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Terminated" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Passed Away" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="End Of Contract" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="Back to Japan" Value="7"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Effecive End Date
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txtEffectiveEndDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="End Date">
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
                                    <div class="tab-pane fade" id="tabEmployeePersonalInformation" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">
                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">
                                                            <asp:Label ID="txtEditHeaderEmployeePersonalInfo" runat="server" Text=""></asp:Label>
                                                        </legend>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <br />
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Date of Birth
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txtBirthDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Date of birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Place of Birth
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtPOB" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Place Of Birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Gender
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlGender" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                                                                                <asp:ListItem Enabled="true" Text="<Select Value>" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Blood Type
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlBloodType" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlBloodType_SelectedIndexChanged">
                                                                                <asp:ListItem Enabled="true" Text="<Select Value>" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="A" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="B" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="AB" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="O" Value="4"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Nationality
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlNationality" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlNationality_SelectedIndexChanged">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    National ID Number
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="hlbKTP" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type National ID Number">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                </div>
                                                                <div class='col-sm-6'>
                                                                    Address
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <textarea class="form-control h-150px" rows="2" id="txtAddress" runat="server"></textarea>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-6'>
                                                                    Domicile
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <textarea class="form-control h-150px" rows="2" id="txtDomicile" runat="server"></textarea>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    City
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlCity" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true" runat="server">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Province
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlProvince" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true" runat="server">
                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Postal Code
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtPostalCode" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Postal Code">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Mobile Phone
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtMobilePhone" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Mobile Phone">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Personal Email
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtPersonalEmail" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Personal Email">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Family Card No.
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtFamilyCard" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Family Card No.">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Marital Status
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlMaritalStatus" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server">
                                                                                <asp:ListItem Enabled="true" Text="<Select Value>" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Single" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Married" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Divorced" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Widowed" Value="4"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Religion
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlReligion" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                                                runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged">
                                                                                <asp:ListItem Enabled="true" Text="<Select Value>" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Islam" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Catholic" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Christian" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Buddhist" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Hinduism" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="Confucianism" Value="6"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Bank Name
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtBankName" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Bank Name">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Bank Account No.
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtBankAccountNo" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Bank Account No.">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    Bank Account Name
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtBankAccountName" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Bank Account Name">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    BPJS TK No.
	                                                                <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtBPjamsostekNumber" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type BPJS TK No.">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    BPJS KS No.
	                                                                <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtBPJSKesehatanNo" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type BPJS KS NO.">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-2'>
                                                                    NPWP No.
	                                                                <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNPWP" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type NPWP No.">
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
                                    <div class="tab-pane fade" id="tabEmployeeEducationInformation" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">
                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">
                                                            <asp:Label ID="txtEditHeaderEmployeeEducation" runat="server" Text=""></asp:Label>
                                                        </legend>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <br />
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Education 1
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlEducation1" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true" runat="server">
                                                                                <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Elementary School" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Junior High School" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Senior High School" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Diploma 3" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Diploma 4" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="Bachelor Degree" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="Master Degree" Value="7"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Major 1
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtMajor1" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Major 1">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-6'>
                                                                    University / Institute / School 1
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtInstitute1" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Institute 1">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Education 2
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <asp:DropDownList ID="ddlEducation2" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true" runat="server">
                                                                                <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                                                <asp:ListItem Text="Elementary School" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Junior High School" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Senior High School" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Diploma 3" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Diploma 4" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="Bachelor Degree" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="Master Degree" Value="7"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Major 2
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtMajor2" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Major 1">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-6'>
                                                                    University / Institute / School 2
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtInstitute2" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Institute 2">
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
                                    <div class="tab-pane fade" id="tabEmployeeFamilyInformation" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">
                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">
                                                            <asp:Label ID="txtEditHeaderEmployeeFamilyInfo" runat="server" Text=""></asp:Label>
                                                        </legend>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <br />
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Spouse Name
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNameOfSpouse" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Spouse Name">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Spouse ID No.
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNoIDSpouse" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Spouse National ID">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Spouse Date of Birth
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txtSpouseDOB" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Date of birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    Spouse Place of Birth
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtSpousePOB" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Place Of Birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    1st Child Name
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNameOf1stChild" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type 1st Child Name">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    1st Child ID No.
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNoID1stChild" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type 1st Child National ID">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    1st Child Date of Birth
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txt1stChildDOB" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Date of birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    1st Child Place of Birth
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txt1stChildPOB" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Place Of Birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    2nd Child Name
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNameOf2ndChild" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type 1st Child Name">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    2nd Child ID No.
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNoID2ndChild" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type 1st Child National ID">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    2nd Child Date of Birth
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txt2ndChildDOB" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Date of birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    2nd Child Place of Birth
		                                                            <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txt2ndChildPOB" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Place Of Birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    3rd Child Name
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNameOf3rdChild" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type 1st Child Name">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    3rd Child ID No.
                                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txtNoID3rdChild" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type 1st Child National ID">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    3rd Child Date of Birth
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input type="text" id="txt3rdChildDOB" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Date of birth">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class='col-sm-3'>
                                                                    3rd Child Place of Birth
				                                                    <div class="form-group">
                                                                        <div class='input-group'>
                                                                            <input runat="server" id="txt3rdChildPOB" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Place Of Birth">
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
                                    <div class="tab-pane fade" id="tabEmergencyContactNew" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">
                                                <div class='col-sm-12'>
                                                    <%--<fieldset class="scheduler-border">
                                                        <div class="control-group">--%>
                                                    <%--<div class="row">--%>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <fieldset class="scheduler-border">
                                                                <legend class="scheduler-border">
                                                                    <asp:Label ID="txtEditHeaderEmergencyContact" runat="server" Text=""></asp:Label>
                                                                </legend>
                                                                <div class="row">
                                                                    <div class='col-sm-12'>
                                                                        <br />
                                                                    </div>
                                                                    <div class='col-sm-3'>
                                                                        Contact Name
                                                                        <div class="form-group">
                                                                            <div class='input-group'>
                                                                                <input runat="server" id="txtEmergencyContactName" clientidmode="Static" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Name">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class='col-sm-3'>
                                                                        Contact Relationship
                                                                        <div class="form-group">
                                                                            <div class='input-group'>
                                                                                <input runat="server" id="txtEmergencyContactRelationship" clientidmode="Static" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Relationship">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class='col-sm-3'>
                                                                        Contact Phone
                                                                        <div class="form-group">
                                                                            <div class='input-group'>
                                                                                <input runat="server" id="txtEmergencyContactNumber" clientidmode="Static" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Phone">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class='col-sm-12'>
                                                                        Contact Address
                                                                        <div class="form-group">
                                                                            <div class='input-group'>
                                                                                <input runat="server" id="txtEmergencyContactAddress" clientidmode="Static" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Contact Name">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class='col-sm-12'>
                                                                        <div class="form-group d-flex gap-2" style="display: flex; gap: 10px;">
                                                                            <asp:Button runat="server" ID="btnNewEC" CssClass="btn buttonColor" Text="New" OnClick="btnNewEC_Click" Style="min-width: 120px;" />
                                                                            <asp:Button runat="server" ID="btnUpdateEC" CssClass="btn buttonColor" Text="Update" OnClick="btnUpdateEC_Click" Style="min-width: 120px;" />
                                                                        </div>
                                                                    </div>
                                                                </div>

<%--                                                                <div class="row">
                                                                    <div class='col-sm-12'>
                                                                        <div class="form-group">
                                                                            <button type="button" class="btn mb-1 buttonColor" onclick="clearEmergencyContactFields()">
                                                                                New
                                                                                <span class="btn-icon-right"><i class="fa fa-plus-circle"></i></span>
                                                                            </button>
                                                                            <button type="button" class="btn mb-1 buttonColor" onclick="addEmergencyContactRow()">
                                                                                Save
                                                                                <span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                </div>--%>
                                                            </fieldset>

                                                            <fieldset class="scheduler-border">
                                                                <div class="row">
                                                                    <div class='col-sm-12'>
                                                                        <br />
                                                                    </div>
                                                                    
                                                                    <div class='col-sm-12'>
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="TableEditEmergencyContactNew" runat="server"
                                                                                CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid"
                                                                                AutoGenerateColumns="False"
                                                                                Style="width: 100%"
                                                                                ShowHeaderWhenEmpty="true"
                                                                                EmptyDataText="No Record Found"
                                                                                DataKeyNames="Oid"
                                                                                OnRowCommand="TableEditEmergencyContactNew_RowCommand"
                                                                                OnRowDataBound="TableEditEmergencyContactNew_RowDataBound"
                                                                                ClientIDMode="Static">
                                                                                <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                                                <Columns>
                                                                            
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnEditEmergency" runat="server" CommandName="EditRow" CommandArgument='<%# Eval("oid") %>' CssClass="btn btn-sm buttonColorGridview" Text="Edit" />
                                                                                            <asp:LinkButton ID="btnDeleteEmergency" runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("oid") %>' CssClass="btn btn-sm btn-danger" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this row?');" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                            
                                                                                    <asp:BoundField DataField="Oid" HeaderText="Oid" Visible="false" />
                                                                                    <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                                                                                    <asp:BoundField DataField="ContactAddress" HeaderText="Contact Address" />
                                                                                    <asp:BoundField DataField="ContactRelationship" HeaderText="Contact Relationship" />
                                                                                    <asp:BoundField DataField="ContactNumber" HeaderText="Phone" />
                                                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    
                                                                    
                                                                    
                                                                    <%--<div class='col-sm-12'>
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="TableEditEmergencyContact" runat="server"
                                                                                CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid"
                                                                                AutoGenerateColumns="False"
                                                                                Style="width: 100%"
                                                                                ShowHeaderWhenEmpty="true"
                                                                                EmptyDataText="No Record Found"
                                                                                OnRowCommand="TableEditEmergencyContact_RowCommand" ClientIDMode="Static">
                                                                                <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemTemplate>
                                                                                            <a href="javascript:void(0);"
                                                                                                class="btn buttonColorGridview"
                                                                                                onclick="fillEmergencyContactFields(
                                                                                                               '<%# Eval("Oid") %>',
                                                                                                               '<%# Eval("ContactName").ToString().Replace("'", "\\'") %>',
                                                                                                               '<%# Eval("ContactAddress").ToString().Replace("'", "\\'") %>',
                                                                                                               '<%# Eval("ContactRelationship").ToString().Replace("'", "\\'") %>',
                                                                                                               '<%# Eval("ContactNumber").ToString().Replace("'", "\\'") %>'
                                                                                                           )">
                                                                                                <i class="fa fa-check"></i>Select
                                                                                            </a>
                                                                                            &nbsp;
                                                                                                        <a href="javascript:void(0);"
                                                                                                            class="btn btn-danger"
                                                                                                            onclick="if(confirm('Are you sure you want to delete this contact?')) deleteEmergencyContactRow(this);">
                                                                                                            <i class="fa fa-trash"></i>Delete
                                                                                                        </a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Oid" HeaderText="Oid" />
                                                                                    <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                                                                                    <asp:BoundField DataField="ContactAddress" HeaderText="Contact Address" />
                                                                                    <asp:BoundField DataField="ContactRelationship" HeaderText="Contact Relationship" />
                                                                                    <asp:BoundField DataField="ContactNumber" HeaderText="Phone" />
                                                                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                                                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                                                                                    <asp:BoundField DataField="modifiedBy" HeaderText="Modified By" />
                                                                                    <asp:BoundField DataField="modifiedDate" HeaderText="Modified Date" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>--%>




                                                                </div>
                                                            </fieldset>

                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="tabCareerHistory" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">
                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">
                                                            <asp:Label ID="txtEditHeaderCareerHistory" runat="server" Text=""></asp:Label>
                                                        </legend>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="TableEditCareerHistory" runat="server"
                                                                            CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid"
                                                                            AutoGenerateColumns="False" Style="width: 100%"
                                                                            ShowHeaderWhenEmpty="true"
                                                                            EmptyDataText="No Record Found"
                                                                            DataKeyNames="id"
                                                                            ClientIDMode="Static"
                                                                            OnRowDataBound="TableEditCareerHistory_RowDataBound">
                                                                            <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" />
                                                                                <asp:BoundField DataField="nik" HeaderText="Employee Number" ReadOnly="True" />
                                                                                <asp:BoundField DataField="Full Name" HeaderText="Full Name" ReadOnly="True" />
                                                                                <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" />
                                                                                <asp:BoundField DataField="Division" HeaderText="Division" ReadOnly="True" />
                                                                                <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="True" />
                                                                                <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" />
                                                                                <%--                                                    <asp:TemplateField HeaderText="Category">
                                                                                    <ItemTemplate>
                                                                                        <%# Eval("Category") %>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                                <asp:TemplateField HeaderText="Category">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select form-select-sm">
                                                                                            <asp:ListItem Value=""></asp:ListItem>
                                                                                            <asp:ListItem Value="Promotion">Promotion</asp:ListItem>
                                                                                            <asp:ListItem Value="Mutation">Mutation</asp:ListItem>
                                                                                            <asp:ListItem Value="Rotation">Rotation</asp:ListItem>
                                                                                            <asp:ListItem Value="Demotion">Demotion</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="modified_by" HeaderText="Modified by" ReadOnly="True" />
                                                                                <asp:BoundField DataField="modification_date" HeaderText="Modified Date" ReadOnly="True" />

                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <tr>
                                                                                    <td colspan="9" style="text-align: center;">No records found.</td>
                                                                                </tr>
                                                                            </EmptyDataTemplate>
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
                                    <div class="tab-pane fade" id="tabTrainingHistory" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">
                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <%--<legend class="scheduler-border">
                                                            <%--<asp:Label ID="Label1" runat="server" Text=""></asp:Label>--%>
                                                        <%--</legend>--%>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="TableViewUserTraining" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                                                            ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                                                            <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="training_code" HeaderText="Code" />
                                                                                <asp:BoundField DataField="training_title" HeaderText="Training Title" />
                                                                                <asp:BoundField DataField="training_type" HeaderText="Training Type" />
                                                                                <asp:BoundField DataField="trainer_type" HeaderText="Trainer Type" />
                                                                                <asp:BoundField DataField="VendorTrainer" HeaderText="Vendor Trainer" />
                                                                                <asp:BoundField DataField="InternalTrainer" HeaderText="Internal Trainer" />
                                                                                <asp:BoundField DataField="method" HeaderText="Method" />
                                                                                <asp:BoundField DataField="location" HeaderText="Location" />
                                                                                <asp:BoundField DataField="training_start_date" HeaderText="Start Date" />
                                                                                <asp:BoundField DataField="training_end_date" HeaderText="End Date" />
                                                                                <asp:BoundField DataField="training_status" HeaderText="Training Status" />
                                                                                <asp:BoundField DataField="training_result" HeaderText="Training Result" />
                                                                                <asp:BoundField DataField="date_complete" HeaderText="Completion Date" />
                                                                                <asp:BoundField DataField="note" HeaderText="Note" />
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
                                    <div class="tab-pane fade" id="tabeDoc" role="tabpanel">
                                        <div class="p-t-15">
                                            <div class="row">

                                                <div class='col-sm-12'>
                                                    <button type="button" onclick="<%=btnAdd_eDoc.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                        Add e-Docs<span class="btn-icon-right">
                                                            <i class="fa fa-plus"></i></span>
                                                    </button>
                                                    <asp:Button runat="server" Style="display: none;" ID="btnAdd_eDoc" OnClick="btnAdd_eDoc_Click"></asp:Button>
                                                </div>
                                                <div class='col-sm-12'>
                                                    <br />
                                                </div>
                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">
                                                            <asp:Label ID="Label2" runat="server" Text="Surat Peringatan"></asp:Label>
                                                        </legend>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="TableeDocsFiles" runat="server" CssClass="table table-striped row-border order-column table-bordered nowrap zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                                                                            ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" DataKeyNames="Oid">
                                                                            <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Oid" HeaderText="Oid" />
                                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                                                <asp:BoundField DataField="Module" HeaderText="Module" />
                                                                                <asp:BoundField DataField="DocumentType" HeaderText="DocumentType" />
                                                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                                <asp:BoundField DataField="FileName" HeaderText="FileName" />
                                                                                <asp:BoundField DataField="Category" HeaderText="Category" />
                                                                                <asp:BoundField DataField="NIK" HeaderText="NIK" />
                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton runat="server" ID="btnDownload_eDoc" CommandName="Download" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnDownload_eDoc_Click" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
                                                                                        <asp:LinkButton runat="server" ID="btnHapus_eDoc" CommandName="Hapus" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnHapus_eDoc_Click" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>

                                                <div class='col-sm-12'>
                                                    <fieldset class="scheduler-border">
                                                        <legend class="scheduler-border">
                                                            <asp:Label ID="Label3" runat="server" Text="Other"></asp:Label>
                                                        </legend>
                                                        <div class="control-group">
                                                            <div class="row">
                                                                <div class='col-sm-12'>
                                                                    <br />
                                                                </div>
                                                                <div class='col-sm-12'>
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="TableeDocsFiles_Other" runat="server" CssClass="table table-striped row-border order-column table-bordered nowrap zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                                                                            ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" DataKeyNames="Oid">
                                                                            <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Oid" HeaderText="Oid" />
                                                                                <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                                                                                <asp:BoundField DataField="Module" HeaderText="Module" />
                                                                                <asp:BoundField DataField="DocumentType" HeaderText="DocumentType" />
                                                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                                <asp:BoundField DataField="FileName" HeaderText="FileName" />
                                                                                <asp:BoundField DataField="Category" HeaderText="Category" />
                                                                                <asp:BoundField DataField="NIK" HeaderText="NIK" />
                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton runat="server" ID="btnDownload_eDoc_Other" CommandName="Download" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnDownload_eDoc_Other_Click" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
                                                                                        <asp:LinkButton runat="server" ID="btnHapus_eDoc_Other" CommandName="Hapus" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnHapus_eDoc_Other_Click" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
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
                        <div class="row">
                            <div class='col-sm-12'>
                                <div class="form-group d-flex gap-2">
                                    <asp:Button runat="server" ID="btnUpdate" CssClass="btn buttonColor" Text="Save All" OnClick="btnUpdate_Click" OnClientClick="extractDatafromGridView(); return true;"></asp:Button>
                                </div>
                            </div>
                        </div>

                        <div class='col-sm-12'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Reff Records</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <div class='col-sm-3'>
                                            Created by
										    <div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtCreateBy" data-validate-length-range="5,15" type="text" class="form-control input-rounded" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Creation Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtCreateDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Modified by
										    <div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtModifiedBy" data-validate-length-range="5,15" type="text" class="form-control input-rounded" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Modification Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtModifiedDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded" required="required" disabled>
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
        </div>
    </div>

    <!--**********************************
	    Modal Table Candidate Join
    ***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlTableCandidateJoin" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1">List Candidate of Joined</h4>
                    <button type="button" onclick="<%=btnCloseModalcandidatejoin.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalcandidatejoin" OnClick="btnCloseModalcandidatejoin_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
			</button>--%>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="card-box table-responsive">
                                    <asp:GridView ID="TableCandidateJoin" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableCandidateJoin_RowDataBound" OnRowCommand="TableCandidateJoin_RowCommand"
                                        OnSelectedIndexChanged="TableCandidateJoin_SelectedIndexChanged">
                                        <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:CommandField HeaderText="Add" ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/adddoc.png" SelectText="Edit" ControlStyle-ForeColor="Blue" />
                                            <asp:BoundField DataField="time_stamp" HeaderText="Time Stamp" />
                                            <asp:BoundField DataField="status_recruitment" HeaderText="Recruitment Status" />
                                            <asp:BoundField DataField="pic_recruitment" HeaderText="Recruitment PIC" />
                                            <asp:BoundField DataField="candidate_job" HeaderText="Applied Position" />
                                            <asp:BoundField DataField="fullname" HeaderText="Full Name" />
                                            <asp:BoundField DataField="id_ktp" HeaderText="National ID Number" />
                                            <asp:BoundField DataField="email" HeaderText="Email" />
                                            <asp:BoundField DataField="nickname" HeaderText="Nickname" />
                                            <asp:BoundField DataField="sex" HeaderText="Gender" />
                                            <asp:BoundField DataField="home_address" HeaderText="Home Address" />
                                            <asp:BoundField DataField="zip_code" HeaderText="Zip Code" />
                                            <asp:BoundField DataField="phone" HeaderText="Phone" />
                                            <asp:BoundField DataField="nationality" HeaderText="Nationality" />
                                            <asp:BoundField DataField="religion" HeaderText="Religion" />
                                            <asp:BoundField DataField="place_of_birth" HeaderText="Place of Birth" />
                                            <asp:BoundField DataField="date_of_birth" HeaderText="Date of Birth" />
                                            <asp:BoundField DataField="marital_status" HeaderText="Marital Status" />
                                            <asp:BoundField DataField="other_id_card" HeaderText="Other ID Card" />
                                            <asp:BoundField DataField="photo" HeaderText="Photograph" />
                                            <asp:BoundField DataField="company_organization_structure" HeaderText="Company Organization Structure" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
		Modal Change Foto
	***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlUploadFoto" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel112a">Upload Photo</h4>
                    <button type="button" onclick="<%=btnCloseModalImport.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalImport" OnClick="btnCloseModalImport_Click"></asp:Button>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class='col-sm-12'>
                                Choose Photo11
					            <div class="form-group">
                                    <div class='input-group'>
                                        <asp:FileUpload ID="FileUploadFoto" AllowMultiple="false" runat="server" />
                                        <asp:Label ID="label1" runat="server" ForeColor="Green"></asp:Label><br />
                                        <asp:HiddenField ID="UploadFotoOK" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclick="<%=btnUploadFoto.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Upload
			            <span class="btn-icon-right"><i class="fa fa-refresh"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnUploadFoto" OnClick="btnUploadFoto_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
     Modal Upload eDocs
     ***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlUploadeDocs" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <div class='col-sm-12'>
                        <button type="button" style="float: right;" class="close" data-dismiss="modal"><span aria-hidden="true">×</span></button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Add Document</legend>
                                    <div class="control-group">
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="input-group-append">
                                                            <span class="input-group-text width-150"><strong>Document Type</strong></span>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDocType" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                            runat="server" OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="SP" Value="Surat Peringatan"></asp:ListItem>
                                                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="input-group-append">
                                                            <span class="input-group-text width-150"><strong>Description</strong></span>
                                                        </div>
                                                        <textarea class="form-control h-150px" rows="3" id="txtDescription" maxlength="150" runat="server" disabled></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class='col-sm-12'>
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="input-group-append">
                                                            <span class="input-group-text width-150" style="height: 45px"><strong>Browse File</strong></span>
                                                        </div>
                                                        <div class="input-group-append">
                                                            <span class="input-group-text width-383">
                                                                <asp:FileUpload ID="FileUploadEDocs" AllowMultiple="false" runat="server" Enabled="false" />
                                                            </span>
                                                        </div>
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
                    <button type="button" onclick="<%=btnUploadeDoc.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Upload <span class="btn-icon-right">
                            <i class="fa fa-upload"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnUploadeDoc" OnClick="btnUploadeDoc_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>


    <script>
        $(document).ready(function () {
            $(".datepicker1").datepicker({ format: 'yyyy/mm/dd', autoclose: true, todayBtn: 'linked' })
        });
    </script>

    <script type="text/javascript">
        function addSuratPeringatanRow() {
            var oid = document.getElementById('<%= lblSuratPeringatanID.ClientID %>').value;
            var SuratPeringatan = document.getElementById('ddlSuratPeringatan').value;
            var StartDate = document.getElementById('txtStartDateSP').value;
            var EndDate = document.getElementById('txtEndDateSP').value;
            var Remarks = document.getElementById('txtRemarkSP').value;
            var ActiveSP = document.getElementById('ddlActiveSP').value;

            // Validation: stop if any field is empty
            if (!SuratPeringatan || !StartDate || !EndDate || !ActiveSP) {
                swal('Save Failed!', 'All emergency contact fields are required.', 'error');
                return;
            }

            var dt = $('#TableEditSuratPeringatan').DataTable();

            // Remove the empty filler row if present (all fields empty except action)
            dt.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var data = this.data();
                // Check if all fields except action are empty
                if (
                    !data[1]
                    //&& !data[2] && !data[3] && !data[4] && !data[5] &&
                    //!data[6] && !data[7] && !data[8] && !data[9]
                ) {
                    this.remove();
                }
            });

            // If oid is empty, generate a new one
            if (!oid) {
                oid = 'SP-' + Date.now() + '-' + Math.floor(Math.random() * 1000);
            }

            // Build the select button HTML
            var selectBtn = "<a href=\"javascript:void(0);\" class=\"btn buttonColorGridview\" onclick=\"fillEmergencyContactFields('" +
                oid.replace(/'/g, "\\'") + "','" +
                SuratPeringatan.replace(/'/g, "\\'") + "','" +
                StartDate.replace(/'/g, "\\'") + "','" +
                EndDate.replace(/'/g, "\\'") + "','" +
                Remarks.replace(/'/g, "\\'") +
                ActiveSP.replace(/'/g, "\\'") +
                "')\"><i class='fa fa-check'></i> Select</a>";

            // Build the delete button HTML
            var deleteBtn = "<a href=\"javascript:void(0);\" class=\"btn btn-danger ml-1\" onclick=\"deleteEmergencyContactRow(this)\"><i class='fa fa-trash'></i> Delete</a>";

            var actionBtns = selectBtn + " " + deleteBtn;

            // Check if oid already exists in the table
            var found = false;
            dt.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var data = this.data();
                if (data[1] === oid) { // Oid is in the second column
                    // Update the row
                    this.data([
                        actionBtns,
                        oid,
                        SuratPeringatan,
                        StartDate,
                        EndDate,
                        Remarks,
                        ActiveSP,
                        data[6], // Keep existing CreatedBy
                        data[7], // Keep existing CreatedDate
                        '', // ModifiedBy
                        ''  // ModifiedDate
                    ]).draw(false);
                    found = true;
                    return false; // break
                }
            });

            // If not found, add as new row
            if (!found) {
                dt.row.add([
                    actionBtns,
                    oid,
                    SuratPeringatan,
                    StartDate,
                    EndDate,
                    Remarks,
                    ActiveSP,
                    '', // CreatedBy
                    '', // CreatedDate
                    '', // ModifiedBy
                    ''  // ModifiedDate
                ]).draw(false);
            }

            // Optionally, clear the input fields
<%--            document.getElementById('<%= lblEmergencyContactID.ClientID %>').value = "";
            document.getElementById('txtEmergencyContactName').value = "";
            document.getElementById('txtEmergencyContactRelationship').value = "";
            document.getElementById('txtEmergencyContactNumber').value = "";
            document.getElementById('txtEmergencyContactAddress').value = "";--%>

            // Adjust columns to ensure header matches body
            dt.columns.adjust().draw(false);
        }
        // Add this function to handle row deletion
        function deleteTableEditSuratPeringatanRow(btn) {
            var dt = $('#TableEditSuratPeringatan').DataTable();
            var row = $(btn).closest('tr');
            var rowData = dt.row(row).data();
            if (!rowData) return;

            var oid = rowData[1]; // Oid is in the second column

            // Stop if oid is null, undefined, or empty string
            if (!oid) {
                return;
            }

            if (oid.startsWith("SP")) {
                // Remove the row if oid starts with "EC"
                dt.row(row).remove().draw(false);
            } else {
                // Prepend "-" if not already present
                if (!oid.startsWith("-")) {
                    oid = "-" + oid;
                    rowData[1] = oid;
                    // Update the action buttons to use the new oid
                    var SuratPeringatan = rowData[2];
                    var StartDate = rowData[3];
                    var EndDate = rowData[4];
                    var Remarks = rowData[5];
                    var ActiveSP = rowData[6];
                    var selectBtn = "<a href=\"javascript:void(0);\" class=\"btn buttonColorGridview\" onclick=\"fillSuratPeringatanFields('" +
                        oid.replace(/'/g, "\\'") + "','" +
                        SuratPeringatan.replace(/'/g, "\\'") + "','" +
                        StartDate.replace(/'/g, "\\'") + "','" +
                        EndDate.replace(/'/g, "\\'") + "','" +
                        Remarks.replace(/'/g, "\\'") +
                        ActiveSP.replace(/'/g, "\\'") +
                        "')\"><i class='fa fa-check'></i> Select</a>";
                    var deleteBtn = "<a href=\"javascript:void(0);\" class=\"btn btn-danger ml-1\" onclick=\"deleteSuratPeringatanRow(this)\"><i class='fa fa-trash'></i> Delete</a>";
                    rowData[0] = selectBtn + " " + deleteBtn;
                    dt.row(row).data(rowData).draw(false);

                    // Add color to the row (e.g., yellow background)
                    setTimeout(function () {
                        $(dt.row(row).node()).css('background-color', '#fff3cd');
                    }, 0);
                }
            }
        }
        function extractDatafromGridViewSP() {
            debugger
            var dt = $('#TableEditSuratPeringatan').DataTable();
            var data = [];
            dt.rows().every(function () {
                var row = this.data();
                data.push({
                    Oid: row[1],
                    SuratPeringatan: row[2],
                    StartDate: row[3],
                    EndDate: row[4],
                    Remarks: row[5],
                    ActiveSP: row[5],
                    CreatedBy: row[7],
                    CreatedDate: row[8],
                    ModifiedBy: row[9],
                    ModifiedDate: row[10]
                });
            });
            document.getElementById('<%= hfSuratPeringatanData.ClientID %>').value = JSON.stringify(data);
        }
    </script>


<%--    <script type="text/javascript">
        function addEmergencyContactRow() {
            var oid = document.getElementById('<%= lblEmergencyContactID.ClientID %>').value;
            var name = document.getElementById('txtEmergencyContactName').value;
            var relationship = document.getElementById('txtEmergencyContactRelationship').value;
            var number = document.getElementById('txtEmergencyContactNumber').value;
            var address = document.getElementById('txtEmergencyContactAddress').value;

            // Validation: stop if any field is empty
            if (!name || !relationship || !number || !address) {
                swal('Save Failed!', 'All emergency contact fields are required.', 'error');
                return;
            }

            var dt = $('#TableEditEmergencyContact').DataTable();

            // Remove the empty filler row if present (all fields empty except action)
            dt.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var data = this.data();
                // Check if all fields except action are empty
                if (
                    !data[1]
                    //&& !data[2] && !data[3] && !data[4] && !data[5] &&
                    //!data[6] && !data[7] && !data[8] && !data[9]
                ) {
                    this.remove();
                }
            });

            // If oid is empty, generate a new one
            if (!oid) {
                oid = 'EC-' + Date.now() + '-' + Math.floor(Math.random() * 1000);
            }

            // Build the select button HTML
            var selectBtn = "<a href=\"javascript:void(0);\" class=\"btn buttonColorGridview\" onclick=\"fillEmergencyContactFields('" +
                oid.replace(/'/g, "\\'") + "','" +
                name.replace(/'/g, "\\'") + "','" +
                address.replace(/'/g, "\\'") + "','" +
                relationship.replace(/'/g, "\\'") + "','" +
                number.replace(/'/g, "\\'") +
                "')\"><i class='fa fa-check'></i> Select</a>";

            // Build the delete button HTML
            var deleteBtn = "<a href=\"javascript:void(0);\" class=\"btn btn-danger ml-1\" onclick=\"deleteEmergencyContactRow(this)\"><i class='fa fa-trash'></i> Delete</a>";

            var actionBtns = selectBtn + " " + deleteBtn;

            // Check if oid already exists in the table
            var found = false;
            dt.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var data = this.data();
                if (data[1] === oid) { // Oid is in the second column
                    // Update the row
                    this.data([
                        actionBtns,
                        oid,
                        name,
                        address,
                        relationship,
                        number,
                        data[6], // Keep existing CreatedBy
                        data[7], // Keep existing CreatedDate
                        '', // ModifiedBy
                        ''  // ModifiedDate
                    ]).draw(false);
                    found = true;
                    return false; // break
                }
            });

            // If not found, add as new row
            if (!found) {
                dt.row.add([
                    actionBtns,
                    oid,
                    name,
                    address,
                    relationship,
                    number,
                    '', // CreatedBy
                    '', // CreatedDate
                    '', // ModifiedBy
                    ''  // ModifiedDate
                ]).draw(false);
            }

            // Optionally, clear the input fields
            document.getElementById('<%= lblEmergencyContactID.ClientID %>').value = "";
            document.getElementById('txtEmergencyContactName').value = "";
            document.getElementById('txtEmergencyContactRelationship').value = "";
            document.getElementById('txtEmergencyContactNumber').value = "";
            document.getElementById('txtEmergencyContactAddress').value = "";

            // Adjust columns to ensure header matches body
            dt.columns.adjust().draw(false);
        }
        // Add this function to handle row deletion
        function deleteEmergencyContactRow(btn) {
            var dt = $('#TableEditEmergencyContact').DataTable();
            var row = $(btn).closest('tr');
            var rowData = dt.row(row).data();
            if (!rowData) return;

            var oid = rowData[1]; // Oid is in the second column

            // Stop if oid is null, undefined, or empty string
            if (!oid) {
                return;
            }

            if (oid.startsWith("EC")) {
                // Remove the row if oid starts with "EC"
                dt.row(row).remove().draw(false);
            } else {
                // Prepend "-" if not already present
                if (!oid.startsWith("-")) {
                    oid = "-" + oid;
                    rowData[1] = oid;
                    // Update the action buttons to use the new oid
                    var name = rowData[2];
                    var address = rowData[3];
                    var relationship = rowData[4];
                    var number = rowData[5];
                    var selectBtn = "<a href=\"javascript:void(0);\" class=\"btn buttonColorGridview\" onclick=\"fillEmergencyContactFields('" +
                        oid.replace(/'/g, "\\'") + "','" +
                        name.replace(/'/g, "\\'") + "','" +
                        address.replace(/'/g, "\\'") + "','" +
                        relationship.replace(/'/g, "\\'") + "','" +
                        number.replace(/'/g, "\\'") +
                        "')\"><i class='fa fa-check'></i> Select</a>";
                    var deleteBtn = "<a href=\"javascript:void(0);\" class=\"btn btn-danger ml-1\" onclick=\"deleteEmergencyContactRow(this)\"><i class='fa fa-trash'></i> Delete</a>";
                    rowData[0] = selectBtn + " " + deleteBtn;
                    dt.row(row).data(rowData).draw(false);

                    // Add color to the row (e.g., yellow background)
                    setTimeout(function () {
                        $(dt.row(row).node()).css('background-color', '#fff3cd');
                    }, 0);
                }
            }
        }
        function extractDatafromGridView() {
            debugger
            var dt = $('#TableEditEmergencyContact').DataTable();
            var data = [];
            dt.rows().every(function () {
                var row = this.data();
                data.push({
                    Oid: row[1],
                    ContactName: row[2],
                    ContactAddress: row[3],
                    ContactRelationship: row[4],
                    ContactNumber: row[5],
                    CreatedBy: row[6],
                    CreatedDate: row[7],
                    ModifiedBy: row[8],
                    ModifiedDate: row[9]
                });
            });
            document.getElementById('<%= hfEmergencyContactData.ClientID %>').value = JSON.stringify(data);
        }
    </script>--%>


    <script type="text/javascript">
        function initSuratPeringatanDataTable() {
            var $table = $('#TableEditSuratPeringatan');
            if ($.fn.DataTable.isDataTable($table)) {

                $table.DataTable().destroy();
            }
            $table.DataTable({
                scrollY: "500px",
                scrollX: true,
                scrollCollapse: true,
                responsive: true,
                paging: true,
                columnDefs: [
                    { targets: [1], visible: false } // Hide OID column
                ]
            });
        }
        // When tabWarningLetters is shown, trigger sorting on TableEditSuratPeringatan
        $('#myTabEmployeeDetail a[href="#tabWarningLetters"]').on('shown.bs.tab', function (e) {
            var tableId = '<%= TableEditSuratPeringatan.ClientID %>';
            var $table = $('#' + tableId);
            if ($.fn.DataTable.isDataTable($table)) {
                var dt = $table.DataTable();
                setTimeout(function () { dt.columns.adjust().draw(); }, 10);
            }
        });

<%--        function clearSuratPeringatanFields() {
            document.getElementById('<%= lblSuratPeringatanID.ClientID %>').value = '';
            document.getElementById('<%= ddlSuratPeringatan.ClientID %>').value = '';
            document.getElementById('<%= txtStartDateSP.ClientID %>').value = '';
            document.getElementById('<%= txtEndDateSP.ClientID %>').value = '';
            document.getElementById('<%= txtRemarkSP.ClientID %>').value = '';
            document.getElementById('<%= ddlActiveSP.ClientID %>').value = '';
            document.getElementById("<%= txtEditHeaderWarningLetters.ClientID %>").innerText = "Add New Surat Peringatan";
        }--%>

        $('#myTabEmployeeDetail a[href="#tabWarningLetters"]').on('shown.bs.tab', function (e) {
            var tableId = '<%= TableEditSuratPeringatan.ClientID %>';
            var $table = $('#' + tableId);
            if ($.fn.DataTable.isDataTable($table)) {
                var dt = $table.DataTable();
                setTimeout(function () { dt.columns.adjust().draw(); }, 10);

                // Add filler row if empty
                if (dt.rows().count() === 0) {
                    dt.row.add(['', '', '', '', '', '', '', '', '', '', '']).draw(false);
                }
            }
            clearSuratPeringatanFields();
        });
    </script>

<%--    <script type="text/javascript">
        function initEmergencyContactDataTable() {
            var $table = $('#TableEditEmergencyContact');
            if ($.fn.DataTable.isDataTable($table)) {
                $table.DataTable().destroy();
            }
            $table.DataTable({
                scrollY: "500px",
                scrollX: true,
                scrollCollapse: true,
                responsive: true,
                paging: true,
                columnDefs: [
                    { targets: [1], visible: false } // Hide OID column
                ]
            });
        }
        // When tabDetailEmergencyContact is shown, trigger sorting on TableEmergencyContact
        $('#myTabEmployeeDetail a[href="#tabEmergencyContact"]').on('shown.bs.tab', function (e) {
            var tableId = '<%= TableEditEmergencyContact.ClientID %>';
            var $table = $('#' + tableId);
            if ($.fn.DataTable.isDataTable($table)) {
                var dt = $table.DataTable();
                setTimeout(function () { dt.columns.adjust().draw(); }, 10);
            }
        });


        $('#myTabEmployeeDetail a[href="#tabEmergencyContact"]').on('shown.bs.tab', function (e) {
            var dt = $('#TableEditEmergencyContact').DataTable();
            // Check if table is empty, then add a filler row
            if (dt.rows().count() === 0) {
                dt.row.add([
                    '', // Action buttons
                    '', // Oid
                    '', // ContactName
                    '', // ContactAddress
                    '', // ContactRelationship
                    '', // ContactNumber
                    '', // CreatedBy
                    '', // CreatedDate
                    '', // ModifiedBy
                    ''  // ModifiedDate
                ]).draw(false);
            }
             clearEmergencyContactFields();--%>

<%--    function clearEmergencyContactFields() {
                document.getElementById('<%= lblEmergencyContactID.ClientID %>').value = '';
                document.getElementById('<%= txtEmergencyContactName.ClientID %>').value = '';
                document.getElementById('<%= txtEmergencyContactAddress.ClientID %>').value = '';
                document.getElementById('<%= txtEmergencyContactRelationship.ClientID %>').value = '';
                document.getElementById('<%= txtEmergencyContactNumber.ClientID %>').value = '';

                document.getElementById("<%= txtEditHeaderEmergencyContact.ClientID %>").innerText = "Add New Emergency Contact List ";
                // Show the modal if needed
                //$('#mdlAddNewEmployee').modal();
            }
    </script>--%>


    <script type="text/javascript">
            function FuncSave() {
                // Split the inner text into an array of items based on a newline character
                var itemsArray = '<%= lbErrorUploadNotif.InnerText.Replace(Environment.NewLine, "|") %>'.split('|');

                var listItems = ""; // Initialize an empty string for list items

                // Build the list items dynamically
                for (var i = 0; i < itemsArray.length; i++) {
                    listItems += "<li>" + itemsArray[i] + "</li>";
                }

                // Construct the HTML content with the list
                var contentHTML = '<span style="color: green; display: none;"><label id="lbErrorUploadNotif" runat="server"></label></span>' +
                    '<ul>' + listItems + '</ul>';

                swal({
                    title: 'Submit Success',
                    text: contentHTML,
                    timer: '2000',
                    type: 'success',
                    showConfirmButton: false,
                    html: true
                },
                    //function redirect() {
                    /* window.location.href = 'ffis_aff_import_operation.aspx';*/

                    //window.location.href = 'employees.aspx';
                    //}
                );
            }
    </script>

</asp:Content>
