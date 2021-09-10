namespace Stoicode.UniLib.Configuration
{
    public class AppConfig
    {
        public string Company { get; protected set; }
        public string Name { get; protected set; }
        public string Version { get; protected set; }


        public AppConfig()
        {
            Company = "Company";
            Name = "Project";
            Version = "1.0";
        }

        public AppConfig(string company, string name, string version)
        {
            Company = company;
            Name = name;
            Version = version;
        }
    }
}