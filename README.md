# ProjIntegSATCMedieval
## Sistema Web de controle de estoque para a Churrascaria Medieval

Documentação do Sistema de Gerenciamento de Estoque para Restaurante 


### Introdução: 

O aplicativo de gerenciamento de estoque tem como objetivo digitalizar e centralizar o processo de controle de estoque, realização de pedidos, cotação com fornecedores, comparação de preços e conferência das mercadorias recebidas. 

Atualmente, grande parte desse processo é realizada manualmente, utilizando listas impressas, contagem física dos produtos, planilhas do Excel, mensagens para fornecedores e conferência manual das notas fiscais. 

O sistema será desenvolvido para substituir essas etapas por um fluxo digital, permitindo que os funcionários responsáveis pelo estoque realizem as atividades diretamente pelo aplicativo. 

A principal finalidade é reduzir erros de contagem e digitação, facilitar a comparação entre fornecedores, manter um histórico das compras e permitir um melhor controle dos produtos que foram pedidos e recebidos. 

** Funcionamento atual do estoque: **

Atualmente, o processo de estoque é realizado seguindo algumas etapas principais: 

Impressão das listas de estoque;  

Contagem física dos produtos;  

Transferência das quantidades para o Excel;  

Realização de cotações com fornecedores;  

Comparação dos preços e definição das compras, manualmente; 

Envio dos pedidos aos fornecedores;  

Recebimento e conferência das mercadorias; 

O aplicativo será desenvolvido considerando esse processo existente, procurando digitalizar as atividades sem alterar desnecessariamente a rotina dos funcionários. 

Primeiro passo — Impressão das listas de estoque 

Atualmente, as listas de estoque são impressas de acordo com a categoria dos produtos e o dia previsto para realização do pedido. 

A organização atual é: 

Categoria 

Dia da lista 

Dia previsto para chegada 

Bebidas 

Domingo 

Terça-feira 

Carnes 

Quinta-feira 

Sexta-feira 

Linha seca 

Terça-feira 

Quarta-feira 

Essas são algumas das listas utilizadas pelos funcionários para realizar a contagem física dos produtos disponíveis no restaurante. 

Incorporação no aplicativo 

No aplicativo, as listas impressas serão substituídas por listas digitais de estoque. 

O sistema poderá organizar automaticamente os produtos de acordo com sua categoria e o dia de contagem. 

 

O responsável poderá acessar a lista diretamente pelo aplicativo e informar a quantidade encontrada no estoque. 

O sistema também poderá identificar automaticamente quais listas precisam ser realizadas naquele dia. 

Benefícios 

A digitalização dessa etapa permitirá: 

Eliminar a necessidade de impressão das listas;  

Evitar perda ou dano das folhas;  

Manter um histórico das contagens;  

Identificar quem realizou a contagem;  

Facilitar a atualização dos produtos;  

Organizar automaticamente as listas por categoria;  

Evitar que produtos sejam esquecidos durante a contagem. 

 

Segundo passo — Contagem dos itens para o pedido 

Após receber ou abrir a lista de estoque, o funcionário realiza a contagem física dos produtos. 

Essa etapa é importante porque determina quais produtos precisam ser comprados e em qual quantidade. 

Funcionamento atual 

O funcionário verifica fisicamente o estoque e anota a quantidade disponível. 

Por exemplo: 

Produto          Quantidade encontrada 
Coca-Cola 2L             8 
Guaraná 2L                 5 
Água 500ml              20 

Posteriormente, essas informações são utilizadas para determinar a quantidade necessária para o próximo pedido. 

 

Incorporação no aplicativo 

O aplicativo terá um campo específico para registrar a quantidade encontrada. 

Exemplo: 

Produto: Coca-Cola 2L 
 
Estoque esperado: 10 
Estoque contado: 8 
Quantidade para compra: 2 

 

Uma possibilidade é o sistema calcular automaticamente a quantidade necessária utilizando o estoque mínimo definido para cada produto. 

Por exemplo: 

Estoque mínimo: 10 
Quantidade encontrada: 6 
 
Quantidade sugerida para compra: 
10 - 6 = 4 unidades 

