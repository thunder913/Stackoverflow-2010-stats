using StackOverflow_Statistics.Common;
using StackOverflow_Statistics.Dtos;
using StackOverflow_Statistics.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using StackOverflow_Statistics.Views;

namespace StackOverflow_Statistics.ViewModels
{
    public class MostViewedPostsViewModel : ObservableObject
    {
        private static bool IsRequesting = true;
        private readonly IPostService postService;
        public ObservableCollection<MostViewedPostsDto> Items { get; set; }
        public MostViewedPostsDto SelectedItem { get; set; }
        public ICommand NextButtonCommand { get; set; }
        public ICommand PrevButtonCommand { get; set; }
        public ICommand FirstButtonCommand { get; set; }
        public ICommand LastButtonCommand { get; set; }
        public ICommand ClickCommand { get; set; }
        private const int PageSize = 10;
        private static int _skip = 0;
        private long PostsCount;
        public string CountString => (PageSize + _skip) + " of " + PostsCount;

        public MostViewedPostsViewModel(IPostService postService)
        {
            this.postService = postService;

            Task.Run(async () =>
            {
                PostsCount = await this.postService.GetPostsCountAsync();
            }).Wait();

            NextButtonCommand = new RelayCommand(_ => ButtonClicked(NextButtonClicked), _ => !IsRequesting && _skip < PostsCount - PageSize);
            PrevButtonCommand = new RelayCommand(_ => ButtonClicked(PrevButtonClicked), _ => !IsRequesting && _skip > 0);
            FirstButtonCommand = new RelayCommand(_ => ButtonClicked(FirstButtonClicked), _ => !IsRequesting && _skip != 0);
            LastButtonCommand = new RelayCommand(_ => ButtonClicked(LastButtonClicked), _ => !IsRequesting && _skip < PostsCount - PageSize);
            GetData(0, PageSize).ConfigureAwait(false);
            ClickCommand = new RelayCommand(_ => DataGridClickEvent());
        }

        private async Task GetData(int skip, int take)
        {
            Items = new ObservableCollection<MostViewedPostsDto>(await postService.GetMostViewedPostsAsync(skip, take));
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
            _skip = Math.Min(_skip, (int)PostsCount - PageSize);
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
            _skip = (int)(PostsCount - PageSize);
            await GetData(_skip, PageSize);
            OnPropertyChanged(nameof(CountString));
        }

        private void DataGridClickEvent()
        {
            var param = new PostAnswerParameter()
            {
                PostId = SelectedItem.Id,
                AnswerId = SelectedItem.AcceptedAnswer ?? 0
            };
            Navigator.Navigate($"Views/{nameof(PostWithAnswerPage)}.xaml", param);
        }
    }
}
