﻿using Octokit;
using System.Xml.Linq;
using VersionControl.Helpers;
using VersionControl.Models.Exceptions;

namespace VersionControl.Models.Versions
{
    /// <summary>
    /// A class representing the version and metadata retrieved from the GitHub repository
    /// </summary>
    public class GitVersion : Version
    {
        /// <summary>
        /// Release Description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Date and time of publication
        /// </summary>
        public DateTimeOffset? PublishedAt { get; private set; }

        /// <summary>
        /// Materials attached to the release
        /// </summary>
        public IReadOnlyList<ReleaseAsset> Assets { get; private set; }

        /// <summary>
        /// Indicates whether or not this release is a pre-release
        /// </summary>
        public bool IsPrerelease { get; private set; }

        /// <summary>
        /// When an instance of a class is created, it is automatically populated with data from the latest release 
        /// of the specified repository with the specified owner
        /// </summary>
        /// <exception cref="NotConnectedToInternetException">Thrown if there is no internet connection</exception>
        /// <exception cref="ArgumentNullException">Thrown if no repository name or owner is specified</exception>
        /// <exception cref="NotAVersionException">Thrown if no version is specified in the release tag or if the version is not in 0.0.0.0 format</exception>
        public GitVersion() 
        {
            var connected = InternetChecker.Check();
            if (connected == InternetChecker.ConnectionStatus.NotConnected)
                throw new NotConnectedToInternetException();

            var repoName = Parameters.RepositoryName;
            var repoOwner = Parameters.RepositoryOwner;
            if (repoName == null || repoName == string.Empty || repoOwner == null || repoOwner == string.Empty)
                throw new ArgumentNullException();

            var gitClient = new GitHubClient(new ProductHeaderValue(repoName));
            var latestRelease = gitClient.Repository.Release.GetLatest(repoOwner, repoName).Result;

            var stringVersion = latestRelease.TagName;
            var versionParts = stringVersion.Split('.');
            if (versionParts.Length != 4) 
                throw new NotAVersionException();

            Major = Convert.ToByte(versionParts[0]);
            Minor = Convert.ToByte(versionParts[1]);
            Patch = Convert.ToByte(versionParts[2]);
            Revision = Convert.ToByte(versionParts[3]);

            Description = latestRelease.Body;
            PublishedAt = latestRelease.PublishedAt;
            Assets = latestRelease.Assets;
            IsPrerelease = latestRelease.Prerelease;
        }
    }
}
