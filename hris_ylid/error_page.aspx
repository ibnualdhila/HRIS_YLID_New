<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error_page.aspx.cs" Inherits="hris_ylid.error_page" %>

<!DOCTYPE html>
<html class="h-100" lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>HRIS | Error Page </title>
    <!-- Favicon icon -->
    <link rel="icon" href="images/favicon.ico" type="image/x-icon">
   <%-- <link rel="icon" type="image/png" sizes="16x16" href="images/favicon.png">--%>
    <link href="css/style.css" rel="stylesheet">
    
</head>

<body class="h-100">
    
    <!--*******************
        Preloader start
    ********************-->
    <div id="preloader">
        <div class="loader">
            <svg class="circular" viewBox="25 25 50 50">
                <circle class="path" cx="50" cy="50" r="20" fill="none" stroke-width="3" stroke-miterlimit="10" />
            </svg>
        </div>
    </div>
    <!--*******************
        Preloader end
    ********************-->
    

    <div class="login-form-bg h-100">
        <div class="container h-100">
            <div class="row justify-content-center h-100">
                <div class="col-xl-6">
                    <div class="error-content">
                        <div class="card mb-0">
                            <div class="card-body text-center">
                                <h1 class="error-text text-primary"><i class="fa fa-warning text-danger"></i></h1>
                                <h4 class="mt-4">Error Request</h4>
                                <p>Your Request resulted in an error, please login again for continue.</p>
                                <form class="mt-5 mb-5" runat="server">
                                    <asp:HiddenField ID="lblUrl" runat="server" />
                                    <%--<div class="text-center mb-4 mt-4"><a href="index.html" class="btn btn-primary">Go to Login</a>
                                    </div>--%>
                                    <div class="text-center mb-4 mt-4">
                                        <button type="button" class="btn btn-sm btn-success shadow-sm" onclick="<%=btnLogin.ClientID %>.click()">
                                        <i class="fa fa-sign-in"></i>
                                        Go to Login
                                        </button>
                                        <asp:Button runat="server" Style="display: none;" ID="btnLogin" OnClick="btnLogin_Click"></asp:Button>
                                     </div>
                                </form>
                                <div class="text-center">
                                    <p>©2022 All Rights Reserved. PT. Yusen Logistics Indonesia. Privacy and Terms</p>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    

    

    <!--**********************************
        Scripts
    ***********************************-->
    <script src="plugins/common/common.min.js"></script>
    <script src="js/custom.min.js"></script>
    <script src="js/settings.js"></script>
    <script src="js/gleek.js"></script>
    <script src="js/styleSwitcher.js"></script>
</body>
</html>
