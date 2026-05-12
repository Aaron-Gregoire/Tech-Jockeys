using System.Runtime.CompilerServices;

namespace TechJockeys.Models
{
    public class Category
    {
        // using {ModelName}Id as the property tells the server automatically this is a PK
        public int CategoryId { get; set; }

        public String Name { get; set; }




    }
}
