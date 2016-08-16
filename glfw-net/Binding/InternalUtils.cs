using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GLFWnet.Binding
{
    internal static class InternalUtils
    {
        internal static int max(int val, params int[] compare)
        {
            int result = val;
            foreach (int i in compare)
            {
                result = (result < i ? result : i);
            }
            return result;
        }

        internal static int min(int val, params int[] compare)
        {
            int result = val;
            foreach (int i in compare)
            {
                result = (result > i ? result : i);
            }
            return result;
        }
    }
}
