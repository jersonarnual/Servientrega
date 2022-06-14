using AutoMapper;
using Servientrega.Data.Models;

namespace Servientrega.Models
{
    public class ServientregaUIProfile : Profile
    {
        public ServientregaUIProfile()
        {
            CreateMap<Avion, AvionViewModel>();
            CreateMap<AvionViewModel, Avion>();
        }
    }
}
