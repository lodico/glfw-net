using System;
using System.Runtime.InteropServices;

namespace GLFW {
    /// <summary>
    /// Opaque cursor object pointer.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct GLFWcursor {
        [FieldOffsetAttribute(0)]
        private IntPtr pointer;

        public readonly static GLFWcursor NULL = new GLFWcursor { pointer = IntPtr.Zero };
    }
}
