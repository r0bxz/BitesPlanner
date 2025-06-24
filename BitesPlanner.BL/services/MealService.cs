using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitesPlanner.Data.entities;
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
            if (meal == null) throw new ApplicationException($"Meal with ID {id} not found");
            return meal;
        }

        public async Task AddMealAsync(Meal meal)
        {
            if (meal == null) throw new ApplicationException("Meal cannot be null");

            await ValidateMeal(meal);
    
            await _mealRepository.AddMealAsync(meal);
        }
        public async Task UpdateMealAsync(Meal meal)
        {
            if (meal == null) throw new ApplicationException("meal cannot be null");

            var dbMeal = await _mealRepository.GetMealByIdAsync(meal.Id);
            if(dbMeal == null) throw new ApplicationException("Couldn't find meal with the provided id");

            await ValidateMeal(meal);

            dbMeal.Name = meal.Name;
            dbMeal.Description = meal.Description;
            dbMeal.Calories = meal.Calories;
            dbMeal.Carb = meal.Carb;
            dbMeal.Fat = meal.Fat;
            dbMeal.Protein = meal.Protein;
            dbMeal.CategoryId = meal.CategoryId;

            await _mealRepository.UpdateMealAsync(dbMeal);

        }
        public async Task DeleteMealAsync(int id)
        {
            await _mealRepository.DeleteMealAsync(id);
        }

        private async Task ValidateMeal(Meal meal)
        {
            var existing = await _mealRepository.GetMealByNameAsync(meal.Name);
            if (existing != null && existing.Id != meal.Id) throw new ApplicationException($"A meal with the name '{meal.Name}' already exists.");

            if (String.IsNullOrWhiteSpace(meal.Name)) throw new ApplicationException("The name of the meal cannot be empty");
            if (meal.Name.Length > 20) throw new ArgumentException("Meal name must not exceed 20 characters.");
            if (!string.IsNullOrWhiteSpace(meal.Description) && meal.Description.Length > 250) throw new ApplicationException("Meal description must not exceed 250 characters.");
            if (meal.Calories < 0) throw new ApplicationException("Calories cannot be negative.");
            if (meal.Carb < 0) throw new ApplicationException("Carbohydrates cannot be negative.");
            if (meal.Fat < 0) throw new ApplicationException("Fat cannot be negative.");
            if (meal.Protein < 0) throw new ApplicationException("Protein cannot be negative.");
        }

    }
}
