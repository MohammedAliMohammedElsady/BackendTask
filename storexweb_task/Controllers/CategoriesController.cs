using Microsoft.AspNetCore.Mvc;
using StorexWebCore.Enities;
using StorexWebService;

namespace StorexWebTask.Controllers
{
    public class CategoriesController : Controller
    {
        [HttpPost]
        public Categories Create(string? Title)
        {
            IUserService IUserService = new UserService();
            Categories Category = new Categories()
            {
                Title = Title,
                UserId = IUserService.GetCurrentUser()
            };
            Categories result = new Categories();
            try
            {
                ICategoryService CategoryService = new CategoryService();

                result = CategoryService.InsertAndReturnModel(Category);
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        [HttpPost]
        public List<Categories> GetALLCategories(long CategoryID, string? Title)
        {
            IUserService IUserService = new UserService();
            Categories Category = new Categories()
            {
                CategoryID = CategoryID,
                Title = Title,
                UserId = IUserService.GetCurrentUser()
            };
            List<Categories> result = new List<Categories>();
            try
            {
                ICategoryService CategoryService = new CategoryService();
                result = CategoryService.Search(Category);
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        [HttpPost]
        public Categories Update(long CategoryID, string? Title)
        {
            IUserService IUserService = new UserService();
            Categories Category = new Categories()
            {
                CategoryID = CategoryID,
                Title = Title,
                UserId = IUserService.GetCurrentUser()
            };
            Categories result = new Categories();
            try
            {
                ICategoryService CategoryService = new CategoryService();
                result = CategoryService.Update(Category);
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        [HttpPost]
        public bool Delete(long CategoryID)
        {
            bool result = false;
            try
            {
                ICategoryService CategoryService = new CategoryService();
                result = CategoryService.Delete(CategoryID);
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
    }
}
