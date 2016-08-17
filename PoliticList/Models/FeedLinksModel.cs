using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliticList.Models
{
    public class FeedLink
    {
        public int FeedLinkId { get; set; }
        public string FeedLinkTitle { get; set; }
        public string FeedLinkUrl { get; set; }
        public DateTime FeedLinkTime { get; set; }
        public string FeedLinkPic { get; set; }
        public int Votes { get; set; }
    }



}