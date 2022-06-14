using Servientrega.Data.Models;
using Servientrega.Infraestructure.Util;
using System;

namespace Servientrega.Business.Interface
{
    public interface IAvionBusiness
    {
        Result GetAll();
        Result GetById(Guid id);
        Result Insert(Avion entity);
        Result Update(Avion entity);
        Result Delete(Guid id);
    }
}
