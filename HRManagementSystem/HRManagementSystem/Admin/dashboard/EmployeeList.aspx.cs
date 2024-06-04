using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace HRManagementSystem.Admin.dashboard
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployeeGridView();
            }
        }

        private void BindEmployeeGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT EmployeeID, FirstName, LastName, Department, Position, DateOfBirth, Salary FROM Employees", conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                EmployeeGridView.DataSource = dt;
                EmployeeGridView.DataBind();
            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEmployee.aspx");
        }

        protected void EmployeeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int employeeId = Convert.ToInt32(EmployeeGridView.DataKeys[e.NewEditIndex].Value);
            Response.Redirect($"UpdateEmployee.aspx?EmployeeID={employeeId}");
        }

        protected void EmployeeGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int employeeId = Convert.ToInt32(EmployeeGridView.DataKeys[e.RowIndex].Value);

            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = @EmployeeID", conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                conn.Open();
                cmd.ExecuteNonQuery();
                ltlMessage.Text = "<div class='alert alert-success'><b>Employee deleted successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Refreshing...</b></div>";
                Response.Write("<script>window.setTimeout(function(){ window.location.href = 'EmployeeList.aspx'; }, 3000);</script>");
                conn.Close();
            }

            BindEmployeeGridView();
        }
    }
}
