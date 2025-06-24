using BitesPlanner.BL.services;
using BitesPlanner.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitesPlanner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanItemController : ControllerBase
    {
        private readonly PlanItemService _planItemService;

        public PlanItemController(PlanItemService planItemService)
        {
            _planItemService = planItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var planItems = await _planItemService.GetAllPlanItemsAsync();
            return Ok(planItems);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var planItem = await _planItemService.GetPlanItemByIdAsync(id);
            if (planItem == null) return NotFound($"Plan Item with ID {id} not found");
            return Ok(planItem);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PlanItem planItem)
        {
            await _planItemService.updatePlanItem(planItem);
            return Ok(planItem);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanItem planItem)
        {
            await _planItemService.AddPlanItemAsync(planItem);
            return Ok(planItem);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _planItemService.DeletePlanItemById(id);
            return NoContent();
        }
    }
}
