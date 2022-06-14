using Servientrega.Data.Models;
using System;
using System.Collections.Generic;

namespace Servientrega.Data.Interface
{
    public interface IAvionRepository
    {
        IEnumerable<Avion> GetAll();
        Avion GetById(Guid id);
        bool Insert(Avion entity);
        bool Update(Avion entity);
        bool Delete(Avion entity);
    }
}
