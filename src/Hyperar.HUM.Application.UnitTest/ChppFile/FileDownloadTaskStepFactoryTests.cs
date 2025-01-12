namespace Hyperar.HUM.Application.UnitTest.ChppFile
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.UnitTest.ChppFile.Fixtures;
    using Hyperar.HUM.Shared.Enums;
    using Microsoft.Extensions.DependencyInjection;

    public class FileDownloadTaskStepFactoryTests : IClassFixture<FileDownloadTaskStepFactoryFixture>
    {
        private readonly IServiceProvider services;

        public FileDownloadTaskStepFactoryTests(FileDownloadTaskStepFactoryFixture fixture)
        {
            this.services = fixture.Services;
        }

        [Fact]
        public void FileDownloadTaskStepFactory_CanceledTaskShouldThrowException()
        {
            var stepFactory = this.services.GetRequiredService<IFileDownloadTaskStepFactory>();

            Assert.Throws<ArgumentOutOfRangeException>(() => stepFactory.GetNextStep(DownloadTaskStatus.Canceled));
        }

        [Fact]
        public void FileDownloadTaskStepFactory_ErroredTaskShouldThrowException()
        {
            var stepFactory = this.services.GetRequiredService<IFileDownloadTaskStepFactory>();

            Assert.Throws<ArgumentOutOfRangeException>(() => stepFactory.GetNextStep(DownloadTaskStatus.Canceled));
        }

        [Fact]
        public void FileDownloadTaskStepFactory_FinishedTaskShouldThrowException()
        {
            var stepFactory = this.services.GetRequiredService<IFileDownloadTaskStepFactory>();

            Assert.Throws<ArgumentOutOfRangeException>(() => stepFactory.GetNextStep(DownloadTaskStatus.Canceled));
        }

        [Fact]
        public void FileDownloadTaskStepFactory_ShouldBeDownloaderStep()
        {
            var stepFactory = this.services.GetRequiredService<IFileDownloadTaskStepFactory>();

            var step = stepFactory.GetNextStep(DownloadTaskStatus.Pending);

            Assert.IsType<FileDownloadTaskDownloader>(step);
        }

        [Fact]
        public void FileDownloadTaskStepFactory_ShouldBeExtractorStep()
        {
            var stepFactory = this.services.GetRequiredService<IFileDownloadTaskStepFactory>();

            var step = stepFactory.GetNextStep(DownloadTaskStatus.Read);

            Assert.IsType<FileDownloadTaskExtractor>(step);
        }

        [Fact]
        public void FileDownloadTaskStepFactory_ShouldBeParserStep()
        {
            var stepFactory = this.services.GetRequiredService<IFileDownloadTaskStepFactory>();

            var step = stepFactory.GetNextStep(DownloadTaskStatus.Downloaded);

            Assert.IsType<FileDownloadTaskParser>(step);
        }

        [Fact]
        public void FileDownloadTaskStepFactory_ShouldBePersisterStep()
        {
            var stepFactory = this.services.GetRequiredService<IFileDownloadTaskStepFactory>();

            var step = stepFactory.GetNextStep(DownloadTaskStatus.Processed);

            Assert.IsType<FileDownloadTaskPersister>(step);
        }
    }
}