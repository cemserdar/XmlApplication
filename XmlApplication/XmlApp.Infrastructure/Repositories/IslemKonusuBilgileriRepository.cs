using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    internal class IslemKonusuBilgileriRepository : Repository<IslemKonusuBilgileri>, IIslemKonusuBilgileriRepository
    {
        public IslemKonusuBilgileriRepository(DataContext context) : base(context)
        {
        }
    }
}
