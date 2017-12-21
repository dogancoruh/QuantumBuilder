using System;

namespace Document.Framework.Event
{
    public class DocumentSaveAsEventArgs : EventArgs
    {
        public string FileName { get; set; }
    }
}
