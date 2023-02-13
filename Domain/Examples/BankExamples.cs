using Domain.Entities;
using TransactionsExample.Data.Repository;
using TransactionsExample.Domain.Interfaces;

namespace Domain.Examples;

public class BankExamples
{
    private IBankAccountRepository _repository;

    private Guid MainAccountId { get; set; }
    private Guid NonExistentAccountId { get; set; }

    public BankExamples()
    {
        _repository = new BankAccountRepository();
        SeedAccount();
    }

    public void NotFoundAccountFailure_WithoutTransaction()
    {
        var amountToTransfer = 1500;
        try
        {
            var origin = _repository.GetById(MainAccountId);
            var money = origin.Take(amountToTransfer);
            _repository.Update(origin);

            var destiny = _repository.GetById(NonExistentAccountId);
            destiny.Deposit(money);
            _repository.Update(destiny);
        }
        catch(Exception ex)
        {
            /***
             *          Nesse cenário teriamos vários possíveis erros que levantariam diferentes exceções 
             *                      (como NullPointerException ou InvalidOperationException)
             * que resultariam em diversos casos de validação que poluiriam o código e dificultariam sua compreensão
             * além de que novas implementações de métodos de saque/depósito acarretariam em novos problemas a serem
             *                          tratados e reduzindo a modularidade do sistema.
             */
            
            // Checagem se o dinheiro saiu da conta de origem;
            // Checagem se a conta de origem possuia o valor para tranferência;
            // Chegagem se a conta de destino é válida para depositos;
            // Extorno dos valores para a conta de origem;
            // Retornar a exeção para que o erro retorne até ser informado ao usuário;

            throw ex;
        }
    }

    public void NotFoundAccountFailure_WithTransaction()
    {
        var transaction = _repository.Transaction().BeginTransaction();

        var amountToTransfer = 1500;
        try
        {
            var origin = _repository.GetById(MainAccountId);
            var money = origin.Take(amountToTransfer);
            _repository.Update(origin);

            var destiny = _repository.GetById(NonExistentAccountId);
            destiny.Deposit(money);
            _repository.Update(destiny);

            // Se chegou até aqui, todas as operações ocorreram conforme o esperado.
            transaction.Commit();
        }
        catch(Exception ex)
        {
            /**
             * Nesse momento em especifico do tratamento de erro nesse exemplo, o motivo do erro tem pouca importância
             * sendo necessário, inicialmente, garantir que todos os valores estarão inalterados em relação a antes do
             *                                         inicio da transação.
             */

            transaction.Rollback();

            // Levantando novamente a exeção para que outras camadas da aplicação tratem o problema especifico
            //                                      notifiquem o usuário.
            throw ex;
        }
    }

    private void SeedAccount()
    {
        var entity = new BankAccount("Joseph Ferraz", 10268);
        _repository.Insert(entity);

        MainAccountId = entity.Id;
        NonExistentAccountId = Guid.NewGuid();
    }
}
