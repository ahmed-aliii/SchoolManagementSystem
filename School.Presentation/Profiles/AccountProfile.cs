using AutoMapper;
using School.Domain;

namespace School.Presentation
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
            //VM => Entity || Entity => VM 
            CreateMap<ApplicationUser, SignupVM>().ReverseMap();

        }
    }
}
