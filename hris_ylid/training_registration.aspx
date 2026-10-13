<%@ Page Title="Training Registration" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="training_registration.aspx.cs" Inherits="hris_ylid.training_registration" %>

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
    <script type="text/javascript">
        function FuncUpdate() {
            swal({
                title: 'Update Success',
                text: 'Data Training Successfully Updated',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'training_registration.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncSave() {
            swal({
                title: 'Save Success',
                text: 'Data Training Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'training_registration.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function DuplicateDataTrainingTitle() {
            swal({
                type: 'error',
                title: 'Save Failed',
                text: 'Training title already exist!',
                showConfirmButton: true
            },
                function redirect() {
                    window.location.href = 'training_registration.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function SelectTrainerOrganizer() {
            swal('Save Failed!', 'Please select trainer or organizer first', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectMethod() {
            swal('Save Failed!', 'Please select method first', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectCertificate() {
            swal('Save Failed!', 'Please select certificate Yes or No', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectType() {
            swal('Save Failed!', 'Please select type first', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldTainingTitle() {
            swal('Save Failed!', 'Please field training title', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldStartDate() {
            swal('Save Failed!', 'Please field training start date', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldEndDate() {
            swal('Save Failed!', 'Please field training end date', 'error');
        }
    </script>
    <script type="text/javascript">
        function Fieldlocation() {
            swal('Save Failed!', 'Please field location', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldCost() {
            swal('Save Failed!', 'Please field cost', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedUpload() {
            swal('Save Failed!', 'Please select the file for upload, please try again', 'error');
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
            <h1 class="page-header text-overflow" style="color: white;">Training Registration</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Master Data</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;HR Training</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Training Registration</li>
        </ol>
    </div>

    <asp:HiddenField ID="txtCodeTrainingEdit" runat="server" />
    <asp:HiddenField ID="txtUploadCode" runat="server" />
    <asp:HiddenField ID="txtUploadCodeEdit" runat="server" />
    <asp:HiddenField ID="txtTrainingTitleEdit" runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Trainings</legend>
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
                                            <%--<div class='col-sm-3'>
											<asp:LinkButton runat="server" ID="btnGenExcel" CssClass="btn mb-1 buttonColor" OnClick="btnGenExcel_Click"><i class="fa fa-file-excel-o"></i> Generate .xlsx</asp:LinkButton>
										</div>--%>

                                            <div class='col-sm-12'>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="TableTraining" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableTraining_RowDataBound" OnRowCommand="TableTraining_RowCommand" OnSelectedIndexChanged="TableTraining_SelectedIndexChanged">
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
                                                            <asp:BoundField DataField="training_code" HeaderText="Code" />
                                                            <asp:BoundField DataField="training_title" HeaderText="Title" />
                                                            <asp:BoundField DataField="training_start_date" HeaderText="Start Date" />
                                                            <asp:BoundField DataField="training_end_date" HeaderText="End Date" />
                                                            <asp:BoundField DataField="trainer_type" HeaderText="Trainer" />
                                                            <asp:BoundField DataField="Vendor Trainer" HeaderText="Vendor" />
                                                            <asp:BoundField DataField="Internal Trainer" HeaderText="Internal Trainer" />
                                                            <asp:BoundField DataField="location" HeaderText="Location" />
                                                            <asp:BoundField DataField="method" HeaderText="Method" />
                                                            <asp:BoundField DataField="certificate" HeaderText="Certificate" />
                                                            <asp:BoundField DataField="training_type" HeaderText="Training Type" />
                                                            <asp:BoundField DataField="regulation" HeaderText="Regulation" />
                                                            <asp:BoundField DataField="government_institution" HeaderText="Institution" />
                                                            <asp:BoundField DataField="validity_period" HeaderText="Validity Period" />
                                                            <asp:BoundField DataField="cost" HeaderText="Cost (IDR)" />
                                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                                            <asp:BoundField DataField="createby" HeaderText="Created by" />
                                                            <asp:BoundField DataField="create_date" HeaderText="Creation Date" />
                                                            <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                                                            <asp:BoundField DataField="modified_date" HeaderText="Modification Date" />
                                                            <asp:BoundField DataField="id_trainer_vendor" HeaderText="id_trainer_vendor" />
                                                            <asp:BoundField DataField="id_trainer_internal" HeaderText="id_trainer_internal" />
                                                            <asp:BoundField DataField="upload_code" HeaderText="upload_code" />

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
    <div class="modal fade bs-example-modal-lg" id="mdlAddNewTraining" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1q">Add New Training</h4>
                    <button type="button" onclick="<%=btnCloseModalNew.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalNew" OnClick="btnCloseModalNew_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
					</button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-12'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Training Detail</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <asp:HiddenField ID="hlbID" runat="server" />
                                        <div class='col-sm-6'>
                                            Training Title
											<div class="form-group">
                                                <div class='input-group'>
                                                    <%--<input runat="server" id="" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="" required="required" >--%>
                                                    <input runat="server" id="txtTrainingTitle" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Training Title">
                                                    <%--<div class="input-group-append">
													<button type="button" class="btn buttonColor" onclick="<%=btnGetCode.ClientID %>.click()">
													<i class="fa fa-check-circle-o"></i>  Get Code</button>
													<asp:Button runat="server" Style="display: none;" ID="btnGetCode" OnClick="btnGetCode_Click"></asp:Button>
													</div>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'></div>
                                        <div class='col-sm-3' hidden>
                                            Training Code
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtCodeTraining" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Auto Generate" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'></div>
                                        <div class='col-sm-3'>
                                            Training Start Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <%--<input type="text" id="txtTrainingStartDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Start Date" >--%>
                                                    <input type="text" class="form-control datepicker1" id="txtTrainingStartDate" runat="server" placeholder="mm/dd/yyyy">
                                                    <span class="input-group-append"><span class="input-group-text"><i class="mdi mdi-calendar-check"></i></span></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Training End Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <%--<input type="text" id="txtTrainingEndDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="End Date" >--%>
                                                    <input type="text" class="form-control datepicker1" id="txtTrainingEndDate" runat="server" placeholder="mm/dd/yyyy">
                                                    <span class="input-group-append"><span class="input-group-text"><i class="mdi mdi-calendar-check"></i></span></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'></div>
                                        <div class='col-sm-3'>
                                            Trainer/Organizer
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlTrainerOrganizer" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlTrainerOrganizer_SelectedIndexChanged" AutoPostBack="True">
                                                        <asp:ListItem Enabled="true" Text="<Select Trainer>" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="INTERNAL" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="VENDOR" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="REGIONAL" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="GLOBAL" Value="4"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3' id="divInternal" runat="server">
                                            Internal Trainer
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlInternal" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlInternal_SelectedIndexChanged">
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3' id="divVendor" runat="server">
                                            Vendor Name
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlVendor" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class='col-sm-3'>
                                            Location
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtLocation" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Training Location">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'></div>
                                        <div class='col-sm-3'>
                                            Method
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlMethod" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlMethod_SelectedIndexChanged" AutoPostBack="false">
                                                        <asp:ListItem Enabled="true" Text="<Select Method>" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="ONLINE" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="OFFLINE" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="HYBRID" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Certificate
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlCertificate" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlCertificate_SelectedIndexChanged" AutoPostBack="false">
                                                        <asp:ListItem Enabled="true" Text="<Select>" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class='col-sm-3'>
                                            Type
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="dllType" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="dllType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Enabled="true" Text="<Select Type>" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Compliance" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Development" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3' id="divRegulation" runat="server">
                                            Regulations
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtRegulation" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Training Regulation">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3' id="divGovInstitut" runat="server">
                                            Institution
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtGovernmentInstitution" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Government Institutions">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3' id="divValidityPeriod" runat="server">
                                            Validity Period
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" class="form-control datepicker1" id="txtValidityPeriod" runat="server" placeholder="mm/dd/yyyy">
                                                    <span class="input-group-append"><span class="input-group-text"><i class="mdi mdi-calendar-check"></i></span></span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class='col-sm-3'>
                                            Cost (IDR)
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtCost" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Training Cost">
                                                </div>
                                            </div>
                                        </div>

                                        <div class='col-sm-3' id="divStatus" runat="server" visible="false">
                                            Status
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlStatus" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                        <asp:ListItem Enabled="true" Text="<Select Status>" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="ACTIVE" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="NON ACTIVE" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-9'></div>
                                        <div class='col-sm-6'>
                                            <fieldset class="scheduler-border">
                                                <legend class="scheduler-border">Reff Records</legend>
                                                <div class="control-group">
                                                    <div class="row">
                                                        <div class='col-sm-6'>
                                                            Created by
															<div class="form-group">
                                                                <div class='input-group'>
                                                                    <input runat="server" id="txtCreateBy" data-validate-length-range="5,15" type="text" class="form-control input-rounded" required="required" disabled>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class='col-sm-6'>
                                                            Creation Date
															<div class="form-group">
                                                                <div class='input-group'>
                                                                    <input type="text" id="txtCreateDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded" required="required" disabled>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class='col-sm-6'>
                                                            Modified by
															<div class="form-group">
                                                                <div class='input-group'>
                                                                    <input runat="server" id="txtModifiedBy" data-validate-length-range="5,15" type="text" class="form-control input-rounded" required="required" disabled>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class='col-sm-6'>
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


                                        <div class='col-sm-6'>
                                            <fieldset class="scheduler-border">
                                                <legend class="scheduler-border">Upload Documents</legend>
                                                <div class="control-group">
                                                    <div class="row">
                                                        <div class='col-sm-12' id="divAddInfo" runat="server">
                                                            Additional Information(Compliance Document)
															<div class="form-group">
                                                                <div class='input-group'>
                                                                    <asp:FileUpload ID="FileUploadAddInfo" AllowMultiple="true" runat="server" />
                                                                    <asp:Label ID="label1" runat="server" ForeColor="Green"></asp:Label>
                                                                    <asp:HiddenField ID="hlbComplianceOK" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class='col-sm-12'>
                                                            Supporting Document
															<div class="form-group">
                                                                <div class='input-group'>
                                                                    <asp:FileUpload ID="FileUploadSupportingDoc" AllowMultiple="true" runat="server" />
                                                                    <asp:HiddenField ID="hlbSupportingOK" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <div class='col-sm-12'>

                                                <asp:Button runat="server" Visible="false" ID="btnSubmit" CssClass="btn buttonColor" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
                                                <asp:Button runat="server" Visible="false" ID="btnUpdate" CssClass="btn buttonColor" Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                                                <%--<asp:Button runat="server" visible="false" ID="btnRefresh" CssClass="btn buttonColor" Text="Refresh" OnClick="btnRefresh_Click"></asp:Button>--%>
                                                <%--<asp:LinkButton runat="server" visible="false" ID="btnSubmit" CssClass="btn btn-success" OnClick="btnSubmit_Click"><i class="fa fa-save"></i> Submit</asp:LinkButton>
												<asp:LinkButton runat="server" visible="false" ID="btnUpdate" CssClass="btn btn-secondary" OnClick="btnUpdate_Click"><i class="fa fa-refresh"></i> Update</asp:LinkButton>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
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
			Modal Select Document Download
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlDocumentDownload" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        >
	<div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title" id="myModalLabel1">Select Document Download</h4>

            </div>
            <div class="modal-body">
                <div class="x_content">
                    <div class="row">
                        <div class='col-sm-12'>
                            Please select document download
					<div class="form-group">
                        <div class='input-group'>
                            <asp:DropDownList ID="ddlDocDownload" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                runat="server" OnSelectedIndexChanged="ddlDocDownload_SelectedIndexChanged" AutoPostBack="false">
                                <asp:ListItem Enabled="true" Text="<Select Document>" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Compliance" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Support_Document" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                        </div>
                        <div class='col-sm-12'>
                            <button type="button" class="btn buttonColor" onclick="<%=btDownload.ClientID %>.click()">
                                <i class="fa fa-download"></i>Download</button>
                            <asp:Button runat="server" Style="display: none;" ID="btDownload" OnClick="btDownload_Click"></asp:Button>
                        </div>

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" onclick="<%=btnCloseModalDownload.ClientID %>.click()" class="btn mb-1 buttonColor">
                    Close <span class="btn-icon-right">
                        <i class="fa fa-close"></i></span>
                </button>
                <asp:Button runat="server" Style="display: none;" ID="btnCloseModalDownload" OnClick="btnCloseModalDownload_Click"></asp:Button>
                <asp:HiddenField ID="hlbCodeTraining" runat="server" />
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
                    paging: true,
                    columnDefs: [
                        {
                            targets: '_all',
                            render: dateFormatRender
                        }
                    ],
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
