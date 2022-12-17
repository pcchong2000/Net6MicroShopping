using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Resource;

namespace Shopping.UI.MemberApp.Commons
{
    public static class UrlHelper
    {
        public static Dictionary<string, string> UrlToDic(string Raw)
        {
            Dictionary<string, string> resp = new Dictionary<string, string>();
            string[] fragments;

            // query string encoded
            if (Raw.Contains("?"))
            {
                fragments = Raw.Split('?');

                var additionalHashFragment = fragments[1].IndexOf('#');
                if (additionalHashFragment >= 0)
                {
                    fragments[1] = fragments[1].Substring(0, additionalHashFragment);
                }
            }
            // fragment encoded
            else if (Raw.Contains("#"))
            {
                fragments = Raw.Split('#');
            }
            // form encoded
            else
            {
                fragments = new[] { "", Raw };
            }

            var qparams = fragments[1].Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var param in qparams)
            {
                var parts = param.Split('=');

                if (parts.Length == 2)
                {
                    resp.Add(parts[0], parts[1]);
                }
                else
                {
                    throw new InvalidOperationException("Malformed callback URL.");
                }
            }

            return resp;
        }
        public static string UrlGetParam(string url,string key)
        {
            try
            {
                return UrlToDic(url)[key];
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
