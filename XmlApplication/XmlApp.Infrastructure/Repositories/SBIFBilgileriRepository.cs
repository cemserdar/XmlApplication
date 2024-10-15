using Microsoft.EntityFrameworkCore;
using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models;

namespace XmlApp.Infrastructure.Repositories
{
    public class SBIFBilgileriRepository : Repository<SBIFBilgileri>, ISBIFBilgileriRepository
    {
        public SBIFBilgileriRepository(DataContext context) : base(context)
        {
        }

        public List<SBIFBilgileri> GetAll()
        {
            return _dbContext.SBIFBilgileri
                                   .Include(q => q.GenelBilgiler)
                                        .ThenInclude(q => q.IslemYonuBilgileri)
                                   .Include(q => q.GenelBilgiler)
                                        .ThenInclude(q => q.IslemTuruBilgileri)
                                   .Include(q => q.GenelBilgiler)
                                        .ThenInclude(q => q.IslemKonusuBilgileri)
                                   .Include(q => q.GenelBilgiler)
                                        .ThenInclude(q => q.SevkiyatSekli)
                                   .Include(q => q.FaturaBilgileri)
                                        .ThenInclude(q => q.Fatura)
                                        .ThenInclude(q => q.TeslimAdresi)
                                   .Include(q => q.FaturaBilgileri)
                                        .ThenInclude(q => q.Fatura)
                                        .ThenInclude(q => q.KarsiFirmaBilgisi)
                                   .Include(q => q.TalepEdilenIsleticiHizmetleri)
                                   .Include(q => q.SbifBilgiFisi)
                                        .ThenInclude(q => q.FonDekont)
                                   .Include(q => q.SbifGumrukBilgileri)
                                        .ThenInclude(q => q.GumrukMudurluguOnayi)
                                        .Include(q => q.SbifGumrukBilgileri)
                                        .ThenInclude(q => q.GumrukMuhafazaMudurluguOnayi)
                                   .Include(q => q.MalKalemBilgileri)
                                       .ThenInclude(q => q.MalKalem)
                                   .ToList();
        }


    }
}
