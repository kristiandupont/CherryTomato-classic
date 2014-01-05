using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.VersionCheck
{
    public class VersionChecker : IPlugin
    {
        public Version CurrentVersion;

        private const string defaultVersionHistoryUrl = "http://www.beatpoints.com/cherrytomato/versionhistory.xml";
        private string xmlData;

        private readonly List<Version> versions = new List<Version>();

        public VersionChecker()
            : this(new Version(Application.ProductVersion))
        {
        }

        public VersionChecker(string xmlData, Version currentVersion) 
            : this(currentVersion)
        {
            this.xmlData = xmlData;
            this.CollectVersions();
        }

        private VersionChecker(Version currentVersion)
        {
            this.CurrentVersion = currentVersion;
        }

        private void CollectVersions()
        {
            if (this.xmlData == null)
            {
                this.xmlData = Helpers.DownloadString(defaultVersionHistoryUrl);

                // this.xmlData can be null in case downloading didn't succeed.
                if (this.xmlData != null)
                {
                    this.ParseVersionsXml();
                }
            }
        }

        private void ParseVersionsXml()
        {
            this.versions.Clear();

            var xd = new XmlDocument();
            xd.LoadXml(this.xmlData);

            var versionNodes = xd.SelectNodes("//cherrytomatoVersionHistory/version");
            foreach (XmlElement versionNode in versionNodes)
            {
                var major = int.Parse(versionNode.GetAttribute("major"));
                var minor = int.Parse(versionNode.GetAttribute("minor"));
                var v = new Version(major, minor);
                this.versions.Add(v);
            }
        }

        public Version GetNewestVersion()
        {
            if (this.versions.Count == 0)
            {
                this.CollectVersions();
            }

            Version result = this.versions.FirstOrDefault();

            foreach (var v in versions)
            {
                if (v > result)
                    result = v;
            }

            return result;
        }

        public Version CheckNewestVersion(Version newestKnownVersion)
        {
            var newestVersion = this.GetNewestVersion();

            if (newestVersion != null && newestVersion > newestKnownVersion)
            {
                var nvad = new NewVersionAvailableForm(newestVersion);
                var dummy = nvad.Handle;    // Calls CreateHandle
                nvad.BeginInvoke(new MethodInvoker(nvad.Show));
                return newestVersion;
            }

            return newestKnownVersion;
        }

        public string PluginName
        {
            get { return "Version Checker"; }
        }

        private void StartCheck()
        {
            new Thread(() =>
            {
                this.NewestKnownVersion = this.CheckNewestVersion(this.NewestKnownVersion);
            }).Start();
        }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe("Application Started", this.StartCheck);
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Configuration Event",
                ea => this.LoadConfig(ea as ConfigureEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Configuration Event",
                ea => this.SaveConfig(ea as ConfigureEventArgs)));
        }

        public Version NewestKnownVersion = new Version(Application.ProductVersion);

        private void LoadConfig(ConfigureEventArgs cea)
        {
            var newestKnownVersionNode = (XmlElement)cea.RootNode.SelectSingleNode("newestKnownVersion");
            if (newestKnownVersionNode != null)
            {
                var minor = int.Parse(newestKnownVersionNode.GetAttribute("minor"));
                var major = int.Parse(newestKnownVersionNode.GetAttribute("major"));
                this.NewestKnownVersion = new Version(major, minor);
            }
        }

        private void SaveConfig(ConfigureEventArgs cea)
        {
            var rootNode = cea.RootNode;
            var newestKnownVersionNode = cea.Document.CreateElement("newestKnownVersion");
            newestKnownVersionNode.SetAttribute("major", this.NewestKnownVersion.Major.ToString());
            newestKnownVersionNode.SetAttribute("minor", this.NewestKnownVersion.Minor.ToString());
            rootNode.AppendChild(newestKnownVersionNode);
        }
    }
}
