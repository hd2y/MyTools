using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;
using Xunit;

namespace MyTools.Daily
{
    public class ZDicHelperTests
    {
        [Theory,
         InlineData("现代汉语常用字表", new[] {"https://www.zdic.net/zd/zb/cc1/", "https://www.zdic.net/zd/zb/cc2/"}),
         InlineData("现代汉语通用字表",
             new[]
             {
                 "https://www.zdic.net/zd/zb/ty/bh/?bh=0", "https://www.zdic.net/zd/zb/ty/bh/?bh=1",
                 "https://www.zdic.net/zd/zb/ty/bh/?bh=2", "https://www.zdic.net/zd/zb/ty/bh/?bh=3",
                 "https://www.zdic.net/zd/zb/ty/bh/?bh=4"
             })]
        public void ReadUniversalUsedWordTableShouldBeSuccess(string fileName, string[] urlList)
        {
            var savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"{fileName}.txt");
            ZDicHelper.ReadUniversalUsedWordTableAsync(savePath, urlList)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        [Fact]
        public void ReadStandardWordTableShouldBeSuccess()
        {
            // 内容不完整
            using var client = new HttpClient();
            var html = client.GetStringAsync("https://www.zdic.net/zd/zb/tc1/")
                .ConfigureAwait(false).GetAwaiter().GetResult();
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var urlList = document.DocumentNode.SelectNodes("//li/a[@class='pck']").Select(n =>
                $"https://www.zdic.net/zd/zb/tc1/bh/?bh={n.Attributes["title"].Value}").ToArray();
            ReadUniversalUsedWordTableShouldBeSuccess("常用國字標準字體表", urlList);
        }
    }
}