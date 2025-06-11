using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BitesPlanner.Data.Entities;

namespace BitesPlanner.Data.entities
{
    public class Category
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public  ICollection<Meal>? Meals { get; set; } 
    }
}
