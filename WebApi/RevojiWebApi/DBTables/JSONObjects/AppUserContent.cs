namespace RevojiWebApi.DBTables
{
    /*
     * JSON is of the form:
     * {
     *     "Avatar": "url",
     * 
     * }
     */

    public class AppUserContent : JSONWrapper
    {
        //what would be in content?
        public AppUserContent(string JSON) : base (JSON) {}

        public string Avatar
        {
            get { return GetString("Avatar"); }
            set { SetString("Avatar", value); }
        }
    }
}
