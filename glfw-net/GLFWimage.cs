using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GLFW {
    /*! @brief Image data.
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct GLFWimage {
        /*! The width, in pixels, of this image.
             */
        public int width;

        /*! The height, in pixels, of this image.
         */
        public int height;

        /*! The pixel data of this image, arranged left-to-right, top-to-bottom.
         */
        [MarshalAs(UnmanagedType.LPArray)]
        public byte[] pixels;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct InternalGLFWimage {
        /*! The width, in pixels, of this image.
             */
        public int width;

        /*! The height, in pixels, of this image.
         */
        public int height;

        /*! The pixel data of this image, arranged left-to-right, top-to-bottom.
         */
        public byte* pixels;
    }
}
