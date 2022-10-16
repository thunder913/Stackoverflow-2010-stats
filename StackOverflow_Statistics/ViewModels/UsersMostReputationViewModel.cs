using StackOverflow_Statistics.Services.Interfaces;

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
