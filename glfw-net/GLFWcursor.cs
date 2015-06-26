using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GLFW {
    /*! @brief Opaque cursor object.
     *
     *  Opaque cursor object.
     *
     *  @ingroup cursor
     */
    [StructLayout(LayoutKind.Explicit)]
    public struct GLFWcursor {
        [FieldOffsetAttribute(0)]
        private IntPtr pointer;

        private GLFWcursor(IntPtr pointer) {
            this.pointer = pointer;
        }

        public readonly static GLFWcursor NULL = new GLFWcursor(IntPtr.Zero);
    }
}
