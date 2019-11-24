using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Logging;

using System.IO;
using System.Threading;
using XmlMetadata.Parsers;

namespace XmlMetadata.Providers
{
    /// <summary>
    /// Class SeasonProviderFromXml
    /// </summary>
    public class SeasonXmlProvider : BaseXmlProvider<Season>, IHasOrder
    {
        private readonly IProviderManager _providerManager;


        public SeasonXmlProvider(IFileSystem fileSystem, ILogger logger, IProviderManager providerManager)
            : base(fileSystem, logger)
        {
            _providerManager = providerManager;

        }

        protected override void Fetch(MetadataResult<Season> result, string path, CancellationToken cancellationToken)
        {
            new SeasonXmlParser(Logger, _providerManager, FileSystem).Fetch(result, path, cancellationToken);
        }

        protected override FileSystemMetadata GetXmlFile(ItemInfo info, IDirectoryService directoryService)
        {
            return directoryService.GetFile(Path.Combine(info.Path, "season.xml"));
        }

        public override int Order
        {
            get
            {
                // After Xbmc
                return 1;
            }
        }
    }
}
