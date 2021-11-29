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
        private readonly CS_DS_PortfolioContext _context;
        public GetDataService(CS_DS_PortfolioContext context)
        {
            _context = context;
        }

        public MainPageModel GetMainPageData()
        {
            #region Select from db
            var projectTypeSelect = (from v in _context.Views
                          join vt in _context.ViewTypes on v.ViewTypeKey equals vt.ViewTypeKey
                          where v.ViewKey == 1 && v.IsDeleted == false
                          orderby v.OrderNumber
                          select new ProjectTypeModel()
                          {
                              ViewName = v.ViewName,
                              ViewTypeName = vt.ViewTypeName
                          }
                          ).ToList();

            var executorSelect = (from ve in _context.ViewExecutors
                          join e in _context.Executors on ve.ExecutorKey equals e.ExecutorKey
                          join er in _context.ExecutorRoles on ve.ExecutorRoleKey equals er.ExecutorRoleKey
                          where ve.ViewKey == 1 && ve.IsDeleted == false
                          orderby ve.OrderNumber
                          select new ExecutorModel()
                          {
                              ExecutorName = e.ExecutorName,
                              ExecutorProfileLink = e.ExecutorProfileLink,
                              RoleName = er.RoleName,
                              OrderNumber = ve.OrderNumber
                          }).ToList();

            var tehnologySelect = (from vt in _context.ViewTags
                          join t in _context.Tags on vt.TagKey equals t.TagKey
                          join d in _context.Directions on t.DirectionKey equals d.DirectionKey
                          where vt.ViewKey == 1 && vt.IsDeleted == false
                          orderby vt.OrderNumber
                          select new TehnologyModel()
                          {
                            TName = t.Name,
                            TLink = t.Link,
                            DName = d.Name,
                            DLink = d.Link,
                            OrderNumber = vt.OrderNumber
                          }).ToList();

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


            ////тут выбирается только один элемент, но корректно
            //var layoutDataSelect2 = _context.Views.Include(ev => ev.ViewElements.Where(x => x.ViewKey == ev.ViewKey))
            //    .ThenInclude(e => e.ElementKeyNavigation)
            //    .ThenInclude(et => et.ElementTypeKeyNavigation)
            //    .Where(x => x.ViewKey == 1)
            //    .Select(s => 
            //    new LayoutDataModel() {
            //    ElementName = s.ViewElements.Select(e => e.ElementKeyNavigation.ElementName).FirstOrDefault(),
            //    IsShowElementName = s.ViewElements.Select(e => e.ElementKeyNavigation.IsShowElementName).FirstOrDefault(),
            //    OrderNumber = s.OrderNumber,
            //    Path = s.ViewElements.Select(e => e.ElementKeyNavigation.Path).FirstOrDefault(),
            //    Value = s.ViewElements.Select(e => e.ElementKeyNavigation.Value).FirstOrDefault(),
            //    ValueText = s.ViewElements.Select(e => e.ElementKeyNavigation.Text).FirstOrDefault(),
            //    LayoutStyleModel = new List<LayoutStyleModel>()
            //    }).OrderBy(ob => ob.OrderNumber).ToList();

            ////тут выбираются все элементы, но некорректно
            //var layoutDataSelect3 = _context.ViewElements
            //    .Include(e => e.ElementKeyNavigation)
            //    .Include(et => et.ElementKeyNavigation.LinkTypeKeyNavigation)
            //    .Where(x => x.ViewKey == 1)
            //    .Select(s => new LayoutDataModel() {
            //        ElementName = s.ElementKeyNavigation.ElementName,
            //        IsShowElementName = s.ElementKeyNavigation.IsShowElementName,
            //        OrderNumber = s.OrderNumber,
            //        Path = s.ElementKeyNavigation.Path,
            //        Value = s.ElementKeyNavigation.Value,
            //        ValueText = s.ElementKeyNavigation.Text,
            //        LayoutStyleModel = new List<LayoutStyleModel>()
            //    }).OrderBy(ob => ob.OrderNumber).ToList();

            var laloutDataSelect4 = _context.Views
                .Join(_context.ViewElements,
                v => v.ViewKey,
                ve => ve.ViewKey,
                (v, ve) => new {
                    OrderNumber = ve.OrderNumber,
                    ElementKey = ve.ElementKey,
                    IsDeleted = ve.IsDeleted
                }).Join(_context.Elements,
                ve => ve.ElementKey,
                e => e.ElementKey,
                (ve, e) => new { 
                    ElementTypeKey = e.ElementTypeKey,
                    ElementName = e.ElementName,
                    Value = e.Value,
                    Path = e.Path,
                    ValueText = e.Text,
                    IsShowElementName = e.IsShowElementName,
                    IsDeleted = e.IsDeleted,
                    ve = new
                    {
                        OrderNumber = ve.OrderNumber,
                        ElementKey = ve.ElementKey,
                        IsDeleted = ve.IsDeleted
                    }
                }).Join(_context.ElementTypes,
                e => e.ElementTypeKey,
                et => et.ElementTypeKey,
                (e, et) => new { 
                    ElementTypeName = et.ElementTypeName,
                    ElementTypeKey = e.ElementTypeKey,
                    ElementName = e.ElementName,
                    Value = e.Value,
                    Path = e.Path,
                    ValueText = e.ValueText,
                    IsShowElementName = e.IsShowElementName,
                    OrderNumber = e.ve.OrderNumber,
                    ElementKey = e.ve.ElementKey,
                    VeIsDeleted = e.ve.IsDeleted,
                    EIsDeleted = e.IsDeleted
                })
                .Where(x => x.VeIsDeleted == false && x.EIsDeleted == false)
                .Select(s => 
                new LayoutDataModel() {
                    ElementName = s.ElementName,
                    ElementTypeName = s.ElementTypeName,
                    IsShowElementName = s.IsShowElementName,
                    OrderNumber = s.OrderNumber,
                    Path = s.Path,
                    Value = s.Value,
                    ValueText = s.ValueText,
                    LayoutStyleModel = new List<LayoutStyleModel>()
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
