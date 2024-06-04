using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace HRManagementSystem.Admin.dashboard
{
    public partial class UpdateEmployee : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int employeeId;
                if (int.TryParse(Request.QueryString["EmployeeID"], out employeeId))
                {
                    UpdateEmployeeIfno(employeeId);
                }
            }
        }

        private void UpdateEmployeeIfno(int employeeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT EmployeeID, FirstName, LastName, Department, Position, DateOfBirth, Salary FROM Employees WHERE EmployeeID = @EmployeeID", conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    EmployeeID.Text = reader["EmployeeID"].ToString();
                    FirstName.Text = reader["FirstName"].ToString();
                    LastName.Text = reader["LastName"].ToString();
                    Department.Text = reader["Department"].ToString();
                    Position.Text = reader["Position"].ToString();
                    DateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    Salary.Text = reader["Salary"].ToString();
                }
                conn.Close();
            }
        }

        protected void UpdateEmployee_Click(object sender, EventArgs e)
        {
            int employeeId;
            if (int.TryParse(EmployeeID.Text, out employeeId))
            {
                SubmitUpdateEmployees(employeeId);
            }
        }

        private void SubmitUpdateEmployees(int employeeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            string updateQuery = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Department = @Department, Position = @Position, DateOfBirth = @DateOfBirth, Salary = @Salary WHERE EmployeeID = @EmployeeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@EmployeeID", EmployeeID.Text);
                command.Parameters.AddWithValue("@FirstName", FirstName.Text);
                command.Parameters.AddWithValue("@LastName", LastName.Text);
                command.Parameters.AddWithValue("@Department", Department.Text);
                command.Parameters.AddWithValue("@Position", Position.Text);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth.Text);
                command.Parameters.AddWithValue("@Salary", Convert.ToDecimal(Salary.Text));

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Show success message and redirect
                        ltlMessage.Text = "<div class='alert alert-success'><b>Employee updated successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Redirecting</b> to Employee table...</div>";
                        Response.Write("<script>window.setTimeout(function(){ window.location.href = 'EmployeeList.aspx'; }, 4400);</script>");
                    }
                    else
                    {
                        // No rows affected, update failed
                        ltlMessage.Text = "<div class='alert alert-danger'><b>Fails</b> to update employee! Please check your <b>field</b>!!!</div>";
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and show a generic error message
                    ltlMessage.Text += ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected void btnBackEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");
        }
    }
}
