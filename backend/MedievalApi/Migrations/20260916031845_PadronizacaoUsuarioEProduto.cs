using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedievalApi.Migrations
{
    /// <inheritdoc />
    public partial class PadronizacaoUsuarioEProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "usuario");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "usuario",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "usuario",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "usuario",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "usuario",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UltimoLogin",
                table: "usuario",
                newName: "ultimo_login");

            migrationBuilder.RenameColumn(
                name: "SenhaHash",
                table: "usuario",
                newName: "senha_hash");

            migrationBuilder.RenameColumn(
                name: "GrupoUsuario",
                table: "usuario",
                newName: "grupo_usuario");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "usuario",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "AtualizadoEm",
                table: "usuario",
                newName: "atualizado_em");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "usuario",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "Ativo",
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "usuario",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "grupo_usuario",
                table: "usuario",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "Consulta",
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "criado_em",
                table: "usuario",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "atualizado_em",
                table: "usuario",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "id");

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    id = table.Column<int>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    criado_em = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unidade_medida",
                columns: table => new
                {
                    id = table.Column<int>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    sigla = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    tipo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    criado_em = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unidade_medida", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "uuid", nullable: false),
                    categoria_id = table.Column<int>(type: "uuid", nullable: false),
                    unidade_medida_id = table.Column<int>(type: "uuid", nullable: false),
                    codigo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    descricao = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    estoque_minimo = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: false, defaultValue: 0m),
                    estoque_maximo = table.Column<decimal>(type: "TEXT", precision: 14, scale: 3, nullable: true),
                    custo_medio_estoque = table.Column<decimal>(type: "TEXT", precision: 14, scale: 4, nullable: false, defaultValue: 0m),
                    ultimo_custo = table.Column<decimal>(type: "TEXT", precision: 14, scale: 4, nullable: false, defaultValue: 0m),
                    validade_dias = table.Column<int>(type: "INTEGER", nullable: true),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    criado_em = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    atualizado_em = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.id);
                    table.ForeignKey(
                        name: "fk_produto_categoria",
                        column: x => x.categoria_id,
                        principalTable: "categoria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_produto_unidade_medida",
                        column: x => x.unidade_medida_id,
                        principalTable: "unidade_medida",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_categoria_ativo",
                table: "categoria",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "ix_categoria_nome",
                table: "categoria",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_produto_ativo",
                table: "produto",
                column: "ativo");

            migrationBuilder.CreateIndex(
                name: "ix_produto_categoria_ativo",
                table: "produto",
                columns: new[] { "categoria_id", "ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_produto_categoria_id",
                table: "produto",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_produto_codigo",
                table: "produto",
                column: "codigo",
                unique: true,
                filter: "codigo IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_produto_nome",
                table: "produto",
                column: "nome");

            migrationBuilder.CreateIndex(
                name: "ix_produto_unidade_medida_id",
                table: "produto",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "unidade_medida");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "ix_usuario_email",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "ix_usuario_grupo_usuario",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "ix_usuario_status",
                table: "usuario");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Usuarios",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Usuarios",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ultimo_login",
                table: "Usuarios",
                newName: "UltimoLogin");

            migrationBuilder.RenameColumn(
                name: "senha_hash",
                table: "Usuarios",
                newName: "SenhaHash");

            migrationBuilder.RenameColumn(
                name: "grupo_usuario",
                table: "Usuarios",
                newName: "GrupoUsuario");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Usuarios",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "atualizado_em",
                table: "Usuarios",
                newName: "AtualizadoEm");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldDefaultValue: "Ativo");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "GrupoUsuario",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldDefaultValue: "Consulta");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CriadoEm",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");
        }
    }
}
