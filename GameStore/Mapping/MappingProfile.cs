using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.DTO;
using DAL.Entities;
using GameStore.Models;

namespace GameStore.Mapping
{
    public class MappingProfile : Profile
    {        
        public MappingProfile()
        {


            CreateMap<LoginModel, UserDTO>()
                .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
                .ForMember(u => u.Password, opt => opt.MapFrom(ur => ur.Password));

            CreateMap<RegisterModel, UserDTO>()
                .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
                .ForMember(u => u.Password, opt => opt.MapFrom(ur => ur.Password))
                .ForMember(u =>u.PhoneNumber, opt =>opt.MapFrom(ur => ur.PhoneNumber))
                .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))               
                .ForMember(u => u.Role, opt =>opt.MapFrom(ur => ur.Role));           

            CreateMap<UserDTO, User>()
                .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Name))
                .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(ur => ur.PhoneNumber));
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
                .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.UserName))
                .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(ur => ur.PhoneNumber));


            CreateMap<UserDTO, UserProfile>()
               .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
               .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))
               .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(ur => ur.PhoneNumber));
            CreateMap<UserProfile,UserDTO>()
               .ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.Email))
               .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))
               .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(ur => ur.PhoneNumber));

            CreateMap<ChangePasswordModel, UserDTO>()
                .ForMember(u => u.Password, opt => opt.MapFrom(ur => ur.OldPassword))
                .ForMember(u => u.NewPassword, opt => opt.MapFrom(ur => ur.NewPassword));

            CreateMap<SearchRequestModel, SearchQueryDTO>()
                .ForMember(u => u.Limit, opt => opt.MapFrom(ur => ur.Limit))
                .ForMember(u => u.Term, opt => opt.MapFrom(ur => ur.Term));

            CreateMap<SearchQueryDTO, SearchQuery>()
                .ForMember(u => u.Limit, opt => opt.MapFrom(ur => ur.Limit))
                .ForMember(u => u.Term, opt => opt.MapFrom(ur => ur.Term));

        }
    }
}