Entretanto, o sistema deverá permitir que o funcionário altere manualmente a quantidade sugerida, pois a necessidade de compra pode variar de acordo com o movimento esperado do restaurante. 

 

Terceiro passo — Transferência da contagem para o Excel 

Atualmente, após realizar a contagem física, os dados anotados na lista são transferidos para uma planilha do Excel. 

Essa etapa representa uma segunda digitação das informações. 

Problema do processo atual: 

O funcionário precisa: 

Contar o produto;  

Anotar na lista;  

Abrir o Excel;  

Localizar o produto;  

Digitar novamente a quantidade.  

Isso pode causar erros, como: 

Digitação incorreta;  

Quantidade digitada no produto errado;  

Produtos esquecidos;  

Informações duplicadas;  

Perda de tempo.  

Incorporação no aplicativo: 

O Excel será substituído pelo banco de dados do aplicativo. 

Quando o funcionário informar a quantidade diretamente no sistema, essa informação será armazenada automaticamente. 

Exemplo: 

Produto: Arroz 5kg 
Quantidade contada: 12 
Data: 03/09/2026 
Responsável: Funcionário 

Esses dados poderão ser utilizados posteriormente na geração dos pedidos e no histórico do estoque. 

Dessa maneira, a contagem realizada no estoque já estará disponível para as próximas etapas, eliminando a necessidade de transferir os dados manualmente para o Excel. 

 

 

Quarto passo — Cotação com fornecedores 

Depois de definir os produtos e quantidades necessárias, começa a etapa de cotação. 

Atualmente, existem duas situações: 

Para pedidos maiores, a planilha do Excel é enviada para vários fornecedores;  

Para pedidos pequenos, o pedido pode ser realizado manualmente.  

Funcionamento atual 

O responsável envia a lista para diferentes fornecedores e recebe os preços dos produtos. 

Exemplo: 

Produto: Arroz 5kg 
Quantidade: 10 
 
Fornecedor A: R$ 25,00 
Fornecedor B: R$ 23,50 
Fornecedor C: R$ 26,00 

Incorporação no aplicativo 

O aplicativo poderá possuir um cadastro de fornecedores contendo informações como: 

Nome;  

Telefone;  

E-mail;  

Produtos fornecidos;  

Condições comerciais;  

Histórico de compras.  

O responsável poderá selecionar os produtos necessários e iniciar uma cotação. 

O sistema poderá gerar uma lista de cotação contendo: 

Arroz 5kg — 10 unidades 
Feijão 1kg — 20 unidades 
Óleo 900ml — 15 unidades 

Essa cotação poderá ser associada aos fornecedores selecionados. 

Para pedidos pequenos, o sistema também poderá permitir que o responsável registre diretamente uma compra sem passar por todo o processo de cotação. 

 

 

 

Quinto passo — Comparação de preços e definição da compra 

Após receber os preços dos fornecedores, é necessário comparar os valores para decidir de qual fornecedor cada produto será comprado. 

Essa etapa é importante porque o menor preço nem sempre representa a melhor opção de compra. 

Exemplo 

Produto 

Fornecedor A 

Fornecedor B 

Fornecedor C 

Arroz 5kg 

R$ 25,00 

R$ 23,00 

R$ 24,50 

Carne X 

R$ 32,00 

R$ 35,00 

R$ 31,00 

Molho X 

R$ 10,00 

R$ 9,00 

R$ 11,00 

Embora o fornecedor B possua o menor preço do arroz, pode existir uma preferência pelo fornecedor A devido à qualidade do produto, marca, prazo de entrega ou outros fatores comerciais. 

Incorporação no aplicativo 

O sistema deverá apresentar os preços recebidos de maneira organizada, facilitando a comparação. 

Exemplo: 

ARROZ 5KG 
 
Fornecedor A: R$ 25,00 
Fornecedor B: R$ 23,00 
Fornecedor C: R$ 24,50 
 
Fornecedor escolhido: Fornecedor A 
Motivo: Qualidade / Marca 

O sistema poderá destacar o menor preço, mas a decisão final continuará sendo do responsável pela compra. 

