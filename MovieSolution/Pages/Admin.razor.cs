using Microsoft.AspNetCore.Components;
using MovieSolution.Entities;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;
using MovieSolution.Shared.Components;

namespace MovieSolution.Pages
{
    public partial class Admin
    {

        [Inject]
        public IProductService ProductService { get; set; }
        public ProductModel Movie { get; set; } = new();
        public List<ProductCategory> Categories { get; set; } = new();
        protected bool IsSaved = false;
        protected string ErrorMessage = string.Empty;
        ShopNavigationLock? NavigationLock { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Categories = await ProductService.GetProductCategories();
            IsSaved = false;
            ErrorMessage = string.Empty;
        }
        protected async Task HandleValidSubmit()
        {
            var result = await ProductService.AddItem(Movie);
            if(result != null)
            {
                Movie = new();
                IsSaved = true;
                // Inform EditContext the model is unmodified ie saved.
                NavigationLock.CurrentEditContext.MarkAsUnmodified();
                StateHasChanged();
                ErrorMessage = string.Empty;
            }
            else
            {
                ErrorMessage = "An error occured when saving product";
                IsSaved = false;
            }
        }
    }
}
