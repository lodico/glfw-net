using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GLFW {
    /*! @brief Opaque monitor object.
     *
     *  Opaque monitor object.
     *
     *  @ingroup monitor
     */
    [StructLayout(LayoutKind.Explicit)]
    public struct GLFWmonitor {
        [FieldOffsetAttribute(0)]
        private IntPtr pointer;

        private GLFWmonitor(IntPtr pointer) {
            this.pointer = pointer;
        }

        public readonly static GLFWmonitor NULL = new GLFWmonitor(IntPtr.Zero);
    }
}
