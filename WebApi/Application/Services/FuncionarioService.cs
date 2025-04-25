using System.Text.RegularExpressions;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICargosServices _cargosServices;
    private readonly ISetorService _setoresServices;
    private readonly IHorariosService _horariosServices;

    public FuncionarioService(IFuncionarioRepository repository, IMapper mapper, ICargosServices cargosServices, ISetorService setoresServices, IHorariosService horariosServices)
    {
        _repository = repository;
        _mapper = mapper;
        _cargosServices = cargosServices;
        _setoresServices = setoresServices;
        _horariosServices = horariosServices;
    }

    public async Task<bool> AdicionarFuncionarioAsync(FuncionarioDto funcionarioDto)
    {
        try
        {
            var funcionario = _mapper.Map<Funcionario>(funcionarioDto);

            var funcionarios = await BuscarFuncionariosPorCPFAsync(funcionario.CPF);

            foreach (var funcionarioAntigo in funcionarios)
            {
                if (funcionario.Ativo == true)
                {
                    throw new Exception("Não é possível adicionar o funcionário pois ele já está ativo.");
                }
            }

            if (!ValidarCPF(funcionario.CPF))
            {
                throw new Exception("CPF inválido!");
            }
            if (!ValidarIdade(funcionario.DataNascimento))
            {
                throw new Exception("Não é permitido o cadastro de menores de 16 anos de idade!");
            }
            if (!ValidarEmail(funcionario.Email))
            {
                throw new Exception("E-mail inválido use o formato example@example.com !");
            }
            if (!ValidarTelefone(funcionario.Telefone))
            {
                throw new Exception("Telefone inválido, digite 11 dígitos e utilize o padrão 99999999999!");
            }
            if (!ValidarContratacao(funcionario.DataContratacao))
            {
                throw new Exception("Não é permitido o cadastro de um funcionário depois da data atual!");
            }
            if (funcionario.Ativo == false)
            {
                throw new Exception("Não é permitido adicionar um funcionário inativo!");
            }
            if(await _cargosServices.BuscarCargosPorIdAsync(funcionario.CargosId) == null)
            {
                throw new Exception("Digite o id de um cargo válido!");
            }
            if(await _setoresServices.BuscarSetorPorIdAsync(funcionario.SetoresId) == null)
            {
                throw new Exception("Digite o id de um setor válido!");
            }
            if(await _horariosServices.BuscarHorarioPorIdAsync(funcionario.HorariosId) == null)
            {
                throw new Exception("Digite o id de um horário válido!");
            }

            var resultado = await _repository.AdicionarFuncionarioAsync(funcionario);
            return resultado;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> AtualizarFuncionarioAsync(int id, FuncionarioDto funcionarioDto)
    {
        try
        {
            var funcionarioAntigo = await BuscarFuncionarioPorIdAsync(id);

            var funcionario = _mapper.Map<Funcionario>(funcionarioDto);

            funcionario.Id = id;

            if (funcionario.CPF != funcionarioAntigo.CPF)
            {
                throw new Exception("Não é permitido alterar o CPF do funcionário!");
            }
            if (!ValidarIdade(funcionario.DataNascimento))
            {
                throw new Exception("Não é permitido o cadastro de menores de 16 anos de idade!");
            }
            if (!ValidarEmail(funcionario.Email))
            {
                throw new Exception("E-mail inválido use o formato example@example.com !");
            }
            if (!ValidarTelefone(funcionario.Telefone))
            {
                throw new Exception("Telefone inválido, digite 11 dígitos e utilize o padrão 99999999999!");
            }
            if (!ValidarContratacao(funcionario.DataContratacao))
            {
                throw new Exception("Não é permitido o cadastro de um funcionário depois da data atual!");
            }
            if (funcionario.Ativo != funcionarioAntigo.Ativo)
            {
                throw new Exception("Não é permitido atualizar a situação do funcionário, caso queira disativa-lo, use o devido método.");
            }
            if (await _cargosServices.BuscarCargosPorIdAsync(funcionario.CargosId) == null)
            {
                throw new Exception("Digite o id de um cargo válido!");
            }
            if (await _setoresServices.BuscarSetorPorIdAsync(funcionario.SetoresId) == null)
            {
                throw new Exception("Digite o id de um setor válido!");
            }
            if (await _horariosServices.BuscarHorarioPorIdAsync(funcionario.HorariosId) == null)
            {
                throw new Exception("Digite o id de um horário válido!");
            }


            var resultado = await _repository.AtualizarFuncionarioAsync(funcionario);
            return resultado;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Funcionario> BuscarFuncionarioPorIdAsync(int id)
    {
        try
        {
            var funcionario = await _repository.BuscarFuncionarioPorIdAsync(id);
            if(funcionario == null)
            {
                throw new Exception($"Não foi possível encontrar um funcionário com id {id}.");
            }
            return funcionario;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Funcionario>> BuscarFuncionariosAsync()
    {
        try
        {
            var funcionarios = await _repository.BuscarFuncionariosAsync();
            if (funcionarios == null)
            {
                throw new Exception($"Não foi possível encontrar nenhum funcionário.");
            }
            return funcionarios;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Funcionario>> BuscarFuncionariosPorCPFAsync(string CPF)
    {
        try
        {
            var funcionarios = await _repository.BuscarFuncionariosPorCPFAsync(CPF);
            if (funcionarios == null)
            {
                throw new Exception($"Não foi possível encontrar um funcionário com CPF {CPF}.");
            }
            return funcionarios;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<RetornoPaginado<Funcionario>> BuscarFuncionariosPaginadoAsync(int pagina, int quantidade)
    {
        try
        {
            var funcionarios = await _repository.BuscarFuncionariosPaginadoAsync(pagina, quantidade);
            if (funcionarios == null)
            {
                throw new Exception($"Não foi possível encontrar nenhum funcionário na página {pagina}.");
            }
            return funcionarios;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DesativarFuncionarioAsync(int id)
    {
        try
        {
            var funcionario = await BuscarFuncionarioPorIdAsync(id);

            if (!funcionario.Ativo)
            {
                throw new Exception("O funcionário já está inativo");
            }

            var resultado = await _repository.DesativarFuncionarioAsync(id);
            if (!resultado)
            {
                throw new Exception($"Não foi possível localizar nenhum funcionário com id {id}.");
            }
            return resultado;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #region Validações
    #region Validação CPF
    public static bool ValidarCPF(string cpf)
    {

        // Verifica se tem 11 dígitos ou se todos os dígitos são iguais
        if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
            return false;

        // Calcula o primeiro dígito verificador
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        // Compara com os dígitos do CPF
        return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }
    #endregion

    #region Validação E-mail
    public static bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Expressão regular para e-mails válidos
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
    #endregion

    #region Validação Telefone
    public bool ValidarTelefone(string numero)
    {
        var regex = new Regex(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$");
        return regex.IsMatch(numero);
    }
    #endregion

    #region Validação Idade
    private bool ValidarIdade(DateTime dataNascimento)
    {
        var idade = DateTime.Today.Year - dataNascimento.Year;

        if (dataNascimento.Date > DateTime.Today.AddYears(-idade))
            idade--;

        return idade >= 16;
    }
    #endregion

    #region Validação Data Contratação
    private bool ValidarContratacao(DateTime dataContratacao)
    {
        if (dataContratacao > DateTime.Now)
        {
            return false;
        }
        return true;
    }
    #endregion
    #endregion
}
