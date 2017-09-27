using Mi_Share.Data.Infrastructure;
using Mi_Share.Data.Repositories;
using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Service
{
    public interface ILoanService
    {
        IEnumerable<Loan> GetBorrowedItems(int UserID);

        IEnumerable<Loan> GetLendedItems(int UserID);

        bool ChangeLoanStatus(Loan loan,bool returned);

        bool AddLoan(Loan loan);

        Loan GetLoanByID(int id);
    }
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        public LoanService(ILoanRepository loanRepository, IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }


        //Get all items borrowed by user
        public IEnumerable<Loan> GetBorrowedItems(int UserID)
        {
            var items = _loanRepository.GetMany(x => x.Request.Requester_ID == UserID);
            return items;
        }


        //Get all loaned items
        public IEnumerable<Loan> GetLendedItems(int UserID)
        {
            var items = _loanRepository.GetMany(x => x.Request.Item.Owner_ID == UserID);
            return items;
        }

        //Update loan status--Returned/Still on loan
        public bool ChangeLoanStatus(Loan loan, bool returned)
        {
            loan.Status = returned ? LoanStatus.Returned : LoanStatus.OnLoan;
            loan.ReturnDate = DateTime.Now;
            _loanRepository.Update(loan);
            return SaveRequest() > 0 ? true : false;
        }
        public Loan GetLoanByID(int id)
        {
            var item = _loanRepository.GetById(id);
            return item;
        }

        //Create an item borrow request--Granted
        public bool AddLoan(Loan loan)
        {
            loan.Status = LoanStatus.OnLoan;
            _loanRepository.Add(loan);
            
            return SaveRequest() > 0 ? true : false;
        }
        public int SaveRequest()
        {
            return _unitOfWork.Commit();
        }



    }
}
