using RestWith.NET5.Data.Converter.Contract;
using RestWith.NET5.Data.VO;
using RestWith.NET5.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWith.NET5.Data.Converter.Implementations
{
    // Essa classe é responsável por converter a entidade VO em PERSON e PERSON em VO
    // É preciso fazer alterações no BUSINESS para que ele receba os VO's dessa classe
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        // VO para PERSON
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };   
        }

        // PERSON para VO
        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        // Faz a conversão da lista de PERSON para PersonVO
        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        // Faz a conversão da lista de PersonVO para Person
        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

    }
}
