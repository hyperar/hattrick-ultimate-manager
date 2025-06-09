namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Models.Chpp;

    public abstract class XmlFileExtractorBase
    {
        protected readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        protected XmlFileExtractorBase(IFileDownloadTaskFactory fileDownloadTaskFactory)
        {
            this.fileDownloadTaskFactory = fileDownloadTaskFactory;
        }

        protected Task<IEnumerable<ImageFileDownloadTask>> ExtractAvatarTasksAsync(Avatar avatar)
        {
            var tasks = new List<ImageFileDownloadTask>();

            if (!ImageHelper.ImageFileExists(avatar.BackgroundImage))
            {
                this.fileDownloadTaskFactory.BuildImageFileDownloadTask(
                    avatar.BackgroundImage);
            }

            if (avatar.Layers is not null)
            {
                tasks.AddRange(
                    avatar.Layers.Select(x => x.Image)
                        .Where(x => !ImageHelper.ImageFileExists(x))
                        .Select(this.fileDownloadTaskFactory.BuildImageFileDownloadTask));
            }

            return Task.FromResult(tasks.AsEnumerable());
        }
    }
}