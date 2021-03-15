﻿using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AddProductToOrder(OrderModelDTO orderDTO);
        Task<IEnumerable<GamesInfoDTO>> GetProductFromOrder(string orderId);

        Task DeleteProductsFromOrder(DeletedGameModelDTO deletedGamesId);
        Task Buy(string userId);
    }
}