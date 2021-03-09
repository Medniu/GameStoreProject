﻿using BLL.DTO;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGamesService
    {
        Task<IEnumerable<TopCategoriesDTO>> GetTopCategories();
        Task<IEnumerable<GamesInfoDTO>> FindGameByName(SearchQueryDTO queryDTO);
        Task<GamesInfoDTO> FindGameById(int id);
        Task<GamesInfoDTO> CreateGame(GamesInfoDTO gamesInfo);
        Task<bool> DeleteGameById(int id);

        Task<GamesInfoDTO> EditGame(int id, GamesInfoDTO gamesInfo);
    }
}