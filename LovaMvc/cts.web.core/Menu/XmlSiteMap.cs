using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace cts.web.core.Menu
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlSiteMap
    {
        /// <summary>
        /// 
        /// </summary>
        public XmlSiteMap()
        {
            SiteMapNodes = new List<SiteMapNode>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<SiteMapNode> SiteMapNodes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public virtual void LoadFrom(string filePath)
        {
            string content = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(content))
            {
                using (var sr = new StringReader(content))
                {
                    using (var xr = XmlReader.Create(sr,
                            new XmlReaderSettings
                            {
                                CloseInput = true,
                                IgnoreWhitespace = true,
                                IgnoreComments = true,
                                IgnoreProcessingInstructions = true
                            }))
                    {
                        var doc = new XmlDocument();
                        doc.Load(xr);

                        if ((doc.DocumentElement != null) && doc.HasChildNodes)
                        {
                            int sort = 0;
                            foreach (XmlNode xmlRootNode in doc.DocumentElement)
                            {
                                sort++;
                                SiteMapNode siteMapNode = new SiteMapNode() { Sort = sort, FatherCode = "",IsMenu="",RouteTemplate="", Target="0" };
                                Iterate(siteMapNode, xmlRootNode);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteMapNode"></param>
        /// <param name="xmlNode"></param>
        private void Iterate(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            PopulateNode(siteMapNode, xmlNode);
            SiteMapNodes.Add(siteMapNode);
            int sort = 0;
            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                sort++;
                if (xmlChildNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new SiteMapNode();
                    siteMapChildNode.Sort = sort;
                    siteMapChildNode.FatherCode = siteMapNode.Code;
                    Iterate(siteMapChildNode, xmlChildNode);
                }
            }
        }

        /// <summary>
        /// 装载数据
        /// </summary>
        /// <param name="siteMapNode"></param>
        /// <param name="xmlNode"></param>
        private void PopulateNode(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            siteMapNode.Name = GetStringValueFromAttribute(xmlNode, "Name");
            siteMapNode.RouteTemplate = GetStringValueFromAttribute(xmlNode, "RouteTemplate");
            siteMapNode.UID = GetStringValueFromAttribute(xmlNode, "UID");
            siteMapNode.IsMenu = GetStringValueFromAttribute(xmlNode, "IsMenu");
            siteMapNode.Target = GetStringValueFromAttribute(xmlNode, "Target");
            siteMapNode.IconClass = GetStringValueFromAttribute(xmlNode, "IconClass");
            siteMapNode.Action = GetStringValueFromAttribute(xmlNode, "Action");
            siteMapNode.Controller = GetStringValueFromAttribute(xmlNode, "Controller");
            siteMapNode.RouteName = GetStringValueFromAttribute(xmlNode, "RouteName");
        }

        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        private string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                XmlAttribute attribute = node.Attributes[attributeName];
                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }
            return value;
        }

    }
}
