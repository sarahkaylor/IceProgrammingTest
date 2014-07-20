using System.Collections.Generic;
using PricingSourcesInterfaces;

namespace IceProgrammingTest
{
    public class AvailableSources {
        public IEnumerable<AvailableSource> GetAvailableSources() {
            yield return new AvailableSource("NYSE", new NyseSimulatedPricingSource.PricingSourceFactory());
        } 
    }

    public class AvailableSource {
        public AvailableSource(string name, IPricingSourceFactory factory) {
            Factory = factory;
            Name = name;
        }

        public string Name { get; private set; }
        public IPricingSourceFactory Factory { get; private set; }
    }
}
