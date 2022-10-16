using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackOverflow_Statistics.Common;
using StackOverflow_Statistics.Models;
using StackOverflow_Statistics.Services;
using StackOverflow_Statistics.Services.Interfaces;
using StackOverflow_Statistics.ViewModels;
using StackOverflow_Statistics.Views;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace StackOverflow_Statistics
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        public static IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\StackOverflow-db\\StackOverflow-SQL-Server-2010\\StackOverflow2010.mdf;Integrated Security=True;Connect Timeout=30"));
                    services.AddTransient<IBadgeService, BadgeService>();
                    services.AddTransient<ICommentService, CommentService>();
                    services.AddTransient<IPostService, PostService>();
                    services.AddTransient<IUserService, UserService>();
                    services.AddTransient<IVoteService, VoteService>();

                    //ViewModels
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<ViewModelLocator>();
                    services.AddSingleton<UsersMostReputationViewModel>();
                    
                    // Windows
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<UsersMostReputationWindow>();
                })
                .Build();

            ServiceProvider = host.Services;
        }

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await host.StartAsync();

            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Source = new Uri("MainPage.xaml", UriKind.Relative);
            window.Show();
        }
    }
}
