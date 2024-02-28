using Microsoft.AspNetCore.Components;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;

namespace MovieSolution.Pages
{
    public partial class Products
    {
        [Inject]
        public IProductService productService { get; set; }

        private List<ProductModel> AllProducts { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            AllProducts = await productService.GetItems();
        }
    }
}
