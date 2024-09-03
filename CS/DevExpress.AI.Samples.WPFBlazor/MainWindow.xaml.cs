using System.Windows.Input;

namespace DevExpress.AI.Samples.WPFBlazor
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessageButton.Command?.Execute(null);
        }
    }
}