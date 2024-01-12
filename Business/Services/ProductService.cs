using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IProductService
    {
        IQueryable<ProductModel> Query();
        Result Add(ProductModel model);
        Result Update(ProductModel model);
        Result Delete(int id);
    }

    public class ProductService : IProductService
    {
        private readonly Db _db;

        public ProductService(Db db)
        {
            _db = db;
        }

        public Result Add(ProductModel model)
        {
            if (_db.Products.Any(p => p.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return new ErrorResult("Product can't be added because product with the same name exists!");
            var entity = new Product()
            {
                CategoryId = model.CategoryId ?? 0,
                ExpirationDate = model.ExpirationDate,
                IsDiscontinued = model.IsDiscontinued,
                Name = model.Name.Trim(),
                ProductStores = model.StoreIdsInput?.Select(sId => new ProductStore()
                {
                    StoreId = sId
                }).ToList(),
                UnitPrice = model.UnitPrice ?? 0
            };
            _db.Products.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Product added successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Products.Include(p => p.ProductStores).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return new ErrorResult("Product can't be found!");
            if (entity.ProductStores is not null && entity.ProductStores.Any())
                _db.ProductStores.RemoveRange(entity.ProductStores);
            _db.Products.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("Product deleted successfully.");
        }

        public IQueryable<ProductModel> Query()
        {
            return _db.Products.Include(p => p.Category).Include(p => p.ProductStores)
                .OrderByDescending(p => p.IsDiscontinued).ThenBy(p => p.Name).Select(p => new ProductModel()
                {
                    CategoryId = p.CategoryId,
                    CategoryOutput = p.Category.Name,
                    ExpirationDate = p.ExpirationDate,
                    ExpirationDateOutput = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy") : "",
                    Id = p.Id,
                    IsDiscontinued = p.IsDiscontinued,
                    IsDiscontinuedOutput = p.IsDiscontinued ? "Yes" : "No",
                    Name = p.Name,
                    StoresOutput = string.Join("<br />", p.ProductStores.Select(ps => ps.Store.Name)),
                    StoreIdsInput = p.ProductStores.Select(ps => ps.StoreId).ToList(),
                    UnitPrice = p.UnitPrice,
                    UnitPriceOutput = p.UnitPrice.ToString("C2")
                });
        }

        public Result Update(ProductModel model)
        {
            if (_db.Products.Any(p => p.Name.ToUpper() == model.Name.ToUpper().Trim() && p.Id != model.Id))
                return new ErrorResult("Product can't be updated because product with the same name exists!");
            var entity = _db.Products.Include(p => p.ProductStores).SingleOrDefault(p => p.Id == model.Id);
            if (entity.ProductStores is not null && entity.ProductStores.Any())
                _db.ProductStores.RemoveRange(entity.ProductStores);
            entity.CategoryId = model.CategoryId ?? 0;
            entity.ExpirationDate = model.ExpirationDate;
            entity.IsDiscontinued = model.IsDiscontinued;
            entity.Name = model.Name.Trim();
            entity.ProductStores = model.StoreIdsInput?.Select(sId => new ProductStore()
            {
                StoreId = sId
            }).ToList();
            entity.UnitPrice = model.UnitPrice ?? 0;
            _db.Products.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Product updated successfully.");
        }
    }
}
