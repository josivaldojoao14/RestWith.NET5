using RestWith.NET5.Model;
using System.Collections.Generic;

namespace RestWith.NET5.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindByID(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);

    }
}
