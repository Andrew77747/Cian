using System.Threading;
using Cian.Framework.PageObjects.Elements;
using Cian.Framework.PageObjects.Pages;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class Authorization : TestBase
    {
        private readonly Header _header;
        private readonly LoginModal _loginModal;
        private readonly ContactPage _contactPage;

        public Authorization()
        {
            _header = new Header(Manager);
            _loginModal = new LoginModal(Manager, Settings);
            _contactPage = new ContactPage(Manager);
        }

        [Test]
        [Description("Проверить, что вход в систему успешно выполняется")]
        public void CheckLogin()
        {
            _header.ClickLoginBtn();
            _loginModal.Login();
            Assert.That(_header.GetUserId().Equals(Settings.UserID));
        }

        [Test]
        [Description("Проверить, что вход в систему успешно выполняется")]
        public void CheckHelpLink()
        {
            _header.ClickLoginBtn();
            _loginModal.ClickHelpLink();
            _contactPage.SwitchToContactWindow();
            Assert.IsTrue(_contactPage.IsContactPageOpened(), "Contact page is not opened");
            _contactPage.CloseAndSwitchPage();
        }
    }
}