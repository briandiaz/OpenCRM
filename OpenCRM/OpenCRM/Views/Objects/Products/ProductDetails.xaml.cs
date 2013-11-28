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
using OpenCRM.Models.Objects.Products;

namespace OpenCRM.Views.Objects.Products
{
   
    /// <summary>
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Page
    {
        private ProductsModel _productModel;

        public ProductDetails()
        {
            InitializeComponent();
            _productModel = new ProductsModel();
            _productModel.LoadProductDetails(this);
        }

        private void btnEditLead_OnClick(object sender, RoutedEventArgs e)
        {
           // var ProductId = ProductsModel.EditProductId;

            ProductsModel.IsEditing = true;
            ProductsModel.IsNew = false;
            ProductsModel.IsSearching = false;
            

            PageSwitcher.Switch("/Views/Objects/Products/CreateProduct.xaml");
        }

        private void btnEditProductCancel_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Products/ProductsView.xaml");
        }
    }
}
