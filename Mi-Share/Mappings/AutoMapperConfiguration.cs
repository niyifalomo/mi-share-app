using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Mi_Share.Model;
using Mi_Share.ViewModels;

namespace Mi_Share.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {

            Mapper.Initialize(x =>
            {
                x.CreateMap<Item, ItemViewModel>()
                    .ForMember(c=>c.Categories,option=>option.Ignore());
                x.CreateMap<ItemViewModel, Item>()
                    .ForMember(c => c.DateCreated, option => option.Ignore());


                x.CreateMap<Category, CategoryViewModel>();
                x.CreateMap<CategoryViewModel, Category>()
                    .ForMember(c => c.Items, option => option.Ignore());

                x.CreateMap<Request, RequestViewModel>();
                x.CreateMap<RequestViewModel, Request>()
                    .ForMember(c => c.DateCreated, option => option.Ignore())
                    .ForMember(c => c.Status, option => option.Ignore());



                x.CreateMap<User, UserViewModel>();
                x.CreateMap<UserViewModel, User>()
                    .ForMember(c => c.ItemRequests, option => option.Ignore())
                    .ForMember(c => c.SentCollectionAccessRequests, option => option.Ignore())
                    .ForMember(c => c.RecievedCollectionAccess, option => option.Ignore());
                    

                x.CreateMap<Loan, LoanViewModel>();
                x.CreateMap<LoanViewModel, Loan>();

                x.CreateMap<UsersCollections, UsersCollectionsViewModel>();
                x.CreateMap<UsersCollectionsViewModel, UsersCollections>();

                x.CreateMap<CollectionAccess, CollectionAccessViewModel>();
                x.CreateMap<CollectionAccessViewModel, CollectionAccess>()
                    .ForMember(c=>c.Status,option=>option.Ignore())
                    .ForMember(c => c.DateRequested, option => option.Ignore());
            });
        }
    }
}