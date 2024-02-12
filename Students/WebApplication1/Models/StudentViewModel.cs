using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class StudentViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("studentId")]
        public string StudentId { get; set; }
        [JsonProperty("oldStudentId")]
        public string OldStudentId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("studentClass")]
        public string StudentClass { get; set; }
        [JsonProperty("studentStatus")]
        public string StudentStatus { get; set; }
        [JsonProperty("studentType")]
        public string StudentType { get; set; }

        [JsonProperty("surveyStatus")]
        public string SurveyStatus { get; set; }

        [JsonProperty("personalPlanStatus")]
        public string PersonalPlanStatus { get; set; }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("birthCountry")]
        public string BirthCountry { get; set; }
    }
}
