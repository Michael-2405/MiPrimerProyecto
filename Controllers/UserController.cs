using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimerProyecto.Clases;
using Microsoft.Data.SqlClient;

namespace MiPrimerProyecto.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public bool Get (string username, string password)
        {
            bool existe = false;
            SqlConnection conn = new SqlConnection(DBConnection.conn);
            conn .Open ();
            string query = "Select * from Users where Username = @username and Password = @password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                existe = true;
            }
            cmd.Dispose();
            conn.Close ();
            return existe;
        }

        [HttpPost]
        public void createUser(string mail, string username, string password)
        {
            SqlConnection conn = new SqlConnection (DBConnection.conn);
            conn .Open ();
            string consulta = "INSERT INTO Users (Mail, Username, Password) VALUES (@mail, @username, @password)";
            SqlCommand cmd = new(consulta, conn);
            cmd.Parameters.AddWithValue("@mail", mail);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

    }
}
