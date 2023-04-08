using Hackathon.Model;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Hackathon.Common;
using Hackathon.Request;
using Microsoft.Extensions.Logging;

namespace Hackathon.DAL
{
    public class UserDAL
    {
        private readonly ILogger _logger;
        public static string? _connStr { get; set; } 
        public UserDAL(string connStr, ILogger ilogger)
        {
            _connStr = connStr;
            _logger = ilogger;
        }


        public List<Users> GetUsers()
        {
            List<Users> UserList = new List<Users>();
            var connection = new MySqlConnection(_connStr);
            _logger.LogInformation(_connStr);
            try
            {
                connection.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "select * from user;";
                cmd.CommandType = CommandType.Text;
                using MySqlDataReader rdr = cmd.ExecuteReader();
                UserList = DataReaderMapToList.DataReaderToList<Users>(rdr);
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return UserList;
        }

        public Users GetUser(string userName, string password)
        {
            List<Users> UserList = new List<Users>();
            var connection = new MySqlConnection(_connStr);
            try
            {
                connection.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "select * from user where username = '" + userName + "' and password = '" +password+ "'; "; 
                cmd.CommandType = CommandType.Text;
                using MySqlDataReader rdr = cmd.ExecuteReader();
                UserList = DataReaderMapToList.DataReaderToList<Users>(rdr);
                connection.Close();
            }
            catch (Exception ex)
            {

            }
            if (UserList.Count > 0)
            {
                return UserList[0];
            }
            return null;
        }

        public bool SaveUser(SaveUserRequest saveRequest)
        {
            string sqlStr = "Insert into User (UserName, Password, UserType,mobilenumber, Status) values ('" + saveRequest.UserName + "','" + saveRequest.Password + "', '" + saveRequest.UserType + "', '" + saveRequest.mobileNumber + "', 0 )";
            return executeDynamicScript(sqlStr);
        }

        public bool executeDynamicScript(string sqlStatement)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                var connection = new MySqlConnection(_connStr);
                connection.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = sqlStatement;
                cmd.CommandTimeout = 300;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
