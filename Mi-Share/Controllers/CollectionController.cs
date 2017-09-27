using AutoMapper;
using Mi_Share.Service;
using Mi_Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mi_Share.Model;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Mi_Share.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;
        private readonly IUserService _userService;
        public CollectionController(ICategoryService categoryService, IItemService itemService, IUserService userService)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            _userService = userService;
        }
        // GET: Collection
        public ActionResult Index()
        {
            //return View();
            
           return RedirectToAction("MyCollection");
        }

        //View Items in logged in user's collection
        public ActionResult MyCollection()
        {
            var itemViewModel = new ItemViewModel();
            var categories = _categoryService.GetCategories();

            itemViewModel.Categories = new SelectList(categories, "ID", "Name");

            //return RedirectToAction("OtherCollection");

            return View("MyCollection",itemViewModel);


        }

        //View other users' libraries
        public ActionResult OthersCollection()
        {
            int userid = GetUserID();

            IEnumerable<UsersCollections> users = _userService.GetCollectionsList(userid);

            var userViewModel = Mapper.Map<IEnumerable<UsersCollections>,IEnumerable<UsersCollectionsViewModel>> (users);

            return View(userViewModel);

        }


        //View other user's collection/library
        public ActionResult ViewOthersCollection(int userId)
        {
            ViewBag.UserName = _userService.GetUserByID(userId).FullName;
            IEnumerable<Item> items = _itemService.GetUserItems(userId).ToList();
            var viewModelItem = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(items);
            return View(viewModelItem);

        }


        //Returns list of items in collection
        public PartialViewResult ItemList()
        {
            int userID = GetUserID();
            IEnumerable<Item> items = _itemService.GetUserItems(userID).ToList();

            var viewModelItem = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(items);

            return PartialView(viewModelItem);
        }

        //Update an item
        public PartialViewResult EditItem(int id)
        {
            var item = _itemService.GetItemByID(id);
            var itemViewModel = Mapper.Map<Item, ItemViewModel>(item);

            
            var categories = _categoryService.GetCategories();

            itemViewModel.Categories = new SelectList(categories, "ID", "Name");

           
            return PartialView(itemViewModel);
        }

        //Add a new Item to library

        [HttpPost]
        public ActionResult CreateItem(ItemViewModel viewModel)
        {
            int userID = GetUserID();

            viewModel.Owner = _userService.GetUserByID(userID);

            viewModel.Category = _categoryService.GetCategoryById(viewModel.Category.ID);

            var data = Mapper.Map<ItemViewModel, Item>(viewModel);

            _itemService.CreateItem(data);

            return Json("Success");
        }

        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            var item = _itemService.GetItemByID(id);


            string response = _itemService.DeleteItem(item) ? "Success" : "False";

            return Json(response);
            
        }


        [HttpPost]
        public ActionResult UpdateItem(ItemViewModel viewModel)
        {

            int userID = GetUserID();
            viewModel.Owner = _userService.GetUserByID(userID);
            viewModel.Owner_ID = userID;
            viewModel.Category = _categoryService.GetCategoryById(viewModel.Category.ID);
            viewModel.Category_ID = viewModel.Category.ID;
            viewModel.Updated_At = DateTime.Now;
            viewModel.Updated_By = userID;

            var initialItem = _itemService.GetItemByID(viewModel.ID);
            initialItem.Category = null;
            initialItem.Owner = null;

            Mapper.Map(viewModel, initialItem);

            _itemService.UpdateItem(initialItem);

            return Json("Success");
        }

        public int GetUserID()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var userID = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return Convert.ToInt32(userID);
        }
        


    }
}