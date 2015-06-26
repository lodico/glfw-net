using System;
using System.Diagnostics;
using GLFW;

namespace glfw_net_test {
    class Program {
        static void Main(string[] args) {
            GLFW3.ConfigureNativesDirectory("../../../../lib/x64/");
            Console.WriteLine(Environment.GetEnvironmentVariable("Path"));

            GLFW3.glfwSetErrorCallback(error);

            int result = GLFW3.glfwInit();
            GLFWwindow window = GLFW3.glfwCreateWindow(800, 400, "Test", GLFWmonitor.NULL, GLFWwindow.NULL);

            GLFW3.glfwSetWindowPosCallback(window, wpos);

            while (!GLFW3.glfwWindowShouldClose(window)) {
                GLFW3.glfwSetWindowTitle(window, new Random().Next().ToString());
                GLFW3.glfwSwapBuffers(window);
                GLFW3.glfwPollEvents();
            }

            GLFW3.glfwTerminate();
        }

        static void error(int code, string description) {
            Console.WriteLine(code + " :: " + description);
        }

        static void wpos(GLFWwindow window, int xpos, int ypos) {
            Console.WriteLine(xpos + " :: " + ypos);
        }
    }
}
