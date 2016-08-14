using System.Runtime.InteropServices;
using System.Security;

namespace GLFWnet.Binding {
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
         *  @since Added in version 3.0.
         *
         *  @ingroup init
         */
        [DllImport(GLFW3.NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        internal static extern sbyte* glfwGetVersionString();

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
        #endregion GLFW API functions
    }
}
