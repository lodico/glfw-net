using System.Runtime.InteropServices;

namespace GLFWnet.Binding
{
    /// <summary>
    /// Video mode type.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 6)]
    public struct GLFWvidmode
    {
        /// <summary>
        ///  The width, in screen coordinates, of the video mode.
        /// </summary>
        [FieldOffset(sizeof(int) * 0)]
        public int width;

        /// <summary>
        ///  The height, in screen coordinates, of the video mode.
        /// </summary>
        [FieldOffset(sizeof(int) * 1)]
        public int height;

        /// <summary>
        ///  The bit depth of the red channel of the video mode.
        /// </summary>
        [FieldOffset(sizeof(int) * 2)]
        public int redBits;

        /// <summary>
        ///  The bit depth of the green channel of the video mode.
        /// </summary>
        [FieldOffset(sizeof(int) * 3)]
        public int greenBits;

        /// <summary>
        ///  The bit depth of the blue channel of the video mode.
        /// </summary>
        [FieldOffset(sizeof(int) * 4)]
        public int blueBits;

        /// <summary>
        ///  The refresh rate, in Hz, of the video mode.
        /// </summary>
        [FieldOffset(sizeof(int) * 5)]
        public int refreshRate;
    }
}
