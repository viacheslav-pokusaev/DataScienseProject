﻿using DataScienseProject.Interfaces;
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

            var query1 = _context.Views.Include(vt => vt.ViewTypeKeyNavigation).Where(x => x.ViewKey == 1 && x.IsDeleted == false).ToList();

            var query2 = _context.ViewExecutors.Include(ve => ve.ExecutorKeyNavigation).Include(er => er.ExecutorRoleKeyNavigation).Where(x => x.ViewKey == 1 && x.IsDeleted == false).ToList();


            return new MainPageModel();
        }
        public List<GaleryModel> GetGaleryPageData()
        {
            throw new NotImplementedException();
        }
    }
}
