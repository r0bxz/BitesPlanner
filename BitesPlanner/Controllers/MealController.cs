using BitesPlanner.BL.services;
using BitesPlanner.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitesPlanner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly MealService _mealService;
        public MealController(MealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meals = await _mealService.GetAllMealsAsync();
            return Ok(meals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return Ok(meal);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Meal meal)
        {
            await _mealService.AddMealAsync(meal);
            return Ok(meal);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Meal meal)
        {
            await _mealService.UpdateMealAsync(meal);
            return Ok(meal);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _mealService.DeleteMealAsync(id);
            return NoContent();
        }

    }
}
