using System;
using System.Numerics;
using System.Text;

namespace OpenGL
{
	/// <summary>
	/// the methods here are just convenience wrappers for calling the raw gl* method
	/// </summary>
	public static partial class GL
	{
		static readonly uint[] uint1 = new uint[1];
		static readonly int[] int1 = new int[1];
		static readonly float[] matrix4Float = new float[16];

		public static uint GenBuffer()
		{
			uint1[0] = 0;
			glGenBuffers(1, uint1);
			return uint1[0];
		}

		public static void DeleteBuffer(uint buffer)
		{
			uint1[0] = 0;
			glDeleteBuffers(1, uint1);
			uint1[0] = 0;
		}

		public static string GetShaderInfoLog(UInt32 shader)
		{
			glGetShaderiv(shader, ShaderParameter.InfoLogLength, int1);
			if (int1[0] == 0)
				return string.Empty;

			var sb = new StringBuilder(int1[0]);
			glGetShaderInfoLog(shader, sb.Capacity, int1, sb);
			return sb.ToString();
		}

		public static void ShaderSource(uint shader, string source)
		{
			int1[0] = source.Length;
			glShaderSource(shader, 1, new[] { source }, int1);
		}

		public static bool GetShaderCompileStatus(UInt32 shader)
		{
			glGetShaderiv(shader, ShaderParameter.CompileStatus, int1);
			return int1[0] == 1;
		}

		public static string GetProgramInfoLog(UInt32 program)
		{
			glGetProgramiv(program, ProgramParameter.InfoLogLength, int1);
			if (int1[0] == 0)
				return string.Empty;

			var sb = new StringBuilder(int1[0]);
			glGetProgramInfoLog(program, sb.Capacity, int1, sb);
			return sb.ToString();
		}

		public static bool GetProgramLinkStatus(UInt32 program)
		{
			glGetProgramiv(program, ProgramParameter.LinkStatus, int1);
			return int1[0] == 1;
		}

		public static unsafe void UniformMatrix4fv(int location, Matrix4x4 param)
		{
			// use the statically allocated float[] for setting the uniform
			matrix4Float[0] = param.M11; matrix4Float[1] = param.M12; matrix4Float[2] = param.M13; matrix4Float[3] = param.M14;
			matrix4Float[4] = param.M21; matrix4Float[5] = param.M22; matrix4Float[6] = param.M23; matrix4Float[7] = param.M24;
			matrix4Float[8] = param.M31; matrix4Float[9] = param.M32; matrix4Float[10] = param.M33; matrix4Float[11] = param.M34;
			matrix4Float[12] = param.M41; matrix4Float[13] = param.M42; matrix4Float[14] = param.M43; matrix4Float[15] = param.M44;

			glUniformMatrix4fv(location, 1, false, matrix4Float);
		}

		public static void VertexAttribPointer(Int32 index, Int32 size, VertexAttribPointerType type, Boolean normalized, Int32 stride, IntPtr pointer)
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException(nameof(index));
			glVertexAttribPointer((UInt32)index, size, type, normalized, stride, pointer);
		}

		public static void EnableVertexAttribArray(Int32 index)
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException(nameof(index));
			glEnableVertexAttribArray((UInt32)index);
		}

		public static uint GenVertexArray()
		{
			uint1[0] = 0;
			glGenVertexArrays(1, uint1);
			return uint1[0];
		}

		public static void DeleteVertexArray(uint vao)
		{
			uint1[0] = vao;
			glDeleteVertexArrays(1, uint1);
		}

		public static uint GenTexture()
		{
			uint1[0] = 0;
			glGenTextures(1, uint1);
			return uint1[0];
		}

		public static void DeleteTexture(uint texture)
		{
			uint1[0] = texture;
			glDeleteTextures(1, uint1);
		}
	}
}
