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
            //var sqlCommand = new SqlCommand("GetViewData") { CommandType = System.Data.CommandType.StoredProcedure };
            //var test = sqlCommand.Parameters.AddWithValue("@ViewKey", 1);

            //List<MainPageModel> resultList = new List<MainPageModel>();
            //IDataReader reader = null;
            //SqlConnection dbConnection = null;
            ////
            //try
            //{
            //    dbConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; database=CS_DS_Portfolio; Integrated Security=True;");
            //    IDbCommand dbCommand = new SqlCommand();
            //    dbCommand.Connection = dbConnection;
            //    dbCommand.CommandType = CommandType.StoredProcedure;
            //    dbCommand.CommandText = "GetViewData";
            //    //
            //    dbConnection.Open();
            //    //
            //    reader = dbCommand.ExecuteReader();
            //    //
            //    while (reader.Read())
            //    {
            //        Object erpEntity = new Object();
            //        erpEntity = reader.GetValue(1);                    
            //        //
            //        //resultList.Add(erpEntity);
            //    }
            //}
            //catch (SqlException exception)
            //{
            //    throw new Exception(exception.Message, exception); //This is good to have!
            //}
            //finally
            //{
            //    if (reader != null)
            //        reader.Close();
            //    //
            //    dbConnection.Close();
            //}
            ////
            //resultList.ToArray();


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

            var latoutStyleSelect = (from e in _context.Elements
                          join ve in _context.ViewElements on e.ElementKey equals ve.ElementKey                          
                          join et in _context.ElementTypes on e.ElementTypeKey equals et.ElementTypeKey
                          join ep in _context.ElementParameters on e.ElementKey equals ep.ElementKey 
                          where ve.ViewKey == 1 && e.IsDeleted == false
                          select new LatoutStyleModel()
                          {
                              ElementName = e.ElementName,
                              ElementTypeName = et.ElementTypeName,
                              Key = ep.Key,
                              Value = ep.Value
                          }).ToList();
            //SELECT

            //    e.ElementName
            // , et.ElementTypeName
            // , ep.[Key]
            // , ep.Value
            //FROM dbo.Elements e
            //LEFT JOIN dbo.ViewElements ve ON ve.ElementKey = e.ElementKey
            //LEFT JOIN dbo.ElementTypes et ON et.ElementTypeKey = e.ElementTypeKey
            //RIGHT JOIN dbo.ElementParameters ep ON ep.ElementKey = e.ElementKey AND ep.IsDeleted = 0
            //WHERE

            //    ve.ViewKey = @ViewKey

            //    AND e.IsDeleted = 0



            

            MainPageModel mainPageModel = new MainPageModel();
            

            return mainPageModel;
        }
        public List<GaleryModel> GetGaleryPageData()
        {
            throw new NotImplementedException();
        }
    }
}
