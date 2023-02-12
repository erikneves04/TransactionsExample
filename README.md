# Unit of Work - Transactions

<div align="justify">
Como o nome sugere, a unidade de trabalho consiste em um padrão de comunicação com o banco de dados que se baseia em transações que, de certa forma, encapsulam os acessos ao serviço de armazenamento.

Isso, por sua vez, é feito por meio da criação de conexões que gerenciam a relação com o banco de dados. Cada conexão pode ter SOMENTE UMA transação em curso, entretanto é possível compartilhar a mesma transação com diversas conexões. Ao executar comandos que acessem o banco, como o SaveChanges() é criada uma transação para atender àquela demanda, implementando manualmente pode-se criar transações com um escopo personalizável, trazendo para o desenvolvedor a responsabilidade de gerenciar seu funcionamento(commit / rollback).

O ideal é que uma sequência de queries interdependentes(que manipulam dados correlatos) sejam tratados em uma transação, tendo em vista que isso possibilita que erros encontrados em sua execução sejam tratados de forma conjunta, garantindo assim a integridade das informações armazenadas.

<b>Exemplo:</b> Uma sequência de solicitações bancárias(simples) é executada para um TED, desde a checagem do saldo, do débito, até o depósito propriamente dito em uma segunda conta. Caso ocorra um erro no ponto de depósito na conta final é possível reverter toda a transação e garantir que o dinheiro permaneça na conta de origem, minimizando erros.

<b>Commit</b> → Dispara a transação, executando suas queries no banco de dados;</br>
<b>RollBack</b> → Retém a transação, impedindo que suas alterações sejam executadas no banco;

Um contexto de transação deve ser descartado após ter suas ações finalizadas com um commit ou rollback.

Pode-se criar save points (transaction.CreateSavepoint) em transações, possibilitando que em caso de erro você a reverta (transaction.RollBackToSavepoint) para um estado desejado e não necessariamente o estado inicial.
</div>
