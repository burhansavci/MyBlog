using MyBlog.Core.DataAccess;
using MyBlog.Entities.Concrete;

namespace MyBlog.DataAccess.Abstract
{
    public interface ILanguageRepository : IRepository<Language>
    {
        void SoftDelete(Language language);
    }
}
