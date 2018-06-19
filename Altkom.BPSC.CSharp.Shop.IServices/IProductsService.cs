using Altkom.BPSC.CSharp.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.BPSC.CSharp.Shop.IServices
{

    public abstract class SearchCriteria
    {

    }

    public class ProductSearchCriteria : SearchCriteria
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal? UnitPrice { get; set; }
    }

    public interface IProductsService : IEntityService<Product, int>
    {
        IList<Product> Get(ProductSearchCriteria criteria);
    }


    public interface ICustomersService : IEntityService<Customer, int>
    {
    }

    public interface IOrdersService : IEntityService<Order, int>
    {
    }

    // interfejs generyczny
    public interface IEntityService<TEntity, TKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TKey id);
        TEntity Get(TKey id);
        IList<TEntity> Get();
    }


    public class MockCustomersService : MockEntitiesService<Customer>
    {
        public MockCustomersService()
        {
        }
    }

    public class MockEntitiesService<TEntity> : IEntityService<TEntity, int>
        where TEntity : Base
    {
        private IList<TEntity> entities = new List<TEntity>();

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public TEntity Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public IList<TEntity> Get()
        {
            return entities;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }


    public class MockProductsService : IProductsService
    {
        private IList<Product> products = new List<Product>();

        public void Add(Product entity)
        {
            products.Add(entity);
        }

        public IList<Product> Get(ProductSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            return products.SingleOrDefault(p => p.Id == id);
        }

        public IList<Product> Get()
        {
            return products;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }



}
