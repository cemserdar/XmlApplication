using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    internal class SbifGumrukBilgileriRepository : Repository<SbifGumrukBilgileri>, ISbifGumrukBilgileriRepository
    {
        public SbifGumrukBilgileriRepository(DataContext context) : base(context)
        {
        }
    }
}
