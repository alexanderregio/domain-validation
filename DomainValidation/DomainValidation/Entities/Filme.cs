using DomainValidation.Exceptions;
using DomainValidation.Primitives;

namespace DomainValidation.Entities;

public sealed class Filme : Entity
{
    public string Titulo { get; set; }

    public string Diretor { get; set; }

    public DateTime DataLancamento { get; set; }

    public double Avaliacao { get; set; }

    private Filme(Guid id, string titulo, string diretor, DateTime dataLancamento, double avaliacao) : base(id)
    {
        Titulo = titulo;
        Diretor = diretor;
        DataLancamento = dataLancamento;
        Avaliacao = avaliacao;
    }

    public static Filme Create(Guid id, string titulo, string diretor, DateTime dataLancamento, double avaliacao)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new FilmeDomainException(FilmeDomainExceptionError.TituloInvalido);

        if (titulo.Length > 100)
            throw new FilmeDomainException(FilmeDomainExceptionError.TituloTamanhoMaximo);

        if (string.IsNullOrWhiteSpace(titulo))
            throw new FilmeDomainException(FilmeDomainExceptionError.DiretorInvalido);

        if (titulo.Length > 100)
            throw new FilmeDomainException(FilmeDomainExceptionError.DiretorTamanhoMaximo);

        if (dataLancamento < new DateTime(1895, 01, 01))
            throw new FilmeDomainException(FilmeDomainExceptionError.DataLancamentoInvalida);

        if (avaliacao < 0 && avaliacao > 5)
            throw new FilmeDomainException(FilmeDomainExceptionError.AvaliacaoInvalida);


        return new Filme(id, titulo, diretor, dataLancamento, avaliacao);
    }
}