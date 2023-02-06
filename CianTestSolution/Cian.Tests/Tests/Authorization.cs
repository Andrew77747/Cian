using System.Threading;
using Cian.Framework.PageObjects.Elements;
using NUnit.Framework;

namespace Cian.Tests.Tests
{
    public class Authorization : TestBase
    {
        private readonly Header _header;
        private readonly LoginModal _loginModal;

        public Authorization()
        {
            _header = new Header(Manager);
            _loginModal = new LoginModal(Manager, Settings);
        }

        [Test]
        [Description("Проверить, что вход в систему успешно выполняется")]
        public void CheckLogin()
        {
            _header.ClickLoginBtn();
            _loginModal.Login();
            Assert.That(_header.GetUserId().Equals(Settings.UserID));
        }
    }
}