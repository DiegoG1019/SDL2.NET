using System;
using System.Runtime.InteropServices;
using System.Text;
using static SDL2.Bindings.SDL;

namespace OpenGL
{
	public static partial class GL
	{
		static T getDelegate<T>() where T : Delegate
        {
            var method = $"gl{typeof(T).Name[..^3]}";
            var ptr = SDL_GL_GetProcAddress(method);
            return ptr == IntPtr.Zero
                ? throw new Exception($"nogo: {method} from {typeof(T).Name}")
                : (T)Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
        }

        /// <summary>
        /// Alternate delegate fetcher for when our delegate Type ends in "Del". These happen when the method needs a wrapper
        /// in GL.Utils.
        /// </summary>
        static T _Del<T>() where T : Delegate
        {
            var method = $"gl{typeof(T).Name[..^3]}";
            var ptr = SDL_GL_GetProcAddress(method);
            return ptr == IntPtr.Zero
                ? throw new Exception($"nogo: {method} from {typeof(T).Name}")
                : (T)Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
		delegate IntPtr GetString(StringName pname);
		static GetString _GetString = getDelegate<GetString>();
		public static unsafe string glGetString(StringName pname) => new string((sbyte*)_GetString(pname));

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GenBuffers(int n, [Out] uint[] buffers);
		public static GenBuffers glGenBuffers = getDelegate<GenBuffers>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void DeleteBuffers(Int32 n, UInt32[] buffers);
		public static DeleteBuffers glDeleteBuffers = getDelegate<DeleteBuffers>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Viewport(int x, int y, int width, int height);
		public static Viewport glViewport = getDelegate<Viewport>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void ClearColor(float r, float g, float b, float a);
		public static ClearColor glClearColor = getDelegate<ClearColor>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Clear(ClearBufferMask mask);
		public static Clear glClear = getDelegate<Clear>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Enable(EnableCap cap);
		public static Enable glEnable = getDelegate<Enable>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Disable(EnableCap cap);
		public static Disable glDisable = getDelegate<Disable>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void BlendEquation(BlendEquationMode mode);
		public static BlendEquation glBlendEquation = getDelegate<BlendEquation>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void BlendFunc(BlendingFactorSrc sfactor, BlendingFactorDest dfactor);
		public static BlendFunc glBlendFunc = getDelegate<BlendFunc>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void UseProgram(uint program);
		public static UseProgram glUseProgram = getDelegate<UseProgram>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GetShaderiv(UInt32 shader, ShaderParameter pname, [Out] int[] @params);
		public static GetShaderiv glGetShaderiv = getDelegate<GetShaderiv>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GetShaderInfoLogDel(UInt32 shader, Int32 maxLength, [Out] Int32[] length, [Out] StringBuilder infoLog);
		public static GetShaderInfoLogDel glGetShaderInfoLog = _Del<GetShaderInfoLogDel>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate uint CreateShader(ShaderType shaderType);
		public static CreateShader glCreateShader = getDelegate<CreateShader>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void ShaderSourceDel(UInt32 shader, Int32 count, String[] @string, Int32[] length);
		public static ShaderSourceDel glShaderSource = _Del<ShaderSourceDel>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void CompileShader(UInt32 shader);
		public static CompileShader glCompileShader = getDelegate<CompileShader>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void DeleteShader(UInt32 shader);
		public static DeleteShader glDeleteShader = getDelegate<DeleteShader>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GetProgramiv(UInt32 program, ProgramParameter pname, [Out] Int32[] @params);
		public static GetProgramiv glGetProgramiv = getDelegate<GetProgramiv>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GetProgramInfoLogDel(uint program, Int32 maxLength, [OutAttribute] Int32[] length, [OutAttribute] StringBuilder infoLog);
		public static GetProgramInfoLogDel glGetProgramInfoLog = _Del<GetProgramInfoLogDel>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate UInt32 CreateProgram();
		public static CreateProgram glCreateProgram = getDelegate<CreateProgram>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void AttachShader(UInt32 program, UInt32 shader);
		public static AttachShader glAttachShader = getDelegate<AttachShader>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void LinkProgram(UInt32 program);
		public static LinkProgram glLinkProgram = getDelegate<LinkProgram>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate Int32 GetUniformLocation(UInt32 program, String name);
		public static GetUniformLocation glGetUniformLocation = getDelegate<GetUniformLocation>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate Int32 GetAttribLocation(UInt32 program, String name);
		public static GetAttribLocation glGetAttribLocation = getDelegate<GetAttribLocation>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void DetachShader(UInt32 program, UInt32 shader);
		public static DetachShader glDetachShader = getDelegate<DetachShader>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void DeleteProgram(UInt32 program);
		public static DeleteProgram glDeleteProgram = getDelegate<DeleteProgram>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GetActiveAttrib(UInt32 program, UInt32 index, Int32 bufSize, [Out] Int32[] length, [Out] Int32[] size, [Out] ActiveAttribType[] type, [Out] StringBuilder name);
		public static GetActiveAttrib glGetActiveAttrib = getDelegate<GetActiveAttrib>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GetActiveUniform(UInt32 program, UInt32 index, Int32 bufSize, [OutAttribute] Int32[] length, [OutAttribute] Int32[] size, [OutAttribute] ActiveUniformType[] type, [OutAttribute] StringBuilder name);
		public static GetActiveUniform glGetActiveUniform = getDelegate<GetActiveUniform>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Uniform1f(Int32 location, Single v0);
		public static Uniform1f glUniform1f = getDelegate<Uniform1f>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Uniform2f(Int32 location, Single v0, Single v1);
		public static Uniform2f glUniform2f = getDelegate<Uniform2f>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Uniform3f(Int32 location, Single v0, Single v1, Single v2);
		public static Uniform3f glUniform3f = getDelegate<Uniform3f>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Uniform4f(Int32 location, Single v0, Single v1, Single v2, Single v3);
		public static Uniform4f glUniform4f = getDelegate<Uniform4f>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Uniform1i(Int32 location, Int32 v0);
		public static Uniform1i glUniform1i = getDelegate<Uniform1i>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Uniform3fv(Int32 location, Int32 count, Single[] value);
		public static Uniform3fv glUniform3fv = getDelegate<Uniform3fv>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Uniform4fv(Int32 location, Int32 count, Single[] value);
		public static Uniform4fv glUniform4fv = getDelegate<Uniform4fv>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void UniformMatrix3fvDel(Int32 location, Int32 count, Boolean transpose, Single[] value);
		public static UniformMatrix3fvDel glUniformMatrix3fv = _Del<UniformMatrix3fvDel>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void UniformMatrix4fvDel(Int32 location, Int32 count, Boolean transpose, Single[] value);
		public static UniformMatrix4fvDel glUniformMatrix4fv = _Del<UniformMatrix4fvDel>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void BindSampler(UInt32 unit, UInt32 sampler);
		public static BindSampler glBindSampler = getDelegate<BindSampler>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void BindVertexArray(UInt32 array);
		public static BindVertexArray glBindVertexArray = getDelegate<BindVertexArray>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void BindBuffer(BufferTarget target, UInt32 buffer);
		public static BindBuffer glBindBuffer = getDelegate<BindBuffer>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void EnableVertexAttribArrayDel(UInt32 index);
		public static EnableVertexAttribArrayDel glEnableVertexAttribArray = _Del<EnableVertexAttribArrayDel>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void DisableVertexAttribArray(UInt32 index);
		public static DisableVertexAttribArray glDisableVertexAttribArray = getDelegate<DisableVertexAttribArray>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void VertexAttribPointerDel(UInt32 index, Int32 size, VertexAttribPointerType type, Boolean normalized, Int32 stride, IntPtr pointer);
		public static VertexAttribPointerDel glVertexAttribPointer = _Del<VertexAttribPointerDel>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void BindTexture(TextureTarget target, UInt32 texture);
		public static BindTexture glBindTexture = getDelegate<BindTexture>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void BufferData(BufferTarget target, IntPtr size, IntPtr data, BufferUsageHint usage);
		public static BufferData glBufferData = getDelegate<BufferData>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void Scissor(Int32 x, Int32 y, Int32 width, Int32 height);
		public static Scissor glScissor = getDelegate<Scissor>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void DrawElementsBaseVertex(BeginMode mode, Int32 count, DrawElementsType type, IntPtr indices, Int32 basevertex);
		public static DrawElementsBaseVertex glDrawElementsBaseVertex = getDelegate<DrawElementsBaseVertex>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void DeleteVertexArrays(Int32 n, UInt32[] arrays);
		public static DeleteVertexArrays glDeleteVertexArrays = getDelegate<DeleteVertexArrays>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GenVertexArrays(Int32 n, [Out] UInt32[] arrays);
		public static GenVertexArrays glGenVertexArrays = getDelegate<GenVertexArrays>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void GenTextures(Int32 n, [OutAttribute] UInt32[] textures);
		public static GenTextures glGenTextures = getDelegate<GenTextures>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void PixelStorei(PixelStoreParameter pname, Int32 param);
		public static PixelStorei glPixelStorei = getDelegate<PixelStorei>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void TexImage2D(TextureTarget target, Int32 level, PixelInternalFormat internalFormat, Int32 width, Int32 height, Int32 border, PixelFormat format, PixelType type, IntPtr data);
		public static TexImage2D glTexImage2D = getDelegate<TexImage2D>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void TexParameteri(TextureTarget target, TextureParameterName pname, TextureParameter param);
		public static TexParameteri glTexParameteri = getDelegate<TexParameteri>();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void DeleteTextures(Int32 n, UInt32[] textures);
		public static DeleteTextures glDeleteTextures = getDelegate<DeleteTextures>();
	}
}
