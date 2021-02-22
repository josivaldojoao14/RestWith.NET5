using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWith.NET5.Model
{
    // Indica qual é a tabela referente no BD (no caso, essa classe pertence à tabela "person")
    [Table("person")]
    public class Person
    {
        // Indica a qual tabela pertence no BD (no BD, o Id está em minusculo)
        [Column("id")]
        public long Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("gender")]
        public string Gender { get; set; }
    }
}
