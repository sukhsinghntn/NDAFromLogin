using DynamicFormsApp.Server.Services;
using DynamicFormsApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicFormsApp.Server.Controllers
{
    [Route("api/forms")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly DynamicFormService _svc;
        public FormsController(DynamicFormService svc)
        {
            _svc = svc;
        }

        // POST /api/forms
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFormDto dto)
        {
            var newFormId = await _svc.CreateFormAsync(dto.Name, dto.Fields);
            return Ok(new { FormId = newFormId });
        }


        // GET /api/forms/{id}/responses
        [HttpGet("{id}/responses")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> GetResponses(int id)
        {
            var rows = await _svc.GetResponsesAsync(id);
            return Ok(rows);
        }


        // GET /api/forms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Form>> Get(int id)
        {
            var form = await _svc.GetFormAsync(id);
            return Ok(form);
        }
        // GET /api/forms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Form>>> GetAll()
        {
            var all = await _svc.GetAllFormsAsync();
            return Ok(all);
        }

        [HttpPost("api/forms")]
        public async Task<IActionResult> CreateForm([FromBody] CreateFormDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var formId = await _svc.CreateFormAsync(dto.Name, dto.Fields);
            return Ok(new { formId });
        }

        // POST /api/forms/{id}/responses
        [HttpPost("{id}/responses")]

        public async Task<IActionResult> Submit(int id, [FromBody] Dictionary<string, object> values)
        {
            try
            {

                await _svc.StoreResponseAsync(id, values);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Return full exception details in development
                return StatusCode(500, new
                {
                    Error = ex.Message,
                    Details = ex.ToString()
                });
            }
        }
    }
}
