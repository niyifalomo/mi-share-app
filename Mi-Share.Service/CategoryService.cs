using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Model;
using Mi_Share.Data.Infrastructure;
using Mi_Share.Data.Repositories;

namespace Mi_Share.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);

    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            this._unitOfWork = unitOfWork;
            this._categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _categoryRepository.GetAll();
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = _categoryRepository.GetById(id);
            return category;
        }
    }
}
