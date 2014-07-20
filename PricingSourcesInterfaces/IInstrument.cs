using System;

namespace PricingSourcesInterfaces {
    public enum InstrumentType {
        Stock, 
    }

    public interface IInstrument {
        IPricingSource PricingSource { get; }
        InstrumentType Type { get; }
        string Name { get; }
    }

    public class Instrument : IInstrument {
        public IPricingSource PricingSource { get; private set; }
        public InstrumentType Type { get; private set; }
        public string Name { get; private set; }

        public Instrument(InstrumentType type, string name, IPricingSource pricingSource) {
            PricingSource = pricingSource;
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentNullException("name");
            }
            Name = name;
            Type = type;
        }

        public override bool Equals(object obj) {
            var asInstrument = obj as IInstrument;
            if (ReferenceEquals(null, asInstrument)) return false;
            if (ReferenceEquals(this, asInstrument)) return true;
            
            return Equals(Name, asInstrument.Name) && Equals(PricingSource, asInstrument.PricingSource);
        }

        public override int GetHashCode() {
            return (PricingSource.GetHashCode() ^ 397) & Name.GetHashCode();
        }
    }
}