Isso é importante porque o aplicativo deve funcionar como uma ferramenta de apoio à decisão e não obrigar o usuário a comprar sempre do fornecedor mais barato. 

Critérios que podem influenciar a decisão: 

Além do preço, poderão ser considerados: 

Qualidade;  

Marca;  

Prazo de entrega;  

Disponibilidade do produto;  

Histórico do fornecedor;  

Confiabilidade;  

Quantidade mínima para pedido. 

 

Sexto passo — Confirmação do pedido 

Após decidir quais produtos serão comprados de cada fornecedor, o responsável finaliza os pedidos. 

Funcionamento atual 

Atualmente, o pedido é organizado manualmente e enviado ao fornecedor, geralmente por algum meio de comunicação utilizado pela empresa. 

Incorporação no aplicativo 

O aplicativo poderá criar um pedido de compra automaticamente após a definição dos fornecedores. 

Exemplo: 

PEDIDO #00125 
 
Fornecedor: Fornecedor A 
 
Produto             Quantidade 
Arroz 5kg              10 
Óleo 900ml              15 
Feijão 1kg              20 
 
Valor estimado: R$ 850,00 
 
Status: Aguardando confirmação 

O pedido poderá possuir diferentes estados: 

Cotação → Aguardando decisão → Pedido criado → Enviado ao fornecedor → Confirmado → Recebido → Conferido; 

Isso permitirá acompanhar o pedido durante todo o processo. 

 

Sétimo passo — Recebimento e conferência dos pedidos 

Quando os produtos chegam ao restaurante, começa uma das etapas mais importantes do processo: a conferência da entrega. 

O responsável deve verificar se os produtos entregues correspondem ao pedido realizado e conferir as informações presentes na nota fiscal. 

Conferência: 

Deve ser verificado: 

Produto;  

Quantidade;  

Marca;  

Valores;  

Produtos faltantes;  

Produtos adicionais;  

Informações da nota fiscal.  

O funcionário compara a nota fiscal e os produtos recebidos com o pedido registrado. 

Tratamento de itens faltantes 

Atualmente, quando um item solicitado não chega, o procedimento depende do fornecedor. 

Existem fornecedores que, quando identificam um item faltante, solicitam a devolução de todo o pedido. 

Outros fornecedores permitem que somente o item faltante seja enviado posteriormente. 

Também pode ocorrer uma entrega de emergência contendo apenas o produto que não foi entregue originalmente. 

Incorporação no aplicativo 

O sistema deverá permitir registrar cada divergência encontrada durante a conferência. 

 

Exemplo: 

Pedido #00125 

Produto: Carne X 

Quantidade solicitada: 10 

Quantidade recebida: 8 

Divergência: 2 unidades faltantes 

O responsável poderá selecionar o procedimento adotado: 

☐ Devolver pedido inteiro 
☐ Aguardar próxima entrega 
☐ Solicitar entrega de emergência 
☐ Outro procedimento 

Também poderá ser registrado o prazo previsto para recebimento do item faltante. 

O status do pedido poderá permanecer como: 

"Recebido parcialmente" 

até que a situação seja resolvida. 

 

Tratamento de itens adicionais: 

Outro problema que pode ocorrer durante a entrega é o recebimento de produtos que não foram solicitados. 

Isso pode acontecer por erro na separação do fornecedor ou porque o produto pertence ao pedido de outro restaurante. 

Funcionamento atual: 

Durante a conferência, o responsável identifica o produto adicional e solicita ao entregador que faça a devolução. 

Isso é especialmente importante quando existe a possibilidade de o produto pertencer a outro restaurante. 

Incorporação no aplicativo: 

O sistema deverá permitir registrar o produto adicional. 

Exemplo: 

Produto recebido: Refrigerante X 
Quantidade recebida: 5 
Quantidade solicitada: 0 
 
Situação: Produto adicional 
Ação: Devolução solicitada 

O sistema poderá registrar: 

Produto;  

Quantidade;  

Fornecedor;  

Pedido relacionado;  

Motivo da devolução;  

Data;  

Responsável pela conferência;  

Status da devolução.  

 

