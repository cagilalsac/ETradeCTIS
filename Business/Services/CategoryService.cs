using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface ICategoryService
    {
        IQueryable<CategoryModel> Query();
        Result Add(CategoryModel model);
        Result Update(CategoryModel model);
        Result Delete(int id);
        List<CategoryModel> GetList();
        CategoryModel GetItem(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly Db _db;

        public CategoryService(Db db)
        {
            _db = db;
        }

        public Result Add(CategoryModel model)
        {
            if (_db.Categories.Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return new ErrorResult("Category can't be added because category with the same name exists!");
            var entity = new Category()
            {
                Name = model.Name.Trim()
            };
            _db.Categories.Add(entity);
            _db.SaveChanges();
            model.Id = entity.Id;
            return new SuccessResult("Category added successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Categories.Include(c => c.Products).SingleOrDefault(c => c.Id == id);
            if (entity is null)
                return new ErrorResult("Category can't be found!");
            if (entity.Products is not null && entity.Products.Any())
                return new ErrorResult("Category can't be deleted because it has relational products!");
            _db.Categories.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Category deleted successfully.");
        }

		public IQueryable<CategoryModel> Query()
        {
            return _db.Categories.Include(c => c.Products).OrderBy(c => c.Name).Select(c => new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name,
                ProductCountOutput = c.Products.Count,
                ProductsOutput = string.Join("<br />", c.Products.OrderBy(p => p.Name).Select(p => p.Name))
            });
        }

        public Result Update(CategoryModel model)
        {
            if (_db.Categories.Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim() && c.Id != model.Id))
                return new ErrorResult("Category can't be updated because category with the same name exists!");
            var entity = _db.Categories.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Category can't be found!");
            entity.Name = model.Name.Trim();
            _db.Categories.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Category updated successfully.");
        }

		public CategoryModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);

        public List<CategoryModel> GetList() => Query().ToList();
	}
}
