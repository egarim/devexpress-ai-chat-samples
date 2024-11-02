using DevExpress.AIIntegration.Blazor.Chat;
using DevExpress.AIIntegration.Services.Chat;
using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Diagnostics;
using Message = DevExpress.AIIntegration.Services.Chat.Message;

namespace DevExpress.AI.Samples.WinBlazor {
    public class WinChatUIWrapperSk : DxAIChat {
        [Inject] ISelfEncapsulationService SelfIncapsulationService { get; set; } = default!;
        [Inject] IChatCompletionService chatCompletionsService { get; set; } = default!;
        protected override void OnInitialized() {
            SelfIncapsulationService.Initialize(this);
            base.OnInitialized();
          
            this.MessageSent =EventCallback.Factory.Create<MessageSentEventArgs>(this, OnMessageSent);
        }
        ChatHistory ChatHistory = new ChatHistory();
        private async Task OnMessageSent(MessageSentEventArgs args)
        {


            //Add to the history the message the user just sent (notice we are adding it using AddUserMessage)
            ChatHistory.AddUserMessage(args.Content);
            //Pass the chat history to the chat completions service
            var Result = await chatCompletionsService.GetChatMessageContentAsync(ChatHistory);

            //based the chat history we get an answer from the service
            string MessageContent = Result.InnerContent.ToString();

            Debug.WriteLine("Message from chat completion service:" + MessageContent);

            //Add to the history the message from the chat  completions service (notice we are adding it using AddAssistantMessage)
            ChatHistory.AddAssistantMessage(MessageContent);

            //the we push the answer to the U.I
            var message = new Message(MessageRole.Assistant, MessageContent);
            args.SendMessage(message);
            // Handle the event here. For example, you can log the message content:
            Console.WriteLine($"Message sent: {args.Content}");
         
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            DxResourceManager.RegisterScripts()(builder);
            base.BuildRenderTree(builder);
        }
    }
}
