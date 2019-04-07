using System.Threading.Tasks;

namespace FlexRule.Models.Repository
{
    public interface ITermRepository : IRepository<GlossaryTerm>
    {
        Task<GlossaryTerm> FindByIdTerm(string term);
    }
}
