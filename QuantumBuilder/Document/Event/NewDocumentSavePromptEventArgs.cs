using System;
using System.Windows.Forms;

namespace Document.Framework.Event
{
    public class NewDocumentSavePromptEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public DialogResult DialogResult { get; set; }
    }
}
