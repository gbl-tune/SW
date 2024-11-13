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
        string connectionString = "Data Source=JARVIS;Initial Catalog=SW;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarUsuarios();
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
        // Método para carregar os usuários no GridView
        private void CarregarUsuarios()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvUsuarios.DataSource = dt;
                gvUsuarios.DataBind();
            }
        }
        // Método para entrar no modo de edição de um usuário
        protected void gvUsuarios_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvUsuarios.EditIndex = e.NewEditIndex;
            CarregarUsuarios();
        }

        // Método para cancelar a edição
        protected void gvUsuarios_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvUsuarios.EditIndex = -1;
            CarregarUsuarios();
        }

        // Método para atualizar as informações do usuário
        protected void gvUsuarios_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            // Obtém o Id do usuário que está sendo editado
            int userId = Convert.ToInt32(gvUsuarios.DataKeys[e.RowIndex].Value.ToString());

            // Obtém os novos valores inseridos pelo admin
            string novoNome = ((TextBox)gvUsuarios.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            string novoEmail = ((TextBox)gvUsuarios.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            // Atualiza no banco de dados
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Username = @Nome WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nome", novoNome);
                cmd.Parameters.AddWithValue("@Email", novoEmail);
                cmd.Parameters.AddWithValue("@Id", userId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // Sai do modo de edição
            gvUsuarios.EditIndex = -1;
            CarregarUsuarios();
        }

        // Método para excluir um usuário
        protected void gvUsuarios_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            // Obtém o Id do usuário que está sendo excluído
            int userId = Convert.ToInt32(gvUsuarios.DataKeys[e.RowIndex].Value.ToString());

            // Deleta o usuário do banco de dados
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", userId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // Recarrega a lista de usuários
            CarregarUsuarios();
        }

    }
}