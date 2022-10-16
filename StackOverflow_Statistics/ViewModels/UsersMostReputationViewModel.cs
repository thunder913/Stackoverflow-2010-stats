using Microsoft.Extensions.DependencyInjection;
using StackOverflow_Statistics.Services.Interfaces;
using StackOverflow_Statistics.Views;

namespace StackOverflow_Statistics.ViewModels
{
    public class UsersMostReputationViewModel
    {
        private readonly IUserService userService;

        public UsersMostReputationViewModel(IUserService userService)
        {
            this.userService = userService;
        }
    }
}
