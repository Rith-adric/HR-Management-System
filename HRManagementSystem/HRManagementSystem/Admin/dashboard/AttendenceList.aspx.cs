using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRManagementSystem.Admin.dashboard
{
    public partial class AttendenceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployeeAttendanceGridView();
            }
        }

        private void BindEmployeeAttendanceGridView()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT AttendanceID, EmployeeID, AttendanceDate, StartTime, EndTime, Status FROM Attendance", conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                AttendanceGridView.DataSource = dt;
                AttendanceGridView.DataBind();
            }
        }

        protected void btnAddAttendanceList_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddAttendance.aspx");
        }

        protected void AttendanceGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int attendanceId = Convert.ToInt32(AttendanceGridView.DataKeys[e.NewEditIndex].Value);
            Response.Redirect($"UpdateAttendance.aspx?AttendanceID={attendanceId}");
        }

        protected void AttendanceGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int attendanceId = Convert.ToInt32(AttendanceGridView.DataKeys[e.RowIndex].Value);

            string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Attendance WHERE AttendanceID = @AttendanceID", conn);
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceId);

                conn.Open();
                cmd.ExecuteNonQuery();
                ltlMessage.Text = "<div class='alert alert-success'><b>Employee Attendance deleted successfully!</b>  <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Refreshing...</b></div>";
                Response.Write("<script>window.setTimeout(function(){ window.location.href = 'AttendenceList.aspx'; }, 3000);</script>");
                conn.Close();
            }

            BindEmployeeAttendanceGridView();
        }

    }
}