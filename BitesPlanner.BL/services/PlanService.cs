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
            if (plan == null) throw new ApplicationException("plan cannot be null");

            await ValidatePlan(plan);

            foreach (var item in plan.PlanItems)
            {
                item.Meal = await _mealRepository.GetMealByIdAsync(item.MealId);
            }
            plan.TotalCalories = plan.PlanItems.Sum(item => item.Meal.Calories * item.Quantity);

            await _planRepository.AddPlanAsync(plan);
        }
        public async Task UpdatePlanAsync(Plan plan)
        {
            if (plan == null)  throw new ApplicationException("plan cannot be null");

            var dbPlan = await _planRepository.GetPlanById(plan.Id);
            if(dbPlan==null) throw new ApplicationException("Couldn't find plan with the provided id");

            await ValidatePlan(plan);

            dbPlan.Name = plan.Name;
            dbPlan.PlanItems = plan.PlanItems;


            foreach (var item in plan.PlanItems)
            {
                item.Meal = await _mealRepository.GetMealByIdAsync(item.MealId);
            }

            plan.TotalCalories = plan.PlanItems.Sum(item => item.Meal.Calories * item.Quantity);

            await _planRepository.UpdatePlanAsync(plan);
        }
        public async Task DeletePlanAsync(int id)
        {
            await _planRepository.DeletePlanAsync(id);
        }

        private async Task ValidatePlan(Plan plan)
        {
            var existingPlan = await _planRepository.GetPlanByNameAsync(plan.Name);
            if (existingPlan != null && existingPlan.Id != plan.Id) throw new ApplicationException($"A plan with the name '{plan.Name}' already exists.");

            if (plan == null) throw new ApplicationException("Plan cannot be null");
            if (string.IsNullOrWhiteSpace(plan.Name))  throw new ApplicationException("Plan name cannot be empty");
            if (plan.PlanItems == null || !plan.PlanItems.Any()) throw new ApplicationException("Plan must have at least one meal item");

            foreach (var item in plan.PlanItems)
            {
                if (item.Quantity <= 0) throw new ApplicationException("Quantity must be greater than zero");
            }
        }
    }
}
