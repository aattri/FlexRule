using System.Collections.Generic;
using System.Threading.Tasks;
using FlexRule.Contracts;
using FlexRule.Models;
using FlexRule.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexRule.Controllers
{
    [Route("api/GlossaryTerm")]
    [ApiController]
    public class GlossaryTermController : ControllerBase
    {
        private readonly IGlossaryTermService _termService;

        public GlossaryTermController(IGlossaryTermService termService)
        {
            _termService = termService;
        }

        [HttpGet("GetAll", Name = "GetAll")]
        public IActionResult Get()
        {
            IEnumerable<GlossaryTerm> glossaryTerms = _termService.GetAll();
            return Ok(glossaryTerms);
        }

        [HttpGet("{term}", Name = "GetByTerm")]
        public async Task<IActionResult> Get(string term)
        {
            GlossaryTerm glossaryTerm = await _termService.FindByIdTerm(term);

            if (glossaryTerm == null)
            {
                return NotFound("The glossary term record couldn't be found.");
            }

            return Ok(glossaryTerm);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGlossaryTermRequest glossaryTermRequest)
        {
            var result = await _termService.ValidateCreateRequest(glossaryTermRequest);

            if (result.Count > 0)
            {
                return BadRequest(result);
            }

            await _termService.CreateGlossaryTerm(glossaryTermRequest);

            return Ok();
        }

        [HttpPut("{termId}")]
        public async Task<IActionResult> Put(int termId, [FromBody] UpdateGlossaryTermRequest request)
        {
            var result = await _termService.ValidateUpdateRequest(request, termId);

            if (result.Count > 0)
            {
                return BadRequest(result);
            }

            await _termService.UpdateGlossaryTerm(request, termId);

            return Ok();
        }

        [HttpDelete("{termId}")]
        public async Task<IActionResult> Delete(int termId)
        {
            await _termService.DeleteGlossaryTerm(termId);

            return NoContent();
        }

    }

}
