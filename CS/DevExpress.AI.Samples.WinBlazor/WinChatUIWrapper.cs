using DevExpress.AIIntegration.Blazor.Chat;
using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace DevExpress.AI.Samples.WinBlazor {
    public class WinChatUIWrapper : DxAIChat {
        [Inject] ISelfEncapsulationService SelfIncapsulationService { get; set; } = default!;
        protected override void OnInitialized() {
            SelfIncapsulationService.Initialize(this);
            base.OnInitialized();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            DxResourceManager.RegisterScripts()(builder);
            base.BuildRenderTree(builder);
        }
    }
}
