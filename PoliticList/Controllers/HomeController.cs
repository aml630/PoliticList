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
            newTopic.TopicSlug = "ExampleSlug";

            newTopic.Date = DateTime.Today;
            db.Topics.Add(newTopic);
            db.SaveChanges();

            return RedirectToAction("Index"); 
        }
        public ActionResult DeleteTopic(int TopicId)
        {

            var thisTopic = db.Topics.FirstOrDefault(x => x.TopicId == TopicId);

            db.Topics.Remove(thisTopic);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult PublishTopic(int TopicId)
        {

            var thisTopic = db.Topics.FirstOrDefault(x => x.TopicId == TopicId);
            thisTopic.TopicPublished = true;
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult UnPublishTopic(int TopicId)
        {

            var thisTopic = db.Topics.FirstOrDefault(x => x.TopicId == TopicId);
            thisTopic.TopicPublished = false;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Topic(string slug)
        {
            var topic = db.Topics.FirstOrDefault(x => x.TopicSlug == slug);

            ViewBag.Links = db.Links.Where(x => x.TopicId == topic.TopicId).OrderByDescending(x => x.Votes);
            ViewBag.NewsLinks = db.NewsLinks.Where(x => x.TopicId == topic.TopicId);


            return View("Topic", topic);
        }
    }
}