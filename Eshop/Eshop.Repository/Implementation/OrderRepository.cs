using Eshop.Domain.Domain;
using Eshop.Domain.Identity;
using Eshop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.ProductInOrder)
                .Include(z => z.OrderOwner)
                .Include("ProductInOrder.SelectedProduct")
                .ToList();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                .Include(z => z.ProductInOrder)
                .Include(z => z.OrderOwner)
                .Include("ProductInOrder.SelectedProduct")
                .SingleOrDefault(z => z.Id == model.Id);
        }
    }
}
