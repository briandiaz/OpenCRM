using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OpenCRM.DataBase;
using OpenCRM.Models.Objects;
using OpenCRM.Controllers.Session;

namespace OpenCRM.Models.Dashboard
{
    class DashboardModel
    {
        List<ChartObject> _chartObjects;
        public DashboardModel()
        { 
        
        }

        #region "Campaigns"

        public List<ChartObject> GroupCampaignsByStatus()
        {
            _chartObjects = new List<ChartObject>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (from _campaign in _db.Campaign
                                 where _campaign.UserId == Session.UserId
                                 group _campaign by new
                                 {
                                     _campaign.Campaign_Status.Name
                                 } into campaign
                                 select new ChartObject()
                                 {
                                     Quantity = campaign.Count(),
                                     Name = (campaign.Key.Name != null) ? campaign.Key.Name : "None"
                                 }
                    ).ToList();
                    _chartObjects = query;
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
            return _chartObjects;
        }

        public List<ChartObject> GroupCampaignsByType()
        {
            _chartObjects = new List<ChartObject>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (from _campaign in _db.Campaign
                                 where _campaign.UserId == Session.UserId
                                 group _campaign by new
                                 {
                                     _campaign.Campaign_Type.Name
                                 } into campaign
                                 select new ChartObject()
                                 {
                                     Quantity = campaign.Count(),
                                     Name = (campaign.Key.Name != null) ? campaign.Key.Name : "None"
                                 }
                    ).ToList();
                    _chartObjects = query;
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
            return _chartObjects;
        }

        public List<ChartObjectPrice> GroupCampaignsByExpectedRevenue()
        {
            List<ChartObjectPrice> _chartObjectsPrice = new List<ChartObjectPrice>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var ranges = new[] { 10000, 100000, 1000000, 2000000, 40000000 };

                    var RevenueGroups = _db.Campaign.GroupBy(x => ranges.FirstOrDefault(y => y > x.ExpectedRevenue && x.UserId == Session.UserId))
                                                            .Select(g => new ChartObjectPrice
                                                            {
                                                                Price = g.Key,
                                                                Quantity = (int)g.Count()
                                                            }
                      ).ToList();

                    var groupedPrizes = ranges.Select(obj => new
                                            ChartObjectPrice
                    {
                        Price = obj,
                        Quantity = (int)RevenueGroups.Where(ord => ord.Price > obj || ord.Price == 0).Sum(g => g.Quantity)
                    }
                    ).ToList();
                    _chartObjectsPrice = groupedPrizes;
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
            return _chartObjectsPrice;
        }

        public List<ChartObject> GroupCampaignsByLeads()
        {
            List<ChartObject> _chartObjects = new List<ChartObject>();
            try
            {

                using (var _db = new OpenCRMEntities())
                {
                    var query = (from _campaign in _db.Campaign
                                 where _campaign.UserId == Session.UserId
                                 group _campaign by new
                                 {
                                     _campaign.Leads
                                 } into campaign
                                 select new ChartObject()
                                 {
                                     Quantity = campaign.Count(),
                                     Name = campaign.ToString()
                                 }
                    ).ToList();
                    _chartObjects = query;
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
            return _chartObjects;
        }

        #endregion

        #region "Oportunities"
        public List<ChartObject> GroupOportunitiesByStatus()
        {
            _chartObjects = new List<ChartObject>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (from _oportunities in _db.Opportunities
                                 where _oportunities.UserId == Session.UserId
                                 group _oportunities by new
                                 {
                                     _oportunities.Opportunities_Status.Name
                                 } into oportunities
                                 select new ChartObject()
                                 {
                                     Quantity = oportunities.Count(),
                                     Name = (oportunities.Key.Name != null) ? oportunities.Key.Name : "None"
                                 }
                    ).ToList();
                    _chartObjects = query;
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
            return _chartObjects;
        }

        public List<ChartObject> GroupOportunitiesByStage()
        {
            _chartObjects = new List<ChartObject>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (from _oportunities in _db.Opportunities
                                 where _oportunities.UserId == Session.UserId
                                 group _oportunities by new
                                 {
                                     _oportunities.Opportunities_Stage.Name
                                 } into oportunities
                                 select new ChartObject()
                                 {
                                     Quantity = oportunities.Count(),
                                     Name = (oportunities.Key.Name != null) ? oportunities.Key.Name : "None"
                                 }
                    ).ToList();
                    _chartObjects = query;
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
            return _chartObjects;
        }

        public List<ChartObject> GroupOportunitiesByLeadSource()
        {
            _chartObjects = new List<ChartObject>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (from _oportunities in _db.Opportunities
                                 where _oportunities.UserId == Session.UserId
                                 group _oportunities by new
                                 {
                                     _oportunities.Lead_Source.Name
                                 } into oportunities
                                 select new ChartObject()
                                 {
                                     Quantity = oportunities.Count(),
                                     Name = (oportunities.Key.Name != null) ? oportunities.Key.Name : "None"
                                 }
                    ).ToList();
                    _chartObjects = query;
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
            return _chartObjects;
        }

        public List<ChartObject> GroupOportunitiesByProbability()
        {
            //Needs to be fixed
            _chartObjects = new List<ChartObject>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (from _oportunities in _db.Opportunities
                                 where _oportunities.UserId == Session.UserId
                                 group _oportunities by new
                                 {
                                     _oportunities.Opportunities_Stage
                                 } into oportunities
                                 select new ChartObject()
                                 {
                                     Quantity = oportunities.Count(),
                                     Name = (oportunities.Key.Opportunities_Stage.Probability.HasValue) ? oportunities.Key.Opportunities_Stage.Name + " - " + (Nullable<int>)oportunities.Key.Opportunities_Stage.Probability.Value + "%" : "None"
                                 }
                    ).ToList();
                    _chartObjects = query;
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
            return _chartObjects;
        }

        #endregion

        #region "Products"

        #endregion

        #region "Leads"

        #endregion
        #region "Contacts"

        #endregion
    }

    public class ChartObject
    {
        public int Quantity { get; set; }
        public String Name { get; set; }

        public ChartObject()
        {

        }

        public ChartObject(int Quantity, String Name)
        {
            this.Name = Name;
            this.Quantity = Quantity;
        }
    }

    public class ChartObjectPrice
    {
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        public ChartObjectPrice()
        {

        }
        public ChartObjectPrice(int quantity, decimal? Price)
        {
            this.Quantity = quantity;
            this.Price = Price;
        }

    }
}
