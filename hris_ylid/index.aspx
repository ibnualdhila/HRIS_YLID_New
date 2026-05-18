<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="hris_ylid.index" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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

        .buttonColorCard {
            background-color: #ff6d10;
            color: white;
        }

            .buttonColorCard:hover {
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
    <script type="text/javascript">
        function CertiEmpty() {
            swal('File not found!', 'There is no certificate for this training', 'error');
        }
    </script>
    <style>
        .page-head {
            background-color: #06183d; /* Change this to your desired background color */
            color: white; /* Text color for the header */
            padding: 3px 2.938rem; /* Optional padding for the header */
            height: 160px;
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
            <div class="pad-all text-center">
                <h3 style="color: white;">Welcome to HR Information System - YLID</h3>
                <p1>
                    Dashboard Overview<p></p>
                </p1>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hlbFullname" runat="server" />
    <asp:HiddenField ID="hlbLvlUser" runat="server" />
    <asp:HiddenField ID="hlbOidEmployees" runat="server" />
    <asp:HiddenField ID="lblGroupChart" runat="server" />
    <asp:HiddenField ID="hlbNameChartGroup" runat="server" />

    <div class="container-fluid" id="divListEmployees" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Employees</legend>
                                    <div class="control-group">
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="TableListEmployees" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                                        <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Detail">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnViewDetail" CommandName="Lihat" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnViewDetail_Click" ToolTip="View Training"><i class="fa fa-search"></i> View Training</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="nik" HeaderText="Employee ID Number" />
                                                            <asp:BoundField DataField="fullname" HeaderText="Full Name" />
                                                            <asp:BoundField DataField="Position" HeaderText="Tittle" />
                                                            <asp:BoundField DataField="division" HeaderText="Division" />
                                                            <asp:BoundField DataField="section" HeaderText="Section" />
                                                            <asp:BoundField DataField="SubSection" HeaderText="Subsection" />
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

    <div class="container-fluid" id="div1" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border" id="ViewHRGeneral" runat="server">
                                    <legend class="scheduler-border">HR General</legend>
                                    <div class="container-fluid mt-3">
                                        <div runat="server" class="row">
                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h3 class="card-title text-white">Active Employees</h3>
                                                        <div class="d-inline-block">
                                                            <h2 class="text-white">
                                                                <asp:Label ID="lbTotalEmployees" runat="server"></asp:Label></h2>
                                                            <p class="text-white mb-0">Active Total</p>
                                                        </div>
                                                        <span class="float-right display-5 opacity-5"><i class="fa fa-users"></i></span>
                                                    </div>
                                                    <div class="card-footer">
                                                        <a href="#">
                                                            <asp:LinkButton runat="server" OnClick="ViewDetailEmployees_Click">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h3 class="card-title text-white">Outsourced Employees</h3>
                                                        <div class="d-inline-block">
                                                            <h2 class="text-white">
                                                                <asp:Label ID="lbTotalOutsourcedEmployees" runat="server"></asp:Label></h2>
                                                            <p class="text-white mb-0">Active Total</p>
                                                        </div>
                                                        <span class="float-right display-5 opacity-5"><i class="fa fa-users"></i></span>
                                                    </div>
                                                    <div class="card-footer">
                                                        <a href="#">
                                                            <asp:LinkButton runat="server" OnClick="ViewDetailOutsourcedEmployees_Click">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h3 class="card-title text-white">Job Applications</h3>
                                                        <div class="d-inline-block">
                                                            <h2 class="text-white">
                                                                <asp:Label ID="lbTotalJobApplication" runat="server"></asp:Label></h2>
                                                            <p class="text-white mb-0">Apply Total</p>
                                                        </div>
                                                        <span class="float-right display-5 opacity-5"><i class="fa fa-binoculars"></i></span>
                                                    </div>
                                                    <div class="card-footer">
                                                        <a href="#">
                                                            <asp:LinkButton runat="server" OnClick="ViewDetailJobApplication_Click">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h3 class="card-title text-white">Company Agreement to Expire</h3>
                                                        <div class="d-inline-block">
                                                            <h2 class="text-white">
                                                                <asp:Label ID="lbTotalCompanyAgreementWillExpired" runat="server"></asp:Label></h2>
                                                            <p class="text-white mb-0">Will be expired</p>
                                                        </div>
                                                        <span class="float-right display-5 opacity-5"><i class="fa fa-handshake-o"></i></span>
                                                    </div>
                                                    <div class="card-footer">
                                                        <a href="#">
                                                            <asp:LinkButton runat="server" OnClick="ViewDetailCompanyAgreementWillExpired_Click">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h3 class="card-title text-white">Employee Agreement to Expire</h3>
                                                        <div class="d-inline-block">
                                                            <h2 class="text-white">
                                                                <asp:Label ID="lbTotalEmployeeAgreementWillExpired" runat="server"></asp:Label></h2>
                                                            <p class="text-white mb-0">Will be expired</p>
                                                        </div>
                                                        <span class="float-right display-5 opacity-5"><i class="fa fa-handshake-o"></i></span>
                                                    </div>
                                                    <div class="card-footer">
                                                        <a href="#">
                                                            <asp:LinkButton runat="server" OnClick="ViewDetailEmployeeAgreementWillExpired_Click">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h3 class="card-title text-white">Retirement</h3>
                                                        <div class="d-inline-block">
                                                            <h2 class="text-white">
                                                                <asp:Label ID="lbTotalEmployeeWillRetired" runat="server"></asp:Label></h2>
                                                            <p class="text-white mb-0">Will retire</p>
                                                        </div>
                                                        <span class="float-right display-5 opacity-5"><i class="fa fa-user-times"></i></span>
                                                    </div>
                                                    <div class="card-footer">
                                                        <a href="#">
                                                            <asp:LinkButton runat="server" OnClick="ViewDetailEmployeeWillRetired_Click">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border" id="ViewHRTraining" runat="server">
                                    <legend class="scheduler-border">HR Training</legend>
                                    <div class="container-fluid mt-3">
                                        <div runat="server" class="row">
                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h3 class="card-title text-white">Course/Training</h3>
                                                        <div class="d-inline-block">
                                                            <h2 class="text-white">
                                                                <asp:Label ID="lbTotalTraining" runat="server"></asp:Label></h2>
                                                            <p class="text-white mb-0">Active Total</p>
                                                            <br />
                                                        </div>
                                                        <span class="float-right display-5 opacity-5"><i class="fa fa-graduation-cap"></i></span>
                                                    </div>
                                                    <div class="card-footer">
                                                        <a href="#">
                                                            <asp:LinkButton runat="server" OnClick="ViewDetailTotalTraining_Click">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton></a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h3 class="card-title text-white">Participants</h3>
                                                        <div class="d-inline-block">
                                                            <h2 class="text-white">
                                                                <asp:Label ID="lbTotalParticipant" runat="server"></asp:Label></h2>
                                                            <p class="text-white mb-0">Employee Total</p>
                                                            <br />
                                                        </div>
                                                        <span class="float-right display-5 opacity-5"><i class="fa fa-users"></i></span>
                                                    </div>
                                                    <div class="card-footer">
                                                        <a href="#">
                                                            <asp:LinkButton runat="server" OnClick="ViewDetailTotalParticipant_Click">View Details <i class="fa fa-arrow-circle-right"></i></asp:LinkButton></a>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h2 class="card-title text-white text-center"><i class="fa fa-graduation-cap"></i></h2>
                                                        <h3 class="card-title text-white text-center">Training Result</h3>
                                                        <div class="row">
                                                            <div class="col-6 border-right">
                                                                <p class="text-center">
                                                                    PASS
                                                                </p>
                                                                <h4 class="text-center">
                                                                    <asp:LinkButton Style="color: white; font-family: Arial,Verdana;" OnClick="DetailPass_Click" runat="server" class="badge badge-pill bg-facebook" Text="">
                                                                        <asp:Label ID="lblPass" runat="server" Style="color: white; font-family: Arial,Verdana;" Text=""></asp:Label><asp:Label ID="lblPersenPass" runat="server" Style="color: white; font-family: Arial,Verdana;" Text=""></asp:Label>
                                                                    </asp:LinkButton>
                                                                </h4>

                                                            </div>
                                                            <div class="col-6">
                                                                <p class="text-center">
                                                                    FAIL
                                                                </p>
                                                                <h4 class="text-center">
                                                                    <asp:LinkButton Style="color: white; font-family: Arial,Verdana;" OnClick="DetailFail_Click" runat="server" class="badge badge-pill bg-danger" Text="">
                                                                        <asp:Label ID="lblFail" runat="server" Style="color: white; font-family: Arial,Verdana;" Text=""></asp:Label><asp:Label ID="lblPersenFail" runat="server" Style="color: white; font-family: Arial,Verdana;" Text=""></asp:Label>
                                                                    </asp:LinkButton>
                                                                </h4>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="card-footer">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-4 col-sm-6">
                                                <div class="card gradient-3">
                                                    <div class="card-body">
                                                        <h2 class="card-title text-white text-center"><i class="fa fa-graduation-cap"></i></h2>
                                                        <h3 class="card-title text-white text-center">Validity Period</h3>
                                                        <div class="row">
                                                            <div class="col-6 border-right">
                                                                <p class="text-center">
                                                                    Will be expired
                                                                </p>
                                                                <h4 class="text-center">
                                                                    <asp:LinkButton Style="color: white; font-family: Arial,Verdana;" OnClick="DetailWillBeExpired_Click" runat="server" class="badge badge-pill bg-facebook" Text="">
                                                                        <asp:Label ID="lbWillbeExpired" runat="server" Style="color: white; font-family: Arial,Verdana;" Text=""></asp:Label>
                                                                    </asp:LinkButton>
                                                                </h4>

                                                            </div>
                                                            <div class="col-6">
                                                                <p class="text-center">
                                                                    Expired
                                                                </p>
                                                                <h4 class="text-center">
                                                                    <asp:LinkButton Style="color: white; font-family: Arial,Verdana;" OnClick="DetailExpired_Click" runat="server" class="badge badge-pill bg-danger" Text="">
                                                                        <asp:Label ID="lbExpired" runat="server" Style="color: white; font-family: Arial,Verdana;" Text=""></asp:Label>
                                                                    </asp:LinkButton>
                                                                </h4>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="card-footer">
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
    </div>

    <!-- View Training modal -->
    <div class="modal fade bs-example-modal-lg" id="mdlViewTraining" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1">List of Trainings</h4>
                    <button type="button" onclick="<%=btnCloseModalViewTraining.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalViewTraining" OnClick="btnCloseModalViewTraining_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
					</button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-12'>
                            <button type="button" onclick="<%=btnGenerateExcelTraining.ClientID %>.click()" class="btn mb-1 buttonColor">
                                Generate Excel
							<span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span>
                            </button>
                            <asp:Button runat="server" Style="display: none;" ID="btnGenerateExcelTraining" OnClick="btnGenerateExcelTraining_Click"></asp:Button>
                            <div class="table-responsive">
                                <asp:GridView ID="TableViewTraining" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap" AutoGenerateColumns="False" Style="width: 100%"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Detail">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnDownloadCertificate" CommandName="DownloadCertificate" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnDownloadCertificate_Click" ToolTip="Download Certificate"><i class="fa fa-download"></i> Download Certificate</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nik_participant" HeaderText="Employee ID Number" />
                                        <asp:BoundField DataField="Participant" HeaderText="Participant" />
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
                                        <%--<asp:BoundField DataField="Position" HeaderText="Position" />
						<asp:BoundField DataField="Section" HeaderText="Section" />--%>
                                        <asp:BoundField DataField="training_status" HeaderText="Training Status" />
                                        <asp:BoundField DataField="training_result" HeaderText="Training Result" />
                                        <asp:BoundField DataField="date_complete" HeaderText="Completion Date" />
                                        <asp:BoundField DataField="note" HeaderText="Note" />
                                        <asp:BoundField DataField="certificate" HeaderText="Certificate" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <%--<button type="button" onclick="<%=btnCloseDetailTraining.ClientID %>.click()" class="btn mb-1 buttonColor">Close <span class="btn-icon-right">
					<i class="fa fa-close"></i></span></button>
					<asp:Button runat="server" Style="display: none;" ID="btnCloseDetailTraining" OnClick="btnCloseDetailTraining_Click"></asp:Button>--%>
                </div>
            </div>
        </div>
    </div>

    <!-- View Detail User Training modal -->
    <div class="modal fade bs-example-modal-lg" id="mdlViewUserTraining" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabelsd1">List of Trainings</h4>
                    <button type="button" onclick="<%=btnCloseModalDetailUserTraining.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalDetailUserTraining" OnClick="btnCloseModalDetailUserTraining_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
					</button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:HiddenField ID="lblNIK" runat="server" />
                        <div class='col-sm-12'>
                            <button type="button" onclick="<%=btnGenerateExcel.ClientID %>.click()" class="btn mb-1 buttonColor">
                                Generate Excel
							<span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span>
                            </button>
                            <asp:Button runat="server" Style="display: none;" ID="btnGenerateExcel" OnClick="btnGenerateExcel_Click"></asp:Button>
                            <div class="table-responsive">
                                <asp:GridView ID="TableViewUserTraining" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap" AutoGenerateColumns="False" Style="width: 100%"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Participant" HeaderText="Participant" />
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
                                        <%--<asp:BoundField DataField="Position" HeaderText="Position" />
						<asp:BoundField DataField="Section" HeaderText="Section" />--%>
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

                <div class="modal-footer">
                    <%--<button type="button" onclick="<%=btnCloseDetailTraining.ClientID %>.click()" class="btn mb-1 buttonColor">Close <span class="btn-icon-right">
					<i class="fa fa-close"></i></span></button>
					<asp:Button runat="server" Style="display: none;" ID="btnCloseDetailTraining" OnClick="btnCloseDetailTraining_Click"></asp:Button>--%>
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
			Modal View Detail
		***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlViewDetail" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1w">View Detail</h4>
                    <button type="button" onclick="<%=btnCloseModalViewDetail.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalViewDetail" OnClick="btnCloseModalViewDetail_Click"></asp:Button>

                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-12'>
                            <fieldset class="scheduler-border" id="ViewTableDetail" runat="server" visible="false">
                                <legend class="scheduler-border">
                                    <asp:Label ID="lbNameTable" runat="server"></asp:Label></legend>
                                <div class="table-responsive">
                                    <%--Detail Employee--%>
                                    <asp:GridView ID="TableEmployees" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="nik" HeaderText="Employee ID Number" />
                                            <asp:BoundField DataField="fullname" HeaderText="Full Name" />
                                            <asp:BoundField DataField="Position" HeaderText="Position" />
                                            <asp:BoundField DataField="Division" HeaderText="Division" />
                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                            <asp:BoundField DataField="SubSection" HeaderText="Subsection" />
                                            <asp:BoundField DataField="report_position" HeaderText="Report to Position" />
                                            <asp:BoundField DataField="code_location" HeaderText="Location Code" />
                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                            <asp:BoundField DataField="category" HeaderText="Category" />
                                            <asp:BoundField DataField="join_date" HeaderText="Join Date" />
                                            <asp:BoundField DataField="date_of_birth" HeaderText="Date of Birth" />
                                            <asp:BoundField DataField="WORK PERIOD" HeaderText="Work Period" />
                                            <asp:BoundField DataField="Retired Date" HeaderText="Retirement Date" />
                                        </Columns>
                                    </asp:GridView>

                                    <%--Detail OutSourced Employee--%>
                                    <asp:GridView ID="TableOutsourcedEmployees" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="nik" HeaderText="Employee ID Number" />
                                            <asp:BoundField DataField="fullname" HeaderText="Full Name" />
                                            <asp:BoundField DataField="Position" HeaderText="Position" />
                                            <asp:BoundField DataField="Division" HeaderText="Division" />
                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                            <asp:BoundField DataField="SubSection" HeaderText="Subsection" />
                                            <asp:BoundField DataField="report_position" HeaderText="Report to Position" />
                                            <asp:BoundField DataField="code_location" HeaderText="Location Code" />
                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                            <asp:BoundField DataField="category" HeaderText="Category" />
                                            <asp:BoundField DataField="institution_name" HeaderText="Institution Name" />
                                            <asp:BoundField DataField="join_date" HeaderText="Join Date" />
                                            <asp:BoundField DataField="date_of_birth" HeaderText="Date of Birth" />
                                            <asp:BoundField DataField="WORK PERIOD" HeaderText="Work Period" />
                                            <asp:BoundField DataField="Retired Date" HeaderText="Retirement Date" />
                                        </Columns>
                                    </asp:GridView>

                                    <%--Detail Job Application--%>
                                    <asp:GridView ID="TablePersonalInformation" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
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
                                        </Columns>
                                    </asp:GridView>

                                    <%--Detail Company Agreement--%>
                                    <asp:GridView ID="TableCompanyAgreement" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableCompanyAgreement_RowDataBound">
                                        <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="nik_pic" HeaderText="Employee ID Number of PIC" />
                                            <asp:BoundField DataField="PIC" HeaderText="Name of PIC" />
                                            <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                                            <asp:BoundField DataField="agreement_name" HeaderText="Name of Agreement" />
                                            <asp:BoundField DataField="Institution" HeaderText="Institution" />
                                            <asp:BoundField DataField="institution_services" HeaderText="Institution Service" />
                                            <asp:BoundField DataField="effective_date" HeaderText="Effective Date" />
                                            <asp:BoundField DataField="expiration_date" HeaderText="Expiration Date" />
                                            <asp:BoundField DataField="DayToExpired" HeaderText="Days until Expiration Date" />
                                            <%--<asp:BoundField DataField="reminder" HeaderText="Reminder" />
												<asp:BoundField DataField="notification_to_email" HeaderText="Start Email Notification" />
												<asp:BoundField DataField="email" HeaderText="Email Address" />--%>
                                            <asp:BoundField DataField="remaks" HeaderText="Remark" />
                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                        </Columns>
                                    </asp:GridView>

                                    <%--Detail Employee Agreement--%>
                                    <asp:GridView ID="TableEmployeeAgreement" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableEmployeeAgreement_RowDataBound">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="nik_employee" HeaderText="Employee ID Number" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name of Employee" />
                                            <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                            <asp:BoundField DataField="JoinDate" HeaderText="Join Date" />
                                            <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />
                                            <asp:BoundField DataField="WorkingStatus" HeaderText="Employment Status" />
                                            <asp:BoundField DataField="nik_pic" HeaderText="PIC Employee Number" />
                                            <asp:BoundField DataField="PIC" HeaderText="Name of PIC" />
                                            <asp:BoundField DataField="end_of_agreement_date" HeaderText="End of Agreement" />
                                            <asp:BoundField DataField="description" HeaderText="Description" />
                                            <asp:BoundField DataField="DayToEndOfAgreement" HeaderText="Days until End of Agreement" />
                                            <%--<asp:BoundField DataField="reminder" HeaderText="Reminder" />
												<asp:BoundField DataField="notification_to_email" HeaderText="Start Email Notification" />
												<asp:BoundField DataField="email" HeaderText="Email Address" />--%>
                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                        </Columns>
                                    </asp:GridView>

                                    <%--Detail Employee Will Retired--%>
                                    <asp:GridView ID="TableEmployeeWillRetired" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableEmployeeWillRetired_RowDataBound">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="nik" HeaderText="Employee ID Number" />
                                            <asp:BoundField DataField="fullname" HeaderText="Full Name" />
                                            <asp:BoundField DataField="Position" HeaderText="Position" />
                                            <asp:BoundField DataField="Division" HeaderText="Division" />
                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                            <asp:BoundField DataField="SubSection" HeaderText="Subsection" />
                                            <asp:BoundField DataField="report_position" HeaderText="Report to Position" />
                                            <asp:BoundField DataField="code_location" HeaderText="Location" />
                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                            <asp:BoundField DataField="category" HeaderText="Category" />
                                            <asp:BoundField DataField="join_date" HeaderText="Join Date" />
                                            <asp:BoundField DataField="date_of_birth" HeaderText="Date of Birth" />
                                            <asp:BoundField DataField="Retired Date" HeaderText="Retirement Date" />
                                            <asp:BoundField DataField="WORK PERIOD" HeaderText="Work Period" />
                                        </Columns>
                                    </asp:GridView>

                                    <%--Detail Training Active Total--%>
                                    <asp:GridView ID="TableTrainingActiveTotal" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="training_code" HeaderText="Code" />
                                            <asp:BoundField DataField="training_title" HeaderText="Title" />
                                            <asp:BoundField DataField="training_start_date" HeaderText="Start Date" />
                                            <asp:BoundField DataField="training_end_date" HeaderText="End Date" />
                                            <asp:BoundField DataField="trainer_type" HeaderText="Trainer Type" />
                                            <asp:BoundField DataField="Vendor Trainer" HeaderText="Vendor Name" />
                                            <asp:BoundField DataField="Internal Trainer" HeaderText="Internal Name" />
                                            <asp:BoundField DataField="location" HeaderText="Location" />
                                            <asp:BoundField DataField="method" HeaderText="Method" />
                                            <asp:BoundField DataField="certificate" HeaderText="Certificate" />
                                            <asp:BoundField DataField="training_type" HeaderText="Training Type" />
                                            <asp:BoundField DataField="regulation" HeaderText="Regulation" />
                                            <asp:BoundField DataField="government_institution" HeaderText="Institution" />
                                            <asp:BoundField DataField="validity_period" HeaderText="Validity period" />
                                            <asp:BoundField DataField="cost" HeaderText="Cost" />
                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                        </Columns>
                                    </asp:GridView>

                                    <%--Detail Training Participant--%>
                                    <asp:HiddenField ID="hlbNIKParticipant" runat="server" />
                                    <asp:HiddenField ID="hlbCerti" runat="server" />
                                    <asp:GridView ID="TableTrainingParticipant" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Detail">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnViewTraining" CommandName="Lihat" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnViewTraining_Click" ToolTip="View Training"><i class="fa fa-search"></i> View Training</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nik_participant" HeaderText="Employee ID Number" />
                                            <asp:BoundField DataField="Participant" HeaderText="Participant" />
                                            <asp:BoundField DataField="Position" HeaderText="Position" />
                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                        </Columns>
                                    </asp:GridView>

                                    <%--Detail Participant Training Pass--%>
                                    <asp:GridView ID="TableParticipantTrainingPass" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Participant" HeaderText="Participant" />
                                            <asp:BoundField DataField="Position" HeaderText="Position" />
                                            <asp:BoundField DataField="Section" HeaderText="Section" />
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

                                    <%--Detail Participant Training Fail--%>
                                    <asp:GridView ID="TableParticipantTrainingFail" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Participant" HeaderText="Participant" />
                                            <asp:BoundField DataField="Position" HeaderText="Position" />
                                            <asp:BoundField DataField="Section" HeaderText="Section" />
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
                            </fieldset>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <!-- View Detail Training Will be expired modal -->
    <div class="modal fade bs-example-modal-lg" id="mdlViewUserValidityWillExpired" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabelsdasd1">List of Trainings - Will be expired</h4>
                    <button type="button" onclick="<%=btnCloseModalWillExpired.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalWillExpired" OnClick="btnCloseModalWillExpired_Click"></asp:Button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <div class='col-sm-12'>
                            <button type="button" onclick="<%=btnGenerateExcelWillExpired.ClientID %>.click()" class="btn mb-1 buttonColor">
                                Generate Excel
				            <span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span>
                            </button>
                            <asp:Button runat="server" Style="display: none;" ID="btnGenerateExcelWillExpired" OnClick="btnGenerateExcelWillExpired_Click"></asp:Button>
                            <div class="table-responsive">
                                <asp:GridView ID="TableWillExpired" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                    <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="training_code" HeaderText="Code" />
                                        <asp:BoundField DataField="training_title" HeaderText="Title" />
                                        <asp:BoundField DataField="training_start_date" HeaderText="Start Date" />
                                        <asp:BoundField DataField="training_end_date" HeaderText="End Date" />
                                        <asp:BoundField DataField="trainer_type" HeaderText="Trainer Type" />
                                        <asp:BoundField DataField="Vendor Trainer" HeaderText="Vendor Name" />
                                        <asp:BoundField DataField="Internal Trainer" HeaderText="Internal Name" />
                                        <asp:BoundField DataField="location" HeaderText="Location" />
                                        <asp:BoundField DataField="method" HeaderText="Method" />
                                        <asp:BoundField DataField="certificate" HeaderText="Certificate" />
                                        <asp:BoundField DataField="training_type" HeaderText="Training Type" />
                                        <asp:BoundField DataField="regulation" HeaderText="Regulation" />
                                        <asp:BoundField DataField="government_institution" HeaderText="Institution" />
                                        <asp:BoundField DataField="validity_period" HeaderText="Validity period" />
                                        <asp:BoundField DataField="cost" HeaderText="Cost" />
                                        <asp:BoundField DataField="status" HeaderText="Status" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <%--<button type="button" onclick="<%=btnCloseDetailTraining.ClientID %>.click()" class="btn mb-1 buttonColor">Close <span class="btn-icon-right">
		<i class="fa fa-close"></i></span></button>
		<asp:Button runat="server" Style="display: none;" ID="btnCloseDetailTraining" OnClick="btnCloseDetailTraining_Click"></asp:Button>--%>
                </div>
            </div>
        </div>
    </div>

    <!-- View Detail Training expired modal -->
    <div class="modal fade bs-example-modal-lg" id="mdlViewUserValidityExpired" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabelsdasdjhk1">List of Trainings - Expired</h4>
                    <button type="button" onclick="<%=btnCloseModalExpired.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalExpired" OnClick="btnCloseModalExpired_Click"></asp:Button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <div class='col-sm-12'>
                            <button type="button" onclick="<%=btnGenerateExcelExpired.ClientID %>.click()" class="btn mb-1 buttonColor">
                                Generate Excel
			            <span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span>
                            </button>
                            <asp:Button runat="server" Style="display: none;" ID="btnGenerateExcelExpired" OnClick="btnGenerateExcelExpired_Click"></asp:Button>
                            <div class="table-responsive">
                                <asp:GridView ID="TableExpired" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                    <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" BackColor="#06183d" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="training_code" HeaderText="Code" />
                                        <asp:BoundField DataField="training_title" HeaderText="Title" />
                                        <asp:BoundField DataField="training_start_date" HeaderText="Start Date" />
                                        <asp:BoundField DataField="training_end_date" HeaderText="End Date" />
                                        <asp:BoundField DataField="trainer_type" HeaderText="Trainer Type" />
                                        <asp:BoundField DataField="Vendor Trainer" HeaderText="Vendor Name" />
                                        <asp:BoundField DataField="Internal Trainer" HeaderText="Internal Name" />
                                        <asp:BoundField DataField="location" HeaderText="Location" />
                                        <asp:BoundField DataField="method" HeaderText="Method" />
                                        <asp:BoundField DataField="certificate" HeaderText="Certificate" />
                                        <asp:BoundField DataField="training_type" HeaderText="Training Type" />
                                        <asp:BoundField DataField="regulation" HeaderText="Regulation" />
                                        <asp:BoundField DataField="government_institution" HeaderText="Institution" />
                                        <asp:BoundField DataField="validity_period" HeaderText="Validity period" />
                                        <asp:BoundField DataField="cost" HeaderText="Cost" />
                                        <asp:BoundField DataField="status" HeaderText="Status" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <%--<button type="button" onclick="<%=btnCloseDetailTraining.ClientID %>.click()" class="btn mb-1 buttonColor">Close <span class="btn-icon-right">
	<i class="fa fa-close"></i></span></button>
	<asp:Button runat="server" Style="display: none;" ID="btnCloseDetailTraining" OnClick="btnCloseDetailTraining_Click"></asp:Button>--%>
                </div>
            </div>
        </div>
    </div>

    <script>
        var pass = document.getElementById('<%=lblPass.ClientID %>').innerText;
        var fail = document.getElementById('<%=lblFail.ClientID %>').innerText;
        var randomScalingFactor = function () {
            return Math.round(Math.random() * 100);
        };
        var randomColorFactor = function () {
            return Math.round(Math.random() * 255);
        };
        var randomColor = function (opacity) {
            return 'rgba(' + randomColorFactor() + ',' + randomColorFactor() + ',' + randomColorFactor() + ',' + (opacity || '.3') + ')';
        };

        var ctx = document.getElementById("myPieChart1");
        var myPieChart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        pass,
                        fail,

                    ],
                    backgroundColor: [
                        "#213c70",
                        "#F7464A",

                    ],
                    label: 'Expenditures'
                }],
                labels: [
                    "Pass:" + pass,
                    "Fail:" + fail

                ]
            },

            options: {
                responsive: true,
                legend: {
                    position: 'right',
                    display: false,
                },

                title: {
                    display: false,
                    text: 'Training Result'

                },
                animation: {
                    animateScale: true,
                    animateRotate: true
                },

                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            var dataset = data.datasets[tooltipItem.datasetIndex];
                            var meta = dataset._meta[Object.keys(dataset._meta)[0]];
                            var total = meta.total;
                            var currentValue = dataset.data[tooltipItem.index];
                            var percentage = parseFloat((currentValue / total * 100).toFixed(1));
                            return currentValue + ' (' + percentage + '%)';
                        },
                        title: function (tooltipItem, data) {
                            return data.labels[tooltipItem[0].index];
                        }
                    }
                }
            }
        });

    </script>
    <script type="text/javascript" src="vendors/date/JS/jquery-1.10.2.min.js"></script>

    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.js"></script>
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
</asp:Content>
