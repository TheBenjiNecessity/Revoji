using System;
using Newtonsoft.Json.Linq;

namespace RevojiWebApi.DBTables
{
    public class JSONWrapper
    {
        /*
         * The json fed into this object should always be a single column
         * No nested objects
         */
        private JObject JSONObject;

        public JSONWrapper(string JSON)
        {
            JSONObject = JObject.Parse(JSON);
        }

        protected string GetString(string name) {
            if (JSONObject[name] != null) {
                return JSONObject[name].ToString();
            }

            return null;
        }

        protected void SetString(string name, string value)
        {
            JSONObject[name] = value;
        }

        protected JObject GetObject(string name) {
            if (JSONObject[name] != null)
            {
                return (JObject)JSONObject[name];
            }

            return null;
        }

        protected void SetObject(string name, JObject value)
        {
            JSONObject[name] = value;
        }
    }
}
