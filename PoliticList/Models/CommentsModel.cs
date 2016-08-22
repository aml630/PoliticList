using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliticList.Models
{
    [Table("Comments")]

    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Post { get; set; }
        public string Author { get; set; }
        public string IpAddress { get; set; }
        public int Votes { get; set; }

        public int FeedLinkId { get; set; }
    }
 }