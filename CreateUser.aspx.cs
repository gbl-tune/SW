using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SW
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserCategory"] == null || Session["UserCategory"].ToString() != "Admin")
                {
                    Response.Redirect("~/Login.aspx");
                }

                string query = "SELECT Id, Name FROM Categories";
                DataTable categories = Database.ExecuteQuery(query);

                ddlCategory.DataSource = categories;
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataBind();
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);

            string query = "INSERT INTO Users (Username, Password, CategoryId) VALUES (@Username, @Password, @CategoryId)";
            SqlParameter[] parameters = {
            new SqlParameter("@Username", username),
            new SqlParameter("@Password", password),
            new SqlParameter("@CategoryId", categoryId)
        };

            int rowsAffected = Database.ExecuteNonQuery(query, parameters);

            if (rowsAffected > 0)
            {
                lblMessage.Text = "User created successfully.";
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
            }
            else
            {
                lblMessage.Text = "Error creating user.";
            }
        }
    }
}