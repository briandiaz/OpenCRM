using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Controllers.Campaign
{
    class CampaignController
    {
        public static int CurrentCampaignId { get; set; }

        public static int previousCampaignId { get; set; }
        
        public static int nextCampaignId { get; set; }

        public static int currentCampaignIndex { get; set; }

        public static int previousCampaignIndex { get; set; }

        public static int nextCampaignIndex { get; set; }


    }
}
