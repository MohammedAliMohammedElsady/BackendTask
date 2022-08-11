using Microsoft.AspNetCore.Mvc;
using StorexWebCore.Enities;
using StorexWebService;

namespace StorexWebTask.Controllers
{
    public class MoviesController : Controller
    {
        [HttpPost]
        public Movies Create(string? Title, string? Description, int? Rate, string? Image, long? CategoryId)
        {
            IUserService IUserService = new UserService();
            Movies Movies = new Movies()
            {
                Title = Title,
                Description = Description,
                Rate = Rate,
                Image = Image,
                CategoryId = CategoryId,
                UserId = (IUserService.GetCurrentUser())
            };
            Movies result = new Movies();
            try
            {
                IMovieService MovieService = new MovieService();

                result = MovieService.InsertAndReturnModel(Movies);
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        [HttpPost]
        public List<Movies> GetALLMovies(long MovieID, string? Title, string? Description, int? Rate, string? Image, long? CategoryId)
        {
            IUserService IUserService = new UserService();
            Movies Movies = new Movies()
            {
                MovieID = MovieID,
                Title = Title,
                Description = Description,
                Rate = Rate,
                Image = Image,
                CategoryId = CategoryId,
                UserId = IUserService.GetCurrentUser()
            };
            List<Movies> result = new List<Movies>();
            try
            {
                IMovieService MovieService = new MovieService();
                result = MovieService.Search(Movies);
                ICategoryService CategoryService = new CategoryService();
                List<Categories> Categories = CategoryService.Search(new Categories());
                for (int indx = 0; indx < result.Count; indx++)
                {
                    result[indx].CategoryName = Categories.Where(t => t.CategoryID == result[indx].CategoryId).Select(t => t.Title).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        [HttpPost]
        public Movies Update(long MovieID, string? Title, string? Description, int? Rate, string? Image, long? CategoryId)
        {
            IUserService IUserService = new UserService();
            Movies Movies = new Movies()
            {
                MovieID = MovieID,
                Title = Title,
                Description = Description,
                Rate = Rate,
                Image = Image,
                CategoryId = CategoryId,
                UserId = IUserService.GetCurrentUser()
            };
            Movies result = new Movies();
            try
            {
                IMovieService MovieService = new MovieService();
                result = MovieService.Update(Movies);
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        [HttpPost]
        public bool Delete(long MovieID)
        {
            bool result = false;
            try
            {
                IMovieService MovieService = new MovieService();
                result = MovieService.Delete(MovieID);
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
    }
}
