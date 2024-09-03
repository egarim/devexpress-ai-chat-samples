<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/851207927/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1251539)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# AI Chat for Blazor - How to add DxAIChat component in Blazor, MAUI, WPF, and WinForms applications

DevExpress Blazor AI Chat component (`DxAIChat`) allows you to integrate AI-driven interactions into your applications. This component offers a variety of powerful features, including: 

* [Customizable message and empty message area appearance](#customize-message-and-empty-message-area-appearance)
* [Manual message processing](#manual-message-processing)
* [Streaming response](#streaming-response)
* [AI assistants compatibility](#ai-assistants-compatibility)
* [Integration into WinForms, WPF and .NET MAUI apps](#integration)

## Implementation Details

This example adds the `DxAIChat` in a Blazor application, customizes its settings and integrates it into WinForms, WPF, and .NET MAUI applications.

### Register AI Service

Add the following code to the _Program.cs_ file to register AI Chat service in the application:

```cs
using DevExpress.AIIntegration;

string azureOpenAIEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
string azureOpenAIKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
...
builder.Services.AddDevExpressAI((config) => {
    var client = new AzureOpenAIClient(
        new Uri(azureOpenAIEndpoint),
        new AzureKeyCredential(azureOpenAIKey));
    config.RegisterChatClientOpenAIService(client, "gpt4o");
});
```

File to review: [Program.cs](./CS/DevExpress.AI.Samples.Blazor/Program.cs)

### Add DxAIChat component in a Blazor Application

Add the `<DxAIChat>…</DxAIChat>` markup to a .razor file:

```razor
@using DevExpress.AIIntegration.Blazor.Chat
@using AIIntegration.Services.Chat;

<DxAIChat CssClass="my-chat" />
```

```css
.my-chat {
    width: 700px;
    margin: 20px;
}
```

![](AIChat.png)


File to review: [Chat.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat.razor)

### Customize message and empty message area appearance

`DxAIChat` component implements the following message customization properties:

* `MessageTemplate` - specifies a template for message bubbles.
* `MessageContentTemplate` - specifies a template for message bubble content.
* `EmptyMessageAreaTemplate` - specifies a template for an empty message area.

```razor
<DxAIChat CssClass="my-chat">
    <EmptyMessageAreaTemplate>
        <div class="my-chat-ui-description">
            AI Assistant is ready to answer your questions.
        </div>
    </EmptyMessageAreaTemplate>
    <MessageTemplate>
        <div class="@GetMessageClasses(context)">
            @if(context.Typing) {
                <span>Loading...</span>
            } else {
                <div class="my-chat-content">
                    @context.Content
                </div>
            }
        </div>
    </MessageTemplate>
</DxAIChat>
```

File to review: [Chat-CustomMessage.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-CustomMessage.razor), [Chat-CustomEmptyState.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-CustomEmptyState.razor)

### Manual message processing

When a user sends a message to the chat, the `MessageSent` event fires. Handle the event to manually process this action.
You can use the `Content` event argument to access the sent text or call the `SendMessage` method to send another message to the chat.

```razor
<DxAIChat CssClass="my-chat" MessageSent="MessageSent" />

@code {
    void MessageSent(MessageSentEventArgs args) {
        var message = new Message(MessageRole.Assistant, $"Processed: {args.Content}");
        args.SendMessage(message);
    }
}
```

File to review: [Chat-MessageSent.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-MessageSent.razor)


### Streaming response

When a user sends a request to the AI client, the entire response is generated before it is sent back. Set the `UseStreaming` property to `true` to enable the chat to stream the response as it is being generated and start displaying the beginning of the response before it is fully complete.

```razor
<DxAIChat CssClass="my-chat" UseStreaming="true" />
```

File to review: [Chat-Streaming.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-Streaming.razor)

### AI assistants compatibility

The `DxAIChat` component supports [OpenAI Assistants](https://techcommunity.microsoft.com/t5/ai-azure-ai-services-blog/announcing-azure-openai-service-assistants-public-preview/ba-p/4143217) that allow you to provide a model with supplementary documents containing external knowledge. OpenAI parses these documents and searches through them to retrieve relevant content to answer user queries.

Add the following code to the _Program.cs_ file to register AI Assistant service in the application:

```cs
builder.Services.AddDevExpressAI((config) => {
    ...
    config.RegisterOpenAIAssistants(client, "gpt4o");
});
```

Handle the `Initialized` event and call the `UseAssistantAsync` method to supply a file to the Open AI Assistant. 

```razor
<DxAIChat CssClass="my-chat" Initialized="Initialized" />

@code {
    const string DocumentResourceName = "DevExpress.AI.Samples.Blazor.Data.Restaurant Menu.pdf";
    const string prompt = "...";

    async Task Initialized(IAIChat chat) {
        await chat.UseAssistantAsync(new OpenAIAssistantOptions(
            $"{Guid.NewGuid().ToString("N")}.pdf",
            Assembly.GetExecutingAssembly().GetManifestResourceStream(DocumentResourceName),
            prompt)
        );
    }
}
```

File to review: [Chat-Assistant.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-Assistant.razor)

### <a name="integration"></a> Integration AI Chat into WinForms, WPF and .NET MAUI Apps

The Blazor Hybrid technology and the `BlazorWebView` components allows you to integrate `DxAIChat` component in WinForms, WPF and .NET MAUI applications. 

The key points of the implementation:

* The `ISelfEncapsulationService` interface allows you to work directly with the `DxAIChat` component instance and its properties from within desktop or mobile app.
* Built-in `DxAIChat` wrappers initialize the required Blazor Theme scripts.
* Custom CSS classes are used to hide the built-in input field and send button (see the _index.htm_ files).

Folders to review: [DevExpress.AI.Samples.MAUIBlazor](./CS/DevExpress.AI.Samples.MAUIBlazor/), [DevExpress.AI.Samples.WinBlazor](./CS/DevExpress.AI.Samples.WinBlazor/), [DevExpress.AI.Samples.WPFBlazor](./CS/DevExpress.AI.Samples.WPFBlazor/)

## Files to Review

* [Chat.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat.razor)
* [Chat-CustomMessage.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-CustomMessage.razor)
* [Chat-CustomEmptyState.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-CustomEmptyState.razor)
* [Chat-MessageSent.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-MessageSent.razor)
* [Chat-Streaming.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-Streaming.razor)
* [Chat-Assistant.razor](./CS/DevExpress.AI.Samples.Blazor/Components/Pages/Chat-Assistant.razor)
* [Program.cs](./CS/DevExpress.AI.Samples.Blazor/Program.cs)

## Folders to Review

* [DevExpress.AI.Samples.MAUIBlazor](./CS/DevExpress.AI.Samples.MAUIBlazor/)
* [DevExpress.AI.Samples.WinBlazor](./CS/DevExpress.AI.Samples.WinBlazor/)
* [DevExpress.AI.Samples.WPFBlazor](./CS/DevExpress.AI.Samples.WPFBlazor/)

## Documentation

* [Create a Blazor Hybrid Project](https://docs.devexpress.com/Blazor/404118/get-started/create-project-hybrid)

## More Examples

* [Rich Text Editor and HTML Editor for Blazor - How to integrate AI-powered extensions](https://github.com/DevExpress-Examples/blazor-ai-integration-to-text-editors)


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=devexpress-ai-chat-samples&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=devexpress-ai-chat-samples&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->

