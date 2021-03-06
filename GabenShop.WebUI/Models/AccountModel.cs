﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GabenShop.WebUI.Models
{
    public class AccountModel
    {
        private static readonly string conStr;
        private static readonly string[] roleNames = { null, "User", "Administrator" };

        static AccountModel()
        {
            conStr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
        }

        private AccountModel()
        {
        }

        public static AccountModel Instance { get; } = new AccountModel();

        public string[] GetRoles(string username)
        {
            using (var con = new MySqlConnection(conStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Role FROM users WHERE UserName=@UserName";
                cmd.Parameters.AddWithValue("@UserName", username);

                con.Open();
                var reader = cmd.ExecuteReader();

                List<int> roles = new List<int>();
                while (reader.Read())
                {
                    roles.Add((byte)reader["Role"]);
                }

                return roles.Select(r => roleNames[r]).ToArray();
            }
        }

        public bool IsInRole(string username, string roleName)
        {
            int roleId = Array.IndexOf(roleNames, roleName);

            using (var con = new MySqlConnection(conStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE UserName=@UserName AND Role=@Role LIMIT 1";
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Role", roleId);

                con.Open();
                return cmd.ExecuteReader().Read();
            }
        }

        public bool IsUserExists(string login, string password)
        {
            using (var con = new MySqlConnection(conStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE UserName=@UserName AND Password=@Password LIMIT 1";
                cmd.Parameters.AddWithValue("@UserName", login);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                return cmd.ExecuteReader().Read();
            }
        }

        public bool IsReadyToRegistration(string login)
        {
            using (var con = new MySqlConnection(conStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE UserName=@UserName LIMIT 1";
                cmd.Parameters.AddWithValue("@UserName", login);
                con.Open();
                return cmd.ExecuteReader().Read();
            }
        }

        public void Registration(string login, string password)
        {
            using (var con = new MySqlConnection(conStr))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO users (UserName, Password, Role) VALUES (?UserName, ?Password, 1)";
                cmd.Parameters.Add("?UserName", MySqlDbType.VarChar).Value = login;
                cmd.Parameters.Add("?Password", MySqlDbType.VarChar).Value = password;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}