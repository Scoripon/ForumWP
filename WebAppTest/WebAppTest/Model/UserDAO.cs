using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest.Model
{
    public class UserDAO
    {
        string connectionString = @"Data Source=(local)\LAZARSQL;Initial Catalog=PaymenySystem;Integrated Security = True;";
        //To View all employees details
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                List<User> users = new List<User>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        User user = new User();
                        user.ID = Convert.ToInt32(rdr["id_korisnik"]);
                        user.Ime = rdr["ime"].ToString();
                        user.Prezime = rdr["prezime"].ToString();
                        user.Email = rdr["email"].ToString();
                        user.Username = rdr["username"].ToString();
                        user.Password = rdr["password"].ToString();
                        user.Tip = rdr["tip"].ToString();
                        users.Add(user);
                    }
                    con.Close();
                }
                return users;
            }
            catch
            {
                throw;
            }
        }
        //To Add new employee record 
        public int AddUser(User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ime", user.Ime);
                    cmd.Parameters.AddWithValue("@Prezime", user.Prezime);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Tip", user.Tip);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //To Update the records of a particluar user
        public int UpdateUser(User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ime", user.Ime);
                    cmd.Parameters.AddWithValue("@Prezime", user.Prezime);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Tip", user.Tip);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //Get the details of a particular employee
        public User GetUserData(int id)
        {
            try
            {
                User user = new User();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM Korisnik WHERE id_korisnik= " + id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user.ID = Convert.ToInt32(rdr["id_korisnik"]);
                        user.Ime = rdr["ime"].ToString();
                        user.Prezime = rdr["prezime"].ToString();
                        user.Email = rdr["email"].ToString();
                        user.Username = rdr["username"].ToString();
                        user.Password = rdr["password"].ToString();
                        user.Tip = rdr["tip"].ToString();
                    }
                }
                return user;
            }
            catch
            {
                throw;
            }
        }
        //To Delete the record on a particular employee
        public int DeleteUser(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_korisnik", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
} 