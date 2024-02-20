using Microsoft.AspNetCore.Components;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;

namespace MovieSolution.Pages
{
    public partial class Products
    {
        [Inject]
        public IProductService productService { get; set; }

        public Products()
        {
        }

        private List<ProductModel>? AllProducts { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AllProducts = await productService.GetItems();
        }
    }
}
