namespace CrudDapperAndAutoMapper.Models
{
    public class ResponseModel<T> //Pode ser a letra que eu quiser
    {
        //T? -> Pode ser nullo pois caso eu fizer uma solicitacao e nao tiver nenhum usuario que atenda a essa condicao o dado vai como nulo
        public T? Dados { get; set; } //Deixando generico eu posso atribuir qualquer classe para ser meu responseModel (dessa forma nao preciso criar varios response model para cada coisa, posso ter esse generico para todos)
        public string Mensagem { get; set; } = string.Empty; //Quando eu instanciar essa classe a `Mensagem` ja inicia vazia de cara (se em nenhum momento ela for preenchida ela continua vazia)
        public bool Status { get; set; } = true; //Quando eu instanciar essa classe o `Status` sera true de cara
    }
}
