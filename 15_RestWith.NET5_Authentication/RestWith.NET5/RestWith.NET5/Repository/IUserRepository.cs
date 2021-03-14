using RestWith.NET5.Data.VO;
using RestWith.NET5.Model;

namespace RestWith.NET5.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
    }
}
