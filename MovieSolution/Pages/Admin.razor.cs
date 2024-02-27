using Microsoft.AspNetCore.Components;
using MovieSolution.Entities;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;

namespace MovieSolution.Pages
{
    public partial class Admin
    {

        [Inject]
        public IProductService ProductService { get; set; }
        public ProductModel Movie { get; set; } = new();
        public List<ProductCategory> Categories { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Categories = await ProductService.GetProductCategories();
        }
        protected void HandleValidSubmit()
        {
            Console.WriteLine("Not implemented");
        }
        protected void HandleInvalidSubmit()
        {
            Console.WriteLine("Not implemented");
        }
    }
}
