using XmlApp.Domain.Repositories.Base;
using XmlApp.Models;

namespace XmlApp.Domain.Repositories
{
    public interface ISBIFBilgileriRepository : IRepository<SBIFBilgileri>
    {
        List<SBIFBilgileri> GetAll();
    }
}
