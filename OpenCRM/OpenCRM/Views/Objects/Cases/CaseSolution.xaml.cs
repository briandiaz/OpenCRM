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
    /// Interaction logic for CaseSolution.xaml
    /// </summary>
    public partial class CaseSolution : Page
    {
        CasesModel _caseModel;
        public CaseSolution()
        {
            InitializeComponent();
            _caseModel = new CasesModel();
            _caseModel.LoadSolution(this);
        }

        private void btnSaveSolution_OnClick(object sender, RoutedEventArgs e)
        {
            _caseModel.SaveSolution(this);
            PageSwitcher.Switch("/Views/Objects/Cases/CaseDetails.xaml");
        }

        private void btnCancelSolution_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CaseDetails.xaml");
        }

        private void LeadImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
