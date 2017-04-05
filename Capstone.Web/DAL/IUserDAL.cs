using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{

    public interface IUserDAL
    {
        User GetUser(string email);
        void SaveUser(User user);
    }

}