<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sidebar.ascx.cs" Inherits="HRManagementSystem.Admin.dashboard.Sidebar" %>

<div class="vertical-menu">
    <div data-simplebar class="h-100">
        <div id="sidebar-menu">
            <ul class="metismenu list-unstyled" id="side-menu">
                <li class="menu-title">Menu</li>
                <li>
                    <a href="javascript: void(0);" class="waves-effect">
                        <i class="bx bx-home-circle"></i>
                        <span class="badge rounded-pill bg-info float-end">04</span>
                        <span>Dashboards</span>
                    </a>
                    <ul class="sub-menu" aria-expanded="false">
                        <li><a href="Dashboard.aspx">Default</a></li>
                    </ul>
                </li>
                <li class="menu-title">Components</li>
                <li>
                    <a href="javascript: void(0);" class="has-arrow waves-effect">
                        <i class="bx bx-layout"></i>
                        <span>Managements Records</span>
                    </a>
                    <ul class="sub-menu" aria-expanded="true">
                        <li>
                            <a href="javascript: void(0);" class="has-arrow">Employee Directory</a>
                            <ul class="sub-menu" aria-expanded="true">
                                <li>
                                    <a href="EmployeeList.aspx">
                                        <span class="badge rounded-pill badge-soft-success float-end">New</span>
                                        <span>Employee Table</span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <a href="javascript: void(0);" class="has-arrow">Employee Attendances</a>
                            <ul class="sub-menu" aria-expanded="true">
                                <li><a href="AttendenceList.aspx">Attendances List</a></li>
                            </ul>
                        </li>
                        <li>
                            <a href="javascript: void(0);" class="has-arrow">EmployeeLeaveRecord</a>
                            <ul class="sub-menu" aria-expanded="true">
                                <li><a href="Leaverecordelist.aspx">Leave Records List</a></li>
                            </ul>
                        </li>
                        <li>
                            <a href="javascript: void(0);" class="has-arrow">Department Directory</a>
                            <ul class="sub-menu" aria-expanded="true">
                                <li><a href="Departmentlist.aspx">Department Table</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li class="menu-title">Report</li>
                <!-- Add List Report Center here -->
            </ul>
        </div>
    </div>
</div>
