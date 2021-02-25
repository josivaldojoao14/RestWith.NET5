using RestWith.NET5.Data.Converter.Implementations;
using RestWith.NET5.Model;
using RestWith.NET5.Repository.Generic;
using System.Collections.Generic;

// Essa classe é responsável pelas regras de negócio, ela buscará os metodos do "Repository"
namespace RestWith.NET5.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public BookVO Create(BookVO book)
        {
            var bookConverter = _converter.Parse(book);
            bookConverter = _repository.Create(bookConverter);
            return _converter.Parse(bookConverter);
        }

        public BookVO Update(BookVO book)
        {
            var bookConverter = _converter.Parse(book);
            bookConverter = _repository.Update(bookConverter);
            return _converter.Parse(bookConverter);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}

