using AutoMapper;
using FluentAssertions;
using Moq;
using ReportService.Application.DTOs.ReportDetailDTOs;
using ReportService.Application.DTOs.ReportDTOs;
using ReportService.Application.DTOs.ResponseDTOs;
using ReportService.Application.DTOs.StatisticDTOs;
using ReportService.Application.Interfaces.Http;
using ReportService.Application.Interfaces.Repositories;
using ReportService.Domain.Entities;
using ReportService.Domain.Enums;

namespace ReportService.Tests.Infrastructure.Services
{
    public class ReportServiceTests
    {
        private readonly Mock<IReportRepository> _mockRepository;
        private readonly Mock<IContactClient> _mockContactClient;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportService.Infrastructure.Services.ReportService _reportService;

        public ReportServiceTests()
        {
            _mockRepository = new Mock<IReportRepository>();
            _mockContactClient = new Mock<IContactClient>();
            _mockMapper = new Mock<IMapper>();
            _reportService = new ReportService.Infrastructure.Services.ReportService(_mockRepository.Object, _mockMapper.Object, _mockContactClient.Object);
        }
        [Fact]
        public async Task AddReportAsync_ShouldReturnReportResponseDto()
        {
            //Arrange
            var report = new Report();
            var addedreport = new Report() { Id = Guid.NewGuid(), RequestedAt = DateTime.UtcNow, ReportStatus = ReportStatus.Preparing };
            var mappedReport = new ReportResponseDto() { Id = addedreport.Id, ReportStatus = addedreport.ReportStatus, RequestedAt = addedreport.RequestedAt };
            _mockRepository.Setup(r => r.AddReportAsync(It.IsAny<Report>())).ReturnsAsync(addedreport);
            _mockMapper.Setup(m => m.Map<ReportResponseDto>(addedreport)).Returns(mappedReport);
            //Act
            var result = await _reportService.AddReportAsync();
            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(mappedReport.Id);
            result.Should().BeOfType<ReportResponseDto>();
            _mockRepository.Verify(r => r.AddReportAsync(It.IsAny<Report>()), Times.Once);
            _mockRepository.Verify(r => r.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task PrepareReportAsync_WhenValidId_ShouldCompleteReport()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var report = new Report { Id = reportId, ReportDetails = new List<ReportDetail>(), ReportStatus = ReportStatus.Preparing };
            var stats = new List<LocationStatisticDto>
             {
                 new() { Location = "Ankara", PersonCount = 3, PhoneCount = 5 }
             };

            _mockRepository.Setup(r => r.GetReportByIdAsync(reportId)).ReturnsAsync(report);
            _mockContactClient.Setup(c => c.GetLocationStatisticAsync()).ReturnsAsync(stats);

            // Act
            await _reportService.PrepareReportAsync(reportId);

            // Assert
            report.ReportStatus.Should().Be(ReportStatus.Completed);
            report.ReportDetails.Should().HaveCount(1);
            _mockRepository.Verify(r => r.UpdateReport(It.IsAny<Report>()), Times.Once);
            _mockRepository.Verify(r => r.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task PrepareReportAsync_ShouldUpdateReportWithDetails()
        {
            var reportId = Guid.NewGuid();

            var existingReport = new Report { Id = reportId, ReportStatus = ReportStatus.Preparing };
            _mockRepository.Setup(r => r.GetReportByIdAsync(reportId)).ReturnsAsync(existingReport);

            var locationStats = new List<LocationStatisticDto> {
                new LocationStatisticDto { Location = "Ankara", PersonCount = 5, PhoneCount = 10 },
                new LocationStatisticDto { Location = "Istanbul", PersonCount = 7, PhoneCount = 14 }
            };
            _mockContactClient.Setup(c => c.GetLocationStatisticAsync()).ReturnsAsync(locationStats);



            await _reportService.PrepareReportAsync(reportId);

            existingReport.ReportStatus.Should().Be(ReportStatus.Completed);
            existingReport.ReportDetails.Should().HaveCount(2);
            existingReport.Id.Should().Be(reportId);
            existingReport.Should().NotBeNull();
            _mockRepository.Verify(r => r.UpdateReport(existingReport), Times.Once);
            _mockRepository.Verify(r => r.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task PrepareReportAsync_WhenContactClientFails_ShouldSetStatusToFailed()
        {
            var reportId = Guid.NewGuid();
            var report = new Report { Id = reportId };

            _mockRepository.Setup(r => r.GetReportByIdAsync(reportId)).ReturnsAsync(report);
            _mockContactClient.Setup(c => c.GetLocationStatisticAsync()).ThrowsAsync(new Exception("HTTP error"));

            // Act
            await _reportService.PrepareReportAsync(reportId);

            // Assert
            report.ReportStatus.Should().Be(ReportStatus.Failed);
            _mockRepository.Verify(r => r.UpdateReport(It.IsAny<Report>()), Times.Once);
            _mockRepository.Verify(r => r.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task GetAllReportsAsync_ShouldReturnMappedDtos()
        {
            //Arrange
            var reports = new List<Report> { new Report { Id = Guid.NewGuid(), RequestedAt = DateTime.UtcNow, ReportStatus = ReportStatus.Completed } };
            var mappedReports = new List<ReportResponseDto> { new ReportResponseDto { Id = reports[0].Id, RequestedAt = reports[0].RequestedAt, ReportStatus = reports[0].ReportStatus } };

            _mockRepository.Setup(r => r.GetAllReportsAsync()).ReturnsAsync(reports);
            _mockMapper.Setup(m => m.Map<IEnumerable<ReportResponseDto>>(reports)).Returns(mappedReports);
            //Act
            var result = await _reportService.GetAllReportsAsync();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(mappedReports);
        }
        [Fact]
        public async Task GetReportByIdAsync_ShouldReturnMappedDto()
        {

            //Arrange
            var report = new Report { Id = Guid.NewGuid(), RequestedAt = DateTime.UtcNow, ReportStatus = ReportStatus.Completed };
            var mappedReport = new ReportResponseDto { Id = report.Id, RequestedAt = report.RequestedAt, ReportStatus = report.ReportStatus };

            _mockRepository.Setup(r => r.GetReportByIdAsync(report.Id)).ReturnsAsync(report);
            _mockMapper.Setup(m => m.Map<ReportResponseDto>(report)).Returns(mappedReport);
            //Act
            var result = await _reportService.GetReportByIdAsync(report.Id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(mappedReport);
        }
        [Fact]
        public async Task UpdateReportAsync_ShouldReturnUpdatedReport()
        {
            //Arrange
            var dto = new ReportUpdateDto { Id = Guid.NewGuid(), ReportStatus = ReportStatus.Completed };
            var entity = new Report { Id = dto.Id, ReportStatus = dto.ReportStatus };
            var mapped = new ReportResponseDto { Id = dto.Id, ReportStatus = dto.ReportStatus };
            _mockMapper.Setup(m => m.Map<Report>(dto)).Returns(entity);
            _mockRepository.Setup(r => r.UpdateReport(entity)).Returns(entity);
            _mockRepository.Setup(r => r.CommitAsync()).ReturnsAsync(1);
            _mockMapper.Setup(m => m.Map<ReportResponseDto>(entity)).Returns(mapped);
            //Act
            var result = await _reportService.UpdateReportAsync(dto);
            //Assert
            result.Should().BeOfType<ReportResponseDto>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(mapped);
            _mockRepository.Verify(r => r.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task GetReportWithDetailListByAsync_ShouldReturnAllReportsWithDetails()
        {
            var reports = new List<Report>
            {
                new Report
                {
                    Id = Guid.NewGuid(),
                    RequestedAt = DateTime.UtcNow,
                    ReportStatus = ReportStatus.Completed,
                    ReportDetails = new List<ReportDetail>
                    {
                        new ReportDetail { Id = Guid.NewGuid(), Location = "Izmir", PersonCount = 3, PhoneCount = 5 }
                    }
                },
                new Report
                {
                    Id = Guid.NewGuid(),
                    RequestedAt = DateTime.UtcNow,
                    ReportStatus = ReportStatus.Completed,
                    ReportDetails = new List<ReportDetail>
                    {
                        new ReportDetail { Id = Guid.NewGuid(), Location = "Ankara", PersonCount = 4, PhoneCount = 6 }
                    }
                }
            }.AsQueryable();

            _mockRepository.Setup(r => r.GetReportQueryable()).Returns(reports);

            var result = await _reportService.GetReportWithDetailListByAsync();

            result.Should().HaveCount(2);
            result.SelectMany(r => r.ReportDetails).Should().HaveCount(2);
        }

    }
}

