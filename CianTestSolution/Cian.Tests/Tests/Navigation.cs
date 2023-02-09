using System.Linq;
using System.Threading;
using Cian.Framework.Data.Submenu;
using Cian.Framework.PageObjects.Elements;
using Cian.Framework.Tools;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class Navigation : TestBase
    {
        private readonly Header _header;

        public Navigation()
        {
            _header = new Header(Manager);
        }

        [Test]
        [Description("Проверить, что пункт меню становится активным при переключении на него")]
        public void CheckTopMenu()
        {
            _header.ClickTopMenuItem(TopMenu.Rent);
            Assert.IsTrue(_header.IsTopMenuElementActive(TopMenu.Rent), "Menu Item is not active");
            _header.ClickTopMenuItem(TopMenu.Sale);
            Assert.IsTrue(_header.IsTopMenuElementActive(TopMenu.Sale), "Menu Item is not active");
            _header.ClickTopMenuItem(TopMenu.NewBuildings);
            Assert.IsTrue(_header.IsTopMenuElementActive(TopMenu.NewBuildings), "Menu Item is not active");
            //_header.ClickTopMenuItem(TopMenu.HouseAndGrounds);
            //Assert.IsTrue(_header.IsTopMenuElementActive(TopMenu.HouseAndGrounds), "Menu Item is not active"); Баг на сайте
            _header.ClickTopMenuItem(TopMenu.Commercial);
            Assert.IsTrue(_header.IsTopMenuElementActive(TopMenu.Commercial), "Menu Item is not active");
        }

        [Test]
        [Description("Проверить, что всплывающее меню работает и в нем присутствуют все пункты")]
        public void CheckSubmenu()
        {
            Assert.That(_header.GetSubMenuNames(TopMenu.Rent).SequenceEqual(Helpers.GetFieldsValue(typeof(Rent))));
            Assert.That(_header.GetSubMenuNames(TopMenu.Sale).SequenceEqual(Helpers.GetFieldsValue(typeof(Sale))));
            Assert.That(_header.GetSubMenuNames(TopMenu.NewBuildings).SequenceEqual(Helpers.GetFieldsValue(typeof(NewBuildings))));

            Thread.Sleep(2000);
        }
    }
}