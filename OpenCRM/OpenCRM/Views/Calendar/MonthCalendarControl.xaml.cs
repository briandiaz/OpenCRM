using OpenCRM.Controllers.Session;
using OpenCRM.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace OpenCRM.Views.Calendar
{
    /// <summary>
    /// Interaction logic for MonthCalendarControl.xaml
    /// </summary>
    public partial class MonthCalendarControl : UserControl
    {
        #region "Values"
        internal DateTime _DisplayStartDate = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1));
        private int _DisplayMonth;
        private int _DisplayYear;
        private CultureInfo _cultureInfo = new CultureInfo(CultureInfo.CurrentUICulture.LCID);
        private System.Globalization.Calendar sysCal;
        private List<Appointment> _monthAppointments;

        #endregion

        #region "Properties"
        public DateTime DisplayStartDate
        {
            get { return _DisplayStartDate; }
            set
            {
                _DisplayStartDate = value;
                _DisplayMonth = _DisplayStartDate.Month;
                _DisplayYear = _DisplayStartDate.Year;
            }
        }
        public List<Appointment> MonthAppointments
        {
            get { return _monthAppointments; }
            set { _monthAppointments = value; }
        }

        #endregion

        #region "Constructor"
        public MonthCalendarControl()
        {
            InitializeComponent();

            _DisplayMonth = _DisplayStartDate.Month;
            _DisplayYear = _DisplayStartDate.Year;
            _cultureInfo = new CultureInfo(CultureInfo.CurrentUICulture.LCID);
            sysCal = _cultureInfo.Calendar;
        }

        #endregion

        #region "Methods"
        public void BuildCalendarUI()
        {
            int iDaysInMonth = sysCal.GetDaysInMonth(_DisplayStartDate.Year, _DisplayStartDate.Month);
            int iOffsetDays = Convert.ToInt32(Enum.ToObject(typeof(DayOfWeek), _DisplayStartDate.DayOfWeek));
            int iWeekCount = 0;

            WeekOfDaysControl weekRowCtrl = new WeekOfDaysControl();

            MonthViewGrid.Children.Clear();
            AddRowsToMonthGrid(iDaysInMonth, iOffsetDays);
            MonthYearLabel.Content = new DateTimeFormatInfo().GetMonthName(_DisplayMonth) + " " + _DisplayYear;

            for (int i = 1; i <= iDaysInMonth; i++)
            {
                if ((i != 1) && Math.IEEERemainder((i + iOffsetDays - 1), 7) == 0)
                {
                    Grid.SetRow(weekRowCtrl, iWeekCount);
                    MonthViewGrid.Children.Add(weekRowCtrl);
                    weekRowCtrl = new WeekOfDaysControl();
                    iWeekCount += 1;
                }

                DayBoxControl dayBox = new DayBoxControl();
                dayBox.DayNumberLabel.Content = i.ToString();
                dayBox.Tag = i;

                dayBox.MouseEnter += dayBox_MouseEnter;
                dayBox.MouseLeave += dayBox_MouseLeave;

                if ((new DateTime(_DisplayYear, _DisplayMonth, i)) == DateTime.Today)
                {
                    dayBox.DayLabelRowBorder.Background = (Brush)dayBox.TryFindResource("OrangeGradientBrush");
                    dayBox.DayAppointmentsStack.Background = Brushes.Wheat;
                }

                if (_monthAppointments != null)
                {
                    int iday = i;
                    List<Appointment> aptInDay = _monthAppointments.FindAll(
                        new System.Predicate<Appointment>((Appointment apt) => Convert.ToDateTime(apt.EndTime).Day == iday)
                    );

                    foreach (Appointment item in aptInDay)
                    {
                        var apt = new DayBoxAppointmentControl();
                        apt.DisplayText.Text = item.Title;
                        apt.Tag = item.AppointmentId;
                        apt.DisplayText.Tag = item.Type;

                        ToolTip tooltip = new ToolTip { Content = item.Subject + "\n" + item.Details };
                        apt.DisplayText.ToolTip = tooltip;

                        apt.MouseUp += Appointment_Click;
                        dayBox.DayAppointmentsStack.Children.Add(apt);
                    }
                }

                Grid.SetColumn(dayBox, (i - (iWeekCount * 7)) + iOffsetDays);
                weekRowCtrl.WeekRowGrid.Children.Add(dayBox);
            }

            Grid.SetRow(weekRowCtrl, iWeekCount);
            MonthViewGrid.Children.Add(weekRowCtrl);
        }

        private void AddRowsToMonthGrid(int DaysInMonth, int OffSetDays)
        {
            MonthViewGrid.RowDefinitions.Clear();
            GridLength rowHeight = new GridLength(60, GridUnitType.Star);

            int EndOffSetDays = 7 - (Convert.ToInt32(Enum.ToObject(typeof(DayOfWeek), _DisplayStartDate.AddDays(DaysInMonth - 1).DayOfWeek)) + 1);

            for (int i = 1; i <= Convert.ToInt32((DaysInMonth + OffSetDays + EndOffSetDays) / 7); i++)
            {
                dynamic rowDef = new RowDefinition();
                rowDef.Height = rowHeight;
                MonthViewGrid.RowDefinitions.Add(rowDef);
            }
        }

        private void UpdateMonth(int MonthsToAdd)
        {
            MonthChangedEventArgs ev = new MonthChangedEventArgs();
            ev.OldDisplayStartDate = _DisplayStartDate;
            this.DisplayStartDate = _DisplayStartDate.AddMonths(MonthsToAdd);
            ev.NewDisplayStartDate = _DisplayStartDate;

            if (DisplayMonthChanged != null)
            {
                DisplayMonthChanged(ev);
            }
        }

        public void MonthView()
        {
            Loaded += MonthView_Loaded;
        }

        #endregion

        #region "Delegates and Events"
        public event DisplayMonthChangedEventHandler DisplayMonthChanged;
        public delegate void DisplayMonthChangedEventHandler(MonthChangedEventArgs e);
        public event AppointmentDblClickedEventHandler AppointmentDblClicked;
        public delegate void AppointmentDblClickedEventHandler(int Appointment_Id, AppointmentType Type);

        #endregion

        #region "UI Event Handlers"

        private void MonthView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_monthAppointments == null)
                BuildCalendarUI();
        }

        private void MonthGoPrev_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            UpdateMonth(-1);
        }

        private void MonthGoNext_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            UpdateMonth(1);
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is DayBoxAppointmentControl)
            {
                var control = (DayBoxAppointmentControl) e.Source;
                if (control.Tag != null)
                {
                    if (AppointmentDblClicked != null)
                        AppointmentDblClicked(Convert.ToInt32(control.Tag), (AppointmentType)Convert.ToInt32(control.DisplayText.Tag));
                }

                e.Handled = true;
            }
        }

        private void dayBox_MouseLeave(object sender, MouseEventArgs e)
        {
            var dayBox = (sender as DayBoxControl);

            dayBox.DayLabelRowBorder.Background = (Brush)dayBox.DayLabelRowBorder.Tag;
            dayBox.DayAppointmentsStack.Background = (Brush)dayBox.DayAppointmentsStack.Tag;
        }

        private void dayBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var dayBox = (sender as DayBoxControl);

            dayBox.DayLabelRowBorder.Tag = dayBox.DayLabelRowBorder.Background;
            dayBox.DayAppointmentsStack.Tag = dayBox.DayAppointmentsStack.Background;

            dayBox.DayAppointmentsStack.Background = Brushes.LightGray;
            dayBox.DayLabelRowBorder.Background = (Brush)dayBox.TryFindResource("GrayGradientBrush");
        }

        #endregion
    }

    public struct MonthChangedEventArgs
    {
        public DateTime OldDisplayStartDate;
        public DateTime NewDisplayStartDate;
    }

}