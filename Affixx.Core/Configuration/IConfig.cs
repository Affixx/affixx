namespace Affixx.Core
{
    public interface IConfig
    {
        string ConnectionString { get; }
        string Environment { get; }

        string CloudSecretFile { get; }
        string CloudCredentialsFolder { get; }
    }
}