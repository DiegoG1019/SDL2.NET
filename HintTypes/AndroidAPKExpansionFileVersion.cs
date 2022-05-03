using static SDL2.SDL;

namespace SDL2.NET.HintTypes;

#warning come back for SDL_RWFromFile
/// <summary>
/// A hint that specifies the Android APK expansion file version. <see cref="SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION" href="https://wiki.libsdl.org/SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION"/>; <see cref="SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION" href="https://wiki.libsdl.org/SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION"/>
/// </summary>
/// <remarks>
/// If both hints were set then SDL_RWFromFile() will look into expansion files after a given relative path was not found in the internal storage and assets. By default this hint is not set and the APK expansion files are not searched.
/// </remarks>
public sealed class AndroidAPKExpansionFileVersion
{
    internal AndroidAPKExpansionFileVersion() { }

    /// <summary>
    /// Sets the hint for the Main File. For this to work, both versions must be set.
    /// </summary>
    public VersionHint MainFileVersion { get; } = new(SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION);

    /// <summary>
    /// Sets the hint for the Patch File. For this to work, both versions must be set.
    /// </summary>
    public VersionHint PatchFileVersion { get; } = new(SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION);

    public sealed class VersionHint : Hint
    {
        internal VersionHint(string name) : base(name) { }

        public void SetPriority(int version, HintPriority priority)
            => Set(version.ToString());

        public int Version
        {
            get => int.Parse(Get() ?? "-1");
            set => Set(value.ToString());
        }
    }
}

//public delegate void HintCallback();
