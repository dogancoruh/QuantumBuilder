using System;

namespace Document.Framework.Event
{
    public class CloseDocumentEventArgs : EventArgs
    {
        public string FileName { get; set; }
    }
}
