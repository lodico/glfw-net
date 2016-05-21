using System;
using GLFWnet.Binding;
using System.Runtime.InteropServices;

namespace GLFWnet.Testing {
    class Program {
        static void Main(string[] args) {
#if X86
            GLFW3.ConfigureNativesDirectory("../../../../lib/x86/");
#elif X64
            GLFW3.ConfigureNativesDirectory("../../../../lib/x64/");
#else
#error "Unknown target architecture."
#endif

            GLFW3.glfwSetErrorCallback(callbackError);

            bool result = GLFW3.glfwInit();
            GLFWwindow window = GLFW3.glfwCreateWindow(800, 400, GLFW3.glfwGetVersionString(), GLFWmonitor.NULL, GLFWwindow.NULL);

            var imgCursor = new GLFWimage {
                width = 16, height = 16,
                pixels = new byte[16 * 16 * 4]
            };

            for (int x = 0; x < imgCursor.width; x++) {
                for (int y = 0; y < imgCursor.height; y++) {
                    imgCursor.pixels[((x * imgCursor.width) + y) * 4 + 0] = 0xFF;
                    imgCursor.pixels[((x * imgCursor.width) + y) * 4 + 1] = 0xFF;
                    imgCursor.pixels[((x * imgCursor.width) + y) * 4 + 2] = 0xFF;
                    imgCursor.pixels[((x * imgCursor.width) + y) * 4 + 3] = 0xFF;
                }
            }
            
            //var cursor = GLFW3.glfwCreateCursor(imgCursor, 0, 0);
            //GLFW3.glfwSetCursor(window, cursor);

            GLFW3.glfwSetWindowPosCallback(window, callbackWindowPos);
            GLFW3.glfwSetWindowSizeCallback(window, callbackWindowSize);

            var ramp = GLFW3.glfwGetGammaRamp(GLFW3.glfwGetPrimaryMonitor());
            for (int i = 0; i < ramp.size; i++)
            {
                ramp.red[i] = ramp.green[i];
                ramp.blue[i] = ramp.green[i];
                ramp.green[i] = ramp.green[i];
            }
            GLFW3.glfwSetGammaRamp(GLFW3.glfwGetPrimaryMonitor(), ref ramp);

            while (!GLFW3.glfwWindowShouldClose(window)) {
                GLFW3.glfwSwapBuffers(window);
                GLFW3.glfwPollEvents();
            }

            GLFW3.glfwTerminate();
        }

        static void callbackError(int code, string description) {
            Console.WriteLine("[glfw_error][ code: {0}, desc: \"{1}\" ]", code, description);
        }

        static void callbackWindowPos(GLFWwindow window, int xpos, int ypos) {
            Console.WriteLine("[glfw_window_pos][ x: {0}, y: {1} ]", xpos, ypos);
        }

        static void callbackWindowSize(GLFWwindow window, int width, int height) {
            Console.WriteLine("[glfw_window_size][ w: {0}, h: {1} ]", width, height);
        }
    }
}
