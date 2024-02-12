using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class StudentService : IStudentService
    {

        public List<StudentViewModel> Sort(List<StudentViewModel> input, string property, string direction)
        {
            var type = typeof(StudentViewModel);
            var sortProperty = type.GetProperty(ToPascalCase(property));
            return direction.ToLower() == "asc" ? input.OrderBy(p => sortProperty.GetValue(p, null)).ToList() :
                input.OrderByDescending(p => sortProperty.GetValue(p, null)).ToList();
        }

        public string ToPascalCase(string original)
        {
            Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
            Regex whiteSpace = new Regex(@"(?<=\s)");
            Regex startsWithLowerCaseChar = new Regex("^[a-z]");
            Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

            // replace white spaces with undescore, then replace all invalid chars with empty string
            var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(original, "_"), string.Empty)
                // split by underscores
                .Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                // set first letter to uppercase
                .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                // replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
                .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                // set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
                .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                // lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
                .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

            return string.Concat(pascalCase);
        }

        public StudentPageModel Get(string firstName = null, int page = 0, int pageSize = 10, string sort = null)
        {
            using (StreamReader r = new StreamReader("students.json"))
            {
                string json = r.ReadToEnd();
                List<StudentDBModel> items = JsonConvert.DeserializeObject<List<StudentDBModel>>(json);

                if (!string.IsNullOrEmpty(firstName))
                {
                    items = items.Where(x => x.FirstName.Contains(firstName)).ToList();
                }

                var result = items.Select(x => new StudentViewModel
                {
                    BirthCountry = x.BirthCountry.Name,
                    FirstName = x.FirstName,
                    BirthDate = x.BirthDate,
                    Gender = x.Gender.ToString(),
                    Id = x.Id,
                    LastName = x.LastName,
                    OldStudentId = x.OldStudentId,
                    PersonalPlanStatus = x.PersonalPlanStatus?.ToString(),
                    StudentClass = x.StudentClass.ToString(),
                    StudentId = x.StudentId,
                    StudentStatus = GetEnumDescription(x.StudentStatus),
                    StudentType = GetEnumDescription(x.StudentType),
                    SurveyStatus = x.SurveyStatus.ToString()
                }).ToList();

                if (!string.IsNullOrEmpty(sort))
                {
                    var sorts = sort.Split("::");
                    result = Sort(result, sorts[0], sorts[1]);
                }

                return new StudentPageModel
                {
                    Total = result.Count,
                    Data = result.Skip(page * pageSize).Take(pageSize).ToList(),
                };
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
