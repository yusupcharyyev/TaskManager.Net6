using AutoMapper;
using TaskManagerSystem.Areas.Admin.Models.DTOs;
using TaskManagerSystem.Areas.Manager.Models.VMs;
using TaskManagerSystem.Areas.Personel.Models.VMs;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.Models.Mappers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, CreateManagerDTO>().ReverseMap();
            CreateMap<Company, CreateCompanyDTO>().ReverseMap();
            CreateMap<Tasks, CreateTaskVM>().ReverseMap();
            CreateMap<Tasks, CreateTaskPersonelVM>().ReverseMap();
        }
    }
}
