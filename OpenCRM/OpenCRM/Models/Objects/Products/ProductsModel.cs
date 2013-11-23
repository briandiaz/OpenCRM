using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OpenCRM.DataBase;
using OpenCRM.Models.Settings;

namespace OpenCRM.Models.Objects.Products
{
    class ProductsModel
    {
        #region Methods

        public void LoadRecentProduts(DataGrid RecentProductsGrid)
        {
            var listProducts = new List<DataGridRecentProducts>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = 
                    (
                        from products in _db.Products
                        select
                         new DataGridRecentProducts()
                         {
                             Nombre = products.Name,
                             Codigo = products.Code,
                             Description = products.Description

                         }
                            
                    ).ToList();
                  

                    listProducts = query;
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            listProducts.ForEach(x => x.Nombre = x.Nombre.PadRight(100));
            RecentProductsGrid.ItemsSource = listProducts;

        }
        #endregion
    }

    class DataGridRecentProducts
    {
        #region Properties
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Description { get; set; }
        #endregion
    }

        
}
    

