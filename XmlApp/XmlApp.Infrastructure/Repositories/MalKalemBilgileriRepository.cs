using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class MalKalemBilgileriRepository : Repository<MalKalemBilgileri>, IMalKalemBilgileriRepository
    {
        public MalKalemBilgileriRepository(DataContext context) : base(context)
        {
        }
    }
}
