namespace GLFWnet.Binding {
    public partial class GLFW3 {
        /*************************************************************************
         * GL API tokens
         *************************************************************************/
        
        public const int GL_TRUE = 1;

        public const int GL_FALSE = 0;

        /*************************************************************************
         * GLFW API tokens
         *************************************************************************/

        /*! @name GLFW version macros
         *  @{ */
        /*! @brief The major version number of the GLFW library.
         *
         *  This is incremented when the API is changed in non-compatible ways.
         *  @ingroup init
         */
        public const int GLFW_VERSION_MAJOR = 3;
        /*! @brief The minor version number of the GLFW library.
         *
         *  This is incremented when features are added to the API but it remains
         *  backward-compatible.
         *  @ingroup init
         */
        public const int GLFW_VERSION_MINOR = 1;
        /*! @brief The revision number of the GLFW library.
         *
         *  This is incremented when a bug fix release is made that does not contain any
         *  API changes.
         *  @ingroup init
         */
        public const int GLFW_VERSION_REVISION = 1;
        /*! @} */

        /*! @name Key and button actions
         *  @{ */
        /*! @brief The key or mouse button was released.
         *
         *  The key or mouse button was released.
         *
         *  @ingroup input
         */
        public const int GLFW_RELEASE = 0;
        /*! @brief The key or mouse button was pressed.
         *
         *  The key or mouse button was pressed.
         *
         *  @ingroup input
         */
        public const int GLFW_PRESS = 1;
        /*! @brief The key was held down until it repeated.
         *
         *  The key was held down until it repeated.
         *
         *  @ingroup input
         */
        public const int GLFW_REPEAT = 2;
        /*! @} */

        /*! @defgroup keys Keyboard keys
         *
         *  See [key input](@ref input_key) for how these are used.
         *
         *  These key codes are inspired by the _USB HID Usage Tables v1.12_ (p. 53-60),
         *  but re-arranged to map to 7-bit ASCII for printable keys (function keys are
         *  put in the 256+ range).
         *
         *  The naming of the key codes follow these rules:
         *   - The US keyboard layout is used
         *   - Names of printable alpha-numeric characters are used (e.g. "A", "R",
         *     "3", etc.)
         *   - For non-alphanumeric characters, Unicode:ish names are used (e.g.
         *     "COMMA", "LEFT_SQUARE_BRACKET", etc.). Note that some names do not
         *     correspond to the Unicode standard (usually for brevity)
         *   - Keys that lack a clear US mapping are named "WORLD_x"
         *   - For non-printable keys, custom names are used (e.g. "F4",
         *     "BACKSPACE", etc.)
         *
         *  @ingroup input
         *  @{
         */

        /* The unknown key */
        public const int GLFW_KEY_UNKNOWN = -1;

