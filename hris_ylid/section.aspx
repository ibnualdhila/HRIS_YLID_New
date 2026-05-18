<%@ Page Title="Section" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="section.aspx.cs" Inherits="hris_ylid.section" %>

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
            toastr["success"]("SAVE SUCCESS, New Section Successfully Submited");

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
            toastr["success"]("UPDATE SUCCESS, Section Successfully Updated");

        }
    </script>
    <link href="vendors/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript">
        function FuncUpdate() {
            swal({
                title: 'Update Success',
                text: 'Data Section Successfully Updated',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'section.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function FuncSave() {
            swal({
                title: 'Save Success',
                text: 'Data Section Successfully Submit',
                timer: '2000',
                type: 'success',
                showConfirmButton: false,
                html: true
            },
                function redirect() {
                    window.location.href = 'section.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function DuplicateData() {
            swal({
                type: 'error',
                title: 'Save Failed',
                text: 'Your data input already exist!',
                showConfirmButton: true
            },
                function redirect() {
                    window.location.href = 'section.aspx';
                }
            );
        }
    </script>
    <script type="text/javascript">
        function LocationEmpty() {
            swal('Save Failed!', 'Please Field Section Name', 'error');
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
            <h1 class="page-header text-overflow" style="color: white;">Section</h1>
        </div>
        <ol class="breadcrumb">
            <li><a href="index.aspx">Dashboard</a></li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Master Data</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Organization</li>
            <li class="active">&nbsp;&nbsp;<i class="fa fa-caret-right"></i></li>
            <li class="active">&nbsp;&nbsp;Section</li>
        </ol>
    </div>

    <asp:HiddenField ID="hlbID" runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 col-sm-12 ">
                <div class="card custom-card-body">
                    <div class="card-body">
                        <div class="row">
                            <div class='col-sm-12'>
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">List of Sections</legend>
                                    <div class='col-sm-12'>
                                        <asp:Button runat="server" Visible="true" ID="btnNew" CssClass="btn buttonColor" Text="New" OnClick="btnNew_Click"></asp:Button>
                                    </div>
                                    <asp:GridView ID="TableSection" runat="server" CssClass="table table-striped table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowCommand="TableSection_RowCommand" OnSelectedIndexChanged="TableSection_SelectedIndexChanged">
                                        <HeaderStyle BackColor="#06183d" ForeColor="White" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnEdit" CommandName="Ubah" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn buttonColorGridview" OnClick="btnEdit_Click" ToolTip="Edit"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="id" HeaderText="ID" />
                                            <asp:BoundField DataField="section" HeaderText="Section" />
                                            <asp:BoundField DataField="active" HeaderText="Active" />
                                            <asp:BoundField DataField="createby" HeaderText="Created by" />
                                            <asp:BoundField DataField="create_date" HeaderText="Creation Date" />
                                            <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                                            <asp:BoundField DataField="modified_date" HeaderText="Modification Date" />

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

    <div class="modal fade bd-example-modal-lg" id="mdlAddNewSection" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel1q">Add New Section</h4>
                    <button type="button" onclick="<%=btnCloseModalNew.ClientID %>.click()" class="close"><span>×</span></button>
                    <asp:Button runat="server" Style="display: none;" ID="btnCloseModalNew" OnClick="btnCloseModalNew_Click"></asp:Button>
                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span>
					</button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class='col-sm-12'>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Section Detail</legend>
                                <div class="control-group">
                                    <div class="row">
                                        <div class='col-sm-12'>
                                            <br />
                                        </div>
                                        <div class='col-sm-12'>
                                            Section
											<div class="form-group">
                                                <div class='input-group'>
                                                    <input runat="server" id="txtSectionName" data-validate-length-range="5,15" type="text" class="form-control input-rounded" placeholder="Type Section Name">
                                                </div>
                                            </div>
                                        </div>
                                        <div class='col-sm-6'>
                                            Status
										<div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <div class="input-group-text">
                                                    <asp:CheckBox runat="server" ID="ckActive" Checked="true" />
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

                <div class="modal-footer">

                    <%--<button type="button" data-dismiss="modal" class="btn mb-1 buttonColor">Close <span class="btn-icon-right">
					<i class="fa fa-close"></i></span></button>--%>
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
            $(".datepicker1").datepicker({ format: 'mm/dd/yyyy', timeFormat: "hh:mm:ss", autoclose: true, todayBtn: 'linked' })
        });
    </script>
</asp:Content>
