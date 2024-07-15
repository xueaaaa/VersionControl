#pragma warning disable SYSLIB0014
using System.Net;
using VersionControl.Models.Versions;
using Version = VersionControl.Models.Versions.Version;

namespace VersionControl.Models.Installers
{
    public class VersionInstaller
    {
        private Version _local;
        private GitVersion _current;

        public WebClient WebClient { get; private set; }

        public VersionInstaller(Version local, GitVersion git)
        {
            _local = local;
            _current = git;
            WebClient = new();
        }

        /// <summary>
        /// Checks if the version in the repository with the installed version has changed
        /// </summary>
        /// <returns>
        /// True - if the repository version is newer than the local one
        /// <para></para>
        /// False - if not
        /// </returns>
        public bool Check() =>
            _current > _local;

        /// <summary>
        /// Downloads the update file to the path specified in the parameters.
        /// <para></para>
        /// <b>Note that this method does not install this update, it only downloads it.</b>
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if no update file name or path is specified</exception>
        public void Install()
        {
            var updFileName = Parameters.UpdateFileName;
            var updFilePath = Parameters.UpdateFilePath;
            if (updFileName == null || updFileName == string.Empty || updFilePath == null || updFilePath == string.Empty)
                throw new ArgumentNullException();

            WebClient.DownloadFileAsync(new Uri(_current.Assets
                .Where(v => v.Name == updFileName)
                .First()
                .BrowserDownloadUrl), updFilePath);
        }
    }
}
#pragma warning restore SYSLIB0014