        /* Printable keys */
        public const int GLFW_KEY_SPACE = 32;
        public const int GLFW_KEY_APOSTROPHE = 39;  /* ' */
        public const int GLFW_KEY_COMMA = 44;  /* , */
        public const int GLFW_KEY_MINUS = 45;  /* - */
        public const int GLFW_KEY_PERIOD = 46;  /* . */
        public const int GLFW_KEY_SLASH = 47;  /* / */
        public const int GLFW_KEY_0 = 48;
        public const int GLFW_KEY_1 = 49;
        public const int GLFW_KEY_2 = 50;
        public const int GLFW_KEY_3 = 51;
        public const int GLFW_KEY_4 = 52;
        public const int GLFW_KEY_5 = 53;
        public const int GLFW_KEY_6 = 54;
        public const int GLFW_KEY_7 = 55;
        public const int GLFW_KEY_8 = 56;
        public const int GLFW_KEY_9 = 57;
        public const int GLFW_KEY_SEMICOLON = 59;  /* ; */
        public const int GLFW_KEY_EQUAL = 61;  /* = */
        public const int GLFW_KEY_A = 65;
        public const int GLFW_KEY_B = 66;
        public const int GLFW_KEY_C = 67;
        public const int GLFW_KEY_D = 68;
        public const int GLFW_KEY_E = 69;
        public const int GLFW_KEY_F = 70;
        public const int GLFW_KEY_G = 71;
        public const int GLFW_KEY_H = 72;
        public const int GLFW_KEY_I = 73;
        public const int GLFW_KEY_J = 74;
        public const int GLFW_KEY_K = 75;
        public const int GLFW_KEY_L = 76;
        public const int GLFW_KEY_M = 77;
        public const int GLFW_KEY_N = 78;
        public const int GLFW_KEY_O = 79;
        public const int GLFW_KEY_P = 80;
        public const int GLFW_KEY_Q = 81;
        public const int GLFW_KEY_R = 82;
        public const int GLFW_KEY_S = 83;
        public const int GLFW_KEY_T = 84;
        public const int GLFW_KEY_U = 85;
        public const int GLFW_KEY_V = 86;
        public const int GLFW_KEY_W = 87;
        public const int GLFW_KEY_X = 88;
        public const int GLFW_KEY_Y = 89;
        public const int GLFW_KEY_Z = 90;
        public const int GLFW_KEY_LEFT_BRACKET = 91;  /* [ */
        public const int GLFW_KEY_BACKSLASH = 92;  /* \ */
        public const int GLFW_KEY_RIGHT_BRACKET = 93;  /* ] */
        public const int GLFW_KEY_GRAVE_ACCENT = 96;  /* ` */
        public const int GLFW_KEY_WORLD_1 = 161; /* non-US #1 */
        public const int GLFW_KEY_WORLD_2 = 162; /* non-US #2 */

        /* Function keys */
        public const int GLFW_KEY_ESCAPE = 256;
        public const int GLFW_KEY_ENTER = 257;
        public const int GLFW_KEY_TAB = 258;
        public const int GLFW_KEY_BACKSPACE = 259;
        public const int GLFW_KEY_INSERT = 260;
        public const int GLFW_KEY_DELETE = 261;
        public const int GLFW_KEY_RIGHT = 262;
        public const int GLFW_KEY_LEFT = 263;
        public const int GLFW_KEY_DOWN = 264;
        public const int GLFW_KEY_UP = 265;
        public const int GLFW_KEY_PAGE_UP = 266;
        public const int GLFW_KEY_PAGE_DOWN = 267;
        public const int GLFW_KEY_HOME = 268;
        public const int GLFW_KEY_END = 269;
        public const int GLFW_KEY_CAPS_LOCK = 280;
        public const int GLFW_KEY_SCROLL_LOCK = 281;
        public const int GLFW_KEY_NUM_LOCK = 282;
        public const int GLFW_KEY_PRINT_SCREEN = 283;
        public const int GLFW_KEY_PAUSE = 284;
        public const int GLFW_KEY_F1 = 290;
        public const int GLFW_KEY_F2 = 291;
        public const int GLFW_KEY_F3 = 292;
        public const int GLFW_KEY_F4 = 293;
        public const int GLFW_KEY_F5 = 294;
        public const int GLFW_KEY_F6 = 295;
        public const int GLFW_KEY_F7 = 296;
        public const int GLFW_KEY_F8 = 297;
        public const int GLFW_KEY_F9 = 298;
        public const int GLFW_KEY_F10 = 299;
        public const int GLFW_KEY_F11 = 300;
        public const int GLFW_KEY_F12 = 301;
        public const int GLFW_KEY_F13 = 302;
        public const int GLFW_KEY_F14 = 303;
        public const int GLFW_KEY_F15 = 304;
        public const int GLFW_KEY_F16 = 305;
        public const int GLFW_KEY_F17 = 306;
        public const int GLFW_KEY_F18 = 307;
        public const int GLFW_KEY_F19 = 308;
        public const int GLFW_KEY_F20 = 309;
        public const int GLFW_KEY_F21 = 310;
        public const int GLFW_KEY_F22 = 311;
        public const int GLFW_KEY_F23 = 312;
        public const int GLFW_KEY_F24 = 313;
        public const int GLFW_KEY_F25 = 314;
        public const int GLFW_KEY_KP_0 = 320;
        public const int GLFW_KEY_KP_1 = 321;
        public const int GLFW_KEY_KP_2 = 322;
        public const int GLFW_KEY_KP_3 = 323;
        public const int GLFW_KEY_KP_4 = 324;
        public const int GLFW_KEY_KP_5 = 325;
        public const int GLFW_KEY_KP_6 = 326;
        public const int GLFW_KEY_KP_7 = 327;
        public const int GLFW_KEY_KP_8 = 328;
        public const int GLFW_KEY_KP_9 = 329;
        public const int GLFW_KEY_KP_DECIMAL = 330;
        public const int GLFW_KEY_KP_DIVIDE = 331;
        public const int GLFW_KEY_KP_MULTIPLY = 332;
        public const int GLFW_KEY_KP_SUBTRACT = 333;
        public const int GLFW_KEY_KP_ADD = 334;
        public const int GLFW_KEY_KP_ENTER = 335;
        public const int GLFW_KEY_KP_EQUAL = 336;
        public const int GLFW_KEY_LEFT_SHIFT = 340;
        public const int GLFW_KEY_LEFT_CONTROL = 341;
        public const int GLFW_KEY_LEFT_ALT = 342;
        public const int GLFW_KEY_LEFT_SUPER = 343;
        public const int GLFW_KEY_RIGHT_SHIFT = 344;
        public const int GLFW_KEY_RIGHT_CONTROL = 345;
        public const int GLFW_KEY_RIGHT_ALT = 346;
        public const int GLFW_KEY_RIGHT_SUPER = 347;
        public const int GLFW_KEY_MENU = 348;
        public const int GLFW_KEY_LAST = GLFW_KEY_MENU;

