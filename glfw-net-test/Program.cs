using System;

using GLFWnet.Binding;
using static GLFWnet.Binding.GLFW3;
using System.Text;

namespace GLFWnet.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            glfwNetConfigureNativesDirectory("../../../../lib/" + GLFW_ARCHITECTURE + "/");

            Console.WriteLine("Is Vulkan supported? {0}", glfwVulkanSupported());

            glfwSetErrorCallback(callbackError);

            bool result = glfwInit();
            GLFWwindow window = glfwCreateWindow(800, 400, "漢字glfw-net/" + glfwGetVersionString(), GLFWmonitor.NULL, GLFWwindow.NULL);

            glfwSetWindowTitle(window, "違う漢字glfw-net/" + glfwGetVersionString());

            var imgCursor = new GLFWimage
            {
                width = 16,
                height = 16,
                pixels = new byte[16 * 16 * 4]
            };

            for (int x = 0; x < imgCursor.width; x++)
            {
                for (int y = 0; y < imgCursor.height; y++)
                {
                    imgCursor.pixels[((x * imgCursor.width) + y) * 4 + 0] = 0xFF;
                    imgCursor.pixels[((x * imgCursor.width) + y) * 4 + 1] = 0xFF;
                    imgCursor.pixels[((x * imgCursor.width) + y) * 4 + 2] = 0xFF;
                    imgCursor.pixels[((x * imgCursor.width) + y) * 4 + 3] = 0xFF;
                }
            }

            var cursor = glfwCreateCursor(imgCursor, 0, 0);
            glfwSetCursor(window, cursor);

            glfwSetWindowPosCallback(window, callbackWindowPos);
            glfwSetWindowSizeCallback(window, callbackWindowSize);

            var ramp = glfwGetGammaRamp(glfwGetPrimaryMonitor());
            for (int i = 0; i < ramp.size; i++)
            {
                ramp.red[i] = ramp.green[i];
                ramp.blue[i] = ramp.green[i];
                ramp.green[i] = ramp.green[i];
            }
            //glfwSetGammaRamp(glfwGetPrimaryMonitor(), ref ramp);

            while (!glfwWindowShouldClose(window))
            {
                glfwSwapBuffers(window);
                glfwPollEvents();
            }

            glfwTerminate();
        }

        static void callbackError(int code, string description)
        {
            Console.WriteLine("[glfw_error][ code: {0}, desc: \"{1}\" ]", code, description);
        }

        static void callbackWindowPos(GLFWwindow window, int xpos, int ypos)
        {
            Console.WriteLine("[glfw_window_pos][ x: {0}, y: {1} ]", xpos, ypos);
        }

        static void callbackWindowSize(GLFWwindow window, int width, int height)
        {
            Console.WriteLine("[glfw_window_size][ w: {0}, h: {1} ]", width, height);
        }
    }
}
