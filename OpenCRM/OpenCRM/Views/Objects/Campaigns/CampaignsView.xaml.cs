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
using OpenCRM.Controllers.Campaign;


namespace OpenCRM.Views.Objects.Campaigns
{
    /// <summary>
    /// Lógica de interacción para CampaingsView.xaml
    /// </summary>
    public partial class CampaignsView
    {
        List<SearchAttribute> _searchTargets;
        String _searchKey;
        List<CampaignsModel> _listCampaigns;
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
            List<SearchAttribute> _keyTypes = new List<SearchAttribute>();
            SearchAttribute keyType = new SearchAttribute(1, "Type");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(2, "Status");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(3, "Active");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(4, "Name");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(5, "Start Date");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(6, "End Date");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(7, "Expected Revenue");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(8, "Expected Response");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(9, "Number Sent");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(10, "Actual Cost");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(11, "Budgeted Cost");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(12, "Created By");
            _keyTypes.Add(keyType);
            keyType = new SearchAttribute(13, "Updated By");
            _keyTypes.Add(keyType);
            cmbTargetKeyCampaign.ItemsSource = _keyTypes;
            cmbTargetKeyCampaign.DisplayMemberPath = "Name";
            cmbTargetKeyCampaign.SelectedValuePath = "ID";
            cmbTargetKeyCampaign.SelectedIndex = 0;
        }

        private void cmbTargetKeyCampaign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAttribute thisTarget = (SearchAttribute)cmbTargetKeyCampaign.SelectedItem;
            switch (thisTarget.ID)
            {
                case 1:
                case 2:
                    cmbTargetValueCampaign.ItemsSource = null;
                    cmbTargetValueCampaign.ItemsSource = getSearchTargetKeys(thisTarget);
                    cmbTargetValueCampaign.Visibility = System.Windows.Visibility.Visible;

                    cmbTargetValueCampaign.DisplayMemberPath = "Name";
                    cmbTargetValueCampaign.SelectedValuePath = "ID";
                    cmbTargetValueCampaign.SelectedIndex = 0;
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
        private List<SearchAttribute> getSearchTargetKeys(SearchAttribute actualAtt)
        {
            _searchTargets = new List<SearchAttribute>();
            try {
                using (var _db = new OpenCRMEntities())
                {
                    switch (actualAtt.ID)
                    { 
                        case 1:
                            var query = (
                                    from type in _db.Campaign_Type
                                    select new SearchAttribute()
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
                                    select new SearchAttribute()
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

        private void cmbTargetValueCampaign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSearchKey((SearchAttribute)cmbTargetKeyCampaign.SelectedItem, (SearchAttribute)cmbTargetValueCampaign.SelectedItem);
        }

        private void SelectedSearchKey(SearchAttribute key,SearchAttribute valueSelected)
        {
            try
            {
                CampaignsModel _cmp;
                if (key.ID.Equals(1) && valueSelected != null)
                {
                     _cmp = new CampaignsModel();
                     _searchKey = "Status";
                     _cmp.LoadCampaigns(gridCampaign, valueSelected.ID, _searchKey);
                     _listCampaigns = _cmp.listCampaigns;
                     CampaignController.currentCampaignIndex = 0;
                     CampaignController.previousCampaignIndex = CampaignController.currentCampaignIndex--;
                     CampaignController.nextCampaignIndex = CampaignController.currentCampaignIndex++;

                     
                }
                else if (key.ID.Equals(2) && valueSelected != null)// && ((SearchAttribute)cmbTargetKeyCampaign.SelectedItem).ID == 2)
                {
                    _cmp = new CampaignsModel();
                    _searchKey = "Type";
                    _cmp.LoadCampaigns(gridCampaign, valueSelected.ID, _searchKey);
                    _listCampaigns = _cmp.listCampaigns;
                    CampaignController.currentCampaignIndex = 0;
                    CampaignController.previousCampaignIndex = CampaignController.currentCampaignIndex--;
                    CampaignController.nextCampaignIndex = CampaignController.currentCampaignIndex++;
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            OpenCRM.Controllers.Campaign.CampaignController.CurrentCampaignId = Convert.ToInt32(tbxCampaignId.Text);
            PageSwitcher.Switch("/Views/Objects/Campaigns/Edit.xaml");
        }

        private void btnNextSlider_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(CampaignController.currentCampaignIndex);
            if (CampaignController.currentCampaignIndex < _listCampaigns.Count-1)
            {
                CampaignController.currentCampaignIndex = CampaignController.currentCampaignIndex + 1;
                gridCampaign.DataContext = _listCampaigns[CampaignController.currentCampaignIndex];
                CampaignController.previousCampaignIndex = CampaignController.currentCampaignIndex - 1;
                CampaignController.nextCampaignIndex = CampaignController.currentCampaignIndex + 1;
            }
        }

        private void btnPreviousSlider_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(CampaignController.currentCampaignIndex);
            if (CampaignController.currentCampaignIndex > 0)
            {
                CampaignController.currentCampaignIndex = CampaignController.currentCampaignIndex - 1;
                gridCampaign.DataContext = _listCampaigns[CampaignController.currentCampaignIndex];
                CampaignController.previousCampaignIndex = CampaignController.currentCampaignIndex - 1;
                CampaignController.nextCampaignIndex = CampaignController.currentCampaignIndex + 1;
            }
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
}
