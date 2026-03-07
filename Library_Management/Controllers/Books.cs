using Google.Protobuf.WellKnownTypes;
using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Library_Management.Controllers
{
    public class Books : Controller
    {
        string _connectionString = "Server=127.0.0.1;Database=library_management;User Id=root;Password=1234;";

        public IActionResult Index(dropdownstudent dropdownstudent)
        {
            dropdownstudent.Items = GetItems();
            return View(dropdownstudent);
        }

        public IActionResult RetrunBook()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RetrunBookLoad(string search, string pagesize, string pageno)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("sp_viewRetrunBook", connection))
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

        [HttpPost]
        public JsonResult Books_Load(string search, string pagesize, string pageno)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("sp_books", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_role", HttpContext.Session.GetString("Role"));
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

        // You can retrieve your data from a database, here it's just a sample list
        private List<SelectListItem> GetItems()
        {
            List<SelectListItem> student = new List<SelectListItem>
    {
        // Add the default item with an empty value for the placeholder
        new SelectListItem { Value = "", Text = "--Select Dropdown--" }
    };
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("Get_dropdown", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_flag", "student");
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        student.Add(new SelectListItem
                        {
                            Value = dr["ERP_no"].ToString(),   // Correct way to access DataRow
                            Text = dr["username"].ToString()   // Use the column names as strings
                        });
                    }
                }
            }
            return student;

        }

        [HttpPost]
        public string BoorowBook(string ERP_no, string ISBN)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("sp_transaction", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_flag", "Boorow");
                    cmd.Parameters.AddWithValue("_ERP_no",ERP_no);
                    cmd.Parameters.AddWithValue("_ISBN",ISBN);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        return Convert.ToString(dt.Rows[0]["msg"]);
                    }
                }
            }
            return null;
        } 
        
        [HttpPost]
        public string RetrunBook(string id, string ISBN)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("sp_transaction", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("_flag", "retrunbook");
                    cmd.Parameters.AddWithValue("_ERP_no",id);
                    cmd.Parameters.AddWithValue("_ISBN",ISBN);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        return Convert.ToString(dt.Rows[0]["msg"]);
                    }
                }
            }
            return null;
        }
    }
}
