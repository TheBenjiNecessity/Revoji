namespace RevojiWebApi.DBTables
{
    public class AppUserSettings : JSONWrapper
    {
        public AppUserSettings(string JSON) : base(JSON) { }

        public string test
        {
            get { return GetString("test"); }
            set { SetString("test", value); }
        }
    }
}
