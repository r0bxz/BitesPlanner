using BitesPlanner.BL.services;
using BitesPlanner.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitesPlanner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly PlanService _planService;
        public PlanController(PlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _planService.GetAllPlansAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plan plan)
        {
            await _planService.AddPlanAsync(plan);
            return Ok(plan);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Plan plan)
        {
            await _planService.UpdatePlanAsync(plan);
            return Ok(plan);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _planService.DeletePlanAsync(id);
            return NoContent();
        }


    }
}
