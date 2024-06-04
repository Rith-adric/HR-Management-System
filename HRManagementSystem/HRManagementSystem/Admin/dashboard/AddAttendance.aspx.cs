using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRManagementSystem.Admin.dashboard
{
    public partial class AddAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowManagerIDDropdown();
            }
        }

        private void ShowManagerIDDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT EmployeeID, FirstName, LastName FROM Employees", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                manager.Items.Clear();
                manager.Items.Add(new ListItem("Select Manager", ""));

                while (reader.Read())
                {
                    int employeeId = Convert.ToInt32(reader["EmployeeID"]);
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string employeeName = $"{firstName} {lastName}";
                    manager.Items.Add(new ListItem(employeeName, employeeId.ToString()));
                }

                reader.Close();
                conn.Close();
            }
        }

        protected void AddAttendance_Click(object sender, EventArgs e)
        {
            string selectedManagerId = manager.SelectedValue;
            if (IsValidInput())
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Attendance (EmployeeID, AttendanceDate, StartTime, EndTime, Status) " +
                                   "VALUES (@EmployeeID, @AttendanceDate, @StartTime, @EndTime, @Status)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", selectedManagerId);
                    command.Parameters.AddWithValue("@AttendanceDate", AttendanceDate.Text.Trim());
                    command.Parameters.AddWithValue("@StartTime", StartTime.Text.Trim());
                    command.Parameters.AddWithValue("@EndTime", EndTime.Text.Trim());
                    command.Parameters.AddWithValue("@Status", SelectStatus.Text.Trim());

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        ltlMessage.Text = "<div class='alert alert-success'><b>Employee Attendance inserted successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Redirecting</b> to employee attendance table...</div>";
                        Response.Write("<script>window.setTimeout(function(){ window.location.href = 'AttendenceList.aspx'; }, 4000);</script>");
                    }
                    catch (Exception ex)
                    {
                        ltlMessage.Text = $"<div class='alert alert-danger'>Error: {ex.Message}</div>";
                    }
                }
            }
        }


        protected void BackAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttendenceList.aspx");
        }

        // Additional method to validate input fields for attendance
        private bool IsValidInput()
        {
            bool isValid = true;
            if (
                string.IsNullOrEmpty(AttendanceDate.Text.Trim()) &&
                string.IsNullOrEmpty(StartTime.Text.Trim()) &&
                string.IsNullOrEmpty(EndTime.Text.Trim()) &&
                string.IsNullOrEmpty(SelectStatus.Text.Trim()))
            {
                ltlMessage.Text = "<div class='alert alert-danger'><b>Fields</b> are empty. Please enter data.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(AttendanceDate.Text.Trim()))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>Attendance Date</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(StartTime.Text.Trim()))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>Start Time</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(EndTime.Text.Trim()))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>End Time</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(SelectStatus.Text.Trim()))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please select a <b>Status</b>.</div>";
                isValid = false;
            }
            else
            {
                // Additional validation logic can be added here
            }
            return isValid;
        }


    }
}