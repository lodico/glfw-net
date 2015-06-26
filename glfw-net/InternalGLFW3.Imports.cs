using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace GLFW {
    public unsafe partial class InternalGLFW3 {
        #region GLFW API functions
        /*! @brief Returns a string describing the compile-time configuration.
         *
         *  This function returns the compile-time generated
         *  [version string](@ref intro_version_string) of the GLFW library binary.  It
         *  describes the version, platform, compiler and any platform-specific
         *  compile-time options.
         *
         *  __Do not use the version string__ to parse the GLFW library version.  The
         *  @ref glfwGetVersion function already provides the version of the running
         *  library binary.
         *
         *  This function always succeeds.
         *
         *  @return The GLFW version string.
         *
         *  @remarks This function may be called before @ref glfwInit.
         *
         *  @par Pointer Lifetime
         *  The returned string is static and compile-time generated.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref intro_version
         *  @sa glfwGetVersion
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup init
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        internal static extern sbyte* glfwGetVersionString();

        /*! @brief Returns the name of the specified monitor.
         *
         *  This function returns a human-readable name, encoded as UTF-8, of the
         *  specified monitor.  The name typically reflects the make and model of the
         *  monitor and is not guaranteed to be unique among the connected monitors.
         *
         *  @param[in] monitor The monitor to query.
         *  @return The UTF-8 encoded name of the monitor, or `NULL` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Pointer Lifetime
         *  The returned string is allocated and freed by GLFW.  You should not free it
         *  yourself.  It is valid until the specified monitor is disconnected or the
         *  library is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_properties
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup monitor
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        internal static extern sbyte* glfwGetMonitorName(GLFWmonitor monitor);

        /*! @brief Returns the name of the specified joystick.
         *
         *  This function returns the name, encoded as UTF-8, of the specified joystick.
         *  The returned string is allocated and freed by GLFW.  You should not free it
         *  yourself.
         *
         *  @param[in] joy The [joystick](@ref joysticks) to query.
         *  @return The UTF-8 encoded name of the joystick, or `NULL` if the joystick
         *  is not present.
         *
         *  @par Pointer Lifetime
         *  The returned string is allocated and freed by GLFW.  You should not free it
         *  yourself.  It is valid until the specified joystick is disconnected, this
         *  function is called again for that joystick or the library is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref joystick_name
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup input
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        internal static extern sbyte* glfwGetJoystickName(int joy);

        /*! @brief Returns the contents of the clipboard as a string.
         *
         *  This function returns the contents of the system clipboard, if it contains
         *  or is convertible to a UTF-8 encoded string.
         *
         *  @param[in] window The window that will request the clipboard contents.
         *  @return The contents of the clipboard as a UTF-8 encoded string, or `NULL`
         *  if an [error](@ref error_handling) occurred.
         *
         *  @par Pointer Lifetime
         *  The returned string is allocated and freed by GLFW.  You should not free it
         *  yourself.  It is valid until the next call to @ref
         *  glfwGetClipboardString or @ref glfwSetClipboardString, or until the library
         *  is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref clipboard
         *  @sa glfwSetClipboardString
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup input
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        internal static extern sbyte* glfwGetClipboardString(GLFWwindow window);
        
        /*! @brief Returns the currently connected monitors.
         *
         *  This function returns an array of handles for all currently connected
         *  monitors.
         *
         *  @param[out] count Where to store the number of monitors in the returned
         *  array.  This is set to zero if an error occurred.
         *  @return An array of monitor handles, or `NULL` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Pointer Lifetime
         *  The returned array is allocated and freed by GLFW.  You should not free it
         *  yourself.  It is guaranteed to be valid only until the monitor configuration
         *  changes or the library is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_monitors
         *  @sa @ref monitor_event
         *  @sa glfwGetPrimaryMonitor
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup monitor
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFWmonitor* glfwGetMonitors(out int count);
        #endregion GLFW API functions
    }
}
