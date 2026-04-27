<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="hris_ylid.login" %>

<!DOCTYPE html>

<html lang="en">
  <head>
	<meta charset="UTF-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<script src="https://kit.fontawesome.com/64d58efce2.js" crossorigin="anonymous"></script>
	<link href="OtherThemes/login/style-boxlogin.css" rel="stylesheet" />
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.css" />
	<script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>
	<link href="vendors/sweetalert.css" rel="stylesheet" />
	<link rel="icon" href="images/favicon.ico" type="image/x-icon">

	  <script type="text/javascript">
		  function SuccessLogin() {
			  swal({
				  title: 'Login Success',
				  text: '<span style=color:grey>WELCOME <label id=txtFullname runat="server"></label></span>',
				  timer: '2000',
				  type: 'success',
				  showConfirmButton: false,
				  html: true
			  },
				  function redirect() {
					  window.location.href = 'index.aspx';
				  }
			  );
		  }
	  </script>

	<script type="text/javascript">
		function FailedLogin() {
			swal('Login Failed!', 'Username or Password incorrect, please try again', 'error');
		}
	</script>
	
	<script type="text/javascript">
        function InfoBox() {
            swal({
                title: '<strong>Perhatian!</strong>',
                text: '<strong>Welcome to HR Information System</strong>' +
                    '<br/> ' +
                    '<br/> ' +
                    'Login sama dengan login windows pada PC/Laptop, ' +
                    '<br/> ' +
                    '<strong>Contoh -> username:YLID-NIK, password:(sama dengan password login windows pada pc/laptop).</strong>' +
					'<br/> ' +
                    '<br/> ' +
                    'Jika belum bisa login menggunakan login windows,silahkan hubungi Admin System' +
                    '<br/> ' +
                    '<strong>Ext. HRD Team: 221 or 243</strong>',
                type: 'info',
                showConfirmButton: true,
                html: true
            }
            );
        }
    </script>
	
	<title>HRIS - YLID | Login Page</title>

  </head>
	
  <body>
	<div id="container" class="container">
		<form runat="server" defaultbutton="btnlogin">
		<!-- FORM SECTION -->
		<div class="row">
			<!-- SIGN UP -->
			<div class="col align-items-center flex-col sign-up">
				<div class="form-wrapper align-items-center">
					<div class="form sign-up">
						<div class="input-group">
							<i class='bx bxs-user'></i>
							<input type="text" placeholder="Username">
						</div>
						<div class="input-group">
							<i class='bx bx-mail-send'></i>
							<input type="email" placeholder="Email">
						</div>
						<div class="input-group">
							<i class='bx bxs-lock-alt'></i>
							<input type="password" placeholder="Password">
						</div>
						<div class="input-group">
							<i class='bx bxs-lock-alt'></i>
							<input type="password" placeholder="Confirm password">
						</div>
						<button>
							Register
						</button>
						<p>
							<span>
								Already have an account?
							</span>
							<b onclick="toggle()" class="pointer">
								Login here
							</b>
						</p>
					</div>
				</div>
			
			</div>
			<!-- END SIGN UP -->
			<!-- SIGN IN -->
			
			<div class="col align-items-center flex-col sign-in">
				<div class="form-wrapper align-items-center">
					<div class="form sign-in">
						<h2>HR Information System</h2>
						<br />
						<%--<div class="input-group">
							<input id="txtusername" runat="server" type="text" placeholder="Username" required="" />
						</div>--%>
						<div>
							<label class="field field_v1">
							<input class="field__input" id="txtusername" runat="server" type="text" placeholder="type your Username...">
							<span class="field__label-wrap">
							  <span class="field__label"><i class="fa fa-user"></i>&nbsp;&nbsp;Username</span>
							</span>
							</label>
						</div>
						
						<div>
							<label class="field field_v1">
							<input class="field__input" id="txtpassword" runat="server" type="password" placeholder="type your password...">
							<span class="field__label-wrap">
							  <span class="field__label"><i class="fa fa-key"></i>&nbsp;&nbsp;Password</span>
							</span>
							</label>
						</div>
						<div>
							<input type="checkbox" onchange="document.getElementById('txtpassword').type = this.checked ? 'text' : 'password'">
							Show password
						</div>

						<%--<div class="input-group">
							<input id="txtpass" runat="server" type="password" placeholder="Password" required="" />
						</div>--%>
						<p>
							<b>
								<button type="button" class="btn btn-sm btn-success shadow-sm" onclick="<%=btnlogin.ClientID %>.click()">
								<i class="fa fa-sign-in"></i>
								Login
								</button>
								<asp:Button runat="server" Style="display: none;" ID="btnlogin" OnClick="btnlogin_Click"></asp:Button>
							</b>
						</p>

						<div class="clearfix"></div>
				
				<div>
				  <p>©2022 All Rights Reserved. PT. Yusen Logistics Indonesia.</p>
				</div>
					</div>
				</div>
			</div>
				<%--<marquee>
					<i class="fa fa-bullhorn" style="font-size:30px;color:white">&nbsp</i>
					<Label style="font-size:30px;color:#ffffff";">
						FFIS DASHBOARD akan pindah server per tanggal 8 AGUSTUS 2022 !!! 
					</Label>
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<i class="fa fa-bullhorn" style="font-size:30px;color:white">&nbsp</i>
					<Label style="font-size:30px;color:#ffffff";">
						LINK baru dapat diakses melalui INTRANET YLID. Terima Kasih
					</Label>
				</marquee>--%>
			<!-- END SIGN IN -->
		</div>
	  
		<!-- END FORM SECTION -->
			</form>
		<!-- CONTENT SECTION -->
		<div class="row content-row">
			<!-- SIGN IN CONTENT -->
			<div class="col align-items-center flex-col">
				<%--<div class="text sign-in">
					<h2>
						Welcome Back
					</h2>
	
				</div>--%>
				<div class="img sign-in">
					<img src="images/img-log-yusen.png" class="image" alt="" />
				</div>
			</div>
			<!-- END SIGN IN CONTENT -->
			<!-- SIGN UP CONTENT -->
			<div class="col align-items-center flex-col">
				<div class="img sign-up">
				
				</div>
				<div class="text sign-up">
					
	
				</div>

			</div>
			<!-- END SIGN UP CONTENT -->
		</div>
		<!-- END CONTENT SECTION -->
	</div>

	  <script src="OtherThemes/login/style-login.js"></script>
	  <script src="vendors/sweetalert.js"></script>
	  <script src="vendors/sweetalert.min.js"></script>
  </body>
</html>