        /*! @} */

        /*! @defgroup mods Modifier key flags
         *
         *  See [key input](@ref input_key) for how these are used.
         *
         *  @ingroup input
         *  @{ */

        /*! @brief If this bit is set one or more Shift keys were held down.
         */
        public const int GLFW_MOD_SHIFT = 0x0001;
        /*! @brief If this bit is set one or more Control keys were held down.
         */
        public const int GLFW_MOD_CONTROL = 0x0002;
        /*! @brief If this bit is set one or more Alt keys were held down.
         */
        public const int GLFW_MOD_ALT = 0x0004;
        /*! @brief If this bit is set one or more Super keys were held down.
         */
        public const int GLFW_MOD_SUPER = 0x0008;

        /*! @} */

        /*! @defgroup buttons Mouse buttons
         *
         *  See [mouse button input](@ref input_mouse_button) for how these are used.
         *
         *  @ingroup input
         *  @{ */
        public const int GLFW_MOUSE_BUTTON_1 = 0;
        public const int GLFW_MOUSE_BUTTON_2 = 1;
        public const int GLFW_MOUSE_BUTTON_3 = 2;
        public const int GLFW_MOUSE_BUTTON_4 = 3;
        public const int GLFW_MOUSE_BUTTON_5 = 4;
        public const int GLFW_MOUSE_BUTTON_6 = 5;
        public const int GLFW_MOUSE_BUTTON_7 = 6;
        public const int GLFW_MOUSE_BUTTON_8 = 7;
        public const int GLFW_MOUSE_BUTTON_LAST = GLFW_MOUSE_BUTTON_8;
        public const int GLFW_MOUSE_BUTTON_LEFT = GLFW_MOUSE_BUTTON_1;
        public const int GLFW_MOUSE_BUTTON_RIGHT = GLFW_MOUSE_BUTTON_2;
        public const int GLFW_MOUSE_BUTTON_MIDDLE = GLFW_MOUSE_BUTTON_3;
        /*! @} */

