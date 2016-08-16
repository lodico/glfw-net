using System.Runtime.InteropServices;
using System.Security;

namespace GLFWnet.Binding
{
    public unsafe partial class InternalGLFW3
    {
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
         *  @since Added in version 3.0.
         *
         *  @ingroup init
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        internal static extern sbyte* glfwGetVersionString();

        /*! @brief Sets the title of the specified window.
         *
         *  This function sets the window title, encoded as UTF-8, of the specified
         *  window.
         *
         *  @param[in] window The window whose title to change.
         *  @param[in] title The UTF-8 encoded window title.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_title
         *
         *  @since Added in version 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup window
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetWindowTitle(GLFWwindow window, byte* title);

        /*! @brief Creates a window and its associated context.
         *
         *  This function creates a window and its associated OpenGL or OpenGL ES
         *  context.  Most of the options controlling how the window and its context
         *  should be created are specified with [window hints](@ref window_hints).
         *
         *  Successful creation does not change which context is current.  Before you
         *  can use the newly created context, you need to
         *  [make it current](@ref context_current).  For information about the `share`
         *  parameter, see @ref context_sharing.
         *
         *  The created window, framebuffer and context may differ from what you
         *  requested, as not all parameters and hints are
         *  [hard constraints](@ref window_hints_hard).  This includes the size of the
         *  window, especially for full screen windows.  To query the actual attributes
         *  of the created window, framebuffer and context, use queries like @ref
         *  glfwGetWindowAttrib and @ref glfwGetWindowSize.
         *
         *  To create a full screen window, you need to specify the monitor the window
         *  will cover.  If no monitor is specified, windowed mode will be used.  Unless
         *  you have a way for the user to choose a specific monitor, it is recommended
         *  that you pick the primary monitor.  For more information on how to query
         *  connected monitors, see @ref monitor_monitors.
         *
         *  For full screen windows, the specified size becomes the resolution of the
         *  window's _desired video mode_.  As long as a full screen window has input
         *  focus, the supported video mode most closely matching the desired video mode
         *  is set for the specified monitor.  For more information about full screen
         *  windows, including the creation of so called _windowed full screen_ or
         *  _borderless full screen_ windows, see @ref window_windowed_full_screen.
         *
         *  By default, newly created windows use the placement recommended by the
         *  window system.  To create the window at a specific position, make it
         *  initially invisible using the [GLFW_VISIBLE](@ref window_hints_wnd) window
         *  hint, set its [position](@ref window_pos) and then [show](@ref window_hide)
         *  it.
         *
         *  If a full screen window has input focus, the screensaver is prohibited from
         *  starting.
         *
         *  Window systems put limits on window sizes.  Very large or very small window
         *  dimensions may be overridden by the window system on creation.  Check the
         *  actual [size](@ref window_size) after creation.
         *
         *  The [swap interval](@ref buffer_swap) is not set during window creation and
         *  the initial value may vary depending on driver settings and defaults.
         *
         *  @param[in] width The desired width, in screen coordinates, of the window.
         *  This must be greater than zero.
         *  @param[in] height The desired height, in screen coordinates, of the window.
         *  This must be greater than zero.
         *  @param[in] title The initial, UTF-8 encoded window title.
         *  @param[in] monitor The monitor to use for full screen mode, or `NULL` to use
         *  windowed mode.
         *  @param[in] share The window whose context to share resources with, or `NULL`
         *  to not share resources.
         *  @return The handle of the created window, or `NULL` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @remarks __Windows:__ Window creation will fail if the Microsoft GDI
         *  software OpenGL implementation is the only one available.
         *
         *  @remarks __Windows:__ If the executable has an icon resource named
         *  `GLFW_ICON,` it will be set as the icon for the window.  If no such icon is
         *  present, the `IDI_WINLOGO` icon will be used instead.
         *
         *  @remarks __Windows:__ The context to share resources with may not be current
         *  on any other thread.
         *
         *  @remarks __OS X:__ The GLFW window has no icon, as it is not a document
         *  window, but the dock icon will be the same as the application bundle's icon.
         *  For more information on bundles, see the
         *  [Bundle Programming Guide](https://developer.apple.com/library/mac/documentation/CoreFoundation/Conceptual/CFBundles/)
         *  in the Mac Developer Library.
         *
         *  @remarks __OS X:__ The first time a window is created the menu bar is
         *  populated with common commands like Hide, Quit and About.  The About entry
         *  opens a minimal about dialog with information from the application's bundle.
         *  The menu bar can be disabled with a
         *  [compile-time option](@ref compile_options_osx).
         *
         *  @remarks __OS X:__ On OS X 10.10 and later the window frame will not be
         *  rendered at full resolution on Retina displays unless the
         *  `NSHighResolutionCapable` key is enabled in the application bundle's
         *  `Info.plist`.  For more information, see
         *  [High Resolution Guidelines for OS X](https://developer.apple.com/library/mac/documentation/GraphicsAnimation/Conceptual/HighResolutionOSX/Explained/Explained.html)
         *  in the Mac Developer Library.
         *
         *  @remarks __X11:__ There is no mechanism for setting the window icon yet.
         *
         *  @remarks __X11:__ Some window managers will not respect the placement of
         *  initially hidden windows.
         *
         *  @par Reentrancy
         *  This function may not be called from a callback.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_creation
         *  @sa glfwDestroyWindow
         *
         *  @since Added in version 3.0.  Replaces `glfwOpenWindow`.
         *
         *  @ingroup window
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern GLFWwindow glfwCreateWindow(int width, int height, byte* title, GLFWmonitor monitor, GLFWwindow share);

        /*! @brief Returns the localized name of the specified printable key.
         *
         *  This function returns the localized name of the specified printable key.
         *  This is intended for displaying key bindings to the user.
         *
         *  If the key is `GLFW_KEY_UNKNOWN`, the scancode is used instead, otherwise
         *  the scancode is ignored.  If a non-printable key or (if the key is
         *  `GLFW_KEY_UNKNOWN`) a scancode that maps to a non-printable key is
         *  specified, this function returns `NULL`.          
         *
         *  This behavior allows you to pass in the arguments passed to the
         *  [key callback](@ref input_key) without modification.
         *
         *  The printable keys are:
         *  - `GLFW_KEY_APOSTROPHE`
         *  - `GLFW_KEY_COMMA`
         *  - `GLFW_KEY_MINUS`
         *  - `GLFW_KEY_PERIOD`
         *  - `GLFW_KEY_SLASH`
         *  - `GLFW_KEY_SEMICOLON`
         *  - `GLFW_KEY_EQUAL`
         *  - `GLFW_KEY_LEFT_BRACKET`
         *  - `GLFW_KEY_RIGHT_BRACKET`
         *  - `GLFW_KEY_BACKSLASH`
         *  - `GLFW_KEY_WORLD_1`
         *  - `GLFW_KEY_WORLD_2`
         *  - `GLFW_KEY_0` to `GLFW_KEY_9`
         *  - `GLFW_KEY_A` to `GLFW_KEY_Z`
         *  - `GLFW_KEY_KP_0` to `GLFW_KEY_KP_9`
         *  - `GLFW_KEY_KP_DECIMAL`
         *  - `GLFW_KEY_KP_DIVIDE`
         *  - `GLFW_KEY_KP_MULTIPLY`
         *  - `GLFW_KEY_KP_SUBTRACT`
         *  - `GLFW_KEY_KP_ADD`
         *  - `GLFW_KEY_KP_EQUAL`
         *
         *  @param[in] key The key to query, or `GLFW_KEY_UNKNOWN`.
         *  @param[in] scancode The scancode of the key to query.
         *  @return The localized name of the key, or `NULL`.
         *
         *  @errors Possible errors include @ref GLFW_NOT_INITIALIZED and @ref
         *  GLFW_PLATFORM_ERROR.
         *
         *  @pointer_lifetime The returned string is allocated and freed by GLFW.  You
         *  should not free it yourself.  It is valid until the next call to @ref
         *  glfwGetKeyName, or until the library is terminated.
         *
         *  @thread_safety This function must only be called from the main thread.
         *
         *  @sa @ref input_key_name
         *
         *  @since Added in version 3.2.
         *
         *  @ingroup input
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern sbyte* glfwGetKeyName(int key, int scancode);

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
         *  @since Added in version 3.0.
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
         *  @since Added in version 3.0.
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
         *  @since Added in version 3.0.
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
         *  @since Added in version 3.0.
         *
         *  @ingroup monitor
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        internal static extern GLFWmonitor* glfwGetMonitors(out int count);

        /*! @brief Returns the current gamma ramp for the specified monitor.
         *
         *  This function returns the current gamma ramp of the specified monitor.
         *
         *  @param[in] monitor The monitor to query.
         *  @return The current gamma ramp, or `NULL` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Pointer Lifetime
         *  The returned structure and its arrays are allocated and freed by GLFW.  You
         *  should not free them yourself.  They are valid until the specified monitor
         *  is disconnected, this function is called again for that monitor or the
         *  library is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_gamma
         *
         *  @since Added in version 3.0.
         *
         *  @ingroup monitor
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        internal static extern InternalGLFWgammaramp* glfwGetGammaRamp(GLFWmonitor monitor);

        /*! @brief Sets the current gamma ramp for the specified monitor.
         *
         *  This function sets the current gamma ramp for the specified monitor.  The
         *  original gamma ramp for that monitor is saved by GLFW the first time this
         *  function is called and is restored by @ref glfwTerminate.
         *
         *  @param[in] monitor The monitor whose gamma ramp to set.
         *  @param[in] ramp The gamma ramp to use.
         *
         *  @remarks Gamma ramp sizes other than 256 are not supported by all platforms
         *  or graphics hardware.
         *
         *  @remarks __Windows:__ The gamma ramp size must be 256.
         *
         *  @par Pointer Lifetime
         *  The specified gamma ramp is copied before this function returns.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_gamma
         *
         *  @since Added in version 3.0.
         *
         *  @ingroup monitor
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        internal static extern void glfwSetGammaRamp(GLFWmonitor monitor, InternalGLFWgammaramp ramp);

        /*! @brief Creates a custom cursor.
         *
         *  Creates a new custom cursor image that can be set for a window with @ref
         *  glfwSetCursor.  The cursor can be destroyed with @ref glfwDestroyCursor.
         *  Any remaining cursors are destroyed by @ref glfwTerminate.
         *
         *  The pixels are 32-bit little-endian RGBA, i.e. eight bits per channel.  They
         *  are arranged canonically as packed sequential rows, starting from the
         *  top-left corner.
         *
         *  The cursor hotspot is specified in pixels, relative to the upper-left corner
         *  of the cursor image.  Like all other coordinate systems in GLFW, the X-axis
         *  points to the right and the Y-axis points down.
         *
         *  @param[in] image The desired cursor image.
         *  @param[in] xhot The desired x-coordinate, in pixels, of the cursor hotspot.
         *  @param[in] yhot The desired y-coordinate, in pixels, of the cursor hotspot.
         *
         *  @return The handle of the created cursor, or `NULL` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Pointer Lifetime
         *  The specified image data is copied before this function returns.
         *
         *  @par Reentrancy
         *  This function may not be called from a callback.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref cursor_object
         *  @sa glfwDestroyCursor
         *  @sa glfwCreateStandardCursor
         *
         *  @since Added in version 3.1.
         *
         *  @ingroup input
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        internal static extern GLFWcursor glfwCreateCursor(InternalGLFWimage image, int xhot, int yhot);

        /*! @brief Sets the icon for the specified window.
         *
         *  This function sets the icon of the specified window.  If passed an array of
         *  candidate images, those of or closest to the sizes desired by the system are
         *  selected.  If no images are specified, the window reverts to its default
         *  icon.
         *
         *  The desired image sizes varies depending on platform and system settings.
         *  The selected images will be rescaled as needed.  Good sizes include 16x16,
         *  32x32 and 48x48.
         *
         *  @param[in] window The window whose icon to set.
         *  @param[in] count The number of images in the specified array, or zero to
         *  revert to the default window icon.
         *  @param[in] images The images to create the icon from.  This is ignored if
         *  count is zero.
         *
         *  @errors Possible errors include @ref GLFW_NOT_INITIALIZED and @ref
         *  GLFW_PLATFORM_ERROR.
         *
         *  @pointer_lifetime The specified image data is copied before this function
         *  returns.
         *
         *  @remark @osx The GLFW window has no icon, as it is not a document
         *  window, so this function does nothing.  The dock icon will be the same as
         *  the application bundle's icon.  For more information on bundles, see the
         *  [Bundle Programming Guide](https://developer.apple.com/library/mac/documentation/CoreFoundation/Conceptual/CFBundles/)
         *  in the Mac Developer Library.
         *
         *  @thread_safety This function must only be called from the main thread.
         *
         *  @sa @ref window_icon
         *
         *  @since Added in version 3.2.
         *
         *  @ingroup window
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        internal static extern void glfwSetWindowIcon(GLFWwindow window, int count, InternalGLFWimage* images);

        /*! @brief Returns the Vulkan instance extensions required by GLFW.
                 *
                 *  This function returns an array of names of Vulkan instance extensions required
                 *  by GLFW for creating Vulkan surfaces for GLFW windows.  If successful, the
                 *  list will always contains `VK_KHR_surface`, so if you don't require any
                 *  additional extensions you can pass this list directly to the
                 *  `VkInstanceCreateInfo` struct.
                 *
                 *  If Vulkan is not available on the machine, this function returns `NULL` and
                 *  generates a @ref GLFW_API_UNAVAILABLE error.  Call @ref glfwVulkanSupported
                 *  to check whether Vulkan is available.
                 *
                 *  If Vulkan is available but no set of extensions allowing window surface
                 *  creation was found, this function returns `NULL`.  You may still use Vulkan
                 *  for off-screen rendering and compute work.
                 *
                 *  @param[out] count Where to store the number of extensions in the returned
                 *  array.  This is set to zero if an error occurred.
                 *  @return An array of ASCII encoded extension names, or `NULL` if an
                 *  [error](@ref error_handling) occurred.
                 *
                 *  @errors Possible errors include @ref GLFW_NOT_INITIALIZED and @ref
                 *  GLFW_API_UNAVAILABLE.
                 *
                 *  @remarks Additional extensions may be required by future versions of GLFW.
                 *  You should check if any extensions you wish to enable are already in the
                 *  returned array, as it is an error to specify an extension more than once in
                 *  the `VkInstanceCreateInfo` struct.
                 *
                 *  @pointer_lifetime The returned array is allocated and freed by GLFW.  You
                 *  should not free it yourself.  It is guaranteed to be valid only until the
                 *  library is terminated.
                 *
                 *  @thread_safety This function may be called from any thread.
                 *
                 *  @sa @ref vulkan_ext
                 *  @sa glfwCreateWindowSurface
                 *
                 *  @since Added in version 3.2.
                 *
                 *  @ingroup vulkan
                 */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern sbyte** glfwGetRequiredInstanceExtensions(out uint count);

