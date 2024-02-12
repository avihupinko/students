using Newtonsoft.Json;
using System.ComponentModel;

namespace WebApplication1.Models
{

    public class StudentDBModel
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
        public Gender Gender { get; set; }

        [JsonProperty("studentClass")]
        public StudentClass StudentClass { get; set; }
        [JsonProperty("studentStatus")]
        public StudentStatus StudentStatus { get; set; }
        [JsonProperty("studentType")]
        public StudentType StudentType { get; set; }

        [JsonProperty("surveyStatus")]
        public SurveyStatus? SurveyStatus { get; set; }

        [JsonProperty("personalPlanStatus")]
        public PersonalPlanStatus? PersonalPlanStatus { get; set; }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("birthCountry")]
        public Country BirthCountry { get; set; }
    }

    public enum StudentStatus
    {
        [Description("לומד ממשיך")]
        Continue,
        [Description("התחיל את לימודיו")]
        Starting,
    }

    public enum StudentType
    {
        [Description("תלמיד מנהל")]
        Minhal,
    }

    public enum SurveyStatus
    {
        מאושר,
        בתהליך
    }

    public enum PersonalPlanStatus
    {

    }

    public enum Gender
    {
        זכר,
        נקבה
    }

    public enum StudentClass
    {
        א,
        ב,
        ג,
        ד,
        ה,
        ו,
        ז,
        ח,
        ט,
        י,
        יא,
        יב
    }
}
