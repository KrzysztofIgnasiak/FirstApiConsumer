using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace CosumeApi.Models
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            string token = " 1MeXz0XxW2Cro_C8fqaoOcZ72SAElluEzjDQC6BjqSf9Uz0iv36ix6evs-6P_g_bMdMP5qvl2MDppmX-VGKcTfgAK9pT7MPuiMwka9ScKLoMmVyjOxY6pK0ozVyXDG2WeZ9N0s5MAtNqK8NdH9nVmFokeT6naLLdK2oYSY5AeFo4FGL_qa79Jgpg6TNZfKZ5NDyqO17WplG4kSG69xK5jl01AU51RAlHj1AMCVwQ2O3sv5HL1TWqpVhJI3EUThqv0R55nMPpwimRWdXoqITmbP6PtlfSgpF5sLlBsi7KcjEKzpmRK0REdiiqaqwZIgst3AyovfVl6IEk9nBmQvuJ4tVGDA7bRhR5qnGJ6AwlUu5ijR9965GNfgUHJyPULwCVKJwPJGT3tZ1Q5BcRV2Y0Jtii5fpAO2sYYo1J1mYI6NxWKAwD1tNFzmbcDOxtnSURVVMkdBNRS3RYLiq0edDvvfEf6pVUmmKwxTwauHtB9mFWknKFzQGz_GnNTmWu9Hp2P9EKKA2dKIUXq6VBWN5tWQ";
            //string token = "coś";
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("http://localhost:61512/api/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //give me json only
            ApiClient.DefaultRequestHeaders.Authorization
                          = new AuthenticationHeaderValue("Bearer", token);




        }
    }
}