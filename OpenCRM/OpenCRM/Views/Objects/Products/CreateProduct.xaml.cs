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
            var codigo = Convert.ToInt32(TxtBoxCodigo.Text);
            var precio = Convert.ToDecimal(TxtBoxPrecio.Text);
            var descripcion = TxtBoxDescripcion.Text.ToString();

            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var product = _db.Products.Create();
                    product.Name = name;
                    product.ProductId = codigo;
                    product.Price = precio;
                    product.Description = descripcion;
                    product.CreateBy = Session.UserId;
                    product.UpdateBy = Session.UserId;
                    product.CreateDate = DateTime.Now;
                    product.UpdateDate = DateTime.Now;

                    if (CheckActive.IsEnabled)
                        product.Active = true;
                    else
                        product.Active = false;

                    _db.Products.Add(product);
                    _db.SaveChanges();

                    MessageBox.Show("Producto ingresado con Exito");
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
