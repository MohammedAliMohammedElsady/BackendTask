using LinqKit;
using StorexWebCore.Enities;
using StorexWebRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorexWebService
{
    public interface IUserService
    {
        List<Users> Search(Users model);
        bool Insert(Users vm);
        Users InsertAndReturnModel(Users vm);
        bool Update(Users vm);
        bool Delete(long ID);
        Users GetById(long ID);
        void SaveCurrentUser(long UserID);
        long? GetCurrentUser();
    }
    public class UserService : IUserService
    {
        private IStorexWebRepository<Users> _UserRepo = null;
        public UserService()
        {
            _UserRepo = new StorexWebRepository<Users>();
        }

        public bool Insert(Users model)
        {
            bool success = _UserRepo.Insert(model);
            return success;
        }
        public Users InsertAndReturnModel(Users model)
        {
            return _UserRepo.InsertAndReturn(model);
        }

        public bool Update(Users vm)
        {
            Users model = _UserRepo.GetById(vm.UserID);
            if (model != null)
            {
                model.UserID = vm.UserID;
                model.Name = vm.Name;
                model.Email = vm.Email;
                model.Birthdate = vm.Birthdate;
                return _UserRepo.Update(model);
            }
            return false;
        }

        public bool Delete(long ID)
        {
            Users model = _UserRepo.GetById(ID);
            return _UserRepo.Delete(model);
        }

        public List<Users> Search(Users model)
        {
            var predicate = PredicateBuilder.New<Users>(true);

            if (model.UserID > 0)
            {
                predicate = predicate.And(p => p.UserID == model.UserID);
            }
            if (!String.IsNullOrEmpty(model.Name))
            {
                predicate = predicate.And(p => p.Name == model.Name);
            }
            if (!String.IsNullOrEmpty(model.Email))
            {
                predicate = predicate.And(p => p.Email == model.Email);
            }
            if (!String.IsNullOrEmpty(model.Birthdate))
            {
                predicate = predicate.And(p => p.Birthdate == model.Birthdate);
            }

            IQueryable<Users> query = _UserRepo.Table.AsExpandable().Where(predicate);

            model.TotalRecordCount = query.Count();

            return query.ToList();
        }

        public Users GetById(long ID)
        {
            return _UserRepo.GetById(ID);
        }

        public void SaveCurrentUser(long UserID)
        {
            CurrentUser CurrentUser = new CurrentUser();
            ICurrentUserService ICurrentUserService = new CurrentUserService();
            CurrentUser? DeleteUser = ICurrentUserService.Search();
            if (DeleteUser != null)
            {
                ICurrentUserService.Delete(DeleteUser);
            }
            ICurrentUserService.Insert(new CurrentUser() { UserID = UserID, ID = 1 });
        }

        public long? GetCurrentUser()
        {
            ICurrentUserService ICurrentUserService = new CurrentUserService();
            CurrentUser? CurrentUser = ICurrentUserService.Search();
            if(CurrentUser != null)
               return CurrentUser.UserID;
            return null;
        }
    }
}
