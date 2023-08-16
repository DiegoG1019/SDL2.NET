namespace SDL2.NET;

public struct RendererInfo
{
    public string Name { get; }
    public RendererFlags Flags { get; }
    public TextureFormatCollection TextureFormats { get; }
    public Size MaxTextureSize { get; }

    public RendererInfo(string name, RendererFlags flags, Size maxTextureSize, TextureFormatCollection formats)
    {
        Name = name;
        Flags = flags;
        TextureFormats = formats;
        MaxTextureSize = maxTextureSize;
    }

    /// <summary>
    /// A collection backed by a pre-defined number of fields. In SDL, texture_formats is a fixed-size array of 16 members.
    /// </summary>
    /// <remarks>
    /// Use <see cref="Count"/> to enumerate this collection
    /// </remarks>
    public struct TextureFormatCollection
    {
        public uint Count { get; }

        public uint this[uint index]
            => index > 15
                ? throw new IndexOutOfRangeException("TextureFormatCollection never has more than 16 members. Valid indices are 0-15")
                : index > Count
                ? throw new IndexOutOfRangeException($"This TextureFormatCollection only has {Count} members")
                : index switch
                {
                    0 => f0,
                    1 => f1,
                    2 => f2,
                    3 => f3,
                    4 => f4,
                    5 => f5,
                    6 => f6,
                    7 => f7,
                    8 => f8,
                    9 => f9,
                    10 => f10,
                    11 => f11,
                    12 => f12,
                    13 => f13,
                    14 => f14,
                    15 => f15,
                    _ => throw new IndexOutOfRangeException("TextureFormatCollection never has more than 16 members. Valid indices are 0-15")
                };

        public unsafe TextureFormatCollection(uint* formats, uint count)
        {
            Count = count;
            f0 = count > 0 ? formats[0] : 0;
            f1 = count > 1 ? formats[1] : 0;
            f2 = count > 2 ? formats[2] : 0;
            f3 = count > 3 ? formats[3] : 0;
            f4 = count > 4 ? formats[4] : 0;
            f5 = count > 5 ? formats[5] : 0;
            f6 = count > 6 ? formats[6] : 0;
            f7 = count > 7 ? formats[7] : 0;
            f8 = count > 8 ? formats[8] : 0;
            f9 = count > 9 ? formats[9] : 0;
            f10 = count > 10 ? formats[10] : 0;
            f11 = count > 11 ? formats[11] : 0;
            f12 = count > 12 ? formats[12] : 0;
            f13 = count > 13 ? formats[13] : 0;
            f14 = count > 14 ? formats[14] : 0;
            f15 = count > 15 ? formats[15] : 0;
        }

        private readonly uint f0;
        private readonly uint f1;
        private readonly uint f2;
        private readonly uint f3;
        private readonly uint f4;
        private readonly uint f5;
        private readonly uint f6;
        private readonly uint f7;
        private readonly uint f8;
        private readonly uint f9;
        private readonly uint f10;
        private readonly uint f11;
        private readonly uint f12;
        private readonly uint f13;
        private readonly uint f14;
        private readonly uint f15;
    }
}
