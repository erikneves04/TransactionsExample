using TransactionsExample.Domain.Entities;
using TransactionsExample.Domain.Interfaces;

namespace TransactionsExample.Domain.Examples;

public class MessageExamples
{
    private readonly IMessageRepository _repository;

    public MessageExamples(IMessageRepository repository)
    {
        _repository = repository;

        SetupMessages();
    }

    public void MessageSendFailure_WithTransaction()
    {
        var transaction = _repository.Transaction().BeginTransaction();

        try
        {
            var messages = _repository
                            .Query()
                            .Where(e => !e.IsSent && !e.Error)
                            .ToList();

            messages.ForEach(message => message.Send());

            _repository.UpdateMany(messages);

            transaction.Commit();
        }
        catch(Exception ex)
        {
            /**
             * Nessa estrutura todas as mensagens devem ser enviadas e só depois disso o status de cada uma será atualizado no banco de dados
             *                  e caso ocorra uma falha no envio de uma delas o status não será alterado para ENVIADO.
             * Supondo que uma mensagem tenha sido enviada antes do caso do erro, ela seria reenviada a cada execução do código, ou seja, o tratamento
             *                              de um erro estava influenciando todo envio de mensagens.
             *                              
             * Obs.: O caso descrito acima ocorreu recenteimente no projeto da tellink, onde uma das pessoas responsáveis pelos testes recebeu um emails
             *                      a cada três minutos durante 3/4 dias até que o problema fosse reportado.
             */

            transaction.Rollback();
        }
    }

    public void MessageSendFailure_WithoutTransaction()
    {
        var messages = _repository
                            .Query()
                            .Where(e => !e.IsSent && !e.Error)
                            .ToList();

        messages.ForEach(message =>
        {
            try
            {
                message.Send();
            }catch(Exception ex)
            {
                /**
                 * O uso das transações não é o centro do problema nesse cenário, mas o uso delas pode induzir ao tratamento 
                 *     conjunto das mensagens, enquanto, por serem independentes, devem ser tratadas de maneira separada.
                 */

                message.Error = true;
                // Pode-se adicionar um tratamento especifico(como notificar o usuário do erro ou procurar os dados em outra tabela)
            }
        });

        _repository.UpdateMany(messages);
    }

    private void SetupMessages()
    {
        {
            var messages = new List<Message>()
            {
                new Message("Olá", "erikrrn04@gmail.com"),
                new Message("Bom dia", "meuEmail@gmail.com"),
                new Message("Segue em anexo", null), // erro proposital de destinatário não encontrado
                new Message("Recebido a documentação", "doc@gmail.com")
            };

            _repository.InsertMany(messages);
        }
    }
}