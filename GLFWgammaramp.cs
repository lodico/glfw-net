using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GLFW {
    /*! @brief Gamma ramp.
     *
     *  This describes the gamma ramp for a monitor.
     *
     *  @sa glfwGetGammaRamp glfwSetGammaRamp
     *
     *  @ingroup monitor
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct GLFWgammaramp {
        /*! An array of value describing the response of the red channel.
         */
        [MarshalAs(UnmanagedType.LPArray)]
        public ushort[] red;

        /*! An array of value describing the response of the green channel.
         */
        [MarshalAs(UnmanagedType.LPArray)]
        public ushort[] green;

        /*! An array of value describing the response of the blue channel.
         */
        [MarshalAs(UnmanagedType.LPArray)]
        public ushort[] blue;

        /*! The number of elements in each array.
         */
        public uint size;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct InternalGLFWgammaramp {
        /*! An array of value describing the response of the red channel.
         */
        public ushort* red;

        /*! An array of value describing the response of the green channel.
         */
        public ushort* green;

        /*! An array of value describing the response of the blue channel.
         */
        public ushort* blue;

        /*! The number of elements in each array.
         */
        public uint size;
    }
}
