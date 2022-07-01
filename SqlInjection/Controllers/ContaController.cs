using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SqlInjection.Controllers
{
    public class ContaController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string password)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProdutosDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var request = "SELECT COUNT(*) FROM Users WHERE User = '" + user + "' AND Password = '" + password + "'";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(request, connection))
                    {
                        var result = (int)command.ExecuteScalar();
                        if (result > 0)
                            ViewBag.Mensagem = "Авторизация прошла успешно";
                        else
                            ViewBag.Mensagem = "Авторизация не удалась";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = "Ошибка: " + e.Message;
            }

            return View();

        }
    }
}