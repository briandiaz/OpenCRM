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
using OpenCRM.DataBase;
using OpenCRM.Models.Home;

namespace OpenCRM.Views.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeModel();
            PageSwitcher.MainButtons(true);
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            MahApps.Metro.Controls.Tile _thisTile = (MahApps.Metro.Controls.Tile)sender;
            switch (_thisTile.Title)
            {
                case "Accounts":
                    PageSwitcher.Switch("/Views/Objects/Accounts/AccountsView.xaml");
                break;
                
                case "Products":
                    PageSwitcher.Switch("/Views/Objects/Products/ProductsView.xaml");
                    break;
           
            }
            
            
                
            
        }

    }
}
