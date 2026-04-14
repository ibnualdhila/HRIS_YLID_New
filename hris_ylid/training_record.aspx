<%@ Page Title="Training Record" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="training_record.aspx.cs" Inherits="hris_ylid.training_record" %>

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
            <h1 class="page-header text-overflow" style="color: white;">Training Record</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;HR Training</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Training Record</li>
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
                                    <legend class="scheduler-border">Search Filter</legend>
                                    <div class="row">
                                        <div class='col-sm-3'>
                                            Training Start Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtTrainingStartDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="Start Date" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Training End Date
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input type="text" id="txtTrainingEndDate" data-validate-length-range="5,15" runat="server" class="form-control input-rounded datepicker1" placeholder="End Date" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'></div>
                                        
                                        <div class='col-sm-3'>
                                            Training Code
										    <div class="form-group">
                                                <asp:DropDownList ID="ddlTrainingCode" class="form-control selectpicker" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                    runat="server" OnSelectedIndexChanged="ddlTrainingCode_SelectedIndexChanged">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Training Title
                                            <div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtTrainingTitle" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Training Title" >
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Trainer/Organizer
				                            <div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlTrainerOrganizer" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlTrainerOrganizer_SelectedIndexChanged" AutoPostBack="false">
                                                        <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="INTERNAL" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="VENDOR" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="REGIONAL" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="GLOBAL" Value="4"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Participant
				                            <div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtParticipant" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Participant" >
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            NIK
                                            <div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtNIK" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="NIK" >
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Training Status
				                            <div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlStatus" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                        <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="ACTIVE" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="NON ACTIVE" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Training Result
                                            <div class="form-group">
                                                <div class='input-group'>
                                                    <asp:DropDownList ID="ddlTrainingResult" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                                        runat="server" OnSelectedIndexChanged="ddlTrainingResult_SelectedIndexChanged1">
                                                        <asp:ListItem Enabled="true" Text="" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Pass" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fail" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-12'>
                                            <br />
                                        </div>
                                        <div class='col-sm-1'>
										    <div class="form-group">
                                                <div class='input-group'>
                                                    <button type="button" onclick="<%=btnSearch.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                        Search         
												    <span class="btn-icon-right"><i class="fa fa-search"></i></span>
                                                    </button>
                                                    <asp:Button runat="server" Style="display: none;" ID="btnSearch" OnClick="btnSearch_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-2'>
										    <div class="form-group">
                                                <div class='input-group'>
                                                    <button type="button" onclick="<%=btnGenExcelNew.ClientID %>.click()" class="btn mb-1 buttonColor">
                                                        Generate .xlsx
												    <span class="btn-icon-right"><i class="fa fa-search"></i></span>
                                                    </button>
                                                    <asp:Button runat="server" Style="display: none;" ID="btnGenExcelNew" OnClick="btnGenExcelNew_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Training Records</legend>
                                    <div class='col-sm-3'>
                                        <asp:LinkButton runat="server" ID="btnGenExcel" Visible="false" CssClass="btn mb-1 buttonColor" OnClick="btnGenExcel_Click"><i class="fa fa-file-excel-o"></i> Generate .xlsx from Table</asp:LinkButton>
                                        <%--&nbsp;
										<div class="form-group">
											<div class='input-group'>
												<button type="button" onclick="<%=btnGenerateExcel.ClientID %>.click()" class="btn mb-1 btn-secondary">Generate .xlsx
												<span class="btn-icon-right"><i class="fa fa-file-excel-o"></i></span></button>
												<asp:Button runat="server" Style="display: none;" ID="btnGenerateExcel" OnClick="btnGenerateExcel_Click"></asp:Button>
											</div>
										</div>--%>
                                    </div>
                                    <div class="table-responsive">
                                        <asp:GridView ID="TableIndividualTraining" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                            ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableIndividualTraining_RowDataBound" OnRowCommand="TableIndividualTraining_RowCommand" OnSelectedIndexChanged="TableIndividualTraining_SelectedIndexChanged">
                                            <HeaderStyle BackColor="#06183d" ForeColor="White" />

                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Edit">
												<ItemTemplate>
													<asp:LinkButton runat="server" ID="btnEditStatus" CommandName="Ubah" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-info" OnClick="btnEditStatus_Click" ToolTip="Edit"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
												</ItemTemplate>
												</asp:TemplateField>--%>
                                                <%--<asp:BoundField DataField="id" HeaderText="ID" />--%>
                                                <asp:BoundField DataField="training_code" HeaderText="Code" />
                                                <asp:BoundField DataField="training_title" HeaderText="Training Title" />
                                                <asp:BoundField DataField="training_type" HeaderText="Training Type" />
                                                <asp:BoundField DataField="trainer_type" HeaderText="Trainer Type" />
                                                <asp:BoundField DataField="VendorTrainer" HeaderText="External Trainer" />
                                                <asp:BoundField DataField="InternalTrainer" HeaderText="Internal Trainer" />
                                                <asp:BoundField DataField="method" HeaderText="Method" />
                                                <asp:BoundField DataField="location" HeaderText="Location" />
                                                <asp:BoundField DataField="training_start_date" HeaderText="Start Date" />
                                                <asp:BoundField DataField="training_end_date" HeaderText="End Date" />
                                                <asp:BoundField DataField="nik_participant" HeaderText="Employee ID Number" />
                                                <asp:BoundField DataField="Participant" HeaderText="Full Name" />
                                                <asp:BoundField DataField="Position" HeaderText="Title" />
                                                <asp:BoundField DataField="Section" HeaderText="Section" />
                                                <asp:BoundField DataField="training_status" HeaderText="Training Status" />
                                                <asp:BoundField DataField="training_result" HeaderText="Training Result" />
                                                <asp:BoundField DataField="date_complete" HeaderText="Completion Date" />
                                                <asp:BoundField DataField="note" HeaderText="Note" />
                                                <asp:BoundField DataField="createby" HeaderText="Created by" />
                                                <asp:BoundField DataField="create_date" HeaderText="Creation Date" />
                                                <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                                                <asp:BoundField DataField="modified_date" HeaderText="Modification Date" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

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
    <script>
        $(document).ready(function () {
            $(".datepicker1").datepicker({ format: 'yyyy/mm/dd', autoclose: true, todayBtn: 'linked' })
        });
    </script>
</asp:Content>
