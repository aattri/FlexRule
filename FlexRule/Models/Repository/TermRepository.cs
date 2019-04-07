using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlexRule.Models.Repository
{
    public class TermRepository : ITermRepository
    {
        readonly FlexRuleContext _flexRuleContext;
        public TermRepository(FlexRuleContext context)
        {
            _flexRuleContext = context;
        }

        public async Task Add(GlossaryTerm entity)
        {
            await _flexRuleContext.GlossaryTerms.AddAsync(entity);
            await _flexRuleContext.SaveChangesAsync();
        }

        public async Task Delete(GlossaryTerm entity)
        {
            _flexRuleContext.GlossaryTerms.Remove(entity);
            await _flexRuleContext.SaveChangesAsync();
        }

        public async Task<GlossaryTerm> FindByIdTerm(string term)
        {
            return await _flexRuleContext.GlossaryTerms.FirstOrDefaultAsync(t => t.Term == term);
        }

        public async Task<GlossaryTerm> Get(int id)
        {
            return await _flexRuleContext.GlossaryTerms.FirstOrDefaultAsync(t => t.TermId == id);
        }

        public IEnumerable<GlossaryTerm> GetAll()
        {
            return _flexRuleContext.GlossaryTerms.OrderBy(t => t.Term).ToList();
        }

        public async Task Update(GlossaryTerm dbEntity, GlossaryTerm entity)
        {
            dbEntity.Definition = entity.Definition;

            await _flexRuleContext.SaveChangesAsync();
        }
    }
}
