using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FindCustomerService.Models;

namespace FindCustomerService.DataModel
{
    public static class BusinessData
    {
        private static readonly ObservableCollection<BusinessGroupUI> BusinessGroups = new ObservableCollection<BusinessGroupUI>();
        private static List<BusinessGroup> _businessGroupsList = new List<BusinessGroup>();
        public static List<Business> BusinessList = new List<Business>();

        public static async Task<ObservableCollection<BusinessGroupUI>> GetGroupsAsync()
        {
            if (BusinessGroups.Count > 0) return BusinessGroups;
            _businessGroupsList = await App.SearchBusinessClient.GetTable<BusinessGroup>().ToListAsync();
            var table = App.SearchBusinessClient.GetTable<Business>();
            foreach (var businessGroup in _businessGroupsList)
            {
                BusinessGroupUI businessGroupUi = new BusinessGroupUI() { Id = businessGroup.Id, Name = businessGroup.Name };
                var _businessList = await table.Take(1000).Where(p => p.GroupName == businessGroup.Name).ToListAsync();
                foreach (Business business in _businessList)
                {
                    businessGroupUi.Businesses.Add(business);
                }
                BusinessGroups.Add(businessGroupUi);
                BusinessList.AddRange(_businessList);
            }
            return BusinessGroups;
        }
    }
}
