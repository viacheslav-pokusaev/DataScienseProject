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
