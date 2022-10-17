using Microsoft.Extensions.DependencyInjection;
using StackOverflow_Statistics.Common;
using StackOverflow_Statistics.Services.Interfaces;
using StackOverflow_Statistics.Views;
using System;
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

            BadgesCount = this.BadgeService.GetBadgesCount();
            CommentsCount = this.commentService.GetCommentsCount();
            PostsCount = this.postService.GetPostsCount();
            VotesCount = this.voteService.GetVotesCount();
            UsersCount = this.userService.GetUsersCount();

            UsersWithMostReputationCommand = new RelayCommand(_ => this.UsersWithMostReputationClick());
            MostViewedPostsCommand = new RelayCommand(_ => this.MostViewedPostsClick());
            UsersCommentsCountCommand = new RelayCommand(_ => this.UsersCommentsCountClick());
            UsersMostBadgesCommand = new RelayCommand(_ => this.UsersMostBadgesClick());
            UsersPostsCountCommand = new RelayCommand(_ => this.UsersPostsCountClick());
        }

        private int _badgesCount;
        public int BadgesCount
        {
            get => _badgesCount;
            set
            {
                _badgesCount = value;
                OnPropertyChanged();
            }
        }

        private int _commentsCount;
        public int CommentsCount
        {
            get => _commentsCount;
            set
            {
                _commentsCount = value;
                OnPropertyChanged();
            }
        }

        private int _postsCount;
        public int PostsCount
        {
            get => _postsCount;
            set
            {
                _postsCount = value;
                OnPropertyChanged();
            }
        }

        private int _usersCount;
        public int UsersCount
        {
            get => _usersCount;
            set
            {
                _usersCount = value;
                OnPropertyChanged();
            }
        }

        private int _votesCount;
        public int VotesCount
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