Fluxo completo do aplicativo 

Com a implantação do sistema, o processo poderá funcionar da seguinte maneira: 

LISTA DE ESTOQUE 

       ↓ 

CONTAGEM DOS PRODUTOS 

       ↓ 

REGISTRO NO SISTEMA 

       ↓ 

DEFINIÇÃO DA NECESSIDADE DE COMPRA 

       ↓ 

COTAÇÃO COM FORNECEDORES 

       ↓ 

COMPARAÇÃO DE PREÇOS 

       ↓ 

ESCOLHA DOS FORNECEDORES 

       ↓ 

GERAÇÃO DOS PEDIDOS 

       ↓ 

CONFIRMAÇÃO DOS FORNECEDORES 

       ↓ 

RECEBIMENTO DOS PRODUTOS 

       ↓ 

CONFERÊNCIA COM O PEDIDO E NOTA FISCAL 

       ↓ 

┌─────────────────────────────┐ 

│  Pedido está correto?                         │ 

└─────────────────────────────┘ 

       ↓                 ↓ 

      SIM               NÃO 

       ↓                 ↓ 

Atualizar estoque    Registrar divergência 

       ↓                 ↓ 

Pedido concluído     Faltante / Adicional 

                         ↓ 

                    Resolver ocorrência 

 

Principais funcionalidades do aplicativo 

Com base no processo atual, o aplicativo poderá ser dividido nas seguintes funcionalidades: 

Cadastro de produtos: 

Permite cadastrar e administrar os produtos utilizados pelo restaurante. 

Informações possíveis: 

Nome;  

Categoria;  

Unidade de medida;  

Marca;  

Estoque mínimo;  

Estoque atual;  

Fornecedores associados;  

Status do produto.  

Controle de estoque: 

Permite realizar as contagens diretamente pelo aplicativo. 

O sistema registra: 

Quantidade atual;  

Data da contagem;  

Funcionário responsável;  

Histórico de alterações.  

Listas de estoque 

O sistema gera automaticamente as listas de acordo com a programação: 

Bebidas;  

Carnes;  

Linha seca;  

Outras categorias que possam ser adicionadas posteriormente. 

Cadastro de fornecedores 

Permite manter informações dos fornecedores e seus produtos. 

Exemplo: 

Fornecedor: 
Empresa XYZ 
 

Produtos: 
- Arroz 
- Feijão 
- Óleo 
- Farinha 
 
Contato: 
Telefone / E-mail 

Sistema de cotação 

Permite registrar os preços enviados pelos fornecedores. 

O sistema relaciona: 

Produto → Fornecedor → Quantidade → Preço 

Isso facilita a comparação das opções. 

Comparação de fornecedores: 

Apresenta os preços dos diferentes fornecedores para o mesmo produto. 

O menor valor pode ser destacado, porém a escolha final é realizada pelo responsável. 

 Geração de pedidos: 

Após a escolha dos fornecedores, o sistema gera os pedidos de compra automaticamente. 

Cada pedido possuirá um número e um status. 

Recebimento e conferência: 

Permite comparar: 

Pedido realizado × Produto recebido × Nota fiscal 

Dessa maneira, o sistema consegue identificar divergências. 

Controle de divergências: 

O sistema deverá registrar situações como: 

Produto faltante;  

Produto adicional;  

Quantidade incorreta;  

Produto incorreto;  

Devolução;  

Entrega posterior;  

Entrega de emergência. 

Comparação entre o processo atual e o aplicativo 

Processo atual 

Processo no aplicativo 

Lista impressa 

Lista digital 

Contagem manual em papel 

Contagem registrada diretamente no sistema 

Transferência para Excel 

Dados armazenados automaticamente 

Cotação por planilha/mensagem 

Cotação registrada no sistema 

Comparação manual 

Comparação organizada por produto 

Pedido montado manualmente 

Pedido gerado pelo sistema 

Conferência manual 

Conferência vinculada ao pedido 

Divergências anotadas separadamente 

Divergências registradas no sistema 

Histórico limitado 

Histórico de estoque e compras 

Acompanhamento manual 

Status do pedido 

 
