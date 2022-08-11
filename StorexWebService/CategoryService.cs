using LinqKit;
using StorexWebCore.Enities;
using StorexWebRepository;

namespace StorexWebService
{
    public interface ICategoryService
    {
        List<Categories> Search(Categories model);
        bool Insert(Categories vm);
        Categories InsertAndReturnModel(Categories vm);
        Categories Update(Categories vm);
        bool Delete(long ID);
        Categories GetById(long ID);
    }
    public class CategoryService : ICategoryService
    {
        private IStorexWebRepository<Categories> _CategoryRepo = null;
        public CategoryService()
        {
            _CategoryRepo = new StorexWebRepository<Categories>();
        }

        public bool Insert(Categories model)
        {
            bool success = _CategoryRepo.Insert(model);
            return success;
        }
        public Categories InsertAndReturnModel(Categories model)
        {
            return _CategoryRepo.InsertAndReturn(model);
        }

        public Categories Update(Categories vm)
        {
            Categories model = _CategoryRepo.GetById(vm.CategoryID);
            if (model != null)
            {
                model.CategoryID = vm.CategoryID;
                model.Title = vm.Title;

                if(! _CategoryRepo.Update(model))
                {
                    model.CategoryID = -1;
                }
            }
            else
            {
                model = new Categories();
                model.CategoryID = -1;
            }
            return model;
        }

        public bool Delete(long ID)
        {
            Categories model = _CategoryRepo.GetById(ID);
            return _CategoryRepo.Delete(model);
        }

        public List<Categories> Search(Categories model)
        {
            var predicate = PredicateBuilder.New<Categories>(true);

            if (model.CategoryID > 0)
            {
                predicate = predicate.And(p => p.CategoryID == model.CategoryID);
            }
            if (!String.IsNullOrEmpty(model.Title))
            {
                predicate = predicate.And(p => p.Title == model.Title);
            }
            if (model.UserId != null)
            {
                predicate = predicate.And(p => p.UserId == model.UserId);
            }
            IQueryable<Categories> query = _CategoryRepo.Table.AsExpandable().Where(predicate);

            model.TotalRecordCount = query.Count();

            return query.ToList();
        }

        public Categories GetById(long ID)
        {
            return _CategoryRepo.GetById(ID);
        }


    }
}
