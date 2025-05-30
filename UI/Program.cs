using AutoMapper;
using UI.AutoMapper;
namespace UI
{
    internal static class Program
    {
        public static IMapper Mapper { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            // ²í³ö³àë³çàö³ÿ AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperUI>();
            });
            Mapper = config.CreateMapper();

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
