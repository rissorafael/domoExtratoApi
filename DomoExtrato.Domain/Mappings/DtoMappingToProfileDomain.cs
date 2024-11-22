using AutoMapper;
using DomoExtrato.Domain.Entities;
using DomoExtrato.Domain.Models;

namespace DomoExtrato.Domain.Mappings
{
    public class DtoMappingToProfileDomain : Profile
    {
        public DtoMappingToProfileDomain()
        {
            CreateMap<Periodos, PeriodosResponseModel>();
            CreateMap<Extrato, ExtratoResponseModel>();
        }
    }
}
