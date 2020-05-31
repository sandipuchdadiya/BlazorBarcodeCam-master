using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBarcode.gRPC.Domain
{
    public class BarcodeProduct
    {
        public string BarcodeNo { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }
    }
}
