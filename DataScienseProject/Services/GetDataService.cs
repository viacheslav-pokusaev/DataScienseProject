using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using DataScienseProject.Models.Gallery;
using Microsoft.AspNetCore.Http;

namespace DataScienseProject.Services
{
    public class GetDataService : IGetDataService
    {
        private readonly DataScienceProjectDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        public GetDataService(DataScienceProjectDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }
        public MainPageModel GetMainPageData()
        {
            #region Select from db

            var projectTypeSelect = _context.Views.Include(vt => vt.ViewTypeKeyNavigation).Where(x => x.ViewKey == 1 && x.IsDeleted == false).Select(s => 
                new ProjectTypeModel() { ViewName = s.ViewName, ViewTypeName = s.ViewTypeKeyNavigation.ViewTypeName}).ToList();

            var executorSelect = _context.ViewExecutors.Include(ve => ve.ExecutorKeyNavigation).Include(er => er.ExecutorRoleKeyNavigation).Where(x => 
            x.ViewKey == 1 && x.IsDeleted == false).Select(s => new ExecutorModel() { ExecutorName = s.ExecutorKeyNavigation.ExecutorName, 
                ExecutorProfileLink = s.ExecutorKeyNavigation.ExecutorProfileLink, OrderNumber = s.OrderNumber, RoleName = s.ExecutorRoleKeyNavigation.RoleName})
            .OrderBy(ob => ob.OrderNumber).ToList();
            var tehnologySelect = _context.ViewTags.Include(vt => vt.TagKeyNavigation).Include(t => t.TagKeyNavigation.DirectionKeyNavigation).Where(x => 
            x.ViewKey == 1 && x.IsDeleted == false).Select(s =>new TechnologyModel() { TName = s.TagKeyNavigation.Name, TLink = s.TagKeyNavigation.Link,
                DName = s.TagKeyNavigation.DirectionKeyNavigation.Name, DLink = s.TagKeyNavigation.DirectionKeyNavigation.Link, OrderNumber = s.OrderNumber})
            .OrderBy(ob => ob.OrderNumber).ToList();

            var layoutStyleSelect = _context.Elements.Join(_context.ViewElements, e => e.ElementKey, ve => ve.ElementKey, (e, ve) => new { 
                ElementTypeKey = e.ElementTypeKey, ViewKey = ve.ViewKey, ElementName = e.ElementName, ElementKey = e.ElementKey, EIsDeleted = e.IsDeleted})
                .Join(_context.ElementTypes, e => e.ElementTypeKey, et => et.ElementTypeKey, (e, et) => new { ElementTypeName = et.ElementTypeName,
                    ElementName = e.ElementName, ViewKey = e.ViewKey, ElementKey = e.ElementKey, EIsDeleted = e.EIsDeleted})
                .Join(_context.ElementParameters, e => e.ElementKey,ep => ep.ElementKey,(e, ep) => new {Key = ep.Key, Value = ep.Value,
                    EpIsDeleted = ep.IsDeleted, ElementTypeName = e.ElementTypeName, e = new { ElementName = e.ElementName, ViewKey = e.ViewKey,
                    ElementKey = e.ElementKey, IsDeleted = e.EIsDeleted}}).Where(x => x.EpIsDeleted == false && x.e.ViewKey == 1 && x.e.IsDeleted == false)
                    .Select(s => new LayoutStyleModel() {ElementName = s.e.ElementName, ElementTypeName = s.ElementTypeName, Key = s.Key, Value = s.Value}).ToList();

            var layoutDataSelect = _context.Views.Join(_context.ViewElements, v => v.ViewKey, ve => ve.ViewKey, (v, ve) => new { OrderNumber = ve.OrderNumber,
                    ElementKey = ve.ElementKey, IsDeleted = ve.IsDeleted, ViewKey = ve.ViewKey }).Join(_context.Elements, ve => ve.ElementKey, e => e.ElementKey, (ve, e) => new { 
                    ElementTypeKey = e.ElementTypeKey, ElementName = e.ElementName, Value = e.Value, Path = e.Path, ValueText = e.Text, IsShowElementName = e.IsShowElementName,
                    IsDeleted = e.IsDeleted, ve = new { OrderNumber = ve.OrderNumber, ElementKey = ve.ElementKey, IsDeleted = ve.IsDeleted, ViewKey = ve.ViewKey}})
                .Join(_context.ElementTypes, e => e.ElementTypeKey, et => et.ElementTypeKey, (e, et) => new { ElementTypeName = et.ElementTypeName,
                    ElementTypeKey = e.ElementTypeKey, ElementName = e.ElementName, Value = e.Value, Path = e.Path, ValueText = e.ValueText, IsShowElementName = e.IsShowElementName,
                    OrderNumber = e.ve.OrderNumber, ElementKey = e.ve.ElementKey, VeIsDeleted = e.ve.IsDeleted, EIsDeleted = e.IsDeleted, ViewKey = e.ve.ViewKey})
                .Where(x => x.VeIsDeleted == false && x.EIsDeleted == false && x.ViewKey == 1).Select(s => new LayoutDataModel() { ElementName = s.ElementName, 
                    ElementTypeName = s.ElementTypeName,IsShowElementName = s.IsShowElementName, OrderNumber = s.OrderNumber, Path = s.Path, Value = s.Value,
                    ValueText = s.ValueText}).OrderBy(ob => ob.OrderNumber).ToList();
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
                mainPageModel.LayoutDataModels.Add(new LayoutDataModel() { ElementName = data.ElementName, ElementTypeName = data.ElementTypeName,
                    IsShowElementName = data.IsShowElementName, LayoutStyleModel = layoutStyleBuff, OrderNumber = data.OrderNumber,
                    Path = data.Path, Value = data.Value, ValueText = data.ValueText});
            }
            return mainPageModel;
        }
        public GalleryResult GetGalleryPageData(string groupName, HttpContext http)
        {
            var cookies = http.Request.Cookies.Where(x => x.Key == "Authorize").ToList();
            if (cookies.Count == 0)
            {
                return new GalleryResult() { ExceptionModel = new ExceptionModel() { ErrorMessage = "Insert password", StatusCode = 403} };
            }

            var shortDescriptionElementName = "Introduction";
            var galleryResult = new GalleryResult();

            galleryResult.GalleryModels = new List<GalleryModel>();

            var groupDataSelect = _context.GroupViews.Join(_context.Groups, gv => gv.GroupKey, g => g.GroupKey, (gv, g) => new { ViewKey = gv.ViewKey,
            g.GroupName, IsDeleted = g.IsDeleted }).Join(_context.Views, gv => gv.ViewKey, v => v.ViewKey, (gv, v) => new { ViewName = v.ViewName,
                OrderNumber = v.OrderNumber, ViewKey = v.ViewKey, gv = new { GroupName = gv.GroupName, IsDeleted = gv.IsDeleted }})
            .Where(x => x.gv.GroupName == groupName && x.gv.IsDeleted == false).Select(s => new GroupData{ ViewName = s.ViewName, ViewKey = (int)s.ViewKey,
            OrderNumber = s.OrderNumber}).OrderBy(ob => ob.OrderNumber).ToList();

            groupDataSelect.ForEach(gds => {
            var executorDataSelect = _context.ViewExecutors.Include(e => e.ExecutorKeyNavigation).Include(er => er.ExecutorRoleKeyNavigation)
                .Where(x => x.ViewKey == gds.ViewKey && x.IsDeleted == false).Select(s => new ExecutorModel{ ExecutorName = s.ExecutorKeyNavigation.ExecutorName, RoleName = 
                s.ExecutorRoleKeyNavigation.RoleName}).ToList();

            var tagDataSelect = _context.ViewTags.Include(t => t.TagKeyNavigation).Where(x => x.ViewKey == gds.ViewKey && x.IsDeleted == false).Select(s => new { 
                Name = s.TagKeyNavigation.Name}).ToList();

                var tagNames = new List<string>();
                tagDataSelect.ForEach(tds => tagNames.Add(tds.Name));

                var shortDescriptionDataSelect = _context.Views.Join(_context.ViewElements, v => v.ViewKey, ve => ve.ViewKey, (v, ve) => new{ IsDeleted = ve.IsDeleted,
                ElementKey = ve.ElementKey, ViewKey = v.ViewKey }).Join(_context.Elements, ve => ve.ElementKey, e => e.ElementKey, (ve, e) => new {
                Value = e.Value, ElementName = e.ElementName, ViewKey = ve.ViewKey }).Where(x => x.ViewKey == gds.ViewKey && x.ElementName == shortDescriptionElementName)
                .Select(s => s.Value).ToList();

               galleryResult.GalleryModels.Add(new GalleryModel() { ViewKey = gds.ViewKey, ViewName = gds.ViewName, OrderNumber = (int)gds.OrderNumber,
               Executors = executorDataSelect, Tags = tagNames, ShortDescription = shortDescriptionDataSelect});
            });
            galleryResult.ExceptionModel = new ExceptionModel() { ErrorMessage = "success", StatusCode = 200};

            return galleryResult; 
        }
    }
}
