
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PoliticList.Models
{
    [Table("Voters")]

    public class Voter
    {
        [Key]
        public int VoterId { get; set; }
        public string VoterIPAddress { get; set; }
        public int ArticleSegmentId { get; set; }
    }
}
