using Azure.AI.OpenAI;
using Azure;
using DevExpress.AI.Samples.Blazor.Components;
using DevExpress.AIIntegration;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();

string azureOpenAIEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
string azureOpenAIKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");


string OpenAiKey = Environment.GetEnvironmentVariable("OpenAiTestKey");

var client = new OpenAIClient(new System.ClientModel.ApiKeyCredential(OpenAiKey));

builder.Services.AddDevExpressBlazor();

//HACK use this for Azure open AI
//builder.Services.AddDevExpressAI((config) => {
//    var client = new AzureOpenAIClient(
//        new Uri(azureOpenAIEndpoint),
//        new AzureKeyCredential(azureOpenAIKey));
//    config.RegisterChatClientOpenAIService(client, "gpt4o");
//    config.RegisterOpenAIAssistants(client, "gpt4o");
//});


//HACK use this for Open AI
builder.Services.AddDevExpressAI((config) => {

    //Open Ai models ID are a bit different than azure, Azure=gtp4o OpenAI=gpt-4o    
    config.RegisterChatClientOpenAIService(client, "gpt-4o");
    config.RegisterOpenAIAssistants(client, "gpt-4o");
});

//Adding semantic kernel
var KernelBuilder = Kernel.CreateBuilder();
KernelBuilder.AddOpenAIChatCompletion("gpt-4o", client);
var sk = KernelBuilder.Build();
var Chat = sk.GetRequiredService<IChatCompletionService>();
builder.Services.AddSingleton<IChatCompletionService>(Chat);

var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
