using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliticList.Models
{
    [Table("FeedLinks")]

    public class FeedLink
    {

        public int FeedLinkId { get; set; }
        public string FeedLinkTitle { get; set; }
        public string FeedLinkUrl { get; set; }
        public DateTime FeedLinkTime { get; set; }
        public string FeedLinkPic { get; set; }
        public int Votes { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }



        public virtual ICollection<Comment> Comments { get; set; }
    }



}