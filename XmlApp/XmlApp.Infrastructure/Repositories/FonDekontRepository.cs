using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class FonDekontRepository : Repository<FonDekont>, IFonDekontRepository
    {
        public FonDekontRepository(DataContext context) : base(context)
        {
        }
    }
}
