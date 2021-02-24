using RestWith.NET5.Model;
using RestWith.NET5.Model.Context;
using RestWith.NET5.Repository;
using RestWith.NET5.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

// Essa classe é responsável pelas regras de negócio, ela buscará os metodos do "Repository"
namespace RestWith.NET5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}

