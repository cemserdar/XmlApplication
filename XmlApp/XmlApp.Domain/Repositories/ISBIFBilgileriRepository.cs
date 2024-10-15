using XmlApp.Domain.Repositories.Base;
using XmlApp.Models;

namespace XmlApp.Domain.Repositories
{
    public interface ISBIFBilgileriRepository : IRepository<SBIFBilgileri>
    {
        Task<SBIFBilgileri> GetByIdAsync(int id);
        Task<List<SBIFBilgileri>> GetAllAsync();
    }
}
