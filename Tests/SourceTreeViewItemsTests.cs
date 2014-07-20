using System.Linq;
using System.Windows.Controls;
using IceProgrammingTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SourceTreeViewItemsTests
    {
        [TestMethod]
        public void TestTreeStructure() {
            var items = new SourceTreeViewItems(new AvailableSources(), Checked).ToArray();
            Assert.AreEqual(1, items.Length);
            Assert.AreEqual("NYSE", items[0].Header);
            var children = items[0].Items.Cast<TreeViewItem>().ToArray();
            Assert.AreEqual(2, children.Length);
        }

        [TestMethod]
        public void TestPerformCheck() {
            var items = new SourceTreeViewItems(new AvailableSources(), Checked).ToArray();
            var itemWithCheckbox = (TreeViewItem)items[0].Items[0];
            var checkbox = ((StackPanel) itemWithCheckbox.Header).Children.OfType<CheckBox>().First();
            var expectation = _checkCount + 1;
            checkbox.IsChecked = !checkbox.IsChecked;
            Assert.AreEqual(expectation, _checkCount);
        }

        private int _checkCount = 0;

        private void Checked(CheckBox obj) {
            _checkCount++;
        }
    }
}
