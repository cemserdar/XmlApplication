using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class SbifBilgiFisiRepository : Repository<SbifBilgiFisi>, ISbifBilgiFisiRepository
    {
        public SbifBilgiFisiRepository(DataContext context) : base(context)
        {
        }
    }
}
