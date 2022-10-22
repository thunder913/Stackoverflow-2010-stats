using Microsoft.Extensions.DependencyInjection;
using StackOverflow_Statistics.Common;
using StackOverflow_Statistics.Services.Interfaces;
using StackOverflow_Statistics.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace StackOverflow_Statistics.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IBadgeService BadgeService;
        private readonly ICommentService commentService;
        private readonly IPostService postService;
        private readonly IVoteService voteService;
        private readonly IUserService userService;
        public ICommand UsersWithMostReputationCommand { get; set; }
        public ICommand MostViewedPostsCommand { get; set; }
        public ICommand UsersCommentsCountCommand { get; set; }
        public ICommand UsersMostBadgesCommand { get; set; }
        public ICommand UsersPostsCountCommand { get; set; }
        public MainViewModel(IBadgeService badgeService, ICommentService commentService, IPostService postService, IVoteService voteService, IUserService userService)
        {
            this.BadgeService = badgeService;
            this.commentService = commentService;
            this.postService = postService;
            this.voteService = voteService;
            this.userService = userService;

            // Setting the values
            Task.Run(async () =>
            {
                BadgesCount = (await this.BadgeService.GetBadgesCountAsync()).ToString();
                CommentsCount = (await this.commentService.GetCommentsCountAsync()).ToString();
                PostsCount = (await this.postService.GetPostsCountAsync()).ToString();
                VotesCount = (await this.voteService.GetVotesCountAsync()).ToString();
                UsersCount = (await this.userService.GetUsersCountAsync()).ToString();
            }).ConfigureAwait(false);

            UsersWithMostReputationCommand = new RelayCommand(_ => this.UsersWithMostReputationClick());
            MostViewedPostsCommand = new RelayCommand(_ => this.MostViewedPostsClick());
            UsersCommentsCountCommand = new RelayCommand(_ => this.UsersCommentsCountClick());
            UsersMostBadgesCommand = new RelayCommand(_ => this.UsersMostBadgesClick());
            UsersPostsCountCommand = new RelayCommand(_ => this.UsersPostsCountClick());
        }

        private string _badgesCount = "Loading...";
        public string BadgesCount
        {
            get => _badgesCount;
            set
            {
                _badgesCount = value;
                OnPropertyChanged();
            }
        }

        private string _commentsCount = "Loading...";
        public string CommentsCount
        {
            get => _commentsCount;
            set
            {
                _commentsCount = value;
                OnPropertyChanged();
            }
        }

        private string _postsCount = "Loading...";
        public string PostsCount
        {
            get => _postsCount;
            set
            {
                _postsCount = value;
                OnPropertyChanged();
            }
        }

        private string _usersCount = "Loading...";
        public string UsersCount
        {
            get => _usersCount;
            set
            {
                _usersCount = value;
                OnPropertyChanged();
            }
        }

        private string _votesCount = "Loading...";
        public string VotesCount
        {
            get => _votesCount;
            set
            {
                _votesCount = value;
                OnPropertyChanged();
            }
        }

        public void UsersWithMostReputationClick()
        {
            Navigator.Navigate($"Views/{nameof(UsersMostReputationPage)}.xaml");
        }

        private void UsersPostsCountClick()
        {
            Navigator.Navigate($"Views/{nameof(UsersPostsCountPage)}.xaml");
        }

        private void UsersMostBadgesClick()
        {
            Navigator.Navigate($"Views/{nameof(UsersMostBadgesPage)}.xaml");
        }

        private void UsersCommentsCountClick()
        {
            Navigator.Navigate($"Views/{nameof(UsersCommentsCountPage)}.xaml");
        }

        private void MostViewedPostsClick()
        {
            Navigator.Navigate($"Views/{nameof(MostViewedPostsPage)}.xaml");
        }
    }
}
