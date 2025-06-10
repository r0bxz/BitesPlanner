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
    public class MealRepository
    {
        private readonly AppDbContext _context;
        public MealRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            return await _context.Meals.ToListAsync();
        }
        public async Task<Meal?> GetMealByIdAsync(int id)
        {
            return await _context.Meals.FindAsync(id);
        }

        public async Task AddMealAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateMealAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMealAsync(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                throw new KeyNotFoundException($"Meal with ID {id} no found");
            }
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
        }

    }
}
