using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class FaturaBilgileriRepository : Repository<FaturaBilgileri>, IFaturabilgileriRepository
    {
        public FaturaBilgileriRepository(DataContext context) : base(context)
        {
        }
    }
}
