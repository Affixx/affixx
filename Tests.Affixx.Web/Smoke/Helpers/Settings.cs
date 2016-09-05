using System;

namespace Tests.Affixx.Web.Smoke.Helpers
{
    public static class Settings
    {
        public static string Url => "http://beta.affixx.co";
        //public static string Url => "http://localhost:51514/";
        public static string MailboxUrl => "https://mail.google.com";

        public static class Credentials
        {
            public static string Username => "Smoke Test User";
            public static string Password => "smoke password";
            public static string PasswordNew => "smoke password new";

            public static string GmailEmail => EnvVar("AFFIXX_SMOKE_TEST_GMAIL_EMAIL");
            public static string GmailPassword => EnvVar("AFFIXX_SMOKE_TEST_GMAIL_PASSWORD");

            public static string FacebookEmail => EnvVar("AFFIXX_SMOKE_TEST_FACEBOOK_EMAIL");
            public static string FacebookPassword => EnvVar("AFFIXX_SMOKE_TEST_FACEBOOK_PASSWORD");

            public static string AdminEmail => EnvVar("AFFIXX_SMOKE_TEST_ADMIN_EMAIL");
            public static string AdminPassword => EnvVar("AFFIXX_SMOKE_TEST_ADMIN_PASSWORD");
        }

        public static class Upload
        {
            public static string Filename => "example.pdf";
            public static string Title => "Smoke test title";
        }

        private static string EnvVar(string key)
        {
            var result = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);
            if (result == null) {
                throw new Exception($"Environment variable {key} not defined");
            }

            return result;
        }
    }
}
