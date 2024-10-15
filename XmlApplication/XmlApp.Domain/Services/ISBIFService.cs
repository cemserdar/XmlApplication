using Microsoft.AspNetCore.Http;
using XmlApp.Models;

namespace XmlApp.Domain.Services
{
    public interface ISBIFService
    {
        Task BelgeEkle(IFormFile file);
    }
}
