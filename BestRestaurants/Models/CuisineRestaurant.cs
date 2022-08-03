namespace BestRestaurants.Models
{
  public class CuisineRestaurant
  {
    public int CuisineRestaurantId { get; set; }
    public int RestaurantId { get; set; }
    public int CuisineId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public virtual Cuisine Cusine { get; set; }
  }
}