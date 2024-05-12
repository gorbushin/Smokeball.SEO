using System.Windows;
using Microsoft.Extensions.Options;
using Smokeball.SEO.Services;

namespace Smokeball.SEO;

public partial class MainWindow : Window
{
    private readonly ICheckSeoService _checkSeoService;
    private readonly IOptions<AppSettings> _config;

    public MainWindow(IOptions<AppSettings> config, ICheckSeoService checkSeoService)
    {
        _checkSeoService = checkSeoService;
        _config = config;

        InitializeComponent();

        UrlField.Text = _config.Value.UrlToCheck;
        KeywordsField.Text = _config.Value.Keywords;
    }

    private void Search_Button_Click(object sender, RoutedEventArgs e)
    {
        var searchEngineUrl = _config.Value.SearchEngineUri;
        var resultLimit = _config.Value.ResultLimit;

        var keywords = KeywordsField.Text.Trim();
        var urlToFind = UrlField.Text.Trim();

        if (!string.IsNullOrEmpty(urlToFind) && !string.IsNullOrEmpty(keywords))
        {
            var seoResult = _checkSeoService.CheckUrlSeo(searchEngineUrl, keywords, resultLimit, urlToFind);
            ShowResult.Text = seoResult.ToString();
        }
    }
}