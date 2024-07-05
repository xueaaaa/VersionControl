using System.Reflection;
using VersionControl.Models.Exceptions;

namespace VersionControl.Models.Versions
{
    /// <summary>
    /// A class representing the base model of format version 0.0.0.0
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Program version recorded in Assembly
        /// </summary>
        /// <exception cref="InvalidAssemblyException">Thrown when the project Assembly cannot be accessed.</exception>
        /// <exception cref="NotAVersionException">Thrown when no version is found within Assembly or when it is not in supported format 0.0.0.0</exception>
        public static Version Local
        {
            get
            {
                string? version = Assembly.GetExecutingAssembly()
                    ?.GetName()
                    ?.Version
                    ?.ToString();
                if (version == null)
                    throw new InvalidAssemblyException();

                var versionParts = version.Split('.');
                if (versionParts.Length != 4) throw new NotAVersionException();

                return new Version(Convert.ToByte(versionParts[0]),
                    Convert.ToByte(versionParts[1]),
                    Convert.ToByte(versionParts[2]),
                    Convert.ToByte(versionParts[3]));
            }
        }

        /// <summary>
        /// First digit in the sequence (x.0.0.0)
        /// </summary>
        public byte Major;
        /// <summary>
        /// Second digit in the sequence (0.x.0.0)
        /// </summary>
        public byte Minor;
        /// <summary>
        /// Third digit in the sequence (0.0.x.0)
        /// </summary>
        public byte Patch;
        /// <summary>
        /// Fourth digit in the sequence (0.0.0.x)
        /// </summary>
        public byte Revision;

        /// <summary>
        /// Version as string. It's a shortcut for ToString(), both parameter and function return the same value
        /// </summary>
        public string String 
        { 
            get => ToString(); 
        }

        public Version(byte major = 0, byte minor = 0, byte patch = 0, byte revision = 0)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Revision = revision;
        }

        /// <summary>
        /// Compares two versions
        /// </summary>
        /// <param name="other">The version to which the current instance of the class is compared to</param>
        /// <returns>Returns a value less than zero if the current version is less than the second, 
        /// 0 if equal, greater than zero if the current version is greater than the second version</returns>
        public int CompareTo(Version other)
        {
            if (Major != other.Major)
                return Major.CompareTo(other.Major);

            if (Minor != other.Minor)
                return Minor.CompareTo(other.Minor);

            if (Patch != other.Patch)
                return Patch.CompareTo(other.Patch);

            return Revision.CompareTo(other.Revision);
        }

        public static bool operator >(Version version1, Version version2) =>
            version1.CompareTo(version2) > 0;

        public static bool operator <(Version version1, Version version2) => 
            version1.CompareTo(version2) < 0;

        public override string ToString() =>
            $"{Major}.{Minor}.{Patch}.{Revision}";
    }
}
