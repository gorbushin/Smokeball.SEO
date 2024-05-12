using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Smokeball.SEO.Services;

namespace Smokeball.SEO
{
    public partial class MainWindow : Window
    {
        private readonly ICheckSeoService _checkSeoService;

        public MainWindow(ICheckSeoService checkSeoService)
        {
            InitializeComponent();
            _checkSeoService = checkSeoService;
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            var searchEngineUrl = "https://www.google.com.au";
            var limit = 100;
            var keywords = KeywordsField.Text;
            var urlToFind = UrlField.Text;

            if (urlToFind != null && keywords != null)
            {
                var seoResult = _checkSeoService.CheckUrlSeo(searchEngineUrl, keywords, limit, urlToFind);
                ShowResult.Text = seoResult.ToString();
            }
        }
    }
}