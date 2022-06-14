using Servientrega.Data.Models;
using System;
using System.Collections.Generic;

namespace Servientrega.Models
{
    public class AvionViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Modelo { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
        public List<Avion> ListAvion { get; set; }
    }
}
