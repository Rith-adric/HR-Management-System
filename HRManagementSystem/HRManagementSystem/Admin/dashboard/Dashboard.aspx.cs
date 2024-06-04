using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace HRManagementSystem.Admin.dashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int employeeCount = GetEmployeeCount();
                employeeCountLiteral.Text = employeeCount.ToString("N0");

                int departmentCount = GetDepartmentCount();
                departmentCountLiteral.Text = departmentCount.ToString("N0");

                int leaveRecordsCount = GetLeaveRecordsCount();
                leaveRecordsCountLiteral.Text = leaveRecordsCount.ToString("N0");
            }
        }

        public int GetEmployeeCount()
        {
            int employeeCount = 0;

            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            // SQL query to count the employees
            string query = "SELECT COUNT(*) AS EmployeeCount FROM Employees";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    employeeCount = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Handle exception (you can log it or display a message)
                    Console.WriteLine(ex.Message);
                }
            }

            return employeeCount;
        }

        public int GetDepartmentCount()
        {
            int departmentCount = 0;

            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            // SQL query to count the departments
            string query = "SELECT COUNT(*) AS DepartmentCount FROM Departments";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    departmentCount = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Handle exception (you can log it or display a message)
                    Console.WriteLine(ex.Message);
                }
            }

            return departmentCount;
        }

        public int GetLeaveRecordsCount()
        {
            int leaveRecordsCount = 0;

            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

            // SQL query to count the leave records
            string query = "SELECT COUNT(*) AS LeaveRecordsCount FROM LeaveRecords";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    leaveRecordsCount = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Handle exception (you can log it or display a message)
                    Console.WriteLine(ex.Message);
                }
            }

            return leaveRecordsCount;
        }

        protected Literal employeeCountLiteral;
        protected Literal departmentCountLiteral;
        protected Literal leaveRecordsCountLiteral;
    }
}
