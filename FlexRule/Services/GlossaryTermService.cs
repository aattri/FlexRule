using FlexRule.Contracts;
using FlexRule.Models;
using FlexRule.Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlexRule.Services
{
    public interface IGlossaryTermService
    {
        IEnumerable<GlossaryTerm> GetAll();
        Task<GlossaryTerm> FindByIdTerm(string term);
        Task<GlossaryTerm> Get(int id);

        Task<List<ValidationResult>> ValidateCreateRequest(CreateGlossaryTermRequest request);
        Task CreateGlossaryTerm(CreateGlossaryTermRequest request);

        Task DeleteGlossaryTerm(int id);
        Task<List<ValidationResult>> ValidateUpdateRequest(UpdateGlossaryTermRequest request, int termId);
        Task UpdateGlossaryTerm(UpdateGlossaryTermRequest request, int termId);
    }

    public class GlossaryTermService : IGlossaryTermService
    {
        private readonly ITermRepository _dataRepository;

        public GlossaryTermService(ITermRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IEnumerable<GlossaryTerm> GetAll()
        {
            return _dataRepository.GetAll();
        }

        public async Task<GlossaryTerm> FindByIdTerm(string term)
        {
            return await _dataRepository.FindByIdTerm(term);
        }

        public async Task<GlossaryTerm> Get(int id)
        {
            return await _dataRepository.Get(id);
        }

        public async Task<List<ValidationResult>> ValidateCreateRequest(CreateGlossaryTermRequest request)
        {
            var result = new List<ValidationResult>();

            if (request == null)
            {
                result.Add(new ValidationResult("Bad request."));
                return result;
            }

            if (string.IsNullOrEmpty(request.Term))
            {
                result.Add(new ValidationResult("Term can not be empty."));
                return result;
            }

            GlossaryTerm glossaryTerm = await _dataRepository.FindByIdTerm(request.Term);
            if (glossaryTerm != null)
            {
                result.Add(new ValidationResult("The term already exists."));
            }

            return result;
        }

        public async Task CreateGlossaryTerm(CreateGlossaryTermRequest request)
        {
            var term = new GlossaryTerm
            {
                Term = request.Term,
                Definition = request.Definition
            };

            await _dataRepository.Add(term);
        }

        public async Task DeleteGlossaryTerm(int id)
        {
            GlossaryTerm glossaryTerm = await _dataRepository.Get(id);

            if (glossaryTerm == null)
            {
                throw new Exception("The term record couldn't be found.");
            }

            await _dataRepository.Delete(glossaryTerm);
        }

        public async Task<List<ValidationResult>> ValidateUpdateRequest(UpdateGlossaryTermRequest request, int termId)
        {
            var result = new List<ValidationResult>();

            if (request == null)
            {
                result.Add(new ValidationResult("Bad request."));
                return result;
            }

            if (string.IsNullOrEmpty(request.Definition))
            {
                result.Add(new ValidationResult("Bad request."));
                return result;
            }

            GlossaryTerm glossaryTerm = await _dataRepository.Get(termId);
            if (glossaryTerm == null)
            {
                result.Add(new ValidationResult("The term record couldn't be found."));
            }

            return result;
        }

        public async Task UpdateGlossaryTerm(UpdateGlossaryTermRequest request, int termId)
        {
            GlossaryTerm glossaryTermToUpdate = await _dataRepository.Get(termId);

            var glossaryTerm = new GlossaryTerm
            {
                Definition = request.Definition
            };

            await _dataRepository.Update(glossaryTermToUpdate, glossaryTerm);
        }
    }
   
}
