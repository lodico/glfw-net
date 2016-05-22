using System.Runtime.InteropServices;

namespace GLFWnet.Binding {
    /// <summary>
    /// Image data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GLFWimage {
        /// <summary>
        /// The width, in pixels, of this image.
        /// </summary>
        public int width;

        /// <summary>
        /// The height, in pixels, of this image.
        /// </summary>
        public int height;

        /// <summary>
        /// The pixel data of this image, arranged left-to-right, top-to-bottom.
        /// </summary>
        [MarshalAs(UnmanagedType.SafeArray)]
        public byte[] pixels;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct InternalGLFWimage {
        internal int width;
        internal int height;
        internal byte* pixels;
    }
}
