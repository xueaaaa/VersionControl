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
    }
}
