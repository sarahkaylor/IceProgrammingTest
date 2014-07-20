using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NyseSimulatedPricingSource;

namespace Tests
{
    [TestClass]
    public class NysePricingSourceFactoryTests
    {
        [TestMethod]
        public void TestCreate() {
            var factory = new PricingSourceFactory();
            var source = factory.Create();
            Assert.IsNotNull(source);
        }
    }
}
