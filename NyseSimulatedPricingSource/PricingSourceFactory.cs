using PricingSourcesInterfaces;

namespace NyseSimulatedPricingSource
{
    public class PricingSourceFactory : IPricingSourceFactory{
        public IPricingSource Create() {
            return new NysePricingSource();
        }
    }
}
