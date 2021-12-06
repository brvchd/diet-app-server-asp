using System;

namespace diet_server_api.DTO.Responses.KnowledgeBase.Get
{
    public class GetNotesResponse
    {
        public int IdNote { get; set; }
        public DateTime NoteCreated { get; set; }
        public string NoteText { get; set; }
    }
}