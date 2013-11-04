using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using OpenCRM.DataBase;
using System.Data.SqlClient;

namespace OpenCRM.Models.Objects.Leads
{
    public class LeadsModel
    {
        public static void SaveLead(DataBase.Leads leadData)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var lead = _db.Leads.Create();
                    lead = leadData;
                    _db.Leads.Add(lead);
                    _db.SaveChanges();
                    MessageBox.Show("Lead created.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
