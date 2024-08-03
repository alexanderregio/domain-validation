namespace DomainValidation.Exceptions;

public sealed class FilmeDomainException(FilmeDomainExceptionError error) 
    : DomainException<FilmeDomainExceptionError>(error) { }

public sealed class FilmeDomainExceptionError(string message) : DomainExceptionError(message)
{
    public static FilmeDomainExceptionError TituloInvalido
        => new("Título do filme inválido");

    public static FilmeDomainExceptionError TituloTamanhoMaximo
        => new("Título do filme deve possuir no máximo 100 caracteres");

    public static FilmeDomainExceptionError DiretorInvalido
        => new("Diretor do filme inválido");

    public static FilmeDomainExceptionError DiretorTamanhoMaximo
        => new("Diretor do filme deve possuir no máximo 100 caracteres");

    public static FilmeDomainExceptionError DataLancamentoInvalida
        => new("Data do lançamento do filme deve ser a partir de 1895");

    public static FilmeDomainExceptionError AvaliacaoInvalida
        => new("A avaliação do filme deve ser de zero à cinco");
}