using simple_crud_api.Models;

namespace simple_crud_api.Data.Repositories
{
    public class CourseRepo
    {
        private readonly CourseCtx _ctx;

        public CourseRepo(CourseCtx ctxHere)
        {
            _ctx = ctxHere;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _ctx.CourseTable.ToListAsync();
        }

        public async Task<Course> GetOne(int courseId)
        {
            return await _ctx.CourseTable.FindAsync(courseId);
        }

        public async Task<int> CreateOne(Course course)
        {
            await _ctx.CourseTable.AddAsync(course);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> UpdateOne(Course course)
        {
            _ctx.CourseTable.Update(course);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> DeleteOne(Course course)
        {
            _ctx.CourseTable.Remove(course);
            return await _ctx.SaveChangesAsync();
        }
    }
}
