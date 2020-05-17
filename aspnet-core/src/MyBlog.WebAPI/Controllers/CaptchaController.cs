using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MyBlog.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CaptchaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Validate(RecaptchaRequest recaptchaRequest)
        {
            if (string.IsNullOrEmpty(recaptchaRequest?.Token)) return Ok(false);

            var secret = _configuration.GetSection("ReCaptchaSettings").GetSection("SecretToken").Value;
            if (string.IsNullOrEmpty(secret)) return Ok(false);

            var url = _configuration.GetSection("ReCaptchaSettings").GetSection("Url").Value
                                                                                      .Replace("{secret}", $"{secret}")
                                                                                      .Replace("{token}", $"{recaptchaRequest?.Token}");
            var client = new WebClient();

            var googleReply = client.DownloadString(url);

            return Ok(JsonConvert.DeserializeObject<RecaptchaResponse>(googleReply).Success);
        }
        public class RecaptchaRequest
        {
            public string Token { get; set; }
        }
        public class RecaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public IEnumerable<string> ErrorCodes { get; set; }

            [JsonProperty("challenge_ts")]
            public DateTime ChallengeTs { get; set; }

            [JsonProperty("hostname")]
            public string Hostname { get; set; }
        }
    }
}