using System.Runtime.InteropServices;

namespace GLFW {
    /// <summary>
    /// Monitor gamma ramp.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GLFWgammaramp {
        /// <summary>
        /// An array of value describing the response of the red channel.
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray)]
        public ushort[] red;

        /// <summary>
        /// An array of value describing the response of the green channel.
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray)]
        public ushort[] green;

        /// <summary>
        /// An array of value describing the response of the blue channel.
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray)]
        public ushort[] blue;

        /// <summary>
        /// The number of elements in each array.
        /// </summary>
        public uint size;
    }
}
