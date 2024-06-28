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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<EShopApplicationUser> entities;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<EShopApplicationUser>();
        }

      

        public IEnumerable<EShopApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public EShopApplicationUser Get(string? id)
        {
            return entities.
                Include(u=>u.ShoppingCart).
                Include("ShoppingCart.Products").
                Include("ShoppingCart.Products.Product").
                SingleOrDefault(u => u.Id == id);
        }

        public void Insert(EShopApplicationUser entity)
        {
            if(entity == null) throw new ArgumentNullException("entity");
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(EShopApplicationUser entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            entities.Update(entity);
            context.SaveChanges();
        }
        public void Delete(EShopApplicationUser entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            entities.Remove(entity);
            context.SaveChanges();
        }

    }
}
