using Library_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;

namespace Library_Management.Controllers
{
    public class HomeController : Controller
    {
        string _connectionString = "Server=127.0.0.1;Database=library_management;User Id=root;Password=qwer@123;";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand("GetDashborad", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (HttpContext.Session.GetString("Role") == "4")
                    {
                        cmd.Parameters.AddWithValue("_ERP_no", HttpContext.Session.GetString("ERP_no"));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("_ERP_no", "");
                    }
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataSet dt = new DataSet();
                    adp.Fill(dt);
                    ViewBag.TotalBooks = Convert.ToString(dt.Tables[0].Rows[0][0]);
                    ViewBag.IssueBooks = Convert.ToString(dt.Tables[1].Rows[0][0]);
                    ViewBag.ReturnBooks = Convert.ToString(dt.Tables[2].Rows[0][0]);
                    ViewBag.UniqueBooks = Convert.ToString(dt.Tables[3].Rows[0][0]);
                }
            }
                        return View();
        }
        public IActionResult Books()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}