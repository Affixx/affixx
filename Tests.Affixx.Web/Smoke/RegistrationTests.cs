using NUnit.Framework;
using Tests.Affixx.Web.Smoke.Helpers;

namespace Tests.Affixx.Web.Smoke
{
    [TestFixture]
    [Category("Web")]
    public class RegistrationTests
    {
        private static Browser browser = null;

        #region setup

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            browser = new Browser();
            browser.NavigateTo(Settings.Url);
        }

        [OneTimeTearDown]
        public void FixtureTeardown()
        {
            browser.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            browser.SetTestTitle(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void Teardown()
        {
            browser.DeleteAccount();
        }

        #endregion

        [Test]
        public void EmailRegistration()
        {
            browser.RegisterViaEmail();
            Assert.That(browser.GetElement("div.alert-success"), Is.Not.Null, "Should see a success confirmation message");
        }

        [Test]
        public void EmailRegistrationWithConfirmation()
        {
            browser.RegisterViaEmail();
            ConfirmEmail();

            var successDiv = browser.GetElement("div.alert-success");
            Assert.That(successDiv, Is.Not.Null, "Should see a success confirmation message");
            Assert.That(successDiv.Text, Does.Contain("Thank you for confirming the email"), "Should see a success confirmation message");
        }

        [Test]
        public void EmailRegistration_UploadPageNotAvailable()
        {
            browser.RegisterViaEmail();

            browser.ClickOn("a[href='/document/upload']");
            Assert.That(browser.Url, Does.Not.Contain("/document/upload"), "The page not available without confirmation");
        }

        [Test]
        public void EmailRegistrationWithConfirmation_UploadPageAvailable()
        {
            browser.RegisterViaEmail();
            ConfirmEmail();

            browser.ClickOn("a[href='/document/upload']");
            Assert.That(browser.Url, Does.Contain("/document/upload"), "The page not available without confirmation");
        }

        [Test]
        public void EmailRegistration_ResendConfirmationEmail()
        {
            browser.RegisterViaEmail();

            //delete the confirmation email
            GmailOpenMailbox();
            GmailOpenEmail();
            GmailDeleteEmail();
            browser.ClickOn("a[href*='#starred']");  //to avoid "you have usaved changes" alert
            browser.ClickOn("a[href*='#inbox']");

            //resend the confirmation email and confirm it
            browser.NavigateTo(Settings.Url);
            //var alreadyLoggedIn = browser.GetElement("a[href*='login']") == null;
            //if (!alreadyLoggedIn) {
            //    browser.LoginViaEmail(Settings.Credentials.Password);
            //}
            browser.ClickOn("a[href='/document/upload']");
            browser.ClickOn("a[href*='/resendconfirmationemail']");

            ConfirmEmail();

            var successDiv = browser.GetElement("div.alert-success");
            Assert.That(successDiv, Is.Not.Null, "Should see a success confirmation message");
            Assert.That(successDiv.Text, Does.Contain("Thank you for confirming the email"), "Should see a success confirmation message");
        }

        [Test]
        public void FacebookRegistration()
        {
            browser.RegisterViaFacebook();
            Assert.That(browser.GetElement("div.alert-success"), Is.Not.Null, "Should see a success confirmation message");
        }

        [Test]
        public void ChangePassword()
        {
            browser.RegisterViaEmail();

            browser.ClickOn("a[href*='my/account']");
            browser.TypeIn("#OldPassword", Settings.Credentials.Password);
            browser.TypeIn("#NewPassword", Settings.Credentials.PasswordNew);
            browser.TypeIn("#NewPasswordConfirm", Settings.Credentials.PasswordNew);
            browser.ClickOn("input[value='Change']");
            Assert.That(browser.GetElement("div.alert-success"), Is.Not.Null, "Should see a success confirmation dialog");

            browser.ClickOn("a.dropdown-toggle");
            browser.ClickOn("a#logout");
            browser.LoginViaEmail(Settings.Credentials.PasswordNew);
            Assert.That(browser.GetElement("a[href*='login']"), Is.Null, "Should not see login link");
        }

        private void ConfirmEmail()
        {
            GmailOpenMailbox();

            //confirm email address by clicking on the link
            GmailOpenEmail();
            browser.GetElementByXpath("//a[contains(.,'here')]").Click();

            //delete the email so that the test is rerunnable
            GmailDeleteEmail();
        }

        private void GmailOpenMailbox()
        {
            browser.Sleep(60, "wait for the email to arrive");
            browser.NavigateTo(Settings.MailboxUrl);

            var alreadyLoggedIn = browser.GetElement("#Email") == null;
            if (alreadyLoggedIn) {
                return;
            }
            browser.TypeIn("#Email", Settings.Credentials.GmailEmail);
            browser.ClickOn("#next");
            browser.TypeIn("#Passwd", Settings.Credentials.GmailPassword);
            browser.ClickOn("#PersistentCookie");
            browser.ClickOn("#signIn");
        }

        private void GmailOpenEmail()
        {
            browser.GetElementByXpath("//span[contains(.,'confirm your email')]").Click();
        }


        private void GmailDeleteEmail()
        {
            browser.SwitchTab(Settings.MailboxUrl);

            browser.ClickOn("img[role='menu']");
            browser.GetElementByXpath("//div[text()='Delete this message']").Click();

            browser.SwitchTab(Settings.Url);
        }
    }
}
