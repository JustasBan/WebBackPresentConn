﻿using Microsoft.AspNetCore.Mvc;
using WebBackPresentConn.Models.Entities;
using WebBackPresentConn.Models.Enums;

namespace WebBackPresentConn.Services.Interfaces
{
    public interface IPizzaOrderService
    {
        Task<PizzaOrder> AddPizzaOrderAsync(PizzaOrder pizzaOrder);
        Task<IEnumerable<PizzaOrder>> GetAllOrdersAsync();
        Task<PizzaOrder> GetPizzaOrderByIdAsync(int id);
        Task<decimal> EstimateCostAsync(PizzaSize size, List<Topping> toppings);
        IEnumerable<string> GetPizzaSizeNames();
    }

}
