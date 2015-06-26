using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GLFW {
    /*! @brief Opaque window object.
     *
     *  Opaque window object.
     *
     *  @ingroup window
     */
    [StructLayout(LayoutKind.Explicit)]
    public struct GLFWwindow {
        [FieldOffsetAttribute(0)]
        private IntPtr pointer;

        private GLFWwindow(IntPtr pointer) {
            this.pointer = pointer;
        }

        public readonly static GLFWwindow NULL = new GLFWwindow(IntPtr.Zero);
    }
}
