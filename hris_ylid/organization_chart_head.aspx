<%@ Page Title="Organization Chart Head" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="organization_chart_head.aspx.cs" Inherits="hris_ylid.organization_chart_head" %>

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
    <link href="vendors/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript" src="vendors/date/JS/jquery-1.10.2.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.min.js"></script>
    <script src="plugins/tables/js/datatable/dataTables.fixedColumns.js"></script>

    <script type="text/javascript">
        function FuncUpdate() {
            swal({
                title: 'Update Success',
                text: 'User Successfully Updated',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'organization_chart_head.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncSubmitGroup() {
            swal({
                title: 'Submit Success',
                text: 'Access Group Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
            );
        }
    </script>
    <script type="text/javascript">
        function FuncRemoveGroup() {
            swal({
                title: 'Save Success',
                text: 'Group Successfully Removed',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'organization_chart_head.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncSave() {
            swal({
                title: 'Save Success',
                text: 'New User Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true,
            },
                function redirect() {
                    window.location.href = 'organization_chart_head.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function ErorrPasswordDoesntMacth() {
            swal('Save Failed!', 'passwords do not match', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectEmployee() {
            swal('Save Failed!', 'please select employee', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectAccess() {
            swal('Save Failed!', 'please select user access', 'error');
        }
    </script>
    <script type="text/javascript">
        function passwordfield() {
            swal('Save Failed!', 'please field the password', 'error');
        }
    </script>
    <script type="text/javascript">
        function Confirmpasswordfield() {
            swal('Save Failed!', 'please field the confirmation password', 'error');
        }
    </script>
    <script type="text/javascript">
        function Savedfailed() {
            swal('Save Failed!', 'group already exist', 'error');
        }
    </script>
    <script type="text/javascript">
        function SelectModule() {
            swal({
                title: 'Save Failed!',
                text: 'please select group',
                type: 'error',
                showConfirmButton: true,
                html: true,
            },
                function redirect() {
                    window.location.href = 'organization_chart_head.aspx';
                }
            );
        }
    </script>
         <script type="text/javascript">
             $(document).ready(function () {
                 $(".zero-configuration").DataTable(
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
            <h1 class="page-header text-overflow" style="color: white;">Organization Chart Head</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Master Data</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Administrator</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Organization Chart Head</li>
        </ol>
    </div>

    <asp:HiddenField ID="hlbNIK" runat="server" />
    <asp:HiddenField ID="hlbOid" runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Organization Chart Head</legend>
                                    <div class="control-group">
                                        <div class="row">
                                            <div class='col-sm-12'>
                                                <br />
                                                <div class="table-responsive">
                                                    <asp:GridView ID="TableOrganizationChartHead" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap" AutoGenerateColumns="False" Style="width: 100%"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                                        <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                        <Columns>
                                                            <%--<asp:CommandField ShowSelectButton="True" buttontype="Image" SelectImageUrl="~/images/edit.png" SelectText="Edit" ControlStyle-ForeColor="Blue"/>--%>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnView" CommandName="Tampil" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnView_Click" ToolTip="Views"><i class="fa fa-search"></i> View Access</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Oid" HeaderText="Oid" />
                                                            <asp:BoundField DataField="nik" HeaderText="Employee ID Number" />
                                                            <asp:BoundField DataField="Fullname" HeaderText="Full Name" />
                                                            <asp:BoundField DataField="Position" HeaderText="Position" />
                                                            <asp:BoundField DataField="Division" HeaderText="Division" />
                                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                                            <%--<asp:BoundField DataField="CreateDate" HeaderText="Registration Date" />
                                                            <asp:BoundField DataField="IsActive" HeaderText="Active Status" />--%>
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
		Modal View
	***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlViews" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" onclick="<%=btnAdd.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Add Access
				<span class="btn-icon-right"><i class="fa fa-plus"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnAdd" OnClick="btnAdd_Click"></asp:Button>

                    <button type="button" onclick="<%=btnCloseModalView.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalView" OnClick="btnCloseModalView_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
				</button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-12'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">User Detail</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <div class='col-sm-3'>
                                            Employee ID Number
				                        <div class="form-group">
                                            <div class='input-group'>
                                                <input runat="server" id="txtNIK" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Username" disabled>
                                            </div>
                                        </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Full Name
				                        <div class="form-group">
                                            <div class='input-group'>
                                                <input runat="server" id="txtFullname" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Username" disabled>
                                            </div>
                                        </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Position
                                            <div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtPosition" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Username" disabled>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-3'>
                                            Section
                                        <div class="form-group">
                                            <div class='input-group'>
                                                <input runat="server" id="txtSection" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Username" disabled>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class='col-sm-12'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Organization Access Detail</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <div class='col-sm-12'>
                                            <div class="table-responsive">
                                                <asp:GridView ID="TableOrganizationAccess" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap" AutoGenerateColumns="False" Style="width: 100%"
                                                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                                    <Columns>
                                                        <%--<asp:CommandField ShowSelectButton="True" buttontype="Image" SelectImageUrl="~/images/edit.png" SelectText="Edit" ControlStyle-ForeColor="Blue"/>--%>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnDelete" CommandName="Hapus" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnDelete_Click" ToolTip="Remove"><i class="fa fa-remove"></i> Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Oid" HeaderText="Oid" />
                                                        <asp:BoundField DataField="nik" HeaderText="Employee ID Number" />
                                                        <asp:BoundField DataField="fullname" HeaderText="Fullname" />
                                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                                                        <asp:BoundField DataField="IsActive" HeaderText="IsActive" />
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

                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <!--**********************************
		Modal Add Group Access Role
	***********************************-->
    <div class="modal fade bs-example-modal-lg" id="mdlGroupAccessRole" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1s">Select Group Access</h4>
                    <button type="button" onclick="<%=btnCloseModalGroupAccess.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalGroupAccess" OnClick="btnCloseModalGroupAccess_Click"></asp:Button>
                </div>
                <div class="modal-body">
                    <div class="x_content">
                        <div class="row">
                            <div class='col-sm-12'>
                                Group Access
							<div class="form-group">
                                <div class='input-group'>
                                    <asp:DropDownList ID="ddlGroupAccess" class="selectpicker form-control" data-show-subtext="true" data-live-search="true" AppendDataBoundItems="true"
                                        runat="server" OnSelectedIndexChanged="ddlGroupAccess_SelectedIndexChanged">
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" onclick="<%=btnSubmitGroup.ClientID %>.click()" class="btn mb-1 buttonColor">
                        Submit
					<span class="btn-icon-right"><i class="fa fa-save"></i></span>
                    </button>
                    <asp:Button runat="server" Style="display: none;" ID="btnSubmitGroup" OnClick="btnSubmitGroup_Click"></asp:Button>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
