using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Logging;

using System;
using System.Xml;
using System.Globalization;

namespace XmlMetadata.Parsers
{
    /// <summary>
    /// Class SeasonXmlParser
    /// </summary>
    public class SeasonXmlParser : BaseItemXmlParser<Season>
    {
        /// <summary>
        /// Fetches the data from XML node.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="result">The result.</param>
        protected override void FetchDataFromXmlNode(XmlReader reader, MetadataResult<Season> result)
        {
            var item = result.Item;

            switch (reader.Name)
            {

                case "SeasonNumber":
                    {
                        var number = reader.ReadElementContentAsString();
                        if (!string.IsNullOrWhiteSpace(number))
                        {
                            if (int.TryParse(number, NumberStyles.Integer, CultureInfo.InvariantCulture, out int num))
                            {
                                item.IndexNumber = num;
                            }
                        }
                        break;
                    }

                default:
                    base.FetchDataFromXmlNode(reader, result);
                    break;
            }
        }

        public SeasonXmlParser(ILogger logger, IProviderManager providerManager, IFileSystem fileSystem) : base(logger, providerManager, fileSystem)
        {
        }
    }
}
