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
               VIsDeleted = v.IsDeleted,
               gv = new { GroupName = gv.GroupName, IsGroupDeleted = gv.IsDeleted }
           })
           .Join(_context.ViewTags, v => v.ViewKey, vt => vt.ViewKey, (v, vt) => new
           {
               ViewName = v.ViewName,
               OrderNumber = v.OrderNumber,
               ViewKey = v.ViewKey,
               VIsDeleted = v.VIsDeleted,
               gv = v.gv,
               TagKey = vt.TagKey
           })
           .Join(_context.Tags, vt => vt.TagKey, t => t.TagKey, (vt, t) => new
           {
               ViewName = vt.ViewName,
               OrderNumber = vt.OrderNumber,
               ViewKey = vt.ViewKey,
               VIsDeleted = vt.VIsDeleted,
               gv = vt.gv,
               TagName = t.Name
           })
           .Join(_context.ViewExecutors, t => t.ViewKey, ve => ve.ViewKey, (t, ve) => new
           {
               ViewName = t.ViewName,
               OrderNumber = t.OrderNumber,
               ViewKey = t.ViewKey,
               TagName = t.TagName,
               VIsDeleted = t.VIsDeleted,
               gv = t.gv,
               ExecutorKey = ve.ExecutorKey
           })
           .Join(_context.Executors, ve => ve.ExecutorKey, e => e.ExecutorKey, (ve, e) => new GroupDataSelectModel
           {
               ViewName = ve.ViewName,
               OrderNumber = ve.OrderNumber,
               ViewKey = ve.ViewKey,
               TagName = ve.TagName,
               IsViewDeleted = ve.VIsDeleted,
               GroupName = ve.gv.GroupName,
               IsGroupDeleted = ve.gv.IsGroupDeleted,
               ExecutorKey = ve.ExecutorKey,
               ExecutorName = e.ExecutorName
           });

            groupDataSelect = (filter == null && filter?.TagsName.Length == 0 && filter?.ExecutorsName.Length == 0) ? groupDataSelect : Filter(groupDataSelect, filter);

            var groupSelectResult = groupDataSelect.Where(x => x.GroupName == groupName && x.IsGroupDeleted == false && x.IsViewDeleted == false).Select(s => new GroupData
           {
               ViewName = s.ViewName,
               ViewKey = (int)s.ViewKey,
               OrderNumber = s.OrderNumber,
               TagName = s.TagName,
               ExecutorName = s.ExecutorName
           }).OrderBy(ob => ob.OrderNumber).AsNoTracking().ToList();


            return new GalleryResult();
        }
        public IQueryable<GroupDataSelectModel> Filter(IQueryable<GroupDataSelectModel> groupDataSelect, FilterModel filter) {

            var res = new List<GroupDataSelectModel>();

            groupDataSelect.ForEachAsync(gds => {
                if (filter.TagsName.ToList().Where(tn => tn == gds.TagName).Count() > 0 && filter.ExecutorsName.ToList().Where(en => en == gds.ExecutorName).Count() > 0) res.Add(gds);
            }).GetAwaiter();

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
            throw new NotImplementedException();
        }
    }
}
