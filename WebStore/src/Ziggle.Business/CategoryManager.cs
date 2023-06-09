using System.Linq;
using Ziggle.Repository;

namespace Ziggle.Business
{
    public interface ICategoryManager
    {
        CategoryModel[] Categories { get; }
        CategoryModel Category(int categoryId);
    }

    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public CategoryModel[] Categories
        {
            get
            {
                return categoryRepository.Categories
                                         .Select(t => new CategoryModel(t.Id, t.Name))
                                         .ToArray();
            }
        }

        public CategoryModel Category(int categoryId)
        {
            var categoryModel = categoryRepository.Category(categoryId);
            return new CategoryModel(categoryModel.Id, categoryModel.Name);
        }
    }
}