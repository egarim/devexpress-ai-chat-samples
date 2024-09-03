using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DevExpress.AI.Samples.MAUIBlazor;

partial class MainViewModel : ObservableObject {

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(SendMessageCommand))]
    public string? message;

    [RelayCommand(CanExecute = nameof(CanSendMessage))]
    async Task SendMessageAsync() {
        var service = DxChatEncapsulationService.Instance;
        service.DxChatUI.CurrentMessage = Message!;
        Message = null;
        if (service.DxChatUI.SendButton != null)
            await service.DxChatUI.SendButton.Click.InvokeAsync();
    }

    bool CanSendMessage() {
        return !string.IsNullOrEmpty(Message);
    }
}
