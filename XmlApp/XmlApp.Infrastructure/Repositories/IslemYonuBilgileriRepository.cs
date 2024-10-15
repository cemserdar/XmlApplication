using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class IslemYonuBilgileriRepository : Repository<IslemYonuBilgileri>, IIslemYonuBilgileriRepository
    {
        public IslemYonuBilgileriRepository(DataContext context) : base(context)
        {
        }
    }
}
