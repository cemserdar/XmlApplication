using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class TeslimAdresiRepository : Repository<TeslimAdresi>, ITeslimAdresiRepository
    {
        public TeslimAdresiRepository(DataContext context) : base(context)
        {
        }
    }
}
