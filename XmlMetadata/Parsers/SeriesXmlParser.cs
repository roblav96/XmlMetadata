﻿using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Logging;

using System;
using System.Xml;

namespace XmlMetadata.Parsers
{
    /// <summary>
    /// Class SeriesXmlParser
    /// </summary>
    public class SeriesXmlParser : BaseItemXmlParser<Series>
    {
        /// <summary>
        /// Fetches the data from XML node.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="result">The result.</param>
        protected override void FetchDataFromXmlNode(XmlReader reader, MetadataResult<Series> result)
        {
            var item = result.Item;

            switch (reader.Name)
            {
                case "Series":
                    //MB generated metadata is within a "Series" node
                    using (var subTree = reader.ReadSubtree())
                    {
                        subTree.MoveToContent();

                        // Loop through each element
                        while (subTree.Read())
                        {
                            if (subTree.NodeType == XmlNodeType.Element)
                            {
                                FetchDataFromXmlNode(subTree, result);
                            }
                        }

                    }
                    break;

                case "id":
                    string id = reader.ReadElementContentAsString();
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        item.SetProviderId(MetadataProviders.Tvdb, id);
                    }
                    break;

                case "DisplayOrder":

                    {
                        var val = reader.ReadElementContentAsString();

                        if (Enum.TryParse(val, true, out SeriesDisplayOrder seriesDisplayOrder))
                        {
                            item.DisplayOrder = seriesDisplayOrder;
                        }
                    }
                    break;

                case "Status":
                    {
                        var status = reader.ReadElementContentAsString();

                        if (!string.IsNullOrWhiteSpace(status))
                        {
                            SeriesStatus seriesStatus;
                            if (Enum.TryParse(status, true, out seriesStatus))
                            {
                                item.Status = seriesStatus;
                            }
                            else
                            {
                                Logger.Info("Unrecognized series status: " + status);
                            }
                        }

                        break;
                    }

                default:
                    base.FetchDataFromXmlNode(reader, result);
                    break;
            }
        }

        public SeriesXmlParser(ILogger logger, IProviderManager providerManager, IFileSystem fileSystem) : base(logger, providerManager, fileSystem)
        {
        }
    }
}
