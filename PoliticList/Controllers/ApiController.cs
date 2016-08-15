using PoliticList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;


namespace PoliticList.Controllers.Api
{

    public class ApiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[HttpGet]
        //public JsonResult Get()
        //{
        //    var topics = db.Topics.ToList();

        //    //string JSONString = string.Empty;
        //    //JSONString = JSONConvert.SerializeObject(topics);
          

        //    return Json(JSONString, JsonRequestBehavior.AllowGet);
        //}
    }
}