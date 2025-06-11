using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitesPlanner.Data.BitesPlannerDbContext;
using BitesPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitesPlanner.Data.Repositories
{
    public class PlanRepository
    {
        private readonly AppDbContext _context;
        public PlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Plan>> GetAllPlansAsync()
        {
            return await _context.Plans
                         .Include(p => p.PlanItems)
                         .ThenInclude(pi => pi.Meal)
                         .ToListAsync();
        }

        public async Task<Plan?> GetPlanById(int id)
        {
            return await _context.Plans
                         .Include(p => p.PlanItems)
                         .ThenInclude(pi => pi.Meal)
                         .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPlanAsync (Plan plan)
        {
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlanAsync (Plan plan)
        {
            _context.Plans.Update(plan);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlanAsync(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                throw new KeyNotFoundException($"Plan with ID {id} no found");
            }
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
        }
    }
}
