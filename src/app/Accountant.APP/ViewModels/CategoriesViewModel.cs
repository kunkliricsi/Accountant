using Accountant.APP.Models.Web;
using Accountant.APP.Services.Settings.Interfaces;
using Accountant.APP.Services.Web.Interfaces;
using Accountant.APP.ViewModels.Base;
using eShopOnContainers.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Accountant.APP.ViewModels
{
    public class CategoriesViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ICategoryService _categoryService;
        private readonly IDialogService _dialogService;

        public CategoriesViewModel(ISettingsService settingsService,
            INavigationService navigationService,
            ICategoryService categoryService,
            IDialogService dialogService)
        {
            _settingsService = settingsService;
            _categoryService = categoryService;
            _dialogService = dialogService;
        }

        private ICollection<Category> _categories;
        public ICollection<Category> Categories
        {
            get => _categories;
            set => Set(ref _categories, value);
        }

        public ICommand AddCategoryCommand => new Command(async () => await AddCategory());

        private async Task AddCategory()
        {
            IsBusy = true;

            try
            {
                var result = await _dialogService.ShowPromptAsync("What should be the category's name?", "Add new category", "Next", "Cancel", "Category name");
                if (result.Ok)
                {
                    var category = new Category { Name = result.Text };
                    var descResult = await _dialogService.ShowPromptAsync("Do you want to add a description?", "Add new category", "Add", "Add without description");
                    if (descResult.Ok)
                    {
                        category.Description = descResult.Text;
                    }

                    await _categoryService.CreateCategoryAsync(category);
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not create category", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            Categories = await _categoryService.GetAllCategoriesAsync();
            IsBusy = false;
        }
    }
}
