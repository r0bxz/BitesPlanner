using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitesPlanner.Data.entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

       // public  ICollection<Meal> Meals { get; set; } = new List<Meal>();
    }
}
