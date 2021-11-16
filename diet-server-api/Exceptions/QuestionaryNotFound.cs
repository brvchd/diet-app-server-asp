using System;

namespace diet_server_api.Exceptions
{
    public class QuestionaryNotFound : Exception
    {
        public QuestionaryNotFound(string message = "Questionnaire for requested patient was not found") : base(message)
        {

        }
    }
}