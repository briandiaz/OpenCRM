using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using OpenCRM.Controllers.Session;

namespace OpenCRM.Views.Objects.Products
{
    /// <summary>
    /// Interaction logic for CreateProduct.xaml
    /// </summary>
    public partial class CreateProduct : Page
    {
        public CreateProduct()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var name = TxtBoxName.Text.ToString();
            var code = TxtBoxCodigo.Text.ToString();
            var precio = Convert.ToDecimal(TxtBoxPrecio.Text);
            var descripcion = TxtBoxDescripcion.Text.ToString();
            var quantity = Convert.ToInt32(TxtBoxQuantity.Text); 

            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var product = _db.Products.Create();

                    product.Name = name;
                    product.Code = code;
                    product.Price = precio;
                    product.Description = descripcion;
                    product.Quantity = quantity;
                    product.CreateBy = Session.UserId;
                    product.UpdateBy = Session.UserId;
                    product.CreateDate = DateTime.Now;
                    product.UpdateDate = DateTime.Now;

                    if (cbxCampaignActive.IsEnabled)
                        product.Active = true;
                    else
                        product.Active = false;

                    _db.Products.Add(product);
                    _db.SaveChanges();

                    TxtBoxCodigo.Text = "";
                    TxtBoxDescripcion.Text = "";
                    TxtBoxName.Text = "";
                    TxtBoxPrecio.Text = "";
                    TxtBoxQuantity.Text = "";

                    MessageBox.Show("Producto ingresado con Exito");
                    PageSwitcher.Switch("/Views/Objects/Products/ProductsView.xaml");
                }


            }

            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Products/ProductsView.xaml");
        }
    }

}
