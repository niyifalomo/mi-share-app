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
    public interface IRequestService {
        bool AddCollectionRequest(CollectionAccess request);
        bool AddItemBorrowRequest(Request request);

        bool UpdateCollectionRequest(CollectionAccess request,bool grant);

        IEnumerable<Request> PendingItemsRequestedFor(int UserID);
        IEnumerable<CollectionAccess> PendingLibrariesRequestedFor(int UserID);
        IEnumerable<Request> MyItemsRequestedFor(int userID);
        IEnumerable<CollectionAccess> MyLibraryRequests(int userID);

        CollectionAccess GetCollectionRequest(int id);

        bool DeleteCollectionRequest(CollectionAccess request);
    }
    public class RequestService : IRequestService
    {
        private readonly ICollectionAccessRepository _collectionAccessRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RequestService(ICollectionAccessRepository collectionAccessRepository, IUnitOfWork unitOfWork,
            IRequestRepository requestRepository)
        {
            _collectionAccessRepository = collectionAccessRepository;
            _requestRepository = requestRepository;
            _unitOfWork = unitOfWork;
        }

        public bool AddItemBorrowRequest(Request request)
        {
            request.Status = RequestStatus.Pending;
            _requestRepository.Add(request);

            return SaveRequest() > 0 ? true : false;
        }
        public bool AddCollectionRequest(CollectionAccess request)
        {
            request.Status = CollectionAccessStatus.Pending;
            _collectionAccessRepository.Add(request);

            return SaveRequest() > 0 ? true : false;
            
        }

        public bool UpdateCollectionRequest(CollectionAccess request, bool grant)
        {
            
            request.Status = (grant)? CollectionAccessStatus.Granted : CollectionAccessStatus.Denied;
            _collectionAccessRepository.Update(request);

            return SaveRequest() > 0 ? true : false;

        }
        public IEnumerable<Request> MyItemsRequestedFor(int userID)
        {
            var requests = _requestRepository.GetMany(x => x.Item.Owner_ID == userID);
            return requests;

        }

        public IEnumerable<CollectionAccess> MyLibraryRequests(int userID)
        {
            var requests = _collectionAccessRepository.GetMany(x => x.Owner_ID == userID);
            return requests;

        }


        public IEnumerable<CollectionAccess> PendingLibrariesRequestedFor(int UserID)
        {
            var requests = _collectionAccessRepository.GetMany(x => x.Requester_ID == UserID);
            return requests;
        }

        public IEnumerable<Request> PendingItemsRequestedFor(int UserID)
        {
            var requests = _requestRepository.GetMany(x => x.Requester_ID == UserID);
            return requests;
        }

        public CollectionAccess GetCollectionRequest(int id)
        {
            var request = _collectionAccessRepository.Get(x => x.ID == id);
            return request;

        }

        public bool DeleteCollectionRequest(CollectionAccess request)
        {
            _collectionAccessRepository.Delete(request);
            return SaveRequest() > 0 ? true : false;
        }
        public int SaveRequest()
        {
            return _unitOfWork.Commit();
        }
    }
}
