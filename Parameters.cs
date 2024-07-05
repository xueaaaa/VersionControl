namespace VersionControl
{
    /// <summary>
    /// This static class is used to configure the version control system and install updates.
    /// Make sure you fill in all its required fields before you start working with the system itself.
    /// </summary>
    public static class Parameters
    {
        /// <summary>
        /// Name of your program repository
        /// </summary>
        public static string RepositoryName { get; set; }

        /// <summary>
        /// Repository owner account name
        /// </summary>
        public static string RepositoryOwner { get; set; }

        /// <summary>
        /// The name of the file in the asset that is the update archive for older versions. 
        /// <para></para>
        /// <b>It is strongly recommended not to change this name from update to update.</b>
        /// </summary>
        public static string UpdateFileName { get; set; }

        /// <summary>
        /// The path to download the update file, including the file name and extension. 
        /// Automatically generated when Set() is called, but can be changed manually if desired.
        /// </summary>
        public static string UpdateFilePath { get; set; }

        /// <summary>
        /// Sets all the parameters required for the version control system to work. 
        /// <para></para>
        /// <b>Be sure to call this method before you start working with other library components.</b>
        /// </summary>
        public static void Set(string repoName, string repoOwner, string updateFileName, string updateFilePath = "")
        {
            RepositoryName = repoName;
            RepositoryOwner = repoOwner;
            UpdateFileName = updateFileName;
            UpdateFilePath = updateFilePath == string.Empty 
                ? $"{Directory.GetCurrentDirectory}/{UpdateFileName}" 
                : updateFilePath;
        }
    }
}
