using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NyseSimulatedPricingSource;
using PricingSourcesInterfaces;

namespace Tests
{
    [TestClass]
    public class NysePricingSourceTests
    {
        [TestMethod]
        public void TestGetInstruments() {
            var source = new NysePricingSource();
            var instruments = source.GetAvailableInstruments().ToArray();
            Assert.AreEqual(2, instruments.Length);
        }

        [TestMethod]
        public void TestGetMostRecent() {
            var source = new NysePricingSource();
            var instrument = source.GetAvailableInstruments().First();
            var price = source.GetMostRecentPriceAsOfTime(instrument, DateTime.Now).First();
            Assert.AreEqual("MSFT", price.Instrument.Name);
            Assert.AreEqual(CurrencyType.Usd, price.Currency);
        }

        [TestMethod]
        public void TestEquality() {
            var src1 = new NysePricingSource();
            var src2 = new NysePricingSource();
            Assert.AreEqual(true, Equals(src1, src2));
        }
    }
}
