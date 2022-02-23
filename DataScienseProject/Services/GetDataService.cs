using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataScienseProject.Services
{
    public class GetDataService : IGetDataService
    {
        private readonly DataScienceProjectDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        private List<GalleryModel> galleryModelsBuff = new List<GalleryModel>();

        private const string SHORT_DESCRIPTION_ELEMENT_TYPE_NAME = "Header Description";
        private const string IMAGE_ELEMENT_NAME = "Header Image";
        public GetDataService(DataScienceProjectDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }
        public MainPageModel GetMainPageData(int id)
        {
            #region Select from db

            var projectTypeSelect = _context.Views.Include(vt => vt.ViewTypeKeyNavigation).Where(x => x.ViewKey == id && x.IsDeleted == false).Select(s =>
                new ProjectTypeModel() { ViewName = s.ViewName, ViewTypeName = s.ViewTypeKeyNavigation.ViewTypeName }).ToList();

            var executorSelect = _context.ViewExecutors.Include(ve => ve.ExecutorKeyNavigation).Include(er => er.ExecutorRoleKeyNavigation).Where(x =>
            x.ViewKey == id && x.IsDeleted == false).Select(s => new ExecutorModel()
            {
                ExecutorName = s.ExecutorKeyNavigation.ExecutorName,
                ExecutorProfileLink = s.ExecutorKeyNavigation.ExecutorProfileLink,
                OrderNumber = s.OrderNumber,
                RoleName = s.ExecutorRoleKeyNavigation.RoleName
            })
            .OrderBy(ob => ob.OrderNumber).ToList();

            var tehnologySelect = _context.ViewTags.Include(vt => vt.TagKeyNavigation).Include(t => t.TagKeyNavigation.DirectionKeyNavigation).Where(x =>
            x.ViewKey == id && x.IsDeleted == false).Select(s => new TechnologyModel()
            {
                TagName = s.TagKeyNavigation.Name,
                TagLink = s.TagKeyNavigation.Link,
                DirectoryName = s.TagKeyNavigation.DirectionKeyNavigation.Name,
                DirectoryLink = s.TagKeyNavigation.DirectionKeyNavigation.Link,
                OrderNumber = s.OrderNumber
            })
            .OrderBy(ob => ob.OrderNumber).ToList();

            var layoutStyleSelect = _context.Elements.Join(_context.ViewElements, e => e.ElementKey, ve => ve.ElementKey, (e, ve) => new
            {
                ElementTypeKey = e.ElementTypeKey,
                ViewKey = ve.ViewKey,
                ElementName = e.ElementName,
                ElementKey = e.ElementKey,
                ElementIsDeleted = e.IsDeleted
            })
                .Join(_context.ElementTypes, e => e.ElementTypeKey, et => et.ElementTypeKey, (e, et) => new
                {
                    ElementTypeName = et.ElementTypeName,
                    ElementName = e.ElementName,
                    ViewKey = e.ViewKey,
                    ElementKey = e.ElementKey,
                    ElementIsDeleted = e.ElementIsDeleted
                })
                .Join(_context.ElementParameters, e => e.ElementKey, ep => ep.ElementKey, (e, ep) => new
                {
                    Key = ep.Key,
                    Value = ep.Value,
                    EpIsDeleted = ep.IsDeleted,
                    ElementTypeName = e.ElementTypeName,
                    e = new
                    {
                        ElementName = e.ElementName,
                        ViewKey = e.ViewKey,
                        ElementKey = e.ElementKey,
                        IsDeleted = e.ElementIsDeleted
                    }
                }).Where(x => x.EpIsDeleted == false && x.e.ViewKey == id && x.e.IsDeleted == false)
                    .Select(s => new LayoutStyleModel() { ElementName = s.e.ElementName, ElementTypeName = s.ElementTypeName, Key = s.Key, Value = s.Value }).ToList();

            var layoutDataSelect = _context.Views.Join(_context.ViewElements, v => v.ViewKey, ve => ve.ViewKey, (v, ve) => new
            {
                OrderNumber = ve.OrderNumber,
                ElementKey = ve.ElementKey,
                IsDeleted = ve.IsDeleted,
                ViewKey = ve.ViewKey
            }).Join(_context.Elements, ve => ve.ElementKey, e => e.ElementKey, (ve, e) => new
            {
                ElementTypeKey = e.ElementTypeKey,
                ElementName = e.ElementName,
                Value = e.Value,
                Path = e.Path,
                ValueText = e.Text,
                IsShowElementName = e.IsShowElementName,
                IsDeleted = e.IsDeleted,
                ve = new { OrderNumber = ve.OrderNumber, ElementKey = ve.ElementKey, IsDeleted = ve.IsDeleted, ViewKey = ve.ViewKey }
            })
                .Join(_context.ElementTypes, e => e.ElementTypeKey, et => et.ElementTypeKey, (e, et) => new
                {
                    ElementTypeName = et.ElementTypeName,
                    ElementTypeKey = e.ElementTypeKey,
                    ElementName = e.ElementName,
                    Value = e.Value,
                    Path = e.Path,
                    ValueText = e.ValueText,
                    IsShowElementName = e.IsShowElementName,
                    OrderNumber = e.ve.OrderNumber,
                    ElementKey = e.ve.ElementKey,
                    ViewElementIsDeleted = e.ve.IsDeleted,
                    ElementIsDeleted = e.IsDeleted,
                    ViewKey = e.ve.ViewKey
                })
                .Where(x => x.ViewElementIsDeleted == false && x.ElementIsDeleted == false && x.ViewKey == id).Select(s => new LayoutDataModel()
                {
                    ElementName = s.ElementName,
                    ElementTypeName = s.ElementTypeName,
                    IsShowElementName = s.IsShowElementName,
                    OrderNumber = s.OrderNumber,
                    Path = s.Path,
                    Value = s.Value,
                    ValueText = s.ValueText
                }).OrderBy(ob => ob.OrderNumber).ToList();
            #endregion


            MainPageModel mainPageModel = new MainPageModel();

            mainPageModel.ProjectTypeModels = projectTypeSelect;
            mainPageModel.ExecutorModels = executorSelect;
            mainPageModel.TehnologyModels = tehnologySelect;

            mainPageModel.LayoutDataModels = new List<LayoutDataModel>();


            foreach (var data in layoutDataSelect)
            {
                var layoutStyleBuff = new List<LayoutStyleModel>();

                foreach (var style in layoutStyleSelect)
                {
                    if (data.ElementName == style.ElementName)
                        layoutStyleBuff.Add(style);
                }
                mainPageModel.LayoutDataModels.Add(new LayoutDataModel()
                {
                    ElementName = data.ElementName,
                    ElementTypeName = data.ElementTypeName,
                    IsShowElementName = data.IsShowElementName,
                    LayoutStyleModel = layoutStyleBuff,
                    OrderNumber = data.OrderNumber,
                    Path = data.Path,
                    Value = data.Value,
                    ValueText = data.ValueText
                });
            }
            return mainPageModel;
        }
        public GalleryResult GetGalleryPageData(string groupName, HttpContext http, FilterModel filter)
        {
            var isAuthorizedResult = _authorizationService.IsAuthorized(http, filter == null ? groupName : filter.GroupName);
            if (isAuthorizedResult.StatusCode == 403) return new GalleryResult() { StatusModel = isAuthorizedResult };

            var galleryResult = new GalleryResult();

            galleryResult.GalleryModels = new List<GalleryModel>();

            #region Group data select start
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
                gv = new { GroupName = gv.GroupName, IsDeleted = gv.IsDeleted }
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
            .Join(_context.Executors, ve => ve.ExecutorKey, e => e.ExecutorKey, (ve, e) => new
            {
                ViewName = ve.ViewName,
                OrderNumber = ve.OrderNumber,
                ViewKey = ve.ViewKey,
                TagName = ve.TagName,
                VIsDeleted = ve.VIsDeleted,
                gv = ve.gv,
                ExecutorKey = ve.ExecutorKey,
                ExecutorName = e.ExecutorName
            })
            .Where(x => x.gv.GroupName == groupName && x.gv.IsDeleted == false && x.VIsDeleted == false).Select(s => new GroupData
            {
                ViewName = s.ViewName,
                ViewKey = (int)s.ViewKey,
                OrderNumber = s.OrderNumber,
                TagName = s.TagName,
                ExecutorName = s.ExecutorName
            }).OrderBy(ob => ob.OrderNumber).AsNoTracking().ToList();

            groupDataSelect.ForEach(gds =>
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
                #endregion
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

            if (filter != null && (filter?.TagsName.Length > 0 || filter?.ExecutorsName.Length > 0))
            {
                if (filter.TagsName.Count() > 0)
                {
                    filter.TagsName.ToList().ForEach(t =>
                    {
                        if (gds.TagName == t && UniqualityCheck(galleryModel, galleryResult.GalleryModels))
                            galleryModelsBuff.Add(galleryModel);
                    });
                }
                else
                {
                    if (UniqualityCheck(galleryModel, galleryResult.GalleryModels))
                            galleryModelsBuff.Add(galleryModel);
                }

                if(filter.ExecutorsName.Count() > 0) { 
                galleryModelsBuff.ToList().ForEach(gmb => {
                        if(!isFilterContainsCheck(gmb.Executors, filter.ExecutorsName) && galleryModelsBuff.Count > 0)
                        {
                            galleryModelsBuff.Remove(gmb);
                        }
                });
                }
                galleryResult.GalleryModels = galleryModelsBuff;
            }
            else
            {
                if (UniqualityCheck(galleryModel, galleryResult.GalleryModels))
                    galleryResult.GalleryModels.Add(galleryModel);
                }
            });
            galleryResult.StatusModel = new StatusModel() { Message = "", StatusCode = 200 };

            return galleryResult;
        }

        public bool UniqualityCheck(GalleryModel galleryModel, List<GalleryModel> currentList)
        {
            return currentList.Find(gm => gm.ViewKey == galleryModel.ViewKey) == null ? true : false;
        }
        public bool isFilterContainsCheck(List<string> checkElements, string[] findElements)
        {
            return checkElements.Where(ce => findElements.Contains(ce)).Count() > 0 ? true : false;
        }
    }
}
