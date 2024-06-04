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
    public partial class Updateleaverecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate department details for updating
                if (Request.QueryString["RequestID"] != null)
                {
                    int RequestID = Convert.ToInt32(Request.QueryString["RequestID"]);
                    ShowDepartmentDetails(RequestID);
                    ShowEmployeeIDDropdown();
                }
            }
        }

        private void ShowEmployeeIDDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT EmployeeID, FirstName, LastName FROM Employees", conn);
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

        private void ShowDepartmentDetails(int RequestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT e.FirstName, e.LastName, lr.RequestID, lr.LeaveStartDate, 
	                lr.LeaveEndDate, lr.LeaveType, lr.Status
                FROM LeaveRecords lr
                    INNER JOIN Employees e ON lr.EmployeeID = e.EmployeeID
                WHERE lr.RequestID = @RequestID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RequestID", RequestID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            RequestIDLabel.Text = $"{RequestID}";
                            StartDate.Text = Convert.ToDateTime(reader["LeaveStartDate"]).ToString("yyyy-MM-dd");
                            EndDate.Text = Convert.ToDateTime(reader["LeaveEndDate"]).ToString("yyyy-MM-dd");
                            string firstName = reader["FirstName"].ToString();
                            string lastName = reader["LastName"].ToString();
                            string employeeName = $"{firstName} {lastName}";
                            EmployeeNameDropDown.Items.Clear();
                            EmployeeNameDropDown.Items.Add(new ListItem(employeeName, RequestID.ToString()));
                            LeaveType.Text = reader["LeaveType"].ToString();
                            StatusDropDown.SelectedValue = reader["Status"].ToString();
                        }
                        reader.Close();
                    }
                }
            }

        }


        protected void UpdateLeaveRecord_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            string updateQuery = @"UPDATE LeaveRecords 
            SET 
                EmployeeID = @EmployeeID, 
                LeaveStartDate = @LeaveStartDate, 
                LeaveEndDate = @LeaveEndDate, 
                LeaveType = @LeaveType, 
                Status = @Status 
            WHERE 
                RequestID = @RequestID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@RequestID", RequestIDLabel.Text);
                command.Parameters.AddWithValue("@EmployeeID", EmployeeNameDropDown.SelectedValue);
                command.Parameters.AddWithValue("@LeaveStartDate", StartDate.Text);
                command.Parameters.AddWithValue("@LeaveEndDate", EndDate.Text);
                command.Parameters.AddWithValue("@LeaveType", LeaveType.Text);
                command.Parameters.AddWithValue("@Status", StatusDropDown.SelectedValue);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Show success message and redirect
                        ltlMessage.Text = "<div class='alert alert-success'><b>Leave record updated successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Redirecting</b> to Leave Records table...</div>";
                        Response.Write("<script>window.setTimeout(function(){ window.location.href = 'Leaverecordelist.aspx'; }, 4400);</script>");
                    }
                    else
                    {
                        // No rows affected, update failed
                        ltlMessage.Text = "<div class='alert alert-danger'><b>Failed</b> to update leave record! Please check your <b>fields</b>!!!</div>";
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and show a generic error message
                    ltlMessage.Text = $"<div class='alert alert-danger'><b>Error:</b> {ex.Message}</div>";
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        protected void BackLeaveRecords_Click(object sender, EventArgs e)
        {
            Response.Redirect("Leaverecordelist.aspx");
        }
    }
}