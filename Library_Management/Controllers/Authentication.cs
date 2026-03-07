using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Security.Cryptography.X509Certificates;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Data;
using MySqlDataAdapter = MySql.Data.MySqlClient.MySqlDataAdapter;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI.Common;
using Microsoft.Win32;

namespace Library_Management.Controllers
{
    public class Authentication : Controller
    {
        string _connectionString = "Server=127.0.0.1;Port=3306;Database=library_management;User Id=root;Password=qwer@123;";
        //public Authentication(IConfiguration iconfiguration)
        //{
        //    _connectionString = iconfiguration.GetConnectionString("ConnectionStrings:DefaultConnection");
        //}
        public IActionResult login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult login(Login login)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var cmd = new MySqlCommand("sp_authentication", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_flag", "Login");
                        cmd.Parameters.AddWithValue("_username", login.Username);
                        cmd.Parameters.AddWithValue("_password", login.Password);
                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(dt.Rows[0]["msg"]) == "Success")
                            {
                                HttpContext.Session.SetString("Username", Convert.ToString(dt.Rows[0]["username"]).ToUpper());
                                HttpContext.Session.SetString("Role", Convert.ToString(dt.Rows[0]["role"]));
                                HttpContext.Session.SetString("ERP_no", Convert.ToString(dt.Rows[0]["ERP_no"]));
                                return RedirectToAction("Index", "Home");
                            }
                            else if(Convert.ToString(dt.Rows[0]["msg"]) == "User has been deactivated kindly contact admin")
                            {
                                ModelState.AddModelError("Password", "User has been deactivated kindly contact admin");
                                return View(login);
                            }
                            else
                            {
                                ModelState.AddModelError("Password", "Username and password does not matched");
                                return View(login);
                            }
                        }
                        else
                        {

                        }
                    }

                }
                return View();
            }
            else
            {
                return View(login);
            }
        }


        public IActionResult logout()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register_Load(string search,string pagesize,string pageno)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("sp_viewRegister", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_search", search);
                    cmd.Parameters.AddWithValue("_pagesize", pagesize);
                    cmd.Parameters.AddWithValue("_pageno", pageno);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        var result = new Dictionary<string, object>();
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            result[$"Table{i + 1}"] = ConvertDataTableToList(ds.Tables[i]);
                        }

                        return Json(result);
                    }
                }
            }
                    return null;
        }
        private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dt)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            return list;
        }

        public IActionResult AddRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRegister(Register register)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var cmd = new MySqlCommand("sp_Register", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_flag", "Add");
                        cmd.Parameters.AddWithValue("_id", "");
                        cmd.Parameters.AddWithValue("_firstname", register.Firstname);
                        cmd.Parameters.AddWithValue("_lastname", register.Lastname);
                        cmd.Parameters.AddWithValue("_username", register.Username);
                        cmd.Parameters.AddWithValue("_emailid", register.Email);
                        cmd.Parameters.AddWithValue("_password", register.Password);
                        cmd.Parameters.AddWithValue("_role", register.Role_id);
                        cmd.Parameters.AddWithValue("_mobile_no", register.Mobile);
                        cmd.Parameters.AddWithValue("_ERP_no", register.ERP_no);
                        cmd.Parameters.AddWithValue("_isactive", 0);
                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ModelState.AddModelError("ERP_no", Convert.ToString(dt.Rows[0]["msg"]));
                        }
                        else
                        {
                            return RedirectToAction("Register", "Authentication");
                        }
                    }
                }
            }
            return View(register);
        }

        [HttpGet]
        public IActionResult Edit(int id,Register register)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("sp_Register", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_flag", "EditView");
                    cmd.Parameters.AddWithValue("_id", id);
                    cmd.Parameters.AddWithValue("_firstname", "");
                    cmd.Parameters.AddWithValue("_lastname", "");
                    cmd.Parameters.AddWithValue("_username", "");
                    cmd.Parameters.AddWithValue("_emailid", "");
                    cmd.Parameters.AddWithValue("_password", "");
                    cmd.Parameters.AddWithValue("_role", 0);
                    cmd.Parameters.AddWithValue("_mobile_no", "");
                    cmd.Parameters.AddWithValue("_ERP_no", "");
                    cmd.Parameters.AddWithValue("_isactive", 0);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        register.Id = Convert.ToInt32(dt.Rows[0]["id"]);
                        register.Firstname = Convert.ToString(dt.Rows[0]["firstname"]);
                        register.Lastname = Convert.ToString(dt.Rows[0]["lastname"]);
                        register.Username = Convert.ToString(dt.Rows[0]["username"]);
                        register.Email = Convert.ToString(dt.Rows[0]["emailid"]);
                        register.Mobile = Convert.ToString(dt.Rows[0]["mobile_no"]);
                        register.ERP_no = Convert.ToString(dt.Rows[0]["ERP_no"]);
                        register.Password = Convert.ToString(dt.Rows[0]["password"]);
                        register.Role_id = Convert.ToInt32(dt.Rows[0]["role"]);
                        register.isActived = Convert.ToBoolean(dt.Rows[0]["isactive"]);
                        ModelState.Clear();
                        return View(register);
                    }
                }
            }
            return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        public IActionResult Edit(Register register)
        {
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var cmd = new MySqlCommand("sp_Register", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_flag", "Edit");
                        cmd.Parameters.AddWithValue("_id", register.Id);
                        cmd.Parameters.AddWithValue("_firstname", register.Firstname);
                        cmd.Parameters.AddWithValue("_lastname", register.Lastname);
                        cmd.Parameters.AddWithValue("_username", register.Username);
                        cmd.Parameters.AddWithValue("_emailid", register.Email);
                        cmd.Parameters.AddWithValue("_password", register.Password);
                        cmd.Parameters.AddWithValue("_role", register.Role_id);
                        cmd.Parameters.AddWithValue("_mobile_no", register.Mobile);
                        cmd.Parameters.AddWithValue("_ERP_no", register.ERP_no);
                        cmd.Parameters.AddWithValue("_isactive", register.isActived);
                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ModelState.AddModelError("ERP_no", Convert.ToString(dt.Rows[0]["msg"]));
                        }
                        else
                        {
                            return RedirectToAction("Register", "Authentication");
                        }
                    }
                }
            }
            return View(register);
        }

        [HttpPost]
        public string Delete(string id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("sp_Register", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_flag", "Delete");
                    cmd.Parameters.AddWithValue("_id", id);
                    cmd.Parameters.AddWithValue("_firstname", "");
                    cmd.Parameters.AddWithValue("_lastname", "");
                    cmd.Parameters.AddWithValue("_username", "");
                    cmd.Parameters.AddWithValue("_emailid", "");
                    cmd.Parameters.AddWithValue("_password", "");
                    cmd.Parameters.AddWithValue("_role", 0);
                    cmd.Parameters.AddWithValue("_mobile_no", "");
                    cmd.Parameters.AddWithValue("_ERP_no", "");
                    cmd.Parameters.AddWithValue("_isactive", 0);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        return null;
                    }
                }
            }
            return null;
        }
    }
}

