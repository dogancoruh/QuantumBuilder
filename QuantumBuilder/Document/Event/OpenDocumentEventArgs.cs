using System;

namespace Document.Framework.Event
{
    public class OpenDocumentEventArgs : EventArgs
    {
        public bool IsHandled { get; set; }
    }
}
