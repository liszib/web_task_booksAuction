using Microsoft.Extensions.Configuration;
using OnlineAuction.DAL.Interface;
using OnlineAuction.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OnlineAuction.DAL
{
    public class LotDao : ILotDao
    {
        private readonly string _connectionString;

        public LotDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OnlineAuction");
        }

        public int Add(Lot lot)
        {
            int lastId = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddLot";
                cmd.Parameters.AddWithValue(@"Name", lot.Name);
                cmd.Parameters.AddWithValue(@"Price", lot.Price);
                cmd.Parameters.AddWithValue(@"Author", lot.Author);
                cmd.Parameters.AddWithValue(@"Genre", lot.Genre);
                cmd.Parameters.AddWithValue(@"Seller", lot.Seller.Id);
                cmd.Parameters.AddWithValue(@"Description", lot.Description);

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

        public void BuyLot(int userId, int lotId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BuyLot";
                cmd.Parameters.AddWithValue(@"UserID", userId);
                cmd.Parameters.AddWithValue(@"LotID", lotId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteLot(int lotId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteLot";
                cmd.Parameters.AddWithValue(@"LotID", lotId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Lot> GetAll()
        {
            List<Lot> lots = new List<Lot>();

            int ID = 0;
            string Name = "";
            string Author = "";
            string Genre = "";
            int Price = 0;
            int Seller = 0;
            int Customer = 0;
            string Description = "";

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetListLots";
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserDao userDao = new UserDao(_connectionString);
                    Lot newLot = new Lot();

                    ID = (int)reader["ID"];
                    Name = (string)reader["Name"];
                    Author = (string)reader["Author"];
                    Genre = (string)reader["Genre"];
                    Price = (int)reader["Price"];
                    Seller = (int)reader["Seller"];
                    Description = (string)reader["Description"];

                    if (reader["Customer"].ToString() != "")
                        Customer = (int)reader["Customer"];
                    else
                        Customer = 0;

                    newLot.Id = ID;
                    newLot.Name = Name;
                    newLot.Price = Price;
                    newLot.Author = Author;
                    newLot.Genre = Genre;
                    newLot.Seller = userDao.GetUserById(Seller);
                    if (Customer != 0)
                        newLot.Customer = userDao.GetUserById(Customer);
                    newLot.Description = Description;

                    lots.Add(newLot);
                }

            }
            return lots;
        }

        public IEnumerable<Lot> GetBoughtLots(int userId)
        {
            List<Lot> lots = new List<Lot>();
            int ID = 0;
            string Name = "";
            string Author = "";
            string Genre = "";
            int Price = 0;
            int Seller = 0;
            int Customer = 0;
            string Description = "";

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetBoughtLots";
                cmd.Parameters.AddWithValue(@"UserID", userId);
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserDao userDao = new UserDao(_connectionString);
                    Lot newLot = new Lot();

                    ID = (int)reader["ID"];
                    Name = (string)reader["Name"];
                    Author = (string)reader["Author"];
                    Genre = (string)reader["Genre"];
                    Price = (int)reader["Price"];
                    Seller = (int)reader["Seller"];

                    Customer = (int)reader["Customer"];
                    Description = (string)reader["Description"];

                    newLot.Id = ID;
                    newLot.Name = Name;
                    newLot.Price = Price;
                    newLot.Author = Author;
                    newLot.Genre = Genre;
                    newLot.Seller = userDao.GetUserById(Seller);
                    newLot.Customer = userDao.GetUserById(Customer);
                    newLot.Description = Description;

                    lots.Add(newLot);
                }
            }
            return lots;
        }

        public Lot GetLotById(int lotId)
        {
            Lot newLot = new Lot();
            UserDao userDao = new UserDao(_connectionString);

            int ID = 0;
            string Name = "";
            string Author = "";
            string Genre = "";
            int Price = 0;
            int Seller = 0;
            int Customer = 0;
            string Description = "";

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetLotByID";
                cmd.Parameters.AddWithValue(@"LotID", lotId);
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    ID = (int)reader["ID"];
                    Name = (string)reader["Name"];
                    Author = (string)reader["Author"];
                    Genre = (string)reader["Genre"];
                    Price = (int)reader["Price"];
                    Seller = (int)reader["Seller"];

                    if (reader["Customer"].ToString() != "")
                        Customer = (int)reader["Customer"];
                    else
                        Customer = 0;
                    Description = (string)reader["Description"];

                    newLot.Id = ID;
                    newLot.Name = Name;
                    newLot.Price = Price;
                    newLot.Author = Author;
                    newLot.Genre = Genre;
                    newLot.Seller = userDao.GetUserById(Seller);
                    if (Customer != 0)
                        newLot.Customer = userDao.GetUserById(Customer);
                    newLot.Description = Description;

                }
            }
            return newLot;
        }

        public IEnumerable<Lot> GetSellLots(int userId)
        {
            List<Lot> lots = new List<Lot>();
            int ID = 0;
            string Name = "";
            string Author = "";
            string Genre = "";
            int Price = 0;
            int Seller = 0;
            int Customer = 0;
            string Description = "";

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetSellLots";
                cmd.Parameters.AddWithValue(@"UserID", userId);
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserDao userDao = new UserDao(_connectionString);
                    Lot newLot = new Lot();

                    ID = (int)reader["ID"];
                    Name = (string)reader["Name"];
                    Author = (string)reader["Author"];
                    Genre = (string)reader["Genre"];
                    Price = (int)reader["Price"];
                    Seller = (int)reader["Seller"];

                    if (reader["Customer"].ToString() != "")
                        Customer = (int)reader["Customer"];
                    else
                        Customer = 0;
                    Description = (string)reader["Description"];

                    newLot.Id = ID;
                    newLot.Name = Name;
                    newLot.Price = Price;
                    newLot.Author = Author;
                    newLot.Genre = Genre;
                    newLot.Seller = userDao.GetUserById(Seller);
                    if (Customer != 0)
                        newLot.Customer = userDao.GetUserById(Customer);
                    newLot.Description = Description;

                    lots.Add(newLot);
                }
            }
            return lots;
        }

        public IEnumerable<Lot> GetNoUserLotForSellByID(int userId)
        {
            List<Lot> lots = new List<Lot>();
            int ID = 0;
            string Name = "";
            string Author = "";
            string Genre = "";
            int Price = 0;
            int Seller = 0;
            int Customer = 0;
            string Description = "";

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetNoUserLotForSellByID";
                cmd.Parameters.AddWithValue(@"UserID", userId);
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserDao userDao = new UserDao(_connectionString);

                    Lot newLot = new Lot();

                    ID = (int)reader["ID"];
                    Name = (string)reader["Name"];
                    Author = (string)reader["Author"];
                    Genre = (string)reader["Genre"];
                    Price = (int)reader["Price"];
                    Seller = (int)reader["Seller"];

                    if (reader["Customer"].ToString() != "")
                        Customer = (int)reader["Customer"];
                    else
                        Customer = 0;
                    Description = (string)reader["Description"];

                    newLot.Id = ID;
                    newLot.Name = Name;
                    newLot.Price = Price;
                    newLot.Author = Author;
                    newLot.Genre = Genre;
                    newLot.Seller = userDao.GetUserById(Seller);
                    if (Customer != 0)
                        newLot.Customer = userDao.GetUserById(Customer);
                    newLot.Description = Description;

                    if (newLot.Customer == null)
                        lots.Add(newLot);
                }
            }
            return lots;
        }

        public IEnumerable<Lot> GetNoUserLotForSellByIDAndGenre(int userId, string genre)
        {
            List<Lot> lots = new List<Lot>();
            int ID = 0;
            string Name = "";
            string Author = "";
            string Genre = "";
            int Price = 0;
            int Seller = 0;
            int Customer = 0;
            string Description = "";

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetNoUserLotForSellByIDAndGenre";
                cmd.Parameters.AddWithValue(@"UserID", userId);
                cmd.Parameters.AddWithValue(@"Genre", genre);
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserDao userDao = new UserDao(_connectionString);
                    Lot newLot = new Lot();

                    ID = (int)reader["ID"];
                    Name = (string)reader["Name"];
                    Author = (string)reader["Author"];
                    Genre = (string)reader["Genre"];
                    Price = (int)reader["Price"];
                    Seller = (int)reader["Seller"];

                    if (reader["Customer"].ToString() != "")
                        Customer = (int)reader["Customer"];
                    else
                        Customer = 0;
                    Description = (string)reader["Description"];

                    newLot.Id = ID;
                    newLot.Name = Name;
                    newLot.Price = Price;
                    newLot.Author = Author;
                    newLot.Genre = Genre;
                    newLot.Seller = userDao.GetUserById(Seller);
                    if (Customer != 0)
                        newLot.Customer = userDao.GetUserById(Customer);
                    newLot.Description = Description;

                    if (newLot.Customer == null)
                        lots.Add(newLot);
                }
            }
            return lots;
        }

        public IEnumerable<Lot> GetAllLotsByGenre(string genre)
        {
            List<Lot> lots = new List<Lot>();

            int ID = 0;
            string Name = "";
            string Author = "";
            string Genre = "";
            int Price = 0;
            int Seller = 0;
            int Customer = 0;
            string Description = "";

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllLotsByGenre";
                cmd.Parameters.AddWithValue(@"Genre", genre);
                connection.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserDao userDao = new UserDao(_connectionString);
                    Lot newLot = new Lot();

                    ID = (int)reader["ID"];
                    Name = (string)reader["Name"];
                    Author = (string)reader["Author"];
                    Genre = (string)reader["Genre"];
                    Price = (int)reader["Price"];
                    Seller = (int)reader["Seller"];

                    if (reader["Customer"].ToString() != "")
                        Customer = (int)reader["Customer"];
                    else
                        Customer = 0;
                    Description = (string)reader["Description"];

                    newLot.Id = ID;
                    newLot.Name = Name;
                    newLot.Price = Price;
                    newLot.Author = Author;
                    newLot.Genre = Genre;
                    newLot.Seller = userDao.GetUserById(Seller);
                    if (Customer != 0)
                        newLot.Customer = userDao.GetUserById(Customer);
                    newLot.Description = Description;

                    lots.Add(newLot);
                }

            }
            return lots;
        }

    }

}
