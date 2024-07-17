using System;
using static OpenGL.GL;

namespace SDL2.Bindings.OpenGL.Constructs
{
    public sealed class GLShader : IDisposable
    {
        // Specifies the OpenGL ShaderID.
        public uint ShaderID { get; private set; }

        // Specifies the type of shader.
        public ShaderType ShaderType { get; private set; }

        // Returns Gl.GetShaderInfoLog(ShaderID), which contains any compilation errors.
        public string ShaderLog => GetShaderInfoLog(ShaderID);

        ~GLShader() => Dispose(false);

        public GLShader(string source, ShaderType type)
        {
            ShaderType = type;
            ShaderID = glCreateShader(type);

            ShaderSource(ShaderID, source);
            glCompileShader(ShaderID);

            if (!GetShaderCompileStatus(ShaderID))
            {
                throw new Exception(ShaderLog);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (ShaderID != 0)
            {
                glDeleteShader(ShaderID);
                ShaderID = 0;
            }
        }
    }
}
