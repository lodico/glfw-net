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

        public readonly static GLFWmonitor NULL = new GLFWmonitor { pointer = IntPtr.Zero };
    }
}
