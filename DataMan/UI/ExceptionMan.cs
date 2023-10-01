using System.Collections.Generic;
using System.Windows.Forms;

namespace SpikeSoft.UtilityManager
{
    public class ExceptionMan
    {
        // Common Exception List Messages
        // Key: Hexadecimal Code with format:
        //      0xX000 = Type of Exception
        //      0x0XXX = Code Number
        // Value: Exception Message
        public static readonly Dictionary<int, string> Exceptions = new Dictionary<int, string>
        {
            { 0x1000, "Invalid File Path" },
            { 0x1001, "File Not Supported" },
            { 0x1002, "File Not Found" },
            { 0x2000, "Unknown or Invalid Data" },
            { 0x2001, "Invalid Data, Try changing Console Mode" }
        };

        public static void ThrowMessage(int exMsg)
        {
            MessageBox.Show(Exceptions[exMsg], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ThrowMessage(int exMsg, string[] args)
        {
            MessageBox.Show(Exceptions[exMsg] + "\n" + string.Join("\n", args), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
