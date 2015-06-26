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

        public readonly static GLFWwindow NULL = new GLFWwindow { pointer = IntPtr.Zero };
    }
}
