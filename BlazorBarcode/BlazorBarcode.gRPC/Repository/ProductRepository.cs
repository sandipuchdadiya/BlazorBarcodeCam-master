using BlazorBarcode.gRPC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBarcode.gRPC.Repository
{
    public class ProductRepository
    {
        public BarcodeProduct GetBarCode(string EAN)
        {
            using (var context = new MyContext())
            {
                return context.Products.FirstOrDefault(x => x.BarcodeNo == EAN);
            }
        }

        public int InsertProduct(BarcodeProduct product)
        {
            using (var context = new MyContext())
            {
                context.Products.Add(product);

                return context.SaveChanges();
            }
        }
    }
}
