using XmlApp.Models;

namespace XmlApp.Services.Abstract
{
    public interface ISBIFService
    {
        Task<SBIFBilgileri> GetByIdAsync(int id);
        Task AddDocumentAsync(SBIFBilgileri sbif);
        Task<List<SBIFBilgileri>> GetAllAsync();
    }
}
