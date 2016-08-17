using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliticList.Models
{
    [Table("Links")]

    public class Link
    {

        [Key]
    
        public int LinkId { get; set; }
        public string SectionTitle { get; set; }
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        public string LinkExplain { get; set; }
        public string LinkQuote { get; set; }
        public string LinkTwitter { get; set; }
        public string LinkInstagram { get; set; }
        public string LinkImage { get; set; }
        

        public bool Published { get; set; }
        public int PosterIp { get; set; }
        public string newPosterIp { get; set; }

        public int Votes { get; set; }

        [ForeignKey("Topic")]
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }


    }
}