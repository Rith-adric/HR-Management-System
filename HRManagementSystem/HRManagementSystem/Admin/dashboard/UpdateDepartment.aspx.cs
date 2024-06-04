using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace HRManagementSystem.Admin.dashboard
{
    public partial class UpdateDepartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate department details for updating
                if (Request.QueryString["DepartmentID"] != null)
                {
                    int departmentId = Convert.ToInt32(Request.QueryString["DepartmentID"]);
                    ShowDepartmentDetails(departmentId);
                    ShowManagerIDDropdown();
                }
            }
        }

        private void ShowManagerIDDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT EmployeeID, FirstName, LastName FROM Employees";

                SqlCommand cmd = new SqlCommand(query, conn);
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

        private void ShowDepartmentDetails(int departmentId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT d.DepartmentName, d.ManagerID, e.FirstName, e.LastName 
                FROM Departments d
                INNER JOIN Employees e ON d.ManagerID = e.EmployeeID
                WHERE d.DepartmentID = @DepartmentID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Display department ID
                    DepartmentID.Text = departmentId.ToString();

                    // Display department name
                    DepartmentName.Text = reader["DepartmentName"].ToString();

                    // Construct manager's full name
                    string firstName = reader["FirstName"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string employeeName = $"{firstName} {lastName}";

                    // Add manager to the dropdown list
                    manager.Items.Add(new ListItem(employeeName, reader["ManagerID"].ToString()));
                    manager.Text = reader["ManagerID"].ToString();
                }

                reader.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int departmentId = Convert.ToInt32(Request.QueryString["DepartmentID"]);
            string departmentName = DepartmentName.Text;
            int managerId = Convert.ToInt32(manager.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Departments SET DepartmentName = @DepartmentName, ManagerID = @ManagerID WHERE DepartmentID = @DepartmentID", conn);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@ManagerID", managerId);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                {
                    ltlMessage.Text = "<div class='alert alert-success'><b>Department updated successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Redirecting</b> to Department table...</div>";
                    Response.Write("<script>window.setTimeout(function(){ window.location.href = 'Departmentlist.aspx'; }, 4400);</script>");
                }
                else
                {
                    ltlMessage.Text = "<div class='alert alert-danger'><b>Failed</b> to update department.</div>";
                }
            }
        }

        protected void btnBackEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Departmentlist.aspx");
        }
    }
}
