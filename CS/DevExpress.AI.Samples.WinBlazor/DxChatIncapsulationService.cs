using DevExpress.AI.Samples.WinBlazor;

namespace DevExpress.Blazor.Internal
{
    class DxChatIncapsulationService : ISelfEncapsulationService
    {
        public WinChatUIWrapperSk? dxChatUI;
        public void Initialize(WinChatUIWrapperSk dxChatUI)
        {
            this.dxChatUI = dxChatUI;
        }
    }
}
