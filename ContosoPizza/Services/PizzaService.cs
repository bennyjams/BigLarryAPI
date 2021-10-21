using ContosoPizza.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContosoPizza.Services
{
    public static class PizzaService
    {
        static List<Pizza> Pizzas {get;}
        static int nextId = 3;
        static PizzaService()
        {
            Pizzas = new List<Pizza>
            {
                new Pizza {Id = 1, Name = "Classic Italian", IsGlutenFree = false},
                new Pizza {Id = 2, Name = "Veggie", IsGlutenFree = true}
            };
        }

        public static List<Pizza> GetAll() => Pizzas;
        
        public static Pizza Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

        public static void Add(Pizza piz)
        {
            piz.Id = nextId++;
            Pizzas.Add(piz);
        }
        
        public static void Delete(int id)
        {
            var pizza = Get(id);
            if(pizza is null)
                return;
            
            Pizzas.Remove(pizza);
        }

        public static void Update(Pizza piz)
        {
            var index = Pizzas.FindIndex(p => p.Id == piz.Id);
            if(index == -1)
                return;

            Pizzas[index] = piz;
        }

    }
}