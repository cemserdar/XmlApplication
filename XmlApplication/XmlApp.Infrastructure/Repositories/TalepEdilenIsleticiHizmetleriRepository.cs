﻿using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    internal class TalepEdilenIsleticiHizmetleriRepository : Repository<TalepEdilenIsleticiHizmetleri>, ITalepEdilenIsleticiHizmetleriRepository
    {
        public TalepEdilenIsleticiHizmetleriRepository(DataContext context) : base(context)
        {
        }
    }
}
