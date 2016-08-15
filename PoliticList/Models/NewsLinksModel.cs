using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliticList.Models
{
    [Table("NewsLinks")]

    public class NewsLink
    {

        [Key]

        public int NewsLinkId { get; set; }
        public string NewsLinkTitle { get; set; }
        public string NewsLinkUrl { get; set; }
        public string NewsLinkSource { get; set; }


        [ForeignKey("Topic")]
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }


    }
}