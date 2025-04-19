CREATE BASE CasoEstudioSM;

USE CasoEstudioSM;

CREATE TABLE CasasSistema (
    IdCasa BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    DescripcionCasa VARCHAR(30),                     
    PrecioCasa DECIMAL(10,2) NOT NULL,               
    UsuarioAlquiler VARCHAR(30) NULL,                
    FechaAlquiler DATETIME NULL                     
);


INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa], [PrecioCasa], [UsuarioAlquiler], [FechaAlquiler])
VALUES ('Casa en San José', 190000, NULL, NULL);

INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa], [PrecioCasa], [UsuarioAlquiler], [FechaAlquiler])
VALUES ('Casa en Alajuela', 145000, NULL, NULL);

INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa], [PrecioCasa], [UsuarioAlquiler], [FechaAlquiler])
VALUES ('Casa en Cartago', 115000, NULL, NULL);

INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa], [PrecioCasa], [UsuarioAlquiler], [FechaAlquiler])
VALUES ('Casa en Heredia', 122000, NULL, NULL);

INSERT INTO [dbo].[CasasSistema] ([DescripcionCasa], [PrecioCasa], [UsuarioAlquiler], [FechaAlquiler])
VALUES ('Casa en Guanacaste', 105000, NULL, NULL);
 