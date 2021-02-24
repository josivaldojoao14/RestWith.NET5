using RestWith.NET5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWith.NET5.Repository.Implementations
{
    public interface IBookRepository
    {
        Book Create(Book book);
        Book FindByID(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
        bool Exists(long id);
    }
}
