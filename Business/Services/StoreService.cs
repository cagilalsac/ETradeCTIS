using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
	public interface IStoreService
	{
		IQueryable<StoreModel> Query();
		Result Add(StoreModel model);
		Result Update(StoreModel model);
		Result Delete(int id);
        List<StoreModel> GetList();
        StoreModel GetItem(int id);
    }

	public class StoreService : IStoreService
	{
		private readonly Db _db;

		public StoreService(Db db)
		{
			_db = db;
		}

		public Result Add(StoreModel model)
		{
			if (_db.Stores.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
				return new ErrorResult("Store can't be added because store with the same name exists!");
			var entity = new Store()
			{
				Name = model.Name.Trim()
			};
			_db.Stores.Add(entity);
			_db.SaveChanges();
			model.Id = entity.Id;
			return new SuccessResult("Store added successfully.");
		}

		public Result Delete(int id)
		{
			var entity = _db.Stores.Include(s => s.ProductStores).SingleOrDefault(s => s.Id == id);
			if (entity is null)
				return new ErrorResult("Store can't be found!");
			if (entity.ProductStores is not null && entity.ProductStores.Any())
				_db.ProductStores.RemoveRange(entity.ProductStores);
			_db.Stores.Remove(entity);
			_db.SaveChanges();
			return new SuccessResult("Store deleted successfully.");
		}

		public IQueryable<StoreModel> Query()
		{
			return _db.Stores.OrderBy(s => s.Name).Select(s => new StoreModel()
			{
				Id = s.Id,
				Name = s.Name
			});
		}

		public Result Update(StoreModel model)
		{
			if (_db.Stores.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
				return new ErrorResult("Store can't be updated because store with the same name exists!");
			var entity = _db.Stores.Find(model.Id);
			if (entity is null)
				return new ErrorResult("Store can't be found!");
            entity.Name = model.Name.Trim();
			_db.Stores.Update(entity);
			_db.SaveChanges();
			return new SuccessResult("Store updated successfully.");
		}

		public StoreModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);

		public List<StoreModel> GetList() => Query().ToList();
    }
}
