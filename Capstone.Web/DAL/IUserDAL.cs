using Capstone.Web.DAL;

namespace Capstone.Web.DAL
{

    public interface IUserDAL
    {
        User GetUser(string email);
        void SaveUser(User user);
    }

}