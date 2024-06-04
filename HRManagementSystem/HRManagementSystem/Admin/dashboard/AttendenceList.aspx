<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendenceList.aspx.cs" Inherits="HRManagementSystem.Admin.dashboard.AttendenceList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
        <title>HR Management System</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta content="Premium Multipurpose Admin & Dashboard Template" name="description" />
        <meta content="Themesbrand" name="author" />
        <!-- Bootstrap Css -->
        <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- Icons Css -->
        <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css" />
        <!-- App Css-->
        <link href="assets/css/app.min.css" rel="stylesheet" type="text/css" />
</head>
<body data-sidebar="dark">
    <form id="form1" runat="server">
        <div id="layout-wrapper"> 
            <header id="page-topbar">
                <div class="navbar-header">
                    <div class="d-flex">
                        <!-- LOGO -->
                        <div class="">
                            <img src="assets/images/profile-img.png" alt="" width="250" style="margin-top: 1px;" height="68" />
                        </div>

                        <button type="button" class="btn btn-sm px-3 font-size-16 header-item waves-effect" id="vertical-menu-btn">
                            <i class="fa fa-fw fa-bars"></i>
                        </button>

                        <!-- App Search-->
                        <form class="app-search d-none d-lg-block">
                            <div class="position-relative">
                                <input type="text" class="form-control" placeholder="Search..." />
                                <span class="bx bx-search-alt"></span>
                            </div>
                        </form>
                    </div>

                    <div class="d-flex">

                        <div class="dropdown d-inline-block d-lg-none ms-2">
                            <button type="button" class="btn header-item noti-icon waves-effect" id="page-header-search-dropdown"
                            data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="mdi mdi-magnify"></i>
                            </button>
                            <div class="dropdown-menu dropdown-menu-lg dropdown-menu-end p-0"
                                aria-labelledby="page-header-search-dropdown">
        
                                <form class="p-3">
                                    <div class="form-group m-0">
                                        <div class="input-group">
                                            <input type="text" class="form-control" placeholder="Search ..." aria-label="Recipient's username" />
                                            <div class="input-group-append">
                                                <button class="btn btn-primary" type="submit"><i class="mdi mdi-magnify"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>

                        <div class="dropdown d-inline-block">
                            <button type="button" class="btn header-item waves-effect"
                            data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <img id="header-lang-img" src="assets/images/flags/us.jpg" alt="Header Language" height="16" />
                            </button>
                            <div class="dropdown-menu dropdown-menu-end">

                                <!-- item-->
                                <a href="javascript:void(0);" class="dropdown-item notify-item language" data-lang="en">
                                    <img src="assets/images/flags/us.jpg" alt="user-image" class="me-1" height="12" /> <span class="align-middle">English</span>
                                </a>
                                <!-- item-->
                                <a href="javascript:void(0);" class="dropdown-item notify-item language" data-lang="sp">
                                    <img src="assets/images/flags/Khmer.png" alt="user-image" class="me-1" height="12" /> <span class="align-middle">Khmer</span>
                                </a>

                                <!-- item-->
                                <a href="javascript:void(0);" class="dropdown-item notify-item language" data-lang="gr">
                                    <img src="assets/images/flags/China.png" alt="user-image" class="me-1" height="12" /> <span class="align-middle">China</span>
                                </a>
                            </div>
                        </div>

                        <div class="dropdown d-none d-lg-inline-block ms-1">
                            <button type="button" class="btn header-item noti-icon waves-effect"
                            data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <i class="bx bx-customize"></i>
                            </button>
                            <div class="dropdown-menu dropdown-menu-lg dropdown-menu-end">
                                <div class="px-lg-2">
                                    <div class="row g-0">
                                        <div class="col">
                                            <a class="dropdown-icon-item" href="#">
                                                <img src="assets/images/brands/github.png" alt="Github">
                                                <span>GitHub</span>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a class="dropdown-icon-item" href="#">
                                                <img src="assets/images/brands/bitbucket.png" alt="bitbucket" />
                                                <span>Bitbucket</span>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a class="dropdown-icon-item" href="#">
                                                <img src="assets/images/brands/dribbble.png" alt="dribbble" />
                                                <span>Dribbble</span>
                                            </a>
                                        </div>
                                    </div>

                                    <div class="row g-0">
                                        <div class="col">
                                            <a class="dropdown-icon-item" href="#">
                                                <img src="assets/images/brands/dropbox.png" alt="dropbox" />
                                                <span>Dropbox</span>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a class="dropdown-icon-item" href="#">
                                                <img src="assets/images/brands/mail_chimp.png" alt="mail_chimp" />
                                                <span>Mail Chimp</span>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a class="dropdown-icon-item" href="#">
                                                <img src="assets/images/brands/slack.png" alt="slack" />
                                                <span>Slack</span>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="dropdown d-none d-lg-inline-block ms-1">
                            <button type="button" class="btn header-item noti-icon waves-effect" data-bs-toggle="fullscreen">
                                <i class="bx bx-fullscreen"></i>
                            </button>
                        </div>

                        <div class="dropdown d-inline-block">
                            <button type="button" class="btn header-item waves-effect" id="page-header-user-dropdown"
                            data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <img class="rounded-circle header-profile-user" src="assets/images/users/Rith-Adric.jpg"
                                    alt="Header Avatar">
                                <span class="d-none d-xl-inline-block ms-1" key="t-henry">Rith Adric</span>
                                <i class="mdi mdi-chevron-down d-none d-xl-inline-block"></i>
                            </button>
                            <div class="dropdown-menu dropdown-menu-end">
                                <!-- item-->
                                <a class="dropdown-item" href="#"><i class="bx bx-user font-size-16 align-middle me-1"></i> <span key="t-profile">Profile</span></a>
                                <a class="dropdown-item" href="#"><i class="bx bx-wallet font-size-16 align-middle me-1"></i> <span key="t-my-wallet">My Wallet</span></a>
                                <a class="dropdown-item d-block" href="#"><span class="badge bg-success float-end">11</span><i class="bx bx-wrench font-size-16 align-middle me-1"></i> <span key="t-settings">Settings</span></a>
                                <a class="dropdown-item" href="#"><i class="bx bx-lock-open font-size-16 align-middle me-1"></i> <span key="t-lock-screen">Lock screen</span></a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item text-danger" href="../Login.aspx"><i class="bx bx-power-off font-size-16 align-middle me-1 text-danger"></i> <span key="t-logout">Logout</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </header>
            <!-- ========== Left Sidebar Start ========== -->
            <%@ Register Src="Sidebar.ascx" TagPrefix="uc" TagName="Sidebar" %>
            <uc:Sidebar ID="Sidebar1" runat="server" />
            <!-- Left Sidebar End -->

            <!-- ============================================================== -->
            <!-- Start right Content here -->
            <!-- ============================================================== -->
            <div class="main-content">

                <div class="page-content">
                    <div class="container-fluid">

                        <!-- start page title -->
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0 font-size-18">Admin Dashboard</h4>

                                </div>
                            </div>
                        </div>
                        <!-- end row -->
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-body">
                                        <h4 class="card-title mb-4">Employee Attendance Table</h4>
                                        <asp:LinkButton ID="btnAddDepartment" runat="server" CssClass="btn btn-primary waves-effect btn-label waves-light" OnClick="btnAddAttendanceList_Click">
                                            <i class="bx bx-smile label-icon"></i> Add Employee Attendance
                                        </asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                                        <div class="table-responsive">
                                            <asp:GridView ID="AttendanceGridView" runat="server" CssClass="table align-middle table-nowrap mb-0" AutoGenerateColumns="False" DataKeyNames="AttendanceID" OnRowEditing="AttendanceGridView_RowEditing" OnRowDeleting="AttendanceGridView_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="AttendanceID" HeaderText="Attendance ID" ReadOnly="True" />
                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <%# Eval("EmployeeID") %>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="EmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Attendance Date">
                                                        <ItemTemplate>
                                                            <%# Eval("AttendanceDate", "{0:dd MMM, yyyy}") %>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox runat="server" ID="AttendanceDate" CssClass="form-control" placeholder="Enter Attendance Date (YYYY-MM-DD)" TextMode="Date" Text='<%# Bind("AttendanceDate", "0:dd MMM, yyyy") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Time">
                                                        <ItemTemplate>
                                                            <%# Eval("StartTime") %>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="StartTime" runat="server" Text='<%# Bind("StartTime") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Time">
                                                        <ItemTemplate>
                                                            <%# Eval("EndTime") %>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="EndTime" runat="server" Text='<%# Bind("EndTime") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <%# Eval("Status") %>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="Status" runat="server" Text='<%# Bind("Status") %>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <!-- end table-responsive -->
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end row -->
                    </div>
                    <!-- container-fluid -->
                </div>
                <!-- End Page-content -->


                <footer class="footer">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-6">
                                <script>document.write(new Date().getFullYear())</script> © HRMS.
                            </div>
                            <div class="col-sm-6">
                                <div class="text-sm-end d-none d-sm-block">
                                    Design & Develop by Sothearith
                                </div>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
            <!-- end main content-->
        </div>
        <!-- END layout-wrapper -->
        <!-- Right bar overlay-->
        <div class="rightbar-overlay"></div>
    </form>
    <script src="../assets/libs/jquery/jquery.min.js"></script>
    <script src="../assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/libs/metismenu/metisMenu.min.js"></script>
    <script src="../assets/libs/simplebar/simplebar.min.js"></script>
    <script src="../assets/libs/node-waves/waves.min.js"></script>
    <!-- apexcharts -->
    <script src="../assets/libs/apexcharts/apexcharts.min.js"></script>
    <!-- dashboard init -->
    <script src="../assets/js/pages/dashboard.init.js"></script>
    <!-- App js -->
    <script src="../assets/js/app.js"></script>
</body>
</html>
