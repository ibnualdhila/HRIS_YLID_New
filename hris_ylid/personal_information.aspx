<%@ Page Title="Personnel Information" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="personal_information.aspx.cs" Inherits="hris_ylid.personal_information" Async="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    </style>

    <script type="text/javascript" src="vendors/date/JS/jquery-1.10.2.min.js"></script>
    <link href="vendors/sweetalert.css" rel="stylesheet" />
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.js"></script>

    <script type="text/javascript">
        function ShowLoading() {
            swal({
                title: 'Loading...',
                text: 'Please wait!',
                imageUrl: "icons/loader.gif",
                showConfirmButton: false,
                allowOutsideClick: false
            });
        }
    </script>

    <script type="text/javascript">
        function FuncUpdate() {
            swal({
                title: 'Update Success',
                text: 'Personal Information Successfully Updated',
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
                text: 'Personal Information Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'personal_information.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncDelete() {
            swal({
                title: 'Delete Success',
                text: 'Personal Information Successfully Delete',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'personal_information.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".grid").DataTable(
                {
                    scrollY: "500px",
                    scrollX: true,
                    scrollCollapse: true,
                    paging: true
                    //fixedColumns: {
                    //	left: 3
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
            <h1 class="page-header text-overflow" style="color: white;">Personel Information</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Recruitments</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Personel Information</li>
        </ol>
    </div>


    <div hidden="hidden">
<%--        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <rsweb:ReportViewer ID="ReportViewerPrintPersonalInformation" runat="server" Width="100%" Height="100%">
        </rsweb:ReportViewer>
        <asp:Label ID="lbPhoto" runat="server"></asp:Label>
        <asp:Label ID="lbKtp" runat="server"></asp:Label>
        <asp:Label ID="lbEmailCandidate" runat="server"></asp:Label>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Import Data</legend>
                                    <div class="control-group">
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                File Upload :
											<div class="form-group">
                                                <div class='input-group'>
                                                    <asp:FileUpload ID="FileUpload" AllowMultiple="true" runat="server" />
                                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            </div>
                                            <div class='col-sm-12'>
                                                <button type="button" class="btn mb-1 buttonColor" onclick="<%=btnUpload.ClientID %>.click()">
                                                    Upload <span class="btn-icon-right">
                                                        <i class="fa fa-upload"></i></span>
                                                </button>
                                                <asp:Button runat="server" Style="display: none;" ID="btnUpload" OnClick="btnUpload_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Personnel Information</legend>
                                    <asp:GridView ID="TablePersonalInformation" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TablePersonalInformation_RowDataBound" OnRowCommand="TablePersonalInformation_RowCommand"
                                        OnSelectedIndexChanged="TablePersonalInformation_SelectedIndexChanged">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnEdit" CommandName="Ubah" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnEdit_Click" ToolTip="Edit"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnPrint" CommandName="print" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnPrint_Click" ToolTip="Print"><i class="fa fa-print"></i> Print</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnDelete" CommandName="hapus" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnDelete_Click" ToolTip="Delete"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fullname" HeaderText="Full Name" />
                                            <asp:BoundField DataField="candidate_job" HeaderText="Applied Position" />
                                            <asp:BoundField DataField="time_stamp" HeaderText="Time Stamp" />
                                            <asp:BoundField DataField="status_recruitment" HeaderText="Recruitment Status" />
                                            <asp:BoundField DataField="pic_recruitment" HeaderText="Recruitment PIC" />
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
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal Detail Personal Info
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlPersonalInformation" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1">Personal Information</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <%--<div class="row">--%>
                    <ul class="nav nav-tabs" id="myTab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#tab1">Edit Status</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tab2">Email Log</a>
                        </li>
                    </ul>

                    <!-- Tab Content -->
                    <div class="tab-content mt-3">
                        <div class="tab-pane fade show active" id="tab1" role="tabpanel">
                            <fieldset class="scheduler-border">
                                <%--<legend class="scheduler-border">Personal Information</legend>--%>
                                <div class="row">
                                    <div class='col-sm-12'>
                                        <br />
                                    </div>
                                    <div class='col-sm-3'>
                                        Time Stamp
									    <div class="form-group">
                                            <div class='input-group'>
                                                <input runat="server" id="txtTimeStamp" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Time Stamp" required="required" disabled>
                                            </div>
                                        </div>
                                    </div>
                                    <div class='col-sm-3'>
                                        Job Applied
											    <div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtJobApplied" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Job Applied" required="required" disabled>
                                                    </div>
                                                </div>
                                    </div>
                                    <div class='col-sm-3'>
                                        Candidate Name
											        <div class="form-group">
                                                        <div class='input-group'>
                                                            <input runat="server" id="txtCandidateName" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Candidate Name" required="required" disabled>
                                                        </div>
                                                    </div>
                                    </div>
                                    <div class='col-sm-3'>
                                        Gender
										        <div class="form-group">
                                                    <div class='input-group'>
                                                        <input runat="server" id="txtGender" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Gender" required="required" disabled>
                                                    </div>
                                                </div>
                                    </div>

                                    <div class='col-sm-3'>
                                        PIC Recruit
										        <div class="form-group">
                                                    <div class='input-group'>
                                                        <asp:DropDownList ID="ddlPICRecruit" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                            runat="server" OnSelectedIndexChanged="ddlPICRecruit_SelectedIndexChanged">
                                                            <asp:ListItem Enabled="true" Text="Select PIC" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="MEILVA MEILANI POHAN" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="MIRNA CHINTYA DEWI W" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="SITI QISTI TSAUROH" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text ="YOHANES PINTOKO ADHI" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                    </div>
                                    <div class='col-sm-3'>
                                        Recruitment Status
		                                <div class="form-group">
                                            <div class='input-group'>
                                                <asp:DropDownList ID="ddlStatus" class="form-control custom-select mr-sm-2" AppendDataBoundItems="true"
                                                    runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                    <asp:ListItem Enabled="true" Text="Select Status" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Submit" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="On Process" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Hold" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Join" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Reject" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="tab-pane fade show" id="tab2" role="tabpanel">
                            <fieldset class="scheduler-border">
                                <%--<legend class="scheduler-border">Log Email Send</legend>--%>
                                <%--<div class="row">--%>
                                    <%--<div class='col-sm-12'>--%>
                                        <div class='col-sm-12'>
                                            <br />
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="TableLogEmailSend" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnSelectedIndexChanged="TableLogEmailSend_SelectedIndexChanged">
                                                <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="create_date" HeaderText="Send Date" />
                                                    <asp:BoundField DataField="Fullname" HeaderText="Send By" />
                                                    <asp:BoundField DataField="email_recipient" HeaderText="Email Recipient" />
                                                    <asp:BoundField DataField="status_recruitment" HeaderText="Status Recruitment" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    <%--</div>--%>
                                <%--</div>--%>
                            </fieldset>
                        </div>
                        <!-- Other fields here -->
                        <!-- Tab 2 Content -->
                        <%--<div class="tab-pane fade" id="tab2" role="tabpanel">
                                <!-- Your existing Log Email Send table -->
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">Log Email Send</legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered"
                                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                            <Columns>
                                                <asp:BoundField DataField="create_date" HeaderText="Send Date" />
                                                <asp:BoundField DataField="Fullname" HeaderText="Send By" />
                                                <asp:BoundField DataField="email_recipient" HeaderText="Email Recipient" />
                                                <asp:BoundField DataField="status_recruitment" HeaderText="Status Recruitment" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>
                            </div>--%>
                        <%--</div>--%>
                    </div>
                </div>
                
                
                <div class="modal-footer">
                    <button type="button" onclick="<%=btnSubmit.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Submit <span class="btn-icon-right">
                        <i class="fa fa-check"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnSubmit" OnClick="btnSubmit_Click" OnClientClick="ShowLoading()"></asp:Button>
                    <button type="button" data-dismiss="modal" class="btn mb-1 buttonColor">
                        Close <span class="btn-icon-right">
                        <i class="fa fa-close"></i></span>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!--**********************************
			Modal Compise Email
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlComposeEmail" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabelComposeEmail">Compose Email</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-6'>
                            <div class="input-group mb-2">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">To</div>
                                </div>
                                <input type="text" id="txtEmailTo" runat="server" class="form-control" placeholder="TO" disabled>
                            </div>
                        </div>
                        <div class='col-sm-6'>
                            <div class="input-group mb-2">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">CC</div>
                                </div>
                                <input type="text" id="txtCCEmail" runat="server" class="form-control" placeholder="CC" value="ylid.ml.hr@id.yusen-logistics.com" disabled>
                            </div>
                        </div>
                        <div class='col-sm-12'>
                            <div class="input-group mb-2">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">Subject</div>
                                </div>
                                <input type="text" id="txtSubject" runat="server" class="form-control" placeholder="Subject">
                            </div>
                        </div>
                        <div class='col-sm-12'>
                            <div class="form-group">
                                <div class='input-group'>
                                    <textarea id="txtEmailContent" runat="server" class="form-control form-control-lg mb-3" rows="3" placeholder="Enter text..."></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <button type="button" onclick="<%=btnSending.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Send <span class="btn-icon-right">
                            <i class="fa fa-paper-plane"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnSending" OnClick="btnSending_Click"></asp:Button>
                    <button type="button" data-dismiss="modal" class="btn mb-1 buttonColor">
                        Close <span class="btn-icon-right">
                            <i class="fa fa-close"></i></span>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal Confirm Delete
		***********************************-->
    <div class="modal fade" id="mdlDelete" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1121">Confirmation Delete</h4>

                </div>
                <div class="modal-body">
                    <div class="row">

                        <div class='col-sm-3'>

                            <div class="form-group">
                                <div class='input-group'>
                                </div>
                            </div>
                        </div>

                        <div class='col-sm-6'>
                            <h4>Are you sure to delete ID
                                <asp:Label ID="lblIDKTP" runat="server"></asp:Label>
                                ?</h4>
                            <div class="form-group">
                                <div class='input-group'>
                                </div>
                            </div>
                        </div>

                        <div class='col-sm-3'>

                            <div class="form-group">
                                <div class='input-group'>
                                </div>
                            </div>
                        </div>

                        <div class='col-sm-3'>

                            <div class="form-group">
                                <div class='input-group'>
                                </div>
                            </div>
                        </div>

                        <div class='col-sm-3'>
                            <br />
                            <button type="button" onclick="<%=btnYesDelete.ClientID %>.click()" class="btn mb-1 buttonColor">
                                <span class="btn-icon-left"><i class="fa fa-check color-success"></i></span>YES</button>
                            <asp:Button runat="server" Style="display: none;" ID="btnYesDelete" OnClick="btnYesDelete_Click"></asp:Button>

                        </div>
                        <div class='col-sm-3'>
                            <br />
                            <button type="button" class="btn mb-1 btn-rounded buttonColor" data-dismiss="modal">
                                <span class="btn-icon-left"><i class="fa fa-remove color-success"></i></span>NO</button>

                        </div>

                        <div class='col-sm-3'>

                            <div class="form-group">
                                <div class='input-group'>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>
