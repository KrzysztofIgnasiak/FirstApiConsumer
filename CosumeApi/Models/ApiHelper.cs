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
           // string token = " LUBsmJPpp2fncO-c_K2eCEJl0VYJ7vqwGXXTO4BMlwdp-ONpFRSq7KHoZ95jlfAe_4ML_INEpGd3hMXcN0QkmcV73PGdo9UpNagFBvIxPhWLcCo61NzL_whYTRl1Dn9sml4RAtC4F1vi_14_2_dgMafKv_VtvPvQ1YReQYUBmMI_yTvvS2ATTdMoMmDD6yq4tYf55AgPdTRZYDxIgSjPfx_I2P-Cms2uO5z960jrDyty5jrNn8YwWLV3TJ7GCv_Z9JWJEZ6qAA3p0JnKvW7rMuB-sOEFy98934UiUL_WyqSsZXPTSYy5hX5aWy6EL7Kscpi6N06D4o3oC1QEy7MSkRo7VUHZE8Gbnmf_8tb3lmOwj9DbL-SsI-tP-aLrdHfF_RTly-wBadZ3uJ3_iO5WrDFngbQCgA6LAQge6DDAAwxbvgRv_d0qIzLMWCkPSSE5zpw-rPPWmCxDjJ-9XgmWf7Yye-tW1Pfwe_lbfLvxq_cT-EmzRiZ9BPsdv_XXdloetEirQ1Xqit6OpeFICPbEhg";
            string token = "coś";
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("http://localhost:61512/api/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //give me json only
            ApiClient.DefaultRequestHeaders.Authorization
                          = new AuthenticationHeaderValue("Bearer", token);




        }
    }
}