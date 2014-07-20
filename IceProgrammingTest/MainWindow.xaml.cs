using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace IceProgrammingTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new MainWindowViewModel(new AvailableSources());
            DataContext = viewModel;
            var context = new DispatcherSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);
            viewModel.SourceItems.PropertyChanged += SourceItems_PropertyChanged;
            ThreadPool.QueueUserWorkItem(TriggerUpdateAfterAWhile, context);
        }

        private void TriggerUpdateAfterAWhile(object state) {
            var context = (SynchronizationContext) state;
            Thread.Sleep(5000);
            context.Send(x => TriggerGridToUpdate(), null);
            ThreadPool.QueueUserWorkItem(TriggerUpdateAfterAWhile, context);
        }

        void SourceItems_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            TriggerGridToUpdate();
        }

        private void TriggerGridToUpdate() {
            var binding = _valuesGrid.GetBindingExpression(DataGrid.ItemsSourceProperty);
            if (binding != null) {
                binding.UpdateTarget();
            }
        }
    }
}
