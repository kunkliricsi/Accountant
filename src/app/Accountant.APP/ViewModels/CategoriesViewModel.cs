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

        public ICommand AddCategoryCommand => new Command(async () => await AddCategoryAsync());
        public ICommand EditCategoryCommand => new Command<Category>(async c => await EditCategoryAsync(c));
        public ICommand DeleteCategoryCommand => new Command<Category>(async c => await DeleteCategoryAsync(c));

        private async Task AddCategoryAsync()
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

        private async Task EditCategoryAsync(Category category)
        {
            IsBusy = true;

            try
            {
                var result = await _dialogService.ShowPromptAsync("What should be the category's name?", $"Edit category '{category.Name}'.", "OK", "Cancel", $"{category.Name}");
                if (result.Ok)
                {
                    category.Name = result.Text;
                    var descResult = await _dialogService.ShowPromptAsync("Do you want to add a description?", "Edit description", "OK", "NO description change");
                    if (descResult.Ok)
                    {
                        category.Description = descResult.Text;
                    }

                    await _categoryService.UpdateCategoryAsync(category);
                    await RefrestCategories();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not edit category", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteCategoryAsync(Category category)
        {
            IsBusy = true;

            try
            {
                var result = await _dialogService.ShowConfirmAsync($"Do you really want to delete category '{category.Name}'", "Are you sure?", "Yes, confirm", "No");
                if (result)
                {
                    await _categoryService.DeleteCategoryAsync(category.Id);
                    await RefrestCategories();
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync($"{ex}", "Could not delete category", "Hmm");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            return RefrestCategories();
        }

        private async Task RefrestCategories()
        {
            IsBusy = true;
            Categories = await _categoryService.GetAllCategoriesAsync();
            IsBusy = false;
        }
    }
}
