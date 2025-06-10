using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BitesPlanner.Data.entities;

namespace BitesPlanner.Data.Entities
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Calories { get; set; }
        public double Carb { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public Category? Category { get; set; }
        [ForeignKey(nameof(entities.Category))]
        public int CategoryId { get; set; }

    }
}
