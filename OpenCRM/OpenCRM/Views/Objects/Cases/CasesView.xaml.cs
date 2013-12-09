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
using OpenCRM.Models.Objects.Cases;

namespace OpenCRM.Views.Objects.Cases
{
    /// <summary>
    /// Lógica de interacción para CasesView.xaml
    /// </summary>
    public partial class CasesView
    {
        CasesModel _casesModel;
        public CasesView()
        {
            InitializeComponent();
            _casesModel = new CasesModel();
            cmbCasesType.Items.Add("Recent Cases");
            cmbCasesType.Items.Add("Today's Cases");
            cmbCasesType.Items.Add("Cases With Solution");
            cmbCasesType.Items.Add("Cases Without Solution");
            cmbCasesType.Items.Add("All Cases");
            cmbCasesType.SelectedValue = "Recent Cases";
            _casesModel.LoadCases(this.DataGridCases, "Recent Cases");
        }

        private void DataGridCases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.DataGridCases.SelectedIndex == -1)
                return;

            var selectedItem = this.DataGridCases.SelectedItem;
            Type type = selectedItem.GetType();
            CasesModel.CaseIdforEdit = Convert.ToInt32(type.GetProperty("CaseId").GetValue(selectedItem, null));

            CasesModel.IsNew = false;
            PageSwitcher.Switch("/Views/Objects/Cases/CaseDetails.xaml");
        }

        private void btn_EditCase_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGridCases.SelectedIndex == -1)
                return;

            var selectedItem = this.DataGridCases.SelectedItem;
            Type type = selectedItem.GetType();
            CasesModel.CaseIdforEdit = Convert.ToInt32(type.GetProperty("CaseId").GetValue(selectedItem, null));

            CasesModel.IsNew = false;
            PageSwitcher.Switch("/Views/Objects/Cases/CreateCase.xaml");
        }

        private void btn_NewCase_OnClick(object sender, RoutedEventArgs e)
        {
            CasesModel.IsNew = true;
            PageSwitcher.Switch("/Views/Objects/Cases/CreateCase.xaml");
        }

        private void CaseImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
        }

        private void cmbSearchTypeCases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            _casesModel.LoadCases(this.DataGridCases, combo.SelectedItem.ToString());
        }
    }
}
