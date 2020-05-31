using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBarcode.gRPC.Domain;
using BlazorBarcode.gRPC.Repository;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace BlazorBarcode.gRPC
{
    public class InventoryService : Inventory.InventoryBase
    {
        public override async Task<ProductResponse> ProductRPC(ProductRequest request, ServerCallContext context)
        {
            //TODO: GO repository and db Bring data
            //var product = _products.FirstOrDefault(p => p.EAN == request.EAN);
            ProductRepository productRepository = new ProductRepository();
            var existingProduct= productRepository.GetBarCode(request.EAN.ToUpper());

            return new ProductResponse { Product = new Product() { EAN = existingProduct==null? request.EAN: existingProduct.BarcodeNo, Name = existingProduct==null?"There is none": existingProduct.ProductName } };

        }

        public override async Task<InsertResponse> InsertProductRPC(InsertRequest request, ServerCallContext context)
        {
            ProductRepository productRepository = new ProductRepository();

            var ena = request.Product.EAN.Replace("*", "");

            return new InsertResponse {Result= productRepository.InsertProduct(
                        new BarcodeProduct() { BarcodeNo = ena, 
                                               ProductName = request.Product.Name }) 
                                             };

        }
    }
}
