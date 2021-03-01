using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using GameStore.Models;

namespace GameStore.Mapping
{
    public class MappingProfile : Profile
    {
        // Domain to Resource
        public MappingProfile()
        {


            CreateMap<LoginModel, UserDTO>()
                .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
                .ForMember(u => u.Password, opt => opt.MapFrom(ur => ur.Password));

            CreateMap<RegisterModel, UserDTO>()
                .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
                .ForMember(u => u.Password, opt => opt.MapFrom(ur => ur.Password))
                .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))
                .ForMember(u => u.Address, opt => opt.MapFrom(ur => ur.Address))
                .ForMember(u => u.Role, opt =>opt.MapFrom(ur => ur.Role));

            CreateMap<UserDTO, User>()
                .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Name));
        }
    }
}
