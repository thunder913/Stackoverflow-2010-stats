using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StackOverflow_Statistics.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
        public UsersMostReputationViewModel UsersMostReputationViewModel => App.ServiceProvider.GetRequiredService<UsersMostReputationViewModel>();
    }
}
