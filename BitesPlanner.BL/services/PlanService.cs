using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitesPlanner.Data.Entities;
using BitesPlanner.Data.Repositories;

namespace BitesPlanner.BL.services
{
    public class PlanService
    {
        private readonly PlanRepository _planRepository;
        private readonly MealRepository _mealRepository;

        public PlanService(PlanRepository planRepository, MealRepository mealRepository)
        {
            _planRepository = planRepository;
            _mealRepository = mealRepository;
        }

        public async Task<List<Plan>> GetAllPlansAsync()
        {
            return await _planRepository.GetAllPlansAsync();
        }

        public async Task<Plan> GetPlanByIdAsync(int id)
        {
            var plan = await _planRepository.GetPlanById(id);
            if (plan == null) throw new ApplicationException($"Plan with ID {id} not found");
            return plan;
        }

        public async Task AddPlanAsync(Plan plan)
        {
            if (plan == null) throw new ApplicationException("Plan cannot be null");

            await CalculatePlanNutritionAsync(plan);

            await ValidatePlan(plan);


            await _planRepository.AddPlanAsync(plan);
        }

        public async Task UpdatePlanAsync(Plan plan)
        {
            if (plan == null) throw new ApplicationException("Plan cannot be null");

            var existing = await _planRepository.GetPlanById(plan.Id);
            if (existing == null) throw new ApplicationException("Couldn't find plan with the provided id");

            await ValidatePlan(plan);
            await CalculatePlanNutritionAsync(plan);

            existing.Name = plan.Name;
            existing.Description = plan.Description;
            existing.assignedUserId = plan.assignedUserId;
            existing.Calories = plan.Calories;
            existing.protein = plan.protein;
            existing.carbs = plan.carbs;
            existing.fats = plan.fats;
            existing.PlanItems = plan.PlanItems;

            await _planRepository.UpdatePlanAsync(existing);
        }

        public async Task DeletePlanAsync(int id)
        {
            await _planRepository.DeletePlanAsync(id);
        }

        private async Task ValidatePlan(Plan plan)
        {
            if (plan == null) throw new ApplicationException("Plan cannot be null");
            if (string.IsNullOrWhiteSpace(plan.Name)) throw new ApplicationException("Plan name cannot be empty");

            var existing = await _planRepository.GetPlanByNameAsync(plan.Name);
            if (existing != null && existing.Id != plan.Id)
                throw new ApplicationException($"A plan with the name '{plan.Name}' already exists.");
        }

        private async Task CalculatePlanNutritionAsync(Plan plan)
        {

            if (plan.PlanItems == null || !plan.PlanItems.Any())
            {
                plan.Calories = 0;
                plan.protein = 0;
                plan.carbs = 0;
                plan.fats = 0;
                return;
            }

            double totalCalories = 0;
            double totalProtein = 0;
            double totalCarb = 0;
            double totalFat = 0;

            foreach (var item in plan.PlanItems)
            {
                var meal = await _mealRepository.GetMealByIdAsync(item.MealId);
                if (meal == null) throw new ApplicationException($"Meal with ID {item.MealId} not found");

                totalCalories += meal.Calories * item.Quantity;
                totalProtein += meal.Protein * item.Quantity;
                totalCarb += meal.Carbs * item.Quantity;
                totalFat += meal.Fats * item.Quantity;


                item.Meal = null;
            }

            plan.Calories = totalCalories;
            plan.protein = totalProtein;
            plan.carbs = totalCarb;
            plan.fats = totalFat;
        }

    }
}
