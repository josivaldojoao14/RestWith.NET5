using RestWith.NET5.Model;
using RestWith.NET5.Model.Context;
using RestWith.NET5.Repository;
using RestWith.NET5.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

// Essa classe é responsável pelas regras de negócio, ela buscará os metodos do "Repository"
namespace RestWith.NET5.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IBookRepository _repository;

        public BookBusinessImplementation(IBookRepository repository)
        {
            _repository = repository;
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}

