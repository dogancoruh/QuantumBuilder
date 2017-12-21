using System;

namespace Document.Framework.Event
{
    public class NewDocumentEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public bool Handled { get; set; }
    }
}
