using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PoliticList.Models
{
    [Table("Topics")]

    public class Topic
    {

        [Key]
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicSlug { get; set; }
        public string TopicPic { get; set; }
        public bool TopicPublished { get; set; }
        public string Intro { get; set; }
        public DateTime Date { get; set; }
        public string YouTube { get; set; }
        public int FbShares { get; set; }
        public int TwitShares { get; set; }

        public virtual ICollection<Link> Links { get; set; }


    }

}

