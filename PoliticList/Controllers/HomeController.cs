using PoliticList.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliticList.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var topics = db.Topics.ToList();
            return View(topics);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddTopic()
        {

            var newTopic = new Topic();
            newTopic.TopicName = "exampleName";
            db.Topics.Add(newTopic);
            db.SaveChanges();

            return RedirectToAction("Index"); 
        }

        public ActionResult Topic(int id)
        {
            var topic = db.Topics.FirstOrDefault(x => x.TopicId == id);

            ViewBag.Links = db.Links.Where(x => x.TopicId == topic.TopicId).OrderByDescending(x => x.Votes);

            return View("Topic", topic);
        }
    }
}