        /*! @brief Returns whether the specified extension is available.
         *
         *  This function returns whether the specified
         *  [client API extension](@ref context_glext) is supported by the current
         *  OpenGL or OpenGL ES context.  It searches both for OpenGL and OpenGL ES
         *  extension and platform-specific context creation API extensions.
         *
         *  A context must be current on the calling thread.  Calling this function
         *  without a current context will cause a @ref GLFW_NO_CURRENT_CONTEXT error.
         *
         *  As this functions retrieves and searches one or more extension strings each
         *  call, it is recommended that you cache its results if it is going to be used
         *  frequently.  The extension strings will not change during the lifetime of
         *  a context, so there is no danger in doing this.
         *
         *  @param[in] extension The ASCII encoded name of the extension.
         *  @return `GL_TRUE` if the extension is available, or `GL_FALSE` otherwise.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref context_glext
         *  @sa glfwGetProcAddress
         *
         *  @since Added in version 1.0.
         *
         *  @ingroup context
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool glfwExtensionSupported(byte* extension);

        /*! @brief Sets the clipboard to the specified string.
         *
         *  This function sets the system clipboard to the specified, UTF-8 encoded
         *  string.
         *
         *  @param[in] window The window that will own the clipboard contents.
         *  @param[in] string A UTF-8 encoded string.
         *
         *  @par Pointer Lifetime
         *  The specified string is copied before this function returns.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref clipboard
         *  @sa glfwGetClipboardString
         *
         *  @since Added in version 3.0.
         *
         *  @ingroup input
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetClipboardString(GLFWwindow window, byte* str);

        /*! @brief Returns the address of the specified function for the current
         *  context.
         *
         *  This function returns the address of the specified
         *  [core or extension function](@ref context_glext), if it is supported
         *  by the current context.
         *
         *  A context must be current on the calling thread.  Calling this function
         *  without a current context will cause a @ref GLFW_NO_CURRENT_CONTEXT error.
         *
         *  @param[in] procname The ASCII encoded name of the function.
         *  @return The address of the function, or `NULL` if the function is
         *  unavailable or an [error](@ref error_handling) occurred.
         *
         *  @remarks The addresses of a given function is not guaranteed to be the same
         *  between contexts.
         *
         *  @remarks This function may return a non-`NULL` address despite the
         *  associated version or extension not being available.  Always check the
         *  context version or extension string presence first.
         *
         *  @par Pointer Lifetime
         *  The returned function pointer is valid until the context is destroyed or the
         *  library is terminated.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref context_glext
         *  @sa glfwExtensionSupported
         *
         *  @since Added in version 1.0.
         *
         *  @ingroup context
         */
        [DllImport(GLFW3.NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWglproc glfwGetProcAddress(byte* procname);
        #endregion GLFW API functions
    }
}
