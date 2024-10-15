using XmlApp.Domain.Repositories;
using XmlApp.Models;
using XmlApp.Services.Abstract;

namespace XmlApp.Services.Concrete
{
    public class SBIFService : ISBIFService
    {
        private readonly ISBIFBilgileriRepository _repository;

        public SBIFService(ISBIFBilgileriRepository repository)
        {
            _repository = repository;
        }

        public async Task AddDocumentAsync(SBIFBilgileri sbif)
        {
            await _repository.AddAsync(sbif);
        }

        public async Task<SBIFBilgileri> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<SBIFBilgileri>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
