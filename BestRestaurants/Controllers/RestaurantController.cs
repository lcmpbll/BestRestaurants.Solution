using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Collections.Generic;
using System.Linq;

namespace BestRestaurants.Controllers
{
  public class RestaurantController : Controller
  {
    private readonly BestRestaurantsContext _db;

    public RestaurantController(BestRestaurantsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      
      
      return View(_db.Restaurant.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.CuisineId = new SelectList(_db.Cuisine, "CuisineId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Restaurant restaurant, int CuisineId)
    {
      _db.Restaurant.Add(restaurant);
      _db.SaveChanges();
      if (CuisineId !=0 )
      {
        _db.CuisineRestaurant.Add(new CuisineRestaurant() { CuisineId = CuisineId, RestaurantId = restaurant.RestaurantId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Restaurant thisRestaurant = _db.Restaurant
        .Include(restaraunt => restaraunt.JoinEntities)
        .ThenInclude(join => join.Cuisine)
        .FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    public ActionResult Edit(int id)
    {
      var thisRestaurant = _db.Restaurant.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      ViewBag.CuisineId = new SelectList(_db.Cuisine, "CuisineId", "Name");
      return View(thisRestaurant);
    }

    [HttpPost]
    public ActionResult Edit(Restaurant restaurant, int CuisineId)
    {
     if (CuisineId != 0)
     {
      _db.CuisineRestaurant.Add(new CuisineRestaurant() { CuisineId = CuisineId, RestaurantId = restaurant.RestaurantId });
     } 
      _db.Entry(restaurant).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisRestaurant = _db.Restaurant.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRestaurant = _db.Restaurant.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      _db.Restaurant.Remove(thisRestaurant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
   }
 }