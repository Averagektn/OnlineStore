// dotnet ef dbcontext scaffold "Host=localhost;Database=clothes_store;Username=postgres;Password=password;" Npgsql.EntityFrameworkCore.PostgreSQL
// dotnet ef migrations add InitialCreate
// dotnet ef database update

using CodeFirst;
using CodeFirst.Model;

var q = new Query(new ClothesStoreContext());

//q.Get_ProductVariants();
//q.Get_Reviews();
//q.Get_Products();
q.Get_Brands();
//q.Get_CompletedOrders();
