using System;
using System.Diagnostics;
using GLFW;

namespace glfw_net_test {
    class Program {
        static void Main(string[] args) {
            GLFW3.ConfigureNativesDirectory("../../../../lib/x64/");

            GLFW3.glfwSetErrorCallback(error);

            bool result = GLFW3.glfwInit();
            int ma, mi, re;
            GLFW3.glfwGetVersion(out ma, out mi, out re);
            Console.WriteLine(result + ": " + ma + "," + mi + "," + re);
            GLFWwindow window = GLFW3.glfwCreateWindow(800, 400, GLFW3.glfwGetVersionString(), GLFWmonitor.NULL, GLFWwindow.NULL);
            GLFWmonitor monitor = GLFW3.glfwGetPrimaryMonitor();
            Console.WriteLine(GLFW3.glfwGetMonitorName(monitor));
            GLFW3.glfwSetClipboardString(window, "testy");
            Console.WriteLine(GLFW3.glfwGetClipboardString(window));

            GLFW3.glfwSetWindowPosCallback(window, wpos);
            GLFW3.glfwSetWindowSizeCallback(window, wsz);

            while (!GLFW3.glfwWindowShouldClose(window)) {
                //GLFW3.glfwSetWindowTitle(window, new Random().Next().ToString());
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

        static void wsz(GLFWwindow window, int xpos, int ypos) {
            Console.WriteLine(xpos + " :: " + ypos);
        }
    }
}
