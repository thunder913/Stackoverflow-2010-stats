using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StackOverflow_Statistics.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
        public UsersMostReputationViewModel UsersMostReputationViewModel => App.ServiceProvider.GetRequiredService<UsersMostReputationViewModel>();
        public MostViewedPostsViewModel MostViewedPostsViewModel => App.ServiceProvider.GetRequiredService<MostViewedPostsViewModel>();
        public UsersCommentsCountViewModel UsersCommentsCountViewModel => App.ServiceProvider.GetRequiredService<UsersCommentsCountViewModel>();
        public UsersMostBadgesViewModel UsersMostBadgesViewModel => App.ServiceProvider.GetRequiredService<UsersMostBadgesViewModel>();
        public UsersPostsCountViewModel UsersPostsCountViewModel => App.ServiceProvider.GetRequiredService<UsersPostsCountViewModel>();
    }
}
