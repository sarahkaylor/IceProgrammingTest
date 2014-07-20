using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using PricingSourcesInterfaces;

namespace IceProgrammingTest
{
    internal class MainWindowViewModel {
        private readonly IReadOnlyDictionary<IPricingSource, List<IInstrument>> _selectedSources;
        private readonly SourceTreeViewItems _sourceItems;

        public MainWindowViewModel(AvailableSources availableSources) {
            var selectedSources = new Dictionary<IPricingSource, List<IInstrument>>();
            foreach (var source in availableSources.GetAvailableSources()) {
                var pricingSource = source.Factory.Create();
                selectedSources.Add(pricingSource, new List<IInstrument>());
            }
            _selectedSources = selectedSources;
            _sourceItems = new SourceTreeViewItems(availableSources, InstrumentChecked);
        }

        public SourceTreeViewItems SourceItems {
            get { return _sourceItems; }
            set { /*Required by WPF for a two-way binding*/ }
        }

        public IEnumerable<IPriceAtTime> Prices {
            get {
                var now = DateTime.Now;
                var prices =
                    from sourceAndInstruments in _selectedSources
                    let source = sourceAndInstruments.Key
                    let instruments = sourceAndInstruments.Value
                    from instrument in instruments
                    let instrumentPrices = source.GetMostRecentPriceAsOfTime(instrument, now)
                    from price in instrumentPrices
                    select price;
                return prices;
            }
        }

        private void InstrumentChecked(CheckBox checkbox) {
            var instrument = (IInstrument) checkbox.Tag;
            if (checkbox.IsChecked.HasValue && checkbox.IsChecked.Value) {
                var source = instrument.PricingSource;
                _selectedSources[source].Add(instrument);
            } else {
                foreach (var instruments in _selectedSources.Values) {
                    instruments.Remove(instrument);
                }
            }
        } 
    }
}
