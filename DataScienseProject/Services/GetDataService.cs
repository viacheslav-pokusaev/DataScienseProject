using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var x = _context.Set<object>("dbo.GetViewData @ViewKey = 1");
            throw new NotImplementedException();
        }
        public List<GaleryModel> GetGaleryPageData()
        {
            throw new NotImplementedException();
        }
    }
}
