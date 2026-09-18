using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedievalApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fornecedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    razao_social = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    nome_fantasia = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    documento = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    inscricao_estadual = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    telefone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    nome_contato = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    cep = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    logradouro = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    numero = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    complemento = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    bairro = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    cidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    estado = table.Column<string>(type: "TEXT", maxLength: 2, nullable: true),
                    dias_entrega = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    condicao_pagamento = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "grupo_produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupo_produto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unidade_medida",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    sigla = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    tipo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unidade_medida", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    senha_hash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    grupo_usuario = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Ativo"),
                    ultimo_login = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    grupo_produto_id = table.Column<int>(type: "uuid", nullable: false),
                    unidade_medida_id = table.Column<int>(type: "INTEGER", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    descricao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    estoque_minimo = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false, defaultValue: 0m),
                    estoque_maximo = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: true),
                    custo_medio_estoque = table.Column<decimal>(type: "TEXT", precision: 14, scale: 4, nullable: false, defaultValue: 0m),
                    ultimo_custo = table.Column<decimal>(type: "TEXT", precision: 14, scale: 4, nullable: false, defaultValue: 0m),
                    validade_dias = table.Column<int>(type: "INTEGER", nullable: true),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.id);
                    table.ForeignKey(
                        name: "fk_produto_grupo_produto",
                        column: x => x.grupo_produto_id,
                        principalTable: "grupo_produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_produto_unidade_medida",
                        column: x => x.unidade_medida_id,
                        principalTable: "unidade_medida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "auditoria",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    usuario_id = table.Column<int>(type: "INTEGER", nullable: true),
                    registro_id = table.Column<int>(type: "INTEGER", nullable: true),
                    tabela = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    tipo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    dados_anteriores = table.Column<string>(type: "jsonb", nullable: true),
                    dados_novos = table.Column<string>(type: "jsonb", nullable: true),
                    ip = table.Column<string>(type: "TEXT", maxLength: 45, nullable: true),
                    user_agent = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditoria", x => x.id);
                    table.ForeignKey(
                        name: "fk_auditoria_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "cotacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    usuario_id = table.Column<int>(type: "INTEGER", nullable: false),
                    data_solicitacao = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    data_limite_resposta = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Rascunho"),
                    observacao = table.Column<string>(type: "TEXT", nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cotacao", x => x.id);
                    table.ForeignKey(
                        name: "fk_cotacao_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "saida_estoque",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    usuario_id = table.Column<int>(type: "INTEGER", nullable: false),
                    data_saida = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    tipo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Rascunho"),
                    motivo = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    observacao = table.Column<string>(type: "TEXT", nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saida_estoque", x => x.id);
                    table.ForeignKey(
                        name: "fk_saida_estoque_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "estoque",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    produto_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade_disponivel = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false, defaultValue: 0m),
                    valor_total = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: false, defaultValue: 0m),
                    ultima_entrada = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    ultima_saida = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estoque", x => x.id);
                    table.ForeignKey(
                        name: "fk_estoque_produto",
                        column: x => x.produto_id,
                        principalTable: "produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido_compra",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fornecedor_id = table.Column<int>(type: "INTEGER", nullable: false),
                    usuario_id = table.Column<int>(type: "INTEGER", nullable: false),
                    cotacao_id = table.Column<int>(type: "INTEGER", nullable: true),
                    data_pedido = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    data_prevista_entrega = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    data_recebimento = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    status = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false, defaultValue: "Rascunho"),
                    total = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: false, defaultValue: 0m),
                    observacao = table.Column<string>(type: "TEXT", nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido_compra", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedido_compra_cotacao",
                        column: x => x.cotacao_id,
                        principalTable: "cotacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_pedido_compra_fornecedor",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pedido_compra_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "saida_estoque_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    saida_estoque_id = table.Column<int>(type: "INTEGER", nullable: false),
                    produto_id = table.Column<int>(type: "INTEGER", nullable: false),
                    unidade_medida_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false),
                    custo_unitario = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: false, defaultValue: 0m),
                    custo_total = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: false, defaultValue: 0m),
                    lote = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    data_validade = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saida_estoque_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_saida_estoque_item_produto",
                        column: x => x.produto_id,
                        principalTable: "produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_saida_estoque_item_saida_estoque",
                        column: x => x.saida_estoque_id,
                        principalTable: "saida_estoque",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_saida_estoque_item_unidade_medida",
                        column: x => x.unidade_medida_id,
                        principalTable: "unidade_medida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "movimentacao_estoque",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    estoque_id = table.Column<int>(type: "INTEGER", nullable: false),
                    usuario_id = table.Column<int>(type: "INTEGER", nullable: false),
                    referencia_id = table.Column<int>(type: "INTEGER", nullable: true),
                    referencia_tipo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    tipo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    origem = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    quantidade = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false),
                    quantidade_anterior = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false),
                    quantidade_posterior = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false),
                    custo_unitario = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: true),
                    valor_total = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: true),
                    lote = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    data_validade = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimentacao_estoque", x => x.id);
                    table.ForeignKey(
                        name: "fk_movimentacao_estoque_estoque",
                        column: x => x.estoque_id,
                        principalTable: "estoque",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_movimentacao_estoque_usuario",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cotacao_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cotacao_id = table.Column<int>(type: "INTEGER", nullable: false),
                    produto_id = table.Column<int>(type: "INTEGER", nullable: false),
                    unidade_medida_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantidade = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false),
                    cotacao_item_fornecedor_escolhido_id = table.Column<int>(type: "INTEGER", nullable: true),
                    status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Pendente"),
                    observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cotacao_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_cotacao_item_cotacao",
                        column: x => x.cotacao_id,
                        principalTable: "cotacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cotacao_item_produto",
                        column: x => x.produto_id,
                        principalTable: "produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cotacao_item_unidade_medida",
                        column: x => x.unidade_medida_id,
                        principalTable: "unidade_medida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cotacao_item_fornecedor",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cotacao_item_id = table.Column<int>(type: "INTEGER", nullable: false),
                    fornecedor_id = table.Column<int>(type: "INTEGER", nullable: false),
                    valor_unitario = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: false),
                    prazo_entrega_dias = table.Column<int>(type: "INTEGER", nullable: true),
                    condicao_pagamento = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cotacao_item_fornecedor", x => x.id);
                    table.ForeignKey(
                        name: "fk_cotacao_item_fornecedor_cotacao_item",
                        column: x => x.cotacao_item_id,
                        principalTable: "cotacao_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cotacao_item_fornecedor_fornecedor",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pedido_compra_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    pedido_compra_id = table.Column<int>(type: "INTEGER", nullable: false),
                    produto_id = table.Column<int>(type: "INTEGER", nullable: false),
                    unidade_medida_id = table.Column<int>(type: "INTEGER", nullable: false),
                    cotacao_item_id = table.Column<int>(type: "INTEGER", nullable: true),
                    quantidade_solicitada = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false),
                    quantidade_recebida = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false, defaultValue: 0m),
                    valor_unitario = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: false),
                    total = table.Column<decimal>(type: "TEXT", precision: 16, scale: 4, nullable: false, defaultValue: 0m),
                    observacao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    criado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido_compra_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedido_compra_item_cotacao_item",
                        column: x => x.cotacao_item_id,
                        principalTable: "cotacao_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_pedido_compra_item_pedido_compra",
                        column: x => x.pedido_compra_id,
                        principalTable: "pedido_compra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pedido_compra_item_produto",
                        column: x => x.produto_id,
                        principalTable: "produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pedido_compra_item_unidade_medida",
                        column: x => x.unidade_medida_id,
                        principalTable: "unidade_medida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_criado_em",
                table: "auditoria",
                column: "criado_em");

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_registro_id",
                table: "auditoria",
                column: "registro_id");

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_tabela",
                table: "auditoria",
                column: "tabela");

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_tabela_registro",
                table: "auditoria",
                columns: new[] { "tabela", "registro_id" });

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_tipo",
                table: "auditoria",
                column: "tipo");

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_usuario_id",
                table: "auditoria",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_data_limite_resposta",
                table: "cotacao",
                column: "data_limite_resposta");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_data_solicitacao",
                table: "cotacao",
                column: "data_solicitacao");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_status",
                table: "cotacao",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_usuario_id",
                table: "cotacao",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_item_cotacao_id",
                table: "cotacao_item",
                column: "cotacao_id");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_item_fornecedor_escolhido_id",
                table: "cotacao_item",
                column: "cotacao_item_fornecedor_escolhido_id");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_item_produto_id",
                table: "cotacao_item",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_item_status",
                table: "cotacao_item",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_item_unidade_medida_id",
                table: "cotacao_item",
                column: "unidade_medida_id");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_item_fornecedor_fornecedor_id",
                table: "cotacao_item_fornecedor",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "ix_cotacao_item_fornecedor_unique",
                table: "cotacao_item_fornecedor",
                columns: new[] { "cotacao_item_id", "fornecedor_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_estoque_produto_id",
                table: "estoque",
                column: "produto_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_estoque_quantidade_disponivel",
                table: "estoque",
                column: "quantidade_disponivel");

            migrationBuilder.CreateIndex(
                name: "ix_fornecedor_ativo",
                table: "fornecedor",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "ix_fornecedor_documento",
                table: "fornecedor",
                column: "documento");

            migrationBuilder.CreateIndex(
                name: "ix_fornecedor_nome_fantasia",
                table: "fornecedor",
                column: "nome_fantasia");

            migrationBuilder.CreateIndex(
                name: "ix_fornecedor_razao_social",
                table: "fornecedor",
                column: "razao_social");

            migrationBuilder.CreateIndex(
                name: "ix_grupo_produto_ativo",
                table: "grupo_produto",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "ix_grupo_produto_nome",
                table: "grupo_produto",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_movimentacao_estoque_criado_em",
                table: "movimentacao_estoque",
                column: "criado_em");

            migrationBuilder.CreateIndex(
                name: "ix_movimentacao_estoque_estoque_id",
                table: "movimentacao_estoque",
                column: "estoque_id");

            migrationBuilder.CreateIndex(
                name: "ix_movimentacao_estoque_origem",
                table: "movimentacao_estoque",
                column: "origem");

            migrationBuilder.CreateIndex(
                name: "ix_movimentacao_estoque_referencia",
                table: "movimentacao_estoque",
                columns: new[] { "referencia_tipo", "referencia_id" });

            migrationBuilder.CreateIndex(
                name: "ix_movimentacao_estoque_tipo",
                table: "movimentacao_estoque",
                column: "tipo");

            migrationBuilder.CreateIndex(
                name: "ix_movimentacao_estoque_usuario_id",
                table: "movimentacao_estoque",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_cotacao_id",
                table: "pedido_compra",
                column: "cotacao_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_data_pedido",
                table: "pedido_compra",
                column: "data_pedido");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_fornecedor_id",
                table: "pedido_compra",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_status",
                table: "pedido_compra",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_usuario_id",
                table: "pedido_compra",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_item_cotacao_item_id",
                table: "pedido_compra_item",
                column: "cotacao_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_item_pedido_compra_id",
                table: "pedido_compra_item",
                column: "pedido_compra_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_item_produto_id",
                table: "pedido_compra_item",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_compra_item_unidade_medida_id",
                table: "pedido_compra_item",
                column: "unidade_medida_id");

            migrationBuilder.CreateIndex(
                name: "ix_produto_ativo",
                table: "produto",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "ix_produto_codigo",
                table: "produto",
                column: "codigo",
                unique: true,
                filter: "codigo IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_produto_grupo_produto_ativo",
                table: "produto",
                columns: new[] { "grupo_produto_id", "ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_produto_grupo_produto_id",
                table: "produto",
                column: "grupo_produto_id");

            migrationBuilder.CreateIndex(
                name: "ix_produto_nome",
                table: "produto",
                column: "nome");

            migrationBuilder.CreateIndex(
                name: "ix_produto_unidade_medida_id",
                table: "produto",
                column: "unidade_medida_id");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_data_saida",
                table: "saida_estoque",
                column: "data_saida");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_status",
                table: "saida_estoque",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_tipo",
                table: "saida_estoque",
                column: "tipo");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_usuario_id",
                table: "saida_estoque",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_item_data_validade",
                table: "saida_estoque_item",
                column: "data_validade");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_item_lote",
                table: "saida_estoque_item",
                column: "lote");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_item_produto_id",
                table: "saida_estoque_item",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_item_saida_estoque_id",
                table: "saida_estoque_item",
                column: "saida_estoque_id");

            migrationBuilder.CreateIndex(
                name: "ix_saida_estoque_item_unidade_medida_id",
                table: "saida_estoque_item",
                column: "unidade_medida_id");

            migrationBuilder.CreateIndex(
                name: "ix_unidade_medida_ativo",
                table: "unidade_medida",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "ix_unidade_medida_sigla",
                table: "unidade_medida",
                column: "sigla",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_unidade_medida_tipo",
                table: "unidade_medida",
                column: "tipo");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_email",
                table: "usuario",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuario_grupo_usuario",
                table: "usuario",
                column: "grupo_usuario");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_status",
                table: "usuario",
                column: "status");

            migrationBuilder.AddForeignKey(
                name: "fk_cotacao_item_fornecedor_escolhido",
                table: "cotacao_item",
                column: "cotacao_item_fornecedor_escolhido_id",
                principalTable: "cotacao_item_fornecedor",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cotacao_usuario",
                table: "cotacao");

            migrationBuilder.DropForeignKey(
                name: "fk_cotacao_item_cotacao",
                table: "cotacao_item");

            migrationBuilder.DropForeignKey(
                name: "fk_cotacao_item_fornecedor_escolhido",
                table: "cotacao_item");

            migrationBuilder.DropTable(
                name: "auditoria");

            migrationBuilder.DropTable(
                name: "movimentacao_estoque");

            migrationBuilder.DropTable(
                name: "pedido_compra_item");

            migrationBuilder.DropTable(
                name: "saida_estoque_item");

            migrationBuilder.DropTable(
                name: "estoque");

            migrationBuilder.DropTable(
                name: "pedido_compra");

            migrationBuilder.DropTable(
                name: "saida_estoque");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "cotacao");

            migrationBuilder.DropTable(
                name: "cotacao_item_fornecedor");

            migrationBuilder.DropTable(
                name: "cotacao_item");

            migrationBuilder.DropTable(
                name: "fornecedor");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "grupo_produto");

            migrationBuilder.DropTable(
                name: "unidade_medida");
        }
    }
}
