using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDL2.NET;

public struct RendererInfo
{
	public string Name { get; }
	public RendererFlags Flags { get; }
	public TextureFormatCollection TextureFormats { get; }
	public Size MaxTextureSize { get; }

	/// <summary>
	/// A collection backed by a pre-defined number of fields. In SDL, texture_formats is a fixed-size array of 16 members.
	/// </summary>
	/// <remarks>
	/// Use <see cref="RendererInfo.TextureFormatCount"/> to enumerate this collection
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
