using DataScienseProject.Models.MainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IMainPageService
    {
        List<TagModel> GetAllTags();
        void AddNewGroup(List<TagModel> tags);
    }
}
