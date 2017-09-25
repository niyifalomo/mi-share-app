using AutoMapper;
using Mi_Share.Model;
using Mi_Share.Service;
using Mi_Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Mi_Share.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;
        public LoanController(ILoanService loanService, IUserService userService, IItemService itemService)
        {
            _loanService = loanService;
            _userService = userService;
            _itemService = itemService;
        }
        // GET: Lending
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoanedItems()
        {
            
            return View();
        }
        public PartialViewResult LoanedItemsList()
        {
            int userID = GetUserID();
            IEnumerable<Loan> loaneditems = _loanService.GetLendedItems(userID).ToList();

            var viewModelItem = Mapper.Map<IEnumerable<Loan>, IEnumerable<LoanViewModel>>(loaneditems);
            

            return PartialView(viewModelItem);

        }
        public ActionResult BorrowedItems()
        {
            int userID = GetUserID();
            IEnumerable<Loan> borroweditems = _loanService.GetBorrowedItems(userID).ToList();

            var viewModelItem = Mapper.Map<IEnumerable<Loan>, IEnumerable<LoanViewModel>>(borroweditems);


            return View(viewModelItem);
        }

        [HttpPost]
        public ActionResult ReturnLoanedItem(int loanId)
        {

            var loan = _loanService.GetLoanByID(loanId);

            if (_loanService.ChangeLoanStatus(loan, true))
            {
                //make item available
                _itemService.ChangeItemStatus(loan.Request.Item, true);
                return Json("Success");

            }
            else
            return Json("Failure");
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