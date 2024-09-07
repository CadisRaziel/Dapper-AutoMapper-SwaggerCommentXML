namespace CrudDapperAndAutoMapper.DTO
{
    public class UsuarioCriarDTO
    {      
        //Nao preciso do ID pois no banco eu ja especifiquei que sera insercao automatica
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public string CPF { get; set; }
        public bool Situacao { get; set; } //1 -> Ativo(true) \ 0 -> Inativo(false)
        public string Senha { get; set; }
    }
}
