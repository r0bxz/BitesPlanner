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
    public class PlanItemRepository
    {
        private readonly AppDbContext _context;
        public PlanItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPlanItemAsync(PlanItem planItem)
        {
            _context.PlanItems.Add(planItem);
            await _context.SaveChangesAsync();
        }
        public async  Task<List<PlanItem>> GetAllPlanItemsAsync()
        {
            return await _context.PlanItems.ToListAsync();
        }

        public async Task<PlanItem?> GetPlanItemByIdAsync(int id)
        {
            return await _context.PlanItems.FindAsync(id);
        }

        public async Task DeletePlanItemById(int id)
        {
            var planItem = await _context.PlanItems.FindAsync(id);
            if (planItem == null)
            {
                throw new KeyNotFoundException($"PlanItem with ID {id} not found");
            }
            _context.PlanItems.Remove(planItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlanItemAsync(PlanItem planItem)
        {
             await _context.SaveChangesAsync();
        }
    }
}
