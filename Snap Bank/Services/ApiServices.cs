using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Snap_Bank.Services
{
    public class ConversionInfo
    {
        public string Base { get; set; }
        public string symbols { get; set; }
    }
    public class ConversionResponse
    {
        public bool Success { get; set; }
        public Rates rates { get; set; }
    }
    public class Rates
    {
        public string USD { get; set; }
        public string EUR { get; set; }
        public string GBP { get; set; }
    }


    public class ApiServices
    {
        public async Task<decimal> GetConversionRate(String country)
        {
            var url = "http://data.fixer.io/api/latest?access_key=ba0c9e66196edcb51488611d288eb7d3";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            country = country.Replace('"', ' ').Trim();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            String body = JsonConvert.SerializeObject(new ConversionInfo()
            {
                Base = "EUR"
            });
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
            };
            HttpResponseMessage response = await client.SendAsync(request);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                String jsonResult = await response.Content.ReadAsStringAsync();
                ConversionResponse conversionresponse = JsonConvert.DeserializeObject<ConversionResponse>(jsonResult);
                if (country == "USD")
                    return decimal.Parse(conversionresponse.rates.USD);
                else if (country=="GBP")
                    return decimal.Parse(conversionresponse.rates.GBP);
                else if (country=="EUR")
                    return decimal.Parse(conversionresponse.rates.EUR);
            }
            return 0;
        }
    }
}