using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Test.Models;

namespace WebApplication_Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            Model_Test model = new Model_Test();

            return View(model);
        }
        
        [HttpPost]
        public JsonResult Create(Model_Test model)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }
            DataTable table = new DataTable();
            using (SqlConnection connect = new SqlConnection())
            {
                string cs = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
                connect.ConnectionString = cs;

                SqlCommand command = new SqlCommand("Sp_Get", connect);

                command.Parameters.AddWithValue("@name", model.Name);
                command.Parameters.AddWithValue("@age", model.Age);
                command.CommandType = CommandType.StoredProcedure;

                

                
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = command;
                adapter.Fill(table);

               
            }

            return Json(table);
        }

        

        public JsonResult Get()
        {
            List<Model_Test> models = new List<Model_Test>();
            using (SqlConnection connect = new SqlConnection())
            {
                string cs = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
                connect.ConnectionString = cs;

                SqlCommand command = new SqlCommand("Select * from [dbo].[User]", connect);

                command.CommandType = CommandType.Text;
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model_Test model = new Model_Test
                        {
                            Name = reader.GetString(1),
                            Age = reader.GetInt32(2)
                        };
                        models.Add(model);
                    }
                }
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            Model_Test model = new Model_Test();
           
            return View(model);
        }
    }
}