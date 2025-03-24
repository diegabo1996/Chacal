using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.AI.Inference;
using Chacal.Chat.Interfaces;

namespace Chacal.Chat.Services
{
    public class AzureLlmClient : ILlmClient
    {
        private readonly ChatCompletionsClient _client;
        private readonly string _model;

        public AzureLlmClient(string endpointUrl, string apiKey, string model)
        {
            var endpoint = new Uri(endpointUrl);
            var credential = new AzureKeyCredential(apiKey);
            _client = new ChatCompletionsClient(endpoint, credential);
            _model = model;
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var requestOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatRequestSystemMessage($$"""
You are an AI model assisting in analyzing live audience feedback during a livestream event.
You will receive a single viewer comment and must classify it according to the specified metrics below.

# Desired response

Respond with a strictly RFC8259-compliant JSON object, using the following format:

{
  "sentiment": "positive | neutral | negative",
  "tone": "friendly | sarcastic | excited | angry | confused | supportive | etc.",
  "engagementLevel": "low | medium | high", 
  "intent": "support | oppose | ask_question | make_joke | off_topic | etc.",
  "toxicity": "low | medium | high",
  "language": "detected ISO 639-1 code (e.g., en, es, zh)",
  "contains_gift": true | false,
  "contains_hashtags": true | false,
  "keywords": ["array", "of", "main", "words", "from", "comment"]
}

Do not provide any explanations or additional text. Only return the JSON object.
"""),
                    new ChatRequestUserMessage(message)
                },
                Model = _model
            };

            Response<ChatCompletions> response = await _client.CompleteAsync(requestOptions);
            return response.Value.Content;
        }
    }
}
