using DataScienseProject.Context;
using DataScienseProject.Entities;
using DataScienseProject.Interfaces;
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
        private const int EXPIRATION_TIME = 3;
        private const int PASSWORD_LENGTH = 10;

        public MainPageService(DataScienceProjectDbContext context)
        {
            _context = context;
        }
        public void AddNewGroup(List<TagModel> tags)
        {
            List<List<TagsData>> tagsData = new List<List<TagsData>>();
            foreach (var tagKey in tags)
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

        }

        public List<TagModel> GetAllTags()
        {
            var tagsSelect = _context.Tags.Select(t => new TagModel()
            {
                TagKey = t.TagKey,
                TagName = t.Name
            }).ToList();

            return tagsSelect;
        }       
    } 

}
