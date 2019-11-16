using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;


namespace Homework7.Controllers
{
    public class HomeController : Controller
    {

        public class RepoData
        {
            public string Name { get; set; }
            public string FullName { get; set; }
            public string OwnerLogin { get; set; }
            public string HtmlUrl { get; set; }
            public string OwnerAvatarUrl { get; set; }
            public string UpdatedAt { get; set; }

        }     //need name, full_name, [owner][login], html_url, [owner][avatar_url], updated_at, 

        public class CommitModel
        {
            public string Sha { get; set; }
            public string Committer { get; set; }
            public string WhenCommitted { get; set; }
            public string CommitMessage { get; set; }
            public string HtmlUrl { get; set; }


        }

        public ActionResult ApiMethod(string userName, string repoName)
        {
            //username will be mjklienst or whatev and repoName be testing123 or watev and then
            //put thatinto a string for uri to send off and then sendRequest() on that!  
            // Do what is needed to obtain a C# object containing data you wish to convert to JSON
            List<CommitModel> commits = new List<CommitModel>();
            //sha committer whencommitted commitmessage htmlurl

            //IEnumerable<CommitModel> commits = new IEnumerable<CommitModel>;

            return new ContentResult
            {
                // serialize C# object "commits" to JSON using Newtonsoft.Json.JsonConvert
                Content = JsonConvert.SerializeObject(commits),
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        private string SendRequest(string uri, string credentials, string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "token " + credentials);
            request.UserAgent = username;       // Required, see: https://developer.github.com/v3/#user-agent-required
            request.Accept = "application/json";

            string jsonString = null;
            // TODO: You should handle exceptions here
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult user()
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["KeyAPI"];
            string uri = "https://api.github.com/user";
            string credentials = apiKey;
            string username = "mjklienst";
            string json = SendRequest(uri, credentials, username);
            JObject gitStuff = JObject.Parse(json);
            List<string> output = new List<string>();
            string login = (string)gitStuff["login"];
            string avatarURL = (string)gitStuff["avatar_url"];
            string htmlURL = (string)gitStuff["html_url"];
            string fullName = (string)gitStuff["name"];
            string company = (string)gitStuff["company"];
            string location = (string)gitStuff["location"];
            string email = (string)gitStuff["email"];
            output.Add($"{login}");
            output.Add($"{email}");
            output.Add($"{company}");
            output.Add($"{location}");
            output.Add($"{htmlURL}");
            output.Add($"{fullName}");
            output.Add($"{avatarURL}");

            Debug.WriteLine($"{output}");

            string jsonString = JsonConvert.SerializeObject(output, Formatting.Indented);
            return new ContentResult {

                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        public ActionResult repositories()
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["KeyAPI"];
            string uri = "https://api.github.com/user/repos";
            string credentials = apiKey;
            string username = "mjklienst";
            string json = SendRequest(uri, credentials, username);
            JArray gitStuff = JArray.Parse(json);
            // int length = ((JArray)gitStuff["JSONObject"]).Count;
            int length = gitStuff.Count;

            // int count = (int)gitStuff["Total"];
            //{{name,fullname,html},{name,fullname,html},{name,full,html},{}}
            List<RepoData> x = new List<RepoData>();
            for (int i = 0; i < length; i++)
            {
                string name = (string)gitStuff[i]["name"];
                string fullName = (string)gitStuff[i]["full_name"];
                string ownerLogin = (string)gitStuff[i]["owner"]["login"];
                string htmlURL = (string)gitStuff[i]["html_url"];
                string ownerAvatarURL = (string)gitStuff[i]["owner"]["avatar_url"];
                string updatedAt = (string)gitStuff[i]["updated_at"];
                x.Add(new RepoData() { Name = name, FullName = fullName, OwnerLogin = ownerLogin, HtmlUrl = htmlURL, OwnerAvatarUrl = ownerAvatarURL, UpdatedAt = updatedAt });
            }

                 Debug.WriteLine($"{length},{x}");
                 Debug.WriteLine(x.Count);

            string jsonString = JsonConvert.SerializeObject(x, Formatting.Indented);
            return new ContentResult
            {

                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };


            //need name, full_name, [owner][login], html_url, [owner][avatar_url], updated_at, 
        }

    }
}