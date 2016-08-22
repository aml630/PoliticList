using PoliticList.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.ServiceModel.Syndication;



namespace PoliticList.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

    
            ViewBag.TempLinks = db.FeedLinks.OrderByDescending(x => x.FeedLinkTime).Include(y=>y.Category).Where(x=>x.FeedLinkTime>=DateTime.Today).ToList();
            ViewBag.VoteLinks = db.FeedLinks.OrderByDescending(x => x.Votes).ThenByDescending(x => x.FeedLinkTime).Where(x=> x.Votes >= 3).Where(x => x.FeedLinkTime >= DateTime.Today).ToList();


            var topics = db.Topics.ToList();
            return View(topics);
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

        public string AlreadyVoted(int FeedLinkId)
        {
            string thisIp = GetUserIP();
            var thisFeedLink = db.FeedLinks.FirstOrDefault(x => x.FeedLinkId == FeedLinkId);


            var alreadyVoted = db.Voters.FirstOrDefault(voter => voter.VoterIPAddress == thisIp && voter.FeedLinkId == FeedLinkId);

            if (alreadyVoted == null)
            {
                return "false";

            }
            return "true";
        }

        public ActionResult StoryUpvote(int FeedLinkId)
        {
            string thisIp = GetUserIP();
            var thisFeedLink = db.FeedLinks.FirstOrDefault(x => x.FeedLinkId == FeedLinkId);


            var alreadyVoted = db.Voters.FirstOrDefault(voter => voter.VoterIPAddress == thisIp && voter.FeedLinkId == FeedLinkId);


            if (alreadyVoted == null || alreadyVoted.VoterIPAddress == "::1")
            {
                var newVote = new Voter();
                newVote.VoterIPAddress = thisIp;
                newVote.FeedLinkId = FeedLinkId;
                db.Voters.Add(newVote);


                thisFeedLink.Votes = thisFeedLink.Votes + 1;
                db.SaveChanges();
            }

            //return RedirectToAction("Index");
            return Content(thisFeedLink.Votes.ToString(), "text/plain");


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

        public ActionResult Scrape()
        {
            Random rnd = new Random();
            string url = "http://fivethirtyeight.com/all/feed";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                var alreadyGotIt = db.FeedLinks.FirstOrDefault(x => x.FeedLinkTitle == item.Title.Text);

                if (alreadyGotIt == null)
                {
                    var tempLink = new FeedLink();

                    tempLink.FeedLinkTitle = item.Title.Text;
                    if (tempLink.FeedLinkTitle.Contains("Clinton"))
                    {
                        tempLink.CategoryId = 5;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Trump"))
                    {
                        tempLink.CategoryId = 6;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Police"))
                    {
                        tempLink.CategoryId = 7;
                    }
                    else
                    {
                        tempLink.CategoryId = 4;
                    }


                    tempLink.FeedLinkTitle = item.Title.Text;

                    tempLink.FeedLinkPic = "https://s0.wp.com/wp-content/themes/vip/espn-fivethirtyeight/assets/images/logo-fox-head-color.svg";
                    tempLink.FeedLinkTime = item.PublishDate.DateTime;
                    tempLink.Votes = rnd.Next(0, 7);



                    tempLink.FeedLinkUrl = item.Links[0].Uri.AbsoluteUri;
                    db.FeedLinks.Add(tempLink);
                    db.SaveChanges();
                }


            }

            string urlVox = "http://www.vox.com/rss/index.xml";
            XmlReader readerVox = XmlReader.Create(urlVox);
            SyndicationFeed feedVox = SyndicationFeed.Load(readerVox);
            readerVox.Close();
            foreach (SyndicationItem item in feedVox.Items)
            {
                var alreadyGotIt = db.FeedLinks.FirstOrDefault(x => x.FeedLinkTitle == item.Title.Text);

                if (alreadyGotIt == null)
                {


                    var tempLink = new FeedLink();

                    tempLink.FeedLinkTitle = item.Title.Text;
                    if (tempLink.FeedLinkTitle.Contains("Clinton"))
                    {
                        tempLink.CategoryId = 5;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Trump"))
                    {
                        tempLink.CategoryId = 6;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Police"))
                    {
                        tempLink.CategoryId = 7;
                    }
                    else
                    {
                        tempLink.CategoryId = 4;
                    }

                    tempLink.FeedLinkTitle = item.Title.Text;
                    tempLink.FeedLinkPic = "https://upload.wikimedia.org/wikipedia/commons/e/e5/Vox_(website)_logo.jpg";
                    tempLink.FeedLinkTime = item.PublishDate.DateTime;
                    tempLink.Votes = rnd.Next(0, 7);



                    tempLink.FeedLinkUrl = item.Links[0].Uri.AbsoluteUri;
                    db.FeedLinks.Add(tempLink);
                    db.SaveChanges();
                }


            }

            string urlHuff = "http://www.huffingtonpost.com/feeds/verticals/politics/index.xml";
            XmlReader readerHuff = XmlReader.Create(urlHuff);
            SyndicationFeed feedHuff = SyndicationFeed.Load(readerHuff);
            readerHuff.Close();
            foreach (SyndicationItem item in feedHuff.Items)
            {
                var alreadyGotIt = db.FeedLinks.FirstOrDefault(x => x.FeedLinkTitle == item.Title.Text);

                if (alreadyGotIt == null)
                {
                    var tempLink = new FeedLink();

                    tempLink.FeedLinkTitle = item.Title.Text;
                    if (tempLink.FeedLinkTitle.Contains("Clinton"))
                    {
                        tempLink.CategoryId = 5;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Trump"))
                    {
                        tempLink.CategoryId = 6;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Police"))
                    {
                        tempLink.CategoryId = 7;
                    }
                    else
                    {
                        tempLink.CategoryId = 4;
                    }

                  

                    tempLink.FeedLinkTitle = item.Title.Text;
                    tempLink.FeedLinkPic = "https://pbs.twimg.com/profile_images/720642862551928832/I58EQMCH.jpg";
                    tempLink.FeedLinkTime = item.PublishDate.DateTime;
                    tempLink.Votes = rnd.Next(0, 7);



                    tempLink.FeedLinkUrl = item.Links[0].Uri.AbsoluteUri;
                    db.FeedLinks.Add(tempLink);
                    db.SaveChanges();
                }


            }
            //string urlThinkProgress = "https://thinkprogress.org/feed";
            //XmlReader readerThinkProgress = XmlReader.Create(urlThinkProgress);
            //SyndicationFeed feedThinkProgress = SyndicationFeed.Load(readerThinkProgress);
            //readerThinkProgress.Close();
            //foreach (SyndicationItem item in feedThinkProgress.Items)
            //{
            //    var alreadyGotIt = db.FeedLinks.FirstOrDefault(x => x.FeedLinkTitle == item.Title.Text);

            //    if (alreadyGotIt == null)
            //    {
            //        var tempLink = new FeedLink();
            //tempLink.FeedLinkTitle = item.Title.Text;
            //if (tempLink.FeedLinkTitle.Contains("Clinton"))
            //{
            //    tempLink.CategoryId = 5;
            //}
            //else if (tempLink.FeedLinkTitle.Contains("Trump"))
            //{
            //    tempLink.CategoryId = 6;
            //}
            //else
            //{
            //    tempLink.CategoryId = 4;
            //}

            //        tempLink.FeedLinkTitle = item.Title.Text;
            //        tempLink.FeedLinkPic = "http://www.underconsideration.com/brandnew/archives/thinkprogress_icon.png";
            //        tempLink.FeedLinkTime = item.PublishDate.DateTime;
            //        tempLink.Votes = rnd.Next(0, 7);



            //        tempLink.FeedLinkUrl = item.Links[0].Uri.AbsoluteUri;
            //        db.FeedLinks.Add(tempLink);
            //        db.SaveChanges();
            //    }
            //}

            string urlPolitifact = "http://www.politifact.com/feeds/articles/truth-o-meter/";
            XmlReader readerPolitifact = XmlReader.Create(urlPolitifact);
            SyndicationFeed feedPolitifact = SyndicationFeed.Load(readerPolitifact);
            readerPolitifact.Close();
            foreach (SyndicationItem item in feedPolitifact.Items)
            {
                var alreadyGotIt = db.FeedLinks.FirstOrDefault(x => x.FeedLinkTitle == item.Title.Text);

                if (alreadyGotIt == null)
                {
                    var tempLink = new FeedLink();

                    tempLink.FeedLinkTitle = item.Title.Text;
                    if (tempLink.FeedLinkTitle.Contains("Clinton"))
                    {
                        tempLink.CategoryId = 5;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Trump"))
                    {
                        tempLink.CategoryId = 6;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Police"))
                    {
                        tempLink.CategoryId = 7;
                    }
                    else
                    {
                        tempLink.CategoryId = 4;
                    }

                    tempLink.FeedLinkTitle = item.Title.Text;
                    tempLink.FeedLinkPic = "http://static.politifact.com/rulings/og/logo-meter.png";
                    tempLink.FeedLinkTime = item.PublishDate.DateTime;
                    tempLink.Votes = rnd.Next(0, 7);



                    tempLink.FeedLinkUrl = item.Links[0].Uri.AbsoluteUri;
                    db.FeedLinks.Add(tempLink);
                    db.SaveChanges();
                }
            }

            string urlSlate = "http://feeds.slate.com/slate";
            XmlReader readerSlate = XmlReader.Create(urlSlate);
            SyndicationFeed feedSlate = SyndicationFeed.Load(readerSlate);
            readerSlate.Close();
            foreach (SyndicationItem item in feedSlate.Items)
            {
                var alreadyGotIt = db.FeedLinks.FirstOrDefault(x => x.FeedLinkTitle == item.Title.Text);

                if (alreadyGotIt == null)
                {
                    var tempLink = new FeedLink();

                    tempLink.FeedLinkTitle = item.Title.Text;
                    if (tempLink.FeedLinkTitle.Contains("Clinton"))
                    {
                        tempLink.CategoryId = 5;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Trump"))
                    {
                        tempLink.CategoryId = 6;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Police"))
                    {
                        tempLink.CategoryId = 7;
                    }
                    else
                    {
                        tempLink.CategoryId = 4;
                    }

                    tempLink.FeedLinkTitle = item.Title.Text;
                    tempLink.FeedLinkPic = "http://pbs.twimg.com/profile_images/2151387107/slateLogo-S-facebookSquarex200_reasonably_small.jpg";
                    tempLink.FeedLinkTime = item.PublishDate.DateTime;
                    tempLink.Votes = rnd.Next(0, 7);



                    tempLink.FeedLinkUrl = item.Links[0].Uri.AbsoluteUri;
                    db.FeedLinks.Add(tempLink);
                    db.SaveChanges();
                }
            }

            string urlNR = "https://newrepublic.com/rss.xml";
            XmlReader readerNR = XmlReader.Create(urlNR);
            SyndicationFeed feedNR = SyndicationFeed.Load(readerNR);
            readerNR.Close();
            foreach (SyndicationItem item in feedNR.Items)
            {
                var alreadyGotIt = db.FeedLinks.FirstOrDefault(x => x.FeedLinkTitle == item.Title.Text);

                if (alreadyGotIt == null)
                {
                    var tempLink = new FeedLink();

                    tempLink.FeedLinkTitle = item.Title.Text;
                    if (tempLink.FeedLinkTitle.Contains("Clinton"))
                    {
                        tempLink.CategoryId = 5;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Trump"))
                    {
                        tempLink.CategoryId = 6;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Police"))
                    {
                        tempLink.CategoryId = 7;
                    }
                    else
                    {
                        tempLink.CategoryId = 4;
                    }

                    tempLink.FeedLinkTitle = item.Title.Text;
                    tempLink.FeedLinkPic = "http://www.pixelmonkey.org/wordpress/wp-content/uploads/2014/12/tnr_logo.jpg";
                    tempLink.FeedLinkTime = item.PublishDate.DateTime;
                    tempLink.Votes = rnd.Next(0, 7);



                    tempLink.FeedLinkUrl = item.Links[0].Uri.AbsoluteUri;
                    db.FeedLinks.Add(tempLink);
                    db.SaveChanges();
                }
            }
            string urlPolitico = "http://www.politico.com/rss/politicopicks.xml";
            XmlReader readerPolitico = XmlReader.Create(urlPolitico);
            SyndicationFeed feedPolitico = SyndicationFeed.Load(readerPolitico);
            readerPolitico.Close();
            foreach (SyndicationItem item in feedPolitico.Items)
            {
                var alreadyGotIt = db.FeedLinks.FirstOrDefault(x => x.FeedLinkTitle == item.Title.Text);

                if (alreadyGotIt == null & item.Id != "")
                {
                    var tempLink = new FeedLink();

                    tempLink.FeedLinkTitle = item.Title.Text;
                    if (tempLink.FeedLinkTitle.Contains("Clinton"))
                    {
                        tempLink.CategoryId = 5;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Trump"))
                    {
                        tempLink.CategoryId = 6;
                    }
                    else if (tempLink.FeedLinkTitle.Contains("Police"))
                    {
                        tempLink.CategoryId = 7;
                    }
                    else
                    {
                        tempLink.CategoryId = 4;
                    }

                    tempLink.FeedLinkTitle = item.Title.Text;
                    tempLink.FeedLinkPic = "https://opportunitynation.org/app/uploads/2014/09/Politico-Logo-News-e1425500998350.png";
                    if(item.PublishDate.DateTime.Year != 2016 )
                    {
                        tempLink.FeedLinkTime = DateTime.Now;
                    }else
                    {
                        tempLink.FeedLinkTime = item.PublishDate.DateTime;

                    }
                    tempLink.Votes = rnd.Next(0, 7);

                      tempLink.FeedLinkUrl = item.Links[0].Uri.AbsoluteUri;
                        db.FeedLinks.Add(tempLink);
                        db.SaveChanges();
                    
               

                }
            }

            return RedirectToAction("Index");

        }
        public ActionResult BlogList()
        {
            var topics = db.Topics.ToList();
            return View(topics);
        }

        public ActionResult ajaxVote()
        {

            return Content("Hello from the controller!", "text/plain");

        }
    }
}