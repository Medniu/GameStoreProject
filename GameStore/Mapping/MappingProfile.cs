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

            CreateMap<Product, GamesInfoDTO>()
                .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))
                .ForMember(u => u.Logo, opt => opt.MapFrom(ur => ur.Logo))
                .ForMember(u => u.Price, opt => opt.MapFrom(ur => ur.Price))
                .ForMember(u => u.Rating, opt => opt.MapFrom(ur => ur.Rating))
                .ForMember(u => u.TotalRating, opt => opt.MapFrom(ur => ur.TotalRating))
                .ForMember(u => u.DateCreated, opt => opt.MapFrom(ur => ur.DateCreated))
                .ForMember(u => u.Category, opt => opt.MapFrom(ur => ur.Category))
                .ForMember(u => u.Background, opt => opt.MapFrom(ur => ur.Background));

            CreateMap<CreateGameModel, GamesInfoDTO>()
                .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))
                .ForMember(u => u.Logo, opt => opt.MapFrom(ur => ur.Logo))
                .ForMember(u => u.Price, opt => opt.MapFrom(ur => ur.Price))
                .ForMember(u => u.Rating, opt => opt.MapFrom(ur => ur.Rating))
                .ForMember(u => u.DateCreated, opt => opt.MapFrom(ur => ur.DateCreated))
                .ForMember(u => u.Category, opt => opt.MapFrom(ur => ur.Category))
                .ForMember(u => u.Background, opt => opt.MapFrom(ur => ur.Background));

            CreateMap<GamesInfoDTO, GamesInformation>()
               .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))
               .ForMember(u => u.Logo, opt => opt.MapFrom(ur => ur.Logo))
               .ForMember(u => u.Price, opt => opt.MapFrom(ur => ur.Price))
               .ForMember(u => u.Rating, opt => opt.MapFrom(ur => ur.Rating))
               .ForMember(u => u.DateCreated, opt => opt.MapFrom(ur => ur.DateCreated))
               .ForMember(u => u.Category, opt => opt.MapFrom(ur => ur.Category))
               .ForMember(u => u.Background, opt => opt.MapFrom(ur => ur.Background));

            CreateMap<GamesInformation, Product>()
               .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))
               .ForMember(u => u.Logo, opt => opt.MapFrom(ur => ur.Logo))
               .ForMember(u => u.Price, opt => opt.MapFrom(ur => ur.Price))
               .ForMember(u => u.Rating, opt => opt.MapFrom(ur => ur.Rating))            
               .ForMember(u => u.DateCreated, opt => opt.MapFrom(ur => ur.DateCreated))
               .ForMember(u => u.Category, opt => opt.MapFrom(ur => ur.Category))
               .ForMember(u => u.Background, opt => opt.MapFrom(ur => ur.Background));

            CreateMap<EditGameModel, GamesInfoDTO>()
                .ForMember(u => u.Name, opt => opt.MapFrom(ur => ur.Name))
                .ForMember(u => u.Logo, opt => opt.MapFrom(ur => ur.Logo))
                .ForMember(u => u.Price, opt => opt.MapFrom(ur => ur.Price))
                .ForMember(u => u.Rating, opt => opt.MapFrom(ur => ur.Rating))
                .ForMember(u => u.DateCreated, opt => opt.MapFrom(ur => ur.DateCreated))
                .ForMember(u => u.Category, opt => opt.MapFrom(ur => ur.Category))
                .ForMember(u => u.Background, opt => opt.MapFrom(ur => ur.Background));

            CreateMap<GameRatingModel, GameRatingDTO>()
                .ForMember(u => u.ProductId, opt => opt.MapFrom(ur => ur.ProductId))
                .ForMember(u => u.GameRating, opt => opt.MapFrom(ur => ur.Rating));

            CreateMap<GameRatingDTO, GameRating>()
                .ForMember(u => u.ProductId, opt => opt.MapFrom(ur => ur.ProductId))
                .ForMember(u => u.Rating, opt => opt.MapFrom(ur => ur.GameRating))
                .ForMember(u => u.UserId, opt => opt.MapFrom(ur => ur.UserId));

            CreateMap<GameRating, ProductRating>()
                .ForMember(u => u.ProductId, opt => opt.MapFrom(ur => ur.ProductId))
                .ForMember(u => u.Rating, opt => opt.MapFrom(ur => ur.Rating))
                .ForMember(u => u.UserId, opt => opt.MapFrom(ur => ur.UserId));

            CreateMap<SortAndFiltrModel, SortAndFiltrDTO>()
                .ForMember(u => u.FiltrByAge, opt => opt.MapFrom(ur => ur.Age))
                .ForMember(u => u.FiltrByGenres, opt => opt.MapFrom(ur => ur.Genres))
                .ForMember(u => u.SortByPrice, opt => opt.MapFrom(ur => ur.Price))
                .ForMember(u => u.SortByRating, opt => opt.MapFrom(ur => ur.Rating))
                .ForMember(u => u.Page, opt => opt.MapFrom(ur => ur.Page))
                .ForMember(u => u.PageSize, opt => opt.MapFrom(ur => ur.PageSize));

            CreateMap<SortAndFiltrDTO, SortAndFiltrInformation>()
                .ForMember(u => u.FiltrByAge, opt => opt.MapFrom(ur => ur.FiltrByAge))
                .ForMember(u => u.FiltrByGenres, opt => opt.MapFrom(ur => ur.FiltrByGenres))
                .ForMember(u => u.SortByPrice, opt => opt.MapFrom(ur => ur.SortByPrice))
                .ForMember(u => u.SortByRating, opt => opt.MapFrom(ur => ur.SortByRating))
                .ForMember(u => u.Page, opt => opt.MapFrom(ur => ur.Page))
                .ForMember(u => u.PageSize, opt => opt.MapFrom(ur => ur.PageSize));

            CreateMap<PageInformation, PageDTO>()
                .ForMember(u => u.GamesInformation,opt => opt.MapFrom(ur=> ur.GamesInformation))
                .ForMember(u => u.CurrentPage, opt => opt.MapFrom(ur => ur.CurrentPage))
                .ForMember(u => u.PageSize, opt => opt.MapFrom(ur => ur.PageSize))
                .ForMember(u => u.TotalItems, opt => opt.MapFrom(ur => ur.TotalItems));

        }
    }
}
