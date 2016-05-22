using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace GLFWnet.Binding {
    public unsafe partial class GLFW3 {
        #region GLFW.NET Additions
        /// <summary>
        /// The native library name constant.
        /// </summary>
        public const string NATIVE = "glfw3";

        /// <summary>
        /// Architecture enumeration.
        /// </summary>
        public enum Architecture : int {
            x86 = 86,
            x64 = 64,
            X86 = x86,
            X64 = x64
        }

        /// <summary>
        /// The library compilation architecture.
        /// </summary>
#if X86
        public const Architecture ARCHITECTURE = Architecture.x86;
#elif X64
        public const Architecture ARCHITECTURE = Architecture.x64;
#else
#error "Unknown target architecture."
#endif

        /// <summary>
        /// Adds the specified native directory path to the Path environment variable to facilitate native loading.
        /// </summary>
        /// <param name="nativeDirectory">The directory that the native library is stored in.</param>
        public static void ConfigureNativesDirectory(string nativeDirectory) {
            if (Directory.Exists(nativeDirectory)) {
                Environment.SetEnvironmentVariable("Path", Environment.GetEnvironmentVariable("Path") + ";" + Path.GetFullPath(nativeDirectory) + ";");
            }
        }
        #endregion GLFW.NET Additions

        #region GLFW API functions
        /*! @brief Initializes the GLFW library.
         *
         *  This function initializes the GLFW library.  Before most GLFW functions can
         *  be used, GLFW must be initialized, and before an application terminates GLFW
         *  should be terminated in order to free any resources allocated during or
         *  after initialization.
         *
         *  If this function fails, it calls @ref glfwTerminate before returning.  If it
         *  succeeds, you should call @ref glfwTerminate before the application exits.
         *
         *  Additional calls to this function after successful initialization but before
         *  termination will return `GL_TRUE` immediately.
         *
         *  @return `GL_TRUE` if successful, or `GL_FALSE` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @remarks __OS X:__ This function will change the current directory of the
         *  application to the `Contents/Resources` subdirectory of the application's
         *  bundle, if present.  This can be disabled with a
         *  [compile-time option](@ref compile_options_osx).
         *
         *  @remarks __X11:__ If the `LC_CTYPE` category of the current locale is set to
         *  `"C"` then the environment's locale will be applied to that category.  This
         *  is done because character input will not function when `LC_CTYPE` is set to
         *  `"C"`.  If another locale was set before this function was called, it will
         *  be left untouched.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref intro_init
         *  @sa glfwTerminate
         *
         *  @since Added in GLFW 1.0.
         *
         *  @ingroup init
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool glfwInit();

        /*! @brief Terminates the GLFW library.
         *
         *  This function destroys all remaining windows and cursors, restores any
         *  modified gamma ramps and frees any other allocated resources.  Once this
         *  function is called, you must again call @ref glfwInit successfully before
         *  you will be able to use most GLFW functions.
         *
         *  If GLFW has been successfully initialized, this function should be called
         *  before the application exits.  If initialization fails, there is no need to
         *  call this function, as it is called by @ref glfwInit before it returns
         *  failure.
         *
         *  @remarks This function may be called before @ref glfwInit.
         *
         *  @warning No window's context may be current on another thread when this
         *  function is called.
         *
         *  @par Reentrancy
         *  This function may not be called from a callback.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref intro_init
         *  @sa glfwInit
         *
         *  @since Added in GLFW 1.0.
         *
         *  @ingroup init
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwTerminate();

        /*! @brief Retrieves the version of the GLFW library.
         *
         *  This function retrieves the major, minor and revision numbers of the GLFW
         *  library.  It is intended for when you are using GLFW as a shared library and
         *  want to ensure that you are using the minimum required version.
         *
         *  Any or all of the version arguments may be `NULL`.  This function always
         *  succeeds.
         *
         *  @param[out] major Where to store the major version number, or `NULL`.
         *  @param[out] minor Where to store the minor version number, or `NULL`.
         *  @param[out] rev Where to store the revision number, or `NULL`.
         *
         *  @remarks This function may be called before @ref glfwInit.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref intro_version
         *  @sa glfwGetVersionString
         *
         *  @since Added in GLFW 1.0.
         *
         *  @ingroup init
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwGetVersion(out int major, out int minor, out int rev);

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
        public static string glfwGetVersionString() {
            return new string(InternalGLFW3.glfwGetVersionString());
        }

        /*! @brief Sets the error callback.
         *
         *  This function sets the error callback, which is called with an error code
         *  and a human-readable description each time a GLFW error occurs.
         *
         *  The error callback is called on the thread where the error occurred.  If you
         *  are using GLFW from multiple threads, your error callback needs to be
         *  written accordingly.
         *
         *  Because the description string may have been generated specifically for that
         *  error, it is not guaranteed to be valid after the callback has returned.  If
         *  you wish to use it after the callback returns, you need to make a copy.
         *
         *  Once set, the error callback remains set even after the library has been
         *  terminated.
         *
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set.
         *
         *  @remarks This function may be called before @ref glfwInit.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref error_handling
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup init
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWerrorfun glfwSetErrorCallback(GLFW3.GLFWerrorfun cbfun);

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
        public static GLFWmonitor[] glfwGetMonitors(out int count) {
            GLFWmonitor* ptrs = InternalGLFW3.glfwGetMonitors(out count);
            var monitors = new GLFWmonitor[count];

            for (int i = 0; i < count; i++) {
                monitors[i] = ptrs[i];
            }

            return monitors;
        }

        /*! @brief Returns the primary monitor.
         *
         *  This function returns the primary monitor.  This is usually the monitor
         *  where elements like the Windows task bar or the OS X menu bar is located.
         *
         *  @return The primary monitor, or `NULL` if an [error](@ref error_handling)
         *  occurred.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_monitors
         *  @sa glfwGetMonitors
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup monitor
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFWmonitor glfwGetPrimaryMonitor();

        /*! @brief Returns the position of the monitor's viewport on the virtual screen.
         *
         *  This function returns the position, in screen coordinates, of the upper-left
         *  corner of the specified monitor.
         *
         *  Any or all of the position arguments may be `NULL`.  If an error occurs, all
         *  non-`NULL` position arguments will be set to zero.
         *
         *  @param[in] monitor The monitor to query.
         *  @param[out] xpos Where to store the monitor x-coordinate, or `NULL`.
         *  @param[out] ypos Where to store the monitor y-coordinate, or `NULL`.
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
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwGetMonitorPos(GLFWmonitor monitor, out int xpos, out int ypos);

        /*! @brief Returns the physical size of the monitor.
         *
         *  This function returns the size, in millimetres, of the display area of the
         *  specified monitor.
         *
         *  Some systems do not provide accurate monitor size information, either
         *  because the monitor
         *  [EDID](https://en.wikipedia.org/wiki/Extended_display_identification_data)
         *  data is incorrect or because the driver does not report it accurately.
         *
         *  Any or all of the size arguments may be `NULL`.  If an error occurs, all
         *  non-`NULL` size arguments will be set to zero.
         *
         *  @param[in] monitor The monitor to query.
         *  @param[out] widthMM Where to store the width, in millimetres, of the
         *  monitor's display area, or `NULL`.
         *  @param[out] heightMM Where to store the height, in millimetres, of the
         *  monitor's display area, or `NULL`.
         *
         *  @remarks __Windows:__ The OS calculates the returned physical size from the
         *  current resolution and system DPI instead of querying the monitor EDID data.
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
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwGetMonitorPhysicalSize(GLFWmonitor monitor, out int widthMM, out int heightMM);

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
        public static string glfwGetMonitorName(GLFWmonitor monitor) {
            return new string(InternalGLFW3.glfwGetMonitorName(monitor));
        }

        /*! @brief Sets the monitor configuration callback.
         *
         *  This function sets the monitor configuration callback, or removes the
         *  currently set callback.  This is called when a monitor is connected to or
         *  disconnected from the system.
         *
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @bug __X11:__ This callback is not yet called on monitor configuration
         *  changes.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_event
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup monitor
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWmonitorfun glfwSetMonitorCallback(GLFW3.GLFWmonitorfun cbfun);

        /*! @brief Returns the available video modes for the specified monitor.
         *
         *  This function returns an array of all video modes supported by the specified
         *  monitor.  The returned array is sorted in ascending order, first by color
         *  bit depth (the sum of all channel depths) and then by resolution area (the
         *  product of width and height).
         *
         *  @param[in] monitor The monitor to query.
         *  @param[out] count Where to store the number of video modes in the returned
         *  array.  This is set to zero if an error occurred.
         *  @return An array of video modes, or `NULL` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Pointer Lifetime
         *  The returned array is allocated and freed by GLFW.  You should not free it
         *  yourself.  It is valid until the specified monitor is disconnected, this
         *  function is called again for that monitor or the library is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_modes
         *  @sa glfwGetVideoMode
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Changed to return an array of modes for a specific monitor.
         *
         *  @ingroup monitor
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.LPStruct)]
        public static extern GLFWvidmode glfwGetVideoModes(GLFWmonitor monitor, out int count);

        /*! @brief Returns the current mode of the specified monitor.
         *
         *  This function returns the current video mode of the specified monitor.  If
         *  you have created a full screen window for that monitor, the return value
         *  will depend on whether that window is iconified.
         *
         *  @param[in] monitor The monitor to query.
         *  @return The current mode of the monitor, or `NULL` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Pointer Lifetime
         *  The returned array is allocated and freed by GLFW.  You should not free it
         *  yourself.  It is valid until the specified monitor is disconnected or the
         *  library is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_modes
         *  @sa glfwGetVideoModes
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwGetDesktopMode`.
         *
         *  @ingroup monitor
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.LPStruct)]
        public static extern GLFWvidmode glfwGetVideoMode(GLFWmonitor monitor);

        /*! @brief Generates a gamma ramp and sets it for the specified monitor.
         *
         *  This function generates a 256-element gamma ramp from the specified exponent
         *  and then calls @ref glfwSetGammaRamp with it.  The value must be a finite
         *  number greater than zero.
         *
         *  @param[in] monitor The monitor whose gamma ramp to set.
         *  @param[in] gamma The desired exponent.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref monitor_gamma
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup monitor
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetGamma(GLFWmonitor monitor, float gamma);

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
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup monitor
         */
        public static GLFWgammaramp glfwGetGammaRamp(GLFWmonitor monitor) {
            var internalRamp = InternalGLFW3.glfwGetGammaRamp(monitor);

            //var ramp = (GLFWgammaramp)Marshal.PtrToStructure(new IntPtr(internalRamp), typeof(GLFWgammaramp));
            
            var ramp = new GLFWgammaramp
            {
                red = new ushort[internalRamp->size],
                green = new ushort[internalRamp->size],
                blue = new ushort[internalRamp->size]
            };

            for (uint i = 0; i < ramp.size; i++) {
                ramp.red[i] = internalRamp->red[i];
                ramp.green[i] = internalRamp->green[i];
                ramp.blue[i] = internalRamp->blue[i];
            }

            return ramp;
        }

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
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup monitor
         */
        public static void glfwSetGammaRamp(GLFWmonitor monitor, ref GLFWgammaramp ramp) {
            fixed (ushort* rampRed = ramp.red, rampBlue = ramp.blue, rampGreen = ramp.green) {
                var internalRamp = new InternalGLFWgammaramp {
                    red = rampRed, blue = rampBlue, green = rampGreen, size = (uint)ramp.size
                };
                InternalGLFW3.glfwSetGammaRamp(monitor, internalRamp);
            }
        }

        /*! @brief Resets all window hints to their default values.
         *
         *  This function resets all window hints to their
         *  [default values](@ref window_hints_values).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_hints
         *  @sa glfwWindowHint
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwDefaultWindowHints();

        /*! @brief Sets the specified window hint to the desired value.
         *
         *  This function sets hints for the next call to @ref glfwCreateWindow.  The
         *  hints, once set, retain their values until changed by a call to @ref
         *  glfwWindowHint or @ref glfwDefaultWindowHints, or until the library is
         *  terminated.
         *
         *  @param[in] target The [window hint](@ref window_hints) to set.
         *  @param[in] hint The new value of the window hint.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_hints
         *  @sa glfwDefaultWindowHints
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwOpenWindowHint`.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwWindowHint(int target, int hint);

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
         *  @since Added in GLFW 3.0.  Replaces `glfwOpenWindow`.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern GLFWwindow glfwCreateWindow(int width, int height, [MarshalAs(UnmanagedType.LPStr)] string title, GLFWmonitor monitor, GLFWwindow share);

        /*! @brief Destroys the specified window and its context.
         *
         *  This function destroys the specified window and its context.  On calling
         *  this function, no further callbacks will be called for that window.
         *
         *  If the context of the specified window is current on the main thread, it is
         *  detached before being destroyed.
         *
         *  @param[in] window The window to destroy.
         *
         *  @note The context of the specified window must not be current on any other
         *  thread when this function is called.
         *
         *  @par Reentrancy
         *  This function may not be called from a callback.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_creation
         *  @sa glfwCreateWindow
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwCloseWindow`.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwDestroyWindow(GLFWwindow window);

        /*! @brief Checks the close flag of the specified window.
         *
         *  This function returns the value of the close flag of the specified window.
         *
         *  @param[in] window The window to query.
         *  @return The value of the close flag.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.  Access is not synchronized.
         *
         *  @sa @ref window_close
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool glfwWindowShouldClose(GLFWwindow window);

        /*! @brief Sets the close flag of the specified window.
         *
         *  This function sets the value of the close flag of the specified window.
         *  This can be used to override the user's attempt to close the window, or
         *  to signal that it should be closed.
         *
         *  @param[in] window The window whose flag to change.
         *  @param[in] value The new value.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.  Access is not synchronized.
         *
         *  @sa @ref window_close
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetWindowShouldClose(GLFWwindow window, bool value);

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
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetWindowTitle(GLFWwindow window, [MarshalAs(UnmanagedType.LPStr)] string title);

        /*! @brief Retrieves the position of the client area of the specified window.
         *
         *  This function retrieves the position, in screen coordinates, of the
         *  upper-left corner of the client area of the specified window.
         *
         *  Any or all of the position arguments may be `NULL`.  If an error occurs, all
         *  non-`NULL` position arguments will be set to zero.
         *
         *  @param[in] window The window to query.
         *  @param[out] xpos Where to store the x-coordinate of the upper-left corner of
         *  the client area, or `NULL`.
         *  @param[out] ypos Where to store the y-coordinate of the upper-left corner of
         *  the client area, or `NULL`.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_pos
         *  @sa glfwSetWindowPos
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwGetWindowPos(GLFWwindow window, out int xpos, out int ypos);

        /*! @brief Sets the position of the client area of the specified window.
         *
         *  This function sets the position, in screen coordinates, of the upper-left
         *  corner of the client area of the specified windowed mode window.  If the
         *  window is a full screen window, this function does nothing.
         *
         *  __Do not use this function__ to move an already visible window unless you
         *  have very good reasons for doing so, as it will confuse and annoy the user.
         *
         *  The window manager may put limits on what positions are allowed.  GLFW
         *  cannot and should not override these limits.
         *
         *  @param[in] window The window to query.
         *  @param[in] xpos The x-coordinate of the upper-left corner of the client area.
         *  @param[in] ypos The y-coordinate of the upper-left corner of the client area.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_pos
         *  @sa glfwGetWindowPos
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetWindowPos(GLFWwindow window, int xpos, int ypos);

        /*! @brief Retrieves the size of the client area of the specified window.
         *
         *  This function retrieves the size, in screen coordinates, of the client area
         *  of the specified window.  If you wish to retrieve the size of the
         *  framebuffer of the window in pixels, see @ref glfwGetFramebufferSize.
         *
         *  Any or all of the size arguments may be `NULL`.  If an error occurs, all
         *  non-`NULL` size arguments will be set to zero.
         *
         *  @param[in] window The window whose size to retrieve.
         *  @param[out] width Where to store the width, in screen coordinates, of the
         *  client area, or `NULL`.
         *  @param[out] height Where to store the height, in screen coordinates, of the
         *  client area, or `NULL`.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_size
         *  @sa glfwSetWindowSize
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwGetWindowSize(GLFWwindow window, out int width, out int height);

        /*! @brief Sets the size of the client area of the specified window.
         *
         *  This function sets the size, in screen coordinates, of the client area of
         *  the specified window.
         *
         *  For full screen windows, this function selects and switches to the resolution
         *  closest to the specified size, without affecting the window's context.  As
         *  the context is unaffected, the bit depths of the framebuffer remain
         *  unchanged.
         *
         *  The window manager may put limits on what sizes are allowed.  GLFW cannot
         *  and should not override these limits.
         *
         *  @param[in] window The window to resize.
         *  @param[in] width The desired width of the specified window.
         *  @param[in] height The desired height of the specified window.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_size
         *  @sa glfwGetWindowSize
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetWindowSize(GLFWwindow window, int width, int height);

        /*! @brief Retrieves the size of the framebuffer of the specified window.
         *
         *  This function retrieves the size, in pixels, of the framebuffer of the
         *  specified window.  If you wish to retrieve the size of the window in screen
         *  coordinates, see @ref glfwGetWindowSize.
         *
         *  Any or all of the size arguments may be `NULL`.  If an error occurs, all
         *  non-`NULL` size arguments will be set to zero.
         *
         *  @param[in] window The window whose framebuffer to query.
         *  @param[out] width Where to store the width, in pixels, of the framebuffer,
         *  or `NULL`.
         *  @param[out] height Where to store the height, in pixels, of the framebuffer,
         *  or `NULL`.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_fbsize
         *  @sa glfwSetFramebufferSizeCallback
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwGetFramebufferSize(GLFWwindow window, out int width, out int height);

        /*! @brief Retrieves the size of the frame of the window.
         *
         *  This function retrieves the size, in screen coordinates, of each edge of the
         *  frame of the specified window.  This size includes the title bar, if the
         *  window has one.  The size of the frame may vary depending on the
         *  [window-related hints](@ref window_hints_wnd) used to create it.
         *
         *  Because this function retrieves the size of each window frame edge and not
         *  the offset along a particular coordinate axis, the retrieved values will
         *  always be zero or positive.
         *
         *  Any or all of the size arguments may be `NULL`.  If an error occurs, all
         *  non-`NULL` size arguments will be set to zero.
         *
         *  @param[in] window The window whose frame size to query.
         *  @param[out] left Where to store the size, in screen coordinates, of the left
         *  edge of the window frame, or `NULL`.
         *  @param[out] top Where to store the size, in screen coordinates, of the top
         *  edge of the window frame, or `NULL`.
         *  @param[out] right Where to store the size, in screen coordinates, of the
         *  right edge of the window frame, or `NULL`.
         *  @param[out] bottom Where to store the size, in screen coordinates, of the
         *  bottom edge of the window frame, or `NULL`.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_size
         *
         *  @since Added in GLFW 3.1.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwGetWindowFrameSize(GLFWwindow window, out int left, out int top, out int right, out int bottom);

        /*! @brief Iconifies the specified window.
         *
         *  This function iconifies (minimizes) the specified window if it was
         *  previously restored.  If the window is already iconified, this function does
         *  nothing.
         *
         *  If the specified window is a full screen window, the original monitor
         *  resolution is restored until the window is restored.
         *
         *  @param[in] window The window to iconify.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_iconify
         *  @sa glfwRestoreWindow
         *
         *  @since Added in GLFW 2.1.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwIconifyWindow(GLFWwindow window);

        /*! @brief Restores the specified window.
         *
         *  This function restores the specified window if it was previously iconified
         *  (minimized).  If the window is already restored, this function does nothing.
         *
         *  If the specified window is a full screen window, the resolution chosen for
         *  the window is restored on the selected monitor.
         *
         *  @param[in] window The window to restore.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_iconify
         *  @sa glfwIconifyWindow
         *
         *  @since Added in GLFW 2.1.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwRestoreWindow(GLFWwindow window);

        /*! @brief Makes the specified window visible.
         *
         *  This function makes the specified window visible if it was previously
         *  hidden.  If the window is already visible or is in full screen mode, this
         *  function does nothing.
         *
         *  @param[in] window The window to make visible.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_hide
         *  @sa glfwHideWindow
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwShowWindow(GLFWwindow window);

        /*! @brief Hides the specified window.
         *
         *  This function hides the specified window if it was previously visible.  If
         *  the window is already hidden or is in full screen mode, this function does
         *  nothing.
         *
         *  @param[in] window The window to hide.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_hide
         *  @sa glfwShowWindow
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwHideWindow(GLFWwindow window);

        /*! @brief Returns the monitor that the window uses for full screen mode.
         *
         *  This function returns the handle of the monitor that the specified window is
         *  in full screen on.
         *
         *  @param[in] window The window to query.
         *  @return The monitor, or `NULL` if the window is in windowed mode or an error
         *  occurred.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_monitor
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFWmonitor glfwGetWindowMonitor(GLFWwindow window);

        /*! @brief Returns an attribute of the specified window.
         *
         *  This function returns the value of an attribute of the specified window or
         *  its OpenGL or OpenGL ES context.
         *
         *  @param[in] window The window to query.
         *  @param[in] attrib The [window attribute](@ref window_attribs) whose value to
         *  return.
         *  @return The value of the attribute, or zero if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_attribs
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwGetWindowParam` and
         *  `glfwGetGLVersion`.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern int glfwGetWindowAttrib(GLFWwindow window, int attrib);

        /*! @brief Sets the user pointer of the specified window.
         *
         *  This function sets the user-defined pointer of the specified window.  The
         *  current value is retained until the window is destroyed.  The initial value
         *  is `NULL`.
         *
         *  @param[in] window The window whose pointer to set.
         *  @param[in] pointer The new value.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.  Access is not synchronized.
         *
         *  @sa @ref window_userptr
         *  @sa glfwGetWindowUserPointer
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetWindowUserPointer(GLFWwindow window, IntPtr pointer);

        /*! @brief Returns the user pointer of the specified window.
         *
         *  This function returns the current value of the user-defined pointer of the
         *  specified window.  The initial value is `NULL`.
         *
         *  @param[in] window The window whose pointer to return.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.  Access is not synchronized.
         *
         *  @sa @ref window_userptr
         *  @sa glfwSetWindowUserPointer
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr glfwGetWindowUserPointer(GLFWwindow window);

        /*! @brief Sets the position callback for the specified window.
         *
         *  This function sets the position callback of the specified window, which is
         *  called when the window is moved.  The callback is provided with the screen
         *  position of the upper-left corner of the client area of the window.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_pos
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWwindowposfun glfwSetWindowPosCallback(GLFWwindow window, GLFW3.GLFWwindowposfun cbfun);

        /*! @brief Sets the size callback for the specified window.
         *
         *  This function sets the size callback of the specified window, which is
         *  called when the window is resized.  The callback is provided with the size,
         *  in screen coordinates, of the client area of the window.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_size
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.  Updated callback signature.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWwindowsizefun glfwSetWindowSizeCallback(GLFWwindow window, GLFW3.GLFWwindowsizefun cbfun);

        /*! @brief Sets the close callback for the specified window.
         *
         *  This function sets the close callback of the specified window, which is
         *  called when the user attempts to close the window, for example by clicking
         *  the close widget in the title bar.
         *
         *  The close flag is set before this callback is called, but you can modify it
         *  at any time with @ref glfwSetWindowShouldClose.
         *
         *  The close callback is not triggered by @ref glfwDestroyWindow.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @remarks __OS X:__ Selecting Quit from the application menu will
         *  trigger the close callback for all windows.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_close
         *
         *  @since Added in GLFW 2.5.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.  Updated callback signature.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWwindowclosefun glfwSetWindowCloseCallback(GLFWwindow window, GLFW3.GLFWwindowclosefun cbfun);

        /*! @brief Sets the refresh callback for the specified window.
         *
         *  This function sets the refresh callback of the specified window, which is
         *  called when the client area of the window needs to be redrawn, for example
         *  if the window has been exposed after having been covered by another window.
         *
         *  On compositing window systems such as Aero, Compiz or Aqua, where the window
         *  contents are saved off-screen, this callback may be called only very
         *  infrequently or never at all.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_refresh
         *
         *  @since Added in GLFW 2.5.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.  Updated callback signature.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWwindowrefreshfun glfwSetWindowRefreshCallback(GLFWwindow window, GLFW3.GLFWwindowrefreshfun cbfun);

        /*! @brief Sets the focus callback for the specified window.
         *
         *  This function sets the focus callback of the specified window, which is
         *  called when the window gains or loses input focus.
         *
         *  After the focus callback is called for a window that lost input focus,
         *  synthetic key and mouse button release events will be generated for all such
         *  that had been pressed.  For more information, see @ref glfwSetKeyCallback
         *  and @ref glfwSetMouseButtonCallback.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_focus
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWwindowfocusfun glfwSetWindowFocusCallback(GLFWwindow window, GLFW3.GLFWwindowfocusfun cbfun);

        /*! @brief Sets the iconify callback for the specified window.
         *
         *  This function sets the iconification callback of the specified window, which
         *  is called when the window is iconified or restored.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_iconify
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWwindowiconifyfun glfwSetWindowIconifyCallback(GLFWwindow window, GLFW3.GLFWwindowiconifyfun cbfun);

        /*! @brief Sets the framebuffer resize callback for the specified window.
         *
         *  This function sets the framebuffer resize callback of the specified window,
         *  which is called when the framebuffer of the specified window is resized.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref window_fbsize
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWframebuffersizefun glfwSetFramebufferSizeCallback(GLFWwindow window, GLFW3.GLFWframebuffersizefun cbfun);

        /*! @brief Processes all pending events.
         *
         *  This function processes only those events that are already in the event
         *  queue and then returns immediately.  Processing events will cause the window
         *  and input callbacks associated with those events to be called.
         *
         *  On some platforms, a window move, resize or menu operation will cause event
         *  processing to block.  This is due to how event processing is designed on
         *  those platforms.  You can use the
         *  [window refresh callback](@ref window_refresh) to redraw the contents of
         *  your window when necessary during such operations.
         *
         *  On some platforms, certain events are sent directly to the application
         *  without going through the event queue, causing callbacks to be called
         *  outside of a call to one of the event processing functions.
         *
         *  Event processing is not required for joystick input to work.
         *
         *  @par Reentrancy
         *  This function may not be called from a callback.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref events
         *  @sa glfwWaitEvents
         *
         *  @since Added in GLFW 1.0.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void glfwPollEvents();

        /*! @brief Waits until events are queued and processes them.
         *
         *  This function puts the calling thread to sleep until at least one event is
         *  available in the event queue.  Once one or more events are available,
         *  it behaves exactly like @ref glfwPollEvents, i.e. the events in the queue
         *  are processed and the function then returns immediately.  Processing events
         *  will cause the window and input callbacks associated with those events to be
         *  called.
         *
         *  Since not all events are associated with callbacks, this function may return
         *  without a callback having been called even if you are monitoring all
         *  callbacks.
         *
         *  On some platforms, a window move, resize or menu operation will cause event
         *  processing to block.  This is due to how event processing is designed on
         *  those platforms.  You can use the
         *  [window refresh callback](@ref window_refresh) to redraw the contents of
         *  your window when necessary during such operations.
         *
         *  On some platforms, certain callbacks may be called outside of a call to one
         *  of the event processing functions.
         *
         *  If no windows exist, this function returns immediately.  For synchronization
         *  of threads in applications that do not create windows, use your threading
         *  library of choice.
         *
         *  Event processing is not required for joystick input to work.
         *
         *  @par Reentrancy
         *  This function may not be called from a callback.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref events
         *  @sa glfwPollEvents
         *
         *  @since Added in GLFW 2.5.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwWaitEvents();

        /*! @brief Posts an empty event to the event queue.
         *
         *  This function posts an empty event from the current thread to the event
         *  queue, causing @ref glfwWaitEvents to return.
         *
         *  If no windows exist, this function returns immediately.  For synchronization
         *  of threads in applications that do not create windows, use your threading
         *  library of choice.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref events
         *  @sa glfwWaitEvents
         *
         *  @since Added in GLFW 3.1.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwPostEmptyEvent();

        /*! @brief Returns the value of an input option for the specified window.
         *
         *  This function returns the value of an input option for the specified window.
         *  The mode must be one of `GLFW_CURSOR`, `GLFW_STICKY_KEYS` or
         *  `GLFW_STICKY_MOUSE_BUTTONS`.
         *
         *  @param[in] window The window to query.
         *  @param[in] mode One of `GLFW_CURSOR`, `GLFW_STICKY_KEYS` or
         *  `GLFW_STICKY_MOUSE_BUTTONS`.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa glfwSetInputMode
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern int glfwGetInputMode(GLFWwindow window, int mode);

        /*! @brief Sets an input option for the specified window.
         *
         *  This function sets an input mode option for the specified window.  The mode
         *  must be one of `GLFW_CURSOR`, `GLFW_STICKY_KEYS` or
         *  `GLFW_STICKY_MOUSE_BUTTONS`.
         *
         *  If the mode is `GLFW_CURSOR`, the value must be one of the following cursor
         *  modes:
         *  - `GLFW_CURSOR_NORMAL` makes the cursor visible and behaving normally.
         *  - `GLFW_CURSOR_HIDDEN` makes the cursor invisible when it is over the client
         *    area of the window but does not restrict the cursor from leaving.
         *  - `GLFW_CURSOR_DISABLED` hides and grabs the cursor, providing virtual
         *    and unlimited cursor movement.  This is useful for implementing for
         *    example 3D camera controls.
         *
         *  If the mode is `GLFW_STICKY_KEYS`, the value must be either `GL_TRUE` to
         *  enable sticky keys, or `GL_FALSE` to disable it.  If sticky keys are
         *  enabled, a key press will ensure that @ref glfwGetKey returns `GLFW_PRESS`
         *  the next time it is called even if the key had been released before the
         *  call.  This is useful when you are only interested in whether keys have been
         *  pressed but not when or in which order.
         *
         *  If the mode is `GLFW_STICKY_MOUSE_BUTTONS`, the value must be either
         *  `GL_TRUE` to enable sticky mouse buttons, or `GL_FALSE` to disable it.  If
         *  sticky mouse buttons are enabled, a mouse button press will ensure that @ref
         *  glfwGetMouseButton returns `GLFW_PRESS` the next time it is called even if
         *  the mouse button had been released before the call.  This is useful when you
         *  are only interested in whether mouse buttons have been pressed but not when
         *  or in which order.
         *
         *  @param[in] window The window whose input mode to set.
         *  @param[in] mode One of `GLFW_CURSOR`, `GLFW_STICKY_KEYS` or
         *  `GLFW_STICKY_MOUSE_BUTTONS`.
         *  @param[in] value The new value of the specified input mode.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa glfwGetInputMode
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwEnable` and `glfwDisable`.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetInputMode(GLFWwindow window, int mode, int value);

        /*! @brief Returns the last reported state of a keyboard key for the specified
         *  window.
         *
         *  This function returns the last state reported for the specified key to the
         *  specified window.  The returned state is one of `GLFW_PRESS` or
         *  `GLFW_RELEASE`.  The higher-level action `GLFW_REPEAT` is only reported to
         *  the key callback.
         *
         *  If the `GLFW_STICKY_KEYS` input mode is enabled, this function returns
         *  `GLFW_PRESS` the first time you call it for a key that was pressed, even if
         *  that key has already been released.
         *
         *  The key functions deal with physical keys, with [key tokens](@ref keys)
         *  named after their use on the standard US keyboard layout.  If you want to
         *  input text, use the Unicode character callback instead.
         *
         *  The [modifier key bit masks](@ref mods) are not key tokens and cannot be
         *  used with this function.
         *
         *  @param[in] window The desired window.
         *  @param[in] key The desired [keyboard key](@ref keys).  `GLFW_KEY_UNKNOWN` is
         *  not a valid key for this function.
         *  @return One of `GLFW_PRESS` or `GLFW_RELEASE`.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref input_key
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern int glfwGetKey(GLFWwindow window, int key);

        /*! @brief Returns the last reported state of a mouse button for the specified
         *  window.
         *
         *  This function returns the last state reported for the specified mouse button
         *  to the specified window.  The returned state is one of `GLFW_PRESS` or
         *  `GLFW_RELEASE`.
         *
         *  If the `GLFW_STICKY_MOUSE_BUTTONS` input mode is enabled, this function
         *  `GLFW_PRESS` the first time you call it for a mouse button that was pressed,
         *  even if that mouse button has already been released.
         *
         *  @param[in] window The desired window.
         *  @param[in] button The desired [mouse button](@ref buttons).
         *  @return One of `GLFW_PRESS` or `GLFW_RELEASE`.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref input_mouse_button
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern int glfwGetMouseButton(GLFWwindow window, int button);

        /*! @brief Retrieves the position of the cursor relative to the client area of
         *  the window.
         *
         *  This function returns the position of the cursor, in screen coordinates,
         *  relative to the upper-left corner of the client area of the specified
         *  window.
         *
         *  If the cursor is disabled (with `GLFW_CURSOR_DISABLED`) then the cursor
         *  position is unbounded and limited only by the minimum and maximum values of
         *  a `double`.
         *
         *  The coordinate can be converted to their integer equivalents with the
         *  `floor` function.  Casting directly to an integer type works for positive
         *  coordinates, but fails for negative ones.
         *
         *  Any or all of the position arguments may be `NULL`.  If an error occurs, all
         *  non-`NULL` position arguments will be set to zero.
         *
         *  @param[in] window The desired window.
         *  @param[out] xpos Where to store the cursor x-coordinate, relative to the
         *  left edge of the client area, or `NULL`.
         *  @param[out] ypos Where to store the cursor y-coordinate, relative to the to
         *  top edge of the client area, or `NULL`.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref cursor_pos
         *  @sa glfwSetCursorPos
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwGetMousePos`.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwGetCursorPos(GLFWwindow window, out double xpos, out double ypos);

        /*! @brief Sets the position of the cursor, relative to the client area of the
         *  window.
         *
         *  This function sets the position, in screen coordinates, of the cursor
         *  relative to the upper-left corner of the client area of the specified
         *  window.  The window must have input focus.  If the window does not have
         *  input focus when this function is called, it fails silently.
         *
         *  __Do not use this function__ to implement things like camera controls.  GLFW
         *  already provides the `GLFW_CURSOR_DISABLED` cursor mode that hides the
         *  cursor, transparently re-centers it and provides unconstrained cursor
         *  motion.  See @ref glfwSetInputMode for more information.
         *
         *  If the cursor mode is `GLFW_CURSOR_DISABLED` then the cursor position is
         *  unconstrained and limited only by the minimum and maximum values of
         *  a `double`.
         *
         *  @param[in] window The desired window.
         *  @param[in] xpos The desired x-coordinate, relative to the left edge of the
         *  client area.
         *  @param[in] ypos The desired y-coordinate, relative to the top edge of the
         *  client area.
         *
         *  @remarks __X11:__ Due to the asynchronous nature of a modern X desktop, it
         *  may take a moment for the window focus event to arrive.  This means you will
         *  not be able to set the cursor position directly after window creation.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref cursor_pos
         *  @sa glfwGetCursorPos
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwSetMousePos`.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetCursorPos(GLFWwindow window, double xpos, double ypos);

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
         *  @since Added in GLFW 3.1.
         *
         *  @ingroup input
         */
        public static GLFWcursor glfwCreateCursor(GLFWimage image, int xhot, int yhot) {
            fixed (byte* imagePixels = image.pixels) {
                var internalImage = new InternalGLFWimage { width = image.width, height = image.height, pixels = imagePixels };
                return InternalGLFW3.glfwCreateCursor(internalImage, xhot, yhot);
            }
        }

        /*! @brief Creates a cursor with a standard shape.
         *
         *  Returns a cursor with a [standard shape](@ref shapes), that can be set for
         *  a window with @ref glfwSetCursor.
         *
         *  @param[in] shape One of the [standard shapes](@ref shapes).
         *
         *  @return A new cursor ready to use or `NULL` if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Reentrancy
         *  This function may not be called from a callback.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref cursor_object
         *  @sa glfwCreateCursor
         *
         *  @since Added in GLFW 3.1.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFWcursor glfwCreateStandardCursor(int shape);

        /*! @brief Destroys a cursor.
         *
         *  This function destroys a cursor previously created with @ref
         *  glfwCreateCursor.  Any remaining cursors will be destroyed by @ref
         *  glfwTerminate.
         *
         *  @param[in] cursor The cursor object to destroy.
         *
         *  @par Reentrancy
         *  This function may not be called from a callback.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref cursor_object
         *  @sa glfwCreateCursor
         *
         *  @since Added in GLFW 3.1.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwDestroyCursor(GLFWcursor cursor);

        /*! @brief Sets the cursor for the window.
         *
         *  This function sets the cursor image to be used when the cursor is over the
         *  client area of the specified window.  The set cursor will only be visible
         *  when the [cursor mode](@ref cursor_mode) of the window is
         *  `GLFW_CURSOR_NORMAL`.
         *
         *  On some platforms, the set cursor may not be visible unless the window also
         *  has input focus.
         *
         *  @param[in] window The window to set the cursor for.
         *  @param[in] cursor The cursor to set, or `NULL` to switch back to the default
         *  arrow cursor.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref cursor_object
         *
         *  @since Added in GLFW 3.1.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetCursor(GLFWwindow window, GLFWcursor cursor);

        /*! @brief Sets the key callback.
         *
         *  This function sets the key callback of the specified window, which is called
         *  when a key is pressed, repeated or released.
         *
         *  The key functions deal with physical keys, with layout independent
         *  [key tokens](@ref keys) named after their values in the standard US keyboard
         *  layout.  If you want to input text, use the
         *  [character callback](@ref glfwSetCharCallback) instead.
         *
         *  When a window loses input focus, it will generate synthetic key release
         *  events for all pressed keys.  You can tell these events from user-generated
         *  events by the fact that the synthetic ones are generated after the focus
         *  loss event has been processed, i.e. after the
         *  [window focus callback](@ref glfwSetWindowFocusCallback) has been called.
         *
         *  The scancode of a key is specific to that platform or sometimes even to that
         *  machine.  Scancodes are intended to allow users to bind keys that don't have
         *  a GLFW key token.  Such keys have `key` set to `GLFW_KEY_UNKNOWN`, their
         *  state is not saved and so it cannot be queried with @ref glfwGetKey.
         *
         *  Sometimes GLFW needs to generate synthetic key events, in which case the
         *  scancode may be zero.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new key callback, or `NULL` to remove the currently
         *  set callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref input_key
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.  Updated callback signature.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWkeyfun glfwSetKeyCallback(GLFWwindow window, GLFW3.GLFWkeyfun cbfun);

        /*! @brief Sets the Unicode character callback.
         *
         *  This function sets the character callback of the specified window, which is
         *  called when a Unicode character is input.
         *
         *  The character callback is intended for Unicode text input.  As it deals with
         *  characters, it is keyboard layout dependent, whereas the
         *  [key callback](@ref glfwSetKeyCallback) is not.  Characters do not map 1:1
         *  to physical keys, as a key may produce zero, one or more characters.  If you
         *  want to know whether a specific physical key was pressed or released, see
         *  the key callback instead.
         *
         *  The character callback behaves as system text input normally does and will
         *  not be called if modifier keys are held down that would prevent normal text
         *  input on that platform, for example a Super (Command) key on OS X or Alt key
         *  on Windows.  There is a
         *  [character with modifiers callback](@ref glfwSetCharModsCallback) that
         *  receives these events.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref input_char
         *
         *  @since Added in GLFW 2.4.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.  Updated callback signature.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWcharfun glfwSetCharCallback(GLFWwindow window, GLFW3.GLFWcharfun cbfun);

        /*! @brief Sets the Unicode character with modifiers callback.
         *
         *  This function sets the character with modifiers callback of the specified
         *  window, which is called when a Unicode character is input regardless of what
         *  modifier keys are used.
         *
         *  The character with modifiers callback is intended for implementing custom
         *  Unicode character input.  For regular Unicode text input, see the
         *  [character callback](@ref glfwSetCharCallback).  Like the character
         *  callback, the character with modifiers callback deals with characters and is
         *  keyboard layout dependent.  Characters do not map 1:1 to physical keys, as
         *  a key may produce zero, one or more characters.  If you want to know whether
         *  a specific physical key was pressed or released, see the
         *  [key callback](@ref glfwSetKeyCallback) instead.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or an
         *  error occurred.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref input_char
         *
         *  @since Added in GLFW 3.1.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWcharmodsfun glfwSetCharModsCallback(GLFWwindow window, GLFW3.GLFWcharmodsfun cbfun);

        /*! @brief Sets the mouse button callback.
         *
         *  This function sets the mouse button callback of the specified window, which
         *  is called when a mouse button is pressed or released.
         *
         *  When a window loses input focus, it will generate synthetic mouse button
         *  release events for all pressed mouse buttons.  You can tell these events
         *  from user-generated events by the fact that the synthetic ones are generated
         *  after the focus loss event has been processed, i.e. after the
         *  [window focus callback](@ref glfwSetWindowFocusCallback) has been called.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref input_mouse_button
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.  Updated callback signature.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWmousebuttonfun glfwSetMouseButtonCallback(GLFWwindow window, GLFW3.GLFWmousebuttonfun cbfun);

        /*! @brief Sets the cursor position callback.
         *
         *  This function sets the cursor position callback of the specified window,
         *  which is called when the cursor is moved.  The callback is provided with the
         *  position, in screen coordinates, relative to the upper-left corner of the
         *  client area of the window.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref cursor_pos
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwSetMousePosCallback`.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWcursorposfun glfwSetCursorPosCallback(GLFWwindow window, GLFW3.GLFWcursorposfun cbfun);

        /*! @brief Sets the cursor enter/exit callback.
         *
         *  This function sets the cursor boundary crossing callback of the specified
         *  window, which is called when the cursor enters or leaves the client area of
         *  the window.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new callback, or `NULL` to remove the currently set
         *  callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref cursor_enter
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWcursorenterfun glfwSetCursorEnterCallback(GLFWwindow window, GLFW3.GLFWcursorenterfun cbfun);

        /*! @brief Sets the scroll callback.
         *
         *  This function sets the scroll callback of the specified window, which is
         *  called when a scrolling device is used, such as a mouse wheel or scrolling
         *  area of a touchpad.
         *
         *  The scroll callback receives all scrolling input, like that from a mouse
         *  wheel or a touchpad scrolling area.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new scroll callback, or `NULL` to remove the currently
         *  set callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref scrolling
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwSetMouseWheelCallback`.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWscrollfun glfwSetScrollCallback(GLFWwindow window, GLFW3.GLFWscrollfun cbfun);

        /*! @brief Sets the file drop callback.
         *
         *  This function sets the file drop callback of the specified window, which is
         *  called when one or more dragged files are dropped on the window.
         *
         *  Because the path array and its strings may have been generated specifically
         *  for that event, they are not guaranteed to be valid after the callback has
         *  returned.  If you wish to use them after the callback returns, you need to
         *  make a deep copy.
         *
         *  @param[in] window The window whose callback to set.
         *  @param[in] cbfun The new file drop callback, or `NULL` to remove the
         *  currently set callback.
         *  @return The previously set callback, or `NULL` if no callback was set or the
         *  library had not been [initialized](@ref intro_init).
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref path_drop
         *
         *  @since Added in GLFW 3.1.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWdropfun glfwSetDropCallback(GLFWwindow window, GLFW3.GLFWdropfun cbfun);

        /*! @brief Returns whether the specified joystick is present.
         *
         *  This function returns whether the specified joystick is present.
         *
         *  @param[in] joy The [joystick](@ref joysticks) to query.
         *  @return `GL_TRUE` if the joystick is present, or `GL_FALSE` otherwise.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref joystick
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwGetJoystickParam`.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern int glfwJoystickPresent(int joy);

        /*! @brief Returns the values of all axes of the specified joystick.
         *
         *  This function returns the values of all axes of the specified joystick.
         *  Each element in the array is a value between -1.0 and 1.0.
         *
         *  @param[in] joy The [joystick](@ref joysticks) to query.
         *  @param[out] count Where to store the number of axis values in the returned
         *  array.  This is set to zero if an error occurred.
         *  @return An array of axis values, or `NULL` if the joystick is not present.
         *
         *  @par Pointer Lifetime
         *  The returned array is allocated and freed by GLFW.  You should not free it
         *  yourself.  It is valid until the specified joystick is disconnected, this
         *  function is called again for that joystick or the library is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref joystick_axis
         *
         *  @since Added in GLFW 3.0.  Replaces `glfwGetJoystickPos`.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.LPArray)]
        public static extern float[] glfwGetJoystickAxes(int joy, out int count);

        /*! @brief Returns the state of all buttons of the specified joystick.
         *
         *  This function returns the state of all buttons of the specified joystick.
         *  Each element in the array is either `GLFW_PRESS` or `GLFW_RELEASE`.
         *
         *  @param[in] joy The [joystick](@ref joysticks) to query.
         *  @param[out] count Where to store the number of button states in the returned
         *  array.  This is set to zero if an error occurred.
         *  @return An array of button states, or `NULL` if the joystick is not present.
         *
         *  @par Pointer Lifetime
         *  The returned array is allocated and freed by GLFW.  You should not free it
         *  yourself.  It is valid until the specified joystick is disconnected, this
         *  function is called again for that joystick or the library is terminated.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref joystick_button
         *
         *  @since Added in GLFW 2.2.
         *
         *  @par
         *  __GLFW 3:__ Changed to return a dynamic array.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.LPArray)]
        public static extern byte[] glfwGetJoystickButtons(int joy, out int count);

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
        public static string glfwGetJoystickName(int joy) {
            return new string(InternalGLFW3.glfwGetJoystickName(joy));
        }

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
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetClipboardString(GLFWwindow window, [MarshalAs(UnmanagedType.LPStr)] string str);

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
        public static string glfwGetClipboardString(GLFWwindow window) {
            return new string(InternalGLFW3.glfwGetClipboardString(window));
        }

        /*! @brief Returns the value of the GLFW timer.
         *
         *  This function returns the value of the GLFW timer.  Unless the timer has
         *  been set using @ref glfwSetTime, the timer measures time elapsed since GLFW
         *  was initialized.
         *
         *  The resolution of the timer is system dependent, but is usually on the order
         *  of a few micro- or nanoseconds.  It uses the highest-resolution monotonic
         *  time source on each supported platform.
         *
         *  @return The current value, in seconds, or zero if an
         *  [error](@ref error_handling) occurred.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.  Access is not synchronized.
         *
         *  @sa @ref time
         *
         *  @since Added in GLFW 1.0.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern double glfwGetTime();

        /*! @brief Sets the GLFW timer.
         *
         *  This function sets the value of the GLFW timer.  It then continues to count
         *  up from that value.  The value must be a positive finite number less than
         *  or equal to 18446744073.0, which is approximately 584.5 years.
         *
         *  @param[in] time The new value, in seconds.
         *
         *  @remarks The upper limit of the timer is calculated as
         *  floor((2<sup>64</sup> - 1) / 10<sup>9</sup>) and is due to implementations
         *  storing nanoseconds in 64 bits.  The limit may be increased in the future.
         *
         *  @par Thread Safety
         *  This function may only be called from the main thread.
         *
         *  @sa @ref time
         *
         *  @since Added in GLFW 2.2.
         *
         *  @ingroup input
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSetTime(double time);

        /*! @brief Makes the context of the specified window current for the calling
         *  thread.
         *
         *  This function makes the OpenGL or OpenGL ES context of the specified window
         *  current on the calling thread.  A context can only be made current on
         *  a single thread at a time and each thread can have only a single current
         *  context at a time.
         *
         *  By default, making a context non-current implicitly forces a pipeline flush.
         *  On machines that support `GL_KHR_context_flush_control`, you can control
         *  whether a context performs this flush by setting the
         *  [GLFW_CONTEXT_RELEASE_BEHAVIOR](@ref window_hints_ctx) window hint.
         *
         *  @param[in] window The window whose context to make current, or `NULL` to
         *  detach the current context.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref context_current
         *  @sa glfwGetCurrentContext
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup context
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwMakeContextCurrent(GLFWwindow window);

        /*! @brief Returns the window whose context is current on the calling thread.
         *
         *  This function returns the window whose OpenGL or OpenGL ES context is
         *  current on the calling thread.
         *
         *  @return The window whose context is current, or `NULL` if no window's
         *  context is current.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref context_current
         *  @sa glfwMakeContextCurrent
         *
         *  @since Added in GLFW 3.0.
         *
         *  @ingroup context
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.LPStruct)]
        public static extern GLFWwindow glfwGetCurrentContext();

        /*! @brief Swaps the front and back buffers of the specified window.
         *
         *  This function swaps the front and back buffers of the specified window.  If
         *  the swap interval is greater than zero, the GPU driver waits the specified
         *  number of screen updates before swapping the buffers.
         *
         *  @param[in] window The window whose buffers to swap.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref buffer_swap
         *  @sa glfwSwapInterval
         *
         *  @since Added in GLFW 1.0.
         *
         *  @par
         *  __GLFW 3:__ Added window handle parameter.
         *
         *  @ingroup window
         */
        [DllImport(NATIVE, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSwapBuffers(GLFWwindow window);

        /*! @brief Sets the swap interval for the current context.
         *
         *  This function sets the swap interval for the current context, i.e. the
         *  number of screen updates to wait from the time @ref glfwSwapBuffers was
         *  called before swapping the buffers and returning.  This is sometimes called
         *  _vertical synchronization_, _vertical retrace synchronization_ or just
         *  _vsync_.
         *
         *  Contexts that support either of the `WGL_EXT_swap_control_tear` and
         *  `GLX_EXT_swap_control_tear` extensions also accept negative swap intervals,
         *  which allow the driver to swap even if a frame arrives a little bit late.
         *  You can check for the presence of these extensions using @ref
         *  glfwExtensionSupported.  For more information about swap tearing, see the
         *  extension specifications.
         *
         *  A context must be current on the calling thread.  Calling this function
         *  without a current context will cause a @ref GLFW_NO_CURRENT_CONTEXT error.
         *
         *  @param[in] interval The minimum number of screen updates to wait for
         *  until the buffers are swapped by @ref glfwSwapBuffers.
         *
         *  @remarks This function is not called during context creation, leaving the
         *  swap interval set to whatever is the default on that platform.  This is done
         *  because some swap interval extensions used by GLFW do not allow the swap
         *  interval to be reset to zero once it has been set to a non-zero value.
         *
         *  @remarks Some GPU drivers do not honor the requested swap interval, either
         *  because of a user setting that overrides the application's request or due to
         *  bugs in the driver.
         *
         *  @par Thread Safety
         *  This function may be called from any thread.
         *
         *  @sa @ref buffer_swap
         *  @sa glfwSwapBuffers
         *
         *  @since Added in GLFW 1.0.
         *
         *  @ingroup context
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern void glfwSwapInterval(int interval);

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
         *  @since Added in GLFW 1.0.
         *
         *  @ingroup context
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool glfwExtensionSupported([MarshalAs(UnmanagedType.LPStr)] string extension);

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
         *  @since Added in GLFW 1.0.
         *
         *  @ingroup context
         */
        [DllImport(NATIVE), SuppressUnmanagedCodeSecurity]
        public static extern GLFW3.GLFWglproc glfwGetProcAddress([MarshalAs(UnmanagedType.LPStr)] string procname);
        #endregion GLFW API functions
    }
}
