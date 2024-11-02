using DevExpress.AI.Samples.WinBlazor;

namespace DevExpress.Blazor.Internal {
    public interface ISelfEncapsulationService {
        void Initialize(WinChatUIWrapperSk dxChatUI);
    }

    class DxChatIncapsulationService : ISelfEncapsulationService {
        public WinChatUIWrapperSk? dxChatUI;
        public void Initialize(WinChatUIWrapperSk dxChatUI) {
            this.dxChatUI = dxChatUI;
        }
    }
}
