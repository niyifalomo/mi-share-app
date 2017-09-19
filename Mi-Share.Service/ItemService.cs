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
    public interface IItemService
    {
        bool CreateItem(Item item);
        bool UpdateItem(Item item);
        IEnumerable<Item> GetItems();

        Item GetItemByID(int id);




    }
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public bool CreateItem(Item item)
        {

            _itemRepository.Add(item);

            return SaveItem() > 0 ? true : false;

        }

        public bool UpdateItem(Item item)
        {
            _itemRepository.Update(item);

            return SaveItem() > 0 ? true : false;
        }
        public IEnumerable<Item> GetItems()
        {
            var items = _itemRepository.GetAll();
            return items;
        }

        public Item GetItemByID(int id)
        {
            var item = _itemRepository.GetById(id);
            return item;
        }


        public int SaveItem()
        {
            return _unitOfWork.Commit();
        }


    }
}
