using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingSourcesInterfaces
{
    public interface IPricingSource {
        IEnumerable<IInstrument> GetAvailableInstruments();
        IEnumerable<IPriceAtTime> GetMostRecentPriceAsOfTime(IInstrument instrument, DateTime time);
    }
}
