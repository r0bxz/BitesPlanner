using BitesPlanner.Data.entities;
using BitesPlanner.Data.Repositories;

namespace BitesPlanner.BL.services
{
    public class CategoryService
    {

        private readonly CategoryRepository _categoryRepository;
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) throw new ApplicationException($"Category with ID {id} not found.");
            return category;
        }
        public async Task AddCategoryAsync(Category category)
        {
            if (category == null) throw new ApplicationException("Category cannot be null.");
       
            await  ValidateCategory(category);

            await _categoryRepository.AddCategoryAsync(category);
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            if (category == null) throw new ApplicationException("Category cannot be null");

            var dbCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);
            if (dbCategory == null) throw new ApplicationException("Couldn't find category with the provided id");

            await ValidateCategory(category);
            
            dbCategory.Name = category.Name;
            dbCategory.Description = category.Description;

            await _categoryRepository.UpdateCategoryAsync(dbCategory);
        }
        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        private async Task ValidateCategory(Category category)
        {
            var existing = await _categoryRepository.GetCategoryByNameAsync(category.Name);
            if (existing != null && existing.Id != category.Id) throw new ApplicationException($"A category with the name '{category.Name}' already exists.");

            if (String.IsNullOrWhiteSpace(category.Name)) throw new ApplicationException("The name of the category cannot be empty");
            if (category.Name.Length > 20) throw new ArgumentException("Category name must not exceed 20 characters.");
            if (!string.IsNullOrWhiteSpace(category.Description) && category.Description.Length > 250) throw new ApplicationException("Category description must not exceed 250 characters.");
        }

        

    }
}
