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
        public ActionResult MyCollection()
        {
            var itemViewModel = new ItemViewModel();
            var categories = _categoryService.GetCategories();

            itemViewModel.Categories = new SelectList(categories, "ID", "Name");

            //return RedirectToAction("OtherCollection");

            return View("MyCollection",itemViewModel);


        }

        public ActionResult OtherCollection()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var userID = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            int userid = Convert.ToInt32(userID);

            IEnumerable<UsersCollections> users = _userService.GetCollectionsList(userid);

            var userViewModel = Mapper.Map<IEnumerable<UsersCollections>,IEnumerable<UsersCollectionsViewModel>> (users);

            return View(userViewModel);

        }


        public PartialViewResult ItemList()
        {
            IEnumerable<Item> items = _itemService.GetItems().ToList();

            var viewModelItem = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(items);

            return PartialView(viewModelItem);
        }
        public PartialViewResult EditItem(int id)
        {
            var item = _itemService.GetItemByID(id);
            var itemViewModel = Mapper.Map<Item, ItemViewModel>(item);

            
            var categories = _categoryService.GetCategories();

            itemViewModel.Categories = new SelectList(categories, "ID", "Name");

           
            return PartialView(itemViewModel);
        }

        [HttpPost]
        public ActionResult CreateItem(ItemViewModel viewModel)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var userID = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            viewModel.Owner = _userService.GetUserByID(Convert.ToInt32(userID));

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
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var userID = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            viewModel.Owner = _userService.GetUserByID(Convert.ToInt32(userID));
            viewModel.Owner_ID = Convert.ToInt32(userID);
            viewModel.Category = _categoryService.GetCategoryById(viewModel.Category.ID);
            viewModel.Category_ID = viewModel.Category.ID;
            viewModel.Updated_At = DateTime.Now;
            viewModel.Updated_By = Convert.ToInt32(userID);

            var initialItem = _itemService.GetItemByID(viewModel.ID);
            initialItem.Category = null;
            initialItem.Owner = null;

            Mapper.Map(viewModel, initialItem);

            _itemService.UpdateItem(initialItem);

            return Json("Success");
        }

        public ActionResult OthersCollection()
        {
            return View();
        }


    }
}