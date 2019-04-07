using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexRule.Models
{
    public class GlossaryTerm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TermId { get; set; }
        [Required]
        public string Term { get; set; }

        public string Definition { get; set; }
    }

}
