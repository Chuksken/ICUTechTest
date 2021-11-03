using ICUTechTest.Web.Models;
using ICUTechTest.Web.Services;
using ICUTechTest.Web.TestServiceReference;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ICUTechTest.Web.Controllers
{
    public class HomeController : Controller
    {
        
        ICUTechClient iCUTechClient = new ICUTechClient();
        
        public ActionResult Details()
        {
            var user = Session["UserDetails"] as EntityDetailsModel;
            //ViewBag.Message = "Your contact page.";

            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Model is not valid");
            }
            string Ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var respObj = iCUTechClient.Login(user.UserName, user.Password, Ip);
            var message = JsonConvert.DeserializeObject<ResponseModel>(respObj);
            if (message.ResultCode == -1)
            {
                ViewBag.Message = message.ResultMessage;
                //return Login();
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var message1  = JsonConvert.DeserializeObject<EntityDetailsModel>(respObj);
                ViewBag.Message = "Successfully LoggedIn";
                
                Session["UserDetails"] = message1;
                return Json(new { Success = true}, JsonRequestBehavior.AllowGet);
                
            }

        }

    }
}