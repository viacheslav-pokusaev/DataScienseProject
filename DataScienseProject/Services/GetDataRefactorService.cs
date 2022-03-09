using DataScienseProject.Context;
using DataScienseProject.Entities;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Services
{
    public class GetDataRefactorService : IGetDataService
    {
        private readonly DataScienceProjectDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        //private List<GalleryModel> galleryModelsBuff = new List<GalleryModel>();

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

            var galleryResult = new GalleryResult();
            galleryResult.GalleryModels = new List<GalleryModel>();

            //select Views from db
            var groupDataSelect = _context.GroupViews.Join(_context.Groups, gv => gv.GroupKey, g => g.GroupKey, (gv, g) => new
            {
                ViewKey = gv.ViewKey,
                GroupName = g.GroupName,
                IsDeleted = g.IsDeleted
            })
           .Join(_context.Views, gv => gv.ViewKey, v => v.ViewKey, (gv, v) => new
           {
               ViewName = v.ViewName,
               OrderNumber = v.OrderNumber,
               ViewKey = v.ViewKey,
               IsViewDeleted = v.IsDeleted,
               gv = new { GroupName = gv.GroupName, IsGroupDeleted = gv.IsDeleted }
           })
           .Join(_context.ViewTags, v => v.ViewKey, vt => vt.ViewKey, (v, vt) => new
           {
               ViewName = v.ViewName,
               OrderNumber = v.OrderNumber,
               ViewKey = v.ViewKey,
               IsViewDeleted = v.IsViewDeleted,
               gv = v.gv,
               TagKey = vt.TagKey
           })
           .Join(_context.Tags, vt => vt.TagKey, t => t.TagKey, (vt, t) => new
           {
               ViewName = vt.ViewName,
               OrderNumber = vt.OrderNumber,
               ViewKey = vt.ViewKey,
               IsViewDeleted = vt.IsViewDeleted,
               gv = vt.gv,
               TagName = t.Name
           })
           .Join(_context.ViewExecutors, t => t.ViewKey, ve => ve.ViewKey, (t, ve) => new
           {
               ViewName = t.ViewName,
               OrderNumber = t.OrderNumber,
               ViewKey = t.ViewKey,
               TagName = t.TagName,
               IsViewDeleted = t.IsViewDeleted,
               gv = t.gv,
               ExecutorKey = ve.ExecutorKey
           })
           .Join(_context.Executors, ve => ve.ExecutorKey, e => e.ExecutorKey, (ve, e) => new GroupDataSelectModel
           {
               ViewName = ve.ViewName,
               OrderNumber = ve.OrderNumber,
               ViewKey = ve.ViewKey,
               TagName = ve.TagName,
               IsViewDeleted = ve.IsViewDeleted,
               GroupName = ve.gv.GroupName,
               IsGroupDeleted = ve.gv.IsGroupDeleted,
               ExecutorKey = ve.ExecutorKey,
               ExecutorName = e.ExecutorName
           });

            //apply filters if it's exist
            groupDataSelect = (filter == null || (filter?.TagsName.Length == 0 && filter?.ExecutorsName.Length == 0)) ? groupDataSelect : Filter(groupDataSelect, filter);

           //select nessesery data to model
           var groupSelectResult = groupDataSelect.Where(x => x.GroupName == groupName && x.IsGroupDeleted == false && x.IsViewDeleted == false).Select(s => new GroupData
           {
               ViewName = s.ViewName,
               ViewKey = (int)s.ViewKey,
               OrderNumber = s.OrderNumber,
               TagName = s.TagName,
               ExecutorName = s.ExecutorName
           }).OrderBy(ob => ob.OrderNumber).AsNoTracking().ToList();

            //configure data
            groupSelectResult.ForEach(gds =>
            {
                var executorDataSelect = _context.ViewExecutors.Include(e => e.ExecutorKeyNavigation).Include(er => er.ExecutorRoleKeyNavigation)
                    .Where(x => x.ViewKey == gds.ViewKey && x.IsDeleted == false).Select(s => new ExecutorModel
                    {
                        ExecutorName = s.ExecutorKeyNavigation.ExecutorName,
                        RoleName = s.ExecutorRoleKeyNavigation.RoleName
                    }).AsNoTracking().ToList();

                var executorNames = new List<string>();
                executorDataSelect.ForEach(eds => executorNames.Add(eds.ExecutorName));

                var tagDataSelect = _context.ViewTags.Include(t => t.TagKeyNavigation).Where(x => x.ViewKey == gds.ViewKey && x.IsDeleted == false).Select(s => new
                {
                    Name = s.TagKeyNavigation.Name
                }).AsNoTracking().ToList();

                var tagNames = new List<string>();
                tagDataSelect.ForEach(tds => tagNames.Add(tds.Name));

                var shortDescriptionDataSelect = new List<string>();
                var shortDescriptionData = _context.Views.Join(_context.ViewElements, v => v.ViewKey, ve => ve.ViewKey, (v, ve) => new
                {
                    IsDeleted = ve.IsDeleted,
                    ElementKey = ve.ElementKey,
                    ViewKey = v.ViewKey
                }).Join(_context.Elements, ve => ve.ElementKey, e => e.ElementKey, (ve, e) => new
                {
                    Value = e.Value,
                    ElementName = e.ElementName,
                    ViewKey = ve.ViewKey,
                    ElementTypeKey = e.ElementTypeKey,
                    ViewElementIsDeleted = ve.IsDeleted,
                    ElementIsDeleted = e.IsDeleted
                }).Join(_context.ElementTypes, e => e.ElementTypeKey, et => et.ElementTypeKey, (e, et) => new
                {
                    e = e,
                    ElementTypeName = et.ElementTypeName
                }).Where(x => x.e.ViewKey == gds.ViewKey && x.ElementTypeName == SHORT_DESCRIPTION_ELEMENT_TYPE_NAME && x.e.ViewElementIsDeleted == false && x.e.ElementIsDeleted == false)
                .Select(s => s.e.Value).AsNoTracking().FirstOrDefault();

                shortDescriptionDataSelect.Add(shortDescriptionData);

                var imageData = _context.Views.Join(_context.ViewElements, v => v.ViewKey, ve => ve.ViewKey, (v, ve) => new
                {
                    IsDeleted = ve.IsDeleted,
                    ElementKey = ve.ElementKey,
                    ViewKey = v.ViewKey
                }).Join(_context.Elements, ve => ve.ElementKey, e => e.ElementKey, (ve, e) => new
                {
                    Value = e.Value,
                    ElementName = e.ElementName,
                    ViewKey = ve.ViewKey,
                    ElementTypeKey = e.ElementTypeKey,
                    ViewElementIsDeleted = ve.IsDeleted,
                    ElementIsDeleted = e.IsDeleted,
                    Path = e.Path
                }).Join(_context.ElementTypes, e => e.ElementTypeKey, et => et.ElementTypeKey, (e, et) => new
                {
                    e = e,
                    ElementTypeName = et.ElementTypeName
                }).Where(x => x.e.ViewKey == gds.ViewKey && x.ElementTypeName == IMAGE_ELEMENT_NAME && x.e.ViewElementIsDeleted == false && x.e.ElementIsDeleted == false)
               .Select(s => s.e.Path).AsNoTracking().FirstOrDefault();

                var galleryModel = new GalleryModel()
                {
                    ViewKey = gds.ViewKey,
                    ViewName = gds.ViewName,
                    OrderNumber = (int)gds.OrderNumber,
                    Executors = executorNames,
                    Tags = tagNames,
                    ShortDescription = shortDescriptionDataSelect,
                    Image = imageData
                };

                if (UniqualityCheck(galleryModel, galleryResult.GalleryModels)) galleryResult.GalleryModels.Add(galleryModel);

            });
            galleryResult.StatusModel = new StatusModel() { Message = "", StatusCode = 200 };

            return galleryResult;
        }
        public IQueryable<GroupDataSelectModel> Filter(IQueryable<GroupDataSelectModel> groupDataSelect, FilterModel filter)
        {
            var res = new List<GroupDataSelectModel>();

            groupDataSelect.ToList().ForEach(gds => {
                filter.TagsName.ToList().ForEach(tn =>
                {
                    if (gds.TagName == tn)
                       res.Add(gds);
                });
            });

            return res.AsQueryable();
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
            return currentList.Find(gm => gm.ViewKey == galleryModel.ViewKey) == null ? true : false;
        }
    }
}
