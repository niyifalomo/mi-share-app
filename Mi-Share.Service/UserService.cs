using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Model;
using Mi_Share.Data.Infrastructure;
using Mi_Share.Data.Repositories;

namespace Mi_Share.Service
{
    public interface IUserService {
        bool CreateUser(User user);
        User GetUserByCredentials(string username, string password);

    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository,IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public bool CreateUser(User user)
        {

            _userRepository.Add(user);

            return SaveUser() > 0 ? true : false;

        }

        public User GetUserByCredentials(string username, string password)
        {
            var user = _userRepository.Get(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);

            return user;
        }

        public int SaveUser()
        {
            return _unitOfWork.Commit();
        }


    }
}
