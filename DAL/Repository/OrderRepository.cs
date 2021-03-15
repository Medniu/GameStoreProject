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
               .Where(w => w.IsDeleted != true)
               .FirstOrDefaultAsync(p => p.Id == orderDTO.ProductId);

            Order order = await context.Orders
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

        public async Task<ICollection<GamesInformation>> GetProductFromOrder(string orderId)
        {
            if (Guid.TryParse(orderId, out var guid))
            {
                var result = await context.Orders
                        .Where(x => x.Status == false)
                        .Where(x => x.UserId == guid)
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

                if (int.TryParse(orderId, out int id))
                {
                    var result = await context.Orders
                        .Where(x=>x.Status ==false)
                        .Where(x => x.Id == id)
                        .Join(context.OrderProducts,
                        o => o.Id,
                        op => op.OrderId,
                        (o, op) => new { o, op })
                        .Where(x=>x.op.IsDeleted == false)
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
        //i really dont know but if i try to get orderProduct using Async method for example FirsOrDefaultAsync instead of default method
        //it doesnt work, but in ProductRepository.cs method Delete work excellent
        public async Task<bool> Delete(DeletedGameDTO deletedGamesId)
        {
            if (Guid.TryParse(deletedGamesId.UserId, out var guid))
            {
                foreach (var item in deletedGamesId.deletedGamesID)
                {
                    var orderProduct = context.Orders
                        .Where(x => x.UserId == guid)
                        .Join(context.OrderProducts,
                        o => o.Id,
                        op => op.OrderId,
                        (o, op) => new { o, op })
                        .Where(x => x.op.IsDeleted == false)
                        .Select(x => x.op)
                        .FirstOrDefault(op => op.ProductId == item);

                    if (orderProduct != null && orderProduct.IsDeleted == false)
                    {
                        orderProduct.IsDeleted = true;
                    }
                }
                //SaveChangesAsync doesnt work too
                context.SaveChanges();
                return true;

                //Another way to find and update property, but anyway doesnt work with ToListAsync() and SaveChangesAsync(); 
                //But first way faster for about 100 ms :)

                //var orderProduct = context.Orders
                //        .Where(x => x.UserId == guid)
                //        .Join(context.OrderProducts,
                //        o => o.Id,
                //        op => op.OrderId,
                //        (o, op) => new { o, op })
                //        .Where(x => x.op.IsDeleted == false)
                //        .Select(x => x.op).ToList();

                //    foreach(var product in orderProduct)
                //    {
                //        product.IsDeleted = true;
                //    }

                //    context.SaveChanges();                      
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> CompleteOrder(string userId)
        {
            //if i change FirstOrDefault to ...DefaultAsync while trying to find order and the same with orderProducts its doesnt work too
            
            if (Guid.TryParse(userId, out var UserGuidId))
            {
                var order = context.Orders.FirstOrDefault(p => p.UserId == UserGuidId);

                var orderProducts = context.Orders
                        .Where(x => x.UserId == UserGuidId)
                        .Join(context.OrderProducts,
                        o => o.Id,
                        op => op.OrderId,
                        (o, op) => new { o, op })
                        .Where(x => x.op.IsDeleted == false)
                        .Select(x => x.op).ToList();    
                
                if (IsEnoughAmountOfProduct(orderProducts).Result)
                {
                    //but here SaveChangesAsync work well 
                    order.Status = true;
                    await context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        //and here all Async methods work well , its really magic for me why its happened
        private async Task<bool> IsEnoughAmountOfProduct(List<OrderProduct> orderProducts)
        {
            var check = true;
            foreach (var item in orderProducts)
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);

                if (product.Count < item.Quantity)
                {
                    check = false;
                }

                product.Count -= item.Quantity;  
            }
            await context.SaveChangesAsync();
            return check;
        }               
    }
}
