using System;
using System.Runtime.InteropServices;

namespace GLFW {
    /// <summary>
    /// Opaque window object pointer.
    /// </summary>
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
