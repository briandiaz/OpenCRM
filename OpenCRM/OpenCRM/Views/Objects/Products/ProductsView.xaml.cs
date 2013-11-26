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
using OpenCRM.Controllers.Session;
using OpenCRM.DataBase;
using OpenCRM.Models.Objects.Products;
using OpenCRM.Models.Settings;

namespace OpenCRM.Views.Objects.Products
{
    /// <summary>
    /// Lógica de interacción para ProductsView.xaml
    /// </summary>
    public partial class ProductsView
    {
        ProductsModel pm;
        public ProductsView()
        {
            InitializeComponent();
            pm = new ProductsModel();
            pm.LoadRecentProduts(this.RecentProductsGrid);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Products/CreateProduct.xaml");
        }

        private void RecentProductsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        public void ProdutNameHyperlink_Click(object sender, RoutedEventArgs e)
        {  
            var ProductId = Convert.ToInt32((sender as TextBlock).Tag);
            ProductsModel.EditProductId = ProductId;

            ProductsModel.IsEditing = true;
            ProductsModel.IsNew = false;
            ProductsModel.IsSearching = false;

            PageSwitcher.Switch("/Views/Objects/Products/ProductDetails.xaml");     
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ProductsModel.IsNew = false;
            ProductsModel.IsEditing = true;

            try
            {
                var selectedItem = this.RecentProductsGrid.SelectedItem;
                Type type = selectedItem.GetType();

                ProductsModel.EditProductId = Convert.ToInt32(type.GetProperty("ProductId").GetValue(selectedItem, null));

                PageSwitcher.Switch("/Views/Objects/Products/ProductDetails.xaml");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Select a Product");
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void cmbSearchProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


       
    }


}
