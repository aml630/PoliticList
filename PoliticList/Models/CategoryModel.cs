using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PoliticList.Models
{
    [Table("Categories")]

    public class Category
    {

        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public string CategoryPic { get; set; }

        public virtual ICollection<FeedLink> FeedLinks { get; set; }

    }

}

