using System;

namespace Document.Framework.Event
{
    public class OpenDocumentSavePromptEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public bool IsHandled { get; set; }
    }
}
