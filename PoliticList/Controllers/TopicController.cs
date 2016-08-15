using PoliticList.Models;
using System.Web.Mvc;
using System.Linq;
using System;

namespace PoliticList.Controllers
{

    public class TopicController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Topic
        public ActionResult Topic(string slug)
        {
            var topic = db.Topics.FirstOrDefault(x => x.TopicSlug == slug);

            return View("Topic", "Home", topic);
        }

        public ActionResult EditTopic(string TopicName, string TopicSlug, string Intro, int TopicId, string YouTube, string TopicPic)
        {
            var Topic = db.Topics.FirstOrDefault(x => x.TopicId == TopicId);

            Topic.TopicName = TopicName;
            Topic.TopicSlug = TopicSlug;
            Topic.TopicPic = TopicPic;

            Topic.YouTube = YouTube;
            Topic.Intro = Intro;
            db.SaveChanges();

            return RedirectToAction("Topic", "Home", new { slug = Topic.TopicSlug });
        }

        [ValidateInput(false)]
        public ActionResult AddLink(string LinkTitle, string LinkUrl, int TopicId, string LinkTwitter, string LinkImage, string LinkInstagram, string LinkQuote)
        {
            var newLink = new Link();
            newLink.LinkTitle = LinkTitle;
            newLink.LinkUrl = LinkUrl;
            newLink.TopicId = TopicId;
            newLink.LinkTwitter = LinkTwitter;
            newLink.LinkImage = LinkImage;
            newLink.LinkInstagram = LinkInstagram;
            newLink.LinkQuote = LinkQuote;


            db.Links.Add(newLink);
            db.SaveChanges();


            var thisTopic = db.Topics.FirstOrDefault(topic => topic.TopicId == newLink.TopicId);

            return RedirectToAction("Topic", "Home", new { slug = thisTopic.TopicSlug });
        }

        public ActionResult AddNewsLink(string LinkTitle, string LinkUrl, string LinkSource, int TopicId)
        {
            var newLink = new NewsLink();
            newLink.NewsLinkTitle = LinkTitle;
            newLink.NewsLinkUrl = LinkUrl;
            newLink.NewsLinkSource = LinkSource;

            newLink.TopicId = TopicId;
            db.NewsLinks.Add(newLink);
            db.SaveChanges();


            var thisTopic = db.Topics.FirstOrDefault(topic => topic.TopicId == newLink.TopicId);

            return RedirectToAction("Topic", "Home", new { slug = thisTopic.TopicSlug });
        }



        [ValidateInput(false)]
        public ActionResult EditLink(string LinkTitle, string LinkUrl, int LinkId, string LinkTwitter, string LinkImage, string LinkInstagram, string LinkQuote)
        {
            var newLink = db.Links.FirstOrDefault(x => x.LinkId == LinkId);
            newLink.LinkTitle = LinkTitle;
            newLink.LinkUrl = LinkUrl;
            newLink.LinkTwitter = LinkTwitter;
            newLink.LinkImage = LinkImage;
            newLink.LinkInstagram = LinkInstagram;
            newLink.LinkQuote = LinkQuote;

            db.SaveChanges();


            var thisTopic = db.Topics.FirstOrDefault(topic => topic.TopicId == newLink.TopicId);

            return RedirectToAction("Topic", "Home", new { slug = thisTopic.TopicSlug });
        }
        private string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
        public ActionResult UpVote(int LinkId)
        {
            string thisIp = GetUserIP();
            var thisLink = db.Links.FirstOrDefault(x => x.LinkId == LinkId);


            var alreadyVoted = db.Voters.FirstOrDefault(voter => voter.VoterIPAddress == thisIp && voter.ArticleSegmentId == thisLink.LinkId);


            if (alreadyVoted == null || alreadyVoted.VoterIPAddress == "::1")
            {
                var newVote = new Voter();
                newVote.VoterIPAddress = thisIp;
                newVote.ArticleSegmentId = LinkId;
                db.Voters.Add(newVote);

         
                thisLink.Votes = thisLink.Votes + 1;
                db.SaveChanges();
            }

            var thisTopic = db.Topics.FirstOrDefault(topic => topic.TopicId == thisLink.TopicId);

            return RedirectToAction("Topic", "Home", new { slug = thisTopic.TopicSlug });
        }

        public ActionResult DeleteLink(int LinkId)
        {

            

            var thisLink = db.Links.FirstOrDefault(x => x.LinkId == LinkId);

            db.Links.Remove(thisLink);
            db.SaveChanges();

            var thisTopic = db.Topics.FirstOrDefault(topic => topic.TopicId == thisLink.TopicId);

            return RedirectToAction("Topic", "Home", new { slug = thisTopic.TopicSlug });
        }

        public ActionResult DeleteNewsLink(int NewsLinkId)
        {

        

            var thisLink = db.NewsLinks.FirstOrDefault(x => x.NewsLinkId == NewsLinkId);

            db.NewsLinks.Remove(thisLink);
            db.SaveChanges();

            var thisTopic = db.Topics.FirstOrDefault(topic => topic.TopicId == thisLink.TopicId);

            return RedirectToAction("Topic", "Home", new { slug = thisTopic.TopicSlug });
        }

    }
}