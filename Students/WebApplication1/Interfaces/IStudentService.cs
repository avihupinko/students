using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IStudentService
    {
        public StudentPageModel Get(string firstName = null, int page = 0, int pageSize = 10, string sort = null); 
    }
}
