using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Trial.Application.DTO;
using Trial.Domain.Entities;

namespace Trial.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TblUser, UserDTO>().ReverseMap();
            // CreateMap<Source, Destination>();
            // CreateMap<Destination, Source>();
            // CreateMap<Source, Destination>().ReverseMap();
            // CreateMap<Source, Destination>().ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property));
            // CreateMap<Source, Destination>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // CreateMap<Source, Destination>().ConvertUsing(new CustomConverter());
        }
    }
}
