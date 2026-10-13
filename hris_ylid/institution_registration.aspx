<%@ Page Title="Institution Registration" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="institution_registration.aspx.cs" Inherits="hris_ylid.institution_registration" %>

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

    <script type="text/javascript" src="vendors/date/JS/jquery-1.10.2.min.js"></script>
    <link href="vendors/sweetalert.css" rel="stylesheet" />
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.js"></script>

    <script type="text/javascript">
        function FuncUpdate() {
            swal({
                title: 'Update Success',
                text: 'Data Institution Successfully Updated',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'institution_registration.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncSave() {
            swal({
                title: 'Save Success',
                text: 'Data Institution Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'institution_registration.aspx';
                }
            );
        }
    </script>

    <script type="text/javascript">
        function DuplicateDataInstitutionName() {
            swal({
                type: 'error',
                title: 'Save Failed',
                text: 'Institution name already exist!',
                showConfirmButton: true
            },
                function redirect() {
                    window.location.href = 'institution_registration.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function DuplicateDataBrandName() {
            swal({
                type: 'error',
                title: 'Save Failed',
                text: 'Brand name already exist!',
                showConfirmButton: true
            },
                function redirect() {
                    window.location.href = 'institution_registration.aspx';
                }
            );
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
        function FieldInstitutionName() {
            swal('Save Failed!', 'Please field Institution Name', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldBrandName() {
            swal('Save Failed!', 'Please field Brand Name', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldAddress() {
            swal('Save Failed!', 'Please field Address', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldCity() {
            swal('Save Failed!', 'Please field City', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldPhone() {
            swal('Save Failed!', 'Please field Phone', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldPICName() {
            swal('Save Failed!', 'Please field PIC Name', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldPICPhone() {
            swal('Save Failed!', 'Please field PIC Phone', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldPICEmail() {
            swal('Save Failed!', 'Please field PIC Email', 'error');
        }
    </script>
    <script type="text/javascript">
        function FieldCompanySite() {
            swal('Save Failed!', 'Please field Company Website', 'error');
        }
    </script>

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
            <h1 class="page-header text-overflow" style="color: white;">Institution Registration</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Master Data</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;HR General</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Institution Registration</li>
        </ol>
    </div>

    <asp:HiddenField ID="hlbOK" runat="server" />

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Institutions</legend>
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
                                                    <asp:GridView ID="TableInstitution" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableInstitution_RowDataBound" OnRowCommand="TableInstitution_RowCommand"
                                                        OnSelectedIndexChanged="TableInstitution_SelectedIndexChanged">
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
                                                            <asp:BoundField DataField="institution_name" HeaderText="Name of Institution" />
                                                            <asp:BoundField DataField="address" HeaderText="Address" />
                                                            <asp:BoundField DataField="city" HeaderText="City" />
                                                            <asp:BoundField DataField="pic_name" HeaderText="Name of PIC" />
                                                            <asp:BoundField DataField="pic_phone" HeaderText="Phone Number of PIC" />
                                                            <asp:BoundField DataField="phone" HeaderText="Phone" />
                                                            <asp:BoundField DataField="pic_email" HeaderText="Email of PIC" />
                                                            <asp:BoundField DataField="institution_site" HeaderText="Institution Website" />
                                                            <asp:BoundField DataField="active" HeaderText="Active" />
                                                            <asp:BoundField DataField="createby" HeaderText="Created by" />
                                                            <asp:BoundField DataField="create_date" HeaderText="Creation Date" />
                                                            <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                                                            <asp:BoundField DataField="modified_date" HeaderText="Modification Date" />
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
    <div class="modal fade bs-example-modal-lg" id="mdlAddNewInstitution" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1q">Add New Institution</h4>
                    <button type="button" onclick="<%=btnCloseModalNew.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalNew" OnClick="btnCloseModalNew_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
					</button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-12'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Institution Detail</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <asp:HiddenField ID="hlbID" runat="server" />
                                        <div class='col-sm-12'>
                                            Name of Institution
												<div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtInstitutionName" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Name of Institution">
                                                    </div>
                                                </div>
                                        </div>
                                        <div class='col-sm-12'>
                                            Address
												<div class="form-group">
                                                    <div class='input-group'>
                                                        <textarea class="form-control h-150px" rows="2" id="txtAddress" runat="server"></textarea>
                                                    </div>
                                                </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            City
												<div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtCity" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type City">
                                                    </div>
                                                </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Phone
												<div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtPhone" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Phone number">
                                                    </div>
                                                </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Name of PIC
												<div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtPICName" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Name of PIC">
                                                    </div>
                                                </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Phone Number of PIC
												<div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtPICPhone" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Phone Number of PIC">
                                                    </div>
                                                </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Email of PIC
												<div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtPICEmail" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Email of PIC">
                                                    </div>
                                                </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Institution Website
												<div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtInstitutionSite" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Institution Website">
                                                    </div>
                                                </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Status
												<div class="input-group mb-3">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <asp:CheckBox runat="server" ID="ckActive" />
                                                        </div>
                                                    </div>
                                                    <input runat="server" id="txtActive" type="text" class="form-control" value="ACTIVE" disabled>
                                                </div>
                                        </div>
                                        <div class='col-sm-6'>
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
                        </div>
                        <div class='col-sm-12'>
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
                    </div>
                </div>
                <%--<fieldset class="scheduler-border">
									<legend class="scheduler-border">Upload Document</legend>
									<div class="control-group">
										<div class="row">
											<div class='col-sm-12'>
												Upload Files
												<div class="form-group">
													<div class='input-group'>
														<asp:FileUpload ID="FileUploadDocument" AllowMultiple="true" runat="server"/>
													</div>
												</div>
											</div>
											<div class='col-sm-12'>
												Detail Upload:
												<div class="form-group">
													<div class='input-group'>
														<asp:Label ID="label3" runat="server" ForeColor="Black"></asp:Label>
													</div>
												</div>
											</div>
											<div class='col-sm-12'>
												<div class="form-group">
													<div class='input-group'>
														<asp:Label ID="label1" runat="server" ForeColor="Green"></asp:Label><br />
													</div>
												</div>
											</div>
											<div class='col-sm-12'>
												<div class="form-group">
													<div class='input-group'>
														<asp:Label ID="labbel2" runat="server" ForeColor="Red"></asp:Label>
													</div>
												</div>
											</div>
											
										</div>
									</div>
								</fieldset>--%>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
