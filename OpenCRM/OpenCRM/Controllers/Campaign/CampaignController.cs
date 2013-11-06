using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Controllers.Campaign
{
    class CampaignController
    {
        public static int CurrentCampaignId;

        public static int getCurrentCampaignID()
        {
            return CurrentCampaignId; 
        }
    }
}
