using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class Database

{

    /// <summary>
    /// Classe para conexao com base de dados para Login
    /// </summary>


    //private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    private static string connectionString = "Data Source=JARVIS;Initial Catalog=SW;Integrated Security=True";

    public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                DataTable result = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(result);

                return result;
            }
        }
    }

    public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
    public static int ExecuReader(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    sdr.Read();

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}