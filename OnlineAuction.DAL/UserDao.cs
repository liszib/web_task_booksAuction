using OnlineAuction.DAL.Interface;
using OnlineAuction.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OnlineAuction.DAL
{
    public class UserDao : IUserDao
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["OnlineAuction"].ConnectionString;
        public int Add(User user)
        {
            int lastId = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddUser";
                cmd.Parameters.AddWithValue(@"Name", user.Name);
                cmd.Parameters.AddWithValue(@"SurName", user.SurName);
                cmd.Parameters.AddWithValue(@"Birthday", user.DateOfBirth);
                cmd.Parameters.AddWithValue(@"PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue(@"Email", user.Email);
                cmd.Parameters.AddWithValue(@"Password", user.Password);

                var id = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "ID",
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(id);
                connection.Open();

                 lastId = int.Parse(cmd.ExecuteScalar().ToString());
            }
            return lastId;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetListUsers";
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    User user = new User();

                    int ID = (int)reader["ID"];
                    string Name = (string)reader["Name"];
                    string SurName = (string)reader["SurName"];
                    DateTime Birthday = (DateTime)reader["Birthday"];
                    string Phone = (string)reader["PhoneNumber"];
                    string Email = (string)reader["Email"];
                    string Password = (string)reader["Password"];

                    user.Id = ID;
                    user.Name = Name;
                    user.SurName = SurName;
                    user.DateOfBirth = Birthday;
                    user.PhoneNumber = Phone;
                    user.Email = Email;
                    user.Password = Password;

                    users.Add(user);
                }

            }
            return users;
        }

        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteUser";
                cmd.Parameters.AddWithValue(@"UserID", userId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserById(int userId)
        {
            User user = new User();

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUserByID";
                cmd.Parameters.AddWithValue(@"UserID", userId);
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int ID = (int)reader["ID"];
                    string Name = (string)reader["Name"];
                    string SurName = (string)reader["SurName"];
                    DateTime Birthday = (DateTime)reader["Birthday"];
                    string Phone = (string)reader["PhoneNumber"];
                    string Email = (string)reader["Email"];
                    string Password = (string)reader["Password"];

                    user.Id = ID;
                    user.Name = Name;
                    user.SurName = SurName;
                    user.DateOfBirth = Birthday;
                    user.PhoneNumber = Phone;
                    user.Email = Email;
                    user.Password = Password;
                }

            }
            return user;
        }
    }
}
