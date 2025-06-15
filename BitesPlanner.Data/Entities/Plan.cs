using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitesPlanner.Data.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double TotalCalories { get; set; }
        public ICollection<PlanItem>? PlanItems { get; set; }

    }
}
