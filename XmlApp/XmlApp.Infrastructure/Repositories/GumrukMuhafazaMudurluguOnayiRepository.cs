using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class GumrukMuhafazaMudurluguOnayiRepository : Repository<GumrukMuhafazaMudurluguOnayi>, IGumrukMuhafazaMudurluguOnayiRepository
    {
        public GumrukMuhafazaMudurluguOnayiRepository(DataContext context) : base(context)
        {
        }
    }
}
