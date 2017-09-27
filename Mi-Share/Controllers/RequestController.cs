using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Mi_Share.Service;
using Mi_Share.ViewModels;
using System.Security.Claims;
using Mi_Share.Model;

namespace Mi_Share.Controllers
{
    //[Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IItemService _itemService;
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        public RequestController(IRequestService requestService, IUserService userService, IItemService itemService,
                ILoanService loanService)
        {
            _requestService = requestService;
            _loanService = loanService;
            _userService = userService;
            _itemService = itemService;
        }
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyRequests()
        {
            return View();
        }

        public ActionResult PendingRequests()
        {
            return View();
        }

        //Send request to view Collection
        [HttpPost]
        public ActionResult RequestAccess(int userId)
        {
            int userID = GetUserID();
            var requestorID = Convert.ToInt32(userID);

            var viewModel = new CollectionAccessViewModel();

            viewModel.Requester = _userService.GetUserByID(requestorID);
            viewModel.Requester_ID = requestorID;
            viewModel.Owner = _userService.GetUserByID(userId);
            viewModel.Owner_ID = userId;

            var data = Mapper.Map<CollectionAccessViewModel, CollectionAccess>(viewModel);

            _requestService.AddCollectionRequest(data);

            return Json("Success");
        }

        //Send request to borrow item to Collection/Item owner
        [HttpPost]
        public ActionResult SendBorrowRequest(int itemId)
        {
            int userID = GetUserID();
            var requestorID = Convert.ToInt32(userID);

            var viewModel = new RequestViewModel();

            viewModel.Requester = _userService.GetUserByID(requestorID);
            viewModel.Requester_ID = requestorID;
            viewModel.Item = _itemService.GetItemByID(itemId);
            viewModel.Item_ID = itemId;
            

            var data = Mapper.Map<RequestViewModel, Request>(viewModel);

            _requestService.AddItemBorrowRequest(data);

            return Json("Success");

        }

        [HttpPost]
        public ActionResult GrantCollectionAccessRequest(int collectionId)
        {
            var request = _requestService.GetCollectionRequest(collectionId);

            string response = _requestService.UpdateCollectionRequest(request,true) ? "Success" : "False";

            return Json(response);

        }

        [HttpPost]
        public ActionResult GrantBorrowRequest(int requestId)
        {
            var request = _requestService.GetItemRequest(requestId);

            if (_requestService.UpdateItemRequest(request, true))
            {
                //update item  status to not available
                var item = _itemService.GetItemByID(request.Item.ID);
                item.Status = ItemStatus.Borrowed;
                _itemService.UpdateItem(item);

                //create loan

                var loan = new LoanViewModel();
                loan.BeginDate = DateTime.Now;
                loan.Request = request;
                loan.Request_ID = requestId;

                var data = Mapper.Map<LoanViewModel, Loan>(loan);
                _loanService.AddLoan(data);
                

                return Json("true");
            }
            else
            {
                return Json("false");
            }
            

        }

        [HttpPost]
        public ActionResult DenyBorrowRequest(int requestId)
        {
            var request = _requestService.GetItemRequest(requestId);

            string response = _requestService.UpdateItemRequest(request, false) ? "Success" : "False";
            
                return Json(response);
           


        }

        [HttpPost]
        public ActionResult DenyCollectionAccessRequest(int collectionId)
        {
            var request = _requestService.GetCollectionRequest(collectionId);

            string response = _requestService.UpdateCollectionRequest(request, false) ? "Success" : "False";

            return Json(response);

        }


        [HttpPost]
        public ActionResult CancelCollectionAccessRequest(int collectionId)
        {

            var request = _requestService.GetCollectionRequest(collectionId);

            string response = _requestService.DeleteCollectionRequest(request) ? "Success" : "False";

            return Json(response);
            
        }

        [HttpPost]
        public ActionResult CancelSentItemRequest(int requestId)
        {
            var request = _requestService.GetItemRequest(requestId);

            string response = _requestService.DeleteItemRequest(request) ? "Success" : "False";

            return Json(response);

        }

        public PartialViewResult PendingItemsRequestedFor()
        {
            int userID = GetUserID();
            IEnumerable<Request> items = _requestService.PendingItemsRequestedFor(userID).ToList();

            var viewModelItem = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestViewModel>>(items);

            return PartialView(viewModelItem);


        }
        public PartialViewResult PendingLibrariesRequestedFor()
        {

            int userID = GetUserID();
            IEnumerable<CollectionAccess> libraries = _requestService.PendingLibrariesRequestedFor(userID).ToList();

            var viewModelItem = Mapper.Map<IEnumerable<CollectionAccess>, IEnumerable<CollectionAccessViewModel>>(libraries);


            return PartialView(viewModelItem);
        }



        public PartialViewResult MyItemsRequestedFor()
        {
            int userID = GetUserID();
            IEnumerable<Request> items = _requestService.MyItemsRequestedFor(userID).ToList();

            var viewModelItem = Mapper.Map<IEnumerable<Request>, IEnumerable<RequestViewModel>>(items);


            return PartialView(viewModelItem);
        }
        public PartialViewResult MyLibraryRequests()
        {

            int userID = GetUserID();
            IEnumerable<CollectionAccess> libraries = _requestService.MyLibraryRequests(userID).ToList();

            var viewModelItem = Mapper.Map<IEnumerable<CollectionAccess>, IEnumerable<CollectionAccessViewModel>>(libraries);


            return PartialView(viewModelItem);
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