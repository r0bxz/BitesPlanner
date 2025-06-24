using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitesPlanner.Data.BitesPlannerDbContext;
using BitesPlanner.Data.Entities;
using BitesPlanner.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BitesPlanner.BL.services
{
    public class PlanItemService
    {
        private readonly PlanItemRepository _planItemRepository;
        private readonly MealRepository _mealRepository;
        private readonly PlanRepository _planRepository;

        public PlanItemService(PlanItemRepository planItemRepository, MealRepository mealRepository, PlanRepository planRepository)
        {
            _planItemRepository = planItemRepository;
            _mealRepository = mealRepository;
            _planRepository = planRepository;
        }

        public async Task AddPlanItemAsync(PlanItem planItem)
        {
            if (planItem == null)
                throw new ApplicationException("Plan Item cannot be null");

            var meal = await _mealRepository.GetMealByIdAsync(planItem.MealId);
            if (meal == null)
                throw new ApplicationException("Meal not found");

            var quantity = planItem.Quantity;

            planItem.Calories = meal.Calories * quantity;
            planItem.Protein = meal.Protein * quantity;
            planItem.Carbs = meal.Carbs * quantity;
            planItem.Fats = meal.Fats * quantity;

            await _planItemRepository.AddPlanItemAsync(planItem);

        }

        public async Task<List<PlanItem>> GetAllPlanItemsAsync()
        {
            return await _planItemRepository.GetAllPlanItemsAsync();
        }

        public async Task<PlanItem?> GetPlanItemByIdAsync(int id)
        {
            return await _planItemRepository.GetPlanItemByIdAsync(id);
        }

        public async Task DeletePlanItemById(int id)
        {
           await _planItemRepository.DeletePlanItemById(id);
        }

        public async Task updatePlanItem(PlanItem planItem)
        {
            if (planItem == null)
                throw new ApplicationException("Plan Item cannot be null");
            var existing = await _planItemRepository.GetPlanItemByIdAsync(planItem.Id);
            if (existing == null)
                throw new ApplicationException("Couldn't find plan item with the provided id");
            existing.MealId = planItem.MealId;
            existing.Quantity = planItem.Quantity;
            var meal = await _mealRepository.GetMealByIdAsync(planItem.MealId);
            if (meal == null)
                throw new ApplicationException("Meal not found");
            existing.Calories = meal.Calories * planItem.Quantity;
            existing.Protein = meal.Protein * planItem.Quantity;
            existing.Carbs = meal.Carbs * planItem.Quantity;
            existing.Fats = meal.Fats * planItem.Quantity;
            _planItemRepository.UpdatePlanItemAsync(existing).Wait();

        }
    }
}
