using KeyEventLogger.Forms;

namespace KeyEventLogger;

/// <summary>Uygulamanın giriş noktası.</summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new AnaForm());
    }
}
