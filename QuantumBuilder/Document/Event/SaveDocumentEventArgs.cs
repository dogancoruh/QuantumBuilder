using System;

namespace Document.Framework.Event
{
    public class SaveDocumentEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public bool Modified { get; set; }
        public bool Handled { get; set; }
    }
}
