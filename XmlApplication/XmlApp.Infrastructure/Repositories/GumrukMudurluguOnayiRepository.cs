using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    internal class GumrukMudurluguOnayiRepository : Repository<GumrukMudurluguOnayi>, IGumrukMudurluguOnayiRepository
    {
        public GumrukMudurluguOnayiRepository(DataContext context) : base(context)
        {
        }
    }
}
