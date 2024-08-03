## Domain Validation: Validando a Consistência de Filmes

Este repositório demonstra a implementação de Domain Validation em C#, utilizando o exemplo de um domínio de filmes. O objetivo é garantir a integridade dos dados e a consistência das regras de negócio, evitando erros e inconsistências durante a criação e manipulação de objetos `Filme`.

### O que é Domain Validation?

Domain Validation é um conceito fundamental em Domain Driven Design (DDD) que visa garantir a validade dos dados e a consistência das regras de negócio dentro de um domínio específico. Em vez de depender apenas de validações de nível de dados, a Domain Validation se concentra em validar as regras de negócio do domínio, garantindo que os objetos criados sejam consistentes com as regras do negócio.

### Implementação de Domain Validation em C#

**1. Classe Base `Entity`**

```C#
namespace DomainValidation.Primitives;

public abstract class Entity(Guid id) : IEquatable<Entity>
{
    public Guid Id { get; private init; } = id;

    // ... (Implementação de Equals, GetHashCode e operadores == e !=)
}
```

* **`Entity` é uma classe abstrata:** Serve como base para todas as entidades do domínio.
* **`Id` é uma propriedade:** Armazena o identificador único da entidade.
* **`Equals`, `GetHashCode` e operadores `==` e `!=`:** Implementados para garantir a comparação de igualdade entre entidades.

**2. Classe `Filme` (Entidade do Domínio)**

```C#
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
        // Validações de regras de negócio
        if (string.IsNullOrWhiteSpace(titulo))
            throw new FilmeDomainException(FilmeDomainExceptionError.TituloInvalido);

        if (titulo.Length > 100)
            throw new FilmeDomainException(FilmeDomainExceptionError.TituloTamanhoMaximo);

        // ... (Outras validações)

        return new Filme(id, titulo, diretor, dataLancamento, avaliacao);
    }
}
```

* **`Filme` é uma classe selada:** Impede a herança, garantindo a consistência da entidade.
* **`Filme` herda da classe `Entity`:** Garante um identificador único para cada filme.
* **`Filme` possui atributos relacionados:** Titulo, Diretor, DataLancamento e Avaliacao.
* **`Create` é um método estático:** Responsável por criar um novo objeto `Filme`, aplicando as validações de regras de negócio.

**3. Classe `FilmeDomainException` (Exceção de Domínio)**

```C#
namespace DomainValidation.Exceptions;

public sealed class FilmeDomainException(FilmeDomainExceptionError error) 
    : DomainException<FilmeDomainExceptionError>(error) { }

public sealed class FilmeDomainExceptionError(string message) : DomainExceptionError(message)
{
    // ... (Definição de erros específicos do domínio de filmes)
}
```

* **`FilmeDomainException` é uma exceção específica do domínio:** Lançada quando uma regra de negócio é violada durante a criação de um `Filme`.
* **`FilmeDomainExceptionError` é um tipo de erro específico do domínio:** Define os diferentes tipos de erros que podem ocorrer durante a validação de um `Filme`.

**4. Classe `DomainException` (Exceção de Domínio Genérica)**

```C#
namespace DomainValidation.Exceptions;

public abstract class DomainException<TError> : Exception where TError : DomainExceptionError
{
    public DomainException(TError error) : base(error.Message) { }

    private DomainException() { }
}

public abstract class DomainExceptionError(string message)
{
    public readonly string Message = message;
}
```

* **`DomainException` é uma exceção genérica de domínio:** Serve como base para outras exceções de domínio.
* **`DomainExceptionError` é um tipo de erro genérico de domínio:** Define a estrutura básica para os erros de domínio.

### Benefícios de usar Domain Validation

* **Integridade de Dados:** Garante que os dados armazenados sejam consistentes com as regras de negócio do domínio.
* **Prevenção de Erros:** Reduz a ocorrência de erros durante a criação e manipulação de objetos do domínio.
* **Código Mais Limpo:** Separa a lógica de validação da lógica de negócio principal, tornando o código mais legível e fácil de manter.
* **Melhor Comunicação:** Facilita a comunicação entre desenvolvedores e stakeholders, pois as regras de negócio são explicitamente definidas.

### Exemplos de Regras de Negócio

* **Título do filme:** O título não pode ser vazio, deve ter no máximo 100 caracteres e não pode conter caracteres inválidos.
* **Diretor do filme:** O diretor não pode ser vazio, deve ter no máximo 100 caracteres e não pode conter caracteres inválidos.
* **Data de lançamento:** A data de lançamento deve ser válida e não pode ser anterior a 1895 (ano do primeiro filme).
* **Avaliação:** A avaliação deve estar entre 0 e 5.

### Conclusões

Domain Validation é uma técnica essencial para garantir a qualidade e a consistência dos dados em aplicações complexas. Ao implementar Domain Validation, você garante que os objetos do domínio sejam criados e manipulados de acordo com as regras de negócio, evitando erros e inconsistências.