using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AIPart
{
    static public class StringParser  
    {
        public static void ExtractValues(string input, out string category, out string minPrice, out string maxPrice)//剪切ai的回答，得到品类和价格区间
        {
            // 分割主部分（品类和价格区间）
            string[] mainParts = input.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            // 提取品类
            string categoryPart = mainParts[0].Trim();
            string[] categorySplit = categoryPart.Split(new[] { ':' }, 2);
            category = categorySplit.Length > 1 ? categorySplit[1].Trim() : string.Empty;

            // 提取价格区间
            string pricePart = mainParts[1].Trim();
            string[] priceSplit = pricePart.Split(new[] { ':' }, 2);
            string[] priceRange = priceSplit.Length > 1 ? priceSplit[1].Trim().Split('-') : new string[0];

            minPrice = priceRange.Length > 0 ? priceRange[0].Trim() : string.Empty;
            maxPrice = priceRange.Length > 1 ? priceRange[1].Trim() : string.Empty;
        }
    }

    static public partial class AI
    {
        static private async Task<string> AIThink(string message)//输入用户的输入，输出ai的回答
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer sk-f17e0a1077a1413487109e740b924928");

                var requestData = new
                {
                    model = "deepseek-chat",
                    messages = new[] { new { role = "system", content = "你要从我的语言中推断出我想买什么东西以及我可以接受的价格区间，并严格按照如下格式输出；输出格式:品类:xxx | 价格区间:xxx-xxx" },
                                       new { role = "user", content = message }
                    },
                    temperature = 0.7,
                    max_tokens = 100
                };

                var response = await client.PostAsync(
                    "https://api.deepseek.com/v1/chat/completions",
                    new StringContent(JsonConvert.SerializeObject(requestData),
                                    Encoding.UTF8,
                                    "application/json")
                );
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseContent);

                return result?.choices?[0]?.message?.content?.ToString()
                       ?? "Error: Invalid response format";
            }
            catch (Exception ex)
            {
                return $"API Error: {ex.Message}";
            }
        }

        static async Task Main()
        {
            string message = "3000元左右的笔记本电脑";
            string result = await AI.AIThink(message); 
            Console.WriteLine(result);

            string category;
            string minPrice;
            string maxPrice;

            StringParser.ExtractValues(result,out category,out minPrice,out maxPrice);

            Console.WriteLine(category);
            Console.WriteLine(minPrice);
            Console.WriteLine(maxPrice);
        }
    }
}