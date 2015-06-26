using System;
using System.Runtime.InteropServices;

namespace GLFW {
    /// <summary>
    /// Opaque monitor object pointer.
    /// </summary>
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
