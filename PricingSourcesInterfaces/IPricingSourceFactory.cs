using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingSourcesInterfaces
{
    public interface IPricingSourceFactory {
        IPricingSource Create();
    }
}
