﻿using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Infrastructure.Repositories.Base;
using XmlApp.Models.Fields;

namespace XmlApp.Infrastructure.Repositories
{
    public class MalKalemRepository : Repository<MalKalem>, IMalKalemRepository
    {
        public MalKalemRepository(DataContext context) : base(context)
        {
        }
    }
}
