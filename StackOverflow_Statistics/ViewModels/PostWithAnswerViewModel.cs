using StackOverflow_Statistics.Common;
using StackOverflow_Statistics.Dtos;
using StackOverflow_Statistics.Services.Interfaces;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.ViewModels
{
    public class PostWithAnswerViewModel : ObservableObject
    {
        public PostWithAnswerViewModel(IPostService postService)
        {
            var parameters = (PostAnswerParameter) Navigator.Parameter;
            
            Task.Run(async () =>
            {
                var post = await postService.GetPostAndAnswerByIdAsync(parameters.PostId, parameters.AnswerId);
                PostString = post.PostString;
                AnswerString = post.AnswerString;
            }).Wait();
        }

        public string postString { get; set; }
        public string PostString
        {
            get => postString;
            set
            {
                postString = value;
                OnPropertyChanged();
            }
        }

        public string answerString { get; set; }
        public string AnswerString
        {
            get => answerString;
            set
            {
                answerString = value;
                OnPropertyChanged();
            }
        }
    }
}