        /*! @defgroup joysticks Joysticks
         *
         *  See [joystick input](@ref joystick) for how these are used.
         *
         *  @ingroup input
         *  @{ */
        public const int GLFW_JOYSTICK_1 = 0;
        public const int GLFW_JOYSTICK_2 = 1;
        public const int GLFW_JOYSTICK_3 = 2;
        public const int GLFW_JOYSTICK_4 = 3;
        public const int GLFW_JOYSTICK_5 = 4;
        public const int GLFW_JOYSTICK_6 = 5;
        public const int GLFW_JOYSTICK_7 = 6;
        public const int GLFW_JOYSTICK_8 = 7;
        public const int GLFW_JOYSTICK_9 = 8;
        public const int GLFW_JOYSTICK_10 = 9;
        public const int GLFW_JOYSTICK_11 = 10;
        public const int GLFW_JOYSTICK_12 = 11;
        public const int GLFW_JOYSTICK_13 = 12;
        public const int GLFW_JOYSTICK_14 = 13;
        public const int GLFW_JOYSTICK_15 = 14;
        public const int GLFW_JOYSTICK_16 = 15;
        public const int GLFW_JOYSTICK_LAST = GLFW_JOYSTICK_16;
        /*! @} */

        /*! @defgroup errors Error codes
         *
         *  See [error handling](@ref error_handling) for how these are used.
         *
         *  @ingroup init
         *  @{ */
        /*! @brief GLFW has not been initialized.
         *
         *  This occurs if a GLFW function was called that may not be called unless the
         *  library is [initialized](@ref intro_init).
         *
         *  @par Analysis
         *  Application programmer error.  Initialize GLFW before calling any function
         *  that requires initialization.
         */
        public const int GLFW_NOT_INITIALIZED = 0x00010001;
        /*! @brief No context is current for this thread.
         *
         *  This occurs if a GLFW function was called that needs and operates on the
         *  current OpenGL or OpenGL ES context but no context is current on the calling
         *  thread.  One such function is @ref glfwSwapInterval.
         *
         *  @par Analysis
         *  Application programmer error.  Ensure a context is current before calling
         *  functions that require a current context.
         */
        public const int GLFW_NO_CURRENT_CONTEXT = 0x00010002;
        /*! @brief One of the arguments to the function was an invalid enum value.
         *
         *  One of the arguments to the function was an invalid enum value, for example
         *  requesting [GLFW_RED_BITS](@ref window_hints_fb) with @ref
         *  glfwGetWindowAttrib.
         *
         *  @par Analysis
         *  Application programmer error.  Fix the offending call.
         */
        public const int GLFW_INVALID_ENUM = 0x00010003;
        /*! @brief One of the arguments to the function was an invalid value.
         *
         *  One of the arguments to the function was an invalid value, for example
         *  requesting a non-existent OpenGL or OpenGL ES version like 2.7.
         *
         *  Requesting a valid but unavailable OpenGL or OpenGL ES version will instead
         *  result in a @ref GLFW_VERSION_UNAVAILABLE = error.;
         *
         *  @par Analysis
         *  Application programmer error.  Fix the offending call.
         */
        public const int GLFW_INVALID_VALUE = 0x00010004;
        /*! @brief A memory allocation failed.
         *
         *  A memory allocation failed.
         *
         *  @par Analysis
         *  A bug in GLFW or the underlying operating system.  Report the bug to our
         *  [issue tracker](https://github.com/glfw/glfw/issues).
         */
        public const int GLFW_OUT_OF_MEMORY = 0x00010005;
        /*! @brief GLFW could not find support for the requested client API on the
         *  system.
         *
         *  GLFW could not find support for the requested client API on the system.  If
         *  emitted by functions other than @ref glfwCreateWindow, no supported client
         *  API was found.
         *
         *  @par Analysis
         *  The installed graphics driver does not support the requested client API, or
         *  does not support it via the chosen context creation backend.  Below are
         *  a few examples.
         *
         *  @par
         *  Some pre-installed Windows graphics drivers do not support OpenGL.  AMD only
         *  supports OpenGL ES via EGL, while Nvidia and Intel only supports it via
         *  a WGL or GLX extension.  OS X does not provide OpenGL ES at all.  The Mesa
         *  EGL, OpenGL and OpenGL ES libraries do not interface with the Nvidia binary
         *  driver.
         */
        public const int GLFW_API_UNAVAILABLE = 0x00010006;
        /*! @brief The requested OpenGL or OpenGL ES version is not available.
         *
         *  The requested OpenGL or OpenGL ES version (including any requested context
         *  or framebuffer hints) is not available on this machine.
         *
         *  @par Analysis
         *  The machine does not support your requirements.  If your application is
         *  sufficiently flexible, downgrade your requirements and try again.
         *  Otherwise, inform the user that their machine does not match your
         *  requirements.
         *
         *  @par
         *  Future invalid OpenGL and OpenGL ES versions, for example OpenGL 4.8 if 5.0
         *  comes out before the 4.x series gets that far, also fail with this error and
         *  not @ref GLFW_INVALID_VALUE, because GLFW cannot know what future versions
         *  will exist.
         */
        public const int GLFW_VERSION_UNAVAILABLE = 0x00010007;
        /*! @brief A platform-specific error occurred that does not match any of the
         *  more specific categories.
         *
         *  A platform-specific error occurred that does not match any of the more
         *  specific categories.
         *
         *  @par Analysis
         *  A bug or configuration error in GLFW, the underlying operating system or
         *  its drivers, or a lack of required resources.  Report the issue to our
         *  [issue tracker](https://github.com/glfw/glfw/issues).
         */
        public const int GLFW_PLATFORM_ERROR = 0x00010008;
        /*! @brief The requested format is not supported or available.
         *
         *  If emitted during window creation, the requested pixel format is not
         *  supported.
         *
         *  If emitted when querying the clipboard, the contents of the clipboard could
         *  not be converted to the requested format.
         *
         *  @par Analysis
         *  If emitted during window creation, one or more
         *  [hard constraints](@ref window_hints_hard) did not match any of the
         *  available pixel formats.  If your application is sufficiently flexible,
         *  downgrade your requirements and try again.  Otherwise, inform the user that
         *  their machine does not match your requirements.
         *
         *  @par
         *  If emitted when querying the clipboard, ignore the error or report it to
         *  the user, as appropriate.
         */
        public const int GLFW_FORMAT_UNAVAILABLE = 0x00010009;
        /*! @} */

