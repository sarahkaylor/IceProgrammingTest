using System;

namespace PricingSourcesInterfaces {
    public interface IPriceAtTime {
        IInstrument Instrument { get; }
        DateTime When { get; }
        double Value { get; }
        CurrencyType Currency { get; }
    }

    public class PriceAtATime : IPriceAtTime {
        public PriceAtATime(DateTime when, CurrencyType currency, double value, IInstrument instrument) {
            Instrument = instrument;
            Value = value;
            Currency = currency;
            When = when;
        }

        public IInstrument Instrument { get; private set; }
        public DateTime When { get; private set; }
        public double Value { get; private set; }
        public CurrencyType Currency { get; private set; }
    }
}