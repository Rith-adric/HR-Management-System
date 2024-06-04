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
    public partial class UpdateAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["AttendanceID"] != null)
                {
                    int attendanceId = Convert.ToInt32(Request.QueryString["AttendanceID"]);
                    ShowManagerIDDropdown(attendanceId);
                    ShowEmployeeList();
                }
            }
        }

        private void ShowManagerIDDropdown(int attendanceId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT A.AttendanceID, A.EmployeeID, E.FirstName, E.LastName, A.AttendanceDate, A.StartTime, A.EndTime, A.Status " +
                                "FROM Attendance A " +
                                "INNER JOIN Employees E ON A.EmployeeID = E.EmployeeID " +
                                "WHERE A.AttendanceID = @AttendanceID", conn);

                cmd.Parameters.AddWithValue("@AttendanceID", attendanceId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    AttendanceID.Text = $"{attendanceId}";
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string employeeName = $"{firstName} {lastName}";
                    EmployeeNameDropDown.Items.Clear();
                    EmployeeNameDropDown.Items.Add(new ListItem(employeeName, attendanceId.ToString()));
                    AttendanceDate.Text = Convert.ToDateTime(reader["AttendanceDate"]).ToString("yyyy-MM-dd");
                    StartTime.Text = reader["StartTime"].ToString();
                    EndTime.Text = reader["EndTime"].ToString();
                    SelectStatus.SelectedValue = reader["Status"].ToString();
                }

                reader.Close();
            }
        }

        private void ShowEmployeeList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT EmployeeID, FirstName, LastName FROM Employees";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int employeeId = Convert.ToInt32(reader["EmployeeID"]);
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string employeeName = $"{firstName} {lastName}";
                    EmployeeNameDropDown.Items.Add(new ListItem(employeeName, employeeId.ToString()));
                }

                reader.Close();
                conn.Close();
            }
        }

        protected void UpdateAttendance_Click(object sender, EventArgs e)
        {
            int attendanceId = Convert.ToInt32(Request.QueryString["AttendanceID"]);
            int employeeId = Convert.ToInt32(EmployeeNameDropDown.SelectedValue);
            string attendanceDate = AttendanceDate.Text;
            string startTime = StartTime.Text;
            string endTime = EndTime.Text;
            string status = SelectStatus.SelectedValue;

            // Update query to update the attendance record
            string updateQuery = "UPDATE Attendance SET EmployeeID = @EmployeeID, AttendanceDate = @AttendanceDate, StartTime = @StartTime, EndTime = @EndTime, Status = @Status WHERE AttendanceID = @AttendanceID";

            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                cmd.Parameters.AddWithValue("@StartTime", startTime);
                cmd.Parameters.AddWithValue("@EndTime", endTime);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAffected > 0)
                {
                    ltlMessage.Text = "<div class='alert alert-success'><b>Employee Attendance updated successfully!</b> <b>Redirecting</b> to employee Attendance table...<i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i></div>";
                    Response.Write("<script>window.setTimeout(function(){ window.location.href = 'AttendenceList.aspx'; }, 4400);</script>");
                }
                else
                {
                    ltlMessage.Text = "<div class='alert alert-danger'><b>Failed</b> to update employee attendance.</div>";
                }
            }
        }


        protected void BackAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("AttendenceList.aspx");
        }
    }
}