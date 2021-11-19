using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
            var sqlCommand = new SqlCommand("GetViewData") { CommandType = System.Data.CommandType.StoredProcedure };
            var test = sqlCommand.Parameters.AddWithValue("@ViewKey", 1);
            return new MainPageModel();
        }
        public List<GaleryModel> GetGaleryPageData()
        {
            throw new NotImplementedException();
        }
    }
}
