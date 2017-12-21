using System;

namespace Document.Framework.Event
{
    public class DocumentModifiedEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public bool IsModified { get; set; }
    }
}
