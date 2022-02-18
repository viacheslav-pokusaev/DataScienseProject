using DataScienseProject.Context;
using DataScienseProject.Entities;
using DataScienseProject.Interfaces;
using DataScienseProject.Models.EmailSender;
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
        public bool AddNewGroup(DataToSendModel dataToSendModel)
        {
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

            var groupKey = _context.Groups.Select(x => x.GroupKey).Max() + 1;

            var group = new Group
            {                
                GroupName = "group_" + groupKey.ToString(),
                IsDeleted = false
            };
            _context.Groups.Add(group);
            _context.SaveChanges();


            RandomNumberGenerator generator = new RandomNumberGenerator();
            var pass = new Password
            {
                GroupKey = _context.Groups.Where(x => x.GroupKey == groupKey).FirstOrDefault().GroupKey,
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

            foreach (var groupView in filteredGroupViews)
            {
                _context.GroupViews.Add(groupView);
            }
            _context.SaveChanges();

            if (dataToSendModel.TagsList.Count < 5)
                _emailSenderService.SendEmailToUser(new EmailSendModel() { GroupName = group.GroupName, Password = pass.PasswordValue, EnterTime = DateTime.Now }, dataToSendModel.Email).ConfigureAwait(false);
            else
                _emailSenderService.SendEmailToAdmins(new EmailSendModel() { GroupName = group.GroupName, Password = pass.PasswordValue, EnterTime = DateTime.Now }, dataToSendModel.Email).ConfigureAwait(false);

            return true;
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
                    res.Add(new TagResModel() { Direction = currentDirrection, TagModels = GetUnique(resTags) });
                }
            }
            return res;
        }
        private List<TagModel> GetUnique(List<TagModel> tagModels)
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
