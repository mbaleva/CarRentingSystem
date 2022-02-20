namespace CarRentingSystem.Common.Settings
{
    public class MessageBrokerSettings
    {
        public MessageBrokerSettings()
        {
        }
        public MessageBrokerSettings(string host, string username, string password)
        {
            this.Host = host;
            this.Username = username;
            this.Password = password;
        }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
