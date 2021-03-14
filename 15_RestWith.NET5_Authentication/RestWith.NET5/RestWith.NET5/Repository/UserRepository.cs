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

        // Método responsável por atualizar as informações dos clientes
        public User RefreshUserInfo(User user)
        {
            // Se não for encontrado ninguém no BD com o mesmo ID recebido do 'user' do param, ele retorna nulo
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            // Se for encontrado alguém no BD que tenha o mesmo ID que o 'user' do param, ele armazena isso em 'result' 
            var result = _context.Users.SingleOrDefault(p => p.Id == user.Id);
            if (result != null)
            {
                // Se o 'result' for diferente de nulo, ele vai tentar atualizar as infos do usuario
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
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
