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
    public partial class AddDepartment : System.Web.UI.Page
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

        protected void AddDepartment_Click(object sender, EventArgs e)
        {
            string selectedManagerId = manager.SelectedValue;
            string departmentName = DepartmentName.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Departments (DepartmentName, ManagerID) VALUES (@DepartmentName, @ManagerID)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@ManagerID", selectedManagerId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    ltlMessage.Text = "<div class='alert alert-success'><b>Department inserted successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Redirecting</b> to department list...</div>";
                    Response.Write("<script>window.setTimeout(function(){ window.location.href = 'DepartmentList.aspx'; }, 4000);</script>");
                }
                catch (Exception ex)
                {
                    ltlMessage.Text += ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void BackDepartment_Click(object sender, EventArgs e)
        {
            Response.Redirect("Departmentlist.aspx");
        }
    }
}