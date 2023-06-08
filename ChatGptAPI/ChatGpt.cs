using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ChatGptAPI
{
    public class chatgpt
    {
        public List<dynamic> systemMessages = new()
        {
             new { role="system", content="You are a helpful assistant Greeting the user ask the user for his name" },
             new { role="system", content="You are a helpful assistant ask the user if he want to know his grade" },
             new { role="system", content="You are a helpful assistant ask the user for his registration number to know his grade" },
        };
        private static List<dynamic> messages = new List<dynamic>();
        public static int systemCounter=0;
        public async Task<ChatGptResponse> SendMessageToChatGPT(string message)
        {
            var result=new ChatGptResponse();

            // add user message to message history list
            messages.Add(new { role = "user", content = message });

            if(systemCounter >= 0 && systemCounter <= systemMessages.Count()-1)
            {
                messages.Add(systemMessages[systemCounter]);
               ++systemCounter;
            }
            string model = "gpt-3.5-turbo";

            var payload = new
            {
                model,
                messages,
                max_tokens = 50,
                temperature=1
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    result.IsSucess = false;
                    result.Message = "Error: " + response.ReasonPhrase.ToString();
                }
                else
                {
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                    var choices = responseObject.choices;
                    result.IsSucess = true;
                    result.Message = choices[0].message.content.ToString();
                    // adding chatgpt response message to message history list
                    messages.Add(new { role = "assistant", content = result.Message });
                }

                return result;

            }


        }


    }
}
