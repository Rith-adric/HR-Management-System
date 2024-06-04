using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRManagementSystem.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND Password=@Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    command.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 1)
                    {
                        Session["Username"] = txtUsername.Text.Trim();
                        ltlMessage.Text = "<div class='alert alert-success'><b>Login successful!</b>  Redirecting to our dasoboard....<i class='bx bx-loader bx-spin font-size-16 align-middle me-2'></i></div>";
                        Response.Write("<script>window.setTimeout(function(){ window.location.href = 'dashboard/Dashboard.aspx'; }, 4600);</script>");
                    }
                    else
                    {
                        ltlMessage.Text = "<div class='alert alert-danger'>Please check, Invalid <b>username</b> or <b>password</b>!!!</div>";
                    }
                }
            }
        }

        private bool IsValidInput()
        {
            bool isValid = true;
            // Check if required fields are filled
            if (string.IsNullOrWhiteSpace(txtUsername.Text) &&
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter your <b>username</b> and <b>password</b> belows.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter your <b>Username</b> below.</div>";
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ltlMessage.Text = "<div class='alert alert-danger'>Please enter your <b>Password</b> below.</div>";
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