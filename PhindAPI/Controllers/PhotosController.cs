using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlickrNet;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PhindAPI.Models;

namespace PhindAPI.Controllers
{
    public class PhotosController : BaseApiController
    {
        public Flickr Context { get; set; }

        public PhotosController()
        {
            this.Context = new Flickr("0c9642199bdc3afa4bc037439fe09c71", "e62b33c2ea0a4f93");
        }


        [HttpGet]
        [ActionName("user-photos")]
        public IEnumerable<PhotoModel> GetUserPhotos(string username)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                UserModel user = this.GetUser(username);

                var photos = this.Context.PhotosSearch(new PhotoSearchOptions()
                {
                    UserId = user.Id,
                    Page = 1,
                    PerPage = 100
                }).Select(x => new PhotoModel
                {
                    Id = x.PhotoId,
                    Title = x.Title,
                    LargeUrl = x.LargeUrl,
                    LargeSquareThumbnailUrl = x.LargeSquareThumbnailUrl,
                    Small320Url = x.Small320Url
                });

                return photos;
            });
        }

        [HttpGet]
        [ActionName("title-photos")]
        public IEnumerable<PhotoModel> GetPhotosByTitle(string title)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                var photos = this.Context.PhotosSearch(new PhotoSearchOptions()
                {
                    Text = title,
                    SortOrder = PhotoSearchSortOrder.Relevance,
                    Page = 1,
                    PerPage = 100
                }).Select(x => new PhotoModel
                {
                    Id = x.PhotoId,
                    Title = x.Title,
                    LargeUrl = x.LargeUrl,
                    LargeSquareThumbnailUrl = x.LargeSquareThumbnailUrl,
                    Small320Url = x.Small320Url
                });

                return photos;
            });
        }

        private UserModel GetUser(string username)
        {
            if (!String.IsNullOrEmpty(username))
            {
                try
                {
                    var found = this.Context.PeopleFindByUserName(username);
                    var model = new UserModel
                    {
                        Id = found.UserId,
                        Username = found.UserName
                    };

                    return model;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }
}
