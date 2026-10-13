<%@ Page Title="Individual Training" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="individual_training.aspx.cs" Inherits="hris_ylid.individual_training" %>

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
                text: 'Participant Status Successfully Updated',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            });
        }
    </script>
    <script type="text/javascript">
        function FuncSave() {
            swal({
                title: 'Save Success',
                text: 'Participant Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            });
        }
    </script>
    <script type="text/javascript">
        function UploadSuccess() {
            swal({
                title: 'Save Success',
                text: 'File Successfully Uploaded',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            });
        }
    </script>
    <script type="text/javascript">
        function SelectResult() {
            swal('Save Failed!', 'Please select result first', 'error');
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
        function FailedCloseBatch() {
            swal('Save Failed!', 'Please complete all participants before close batch', 'error');
        }
    </script>
    <script type="text/javascript">
        function FailedAddParticipant() {
            swal('Save Failed!', 'Please select participants', 'error');
        }
    </script>
    <script type="text/javascript">
        function DuplicateParticipant() {
            swal('Save Failed!', 'The participant is already in the training list, please select another participant', 'error');
        }
    </script>
    <script type="text/javascript">  

        function DeleteConfirm() {
            var Ans = confirm("Do you want to Remove Selected Participant Record?");
            if (Ans) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">  

        function CloseConfirm() {
            var Ans = confirm("Do you want to close the training batch?");
            if (Ans) {
                return true;
            }
            else {
                return false;
            }
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
            <h1 class="page-header text-overflow" style="color: white;">Individual Training</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;HR Training</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Individual Training</li>
        </ol>
    </div>

    <div class="container-fluid">
        <asp:HiddenField ID="hlbID" runat="server" />
        <asp:HiddenField ID="hlbNIK" runat="server" />
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Ongoing Training</legend>
                                    <div class="row">
                                        <div class='col-sm-6'>
                                            Training Title
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlTrainingTitle" class="form-control selectpicker" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlTrainingTitle_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Training Code
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtCodeTraining" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Training Code" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'></div>
                                        <div class='col-sm-3'>
                                            Training Start Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtTrainingStartDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Start Date">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Training End Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtTrainingEndDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="End Date">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'></div>
                                        <div class='col-sm-3'>
                                            Trainer
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtTrainerType" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Trainer Type" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>

                                        <div class='col-sm-3'>
                                            Internal Trainer
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtInternalTraining" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Internal Trainer Organizer" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            External Trainer
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtVendorTraining" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="External Trainer Organizer" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Location
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtLocation" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Location" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class='col-sm-3'>
											Regional Trainer
											<div class="form-group">
												<div class='input-group'>
													<input runat="server" id="txtRegional" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Regional Training" required="required" disabled>
												</div>
											</div>
										</div>
										<div class='col-sm-3'>
											Global Trainer
											<div class="form-group">
												<div class='input-group'>
													<input runat="server" id="txtGlobal" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Global Training" required="required" disabled>
												</div>
											</div>
										</div>--%>
                                        <div class='col-sm-3'>
                                            Method
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtMethod" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Training Method" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Training Type
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtTrainingType" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Training Type" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Certificate
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtCertificateAvailable" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Certificate Available" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Cost
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtCost" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Cost" required="required" disabled>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class='col-sm-12' id="divParticipantList" runat="server" visible="false">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Participant List</legend>
                                    <div class="row">
                                        <div class='col-sm-12'>
                                            <button type="button" onclick="<%=btnAdd.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                Add
											<span class="btn-icon-right"><i class="fa fa-plus-circle"></i></span>
                                            </button>
                                            <asp:Button runat="server" Style="display: none;" ID="btnAdd" OnClick="btnAdd_Click"></asp:Button>

                                            <button type="button" onclick="<%=btnRemove.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                Remove
											<span class="btn-icon-right"><i class="fa fa-remove"></i></span>
                                            </button>
                                            <asp:Button runat="server" Style="display: none;" ID="btnRemove" OnClick="btnRemove_Click"></asp:Button>

                                            <button type="button" onclick="<%=btnUpdateBatch.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                Close Batch
											<span class="btn-icon-right"><i class="fa fa-users"></i></span>
                                            </button>
                                            <asp:Button runat="server" Style="display: none;" ID="btnUpdateBatch" OnClick="btnUpdateBatch_Click"></asp:Button>

                                            <button type="button" onclick="<%=btnImportFile.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                Import Excel
											<span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span>
                                            </button>
                                            <asp:Button runat="server" Style="display: none;" ID="btnImportFile" OnClick="btnImportFile_Click"></asp:Button>

                                        </div>
                                        <div class='col-sm-12'>
                                            <div class="table-responsive">
                                                <asp:GridView ID="TableParticipant" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableParticipant_RowDataBound" OnRowCommand="TableParticipant_RowCommand" OnSelectedIndexChanged="TableParticipant_SelectedIndexChanged">
                                                    <HeaderStyle BackColor="#06183d" ForeColor="White" />

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                                        <asp:BoundField DataField="id_training_title" HeaderText="id_training_title" />
                                                        <asp:BoundField DataField="nik_participant" HeaderText="Employee ID Number" />
                                                        <asp:BoundField DataField="Participant" HeaderText="Full Name" />
                                                        <asp:BoundField DataField="training_status" HeaderText="Status" />
                                                        <asp:BoundField DataField="training_result" HeaderText="Result" />
                                                        <asp:BoundField DataField="date_complete" HeaderText="Completion Date" />

                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnEditStatus" CommandName="Ubah" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnEditStatus_Click" ToolTip="Edit"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                            </div>
                            <%--<div class='col-sm-12'>
								<fieldset class="scheduler-border">
									<legend class="scheduler-border">Training Record</legend>
										<div class="row">
											<div class="table-responsive">
											<asp:GridView ID="TableTrainingRecord" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" style="width:100%"
										ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableTrainingRecord_RowDataBound" OnRowCommand="TableTrainingRecord_RowCommand" OnSelectedIndexChanged="TableTrainingRecord_SelectedIndexChanged">
											<HeaderStyle BackColor="#06183d" ForeColor="White"  />
											
											<Columns>
												
												<asp:BoundField DataField="id" HeaderText="ID" />
												<asp:BoundField DataField="training_code" HeaderText="Code" />
												<asp:BoundField DataField="training_title" HeaderText="Training Title" />
												<asp:BoundField DataField="training_type" HeaderText="Training Type" />
												<asp:BoundField DataField="trainer_type" HeaderText="Trainer Type" />
												<asp:BoundField DataField="VendorTrainer" HeaderText="Vendor Trainer" />
												<asp:BoundField DataField="InternalTrainer" HeaderText="Internal Trainer" />
												<asp:BoundField DataField="method" HeaderText="Method" />
												<asp:BoundField DataField="location" HeaderText="Location" />
												<asp:BoundField DataField="Participant" HeaderText="Participant" />
												<asp:BoundField DataField="Position" HeaderText="Position" />
												<asp:BoundField DataField="Section" HeaderText="Section" />
												<asp:BoundField DataField="training_status" HeaderText="Training Status" />
												<asp:BoundField DataField="training_result" HeaderText="Training Result" />
												<asp:BoundField DataField="location" HeaderText="Location" />
												<asp:BoundField DataField="date_complete" HeaderText="Completion Date" />
												<asp:BoundField DataField="note" HeaderText="Note" />
												<asp:BoundField DataField="createby" HeaderText="createby" />
												<asp:BoundField DataField="create_date" HeaderText="create_date" />
												<asp:BoundField DataField="modifiedby" HeaderText="modifiedby" />
												<asp:BoundField DataField="modified_date" HeaderText="modified_date" />
											</Columns>
										</asp:GridView>
										</div>
										</div>
								</fieldset>
							</div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal Table Employees
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlTableEmployees" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1">List of Employees</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class='col-sm-12'>
                                Employees
					<div class="form-group">
                        <div class='input-group'>
                            <asp:DropDownList ID="ddlEmployees" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                runat="server" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclick="<%=btnSubmit.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Submit
			<span class="btn-icon-right"><i class="fa fa-save"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnSubmit" OnClick="btnSubmit_Click"></asp:Button>
                    <button type="button" class="btn buttonColor" data-dismiss="modal">
                        Close
			<span class="btn-icon-right"><i class="fa fa-remove"></i></span>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal Update Status Participant
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlStatusParticipant" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel12">Update Status Participant</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class='col-sm-12'>
                                Training Result
					<div class="form-group">
                        <div class='input-group'>
                            <asp:DropDownList ID="ddlTrainingResult" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                runat="server" OnSelectedIndexChanged="ddlTrainingResult_SelectedIndexChanged">
                                <asp:ListItem Enabled="true" Text="<Select Result>" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                            </div>
                            <div class='col-sm-12'>
                                Completion Date
					<div class="form-group">
                        <div class='input-group'>
                            <input type="text" id="txtCompletionDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Completion Date">
                        </div>
                    </div>
                            </div>
                            <div class='col-sm-12'>
                                Certificate
					<div class="form-group">
                        <div class='input-group'>
                            <asp:FileUpload ID="FileUploadCertificate" AllowMultiple="true" runat="server" />
                            <asp:HiddenField ID="hlbCertificateOK" runat="server" />
                        </div>
                    </div>
                            </div>
                            <div class='col-sm-12'>
                                Note
					<div class="form-group">
                        <div class='input-group'>
                            <textarea class="form-control h-150px" rows="2" id="txtNote" runat="server"></textarea>
                        </div>
                    </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclick="<%=btnUpdateStatus.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Update
			<span class="btn-icon-right"><i class="fa fa-refresh"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnUpdateStatus" OnClick="btnUpdateStatus_Click"></asp:Button>
                    <button type="button" class="btn buttonColor" data-dismiss="modal">
                        Close
			<span class="btn-icon-right"><i class="fa fa-remove"></i></span>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal Update Batch
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlUpdateBatch" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel112">Close Training Batch</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class='col-sm-12'>
                                Participant Attendance
					<div class="form-group">
                        <div class='input-group'>
                            <asp:FileUpload ID="FileUploadAttendance" AllowMultiple="true" runat="server" />
                            <asp:HiddenField ID="AttendanceOK" runat="server" />
                        </div>
                    </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclick="<%=btnUpdateStatusBatch.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Close Batch
			<span class="btn-icon-right"><i class="fa fa-refresh"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnUpdateStatusBatch" OnClick="btnUpdateStatusBatch_Click"></asp:Button>
                    <button type="button" class="btn buttonColor" data-dismiss="modal">
                        Close
			<span class="btn-icon-right"><i class="fa fa-remove"></i></span>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal Import Excel
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlImportExcel" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel112a">Import File Excel</h4>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
			</button>--%>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class='col-sm-12'>
                                Upload File
					<div class="form-group">
                        <div class='input-group'>
                            <asp:FileUpload ID="FileUploadImportExcel" AllowMultiple="true" runat="server" />
                            <asp:HiddenField ID="ImportExcelOK" runat="server" />
                        </div>
                    </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclick="<%=btnUploadFile.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Upload
			<span class="btn-icon-right"><i class="fa fa-refresh"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnUploadFile" OnClick="btnUploadFile_Click"></asp:Button>
                    <button type="button" onclick="<%=btnCloseModalImport.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Close
			<span class="btn-icon-right"><i class="fa fa-remove"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalImport" OnClick="btnCloseModalImport_Click"></asp:Button>
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
