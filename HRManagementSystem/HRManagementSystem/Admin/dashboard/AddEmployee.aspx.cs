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
    public partial class AddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void AddEmployee_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Employees (FirstName, LastName, Department, Position, DateOfBirth, Salary) " +
                                   "VALUES (@FirstName, @LastName, @Department, @Position, @DateOfBirth, @Salary)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", FirstName.Text);
                    command.Parameters.AddWithValue("@LastName", LastName.Text);
                    command.Parameters.AddWithValue("@Department", Department.Text);
                    command.Parameters.AddWithValue("@Position", Position.Text);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth.Text);
                    command.Parameters.AddWithValue("@Salary", Convert.ToDecimal(Salary.Text));

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        ltlMessage.Text = "<div class='alert alert-success'><b>Employee inserted successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Redirecting</b> to employees table...</div>";
                        Response.Write("<script>window.setTimeout(function(){ window.location.href = 'EmployeeList.aspx'; }, 4500);</script>");
                    }
                    catch (Exception ex)
                    {
                        ltlMessage.Text += ex.Message;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        protected void btnBackEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");
        }

        private bool IsValidInput()
        {
            bool isValid = true;
            // Check if required fields are filled
            if (string.IsNullOrWhiteSpace(FirstName.Text) &&
                string.IsNullOrWhiteSpace(LastName.Text) &&
                string.IsNullOrWhiteSpace(Department.Text) &&
                string.IsNullOrWhiteSpace(Position.Text) &&
                string.IsNullOrWhiteSpace(DateOfBirth.Text) &&
                string.IsNullOrWhiteSpace(Salary.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'><b>Fields</b> are empty. Please enter data.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(FirstName.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>first name</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(LastName.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>last name</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(Department.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>department</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(Position.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>position</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(DateOfBirth.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>date of birth</b>.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(Salary.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter a <b>salary</b>.</div>";
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