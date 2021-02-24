using Microsoft.EntityFrameworkCore;
using RestWith.NET5.Model.Base;
using RestWith.NET5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWith.NET5.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly MySQLContext _context;

        private readonly DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindByID(long id)
        {
            return dataset.SingleOrDefault(p => p.Id == id);
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else 
            {
                return null;
            }
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id == id);
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id == id);
        }
    }
}
