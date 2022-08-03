using System.Collections.Generic;

namespace BestRestaurants.Models
{
    public class Cuisine
    {
        public Cuisine()
        {
            this.JoinEntities = new HashSet<CuisineRestaurant>();
        }

        public int CuisineId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CuisineRestaurant> JoinEntities { get; set; }
    }
}