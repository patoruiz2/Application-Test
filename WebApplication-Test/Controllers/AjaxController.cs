using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Test.Models;
using WebApplication_Test.Modules;

namespace WebApplication_Test.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ModulesDb modules = new ModulesDb();
        // GET: Ajax
        public ActionResult Index(Model_Test model)
        {
            
            return View(model);
        }

        [HttpPost]
        public JsonResult GetUser(Model_Test model)
        {

                if (model.Name != null && model.LastName != null)
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(modules.ParameterString("@name", model.Name));
                    parameters.Add(modules.ParameterString("@lastname", model.LastName));

                    var res = modules.UserGet
                        (
                        "Select * from [dbo].[Users] where @name = Name AND @lastname = LastName"
                        ,parameters
                        );
                    if(res.Id <= 0)
                    {
                        return Json(false);
                    }
                    return Json(res);
                }
                else
                {

                    return Json(false);
                }

           

        }
        public ActionResult ListUser()
        {
            List<Model_Test> petition;
            try
            {
                petition = modules.ListUserGet("Select * from [dbo].[Users]");
                
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
            return View(petition);
        }
    }
}