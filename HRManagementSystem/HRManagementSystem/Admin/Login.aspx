<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HRManagementSystem.Admin.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Human Resource Management System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta content="Premium Multipurpose Admin & Dashboard Template" name="description" />
    <meta content="Themesbrand" name="author" />
    <!-- App favicon -->
    <link rel="shortcut icon" href="assets/images/favicon.ico" />
    <!-- Bootstrap Css -->
    <link href="assets/css/bootstrap.min.css"  rel="stylesheet" type="text/css" />
    <!-- Icons Css -->
    <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <!-- App Css-->
    <link href="assets/css/app.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <button type="button" class="btn header-item noti-icon waves-effect" data-bs-toggle="fullscreen" style="margin-left: 95%;">
                <i class="bx bx-fullscreen"></i><br />
                    Screen
        </button>
        <div class="account-pages my-5 pt-sm-5">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-8 col-lg-6 col-xl-5">
                        <div class="card overflow-hidden">
                            <div class="bg-primary bg-soft">
                                <img src="assets/images/profile-img.png" alt="" class="img-fluid" />
                            </div>
                            <div class="card-body pt-0">
                                <div class="auth-logo">
                                    <a href="#" class="auth-logo-light">
                                        <div class="avatar-md profile-user-wid mb-4">
                                            <span class="avatar-title rounded-circle bg-light">
                                                <img src="assets/images/logo-light.svg" alt="" class="rounded-circle" height="34">
                                            </span>
                                        </div>
                                    </a>

                                    <a href="#" class="auth-logo-dark">
                                        <div class="avatar-md profile-user-wid mb-4">
                                            <span class="avatar-title rounded-circle bg-light">
                                                <img src="assets/images/logo.svg" alt="" class="rounded-circle" height="34">
                                            </span>
                                        </div>
                                    </a>
                                </div>
                                <div class="p-2">
                                    <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                                    <div class="mb-3">
                                        <asp:Label ID="lblUsername" runat="server" Text="Username" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
                                    </div>

                                    <div class="mb-3">
                                        <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="form-label"></asp:Label>
                                        <div class="input-group auth-pass-inputgroup">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
                                            <button id="btnTogglePassword" class="btn btn-light" type="button" onclick="togglePasswordVisibility()">
                                                <i id="eyeIcon" class="mdi mdi-eye-outline"></i>
                                            </button>

                                        </div>
                                    </div>

                                    <div class="form-check">
                                        <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" />
                                        <asp:Label ID="lblRememberMe" runat="server" AssociatedControlID="chkRememberMe" CssClass="form-check-label" Text="Remember me"></asp:Label>
                                    </div>

                                    <div class="mt-3 d-grid">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary waves-effect waves-light" Text="Log In" OnClick="btnLogin_Click" />
                                    </div>

                                    <div class="mt-4 text-center">
                                        <h5 class="font-size-14 mb-3">Sign in with</h5>
                                        <ul class="list-inline">
                                            <li class="list-inline-item">
                                                <a href="javascript:void(0)" class="social-list-item bg-primary text-white border-primary">
                                                    <i class="mdi mdi-facebook"></i>
                                                </a>
                                            </li>
                                            <li class="list-inline-item">
                                                <a href="javascript:void(0)" class="social-list-item bg-info text-white border-info">
                                                    <i class="mdi mdi-twitter"></i>
                                                </a>
                                            </li>
                                            <li class="list-inline-item">
                                                <a href="javascript:void(0)" class="social-list-item bg-danger text-white border-danger">
                                                    <i class="mdi mdi-google"></i>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="mt-4 text-center">
                                        <a href="#" class="text-muted"><i class="mdi mdi-lock me-1"></i> Forgot your password?</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- JAVASCRIPT -->
    <script src="assets/libs/jquery/jquery.min.js"></script>
    <script src="assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/libs/metismenu/metisMenu.min.js"></script>
    <script src="assets/libs/simplebar/simplebar.min.js"></script>
    <script src="assets/libs/node-waves/waves.min.js"></script>
    <!-- App js -->
    <script src="assets/js/app.js"></script>
    <script>
        function togglePasswordVisibility() {
            var passwordInput = document.getElementById('txtPassword');
            var eyeIcon = document.getElementById('eyeIcon');

            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                eyeIcon.classList.remove('mdi-eye-outline');
                eyeIcon.classList.add('mdi-eye-off-outline');
            } else {
                passwordInput.type = "password";
                eyeIcon.classList.remove('mdi-eye-off-outline');
                eyeIcon.classList.add('mdi-eye-outline');
            }
        }
    </script>


</body>
</html>
