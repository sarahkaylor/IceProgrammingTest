using System;
using System.Collections.Generic;
using System.Linq;
using PricingSourcesInterfaces;

namespace NyseSimulatedPricingSource {
    public class NysePricingSource : IPricingSource {
        private readonly IReadOnlyDictionary<string, IPriceAtTime[]> _prices;
        private readonly IEnumerable<Instrument> _instruments; 

        public NysePricingSource() {
            var microsoft = new Instrument(InstrumentType.Stock, "MSFT", this);
            var ford = new Instrument(InstrumentType.Stock, "F", this);
            _prices = new Dictionary<string, IPriceAtTime[]> {
                {"MSFT", new IPriceAtTime[] {
                    new PriceAtATime(DateTime.Now.AddSeconds(0), CurrencyType.Usd, 40, microsoft),
                    new PriceAtATime(DateTime.Now.AddSeconds(10), CurrencyType.Usd, 40.1, microsoft), 
                    new PriceAtATime(DateTime.Now.AddSeconds(20), CurrencyType.Usd, 39.8, microsoft), 
                    new PriceAtATime(DateTime.Now.AddSeconds(30), CurrencyType.Usd, 41.1, microsoft), 
                    new PriceAtATime(DateTime.Now.AddSeconds(40), CurrencyType.Usd, 40, microsoft), 
                    new PriceAtATime(DateTime.Now.AddSeconds(50), CurrencyType.Usd, 45.7, microsoft), 
                    new PriceAtATime(DateTime.Now.AddSeconds(60), CurrencyType.Usd, 43.3, microsoft), 
                }},
                {"F", new IPriceAtTime[] {
                    new PriceAtATime(DateTime.Now.AddSeconds(0), CurrencyType.Usd, 18, ford), 
                    new PriceAtATime(DateTime.Now.AddSeconds(10), CurrencyType.Usd, 18.7, ford),
                    new PriceAtATime(DateTime.Now.AddSeconds(20), CurrencyType.Usd, 18.8, ford),
                    new PriceAtATime(DateTime.Now.AddSeconds(30), CurrencyType.Usd, 17.9, ford),
                    new PriceAtATime(DateTime.Now.AddSeconds(40), CurrencyType.Usd, 18.2, ford),
                    new PriceAtATime(DateTime.Now.AddSeconds(50), CurrencyType.Usd, 18.8, ford),
                    new PriceAtATime(DateTime.Now.AddSeconds(60), CurrencyType.Usd, 18.5, ford),
                }}
            };
            _instruments = new[] {microsoft, ford};
        }

        public IEnumerable<IInstrument> GetAvailableInstruments() {
            return _instruments;
        }

        private static readonly IPriceAtTime[] Empty = new IPriceAtTime[0];

        public IEnumerable<IPriceAtTime> GetMostRecentPriceAsOfTime(IInstrument instrument, DateTime time) {
            IPriceAtTime[] prices;
            if (!_prices.TryGetValue(instrument.Name, out prices)) {
                prices = Empty;
            }
            var beforeGivenTime = 
                from price in prices 
                where price.When < time 
                orderby price.When descending 
                select price;
            var mostRecentPrice = beforeGivenTime.Take(1);
            return mostRecentPrice;
        }


        public override bool Equals(object obj) {
            var asNyse = obj as NysePricingSource;
            if (ReferenceEquals(null, asNyse)) {
                return false;
            }
            return true;
        }

        protected bool Equals(NysePricingSource other)
        {
            return Equals(_prices, other._prices);
        }

        public override int GetHashCode() {
            return "NYSE".GetHashCode();
        }
    }
}