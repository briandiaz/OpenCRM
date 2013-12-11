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
    /// Interaction logic for CaseDetails.xaml
    /// </summary>
    public partial class CaseDetails : Page
    {
        CasesModel _casesModel;
        public CaseDetails()
        {
            InitializeComponent();
            _casesModel = new CasesModel();
            _casesModel.LoadCaseDetails(this);
        }

        private void btnEditCase_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CreateCase.xaml");
        }

        private void btnDelete_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnSolution_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CaseSolution.xaml");
        }

        private void CaseImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
        }
    }
}
