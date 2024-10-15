﻿using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    internal class KarsiFirmaBilgileriRepository : Repository<KarsiFirmaBilgisi>, IKarsiFirmaBilgileriRepository
    {
        public KarsiFirmaBilgileriRepository(DataContext context) : base(context)
        {
        }
    }
}
