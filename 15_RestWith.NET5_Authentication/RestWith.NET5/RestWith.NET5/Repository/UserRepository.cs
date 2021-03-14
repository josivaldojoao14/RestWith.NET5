using RestWith.NET5.Data.VO;
using RestWith.NET5.Model;
using RestWith.NET5.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWith.NET5.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;
        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            // Quando a senha é recebida no "user", ela não está criptografada, então precisamos criptografar
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());

            // Após criptografar, fazemos a comparação da senha criptografada acima com a senha do BD
            return _context.Users.FirstOrDefault(u => (u.Username == user.UserName) && (u.Password == pass));
        }

        // Método responsável por encriptar a senha
        private string ComputeHash(string input, SHA256CryptoServiceProvider Algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = Algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
