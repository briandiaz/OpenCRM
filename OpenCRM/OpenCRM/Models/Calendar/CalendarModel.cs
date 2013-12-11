using OpenCRM.Controllers.Session;
using OpenCRM.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpenCRM.Models.Calendar
{
    public static class CalendarModel
    {
        #region "Methods"
        public static List<Appointment> getAllAppointments()
        {
            var listAppointments = new List<Appointment>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var queryOpportunity = (
                        from opportunity in db.Opportunities
                        where opportunity.UserId == Session.UserId
                        select new
                        {
                            Id = opportunity.OpportunityId,
                            Type = AppointmentType.Opportunity,
                            Title = opportunity.Name,
                            Account = (opportunity.AccountId.HasValue) ? opportunity.Account.Name : string.Empty,
                            Campaign = (opportunity.CampaignPrimarySourceId.HasValue) ? opportunity.Campaign.Name : string.Empty,
                            Product = (opportunity.ProductId.HasValue) ? opportunity.Products.Name : string.Empty,
                            EndTime = opportunity.CloseDate
                        }
                    ).ToList().AsParallel().Select(x =>
                        new Appointment(x.Id, x.Type)
                        {
                            Title = x.Title,
                            Subject = x.Type.ToString(),
                            Details =
                                ((x.Account == string.Empty) ? string.Empty : "Account: " + x.Account + "\n") +
                                ((x.Campaign == string.Empty) ? string.Empty : "Campaign: " + x.Campaign + "\n") +
                                ((x.Product == string.Empty) ? string.Empty : "Product: " + x.Product + "\n"),
                            EndTime = x.EndTime
                        }
                    );

                    var queryCampaing = ((
                        from campaign in db.Campaign
                        where campaign.UserId == Session.UserId
                        select new
                        {
                            Id = campaign.CampaignId,
                            Type = AppointmentType.Campaign,
                            Title = campaign.Name,
                            EndTime = campaign.EndDate
                        }
                    ).ToList().AsParallel().Select(x =>
                        new Appointment(x.Id, x.Type)
                        {
                            Title = x.Title,
                            Subject = x.Type.ToString(),
                            EndTime = x.EndTime
                        }
                    ));

                    Parallel.ForEach(queryOpportunity, item => listAppointments.Add(item));
                    Parallel.ForEach(queryCampaing, item => listAppointments.Add(item));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return listAppointments;
        }

        #endregion
    }

    public class Appointment
    {
        #region "Values"
        private int _appointmentId;
        private AppointmentType _type;
        private string _subject;
        private string _details;
        private string _title;
        private Nullable<DateTime> _endTime;
        
        #endregion

        #region "Properties"
        public int AppointmentId
        {
            get { return this._appointmentId; }
        }
        public AppointmentType Type
        {
            get { return _type; }
        }
        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }
        public string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }
        public string Details
        {
            get { return this._details; }
            set { this._details = value; }
        }
        public Nullable<DateTime> EndTime
        {
            get { return this._endTime; }
            set { this._endTime = value; }
        }

        #endregion

        #region Constructors
        public Appointment(int AppointmentId, AppointmentType Type)
        {
            this._appointmentId = AppointmentId;
            this._type = Type;
        }

        #endregion
    }

    public enum AppointmentType
    {
        Opportunity = 1,
        Campaign
    }
}
