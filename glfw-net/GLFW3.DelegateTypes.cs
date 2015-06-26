using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GLFW {
    public partial class GLFW3 {

        /*! @brief Client API function pointer type.
         *
         *  Generic function pointer used for returning client API function pointers
         *  without forcing a cast from a regular pointer.
         *
         *  @ingroup context
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWglproc();

        /*! @brief The function signature for error callbacks.
         *
         *  This is the function signature for error callback functions.
         *
         *  @param[in] error An [error code](@ref errors).
         *  @param[in] description A UTF-8 encoded string describing the error.
         *
         *  @sa glfwSetErrorCallback
         *
         *  @ingroup init
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWerrorfun(int error, [MarshalAs(UnmanagedType.LPStr)] string description);

        /*! @brief The function signature for window position callbacks.
         *
         *  This is the function signature for window position callback functions.
         *
         *  @param[in] window The window that was moved.
         *  @param[in] xpos The new x-coordinate, in screen coordinates, of the
         *  upper-left corner of the client area of the window.
         *  @param[in] ypos The new y-coordinate, in screen coordinates, of the
         *  upper-left corner of the client area of the window.
         *
         *  @sa glfwSetWindowPosCallback
         *
         *  @ingroup window
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowposfun(GLFWwindow window, int xpos, int ypos);
        
        /*! @brief The function signature for window resize callbacks.
         *
         *  This is the function signature for window size callback functions.
         *
         *  @param[in] window The window that was resized.
         *  @param[in] width The new width, in screen coordinates, of the window.
         *  @param[in] height The new height, in screen coordinates, of the window.
         *
         *  @sa glfwSetWindowSizeCallback
         *
         *  @ingroup window
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowsizefun(GLFWwindow window, int width, int height);

        /*! @brief The function signature for window close callbacks.
         *
         *  This is the function signature for window close callback functions.
         *
         *  @param[in] window The window that the user attempted to close.
         *
         *  @sa glfwSetWindowCloseCallback
         *
         *  @ingroup window
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowclosefun(GLFWwindow window);

        /*! @brief The function signature for window content refresh callbacks.
         *
         *  This is the function signature for window refresh callback functions.
         *
         *  @param[in] window The window whose content needs to be refreshed.
         *
         *  @sa glfwSetWindowRefreshCallback
         *
         *  @ingroup window
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowrefreshfun(GLFWwindow window);

        /*! @brief The function signature for window focus/defocus callbacks.
         *
         *  This is the function signature for window focus callback functions.
         *
         *  @param[in] window The window that gained or lost input focus.
         *  @param[in] focused `GL_TRUE` if the window was given input focus, or
         *  `GL_FALSE` if it lost it.
         *
         *  @sa glfwSetWindowFocusCallback
         *
         *  @ingroup window
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowfocusfun(GLFWwindow window, int focused);

        /*! @brief The function signature for window iconify/restore callbacks.
         *
         *  This is the function signature for window iconify/restore callback
         *  functions.
         *
         *  @param[in] window The window that was iconified or restored.
         *  @param[in] iconified `GL_TRUE` if the window was iconified, or `GL_FALSE`
         *  if it was restored.
         *
         *  @sa glfwSetWindowIconifyCallback
         *
         *  @ingroup window
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowiconifyfun(GLFWwindow window, int iconified);

        /*! @brief The function signature for framebuffer resize callbacks.
         *
         *  This is the function signature for framebuffer resize callback
         *  functions.
         *
         *  @param[in] window The window whose framebuffer was resized.
         *  @param[in] width The new width, in pixels, of the framebuffer.
         *  @param[in] height The new height, in pixels, of the framebuffer.
         *
         *  @sa glfwSetFramebufferSizeCallback
         *
         *  @ingroup window
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWframebuffersizefun(GLFWwindow window, int width, int height);

        /*! @brief The function signature for mouse button callbacks.
         *
         *  This is the function signature for mouse button callback functions.
         *
         *  @param[in] window The window that received the event.
         *  @param[in] button The [mouse button](@ref buttons) that was pressed or
         *  released.
         *  @param[in] action One of `GLFW_PRESS` or `GLFW_RELEASE`.
         *  @param[in] mods Bit field describing which [modifier keys](@ref mods) were
         *  held down.
         *
         *  @sa glfwSetMouseButtonCallback
         *
         *  @ingroup input
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWmousebuttonfun(GLFWwindow window, int button, int action, int mods);

        /*! @brief The function signature for cursor position callbacks.
         *
         *  This is the function signature for cursor position callback functions.
         *
         *  @param[in] window The window that received the event.
         *  @param[in] xpos The new x-coordinate, in screen coordinates, of the cursor.
         *  @param[in] ypos The new y-coordinate, in screen coordinates, of the cursor.
         *
         *  @sa glfwSetCursorPosCallback
         *
         *  @ingroup input
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWcursorposfun(GLFWwindow window, double xpos, double ypos);

        /*! @brief The function signature for cursor enter/leave callbacks.
         *
         *  This is the function signature for cursor enter/leave callback functions.
         *
         *  @param[in] window The window that received the event.
         *  @param[in] entered `GL_TRUE` if the cursor entered the window's client
         *  area, or `GL_FALSE` if it left it.
         *
         *  @sa glfwSetCursorEnterCallback
         *
         *  @ingroup input
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWcursorenterfun(GLFWwindow window, int entered);

        /*! @brief The function signature for scroll callbacks.
         *
         *  This is the function signature for scroll callback functions.
         *
         *  @param[in] window The window that received the event.
         *  @param[in] xoffset The scroll offset along the x-axis.
         *  @param[in] yoffset The scroll offset along the y-axis.
         *
         *  @sa glfwSetScrollCallback
         *
         *  @ingroup input
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWscrollfun(GLFWwindow window, double xoffset, double yoffset);

        /*! @brief The function signature for keyboard key callbacks.
         *
         *  This is the function signature for keyboard key callback functions.
         *
         *  @param[in] window The window that received the event.
         *  @param[in] key The [keyboard key](@ref keys) that was pressed or released.
         *  @param[in] scancode The system-specific scancode of the key.
         *  @param[in] action `GLFW_PRESS`, `GLFW_RELEASE` or `GLFW_REPEAT`.
         *  @param[in] mods Bit field describing which [modifier keys](@ref mods) were
         *  held down.
         *
         *  @sa glfwSetKeyCallback
         *
         *  @ingroup input
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWkeyfun(GLFWwindow window, int key, int scancode, int action, int mods);

        /*! @brief The function signature for Unicode character callbacks.
         *
         *  This is the function signature for Unicode character callback functions.
         *
         *  @param[in] window The window that received the event.
         *  @param[in] codepoint The Unicode code point of the character.
         *
         *  @sa glfwSetCharCallback
         *
         *  @ingroup input
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWcharfun(GLFWwindow window, uint codepoint);

        /*! @brief The function signature for Unicode character with modifiers
         *  callbacks.
         *
         *  This is the function signature for Unicode character with modifiers callback
         *  functions.  It is called for each input character, regardless of what
         *  modifier keys are held down.
         *
         *  @param[in] window The window that received the event.
         *  @param[in] codepoint The Unicode code point of the character.
         *  @param[in] mods Bit field describing which [modifier keys](@ref mods) were
         *  held down.
         *
         *  @sa glfwSetCharModsCallback
         *
         *  @ingroup input
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWcharmodsfun(GLFWwindow window, uint codepoint, int mods);

        /*! @brief The function signature for file drop callbacks.
         *
         *  This is the function signature for file drop callbacks.
         *
         *  @param[in] window The window that received the event.
         *  @param[in] count The number of dropped files.
         *  @param[in] paths The UTF-8 encoded file and/or directory path names.
         *
         *  @sa glfwSetDropCallback
         *
         *  @ingroup input
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWdropfun(GLFWwindow window, int count, [In, Out] string[] paths);

        /*! @brief The function signature for monitor configuration callbacks.
         *
         *  This is the function signature for monitor configuration callback functions.
         *
         *  @param[in] monitor The monitor that was connected or disconnected.
         *  @param[in] event One of `GLFW_CONNECTED` or `GLFW_DISCONNECTED`.
         *
         *  @sa glfwSetMonitorCallback
         *
         *  @ingroup monitor
         */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWmonitorfun(GLFWmonitor monitor, int eventStatus);
    }
}
