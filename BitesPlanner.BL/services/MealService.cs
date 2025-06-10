using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitesPlanner.Data.Entities;
using BitesPlanner.Data.Repositories;

namespace BitesPlanner.BL.services
{
    public class MealService
    {
        private readonly MealRepository _mealRepository;

        public MealService(MealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            return await _mealRepository.GetAllMealsAsync();
        }

        public async Task<Meal> GetMealByIdAsync(int id)
        {
            var meal = await _mealRepository.GetMealByIdAsync(id);
            if (meal == null)
            {
                throw new KeyNotFoundException($"Meal with ID {id} not found");
            }
            return meal;
        }

        public async Task AddMealAsync(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal), "meal cannot be null");
            }
            await _mealRepository.AddMealAsync(meal);
        }
        public async Task UpdateMealAsync(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal), "meal cannot be null");
            }
            await _mealRepository.UpdateMealAsync(meal);
        }
        public async Task DeleteMealAsync(int id)
        {
            await _mealRepository.DeleteMealAsync(id);
        }

    }
}
