﻿using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    internal class SevkiyatSekliRepository : Repository<SevkiyatSekli>, ISevkiyatSekliRepository
    {
        public SevkiyatSekliRepository(DataContext context) : base(context)
        {
        }
    }
}
