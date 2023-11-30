using LinkApplicationGraphics.Core;
using LinkApplicationGraphics.NVVM.ViewModel;
using LinkApplicationGraphics.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;

namespace LinkApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly ServiceProvider _serviceProvider;

        public App() 
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(serviceProvider => new MainWindow
            {
                DataContext = serviceProvider.GetRequiredService<MainViewModel>()

            });

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<InterestViewModel>();
            services.AddSingleton<RegisterViewModel>();
            services.AddSingleton<HomePageViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<MatchesViewModel>();
            services.AddSingleton<EventsViewModel>();
            services.AddSingleton<ProfileViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();


            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));


            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
        public MainWindow CreateNewMainWindow()
        {
            return _serviceProvider.GetRequiredService<MainWindow>();
        }
    }
}
