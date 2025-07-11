using System.Windows.Forms;

namespace SpikeSoft.UtilityManager
{
    public interface IEditor
    {
        Control UIEditor { get; }
        void Initialize(string filePath);
        string[] FileNamePatterns { get; }
    }
}
