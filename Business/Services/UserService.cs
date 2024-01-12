using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;

namespace Business.Services
{
    public interface IUserService
    {
        IQueryable<UserModel> Query();
        Result Add(UserModel model);
    }

    public class UserService : IUserService
    {
        private readonly Db _db;

        public UserService(Db db)
        {
            _db = db;
        }

        public Result Add(UserModel model)
        {
            if (_db.Users.Any(u => u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.IsActive))
                return new ErrorResult("User can't be added because active user with the same name exists!");
            var entity = new User()
            {
                IsActive = model.IsActive,
                UserName = model.UserName.Trim(),
                Password = model.Password.Trim(),
                RoleId = model.RoleId.Value
            };
            _db.Users.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("User added successfully.");
        }

        public IQueryable<UserModel> Query() => _db.Users.OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName)
                                                .Select(u => new UserModel()
                                                {
                                                    Id = u.Id,
                                                    UserName = u.UserName,
                                                    Password = u.Password,
                                                    IsActive = u.IsActive,
                                                    RoleId = u.RoleId,
                                                    RoleOutput = u.Role.Name
                                                });
    }
}
