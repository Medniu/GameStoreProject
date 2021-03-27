using AutoMapper;
using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper _mapper;
        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddToOrder(OrderDTO orderDTO)
        {
            Product product = await context.Products
               .AsNoTracking()
               .Where(w => w.IsDeleted != true)
               .FirstOrDefaultAsync(p => p.Id == orderDTO.ProductId);

            Order order = await context.Orders
                .AsNoTracking()
                .Where(w => w.Status == false)
                .FirstOrDefaultAsync(p => p.UserId.ToString() == orderDTO.UserId);

            if (order != null && product != null)
            {
                OrderProduct orderProduct = new OrderProduct
                {
                    DateCreated = DateTime.Now,
                    OrderId = order.Id,
                    ProductId = orderDTO.ProductId,
                    Quantity = orderDTO.Quantity
                };

                await context.OrderProducts.AddAsync(orderProduct);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                if (product == null)
                {
                    return false;
                }
                else
                {
                    var newOrder = _mapper.Map<OrderDTO, Order>(orderDTO);
                    await context.Orders.AddAsync(newOrder);
                    await context.SaveChangesAsync();

                    newOrder = await context.Orders
                    .AsNoTracking()
                    .Where(w => w.Status == false)
                    .FirstOrDefaultAsync(p => p.UserId.ToString() == orderDTO.UserId);

                    OrderProduct orderProduct = new OrderProduct
                    {
                        DateCreated = DateTime.Now,
                        OrderId = newOrder.Id,
                        ProductId = orderDTO.ProductId,
                        Quantity = orderDTO.Quantity
                    };

                    await context.OrderProducts.AddAsync(orderProduct);
                    await context.SaveChangesAsync();

                    return true;
                }
            }
        }

        public async Task<ICollection<GamesInformation>> GetProductFromOrder(string orderId, string userId)
        {
            if (String.IsNullOrEmpty(orderId))
            {
                if (Guid.TryParse(userId, out var guid))
                {
                    var result = await context.Orders
                            .AsNoTracking()
                            .Where(x => x.Status == false && x.UserId == guid)                           
                            .Join(context.OrderProducts,
                            o => o.Id,
                            op => op.OrderId,
                            (o, op) => new { o, op })
                            .Where(x => x.op.IsDeleted == false)
                            .Join(context.Products,
                            op => op.op.ProductId,
                            p => p.Id,
                            (op, p) => new GamesInformation
                            {
                                Name = p.Name,
                                Category = p.Category,
                                TotalRating = p.TotalRating,
                                DateCreated = p.DateCreated,
                                Logo = p.Logo,
                                Price = p.Price,
                                Background = p.Background,
                                Rating = p.Rating,
                                Count = p.Count
                            })
                            .ToListAsync();

                    return result;
                }
                return null;
            }
            else
            {
                if (int.TryParse(orderId, out int id) && Guid.TryParse(userId, out var guid))
                {
                    var result = await context.Orders
                            .AsNoTracking()
                            .Where(x => x.Id == id && x.UserId == guid)
                            .Join(context.OrderProducts,
                            o => o.Id,
                            op => op.OrderId,
                            (o, op) => new { o, op })
                            .Where(x => x.op.IsDeleted == false)
                            .Join(context.Products,
                            op => op.op.ProductId,
                            p => p.Id,
                            (op, p) => new GamesInformation
                            {
                                Name = p.Name,
                                Category = p.Category,
                                TotalRating = p.TotalRating,
                                DateCreated = p.DateCreated,
                                Logo = p.Logo,
                                Price = p.Price,
                                Background = p.Background,
                                Rating = p.Rating,
                                Count = p.Count
                            })
                            .ToListAsync();

                    return result;
                }
                else
                {
                    return null;
                }
            }                                         
        }
       
        public async Task<bool> Delete(DeletedGameDTO deletedGamesId)
        {
            if (Guid.TryParse(deletedGamesId.UserId, out var guid))
            {               
                foreach (var item in deletedGamesId.deletedGamesID)
                {
                    var orderProduct = await context.Orders
                        .Where(x => x.UserId == guid)
                        .Join(context.OrderProducts,
                        o => o.Id,
                        op => op.OrderId,
                        (o, op) => new { o, op })
                        .Where(x => x.op.IsDeleted == false)
                        .Select(x => x.op)
                        .FirstOrDefaultAsync(op => op.ProductId == item);

                    if (orderProduct != null && orderProduct.IsDeleted == false)
                    {
                        orderProduct.IsDeleted = true;
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CompleteOrder(string userId)
        {            
            if (Guid.TryParse(userId, out var UserGuidId))
            {
                var order = await context.Orders.FirstOrDefaultAsync(p => p.UserId == UserGuidId);

                var orderProducts = await context.Orders
                        .Where(x => x.UserId == UserGuidId)
                        .Join(context.OrderProducts,
                        o => o.Id,
                        op => op.OrderId,
                        (o, op) => new { o, op })
                        .Where(x => x.op.IsDeleted == false)
                        .Select(x => x.op)
                        .ToListAsync();

                if (IsEnoughAmountOfProduct(orderProducts).Result && orderProducts.Count != 0)
                {
                    order.Status = true;
                    order.DateOrdered = DateTime.UtcNow;
                    await context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            return false;
        }
        
        private async Task<bool> IsEnoughAmountOfProduct(List<OrderProduct> orderProducts)
        {            
            foreach (var item in orderProducts)
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);

                if (product.Count < item.Quantity || product.IsDeleted == true)
                {
                    return false;
                }
                product.Count -= item.Quantity;  
            }

            await context.SaveChangesAsync();
            return true;
        }               
    }
}
