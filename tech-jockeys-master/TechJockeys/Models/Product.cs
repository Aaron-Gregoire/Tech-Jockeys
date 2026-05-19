using Microsoft.AspNetCore.Components.Forms;

namespace TechJockeys.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }

        //FK for parent CategoryId  (FK stands for foreign key) (required)
        public int CategoryId { get; set; }

        // parent object reference. we mark as nullable using ? for reasons well see later
        public Category? Category { get; set; }
        
    }
}
