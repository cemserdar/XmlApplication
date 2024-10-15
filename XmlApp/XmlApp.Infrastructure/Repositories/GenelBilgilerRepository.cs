using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class GenelBilgilerRepository : Repository<GenelBilgiler>, IGenelBilgilerRepository
    {
        public GenelBilgilerRepository(DataContext context) : base(context)
        {
        }
    }
}
