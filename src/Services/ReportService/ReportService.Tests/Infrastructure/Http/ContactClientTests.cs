using Moq.Protected;
using Moq;
using ReportService.Infrastructure.Http;
using System.Net;
using ReportService.Application.DTOs.StatisticDTOs;
using System.Text.Json;
using FluentAssertions;

namespace ReportService.Tests.Infrastructure.Http
{
    public class ContactClientTests
    {
        [Fact]
        public async Task GetLocationStatisticAsync_ShouldReturnLocationStats()
        {
            // Arrange
            var expectedStats = new List<LocationStatisticDto>
            {
            new LocationStatisticDto { Location = "Ankara", PersonCount = 5, PhoneCount = 10 },
            new LocationStatisticDto { Location = "İstanbul", PersonCount = 3, PhoneCount = 6 },
            };

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(expectedStats)),
                });

            var httpClient = new HttpClient(mockHandler.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };

            var contactClient = new ContactClient(httpClient);

            // Act
            var result = await contactClient.GetLocationStatisticAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedStats);
            result.First().Location.Should().Be("Ankara");
            result.First().PhoneCount.Should().Be(10);
            result.First().PersonCount.Should().Be(5);
            result.First(r => r.Location == "İstanbul").PersonCount.Should().Be(3);
            result.First(r => r.Location == "İstanbul").PhoneCount.Should().Be(6);
        }
        [Fact]
        public async Task GetLocationStatisticAsync_ShouldReturnEmptyList_OnFailure()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Error")
                });

            var httpClient = new HttpClient(mockHandler.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };

            var contactClient = new ContactClient(httpClient);

            // Act
            var result = await contactClient.GetLocationStatisticAsync();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
