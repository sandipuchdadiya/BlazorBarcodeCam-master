using BlazorBarcode.gRPC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBarcode.Client.State
{
	public class ProductsState
	{
		private readonly Inventory.InventoryClient _inventoryClient;
		private readonly List<Product> _rows = new List<Product>();

		public ProductsState(Inventory.InventoryClient inventoryClient)
		{
			_inventoryClient = inventoryClient;
		}

		public IReadOnlyList<Product> Rows => _rows;


		public async Task AddItemAsync(string ean)
		{
			var row = await FetchProduct(ean);
			if (row.Name != null)
			{
				_rows.Add(row);
			}


		}

		private async Task<Product> FetchProduct(string ean)
		{
			var request = new ProductRequest { EAN = ean };
			var response = await _inventoryClient.ProductRPCAsync(request);
			return response.Product;
		}

		public async Task InsertProductAsync(string ean, string name, string description)
		{
			Product product = new Product();
			product.EAN = ean;
			product.Name = name;
			product.Description = description;

			var request = new InsertRequest { Product = product };
			await _inventoryClient.InsertProductRPCAsync(request);

		}
	}
}
