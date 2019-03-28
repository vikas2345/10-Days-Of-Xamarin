using System;
using System.Collections.Generic;
using System.Text;

namespace XamCross10.Common
{
    public class Constants
    {
        string sampleReq = "https://api.foursquare.com/v2/venues/search?ll=12.971599,77.594566&" +
            "client_id=BV52JG3OG0HCL1E2GNLU3XM55BASQXNX0CY1NBREG1BTDJ2O&" +
            "client_secret=0HOZHFM3FOFGGE2SMKJ0N2LCVWEYK05TUH2IVL5YANXZYDGY&" +
            "v=20193026";

        public const string FourSquare_APIBaseUrl = "https://api.foursquare.com/v2/";
        public const string Venues = "/venues";
        public const string Search = "/search";
        public const string amPerSand = "&";
        public const string symbolEqualTo = "=";
        public const string client_id = "client_id";
        public const string client_secret = "client_secret";


        public const string FourSquare_clientID = "BV52JG3OG0HCL1E2GNLU3XM55BASQXNX0CY1NBREG1BTDJ2O" ;

        public const string FourSquare_clientSecret = "0HOZHFM3FOFGGE2SMKJ0N2LCVWEYK05TUH2IVL5YANXZYDGY";
    }
}
