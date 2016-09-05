using NUnit.Framework;
using Tests.Affixx.Web.Smoke.Helpers;

namespace Tests.Affixx.Web.Smoke
{
    [TestFixture]
    [Category("Web")]
    public class UploadFileTests
    {
        private static Browser browser = null;

        #region setup

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            browser = new Browser();
            browser.NavigateTo(Settings.Url);
            browser.RegisterViaFacebook();
        }

        [OneTimeTearDown]
        public void FixtureTeardown()
        {
            browser.DeleteAccount();
            browser.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            browser.SetTestTitle(TestContext.CurrentContext.Test.Name);
        }

        #endregion

        [Test]
        public void UploadFileViaBrowse()
        {
            browser.UploadFile();

            Assert.That(browser.GetElement("div.alert-success"), Is.Not.Null, "Should see a success confirmation message");
            DeleteUploadedFiles();
        }

        [Test]
        public void UploadFileTwice()
        {
            browser.UploadFile();
            browser.UploadFile();

            Assert.That(browser.GetElement("div.alert-warning"), Is.Not.Null, "Should see a warning message");
            DeleteUploadedFiles();
        }

        [Test]
        public void FileSearch()
        {
            browser.UploadFile();

            var title = Settings.Upload.Title;
            Search(title);
            var document = browser.GetElement("article.search.well");
            Assert.That(document.Text, Does.Contain(title), "Uploaded documents should appear in search results");

            DeleteUploadedFiles();
        }

        [Test]
        public void FileSearchAfterDelete()
        {
            browser.UploadFile();

            DeleteUploadedFiles();
            Search(Settings.Upload.Title);

            var notFound = browser.GetElement("div.alert-danger");
            Assert.That(notFound.Text, Is.EqualTo("Oops:\r\nOops! No documents found. Try a different query!"), "Deleted documents should not appear in search results");
        }

        private void Search(string text)
        {
            browser.ClickOn("a[href='/']");
            browser.TypeIn("#q", text);
            browser.ClickOn("button[type='submit']");
        }

        private void DeleteUploadedFiles()
        {
            browser.ClickOn("a.dropdown-toggle");
            browser.ClickOn("a[href='/my/uploads']");
            browser.ClickOn("a[data-target='#confirm-delete']");

            browser.Sleep(1);
            browser.ClickOn("div.modal-dialog .btn-danger");
        }
    }
}
