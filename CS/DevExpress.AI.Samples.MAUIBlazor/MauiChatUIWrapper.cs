using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using DevExpress.Blazor;
using DevExpress.AIIntegration.Blazor.Chat;

namespace DevExpress.AI.Samples.MAUIBlazor;

public interface ISelfEncapsulationService {
    void Initialize(MauiChatUIWrapper dxChatUI);
}

sealed class DxChatEncapsulationService : ISelfEncapsulationService {
    public static DxChatEncapsulationService Instance { get; private set; } = default!;
    public MauiChatUIWrapper DxChatUI { get; set; } = default!;

    public DxChatEncapsulationService() {
        Instance = this;
    }

    public void Initialize(MauiChatUIWrapper dxChatUI) {
        this.DxChatUI = dxChatUI;
    }
}

public class MauiChatUIWrapper : DxAIChat {
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