        public const int GLFW_FOCUSED = 0x00020001;
        public const int GLFW_ICONIFIED = 0x00020002;
        public const int GLFW_RESIZABLE = 0x00020003;
        public const int GLFW_VISIBLE = 0x00020004;
        public const int GLFW_DECORATED = 0x00020005;
        public const int GLFW_AUTO_ICONIFY = 0x00020006;
        public const int GLFW_FLOATING = 0x00020007;

        public const int GLFW_RED_BITS = 0x00021001;
        public const int GLFW_GREEN_BITS = 0x00021002;
        public const int GLFW_BLUE_BITS = 0x00021003;
        public const int GLFW_ALPHA_BITS = 0x00021004;
        public const int GLFW_DEPTH_BITS = 0x00021005;
        public const int GLFW_STENCIL_BITS = 0x00021006;
        public const int GLFW_ACCUM_RED_BITS = 0x00021007;
        public const int GLFW_ACCUM_GREEN_BITS = 0x00021008;
        public const int GLFW_ACCUM_BLUE_BITS = 0x00021009;
        public const int GLFW_ACCUM_ALPHA_BITS = 0x0002100A;
        public const int GLFW_AUX_BUFFERS = 0x0002100B;
        public const int GLFW_STEREO = 0x0002100C;
        public const int GLFW_SAMPLES = 0x0002100D;
        public const int GLFW_SRGB_CAPABLE = 0x0002100E;
        public const int GLFW_REFRESH_RATE = 0x0002100F;
        public const int GLFW_DOUBLEBUFFER = 0x00021010;

