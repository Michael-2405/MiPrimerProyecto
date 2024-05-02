using Microsoft.AspNetCore.Mvc;
using MiPrimerProyecto.Clases;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace MiPrimerProyecto.Controllers
{
    [Route("task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public List <TaskView> Get (string username)
        {
            List<TaskView> tarea = new List<TaskView>();
            SqlConnection conn = new SqlConnection(DBConnection.conn);
            conn. Open();
            string consulta = "SELECT Tasks.Id, Tasks.Task, " +
                "Tasks.init_date AS initDate, Tasks.due_date AS dueDate, " +
                "Tasks.User_id AS userId, Status_Task.Status_Task AS statusTask, " +
                "Type_Task.Type_Task AS typeTask " +
                "FROM Tasks " +
                "JOIN Status_Task " +
                "ON Status_Task.Id = Tasks.Status_id " +
                "JOIN Type_Task " +
                "ON Type_Task.Id = Tasks.Type_Task_id " +
                "JOIN Users ON Users.Id = Tasks.User_id " +
                "WHERE Users.Username = @username";
            SqlCommand cmd = new SqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@username", username);
            
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) 
            {
                TaskView actualTask = new TaskView();
                actualTask.taskId = reader["Id"].ToString();
                actualTask.task = reader["Task"].ToString();
                actualTask.taskInitDate = reader["initDate"].ToString();
                actualTask.taskDueDate = reader["dueDate"].ToString();
                actualTask.taskStatus = reader["statusTask"].ToString();
                actualTask.taskType = reader["typeTask"].ToString();

                tarea.Add(actualTask);
                
            }

            cmd.Dispose();
            conn.Close();
            return tarea;

        }

        [HttpPost]
        public void createTask(string task, string taskInitDate, string taskDueDate, string userId, string taskType, string taskStatus)
        {
            SqlConnection conn = new (DBConnection.conn);
            conn.Open();
            string consulta = "INSERT INTO Tasks (Task, init_date, due_date, User_id, Status_id, Type_Task_id) " +
                "VALUES (@task, @init_date, @due_date, @user_id, @status_id, @type_task_id)";
            SqlCommand cmd = new (consulta, conn);
            cmd.Parameters.AddWithValue("@task", task);
            cmd.Parameters.AddWithValue("@init_date", taskInitDate);
            cmd.Parameters.AddWithValue("@due_date", taskDueDate);
            cmd.Parameters.AddWithValue("@user_id", userId);
            cmd.Parameters.AddWithValue("@status_id", taskStatus);
            cmd.Parameters.AddWithValue("@type_task_id", taskType);
            cmd.ExecuteNonQuery();
            cmd.Dispose (); 
            conn.Close();
        }
        
        
    }
}
