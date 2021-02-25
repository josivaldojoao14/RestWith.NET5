using RestWith.NET5.Data.Converter.Implementations;
using RestWith.NET5.Data.VO;
using RestWith.NET5.Model;
using RestWith.NET5.Repository.Generic;
using System.Collections.Generic;

// Essa classe é responsável pelas regras de negócio, ela buscará os metodos do "Repository"
// Após essas mudanças, é preciso indicar ao CONTROLLER que usamos o 'VO'
namespace RestWith.NET5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        // Para receber os VO's, temos que adicionar esse novo atributo
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        // É preciso fazer a conversão para entidade
        // Quando o objeto chega, ele é um VO e não dá pra criar direto no BD, então convertemos para ENTITY
        // Depois que ele é convertido(parse), é possível criar(Create) no BD "_repository.Create(personEntity);"
        // Por fim, retornamos essa entitade para o VO e devolve a resposta
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}

