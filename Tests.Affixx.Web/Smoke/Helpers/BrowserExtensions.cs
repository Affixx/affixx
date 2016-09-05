namespace Tests.Affixx.Web.Smoke.Helpers
{
    /// <summary>
    /// Common functions which are used across multiple tests.
    /// </summary>
    public static class BrowserExtensions
    {
        internal static void RegisterViaEmail(this Browser browser)
        {
            browser.ClickOn("a[href*='register']");
            browser.TypeIn("#UserName", Settings.Credentials.Username);
            browser.TypeIn("#Email", Settings.Credentials.GmailEmail);
            browser.TypeIn("#Password", Settings.Credentials.Password);
            browser.ClickOn("#AgreeToTerms");
            browser.SelectDropdown("#UniversityId", 1);
            browser.ClickOn("input#register");
        }

        internal static void LoginViaEmail(this Browser browser, string password)
        {
            browser.ClickOn("a[href*='login']");
            browser.TypeIn("#Email", Settings.Credentials.GmailEmail);
            browser.TypeIn("#Password", password);
            browser.ClickOn("input#login");
        }

        internal static void RegisterViaFacebook(this Browser browser)
        {
            browser.ClickOn("a[href*='register']");
            browser.ClickOn("input#register-facebook");

            if (browser.Url.Contains("facebook.com")) {
                //need to login to facebook first, then will be redirected back to the registration page
                browser.TypeIn("#email", Settings.Credentials.FacebookEmail);
                browser.TypeIn("#pass", Settings.Credentials.FacebookPassword);
                browser.ClickOn("#loginbutton");
            }

            browser.ClickOn("#AgreeToTerms", scroll: true);
            browser.ClickOn("input#register", scroll: true);
        }

        internal static void UploadFile(this Browser browser)
        {
            browser.ClickOn("a[href*='upload']");
            browser.ClickOn("#drop-zone");
            browser.SelectFile(Settings.Upload.Filename);

            browser.TypeIn("#Title", Settings.Upload.Title);
            browser.TypeIn("#Description", "Lorem ipsum description - just some random text");
            browser.TypeIn("#Degree", "Some random degree");
            browser.TypeIn("#CourseName", "Some course name");
            browser.TypeIn("#CourseCode", "SCC1234");
            browser.SelectDropdown("#UniversityId", 0);
            browser.ClickOn("input[value='Upload']");
        }

        internal static void DeleteAccount(this Browser browser)
        {
            browser.ClickOn("a[href*='my/account']");
            browser.ClickOn("a[data-target='#confirm-delete']", scroll: true); //for some reason, scrollTo doesn't work?

            browser.Sleep(1);
            browser.ClickOn("div.modal-dialog .btn-danger");
        }  
    }
}
