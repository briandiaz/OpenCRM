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

using OpenCRM.Models.Objects.Opportunities;
using OpenCRM.Models.Calendar;
using OpenCRM.Models.Objects.Campaigns;
using OpenCRM.Controllers.Campaign;

namespace OpenCRM.Views.Calendar
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Page
    {
        MonthCalendarControl MonthCalendar;
        List<Appointment> AppointmentsList;

        public CalendarView()
        {
            InitializeComponent();

            MonthCalendar = new MonthCalendarControl();
            MonthCalendar.VerticalAlignment = VerticalAlignment.Stretch;
            MonthCalendar.VerticalContentAlignment = VerticalAlignment.Stretch;
            MonthCalendar.DisplayMonthChanged += DisplayMonthChanged;
            //MonthCalendar.DayBoxDoubleClicked += DayBox_DoubleClicked;
            MonthCalendar.AppointmentDblClicked += Appointment_Clicked;

            this.Calendar.Children.Add(MonthCalendar);
            AppointmentsList = CalendarModel.getAllAppointments();

            SetAppointments();
        }

        private void Appointment_Clicked(int Appointment_Id, AppointmentType Type)
        {
            if(AppointmentType.Opportunity == Type)
            {
                OpportunitiesModel.IsViewingCalendar = true;
                OpportunitiesModel.IsEditing = true;
                OpportunitiesModel.IsNew = false;

                OpportunitiesModel.EditOpportunityId = Appointment_Id;

                PageSwitcher.Switch("/Views/Objects/Opportunities/OpportunitiesDetails.xaml");
            }
            else if (AppointmentType.Campaign == Type)
            {
                CampaignController.CurrentCampaignId = Appointment_Id;
                PageSwitcher.Switch("/Views/Objects/Campaigns/Edit.xaml");
            }
        }

        private void DisplayMonthChanged(MonthChangedEventArgs e)
        {
            SetAppointments();
        }

        private void SetAppointments()
        {
            MonthCalendar.MonthAppointments = AppointmentsList.FindAll(
                item =>
                    item.EndTime != null &&
                    Convert.ToDateTime(item.EndTime).Month == MonthCalendar.DisplayStartDate.Month &&
                    Convert.ToDateTime(item.EndTime).Year == MonthCalendar.DisplayStartDate.Year
            );

            MonthCalendar.BuildCalendarUI();
        }
    }
}
