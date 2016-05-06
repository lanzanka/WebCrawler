# WebCrawler

Simple Web Crawler. Crawling all hrefs on given link to the given depth and gathering page url, title, timestamp and images count. Exporting results to a .csv file.

Using:
- HtmlAgilityPack for HTML parsing
- ServiceStack.Text for CSV serializing
- Ninject for Dependency Injection
- log4net for errors logging
- Moq & NUnit for unit testing
