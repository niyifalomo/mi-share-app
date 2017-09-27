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

        IEnumerable<User> GetUsers();

        IEnumerable<UsersCollections> GetCollectionsList(int currentUserID);

        User GetUserByID(int id);

    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICollectionAccessRepository _collectionAccessRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository,IItemRepository itemRepository,IUnitOfWork unitOfWork, ICollectionAccessRepository collectionAccessRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _collectionAccessRepository = collectionAccessRepository;
            _itemRepository = itemRepository;
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

        public User GetUserByID(int id)
        {
            var user = _userRepository.GetById(id);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetAll();
            return users;
        }

        public IEnumerable<UsersCollections> GetCollectionsList(int currentUserID)
        {
            var users = _userRepository.GetMany(x=>x.ID != currentUserID)

               .GroupJoin(
                   _collectionAccessRepository.GetMany(x => x.Requester_ID == currentUserID),
                   i => i.ID,
                   p => p.Owner_ID,
                   (i, g) =>
                       new
                       {
                           i = i,
                           g = g
                       }
               )

               .SelectMany(
                   temp => temp.g.DefaultIfEmpty(),
                   (temp, p) =>
                       new UsersCollections
                       {
                           UserID = temp.i.ID,
                           FullName = temp.i.FullName,
                           ItemCount = _itemRepository.GetMany(x => x.Owner_ID == temp.i.ID).Count(),
                           Access = (p == null) ? CollectionAccessStatus.None : p.Status

                       }
               );
            return users;


            
        }



        public int SaveUser()
        {
            return _unitOfWork.Commit();
        }


    }
}
