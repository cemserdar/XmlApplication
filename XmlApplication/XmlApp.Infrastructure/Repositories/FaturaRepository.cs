using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class FaturaRepository : Repository<Fatura>, IFaturaRepository
    {
        public FaturaRepository(DataContext dbContext) : base(dbContext)
        {

        }
    }
}
