namespace CrudDapperAndAutoMapper.DTO
{
    public class UsuarioEditarDTO
    {
        //No editar usuario eu preciso do ID, vou remover a senha desse DTO para fim de estudo
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public string CPF { get; set; }
        public bool Situacao { get; set; } //1 -> Ativo(true) \ 0 -> Inativo(false)        
    }
}
