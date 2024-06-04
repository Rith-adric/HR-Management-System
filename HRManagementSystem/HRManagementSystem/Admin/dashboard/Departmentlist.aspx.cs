using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace HRManagementSystem.Admin.dashboard
{
    public partial class DepartmentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartmentGridView();
            }
        }

        private void BindDepartmentGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DepartmentID, DepartmentName, ManagerID FROM Departments", conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                DepartmentGridView.DataSource = dt;
                DepartmentGridView.DataBind();
            }
        }

        protected void btnAddDepartment_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddDepartment.aspx");
        }

        protected void DepartmentGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int departmentId = Convert.ToInt32(DepartmentGridView.DataKeys[e.NewEditIndex].Value);
            Response.Redirect($"UpdateDepartment.aspx?DepartmentID={departmentId}");
        }

        protected void DepartmentGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int departmentId = Convert.ToInt32(DepartmentGridView.DataKeys[e.RowIndex].Value);

            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Departments WHERE DepartmentID = @DepartmentID", conn);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                conn.Open();
                cmd.ExecuteNonQuery();
                ltlMessage.Text = "<div class='alert alert-success'><b>Department deleted successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Refreshing...</b></div>";
                Response.Write("<script>window.setTimeout(function(){ window.location.href = 'DepartmentList.aspx'; }, 3000);</script>");
                conn.Close();
            }

            BindDepartmentGridView();
        }
    }
}
