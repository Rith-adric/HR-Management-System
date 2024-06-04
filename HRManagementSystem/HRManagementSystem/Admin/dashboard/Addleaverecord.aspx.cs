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
    public partial class Addleaverecord : System.Web.UI.Page
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

                EmployeeIDDropDown.Items.Clear();
                EmployeeIDDropDown.Items.Add(new ListItem("Select Manager", ""));

                while (reader.Read())
                {
                    int employeeId = Convert.ToInt32(reader["EmployeeID"]);
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string employeeName = $"{firstName} {lastName}";
                    EmployeeIDDropDown.Items.Add(new ListItem(employeeName, employeeId.ToString()));
                }

                reader.Close();
                conn.Close();
            }
        }

        protected void AddLeaveRecord_Click(object sender, EventArgs e)
        {
            string selectedEmployeeId = EmployeeIDDropDown.SelectedValue;

            if (!string.IsNullOrEmpty(selectedEmployeeId) && IsValidInput())
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO LeaveRecords (EmployeeID, LeaveStartDate, LeaveEndDate, LeaveType, Status) VALUES (@EmployeeID, @LeaveStartDate, @LeaveEndDate, @LeaveType, @Status)";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@EmployeeID", int.Parse(selectedEmployeeId));
                    cmd.Parameters.AddWithValue("@LeaveStartDate", DateTime.Parse(StartDate.Text));
                    cmd.Parameters.AddWithValue("@LeaveEndDate", DateTime.Parse(EndDate.Text));
                    cmd.Parameters.AddWithValue("@LeaveType", LeaveType.Text);
                    cmd.Parameters.AddWithValue("@Status", StatusDropDown.Text);

                    try
                    {
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ltlMessage.Text = "<div class='alert alert-success'><b>Leave record inserted successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Redirecting</b> to leave record table...</div>";
                            Response.Write("<script>window.setTimeout(function(){ window.location.href = 'Leaverecordelist.aspx'; }, 4000);</script>");
                        }
                        else
                        {
                            ltlMessage.Text = "<div class='alert alert-danger'>Failed to insert leave record.</div>";
                        }
                    }
                    catch (Exception ex)
                    {
                        ltlMessage.Text = "<div class='alert alert-danger'>" + ex.Message + "</div>";
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please select a valid manager and ensure all input fields are correctly filled.</div>";
            }
        }


        private bool IsValidInput()
        {
            bool isValid = true;
            // Check if required fields are filled
            if (string.IsNullOrWhiteSpace(StartDate.Text) &&
                string.IsNullOrWhiteSpace(EndDate.Text) &&
                string.IsNullOrWhiteSpace(LeaveType.Text) &&
                string.IsNullOrWhiteSpace(StatusDropDown.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'><b>Fields</b> are empty. Please enter data.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(StartDate.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please choosing a <b>Start Time</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(EndDate.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please choosing a <b>End Time</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(LeaveType.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter <b>Leave Types</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(StatusDropDown.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please selecting a <b>Status</b>.</div>";
                isValid = false;
            }
            else
            {
                // Additional validation logic can be added here
            }
            return isValid;
        }

        protected void BackLeaveRecords_Click(object sender, EventArgs e)
        {
            Response.Redirect("Leaverecordelist.aspx");
        }
    }
}