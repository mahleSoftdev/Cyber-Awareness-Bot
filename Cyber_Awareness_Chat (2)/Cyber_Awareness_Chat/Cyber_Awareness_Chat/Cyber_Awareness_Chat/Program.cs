namespace Cyber_Awareness_Chat
{
    internal static class Program
    {
         
        [STAThread]
        static void Main()
        {
          
            ApplicationConfiguration.Initialize();
            Application.Run(new Mainform());
        }
    }
}