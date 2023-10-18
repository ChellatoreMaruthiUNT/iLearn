using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using iLearn.Utils;

namespace iLearn.Services
{
    public class UserService : IUserService
    {
        private readonly iLearnAppContext _context;
        private IConfiguration _config;

        public UserService(iLearnAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public Instructor GetInstructor(string userId)
        {
            return _context.Instructors.FirstOrDefault(instructor => instructor.UserId == userId);    
        }
        public Instructor CreateInstructor(Instructor instructor)
        {
            //_context.Instructors.Remove(instructor.UserId);
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
            return instructor;
        }
        public void UpdateInstructor(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            _context.SaveChanges();
        }
    }
}
