using Microsoft.EntityFrameworkCore;

namespace FlexRule.Models
{
    public class FlexRuleContext: DbContext
    {
        public FlexRuleContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<GlossaryTerm> GlossaryTerms { get; set; }
    }
}
