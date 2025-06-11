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

        public PlanService(PlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<List<Plan>> GetAllPlansAsync()
        {
            return await _planRepository.GetAllPlansAsync();
        }

        public async Task<Plan> GetPlanByIdAsync(int id)
        {
            var plan = await _planRepository.GetPlanById(id);
            if (plan == null)
            {
                throw new KeyNotFoundException($"Plan with ID {id} not found");
            }
            return plan;
        }

        public async Task AddPlanAsync(Plan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException(nameof(plan), "plan cannot be null");
            }
            await _planRepository.AddPlanAsync(plan);
        }
        public async Task UpdatePlanAsync(Plan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException(nameof(plan), "plan cannot be null");
            }
            await _planRepository.UpdatePlanAsync(plan);
        }
        public async Task DeletePlanAsync(int id)
        {
            await _planRepository.DeletePlanAsync(id);
        }
    }
}
