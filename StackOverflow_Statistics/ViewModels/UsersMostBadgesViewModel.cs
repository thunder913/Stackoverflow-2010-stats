using StackOverflow_Statistics.Common;
using StackOverflow_Statistics.Dtos;
using StackOverflow_Statistics.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StackOverflow_Statistics.ViewModels
{
    public class UsersMostBadgesViewModel : ObservableObject
    {
        private static bool IsRequesting = true;
        private readonly IUserService userService;
        private readonly IBadgeService badgeService;
        public ObservableCollection<UsersMostBadgesDto> Items { get; set; }
        public ICommand NextButtonCommand { get; set; }
        public ICommand PrevButtonCommand { get; set; }
        public ICommand FirstButtonCommand { get; set; }
        public ICommand LastButtonCommand { get; set; }
        private const int PageSize = 10;
        private static int _skip = 0;
        private long UsersCount;
        public string CountString => (PageSize + _skip) + " of " + UsersCount;

        public UsersMostBadgesViewModel(IUserService userService, IBadgeService badgeService)
        {
            this.userService = userService;
            this.badgeService = badgeService;

            Task.Run(async () =>
            {
                UsersCount = await this.badgeService.GetUsersWithBadgesAsync();
            }).Wait();

            NextButtonCommand = new RelayCommand(_ => ButtonClicked(NextButtonClicked), _ => !IsRequesting && _skip < UsersCount - PageSize);
            PrevButtonCommand = new RelayCommand(_ => ButtonClicked(PrevButtonClicked), _ => !IsRequesting && _skip > 0);
            FirstButtonCommand = new RelayCommand(_ => ButtonClicked(FirstButtonClicked), _ => !IsRequesting && _skip != 0);
            LastButtonCommand = new RelayCommand(_ => ButtonClicked(LastButtonClicked), _ => !IsRequesting && _skip < UsersCount - PageSize);
            GetData(0, PageSize).ConfigureAwait(false);
        }

        private async Task GetData(int skip, int take)
        {
            Items = new ObservableCollection<UsersMostBadgesDto>(await userService.GetUsersWithMostBadgesAsync(skip, take));
            OnPropertyChanged(nameof(Items));
            IsRequesting = false;
        }

        private void ButtonClicked(Func<Task> action)
        {
            IsRequesting = true;

            var task = action.Invoke();

            task.ContinueWith(_ =>
                {
                    IsRequesting = false;
                });
        }

        private async Task NextButtonClicked()
        {
            _skip += PageSize;
            _skip = Math.Min(_skip, (int)UsersCount - PageSize);
            await GetData(_skip, PageSize);
            OnPropertyChanged(nameof(CountString));
        }

        private async Task PrevButtonClicked()
        {
            _skip -= PageSize;
            _skip = Math.Max(0, _skip);
            await GetData(_skip, PageSize);
            OnPropertyChanged(nameof(CountString));
        }

        private async Task FirstButtonClicked()
        {
            _skip = 0;
            await GetData(_skip, PageSize);
            OnPropertyChanged(nameof(CountString));
        }

        private async Task LastButtonClicked()
        {
            _skip = (int)(UsersCount - PageSize);
            await GetData(_skip, PageSize);
            OnPropertyChanged(nameof(CountString));
        }
    }
}