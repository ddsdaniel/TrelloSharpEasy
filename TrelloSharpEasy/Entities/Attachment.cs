using System;

namespace TrelloSharpEasy.Entities
{
    public class Attachment : EntityBase
    {
        public int? Bytes { get; private set; }
        public DateTime Date { get; private set; }
        public string EdgeColor { get; private set; }
        public string IdMember { get; private set; }
        public bool IsUpload { get; private set; }
        public string MimeType { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public int Pos { get; private set; }
        public string FileName { get; private set; }

        public Attachment(
            string id,
            int? bytes,
            DateTime date,
            string edgeColor,
            string idMember,
            bool isUpload,
            string mimeType,
            string name,
            string url,
            int pos,
            string fileName)
            : base(id)
        {
            Bytes = bytes;
            Date = date;
            EdgeColor = edgeColor;
            IdMember = idMember;
            IsUpload = isUpload;
            MimeType = mimeType;
            Name = name;
            Url = url;
            Pos = pos;
            FileName = fileName;
        }

        public override string ToString() => Name;
    }
}
