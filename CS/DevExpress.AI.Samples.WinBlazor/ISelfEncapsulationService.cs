using DevExpress.AI.Samples.WinBlazor;

namespace DevExpress.Blazor.Internal {
    public interface ISelfEncapsulationService {
        void Initialize(WinChatUIWrapper dxChatUI);
    }

    class DxChatIncapsulationService : ISelfEncapsulationService {
        public WinChatUIWrapper? dxChatUI;
        public void Initialize(WinChatUIWrapper dxChatUI) {
            this.dxChatUI = dxChatUI;
        }
    }
}
