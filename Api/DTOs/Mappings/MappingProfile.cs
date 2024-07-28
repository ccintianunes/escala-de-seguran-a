using AutoMapper;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Policial, PolicialDTO>().ReverseMap();
        CreateMap<Escala, EscalaDTO>().ReverseMap();
        CreateMap<Local, LocalDTO>().ReverseMap();
        CreateMap<MarcacaoEscala, MarcacaoEscalaDTO>().ReverseMap();
        CreateMap<Policial, InativadoDTOPatch>().ReverseMap();
        CreateMap<Escala, InativadoDTOPatch>().ReverseMap();
        CreateMap<Local, InativadoDTOPatch>().ReverseMap();
        CreateMap<MarcacaoEscala, InativadoDTOPatch>().ReverseMap();
    }
}
