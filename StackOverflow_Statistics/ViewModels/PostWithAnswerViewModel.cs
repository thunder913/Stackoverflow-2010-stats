using StackOverflow_Statistics.Common;

namespace StackOverflow_Statistics.ViewModels
{
    public class PostWithAnswerViewModel : ObservableObject
    {
        public PostWithAnswerViewModel()
        {
            HtmlString = "<h1>Test Message</h1>";
        }

        public string htmlString { get; set; }
        public string HtmlString
        {
            get => htmlString;
            set
            {
                htmlString = value;
                OnPropertyChanged();
            }
        }
    }
}
