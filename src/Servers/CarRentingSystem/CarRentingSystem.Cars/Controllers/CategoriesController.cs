﻿namespace CarRentingSystem.Cars.Controllers
{
    using CarRentingSystem.Cars.Models.Categories;
    using CarRentingSystem.Cars.Services.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    [ApiController]
    [Route("/[controller]/[action]")]
    public class CategoriesController : ControllerBase
    {
        private ICategoriesService categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
               => this.categoriesService = categoriesService;
        [HttpGet]
        public IEnumerable<CategoryModel> All()
               => this.categoriesService.GetAll();
    }
}