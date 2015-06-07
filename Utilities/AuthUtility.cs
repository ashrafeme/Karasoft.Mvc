using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Karasoft.Mvc.Utilities.Auth
{
    public static class GoogleOAuth2
    {
        public static string ClientID { get; set; }
        public static string ClientSecret { get; set; }
        public static string RedirectURIs { get; set; }
        public static string GetGoogleAuthURL(string clientId, string state, string redirecturi)
        {
            string requrl = string.Format("https://accounts.google.com/o/oauth2/auth?response_type=code&scope={0}&redirect_uri={1}&client_id={2}&state={3}",
                "email%20profile",
                redirecturi,
                clientId,
                state
                );

            return requrl;
        }



        //Exchange authorization code for tokens
        public static GoogleResponse ExchangeAuthorizationCodeForTokens(String state, String code, string clientID, string clientSecret, string redirectURIs)
        {
            GoogleResponse responseFromServer = new GoogleResponse();
            responseFromServer.error = string.Empty;
            responseFromServer.haserror = false;
            HttpWebRequest requestToken = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            requestToken.Method = "POST";
            String postData = String.Format(
                "code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code",
                code, clientID, clientSecret, redirectURIs
            );
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            requestToken.ContentType = "application/x-www-form-urlencoded";
            requestToken.ContentLength = byteArray.Length;
            using (Stream dataStream = requestToken.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = requestToken.GetResponse();
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseFromServer = Deserialise<GoogleResponse>(reader.ReadToEnd());
                        reader.Close();
                        dataStream.Close();
                        response.Close();
                    }
                }
                else
                {
                    dataStream.Close();
                    response.Close();
                    responseFromServer.error = ((HttpWebResponse)response).StatusDescription;
                    responseFromServer.haserror = true;
                  //  return View("Index");
                }
            }

            return responseFromServer;
        }

        public static GoogleId RequestUserInfo(string accesstoken)
        {
            string url = String.Format("https://www.googleapis.com/oauth2/v1/userinfo?access_token={0}", accesstoken);
            GoogleId gId = new GoogleId();
            
            HttpWebRequest requestUserInfo = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse responseUserInfo = (HttpWebResponse)requestUserInfo.GetResponse();
            if (((HttpWebResponse)responseUserInfo).StatusCode == HttpStatusCode.OK)
            {
                using (Stream receiveStream = responseUserInfo.GetResponseStream())
                {
                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    using (StreamReader readStream = new StreamReader(receiveStream, encode))
                    {
                        gId = Deserialise<GoogleId>(readStream.ReadToEnd());
                        responseUserInfo.Close();
                        readStream.Close();
                        
                    }
                }
            }

            return gId;

        }


        public static T Deserialise<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serialiser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                return (T)serialiser.ReadObject(ms);
            }
        }
    }


    public class GoogleResponse
    {
        public String access_token { get; set; }
        public String refresh_token { get; set; }
        public String expires_in { get; set; }
        public String token_type { get; set; }
        public String error { get; set; }
        public bool haserror { get; set; }
    }

    public class GoogleId
    {
        public String id { get; set; }
        public String email { get; set; }
        public Boolean verified_email { get; set; }
        public String name { get; set; }
        public String given_name { get; set; }
        public String family_name { get; set; }
        public String link { get; set; }
        public String picture { get; set; }
        public String gender { get; set; }
        public String locale { get; set; }
    }
}
