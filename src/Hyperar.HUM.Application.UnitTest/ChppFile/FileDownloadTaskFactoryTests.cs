namespace Hyperar.HUM.Application.UnitTest.ChppFile
{
    using System;
    using System.Collections.Specialized;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;

    public class FileDownloadTaskFactoryTests : IClassFixture<ServicesFixture>
    {
        private readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        public FileDownloadTaskFactoryTests(ServicesFixture fixture)
        {
            this.fileDownloadTaskFactory = fixture.Services.GetRequiredService<IFileDownloadTaskFactory>();
        }

        [Fact]
        public void FileDownloadTaskFactoryImageFileDownloadTask_ShouldBeEqual()
        {
            var task = this.fileDownloadTaskFactory.BuildImageFileDownloadTask("/images/file.png");

            Assert.NotNull(task);
            Assert.NotEqual(Guid.Empty, task.Id);
            Assert.Null(task.ErrorMessage);
            Assert.Equal(FileType.ImageFile, task.FileType);
            Assert.Null(task.ImageFileBytes);
            Assert.Equal("/images/file.png", task.Title);
            Assert.Equal("/images/file.png", task.Url);
        }

        [Fact]
        public void FileDownloadTaskFactoryXmlFileDownloadTask_ShouldBeEqual()
        {
            var userProfileId = Guid.NewGuid();

            var task = this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(
                userProfileId,
                XmlFileType.ArenaDetails,
                new NameValueCollection { { "teamId", "123456" } });

            Assert.NotNull(task);
            Assert.NotEqual(Guid.Empty, task.Id);
            Assert.Equal(FileType.XmlFile, task.FileType);
            Assert.Null(task.Entity);
            Assert.Null(task.ErrorMessage);
            Assert.Null(task.FileContent);
            Assert.NotNull(task.Parameters);
            Assert.Equal(DownloadTaskStatus.Pending, task.Status);
            Assert.Equal(Globalization.Enums.XmlFileType_ArenaDetails, task.Title);
            Assert.Equal(userProfileId, task.UserProfileId);
            Assert.Equal(XmlFileType.ArenaDetails, task.XmlFile);
        }
    }
}