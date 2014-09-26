using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using FindCustomerService.Annotations;
using FindCustomerService.DataModel;
using FindCustomerService.Helpers;
using FindCustomerService.Models;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace FindCustomerService.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchText = string.Empty;
        private ObservableCollection<BusinessGroupUI> _businessGroups = new ObservableCollection<BusinessGroupUI>();
        private readonly string _dataFileName = "businesses.json";
        private StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        private Visibility _isProgressVisible = Visibility.Collapsed;

        public ObservableCollection<BusinessGroupUI> BusinessGroups
        {
            get { return _businessGroups; }
            set
            {
                _businessGroups = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }

        public Visibility IsProgressVisible
        {
            get { return _isProgressVisible; }
            set { _isProgressVisible = value; OnPropertyChanged();}
        }


        public MainViewModel()
        {
            //InsertGroup(new BusinessGroup() {Name = "Test"});
            //InsertBusiness(new Business() {Name = "Test", GroupName = "Test", PhoneNumber = "12345678910"});
            //InsertUpdateTimes(new UpdatedTimes(){ BusinessGroupUpdatedTime = DateTime.Now, BusinessUpadteDateTime = DateTime.Now });
            //BusinessGroups = BusinessData.GetGroupsAsync().Result;
            IsProgressVisible = Visibility.Visible;
            GetDataAsync();
        }

        private async Task GetDataAsync()
        {
            bool jsonFileExists = false;
            try
            {
                StorageFile textFile = await localFolder.GetFileAsync(_dataFileName);
                jsonFileExists = true;
                using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                {
                    // Read text stream     
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        //get size                       
                        uint textLength = (uint) textStream.Size;
                        await textReader.LoadAsync(textLength);
                        // read it                    
                        string jsonContents = textReader.ReadString(textLength);
                        // deserialize back to our product!  
                        var serializeBusinessGroup = JsonConvert.DeserializeObject<SerializeBusinessGroup>(jsonContents);
                        // and show it                     
                        BusinessGroups =
                            new ObservableCollection<BusinessGroupUI>(serializeBusinessGroup.BusinessGroupUis);
                        IsProgressVisible = Visibility.Collapsed;
                        
                        BusinessData.BusinessList = serializeBusinessGroup.BusinessList;

                        CheckAndUpdateDataFromService(serializeBusinessGroup);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {

            }
            catch (Exception ex)
            {
                jsonFileExists = false;
            }

            if (jsonFileExists == false)
            {
                bool networkError = false;
                try
                {
                var businessGroupsTable = App.SearchBusinessClient.GetTable<BusinessGroup>();
                var groups = await businessGroupsTable.ToListAsync();
                var businessTable = App.SearchBusinessClient.GetTable<Business>();
                foreach (var businessGroup in groups)
                {
                    BusinessGroupUI businessGroupUi = new BusinessGroupUI() { Id = businessGroup.Id, Name = businessGroup.Name };
                    var businessList = await businessTable.Take(1000).Where(p => p.GroupName == businessGroup.Name).ToListAsync();
                    businessGroupUi.Businesses = new ObservableCollection<Business>(businessList);
                    BusinessGroups.Add(businessGroupUi);
                    BusinessData.BusinessList.AddRange(businessList);
                    IsProgressVisible = Visibility.Collapsed;
                }
                }
                catch (MobileServiceInvalidOperationException ex)
                {
                    networkError = true;
                }

                if (networkError)
                {

                    MessageBoxResult msBoxResult =
                        await MessageBox.Show("No Network. App is Closing.", "Error", MessageBoxButton.OK);
                    Application.Current.Exit();
                    return;
                }

                CreateJsonFile();
            }
            //return businessGroups;
        }

        private async Task CheckAndUpdateDataFromService(SerializeBusinessGroup serializeBusinessGroup)
        {
            var updatedTimes = await App.SearchBusinessClient.GetTable<UpdatedTimes>().ToListAsync();

            var businessLastUpdated = updatedTimes[0].BusinessUpadteDateTime;

            var businessGroupsLastUpdated = updatedTimes[0].BusinessGroupUpdatedTime;

            if (businessGroupsLastUpdated > serializeBusinessGroup.BusinessGroupUpdatedDateTime)
            {
                var businessGroupsTable = App.SearchBusinessClient.GetTable<BusinessGroup>();
                var groups =
                    await
                        businessGroupsTable.Where(p => p.UpdateDateTime > serializeBusinessGroup.BusinessGroupUpdatedDateTime)
                            .ToListAsync();
                var businessTable = App.SearchBusinessClient.GetTable<Business>();
                foreach (var businessGroup in groups)
                {
                    BusinessGroupUI businessGroupUi = new BusinessGroupUI() {Id = businessGroup.Id, Name = businessGroup.Name};
                    var businessList =
                        await businessTable.Take(1000).Where(p => p.GroupName == businessGroup.Name).ToListAsync();
                    foreach (Business business in businessList)
                    {
                        businessGroupUi.Businesses.Add(business);
                    }
                    BusinessGroups.Add(businessGroupUi);
                    BusinessData.BusinessList.AddRange(businessList);
                }
            }
            if (businessLastUpdated > serializeBusinessGroup.BusinessUpdatedDateTime)
            {
                foreach (BusinessGroupUI businessGroupUi in BusinessGroups)
                {
                    var businessTable = App.SearchBusinessClient.GetTable<Business>();
                    BusinessGroupUI ui = businessGroupUi;
                    var businessList = await
                        businessTable.Take(1000)
                            .Where(
                                p =>
                                    p.GroupName == ui.Name &&
                                    p.UpdateDateTime > serializeBusinessGroup.BusinessUpdatedDateTime)
                            .ToListAsync();
                    foreach (var business in businessList)
                    {
                        if (ui.Businesses.Count(p => p.Name == business.Name) == 0)
                        {
                            ui.Businesses.Add(business);
                        }
                        if (BusinessData.BusinessList.Count(p => p.Name == business.Name) == 0)
                        {
                            BusinessData.BusinessList.Add(business);
                        }
                    }
                }
            }

            if (businessGroupsLastUpdated > serializeBusinessGroup.BusinessGroupUpdatedDateTime ||
                businessLastUpdated > serializeBusinessGroup.BusinessUpdatedDateTime)
            {
                CreateJsonFile(businessGroupsLastUpdated, businessLastUpdated);
            }
        }

        private async Task CreateJsonFile(DateTime? businessGroupsLastUpdated = null, DateTime? businessLastUpdated = null)
        {
            if (businessGroupsLastUpdated == null || businessLastUpdated == null)
            {
                var updatedTimes = await App.SearchBusinessClient.GetTable<UpdatedTimes>().ToListAsync();

                businessLastUpdated = updatedTimes[0].BusinessUpadteDateTime;

                businessGroupsLastUpdated = updatedTimes[0].BusinessGroupUpdatedTime;
            }

            if (businessGroupsLastUpdated != null &&  businessLastUpdated != null)
            {
                SerializeBusinessGroup serializeBusinessGroup = new SerializeBusinessGroup()
                {
                    BusinessGroupUpdatedDateTime = (DateTime)businessGroupsLastUpdated,
                    BusinessUpdatedDateTime = (DateTime)businessLastUpdated,
                    BusinessGroupUis = BusinessGroups.ToList(),
                    BusinessList = BusinessData.BusinessList
                };

                // Serialize our Product class into a string             
                string jsonContents = JsonConvert.SerializeObject(serializeBusinessGroup);
                // Get the app data folder and create or replace the file we are storing the JSON in.   
                StorageFile textFile = await localFolder.CreateFileAsync(_dataFileName,
                    CreationCollisionOption.ReplaceExisting);
                // Open the file...      
                using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    // write the JSON string!
                    using (DataWriter textWriter = new DataWriter(textStream))
                    {
                        textWriter.WriteString(jsonContents);
                        await textWriter.StoreAsync();
                    }
                }
            }
        }


        //private static readonly ObservableCollection<BusinessGroupUI> businessGroups = new ObservableCollection<BusinessGroupUI>();
        //private static List<BusinessGroup> _businessGroupsList = new List<BusinessGroup>();
        

        //public static async Task<ObservableCollection<BusinessGroupUI>> GetGroupsAsync()
        //{
        //    if (businessGroups.Count > 0) return businessGroups;
        //    var businessGroupsList = await App.SearchBusinessClient.GetTable<BusinessGroup>().ToListAsync();
        //    var table = App.SearchBusinessClient.GetTable<Business>();
        //    foreach (var businessGroup in businessGroupsList)
        //    {
        //        BusinessGroupUI businessGroupUi = new BusinessGroupUI() { Id = businessGroup.Id, Name = businessGroup.Name };
        //        var _businessList = await table.Take(1000).Where(p => p.GroupName == businessGroup.Name).ToListAsync();
        //        foreach (Business business in _businessList)
        //        {
        //            businessGroupUi.Businesses.Add(business);
        //        }
        //        businessGroups.Add(businessGroupUi);
        //        BusinessList.AddRange(_businessList);
        //    }
        //    return businessGroups;
        //}

        private async Task InsertGroup(BusinessGroup businessGroup)
        {
            IMobileServiceTable<BusinessGroup> businessGroupTable =
                App.SearchBusinessClient.GetTable<BusinessGroup>();
            try
            {
                await businessGroupTable.InsertAsync(businessGroup);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task InsertBusiness(Business business)
        {
            IMobileServiceTable<Business> businessTable =
                App.SearchBusinessClient.GetTable<Business>();
            try
            {
                await businessTable.InsertAsync(business);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task InsertUpdateTimes(UpdatedTimes updatedTimes)
        {
            IMobileServiceTable<UpdatedTimes> businessTable =
                App.SearchBusinessClient.GetTable<UpdatedTimes>();
            try
            {
                await businessTable.InsertAsync(updatedTimes);
            }
            catch (Exception ex)
            {

            }
        }

        internal void PhoneNumberClick(string phoneNumber)
        {
            PhoneCallManager.ShowPhoneCallUI(phoneNumber, "");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}