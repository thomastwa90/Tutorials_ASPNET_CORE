using OTF.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace OTF.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById (int id);
        Restaurant Update (Restaurant updateRestaurant);
        Restaurant Add (Restaurant newRestaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
              new Restaurant{Id = 1, Name = "Taco_Bell", cusine = CuisineType.Mexican},
              new Restaurant{Id = 2, Name  = "Tokyo_Tokyo", cusine = CuisineType.Japanese}
            };
        }
        public Restaurant Add (Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }
        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
            where string.IsNullOrEmpty(name) || r.Name.StartsWith(name) 
            orderby r.Name
            select r;
        }
        public Restaurant Update (Restaurant updateRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updateRestaurant.Id);
            if (restaurant !=null)
            {
                restaurant.Name = updateRestaurant.Name;
                restaurant.Location = updateRestaurant.Location;
                restaurant.cusine = updateRestaurant.cusine;
            }
            return restaurant;
        }
        public int Commit()
        {
            return 0;
        }
    }
}
