using iLearn.iLearnDbModels;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Services.Interfaces
{
    public interface IUserService
    {
        public Instructor GetInstructor(string userId);
        public Instructor CreateInstructor(Instructor instructor);
        public void UpdateInstructor(Instructor instructor);
    }
}
