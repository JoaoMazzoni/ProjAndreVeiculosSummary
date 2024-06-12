using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using ProjAndreVeiculosSummary.Controllers;
using ProjAndreVeiculosSummary.Data;

namespace ProjAndreVeiculosSummary.Test
{
    public class UnitTestAddress
    {

        private DbContextOptions<ProjAndreVeiculosSummaryContext> options;

        private void InitializeDataBase()
        {
            options = new DbContextOptionsBuilder<ProjAndreVeiculosSummaryContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var context = new ProjAndreVeiculosSummaryContext(options))
            {
                context.Address.Add(new Address { Id = 1, CEP = "14500210", Description = "Beco Diagonal" });
                context.Address.Add(new Address { Id = 2, CEP = "15504824", Description = "Rua Quadrada" });
                context.Address.Add(new Address { Id = 3, CEP = "13500998", Description = "Avenida Circular" });
                context.SaveChanges();
            }
        }

        [Fact]
        public void TestGetAll()
        {
            InitializeDataBase();

            using (var context = new ProjAndreVeiculosSummaryContext(options))
            {
                AddressesController addressesController = new AddressesController(context);
                IEnumerable<Address> addresses = addressesController.GetAddress().Result.Value;

                Assert.Equal(3, addresses.Count());
                
            }

        }

        [Fact]
        public void TestGetById()
        {
            InitializeDataBase();
            using (var context = new ProjAndreVeiculosSummaryContext(options))
            {
                AddressesController addressesController = new AddressesController(context);
                Address address = addressesController.GetAddress(2).Result.Value;
                Assert.Equal(2, address.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();
            
            using (var context = new ProjAndreVeiculosSummaryContext(options))
            {
                AddressesController controller = new AddressesController(context);
                Address address = new Address { Id = 4, CEP = "15997020", Description = "Rua da Lua" };
                Address add = controller.PostAddress(address).Result.Value;

                Assert.Equal("15997020", add.CEP);
            }
       
        
        }

    
    
    }
}