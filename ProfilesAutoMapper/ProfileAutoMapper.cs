using AutoMapper;
using CrudDapperAndAutoMapper.DTO;
using CrudDapperAndAutoMapper.Models;

namespace CrudDapperAndAutoMapper.ProfilesAutoMapper
{
    public class ProfileAutoMapper : Profile
    {
        //Classe de profile para nos falar quem sera transformado em quem
        public ProfileAutoMapper()
        {
            //Vou transformar a classe Usuario em um UsuarioListarDTO
            CreateMap<Usuario, UsuarioListarDTO>();


            CreateMap<Usuario, UsuarioCriarDTO>();

            //Caso eu queira fazer o contrario e nao quiser criar outro map eu so coloco o .ReverseMap() que ai o UsuarioListarDTO ira se transformar em Usuario
            //CreateMap<Usuario, UsuarioListarDTO>().ReverseMap();
        }
    }
}
