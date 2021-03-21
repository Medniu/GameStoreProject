using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AddProductToOrder(OrderModelDTO orderDTO);
        Task<IEnumerable<GamesInfoDTO>> GetProductFromOrder(string orderId, string userId);
        Task DeleteProductsFromOrder(DeletedGameModelDTO deletedGamesId);
        Task<bool> Buy(string userId);
    }
}
