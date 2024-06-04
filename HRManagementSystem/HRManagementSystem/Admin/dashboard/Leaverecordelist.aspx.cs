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
    public partial class Leaverecordelist : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Sqlserver"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLeaveRecordsGridView();
            }
        }

        private void BindLeaveRecordsGridView()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM LeaveRecords", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        LeaveRecordsGridView.DataSource = dt;
                        LeaveRecordsGridView.DataBind();
                    }
                }
            }
        }

        protected void btnAddLeaveRecords_Click(object sender, EventArgs e)
        {
            Response.Redirect("Addleaverecord.aspx");
        }

        protected void LeaveRecordsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int RequestID = Convert.ToInt32(LeaveRecordsGridView.DataKeys[e.NewEditIndex].Value);
            Response.Redirect($"Updateleaverecord.aspx?RequestID={RequestID}");
        }

        protected void LeaveRecordsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int requestID = Convert.ToInt32(LeaveRecordsGridView.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM LeaveRecords WHERE RequestID = @RequestID", con))
                {
                    cmd.Parameters.AddWithValue("@RequestID", requestID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ltlMessage.Text = "<div class='alert alert-success'><b>Leave record deleted successfully!</b> <i class='bx bx-hourglass bx-spin font-size-16 align-middle me-2'></i> <b>Refreshing...</b></div>";
                    Response.Write("<script>window.setTimeout(function(){ window.location.href = 'Leaverecordelist.aspx'; }, 2600);</script>");
                    con.Close();
                }
            }

            BindLeaveRecordsGridView();
        }
    }
}