using DataScienseProject.Context;
using DataScienseProject.Entities;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.EmailSender;
using DataScienseProject.Models.Gallery;
using DataScienseProject.Models.MainPage;
using DataScienseProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataScienseProject.Services
{
    public class MainPageService : IMainPageService
    {
        private readonly DataScienceProjectDbContext _context;
        private readonly IEmailSenderService _emailSenderService;
        private const int EXPIRATION_TIME = 3;
        private const int PASSWORD_LENGTH = 10;

        public MainPageService(DataScienceProjectDbContext context, IEmailSenderService emailSenderService)
        {
            _context = context;
            _emailSenderService = emailSenderService;
        }
        public StatusModel AddNewGroup(DataToSendModel dataToSendModel)
        {
            if(dataToSendModel.TagsList.Count == 0 || dataToSendModel.TagsList == null) return null;

            List<List<TagsData>> tagsData = new List<List<TagsData>>();
            foreach (var tagKey in dataToSendModel.TagsList)
            {
                var dataSelect = _context.ViewTags
               .Join(_context.Views, vt => vt.ViewKey, v => v.ViewKey, (vt, v) => new
               {
                   ViewName = v.ViewName,
                   ViewKey = v.ViewKey,
                   TagKey = vt.TagKey,
                   IsDeleted = v.IsDeleted,
                   OrderNumber = v.OrderNumber
               })
               .Join(_context.Tags, vt => vt.TagKey, t => t.TagKey, (vt, t) => new
               {
                   TagName = t.Name,
                   TagKey = t.TagKey,
                   vt = vt
               })
               .Where(x => x.vt.TagKey == tagKey.TagKey && x.vt.IsDeleted == false).Select(s => new TagsData
               {
                   ViewName = s.vt.ViewName,
                   ViewKey = (int)s.vt.ViewKey,
                   TagName = s.TagName,
                   TagKey = s.TagKey,
                   OrderNumber = s.vt.OrderNumber
               }).OrderBy(ob => ob.OrderNumber).ToList();
               if (dataSelect.Any()) {
                   tagsData.Add(dataSelect);
               }
            }

            var groupList = _context.Groups.Select(s => s.GroupName).Where(gn => gn.Contains("_")).ToList();
            var groupNumber = groupList.LastOrDefault() == null ? Convert.ToInt32("1"): Convert.ToInt32(groupList.LastOrDefault().Split("_").Last()) + 1;

            var group = new Group
            {                
                GroupName = "group_" + groupNumber.ToString(),
                IsDeleted = false
            };
            _context.Groups.Add(group);
            _context.SaveChanges();

            var groupKey = _context.Groups.Where(x => x.GroupKey == group.GroupKey).FirstOrDefault().GroupKey;
            RandomNumberGenerator generator = new RandomNumberGenerator();
            var pass = new Password
            {
                GroupKey = groupKey,
                PasswordValue = generator.RandomPassword(PASSWORD_LENGTH),
                CreatedDate = DateTime.Now.Date,
                ExpirationDate = DateTime.Now.Date.AddDays(EXPIRATION_TIME),
                IsDeleted = false
            };
            _context.Passwords.Add(pass);
            _context.SaveChanges();


            List<GroupView> groupViews = new List<GroupView>();
            foreach (var v in tagsData)
            {
                foreach (var vs in v)
                {
                    var groupView = new GroupView
                    {
                        ViewKey = vs.ViewKey,
                        GroupKey = groupKey,
                        IsDeleted = false
                    };
                    groupViews.Add(groupView);
                }
            }

            List<GroupView> filteredGroupViews = groupViews.GroupBy(x => x.ViewKey).Select(x => x.First()).ToList();

            _context.GroupViews.AddRange(filteredGroupViews);
            _context.SaveChanges();

            StatusModel statusModel = new StatusModel();
            var emailSendModel = new EmailSendModel() { GroupName = group.GroupName, Password = pass.PasswordValue, EnterTime = DateTime.Now };
            
            var countTosendEmailToUser = Convert.ToInt32(_context.ConfigValues.Where(x => x.Key == "SendNewGroupEmailToUser" && x.IsEnabled == true).ToList().LastOrDefault()?.Value);
            var countTosendEmailToAdmin = Convert.ToInt32(_context.ConfigValues.Where(x => x.Key == "SendNewGroupEmail" && x.IsEnabled == true).ToList().LastOrDefault()?.Value);

            if (countTosendEmailToUser > dataToSendModel.TagsList.Count)
            {
                _emailSenderService.SendEmail(emailSendModel, dataToSendModel.Email, null, EmailType.NewGroupToUser).ConfigureAwait(false);
                statusModel.Message = "We sent a link and password to your group to your email.";

                if (countTosendEmailToAdmin < dataToSendModel.TagsList.Count)
                    _emailSenderService.SendEmail(emailSendModel, dataToSendModel.Email, null, EmailType.NewGroupToAdminAndUser).ConfigureAwait(false);
            }
            else 
            {
                _emailSenderService.SendEmail(emailSendModel, dataToSendModel.Email, null, EmailType.NewGroupToAdmin).ConfigureAwait(false);
                statusModel.Message = "Thank you for your request, our administrator will contact you as soon as possible.";
            }


            return statusModel;
        }

        public List<TagResModel> GetAllTags()
        {
            var res = new List<TagResModel>();

            var tagsSelect = _context.Tags.Join(_context.ViewTags, t => t.TagKey, vt => vt.TagKey, (t, vt) => new {
                TagKey = t.TagKey,
                TagName = t.Name,
                DirectionKey = t.DirectionKey
            }).Join(_context.Directions, t => t.DirectionKey, d => d.DirectionKey, (t, d) => new {
                TagKey = t.TagKey,
                TagName = t.TagName,
                Direction = d.Name
            }).OrderBy(t => t.TagKey).ToList();

            foreach(var tag in tagsSelect)
            {
                var currentDirrection = tag.Direction;
                if (res.Find(ts => ts.Direction == currentDirrection) == null)
                {
                    var resTags = tagsSelect.Where(t => t.Direction == currentDirrection)
                        .Select(s => new TagModel() { TagKey = s.TagKey, TagName = s.TagName }).ToList();
                    res.Add(new TagResModel() { Direction = currentDirrection, TagModels = DistinctTagModels(resTags) });
                }
            }
            return res;
        }
        private List<TagModel> DistinctTagModels(List<TagModel> tagModels)
        {
            var res = new List<TagModel>();

            tagModels.ForEach(tm =>
            {
                if(res.Find(f => f.TagKey == tm.TagKey) == null)
                {
                    res.Add(tm);
                }
            });

            return res;
        }
    } 
}
