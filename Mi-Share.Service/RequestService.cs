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
        IEnumerable<Request> PendingItemsRequestedFor(int UserID);
        IEnumerable<CollectionAccess> PendingLibrariesRequestedFor(int UserID);
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
        public bool AddCollectionRequest(CollectionAccess request)
        {
            request.Status = CollectionAccessStatus.Pending;
            _collectionAccessRepository.Add(request);

            return SaveRequest() > 0 ? true : false;
            
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

        public int SaveRequest()
        {
            return _unitOfWork.Commit();
        }
    }
}
