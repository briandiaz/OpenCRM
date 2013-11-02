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

namespace OpenCRM.Views.Objects.Products
{
    /// <summary>
    /// Lógica de interacción para ProductsView.xaml
    /// </summary>
    public partial class ProductsView
    {
        public ProductsView()
        {
            InitializeComponent();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Products/CreateProduct.xaml");
        }

    }
}
