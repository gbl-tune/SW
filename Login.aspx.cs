using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SW
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {





        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Value.Trim();
            string password = txtPassword.Value.Trim();
            
            string query = @"SELECT u.Id, u.Username, c.Name AS CategoryName 
                         FROM Users u 
                         INNER JOIN Categories c ON u.CategoryId = c.Id 
                         WHERE u.Username = @Username AND u.Password = @Password";

            SqlParameter[] parameters = {
            new SqlParameter("@Username", username),
            new SqlParameter("@Password", password)
             };

            DataTable result = Database.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                Session["UserCategory"] = result.Rows[0]["CategoryName"].ToString();
                Session["User"] = username;
                Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }
    }
}
