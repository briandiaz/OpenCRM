using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenCRM.Models.Objects.Campaigns;
using System.Data.SqlClient;
using OpenCRM.DataBase;
using OpenCRM.Controllers.Session;


namespace OpenCRM.Views.Objects.Campaigns
{
    /// <summary>
    /// Lógica de interacción para CampaingsView.xaml
    /// </summary>
    public partial class CampaignsView
    {
        public CampaignsView()
        {
            InitializeComponent();
            loadKeyType();
        }

        private void bntCreate_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/Create.xaml");
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Home/HomeView.xaml");
        }

        private void loadKeyType()
        {
            List<SearchTargets> _keyTypes = new List<SearchTargets>();
            SearchTargets keyType = new SearchTargets(1,"Type");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(2, "Status");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(3, "Active");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(4, "Name");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(5, "Start Date");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(6, "End Date");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(7, "Expected Revenue");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(8, "Expected Response");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(9, "Number Sent");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(10, "Actual Cost");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(11, "Budgeted Cost");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(12, "Created By");
            _keyTypes.Add(keyType);
            keyType = new SearchTargets(13, "Updated By");
            _keyTypes.Add(keyType);
            cmbTargetKeyCampaign.ItemsSource = _keyTypes;
            cmbTargetKeyCampaign.DisplayMemberPath = "Name";
            cmbTargetKeyCampaign.SelectedValuePath = "ID";
            cmbTargetKeyCampaign.SelectedItem = 1;
        }

        private void cmbTargetKeyCampaign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAttribute thisTarget = (SearchAttribute)cmbTargetKeyCampaign.SelectedItem;
            switch (thisTarget.ID)
            {
                case 1:
                case 2:
                    cmbTargetValueCampaign.ItemsSource = getSearchTargetKeys(thisTarget.ID);
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Visible;

                    cmbTargetValueCampaign.DisplayMemberPath = "Name";
                    cmbTargetValueCampaign.SelectedValuePath = "ID";
                    cmbTargetValueCampaign.SelectedItem = 1;
                    break;
                case 3:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 4:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 5:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 6:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 7:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 8:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 9:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 10:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 11:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 12:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case 13:
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Hidden;
                    break;
            }
        }
        private List<CampaignsModel> getCampaignsBySearch(String atts)
        {
            return null;
        }
        private List<SearchTargets> getSearchTargetKeys(int by)
        {
            List<SearchTargets> _searchTargets = new List<SearchTargets>();
            try {
                using (var _db = new OpenCRMEntities())
                {
                    switch (by)
                    { 
                        case 1:
                            var query = (
                                    from type in _db.Campaign_Type
                                    select new SearchTargets()
                                    {
                                        ID = type.CampaignTypeId,
                                        Name = type.Name
                                    }
                            ).ToList();
                            _searchTargets = query;
                            break;
                        case 2:
                            query = (
                                    from status in _db.Campaign_Status
                                    select new SearchTargets()
                                    {
                                        ID = status.CampaignStatusId,
                                        Name = status.Name
                                    }
                            ).ToList();
                            _searchTargets = query;
                            break;
                    }
                }
            }   
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _searchTargets;
        }
    }
    public class SearchAttribute {
        
        public int ID { get; set; }
        public String Name { get; set; }
        public SearchAttribute() { }
        public SearchAttribute(int id, String Name)
        {
            this.ID = id;
            this.Name = Name;
        }
    }
    public class SearchTargets : SearchAttribute
    {
        public SearchTargets() { }
        public SearchTargets(int ID, String Name) : base(ID,Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
