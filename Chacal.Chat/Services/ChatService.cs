using Chacal.Chat.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;

namespace Chacal.Chat.Services
{
    /// <summary>
    /// A chat service that stores the conversation history in memory
    /// and calls the LLM via the IAIClient (OLlama in this example).
    /// </summary>
    public class ChatService : IChatService
    {
        private readonly IAIClient _aiClient;

        // Local list to store message turns: (role, content)
        private readonly List<(string Role, string Content)> _conversationHistory;

        public ChatService(IAIClient aiClient)
        {
            _aiClient = aiClient;
            _conversationHistory = new List<(string, string)>();
        }

        public async Task<string> SendMessageAsync(string userMessage)
        {
            // Add the user's message to the conversation history
            _conversationHistory.Add(("user", userMessage));

            // Build a ChatRequest using Microsoft.Extensions.AI
            var chatRequest = new ChatRequest();

            // Convert our local history into the format expected by the library
            foreach (var (role, content) in _conversationHistory)
            {
                // ChatMessage is a class provided by Microsoft.Extensions.AI (or a similar namespace)
                chatRequest.Messages.Add(new ChatMessage(role, content));
            }

            // Ask the AI client for completions
            var completions = await _aiClient.GetChatCompletionsAsync(chatRequest);

            // Take the content of the first response (if any)
            var assistantResponse = completions.FirstOrDefault()?.Content ?? string.Empty;

            // Store the assistant response in the history with role "assistant"
            _conversationHistory.Add(("assistant", assistantResponse));

            return assistantResponse;
        }

        public IEnumerable<string> GetConversationHistory()
        {
            // Return each turn as "role: content"
            return _conversationHistory.Select(entry => $"{entry.Role}: {entry.Content}");
        }
    }
}
