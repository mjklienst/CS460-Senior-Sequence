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
        //Class containing all repository data that we need
        public class RepoData
        {
            public string Name { get; set; }
            public string FullName { get; set; }
            public string OwnerLogin { get; set; }
            public string HtmlUrl { get; set; }
            public string OwnerAvatarUrl { get; set; }
            public string UpdatedAt { get; set; }

        }

        //Class containing all commit data that we need
        public class CommitModel
        {
            public string Sha { get; set; }
            public string Committer { get; set; }
            public string WhenCommitted { get; set; }
            public string CommitMessage { get; set; }
            public string Url { get; set; }


        }

        //Pass other functions into here to request info for user, repo, and commit data
        private string SendRequest(string uri, string credentials, string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "token " + credentials);
            request.UserAgent = username;       // Required, see: https://developer.github.com/v3/#user-agent-required
            request.Accept = "application/json";
            System.Diagnostics.Debug.WriteLine("request: " + request);
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

        //Getting all necessary commit data
        public ActionResult commits()
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["KeyAPI"];
            string newUrlName = "https://api.github.com/repos/" + Request.QueryString["user"] + "/" + Request.QueryString["repo"] + "/commits";
            string credentials = apiKey;
            string username = Request.QueryString["user"];
            string json = SendRequest(newUrlName, credentials, username);
            JArray gitStuff = JArray.Parse(json);
            int length = gitStuff.Count;
            // Do what is needed to obtain a C# object containing data you wish to convert to JSON
            List<CommitModel> commitList = new List<CommitModel>();
            //Going through data, assigning data into string variables, and adding those to new list to pass back
            for (int i = 0; i < length; i++)
            {
                string sha = (string)gitStuff[i]["sha"];
                string committer = (string)gitStuff[i]["commit"]["committer"]["name"];
                string whenCommitted = (string)gitStuff[i]["commit"]["committer"]["date"];
                string commitMessage = (string)gitStuff[i]["commit"]["message"];
                string commitUrl = (string)gitStuff[i]["html_url"];
                commitList.Add(new CommitModel() { Sha = sha, Committer = committer, WhenCommitted = whenCommitted, CommitMessage = commitMessage, Url = commitUrl });
            }

            return new ContentResult
            {
                // serialize C# object "commits" to JSON using Newtonsoft.Json.JsonConvert
                Content = JsonConvert.SerializeObject(commitList),
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        public ActionResult Index()
        {
            return View();
        }

        //Getting all necessary user data
        public ActionResult user()
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["KeyAPI"];
            string uri = "https://api.github.com/user";
            string credentials = apiKey;
            string username = "mjklienst";
            string json = SendRequest(uri, credentials, username);
            //Creating new object to obtain certain data from GitHub
            JObject gitStuff = JObject.Parse(json);
            //Creating new list so we can pass the JObject data into it, so we can return the list
            List<string> output = new List<string>();
            string login = (string)gitStuff["login"];
            string avatarURL = (string)gitStuff["avatar_url"];
            string htmlURL = (string)gitStuff["html_url"];
            string fullName = (string)gitStuff["name"];
            string company = (string)gitStuff["company"];
            string location = (string)gitStuff["location"];
            string email = (string)gitStuff["email"];
            //Adding the objects to the list
            output.Add($"{login}");
            output.Add($"{email}");
            output.Add($"{company}");
            output.Add($"{location}");
            output.Add($"{htmlURL}");
            output.Add($"{fullName}");
            output.Add($"{avatarURL}");

            //Sending data back out
            string jsonString = JsonConvert.SerializeObject(output, Formatting.Indented);
            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        //Getting all necessary repository data
        public ActionResult repositories()
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["KeyAPI"];
            string uri = "https://api.github.com/user/repos";
            string credentials = apiKey;
            string username = "mjklienst";
            string json = SendRequest(uri, credentials, username);
            //Creating JArray to obtain certain data from GitHub
            JArray gitStuff = JArray.Parse(json);
            int length = gitStuff.Count;
            //Creating new list so we can pass the JArray data into it, so we can return the list
            List<RepoData> x = new List<RepoData>();
            //Going through all data, assigning objects to strings, and adding those values to repo data list
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

            //Sending data back out
            string jsonString = JsonConvert.SerializeObject(x, Formatting.Indented);
            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

    }
}