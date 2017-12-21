using System;

namespace Document.Framework.Event
{
    public class DocumentFileNameChangedEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public string Title { get; set; }
    }
}
