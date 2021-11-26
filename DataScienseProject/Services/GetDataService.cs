using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace DataScienseProject.Services
{
    public class GetDataService : IGetDataService
    {
        private readonly DataScienceProjectDbContext _context;
        public GetDataService(DataScienceProjectDbContext context)
        {
            _context = context;
        }

        public MainPageModel GetMainPageData()
        {
            #region Select from db
            //var projectTypeSelect = (from v in _context.Views
            //              join vt in _context.ViewTypes on v.ViewTypeKey equals vt.ViewTypeKey
            //              where v.ViewKey == 1 && v.IsDeleted == false
            //              orderby v.OrderNumber
            //              select new ProjectTypeModel()
            //              {
            //                  ViewName = v.ViewName,
            //                  ViewTypeName = vt.ViewTypeName
            //              }
            //              ).ToList();

            //var executorSelect = (from ve in _context.ViewExecutors
            //              join e in _context.Executors on ve.ExecutorKey equals e.ExecutorKey
            //              join er in _context.ExecutorRoles on ve.ExecutorRoleKey equals er.ExecutorRoleKey
            //              where ve.ViewKey == 1 && ve.IsDeleted == false
            //              orderby ve.OrderNumber
            //              select new ExecutorModel()
            //              {
            //                  ExecutorName = e.ExecutorName,
            //                  ExecutorProfileLink = e.ExecutorProfileLink,
            //                  RoleName = er.RoleName,
            //                  OrderNumber = ve.OrderNumber
            //              }).ToList();

            //var tehnologySelect = (from vt in _context.ViewTags
            //              join t in _context.Tags on vt.TagKey equals t.TagKey
            //              join d in _context.Directions on t.DirectionKey equals d.DirectionKey
            //              where vt.ViewKey == 1 && vt.IsDeleted == false
            //              orderby vt.OrderNumber
            //              select new TehnologyModel()
            //              {
            //                TName = t.Name,
            //                TLink = t.Link,
            //                DName = d.Name,
            //                DLink = d.Link,
            //                OrderNumber = vt.OrderNumber
            //              }).ToList();

            //var layoutDataSelect = (from v in _context.Views
            //              join ve in _context.ViewElements on v.ViewKey equals ve.ViewKey
            //              join e in _context.Elements on ve.ElementKey equals e.ElementKey
            //              join et in _context.ElementTypes on e.ElementTypeKey equals et.ElementTypeKey                         
            //              where v.ViewKey == 1
            //              orderby ve.OrderNumber
            //              select new LayoutDataModel()
            //              {
            //                  ElementName = e.ElementName,
            //                  Value = e.Value,
            //                  Path = e.Path,
            //                  ValueText = e.Text,
            //                  ElementTypeName = et.ElementTypeName,
            //                  OrderNumber = ve.OrderNumber,
            //                  IsShowElementName = e.IsShowElementName
            //              }).ToList();

            //var layoutStyleSelect = (from e in _context.Elements
            //              join ve in _context.ViewElements on e.ElementKey equals ve.ElementKey                          
            //              join et in _context.ElementTypes on e.ElementTypeKey equals et.ElementTypeKey
            //              join ep in _context.ElementParameters on e.ElementKey equals ep.ElementKey 
            //              where ve.ViewKey == 1 && e.IsDeleted == false
            //              select new LayoutStyleModel()
            //              {
            //                  ElementName = e.ElementName,
            //                  ElementTypeName = et.ElementTypeName,
            //                  Key = ep.Key,
            //                  Value = ep.Value
            //              }).ToList();
            #endregion


            var projectTypeSelect = _context.Views
                .Include(vt => vt.ViewTypeKeyNavigation)
                .Where(x => x.ViewKey == 1 && x.IsDeleted == false)
                .Select(s => 
                new ProjectTypeModel() { 
                    ViewName = s.ViewName,
                    ViewTypeName = s.ViewTypeKeyNavigation.ViewTypeName
                }).ToList();
            var executorSelect = _context.ViewExecutors
                .Include(ve => ve.ExecutorKeyNavigation)
                .Include(er => er.ExecutorRoleKeyNavigation)
                .Where(x => x.ViewKey == 1 && x.IsDeleted == false)
                .Select(s => 
                new ExecutorModel() { 
                    ExecutorName = s.ExecutorKeyNavigation.ExecutorName, 
                    ExecutorProfileLink = s.ExecutorKeyNavigation.ExecutorProfileLink, 
                    OrderNumber = s.OrderNumber, 
                    RoleName = s.ExecutorRoleKeyNavigation.RoleName
                }).OrderBy(ob => ob.OrderNumber).ToList();
            var tehnologySelect = _context.ViewTags
                .Include(vt => vt.TagKeyNavigation)
                .Include(t => t.TagKeyNavigation.DirectionKeyNavigation)
                .Where(x => x.ViewKey == 1 && x.IsDeleted == false)
                .Select(s =>
                new TehnologyModel()
                {
                    TName = s.TagKeyNavigation.Name,
                    TLink = s.TagKeyNavigation.Link,
                    DName = s.TagKeyNavigation.DirectionKeyNavigation.Name,
                    DLink = s.TagKeyNavigation.DirectionKeyNavigation.Link,
                    OrderNumber = s.OrderNumber
                }).OrderBy(ob => ob.OrderNumber).ToList();


            var layoutDataSelect = (from v in _context.Views
                          join ve in _context.ViewElements on v.ViewKey equals ve.ViewKey
                          join e in _context.Elements on ve.ElementKey equals e.ElementKey
                          join et in _context.ElementTypes on e.ElementTypeKey equals et.ElementTypeKey                         
                          where v.ViewKey == 1
                          orderby ve.OrderNumber
                          select new LayoutDataModel()
                          {
                              ElementName = e.ElementName,
                              Value = e.Value,
                              Path = e.Path,
                              ValueText = e.Text,
                              ElementTypeName = et.ElementTypeName,
                              OrderNumber = ve.OrderNumber,
                              IsShowElementName = e.IsShowElementName
                          }).ToList();

            var layoutDataSelect2 = _context.Views
                .Include(ev => ev.ViewElements.Where(x => x.ViewKey == ev.ViewKey))
                .ThenInclude(e => e.ElementKeyNavigation)
                .ThenInclude(et => et.ElementTypeKeyNavigation)
                .Where(x => x.ViewKey == 1)
                .Select(s =>
                new LayoutDataModel()
                {
                    ElementName = s.ViewElements
                    .Select(e => e.ElementKeyNavigation.ElementName).FirstOrDefault(),

                    ElementTypeName = s.ViewElements
                    .Select(et => et.ElementKeyNavigation.ElementTypeKeyNavigation.ElementTypeName).FirstOrDefault(),
                    IsShowElementName = s.ViewElements.Select(e => e.ElementKeyNavigation.IsShowElementName).FirstOrDefault(),
                    OrderNumber = s.OrderNumber, 
                    Path = s.ViewElements.Select(e => e.ElementKeyNavigation.Path).FirstOrDefault(), 
                    Value = s.ViewElements.Select(e => e.ElementKeyNavigation.Value).FirstOrDefault(), 
                    ValueText = s.ViewElements.Select(e => e.ElementKeyNavigation.Text).FirstOrDefault(),
                    LayoutStyleModel = new List<LayoutStyleModel>()
                }).OrderBy(ob => ob.OrderNumber).ToList();

            var layoutStyleSelect = (from e in _context.Elements
                          join ve in _context.ViewElements on e.ElementKey equals ve.ElementKey                          
                          join et in _context.ElementTypes on e.ElementTypeKey equals et.ElementTypeKey
                          join ep in _context.ElementParameters on e.ElementKey equals ep.ElementKey 
                          where ve.ViewKey == 1 && e.IsDeleted == false
                          select new LayoutStyleModel()
                          {
                              ElementName = e.ElementName,
                              ElementTypeName = et.ElementTypeName,
                              Key = ep.Key,
                              Value = ep.Value
                          }).ToList();


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
                    if(data.ElementName == style.ElementName)
                    {
                        layoutStyleBuff.Add(style);
                    }
                }
                mainPageModel.LayoutDataModels.Add(new LayoutDataModel() { 
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
        public List<GaleryModel> GetGaleryPageData()
        {
            throw new NotImplementedException();
        }
    }
}
