using PoliticList.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Data.Entity;

namespace PoliticList.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult CategoryPage(string slug)
        {
            ViewBag.TempLinks = db.FeedLinks.OrderByDescending(x => x.FeedLinkTime).Where(x => x.FeedLinkTime >= DateTime.Today).Where(x => x.Category.CategorySlug == slug).ToList();

            ViewBag.VoteLinks = db.FeedLinks.OrderByDescending(x => x.Votes).ThenByDescending(x => x.FeedLinkTime).Where(x => x.Votes >= 3).Where(x => x.FeedLinkTime >= DateTime.Today).Where(x => x.Category.CategorySlug == slug).ToList();
            var thisCategory = db.Categories.FirstOrDefault(x => x.CategorySlug == slug);
            return View(thisCategory);

        }

        public ActionResult Comment(string Post, int FeedLinkId, string slug)
        {
            var newComment = new Comment();
            newComment.Post = Post;
            newComment.FeedLinkId = FeedLinkId;
            newComment.Votes = 0;
            newComment.IpAddress = GetUserIP();

            db.Comments.Add(newComment);
            db.SaveChanges();

            return Content(Post, "text/plain");

            //return RedirectToAction("CategoryPage", "Category", new { slug = slug });
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

        public ActionResult CommentUpvote(int CommentId)
        {
            string thisIp = GetUserIP();
            var thisComment = db.Comments.FirstOrDefault(x => x.CommentId == CommentId);


            var alreadyVoted = db.Voters.FirstOrDefault(voter => voter.VoterIPAddress == thisIp && voter.CommentId == thisComment.CommentId);


            if (alreadyVoted == null || alreadyVoted.VoterIPAddress == "::1")
            {
                var newVote = new Voter();
                newVote.VoterIPAddress = thisIp;
                newVote.CommentId = CommentId;
                db.Voters.Add(newVote);


                thisComment.Votes = thisComment.Votes + 1;
                db.SaveChanges();
            }

            //var thisTopic = db.Topics.FirstOrDefault(topic => topic.TopicId == thisLink.FeedLinkId);

            return Content(thisComment.Votes.ToString(), "text/plain");

        }
        public string AlreadyVotedComment(int CommentId)
        {
            string thisIp = GetUserIP();
            var thisComment = db.Comments.FirstOrDefault(x => x.CommentId == CommentId);


            var alreadyVoted = db.Voters.FirstOrDefault(voter => voter.VoterIPAddress == thisIp && voter.CommentId == CommentId);

            if (alreadyVoted == null)
            {
                return "false";

            }
            return "true";
        }

    }
}