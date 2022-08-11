using Microsoft.AspNetCore.Mvc;
using StorexWebCore.Enities;
using StorexWebService;
using Microsoft.AspNetCore.Http;

namespace storexweb_task.Controllers
{
    public class UsersController : Controller
    {
        [HttpPost]
        public Users Create(string? Name,string? Email,string? Birthdate)
        {
            Users Users = new Users()
            {
                Name = Name,
                Email = Email,
                Birthdate = Birthdate
            };
            Users result = new Users();
            try
            {
                IUserService UserService = new UserService();
                if (UserService.Search(new Users() { Name = Name }).Count > 0)
                {
                    return (new Users() { UserID = -1 });
                }
                else if (UserService.Search(new Users() { Email = Email }).Count > 0)
                {
                    return (new Users() { UserID = -2 });
                }
                else
                {
                    result = UserService.InsertAndReturnModel(Users);
                    UserService.SaveCurrentUser(result.UserID);
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        [HttpPost]
        public List<Users> GetALLUsers()
        {
            List<Users> result = new List<Users>();
            try
            {
                IUserService UserService = new UserService();
                result = UserService.Search(new Users());
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        public List<Users> Login(string? Name, string? Email)
        {
            Users Users = new Users()
            {
                Name = Name,
                Email = Email
            };
            List<Users> result = new List<Users>();
            try
            {
                IUserService UserService = new UserService();

                result = UserService.Search(Users);
                if(result.Count > 0)
                {
                    UserService.SaveCurrentUser(result[0].UserID);
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
    }
}
