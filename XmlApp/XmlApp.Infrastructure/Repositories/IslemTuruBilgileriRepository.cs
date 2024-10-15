using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class IslemTuruBilgileriRepository : Repository<IslemTuruBilgileri>, IIslemTuruBilgileriRepository
    {
        public IslemTuruBilgileriRepository(DataContext context) : base(context)
        {
        }
    }
}
