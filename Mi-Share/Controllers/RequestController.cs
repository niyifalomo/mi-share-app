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
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IUserService _userService;
        public RequestController(IRequestService requestService, IUserService userService)
        {
            _requestService = requestService;
            _userService = userService;
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

        [HttpPost]
        public ActionResult RequestAccess(int userId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var userID = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
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

        public int GetUserID()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var userID = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return Convert.ToInt32(userID);
        }
    }
}