using LinqKit;
using StorexWebCore.Enities;
using StorexWebRepository;


namespace StorexWebService
{
    public interface IMovieService
    {
        List<Movies> Search(Movies model);
        bool Insert(Movies vm);
        Movies InsertAndReturnModel(Movies vm);
        Movies Update(Movies vm);
        bool Delete(long ID);
        Movies GetById(long ID);
    }
    public class MovieService : IMovieService
    {
        private IStorexWebRepository<Movies> _MovieRepo = null;
        public MovieService()
        {
            _MovieRepo = new StorexWebRepository<Movies>();
        }

        public bool Insert(Movies model)
        {
            bool success = _MovieRepo.Insert(model);
            return success;
        }
        public Movies InsertAndReturnModel(Movies model)
        {
            return _MovieRepo.InsertAndReturn(model);
        }

        public Movies Update(Movies vm)
        {
            Movies model = _MovieRepo.GetById(vm.MovieID);
            if (model != null)
            {
                model.MovieID = vm.MovieID;
                model.Title = vm.Title;
                model.Description = vm.Description;
                model.Rate = vm.Rate;
                model.Image = vm.Image;
                model.CategoryId = vm.CategoryId;
                if (!_MovieRepo.Update(model))
                {
                    model.MovieID = -1;
                }
            }
            else
            {
                model = new Movies();
                model.MovieID = -1;
            }
            return model;
        }

        public bool Delete(long ID)
        {
            Movies model = _MovieRepo.GetById(ID);
            return _MovieRepo.Delete(model);
        }

        public List<Movies> Search(Movies model)
        {
            var predicate = PredicateBuilder.New<Movies>(true);

            if (model.MovieID > 0)
            {
                predicate = predicate.And(p => p.MovieID == model.MovieID);
            }
            if (!String.IsNullOrEmpty(model.Title))
            {
                predicate = predicate.And(p => p.Title == model.Title);
            }
            if (!String.IsNullOrEmpty(model.Description))
            {
                predicate = predicate.And(p => p.Description == model.Description);
            }
            if (!String.IsNullOrEmpty(model.Image))
            {
                predicate = predicate.And(p => p.Image == model.Image);
            }
            if (model.Rate != null)
            {
                predicate = predicate.And(p => p.Rate == model.Rate);
            }
            if (model.CategoryId != null)
            {
                predicate = predicate.And(p => p.CategoryId == model.CategoryId);
            }
            if (model.UserId != null)
            {
                predicate = predicate.And(p => p.UserId == model.UserId);
            }
            IQueryable<Movies> query = _MovieRepo.Table.AsExpandable().Where(predicate);

            model.TotalRecordCount = query.Count();

            return query.ToList();
        }

        public Movies GetById(long ID)
        {
            return _MovieRepo.GetById(ID);
        }


    }
}
