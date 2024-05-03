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
        public void createTask(TaskCreate newTask)
        {
            SqlConnection conn = new (DBConnection.conn);
            conn.Open();
            string consulta = "INSERT INTO Tasks (Task, init_date, due_date, User_id, Status_id, Type_Task_id) " +
                "VALUES (@task, @initDate, @dueDate, @userId, @statusId, @typeTaskid)";
            SqlCommand cmd = new (consulta, conn);
            cmd.Parameters.AddWithValue("@task", newTask.task);
            cmd.Parameters.AddWithValue("@initDate", newTask.taskInitDate);
            cmd.Parameters.AddWithValue("@dueDate", newTask.taskDueDate);
            cmd.Parameters.AddWithValue("@userId", newTask.userId);
            cmd.Parameters.AddWithValue("@statusId", newTask.taskStatus);
            cmd.Parameters.AddWithValue("@typeTaskid", newTask.taskType);
            cmd.ExecuteNonQuery();

            cmd.Dispose (); 
            conn.Close();
        }

        [HttpPut]
        [Route("update/task")]
        public void updateTask(string taskId, string task) 
        {
            SqlConnection conn = new(DBConnection.conn);
            conn.Open();
            string consulta = "UPDATE Tasks SET Task = @task WHERE Id = @taskId";
            SqlCommand cmd = new(consulta, conn);
            cmd.Parameters.AddWithValue("@task", task);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.ExecuteNonQuery ();

            cmd.Dispose();
            conn.Close();
        }

        [HttpPut]
        [Route("update/dueDate")]
        public void updateDueDate(string taskId, string taskDueDate)
        {
            SqlConnection conn = new(DBConnection.conn);
            conn.Open();
            string consulta = "UPDATE Tasks SET due_date = @taskDueDate WHERE Id = @taskId";
            SqlCommand cmd = new(consulta, conn);
            cmd.Parameters.AddWithValue("@taskDueDate", taskDueDate);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }
        [HttpPut]
        [Route("update/taskStatus")]
        public void updateTaskStatus(string taskId, string taskStatus)
        {
            SqlConnection conn = new(DBConnection.conn);
            conn.Open();
            string consulta = "UPDATE Tasks SET Status_id = @taskStatus WHERE Id = @taskId";
            SqlCommand cmd = new(consulta, conn);
            cmd.Parameters.AddWithValue("@taskStatus", taskStatus);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        [HttpPut]
        [Route("update/taskType")]
        public void updateTask2(string taskId, string taskType)
        {
            SqlConnection conn = new(DBConnection.conn);
            conn.Open();
            string consulta = "UPDATE Tasks SET Type_Task_id = @taskType WHERE Id = @taskId";
            SqlCommand cmd = new(consulta, conn);
            cmd.Parameters.AddWithValue("@taskType", taskType);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        [HttpDelete]
        public void deleteTask(string taskId) 
        {
            SqlConnection conn = new(DBConnection.conn);
            conn.Open();
            string consulta = "DELETE FROM Tasks WHERE Id = @taskId;";
            SqlCommand cmd = new(consulta, conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }



    }
}
