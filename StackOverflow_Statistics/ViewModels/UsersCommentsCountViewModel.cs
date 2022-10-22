using StackOverflow_Statistics.Common;
using StackOverflow_Statistics.Common.Enums;
using StackOverflow_Statistics.Dtos;
using StackOverflow_Statistics.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StackOverflow_Statistics.ViewModels
{
    public class UsersCommentsCountViewModel : ObservableObject
    {
        private static bool IsRequesting = true;
        private readonly IUserService userService;
        public ObservableCollection<UsersCommentsCountDto> Items { get; set; }
        public ICommand NextButtonCommand { get; set; }
        public ICommand PrevButtonCommand { get; set; }
        public ICommand FirstButtonCommand { get; set; }
        public ICommand LastButtonCommand { get; set; }
        private const int PageSize = 10;
        private static int _skip = 0;
        private readonly long UsersCount;
        private UserCommentOrderEnum currentOption;
        public string CountString => (PageSize + _skip) + " of " + UsersCount;
        private enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };

        public UsersCommentsCountViewModel(IUserService userService)
        {
            this.userService = userService;

            UsersCount = this.userService.GetUsersWithCommentCount();

            NextButtonCommand = new RelayCommand(_ => ButtonClicked(NextButtonClicked), _ => !IsRequesting && _skip < UsersCount - PageSize);
            PrevButtonCommand = new RelayCommand(_ => ButtonClicked(PrevButtonClicked), _ => !IsRequesting && _skip > 0);
            FirstButtonCommand = new RelayCommand(_ => ButtonClicked(FirstButtonClicked), _ => !IsRequesting && _skip != 0);
            LastButtonCommand = new RelayCommand(_ => ButtonClicked(LastButtonClicked), _ => !IsRequesting && _skip < UsersCount - PageSize);
            GetData(0, PageSize).ConfigureAwait(false);
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

        public UserCommentOrderEnum OrderType
        {
            get => currentOption;
            set
            {
                if (currentOption != value)
                {
                    currentOption = value;
                    this.FirstButtonCommand.Execute(null);
                }
            }
        }

        private async Task GetData(int skip, int take)
        {
            Items = new ObservableCollection<UsersCommentsCountDto>(await userService.GetUserComments(skip, take, OrderType));
            OnPropertyChanged(nameof(Items));
            IsRequesting = false;
        }
    }
}