        public const int GLFW_CLIENT_API = 0x00022001;
        public const int GLFW_CONTEXT_VERSION_MAJOR = 0x00022002;
        public const int GLFW_CONTEXT_VERSION_MINOR = 0x00022003;
        public const int GLFW_CONTEXT_REVISION = 0x00022004;
        public const int GLFW_CONTEXT_ROBUSTNESS = 0x00022005;
        public const int GLFW_OPENGL_FORWARD_COMPAT = 0x00022006;
        public const int GLFW_OPENGL_DEBUG_CONTEXT = 0x00022007;
        public const int GLFW_OPENGL_PROFILE = 0x00022008;
        public const int GLFW_CONTEXT_RELEASE_BEHAVIOR = 0x00022009;

        public const int GLFW_OPENGL_API = 0x00030001;
        public const int GLFW_OPENGL_ES_API = 0x00030002;

        public const int GLFW_NO_ROBUSTNESS = 0;
        public const int GLFW_NO_RESET_NOTIFICATION = 0x00031001;
        public const int GLFW_LOSE_CONTEXT_ON_RESET = 0x00031002;

        public const int GLFW_OPENGL_ANY_PROFILE = 0;
        public const int GLFW_OPENGL_CORE_PROFILE = 0x00032001;
        public const int GLFW_OPENGL_COMPAT_PROFILE = 0x00032002;

        public const int GLFW_CURSOR = 0x00033001;
        public const int GLFW_STICKY_KEYS = 0x00033002;
        public const int GLFW_STICKY_MOUSE_BUTTONS = 0x00033003;

        public const int GLFW_CURSOR_NORMAL = 0x00034001;
        public const int GLFW_CURSOR_HIDDEN = 0x00034002;
        public const int GLFW_CURSOR_DISABLED = 0x00034003;

        public const int GLFW_ANY_RELEASE_BEHAVIOR = 0;
        public const int GLFW_RELEASE_BEHAVIOR_FLUSH = 0x00035001;
        public const int GLFW_RELEASE_BEHAVIOR_NONE = 0x00035002;

        /*! @defgroup shapes Standard cursor shapes
         *
         *  See [standard cursor creation](@ref cursor_standard) for how these are used.
         *
         *  @ingroup input
         *  @{ */

        /*! @brief The regular arrow cursor shape.
         *
         *  The regular arrow cursor.
         */
        public const int GLFW_ARROW_CURSOR = 0x00036001;
        /*! @brief The text input I-beam cursor shape.
         *
         *  The text input I-beam cursor shape.
         */
        public const int GLFW_IBEAM_CURSOR = 0x00036002;
        /*! @brief The crosshair shape.
         *
         *  The crosshair shape.
         */
        public const int GLFW_CROSSHAIR_CURSOR = 0x00036003;
        /*! @brief The hand shape.
         *
         *  The hand shape.
         */
        public const int GLFW_HAND_CURSOR = 0x00036004;
        /*! @brief The horizontal resize arrow shape.
         *
         *  The horizontal resize arrow shape.
         */
        public const int GLFW_HRESIZE_CURSOR = 0x00036005;
        /*! @brief The vertical resize arrow shape.
         *
         *  The vertical resize arrow shape.
         */
        public const int GLFW_VRESIZE_CURSOR = 0x00036006;
        /*! @} */

        public const int GLFW_CONNECTED = 0x00040001;
        public const int GLFW_DISCONNECTED = 0x00040002;

        public const int GLFW_DONT_CARE = -1;
    }
}
