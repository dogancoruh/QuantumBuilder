using Document.Framework.Event;
using System;
using System.Text;

namespace Document.Framework
{
    public class Document
    {
        public event EventHandler<NewDocumentEventArgs> OnNewDocument;
        public event EventHandler<NewDocumentSavePromptEventArgs> OnNewDocumentPrompt;
        public event EventHandler<OpenDocumentEventArgs> OnOpenDocument;
        public event EventHandler<OpenDocumentSavePromptEventArgs> OnOpenDocumentSavePrompt;
        public event EventHandler<SaveDocumentEventArgs> OnSaveDocument;
        public event EventHandler<SaveDocumentPromptEventArgs> OnSaveDocumentPrompt;
        public event EventHandler<DocumentSaveAsEventArgs> OnSaveAsDocument;
        public event EventHandler<SaveAsDocumentPromptEventArgs> OnSaveAsDocumentPrompt;
        public event EventHandler<CloseDocumentEventArgs> OnCloseDocument;

        public event EventHandler<DocumentFileNameChangedEventArgs> OnDocumentFileNameChanged;
        public event EventHandler<DocumentModifiedEventArgs> OnDocumentModified;

        private string documentFileName;

        public string DocumentFileName
        {
            get { return documentFileName; }
            set
            {
                documentFileName = value;
                DoDocumentFileNameChange();
            }
        }


        private bool isDocumentModified;

        public bool IsDocumentModified
        {
            get { return isDocumentModified; }
            set
            {
                isDocumentModified = value;
                DoDocumentModify();
            }
        }

        public string DefaultTitle { get; set; }

        public string Title
        {
            get
            {
                var stringBuilder = new StringBuilder();

                if (documentFileName != string.Empty)
                    stringBuilder.Append(documentFileName);
                else
                    stringBuilder.Append("Unnamed");

                if (isDocumentModified)
                    stringBuilder.Append("*");

                stringBuilder.Append(" - ");

                stringBuilder.Append(DefaultTitle);

                return stringBuilder.ToString();
            }
        }

        public Document()
        {
            documentFileName = string.Empty;
            isDocumentModified = false;
        }

        virtual public bool NewDocument(string fileName = "")
        {
            bool handled = true;

            if (isDocumentModified/* && documentFileName != string.Empty*/)
            {
                if (OnNewDocumentPrompt != null)
                {
                    var newPromptArgs = new NewDocumentSavePromptEventArgs();

                    OnNewDocumentPrompt(this, newPromptArgs);

                    switch (newPromptArgs.DialogResult)
                    {
                        case System.Windows.Forms.DialogResult.Cancel:
                            return false;
                        case System.Windows.Forms.DialogResult.No:
                            break;
                        case System.Windows.Forms.DialogResult.Yes:
                            var saveArgs = new SaveDocumentEventArgs
                            {
                                FileName = documentFileName
                            };

                            OnSaveDocument?.Invoke(this, saveArgs);

                            handled = saveArgs.Handled;

                            break;
                        default:
                            break;
                    }
                }
            }

            if (handled && OnNewDocument != null)
            {
                var args = new NewDocumentEventArgs
                {
                    FileName = fileName,
                    Handled = false
                };

                OnNewDocument(this, args);

                if (args.Handled)
                {
                    DocumentFileName = args.FileName;
                    IsDocumentModified = false;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        virtual public bool OpenDocument(string fileName)
        {
            if (isDocumentModified && documentFileName != string.Empty)
            {
                var args = new OpenDocumentSavePromptEventArgs();
                OnOpenDocumentSavePrompt?.Invoke(this, args);
            }

            if (OnOpenDocument != null)
            {
                var args = new OpenDocumentEventArgs();
                OnOpenDocument?.Invoke(this, args);

                DocumentFileName = fileName;
                IsDocumentModified = false;

                return true;
            }
            else
                return false;
        }

        virtual public bool OpenDocument()
        {
            if (isDocumentModified && documentFileName != string.Empty)
            {
                if (OnOpenDocumentSavePrompt != null)
                {
                    var args = new OpenDocumentSavePromptEventArgs();

                    OnOpenDocumentSavePrompt(this, args);

                    if (!args.IsHandled)
                        return false;
                }
            }

            if (OnOpenDocument != null)
            {
                var args = new OpenDocumentEventArgs();

                OnOpenDocument(this, args);

                if (args.IsHandled)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        virtual public void SaveDocument()
        {
            var args = new SaveDocumentEventArgs
            {
                FileName = DocumentFileName,
                Modified = IsDocumentModified
            };

            OnSaveDocument?.Invoke(this, args);
        }

        virtual public void SaveAsDocument()
        {
            string fileName_ = string.Empty;

            if (isDocumentModified && documentFileName != string.Empty)
            {
                if (OnSaveAsDocumentPrompt != null)
                {
                    var args_ = new SaveAsDocumentPromptEventArgs();

                    OnSaveAsDocumentPrompt(this, args_);

                    if (args_.IsHandled)
                        fileName_ = args_.FileName;
                }
            }

            var args = new DocumentSaveAsEventArgs
            {
                FileName = fileName_
            };
            OnSaveAsDocument?.Invoke(this, args);
        }

        virtual public void CloseDocument()
        {
            var args = new CloseDocumentEventArgs
            {
                FileName = string.Empty
            };
            OnCloseDocument?.Invoke(this, args);
        }

        virtual public void DoDocumentFileNameChange()
        {
            var args = new DocumentFileNameChangedEventArgs
            {
                FileName = documentFileName,
                Title = Title
            };
            OnDocumentFileNameChanged?.Invoke(this, args);
        }

        virtual public void DoDocumentModify()
        {
            var args = new DocumentModifiedEventArgs
            {
                FileName = documentFileName,
                IsModified = isDocumentModified,
                Title = Title
            };
            OnDocumentModified?.Invoke(this, args);
        }

        public void RefreshTitle()
        {
            DoDocumentFileNameChange();
        }
    }
}
