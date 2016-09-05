namespace Tests.Affixx.Web.Smoke.Helpers
{
    /// <summary>
    /// Common functions for Gmail interactions.
    /// </summary>
    public static class BrowserGmailExtensions
    {
        internal static void GmailLogin(this Browser browser)
        {
            browser.NavigateTo(Settings.MailboxUrl);
            browser.TypeIn("#Email", Settings.Credentials.GmailEmail);
            browser.ClickOn("#next");
            browser.TypeIn("#Passwd", Settings.Credentials.GmailPassword);
            browser.ClickOn("#PersistentCookie");
            browser.ClickOn("#signIn");
        }
    }
}
