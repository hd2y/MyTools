using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace MyTools.Daily
{
    public class MarkdownHelperTests
    {
        [Fact]
        public void DownloadImagesShouldBeSuccess()
        {
            var postPath = @"C:\Code\Github\hd2y.github.io\source\_posts";
            var imagePath = @"C:\Code\Github\hd2y.github.io\source\_posts";
            var newPostPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "post");
            MarkdownHelper.DownloadImagesAsync(postPath, imagePath, true, "./", newPostPath)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [Fact]
        public void CheckImagesShouldBeSuccess()
        {
            var postPath = @"C:\Code\Github\hd2y.github.io\source\_posts";
            var images = MarkdownHelper.CheckImages(postPath);
            var text = string.Join(Environment.NewLine, images.Select(a =>
                $"{a.Key}{string.Concat(a.Value.Select(b => $"{Environment.NewLine}\t--{b}"))}"));
            Debug.WriteLine(text);
        }
    }
}