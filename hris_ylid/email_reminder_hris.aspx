<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email_reminder_hris.aspx.cs" Inherits="hris_ylid.email_reminder_hris" Async="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>HRIS | Email Reminder </title>
    <meta name="description" content="Home screen that contains stats, charts, call to action buttons and various listing elements." />
    <!-- Favicon Tags Start -->
    <%--<link rel="apple-touch-icon-precomposed" sizes="57x57" href="OtherThemes/acorn/img/favicon/apple-touch-icon-57x57.png" />
	<link rel="apple-touch-icon-precomposed" sizes="114x114" href="OtherThemes/acorn/img/favicon/apple-touch-icon-114x114.png" />
	<link rel="apple-touch-icon-precomposed" sizes="72x72" href="OtherThemes/acorn/img/favicon/apple-touch-icon-72x72.png" />
	<link rel="apple-touch-icon-precomposed" sizes="144x144" href="OtherThemes/acorn/img/favicon/apple-touch-icon-144x144.png" />
	<link rel="apple-touch-icon-precomposed" sizes="60x60" href="OtherThemes/acorn/img/favicon/apple-touch-icon-60x60.png" />
	<link rel="apple-touch-icon-precomposed" sizes="120x120" href="OtherThemes/acorn/img/favicon/apple-touch-icon-120x120.png" />
	<link rel="apple-touch-icon-precomposed" sizes="76x76" href="OtherThemes/acorn/img/favicon/apple-touch-icon-76x76.png" />
	<link rel="apple-touch-icon-precomposed" sizes="152x152" href="OtherThemes/acorn/img/favicon/apple-touch-icon-152x152.png" />--%>
    <link rel="icon" type="image/png" href="images/favicon.ico" sizes="196x196" />
    <%--<link rel="icon" type="image/png" href="OtherThemes/acorn/img/favicon/favicon-96x96.png" sizes="96x96" />
	<link rel="icon" type="image/png" href="OtherThemes/acorn/img/favicon/favicon-32x32.png" sizes="32x32" />
	<link rel="icon" type="image/png" href="OtherThemes/acorn/img/favicon/favicon-16x16.png" sizes="16x16" />
	<link rel="icon" type="image/png" href="OtherThemes/acorn/img/favicon/favicon-128.png" sizes="128x128" />--%>
    <meta name="application-name" content="&nbsp;" />
    <meta name="msapplication-TileColor" content="#FFFFFF" />
    <meta name="msapplication-TileImage" content="OtherThemes/acorn/img/favicon/mstile-144x144.png" />
    <meta name="msapplication-square70x70logo" content="OtherThemes/acorn/img/favicon/mstile-70x70.png" />
    <meta name="msapplication-square150x150logo" content="OtherThemes/acorn/img/favicon/mstile-150x150.png" />
    <meta name="msapplication-wide310x150logo" content="OtherThemes/acorn/img/favicon/mstile-310x150.png" />
    <meta name="msapplication-square310x310logo" content="OtherThemes/acorn/img/favicon/mstile-310x310.png" />
    <!-- Favicon Tags End -->
    <!-- Font Tags Start -->
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@300;400;600&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="OtherThemes/acorn/font/OtherThemes/acorn/CS-Interface/style.css" />
    <!-- Font Tags End -->
    <!-- Vendor Styles Start -->
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/bootstrap.min.css" />
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/OverlayScrollbars.min.css" />
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/glide.core.min.css" />
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/glide.core.min.css" />
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/introjs.min.css" />
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/select2.min.css" />
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/select2-bootstrap4.min.css" />
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/plyr.css" />
    <link rel="stylesheet" href="OtherThemes/acorn/css/vendor/datatables.min.css" />
    <!-- Vendor Styles End -->
    <!-- Template Base Styles Start -->
    <link rel="stylesheet" href="OtherThemes/acorn/css/styles.css" />
    <!-- Template Base Styles End -->

    <link rel="stylesheet" href="OtherThemes/acorn/css/main.css" />
    <script src="OtherThemes/acorn/js/base/loader.js"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>

    <link href="OtherThemes/acorn/scripts/sweetalert.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div hidden="hidden">
            <asp:HiddenField ID="hlbID" runat="server" />
            <asp:Label ID="lblNamaBranch" runat="server" Text=""></asp:Label>

            <asp:HiddenField ID="hlbAgreementNumberCompanyAgreement" runat="server" />
            <asp:HiddenField ID="hlbAgreementNameCompanyAgreement" runat="server" />
            <asp:HiddenField ID="hlbEffectiveDateCompanyAgreement" runat="server" />
            <asp:HiddenField ID="hlbExpirationDateCompanyAgreement" runat="server" />
            <asp:HiddenField ID="hlbDayToExpiredCompanyAgreement" runat="server" />

            <asp:HiddenField ID="hlbAgreementNumberEmployeeAgreement" runat="server" />
            <asp:HiddenField ID="hlbAgreementNameEmployeeAgreement" runat="server" />
            <asp:HiddenField ID="hlbEffectiveDateEmployeeAgreement" runat="server" />
            <asp:HiddenField ID="hlbExpirationDateEmployeeAgreement" runat="server" />
            <asp:HiddenField ID="hlbDayToExpiredEmployeeAgreement" runat="server" />

            <asp:HiddenField ID="hlbAgreementNumberRetirement" runat="server" />
            <asp:HiddenField ID="hlbAgreementNameRetirement" runat="server" />
            <asp:HiddenField ID="hlbEffectiveDateRetirement" runat="server" />
            <asp:HiddenField ID="hlbExpirationDateRetirement" runat="server" />
            <asp:HiddenField ID="hlbDayToExpiredRetirement" runat="server" />


            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ViewerEmailReminderCompanyAgreement" runat="server" Width="100%" Height="100%">
            </rsweb:ReportViewer>
            <rsweb:ReportViewer ID="ViewerEmailReminderEmployeeAgreement" runat="server" Width="100%" Height="100%">
            </rsweb:ReportViewer>
            <rsweb:ReportViewer ID="ViewerEmailReminderEmployeeWillRetired" runat="server" Width="100%" Height="100%">
            </rsweb:ReportViewer>
        </div>

        <div id="root">
            <div id="nav" class="nav-container d-flex">
                <div class="nav-content d-flex">
                    <!-- Logo Start -->
                    <div class="logo position-relative">
                        <a href="#">
                            <!-- Logo can be added directly -->
                            <h1 class="mb-0 pb-0 display-4" id="title" style="color: #ffffff;">HRIS - Email Reminder</h1>
                        </a>
                    </div>
                </div>
                <div class="nav-shadow"></div>
            </div>
        </div>
        <main>
            <asp:Button runat="server" Visible="true" ID="btnSubmit" CssClass="btn buttonColor" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
            <asp:Button runat="server" Visible="true" ID="btnClose" CssClass="btn buttonColor" Text="Close" OnClick="btnClose_Click"></asp:Button>
            <asp:Label ID="lbDayToExpired" runat="server"></asp:Label>
            <div class="table-responsive" hidden="hidden">
                <asp:GridView ID="TableCompanyAgreement30DayExpired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableCompanyAgreement30DayExpired_RowDataBound" OnRowCommand="TableCompanyAgreement30DayExpired_RowCommand" OnSelectedIndexChanged="TableCompanyAgreement30DayExpired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                        <asp:BoundField DataField="agreement_name" HeaderText="Agreement Name" />
                        <asp:BoundField DataField="Institution" HeaderText="Institution" />
                        <asp:BoundField DataField="institution_services" HeaderText="Institution Services" />
                        <asp:BoundField DataField="nik_pic" HeaderText="PIC NIK" />
                        <asp:BoundField DataField="PIC" HeaderText="PIC Name" />
                        <asp:BoundField DataField="effective_date" HeaderText="Effective Date" />
                        <asp:BoundField DataField="expiration_date" HeaderText="Expiration Date" />
                        <asp:BoundField DataField="DayToExpired" HeaderText="Day To Expired" />
                        <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                        <asp:BoundField DataField="notification_to_email" HeaderText="Start Notification Email" />
                        <asp:BoundField DataField="remaks" HeaderText="Remaks" />
                        <asp:BoundField DataField="location" HeaderText="Location" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="createby" HeaderText="Create by" />
                        <asp:BoundField DataField="create_date" HeaderText="Create Date" />
                        <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                        <asp:BoundField DataField="modified_date" HeaderText="Modified Date" />
                        <asp:BoundField DataField="email" HeaderText="Email 1" />
                        <asp:BoundField DataField="email2" HeaderText="Email 2" />
                        <asp:BoundField DataField="email3" HeaderText="Email 3" />
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="TableCompanyAgreement60DayExpired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableCompanyAgreement60DayExpired_RowDataBound" OnRowCommand="TableCompanyAgreement60DayExpired_RowCommand" OnSelectedIndexChanged="TableCompanyAgreement60DayExpired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                        <asp:BoundField DataField="agreement_name" HeaderText="Agreement Name" />
                        <asp:BoundField DataField="Institution" HeaderText="Institution" />
                        <asp:BoundField DataField="institution_services" HeaderText="Institution Services" />
                        <asp:BoundField DataField="nik_pic" HeaderText="PIC NIK" />
                        <asp:BoundField DataField="PIC" HeaderText="PIC Name" />
                        <asp:BoundField DataField="effective_date" HeaderText="Effective Date" />
                        <asp:BoundField DataField="expiration_date" HeaderText="Expiration Date" />
                        <asp:BoundField DataField="DayToExpired" HeaderText="Day To Expired" />
                        <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                        <asp:BoundField DataField="notification_to_email" HeaderText="Start Notification Email" />
                        <asp:BoundField DataField="remaks" HeaderText="Remaks" />
                        <asp:BoundField DataField="location" HeaderText="Location" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="createby" HeaderText="Create by" />
                        <asp:BoundField DataField="create_date" HeaderText="Create Date" />
                        <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                        <asp:BoundField DataField="modified_date" HeaderText="Modified Date" />
                        <asp:BoundField DataField="email" HeaderText="Email 1" />
                        <asp:BoundField DataField="email2" HeaderText="Email 2" />
                        <asp:BoundField DataField="email3" HeaderText="Email 3" />
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="TableCompanyAgreement90DayExpired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableCompanyAgreement90DayExpired_RowDataBound" OnRowCommand="TableCompanyAgreement90DayExpired_RowCommand" OnSelectedIndexChanged="TableCompanyAgreement90DayExpired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                        <asp:BoundField DataField="agreement_name" HeaderText="Agreement Name" />
                        <asp:BoundField DataField="Institution" HeaderText="Institution" />
                        <asp:BoundField DataField="institution_services" HeaderText="Institution Services" />
                        <asp:BoundField DataField="nik_pic" HeaderText="PIC NIK" />
                        <asp:BoundField DataField="PIC" HeaderText="PIC Name" />
                        <asp:BoundField DataField="effective_date" HeaderText="Effective Date" />
                        <asp:BoundField DataField="expiration_date" HeaderText="Expiration Date" />
                        <asp:BoundField DataField="DayToExpired" HeaderText="Day To Expired" />
                        <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                        <asp:BoundField DataField="notification_to_email" HeaderText="Start Notification Email" />
                        <asp:BoundField DataField="remaks" HeaderText="Remaks" />
                        <asp:BoundField DataField="location" HeaderText="Location" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="createby" HeaderText="Create by" />
                        <asp:BoundField DataField="create_date" HeaderText="Create Date" />
                        <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                        <asp:BoundField DataField="modified_date" HeaderText="Modified Date" />
                        <asp:BoundField DataField="email" HeaderText="Email 1" />
                        <asp:BoundField DataField="email2" HeaderText="Email 2" />
                        <asp:BoundField DataField="email3" HeaderText="Email 3" />
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="TableEmployeeAgreement30DayExpired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableEmployeeAgreement30DayExpired_RowDataBound" OnRowCommand="TableEmployeeAgreement30DayExpired_RowCommand" OnSelectedIndexChanged="TableEmployeeAgreement30DayExpired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                        <asp:BoundField DataField="nik_employee" HeaderText="nik_employee" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="JoinDate" HeaderText="Join Date" />
                        <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />
                        <asp:BoundField DataField="WorkingStatus" HeaderText="Working Status" />
                        <asp:BoundField DataField="nik_pic" HeaderText="nik_pic" />
                        <asp:BoundField DataField="PIC" HeaderText="PIC Name" />
                        <asp:BoundField DataField="end_of_agreement_date" HeaderText="End of Agreement Date" />
                        <asp:BoundField DataField="description" HeaderText="Description" />
                        <asp:BoundField DataField="DayToEndOfAgreement" HeaderText="Day to end of agreement" />
                        <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                        <asp:BoundField DataField="notification_to_email" HeaderText="Start Notification Email" />
                        <asp:BoundField DataField="email" HeaderText="Email Address" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="createby" HeaderText="Create by" />
                        <asp:BoundField DataField="create_date" HeaderText="Create Date" />
                        <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                        <asp:BoundField DataField="modified_date" HeaderText="Modified Date" />
                        <asp:BoundField DataField="email2" HeaderText="Email 2" />
                        <asp:BoundField DataField="email3" HeaderText="Email 3" />
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="TableEmployeeAgreement60DayExpired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableEmployeeAgreement60DayExpired_RowDataBound" OnRowCommand="TableEmployeeAgreement60DayExpired_RowCommand" OnSelectedIndexChanged="TableEmployeeAgreement60DayExpired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                        <asp:BoundField DataField="nik_employee" HeaderText="nik_employee" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="JoinDate" HeaderText="Join Date" />
                        <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />
                        <asp:BoundField DataField="WorkingStatus" HeaderText="Working Status" />
                        <asp:BoundField DataField="nik_pic" HeaderText="nik_pic" />
                        <asp:BoundField DataField="PIC" HeaderText="PIC Name" />
                        <asp:BoundField DataField="end_of_agreement_date" HeaderText="End of Agreement Date" />
                        <asp:BoundField DataField="description" HeaderText="Description" />
                        <asp:BoundField DataField="DayToEndOfAgreement" HeaderText="Day to end of agreement" />
                        <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                        <asp:BoundField DataField="notification_to_email" HeaderText="Start Notification Email" />
                        <asp:BoundField DataField="email" HeaderText="Email Address" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="createby" HeaderText="Create by" />
                        <asp:BoundField DataField="create_date" HeaderText="Create Date" />
                        <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                        <asp:BoundField DataField="modified_date" HeaderText="Modified Date" />
                        <asp:BoundField DataField="email2" HeaderText="Email 2" />
                        <asp:BoundField DataField="email3" HeaderText="Email 3" />
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="TableEmployeeAgreement90DayExpired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableEmployeeAgreement90DayExpired_RowDataBound" OnRowCommand="TableEmployeeAgreement90DayExpired_RowCommand" OnSelectedIndexChanged="TableEmployeeAgreement90DayExpired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                        <asp:BoundField DataField="nik_employee" HeaderText="nik_employee" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="JoinDate" HeaderText="Join Date" />
                        <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />
                        <asp:BoundField DataField="WorkingStatus" HeaderText="Working Status" />
                        <asp:BoundField DataField="nik_pic" HeaderText="nik_pic" />
                        <asp:BoundField DataField="PIC" HeaderText="PIC Name" />
                        <asp:BoundField DataField="end_of_agreement_date" HeaderText="End of Agreement Date" />
                        <asp:BoundField DataField="description" HeaderText="Description" />
                        <asp:BoundField DataField="DayToEndOfAgreement" HeaderText="Day to end of agreement" />
                        <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                        <asp:BoundField DataField="notification_to_email" HeaderText="Start Notification Email" />
                        <asp:BoundField DataField="email" HeaderText="Email Address" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="createby" HeaderText="Create by" />
                        <asp:BoundField DataField="create_date" HeaderText="Create Date" />
                        <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                        <asp:BoundField DataField="modified_date" HeaderText="Modified Date" />
                        <asp:BoundField DataField="email2" HeaderText="Email 2" />
                        <asp:BoundField DataField="email3" HeaderText="Email 3" />
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="TableEmployeeAgreement120DayExpired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableEmployeeAgreement120DayExpired_RowDataBound" OnRowCommand="TableEmployeeAgreement120DayExpired_RowCommand" OnSelectedIndexChanged="TableEmployeeAgreement120DayExpired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                        <asp:BoundField DataField="nik_employee" HeaderText="nik_employee" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="JoinDate" HeaderText="Join Date" />
                        <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />
                        <asp:BoundField DataField="WorkingStatus" HeaderText="Working Status" />
                        <asp:BoundField DataField="nik_pic" HeaderText="nik_pic" />
                        <asp:BoundField DataField="PIC" HeaderText="PIC Name" />
                        <asp:BoundField DataField="end_of_agreement_date" HeaderText="End of Agreement Date" />
                        <asp:BoundField DataField="description" HeaderText="Description" />
                        <asp:BoundField DataField="DayToEndOfAgreement" HeaderText="Day to end of agreement" />
                        <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                        <asp:BoundField DataField="notification_to_email" HeaderText="Start Notification Email" />
                        <asp:BoundField DataField="email" HeaderText="Email Address" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="createby" HeaderText="Create by" />
                        <asp:BoundField DataField="create_date" HeaderText="Create Date" />
                        <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                        <asp:BoundField DataField="modified_date" HeaderText="Modified Date" />
                        <asp:BoundField DataField="email2" HeaderText="Email 2" />
                        <asp:BoundField DataField="email3" HeaderText="Email 3" />
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="TableEmployeeAgreement180DayExpired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableEmployeeAgreement180DayExpired_RowDataBound" OnRowCommand="TableEmployeeAgreement180DayExpired_RowCommand" OnSelectedIndexChanged="TableEmployeeAgreement180DayExpired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="agreement_number" HeaderText="Agreement Number" />
                        <asp:BoundField DataField="nik_employee" HeaderText="nik_employee" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="JoinDate" HeaderText="Join Date" />
                        <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />
                        <asp:BoundField DataField="WorkingStatus" HeaderText="Working Status" />
                        <asp:BoundField DataField="nik_pic" HeaderText="nik_pic" />
                        <asp:BoundField DataField="PIC" HeaderText="PIC Name" />
                        <asp:BoundField DataField="end_of_agreement_date" HeaderText="End of Agreement Date" />
                        <asp:BoundField DataField="description" HeaderText="Description" />
                        <asp:BoundField DataField="DayToEndOfAgreement" HeaderText="Day to end of agreement" />
                        <asp:BoundField DataField="reminder" HeaderText="Reminder" />
                        <asp:BoundField DataField="notification_to_email" HeaderText="Start Notification Email" />
                        <asp:BoundField DataField="email" HeaderText="Email Address" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                        <asp:BoundField DataField="createby" HeaderText="Create by" />
                        <asp:BoundField DataField="create_date" HeaderText="Create Date" />
                        <asp:BoundField DataField="modifiedby" HeaderText="Modified by" />
                        <asp:BoundField DataField="modified_date" HeaderText="Modified Date" />
                        <asp:BoundField DataField="email2" HeaderText="Email 2" />
                        <asp:BoundField DataField="email3" HeaderText="Email 3" />
                    </Columns>
                </asp:GridView>

                <asp:GridView ID="TableEmployeeWillRetired" runat="server" CssClass="table table-striped table-bordered zero-configuration" AutoGenerateColumns="False" Style="width: 100%"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowDataBound="TableEmployeeWillRetired_RowDataBound" OnRowCommand="TableEmployeeWillRetired_RowCommand" OnSelectedIndexChanged="TableEmployeeWillRetired_SelectedIndexChanged">
                    <HeaderStyle BackColor="#06183d" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="id_ktp" HeaderText="ID KTP" />
                        <asp:BoundField DataField="fullname" HeaderText="FULLNAME" />
                        <asp:BoundField DataField="nik" HeaderText="NIK" />
                        <asp:BoundField DataField="Position" HeaderText="POSITION" />
                        <asp:BoundField DataField="Division" HeaderText="DIVISION" />
                        <asp:BoundField DataField="Section" HeaderText="SECTION" />
                        <asp:BoundField DataField="SubSection" HeaderText="SUB SECTION" />
                        <asp:BoundField DataField="report_position" HeaderText="REPORT TO POSITION" />
                        <asp:BoundField DataField="code_location" HeaderText="LOCATION CODE" />
                        <asp:BoundField DataField="status" HeaderText="STATUS" />
                        <asp:BoundField DataField="category" HeaderText="CATEGORY" />
                        <asp:BoundField DataField="join_date" HeaderText="JOIN DATE" />
                        <asp:BoundField DataField="date_of_birth" HeaderText="Date of Birth" />
                        <asp:BoundField DataField="Retired Date" HeaderText="Retired Date" />
                        <asp:BoundField DataField="WORK PERIOD" HeaderText="WORK PERIOD" />
                        <asp:BoundField DataField="createby" HeaderText="CREATE BY" />
                        <asp:BoundField DataField="create_date" HeaderText="CREATE DATE" />
                        <asp:BoundField DataField="modifiedby" HeaderText="MODIFIED BY" />
                        <asp:BoundField DataField="modified_date" HeaderText="MODIFIED DATE" />
                        <asp:BoundField DataField="id_position" HeaderText="id_position" />
                        <asp:BoundField DataField="id_division" HeaderText="id_division" />
                        <asp:BoundField DataField="id_section" HeaderText="id_section" />
                        <asp:BoundField DataField="id_sub_section" HeaderText="id_sub_section" />
                    </Columns>
                </asp:GridView>

                <%--<asp:GridView ID="TableWillExpired" runat="server" CssClass="table table-striped row-border order-column table-bordered zero-configuration text-nowrap grid" AutoGenerateColumns="False" Style="width: 100%"
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
                </asp:GridView>--%>
            </div>
        </main>
    </form>

    <!-- Vendor Scripts Start -->

    <script src="OtherThemes/acorn/js/vendor/jquery-3.5.1.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/bootstrap.bundle.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/OverlayScrollbars.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/autoComplete.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/clamp.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/Chart.bundle.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/chartjs-plugin-datalabels.js"></script>
    <script src="OtherThemes/acorn/js/vendor/chartjs-plugin-rounded-bar.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/glide.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/intro.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/select2.full.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/plyr.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/bootstrap-submenu.js"></script>
    <script src="OtherThemes/acorn/js/vendor/datatables.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/mousetrap.min.js"></script>
    <script src="OtherThemes/acorn/js/vendor/jquery.barrating.min.js"></script>
    <!-- Vendor Scripts End -->

    <!-- Template Base Scripts Start -->
    <script src="OtherThemes/acorn/font/CS-Line/csicons.min.js"></script>
    <script src="OtherThemes/acorn/js/base/helpers.js"></script>
    <script src="OtherThemes/acorn/js/base/globals.js"></script>
    <script src="OtherThemes/acorn/js/base/nav.js"></script>
    <script src="OtherThemes/acorn/js/base/search.js"></script>
    <script src="OtherThemes/acorn/js/base/settings.js"></script>
    <script src="OtherThemes/acorn/js/base/init.js"></script>
    <!-- Template Base Scripts End -->
    <!-- Page Specific Scripts Start -->
    <script src="OtherThemes/acorn/js/cs/glide.custom.js"></script>
    <script src="OtherThemes/acorn/js/cs/charts.extend.js"></script>
    <script src="OtherThemes/acorn/js/pages/dashboard.analytic.js"></script>
    <script src="OtherThemes/acorn/js/common.js"></script>
    <script src="OtherThemes/acorn/js/scripts.js"></script>
    <script src="OtherThemes/acorn/js/cs/datatable.extend.js"></script>
    <script src="OtherThemes/acorn/js/plugins/datatable.editableboxed.js"></script>
    <script src="OtherThemes/acorn/js/plugins/datatable.boxedvariations.js"></script>
    <script src="OtherThemes/acorn/js/pages/dashboard.visual.js"></script>
    <script src="OtherThemes/acorn/js/plugins/datatable.serverside.js"></script>
    <script type="text/javascript" src="vendors/date/JS/jquery-1.10.2.min.js"></script>
    <!-- Page Specific Scripts End -->

    <link href="OtherThemes/acorn/scripts/tables/css/datatable/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="OtherThemes/acorn/scripts/tables/js/jquery.dataTables.min.js"></script>
    <script src="OtherThemes/acorn/scripts/tables/js/datatable/dataTables.bootstrap4.min.js"></script>
    <script src="OtherThemes/acorn/scripts/tables/js/datatable-init/datatable-basic.min.js"></script>
    <script src="OtherThemes/acorn/scripts/sweetalert.js"></script>
    <script src="OtherThemes/acorn/scripts/sweetalert.min.js"></script>
</body>
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
<script>
    window.onload = function () {
        $("#btnSubmit").click();
    }
</script>
<script>
    function CloseWindow() {
        window.close();
    }
</script>
</html>
