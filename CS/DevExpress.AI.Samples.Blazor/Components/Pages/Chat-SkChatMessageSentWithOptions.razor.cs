using DevExpress.AI.Samples.Blazor.Data;
using DevExpress.AIIntegration.Blazor.Chat;
using DevExpress.AIIntegration.Services.Chat;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Components;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Diagnostics;
using System.Text.Json;

namespace DevExpress.AI.Samples.Blazor.Components.Pages
{
    public partial class Chat_SkChatMessageSentWithOptions:ComponentBase
    {
        string CurrentMessage { get; set; }
        DxAIChat dxAIChat { get; set; }
        ChatHistory ChatHistory = new ChatHistory();
        string Structure = "";
        string OptionSets = "";
        protected override void OnInitialized()
        {
            base.OnInitialized();

            MessageData ResponseWithOption = new MessageData();
            ResponseWithOption.MessageTemplateName = "Options";
            ResponseWithOption.Message = "Please select one of the location";
            // Create a list of options
            ResponseWithOption.Options = new List<Option>
        {
            new Option { Image = "./images/location1.png", Url = "https://example.com/location1", Description = "Location 1" },
            new Option { Image = "./images/location2.png", Url = "https://example.com/location2", Description = "Location 2" },
            new Option { Image = "./images/location3.png", Url = "https://example.com/location3", Description = "Location 3" }
        };
            List<OptionSet> optionSets = new List<OptionSet>();
            OptionSet optionSetCats = new OptionSet
            {
                Name = "Cat Halloween customs",
                Description = "A list of or Halloween customs for cats",
                Options = new List<Option>
            {
                new Option { Image = "./images/catblack.png", Url = "https://cat.com/black", Description = "Black cat custom" },
                new Option { Image = "./images/catwhite.png", Url = "https://cat.com/white", Description = "White cat custom" },
                new Option { Image = "./images/catorange.png", Url = "https://cat.com/orange", Description = "Orange cat custom" }
            }
            };
            optionSets.Add(optionSetCats);
            OptionSet optionSetDogs = new OptionSet
            {
                Name = "Dogs Halloween customs",
                Description = "A list of or Halloween customs for dogs",
                Options = new List<Option>
            {
                new Option { Image = "./images/dogblack.png", Url = "https://dog.com/black", Description = "Black dog custom" },
                new Option { Image = "./images/dogwhite.png", Url = "https://dog.com/white", Description = "White dog custom" },
                new Option { Image = "./images/dogorange.png", Url = "https://dog.com/orange", Description = "Orange dog custom" }
            }
            };
            optionSets.Add(optionSetDogs);

            Structure = JsonSerializer.Serialize(ResponseWithOption);
            OptionSets = JsonSerializer.Serialize(optionSets);



        }
        MessageData PreProcessUserMessage(string Message)
        {
            MessageData messageData =new MessageData();
            messageData.Message = Message;
            messageData.MessageTemplateName = "Message";
            return messageData;
        }
        async Task SendMessageCustom()
        {
            
        }
        async Task OptionClicked(Option option, MessageData md)
        {
            this.dxAIChat.CurrentMessage = $"the option you selected is {option.Description}";
            await dxAIChat.SendButton.Click.InvokeAsync();
        }

        async Task MessageSent(MessageSentEventArgs args)
        {
#pragma warning disable SKEXP0010

            //Add to the history the message the user just sent (notice we are adding it using AddUserMessage)
            ChatHistory.AddUserMessage(args.Content);
            //Pass the chat history to the chat completions service

            var Settings = new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0.5, ResponseFormat = "json_object" };


            Settings.ChatSystemPrompt = $"you need to answer on using this json format using this structure {Structure}"
            + $"before given an answer check if the answer exist on this list of option sets {OptionSets}"
            + $"if your answer does not include options the message template value should be 'Message' otherwise 'Options'";
            var Result = await chatCompletionsService.GetChatMessageContentAsync(ChatHistory, Settings);

            //based the chat history we get an answer from the service
            string MessageContent = Result.InnerContent.ToString();


            Debug.WriteLine("Message from chat completion service:" + MessageContent);

            //Add to the history the message from the chat  completions service (notice we are adding it using AddAssistantMessage)
            ChatHistory.AddAssistantMessage(MessageContent);

            //the we push the answer to the U.I
            var message = new AIIntegration.Services.Chat.Message(MessageRole.Assistant, MessageContent);
            args.SendMessage(message);
        }
    }
}
