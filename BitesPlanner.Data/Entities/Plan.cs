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

        public string? Description { get; set; }
        public double Calories { get; set; }

        public double protein { get; set; }

        public double fats { get; set; }

        public double carbs { get; set; }
        public ICollection<PlanItem>? PlanItems { get; set; }

        [ForeignKey(nameof(Entities.User))]
        public int assignedUserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
