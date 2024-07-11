using BlogPost.Application.Contract.Interface;
using BlogPost.Application.Feature.Author.Query;
using BlogPost.Application.Feature.Post.Query;
using BlogPost.Domain.DTOs.Response;
using Serilog;
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace BlogPost.Test;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

public class GetAllAuthorIdHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<ILogger> _loggerMock;
    private readonly GetAllAuthorIdHandler _handler;

    public GetAllAuthorIdHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _loggerMock = new Mock<ILogger>();
        _handler = new GetAllAuthorIdHandler(_loggerMock.Object, _authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_NoAuthorsFound_ReturnsNoRecordFound()
    {
        // Arrange
        var request = new GetAllAuthorsQuery(1);
        _authorRepositoryMock.Setup(repo => repo.GetAllAuthors(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((new List<AuthorResponseDto>(), 0));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal("00", result.ResponseCode);
        Assert.Empty(result.ResponseDetails);
        Assert.Equal("No record found.", result.ResponseMsg);
    }

    [Fact]
    public async Task Handle_AuthorsFound_ReturnsSuccess()
    {
        var request = new GetAllAuthorsQuery(1);
        var authors = new List<AuthorResponseDto> { new AuthorResponseDto { Id = 1, FirstName = "Author 1" , LastName = "Author 2", Email = "Author@gamil.com"} };
        _authorRepositoryMock.Setup(repo => repo.GetAllAuthors(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((authors, authors.Count));
        
        var result = await _handler.Handle(request, CancellationToken.None);

        
        Assert.Equal("00", result.ResponseCode);
        Assert.Equal(authors, result.ResponseDetails);
        Assert.Equal("Record retrieve successfully.", result.ResponseMsg);
        Assert.Equal(1, result.PaginationDetails.PageNumber);
        Assert.Equal(50, result.PaginationDetails.PageSize);
        Assert.Equal(authors.Count, result.PaginationDetails.TotalRecords);
    }
}
