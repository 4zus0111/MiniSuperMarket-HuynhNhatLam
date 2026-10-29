namespace MiniSupermarket.WinForms;

/// <summary>Khung ứng dụng desktop; giao diện đầy đủ được xây dựng ở buổi 5.</summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form
        {
            Text = "MiniSupermarket Client - buổi 3 (giao diện đầy đủ ở buổi 5)",
            StartPosition = FormStartPosition.CenterScreen,
            Size = new Size(640, 360)
        });
    }
}
