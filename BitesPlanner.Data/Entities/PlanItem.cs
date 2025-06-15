using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BitesPlanner.Data.Entities
{
    public class PlanItem
    {
        [ForeignKey(nameof(Entities.Plan))]
        public int PlanId { get; set; }

        public int LineNumber { get; set; }

        [ForeignKey(nameof(Entities.Meal))]
        public int MealId { get; set; }
        public double Quantity { get; set; }

        [JsonIgnore]
        public Plan? Plan { get; set; }
        [JsonIgnore]
        public Meal? Meal { get; set; } 

    }
}
