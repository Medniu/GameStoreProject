using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> AddToOrder(OrderDTO orderDTO);
        Task<ICollection<GamesInformation>> GetProductFromOrder(string orderId, string userId);
        Task<bool> Delete(DeletedGameDTO deletedGamesId);
        Task<bool> CompleteOrder(string userId);
    }
}
