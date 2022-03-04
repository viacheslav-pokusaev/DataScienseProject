using DataScienseProject.Context;
using DataScienseProject.Entities;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Services
{
    public class GetDataRefactorService : IGetDataService
    {
        private readonly DataScienceProjectDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        private List<GalleryModel> galleryModelsBuff = new List<GalleryModel>();

        private const string SHORT_DESCRIPTION_ELEMENT_TYPE_NAME = "Header Description";
        private const string IMAGE_ELEMENT_NAME = "Header Image";
        public GetDataRefactorService(DataScienceProjectDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }
        public GalleryResult GetGalleryPageData(string groupName, HttpContext http, FilterModel filter)
        {
            var isAuthorizedResult = _authorizationService.IsAuthorized(http, filter == null ? groupName : filter.GroupName);
            if (isAuthorizedResult.StatusCode == 403) return new GalleryResult() { StatusModel = isAuthorizedResult };

            var selectFilterTags = (filter != null && filter?.TagsName.Length > 0) ? FindTags(_context.Tags.ToList(), filter.TagsName.ToList()) : _context.Tags.ToList();

            var selectFilterExecutors = (filter != null && filter?.ExecutorsName.Length > 0) ? FindExecutors(_context.Executors.ToList(), filter.ExecutorsName.ToList()) : _context.Executors.ToList();

            return new GalleryResult();
        }

        public List<Tag> FindTags(List<Tag> tags, List<string> tagsName)
        {
            var res = new List<Tag>();

            tagsName.ForEach(tn =>
            {
                var findRes = tags.Where(t => t.Name == tn);
                if (findRes != null || findRes?.Count() > 0)
                {
                    res.AddRange(findRes);
                }
            });

            return res;
        }

        public List<Executor> FindExecutors(List<Executor> executors, List<string> executorsName)
        {
            var res = new List<Executor>();

            executorsName.ForEach(en =>
            {
                var findRes = executors.Where(t => t.ExecutorName == en);
                if (findRes != null || findRes?.Count() > 0)
                {
                    res.AddRange(findRes);
                }
            });

            return res;
        }

        public MainPageModel GetMainPageData(int id)
        {
            throw new NotImplementedException();
        }

        public bool isFilterContainsCheck(List<string> checkElements, string[] findElements)
        {
            throw new NotImplementedException();
        }

        public bool UniqualityCheck(GalleryModel galleryModel, List<GalleryModel> currentList)
        {
            throw new NotImplementedException();
        }
    }
}
