using RestWith.NET5.Model;
using RestWith.NET5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

// Classe responsável por executar as ações 
namespace RestWith.NET5.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private readonly MySQLContext _context;
        
        public PersonRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id == id);
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public Person Update(Person person)
        {
            // Se a pessoa NÃO existir no banco de dados, retornamos NULO
            if (!Exists(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id == person.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id == id);

            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
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
            return _context.Persons.Any(p => p.Id == id);
        }
    }
}

