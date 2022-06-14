using Servientrega.Data.Models.Base;

namespace Servientrega.Data.Models
{
    public class Avion : BaseEntity
    {
        public string Modelo { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
    }
}
