using simple_crud_api.Models;

namespace simple_crud_api.Data.Repositories
{
    public class ICourseRepo
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetOne(int courseId);
        Task<int> CreateOne(Course course);
        Task<int> UpdateOne(Course course);
        Task<int> DeleteOne(Course course);
    }
}
