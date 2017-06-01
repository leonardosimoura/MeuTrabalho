using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.Data.Migrations
{
    [Migration(20170528, "Initial Migration")]
    public class InitialMigration : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.Sql(@"
                            CREATE TABLE [dbo].[Usuario] (
                                [UsuarioId]               UNIQUEIDENTIFIER NOT NULL,
                                [Nome]                    NVARCHAR (250)   NOT NULL,
                                [Email]                   VARCHAR (250)    NOT NULL,
                                [Senha]                   VARCHAR (250)    NOT NULL,
                                [DataCadastro]            DATE             NULL,
                                [DataConfirmacaoCadastro] DATE             NULL,
                                CONSTRAINT [PK_Usuario] PRIMARY KEY NONCLUSTERED ([UsuarioId] ASC)
                            );
                            GO
                            CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuario_Email]
                                ON [dbo].[Usuario]([Email] ASC);
                            GO                           
            ");
        }
    }
}
