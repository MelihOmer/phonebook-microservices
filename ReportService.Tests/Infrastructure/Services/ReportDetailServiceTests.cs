using AutoMapper;
using FluentAssertions;
using Moq;
using ReportService.Application.DTOs.ReportDetailDTOs;
using ReportService.Application.Interfaces.Repositories;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.Services;

namespace ReportService.Tests.Infrastructure.Services
{
    public class ReportDetailServiceTests
    {
        private readonly Mock<IReportDetailsRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportDetailService _reportDetailService;

        public ReportDetailServiceTests()
        {
            _mockRepository = new Mock<IReportDetailsRepository>();
            _mockMapper = new Mock<IMapper>();
            _reportDetailService = new ReportDetailService(_mockRepository.Object, _mockMapper.Object);

        }
        [Fact]
        public async Task AddReportDetailAsync_ShouldReturnMappedDto()
        {
            //Arrange
            var createDto = new ReportDetailCreateDto { ReportId = Guid.NewGuid(), Location = "Test", PersonCount = 3, PhoneCount = 2 };
            var entity = new ReportDetail { Id = Guid.NewGuid(), Location = createDto.Location };
            var mappedDto = new ReportDetailResponseDto { Id = entity.Id, Location = entity.Location };

            _mockMapper.Setup(m => m.Map<ReportDetail>(createDto)).Returns(entity);
            _mockRepository.Setup(r => r.AddReportDetailAsync(entity)).ReturnsAsync(entity);
            _mockRepository.Setup(r => r.CommitAsync()).ReturnsAsync(1);
            _mockMapper.Setup(m => m.Map<ReportDetailResponseDto>(entity)).Returns(mappedDto);
            //Act
            var result = await _reportDetailService.AddReportDetailAsync(createDto);
            //Assert
            result.Should().BeEquivalentTo(mappedDto);
            result.Should().BeOfType<ReportDetailResponseDto>();
            _mockRepository.Verify(r => r.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task GetAllReportDetailsAsync_ShouldReturnMappedList()
        {
            //Arrange
            var entities = new List<ReportDetail> { new ReportDetail() { Location = "Ankara" }, new ReportDetail() { Location = "İstanbul" } };
            var mappedDtos = new List<ReportDetailResponseDto> { new ReportDetailResponseDto(), new ReportDetailResponseDto() };

            _mockRepository.Setup(r => r.GetAllReportDetailsAsync()).ReturnsAsync(entities);
            _mockMapper.Setup(m => m.Map<IEnumerable<ReportDetailResponseDto>>(entities)).Returns(mappedDtos);
            //Act
            var result = await _reportDetailService.GetAllReportDetailsAsync();
            //Assert
            result.Should().BeEquivalentTo(mappedDtos);
            result.Should().BeOfType<List<ReportDetailResponseDto>>();
        }
        [Fact]
        public async Task GetReportDetailsByReportId_ShouldReturnFilteredMappedList()
        {
            var reportId = Guid.NewGuid();
            var data = new List<ReportDetail>
            {
            new ReportDetail { ReportId = reportId },
            new ReportDetail { ReportId = Guid.NewGuid() }
            }.AsQueryable();

            var expected = new List<ReportDetailResponseDto>
            {
            new ReportDetailResponseDto { ReportId = reportId }
            };

            _mockRepository.Setup(r => r.GetReportDetailQueryable()).Returns(data);
            _mockMapper.Setup(m => m.Map<IEnumerable<ReportDetailResponseDto>>(It.Is<IEnumerable<ReportDetail>>(l => l.All(x => x.ReportId == reportId)))).Returns(expected);

            var result = await _reportDetailService.GetReportDetailsByReportId(reportId);

            result.Should().BeEquivalentTo(expected);
            result.First().ReportId.Should().Be(reportId);
            result.Should().BeOfType<List<ReportDetailResponseDto>>();
        }

    }

}