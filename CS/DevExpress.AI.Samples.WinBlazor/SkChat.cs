using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.AIIntegration;
using DevExpress.Blazor.Internal;
using DevExpress.XtraEditors;
using Windows.Media.Protection.PlayReady;
using OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;

namespace DevExpress.AI.Samples.WinBlazor
{
    public partial class SkChat : XtraForm
    {
        DxChatIncapsulationService service = new ();
        public SkChat()
        {
            InitializeComponent();
            InitializeBlazorAIChat();
        }

        private void InitializeBlazorAIChat()
        {
            var services = new ServiceCollection();

            string azureOpenAIEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            string azureOpenAIKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

            services.AddWindowsFormsBlazorWebView();
            services.AddDevExpressBlazor();
            //HACK use this for Azure Open AI
            //services.AddDevExpressAI((config) =>
            //{
            //    config.RegisterChatClientOpenAIService(
            //        new AzureOpenAIClient(
            //        new Uri(azureOpenAIEndpoint),
            //        new AzureKeyCredential(azureOpenAIKey)), "gpt4o");
            //});

            string OpenAiKey = Environment.GetEnvironmentVariable("OpenAiTestKey");
            var client = new OpenAIClient(new System.ClientModel.ApiKeyCredential(OpenAiKey));

            //HACK use this for Open AI
            services.AddDevExpressAI((config) => {

                //Open Ai models ID are a bit different than azure, Azure=gtp4o OpenAI=gpt-4o    
                config.RegisterChatClientOpenAIService(client, "gpt-4o");
                config.RegisterOpenAIAssistants(client, "gpt-4o");
            });

            var KernelBuilder = Kernel.CreateBuilder();
            KernelBuilder.AddOpenAIChatCompletion("gpt-4o", client);
            var sk = KernelBuilder.Build();
            var Chat = sk.GetRequiredService<IChatCompletionService>();
            services.AddSingleton<IChatCompletionService>(Chat);

            services.AddSingleton<ISelfEncapsulationService>(service);
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<WinChatUIWrapperSk>("#app",
                new Dictionary<string, object> {
                {
                        "UseStreaming",
                        true
                }
            });

        
         
        }

        private async void SimpleButton1_Click(object sender, EventArgs e)
        {
            service.dxChatUI.CurrentMessage = textInput.Text;
            textInput.Text = string.Empty;
            simpleButton1.Enabled = false;
            await service.dxChatUI.SendButton?.Click.InvokeAsync();
        }

        private async void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                service.dxChatUI.CurrentMessage = textInput.Text;
                textInput.Text = string.Empty;
                await service.dxChatUI.SendButton?.Click.InvokeAsync();
            }
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            simpleButton1.Enabled = textInput.Text.Length > 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter & Keys.Control))
            {
                simpleButton1.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
