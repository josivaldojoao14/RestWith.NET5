using System.Collections.Generic;

namespace RestWith.NET5.Data.Converter.Contract
{
    // Essa interface recebe dois tipos genéricos, "O" de origem e "D" de destino
    public interface IParser<O, D>
    {
        D Parse(O origin);
        // Recebe um objeto de origem e retornar um destino
        List<D> Parse(List<O> origin);
    }
}
