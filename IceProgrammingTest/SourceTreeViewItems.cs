using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace IceProgrammingTest {
    public class SourceTreeViewItems : IEnumerable<TreeViewItem>, INotifyPropertyChanged {
        private readonly AvailableSources _availableSources;
        private readonly Action<CheckBox> _checkedFn;

        public SourceTreeViewItems(AvailableSources availableSources, Action<CheckBox> checkedFn) {
            _availableSources = availableSources;
            _checkedFn = checkedFn;
        }

        private IEnumerable<TreeViewItem> CreateSourceItems()
        {
            foreach (var source in _availableSources.GetAvailableSources())
            {
                var item = new TreeViewItem();
                item.Header = source.Name;
                item.IsExpanded = true;
                item.Tag = source;
                foreach (var instrument in source.Factory.Create().GetAvailableInstruments())
                {
                    var child = new TreeViewItem();
                    var header = new StackPanel { Orientation = Orientation.Horizontal };
                    var checkbox = new CheckBox { IsChecked = false };
                    checkbox.Tag = instrument;
                    checkbox.Checked += Checked;
                    checkbox.Unchecked += Checked;
                    var label = new Label { Content = instrument.Name };
                    header.Children.Add(checkbox);
                    header.Children.Add(label);
                    child.Header = header;
                    child.Tag = instrument;
                    item.Items.Add(child);
                }
                yield return item;
            }
        }

        private void Checked(object sender, RoutedEventArgs e) {
            _checkedFn((CheckBox) sender);
            OnPropertyChanged(new PropertyChangedEventArgs("Checked"));
        }

        public IEnumerator<TreeViewItem> GetEnumerator() {
            return CreateSourceItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}