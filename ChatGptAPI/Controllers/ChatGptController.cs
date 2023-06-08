using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly chatgpt chatgpt;
        private  ChatGptResponse result;

        public ChatGptController()
        {
            chatgpt = new chatgpt();
            result=new ChatGptResponse();
        }

        [HttpGet]
        public async Task<ChatGptResponse> GetUserMessage([FromQuery] string message)
        {
            int registrationNumber;
            var msg = new List<string>();
            {
                // check if user enter the registration number 
                if (int.TryParse(message, out registrationNumber))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string baseUrl = $"https://localhost:44307/api/Student/getById?registrationNumber={message}";
                        string parameter1 = message;

                        HttpResponseMessage response = await client.GetAsync(baseUrl);
                        response.EnsureSuccessStatusCode();

                        string Grade = await response.Content.ReadAsStringAsync();
                        result.IsSucess= true;
                        result.Message = $"Grade : {Grade}";                     
                        return result;
                    }
                }
                else
                {
                     result = await chatgpt.SendMessageToChatGPT(message);
                }
                return result;
            }
        }


        [HttpPost("upload")]
        public async Task<ChatGptResponse> UploadFile(IFormFile file)
        {

                if (file == null || file.Length == 0)
                {
                    result.IsSucess = false;
                    result.Message = "Error Occur while upload file";
                    return result;
                }            
                string fileContent;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = await reader.ReadToEndAsync();
                }
                 result = await chatgpt.SendMessageToChatGPT(fileContent);

                return result;          
        }
    }
}
