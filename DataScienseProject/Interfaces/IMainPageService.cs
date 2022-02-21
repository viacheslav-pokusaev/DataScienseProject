using DataScienseProject.Models.Gallery;
using DataScienseProject.Models.MainPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IMainPageService
    {
        List<TagResModel> GetAllTags();
        StatusModel AddNewGroup(DataToSendModel dataToSendModel);
    }
}
