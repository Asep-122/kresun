<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aplikasi Koperasi</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="Aplikasi Koperasi" name="description" />
    <%--<meta content="Coderthemes" name="author" />--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  
   
<!--===============================================================================================-->	
	<link rel="icon" type="Login_v3/image/png" href="Login_v3/images/icons/koperasi.ico"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v3/vendor/bootstrap/css/bootstrap.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v3/fonts/font-awesome-4.7.0/css/font-awesome.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v3/fonts/iconic/css/material-design-iconic-font.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v3/vendor/animate/animate.css"/>
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="Login_v3/vendor/css-hamburgers/hamburgers.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v3/vendor/animsition/css/animsition.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v3/vendor/select2/select2.min.css"/>
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="Login_v3/vendor/daterangepicker/daterangepicker.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v3/css/util.css"/>
	<link rel="stylesheet" type="text/css" href="Login_v3/css/main.css"/>
<!--===============================================================================================-->
      <!-- Sweet Alert css -->

	    <link href="../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <%--<script src="Javascript/jquery-1.12.0.min.js"></script>--%>

<%--    <script type="text/javascript">
        jQuery(document).ready(function () {
            if (window != window.top) {
                window.top.out();
            }
        });

    </script>--%>

    <script type="text/javascript">
         function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

        };
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            margin: 0 auto;
            height: 709px;
        }
    </style>
</head>
<body>
	
	<div class="limiter">
		<div class="container-login100" style="background-image: url('Login_v3/images/bg-01.jpg');">
			<div class="wrap-login100 p-t-190 p-b-30" >
				<form class="login100-form validate-form" runat="server">
				
                
                    
			<%--		<span class="login100-form-title p-b-20 p-t-20">
						Log in SYSTEM ERP
					</span>--%>

                    	<div class="wrap-input100 validate-input" data-validate = "Enter username">
						<asp:TextBox ID="txtUserName" type="text" CssClass="input100" placeholder="Username" runat="server"></asp:TextBox>
					   <span class="focus-input100" data-placeholder="&#xf207;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="Enter password">
						<asp:TextBox ID="txtPassword" CssClass="input100" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>						
						<span class="focus-input100" data-placeholder="&#xf191;"></span>
					</div>

				

					<div class="container-login100-form-btn">
					<asp:Button ID="Button1" CssClass="login100-form-btn" runat="server" Text="Login" OnClick="btnSubmit_Click" />
						
					</div>

 

			
				</form>
			</div>
		</div>
	</div>
	
	
 

</body>

     <!--===============================================================================================-->	
	<script src="Login_v3/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="Login_v3/vendor/bootstrap/js/popper.js"></script>
	<script src="Login_v3/vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="Login_v3/vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="Login_v3/js/main.js"></script>
        <!-- Sweet Alert Js  -->
    <script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>
</html>