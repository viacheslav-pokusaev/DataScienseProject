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

            

            var layoutStyleSelect = _context.Elements
                .Join(_context.ViewElements,
                e => e.ElementKey,
                ve => ve.ElementKey,
                (e, ve) => new
                {
                    ElementTypeKey = e.ElementTypeKey,
                    ViewKey = ve.ViewKey,
                    ElementName = e.ElementName,
                    ElementKey = e.ElementKey,
                    EIsDeleted = e.IsDeleted
                })
                .Join(_context.ElementTypes,
                e => e.ElementTypeKey,
                et => et.ElementTypeKey,
                (e, et) => new {
                    ElementTypeName = et.ElementTypeName,
                    ElementName = e.ElementName,
                    ViewKey = e.ViewKey,
                    ElementKey = e.ElementKey,
                    EIsDeleted = e.EIsDeleted
                })
                .Join(_context.ElementParameters,
                e => e.ElementKey,
                ep => ep.ElementKey,
                (e, ep) => new {
                    Key = ep.Key,
                    Value = ep.Value,
                    EpIsDeleted = ep.IsDeleted,
                    ElementTypeName = e.ElementTypeName,
                    e = new {
                        ElementName = e.ElementName,
                        ViewKey = e.ViewKey,
                        ElementKey = e.ElementKey,
                        IsDeleted = e.EIsDeleted
                    }
                })
                .Where(x => x.EpIsDeleted == false && x.e.ViewKey == 1 && x.e.IsDeleted == false)
                .Select(s => new LayoutStyleModel() {
                    ElementName = s.e.ElementName,
                    ElementTypeName = s.ElementTypeName,
                    Key = s.Key,
                    Value = s.Value
                }).ToList();

            var layoutDataSelect = _context.Views
                .Join(_context.ViewElements,
                v => v.ViewKey,
                ve => ve.ViewKey,
                (v, ve) => new {
                    OrderNumber = ve.OrderNumber,
                    ElementKey = ve.ElementKey,
                    IsDeleted = ve.IsDeleted
                })
                .Join(_context.Elements,
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
                })
                .Join(_context.ElementTypes,
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
                    ValueText = s.ValueText
                })
                .OrderBy(ob => ob.OrderNumber).ToList();
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
