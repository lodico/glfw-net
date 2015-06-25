using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GLFW {
    /*! @brief Video mode type.
     *
     *  This describes a single video mode.
     *
     *  @ingroup monitor
     */
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 6)]
    public struct GLFWvidmode {
        /*! The width, in screen coordinates, of the video mode.
         */
        [FieldOffset(sizeof(int) * 0)]
        public int width;

        /*! The height, in screen coordinates, of the video mode.
         */
        [FieldOffset(sizeof(int) * 1)]
        public int height;

        /*! The bit depth of the red channel of the video mode.
         */
        [FieldOffset(sizeof(int) * 2)]
        public int redBits;

        /*! The bit depth of the green channel of the video mode.
         */
        [FieldOffset(sizeof(int) * 3)]
        public int greenBits;

        /*! The bit depth of the blue channel of the video mode.
         */
        [FieldOffset(sizeof(int) * 4)]
        public int blueBits;

        /*! The refresh rate, in Hz, of the video mode.
         */
        [FieldOffset(sizeof(int) * 5)]
        public int refreshRate;
    }
}
