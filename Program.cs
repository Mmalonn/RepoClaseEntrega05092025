using Repository2025.Domain;
using Repository2025.Services;

ProductService oService = new ProductService();

List<Product> lp = oService.GetProducts();

if (lp.Count > 0)
    foreach (Product p in lp)
        Console.WriteLine(p);
else
    Console.WriteLine("No hay productos...");


Product prod = oService.GetProduct(2);
if (prod != null)
{
    Console.WriteLine(prod);
}
else
{
    Console.WriteLine("no quiso");
}


Console.WriteLine(oService.DeleteProduct(2));