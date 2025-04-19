---------- Procedimiento MostrarCasas ----------
-- Muestra el punto 1 de Vista Consulta de casas

CREATE PROCEDURE MostrarCasas
AS
BEGIN
    SELECT 
        DescripcionCasa, 
        PrecioCasa, 
        UsuarioAlquiler, 
       
        CASE 
            WHEN Estado = 'Disponible' THEN 'Disponible'
            ELSE 'Reservada'
        END AS Estado,
        
        FORMAT(FechaAlquiler, 'dd/MM/yyyy') AS Fecha
    FROM 
        CasasSistema
END

---------- Procedimiento MostrarCasasPorPrecio ----------
-- Muestra el punto 2 de Vista Consulta de casas

CREATE PROCEDURE MostrarCasasPorPrecio
AS
BEGIN
    SELECT 
        DescripcionCasa, 
        PrecioCasa, 
        UsuarioAlquiler, 
        CASE 
            WHEN Estado = 'Disponible' THEN 'Disponible'
            ELSE 'Reservada'
        END AS Estado,
        FORMAT(FechaAlquiler, 'dd/MM/yyyy') AS Fecha
    FROM 
        CasasSistema
    WHERE 
        PrecioCasa BETWEEN 115000 AND 180000
END

---------- Procedimiento MostrarCasasOrdenadasPorEstado ----------
-- Muestra el punto 3 de Vista Consulta de casas

CREATE PROCEDURE MostrarCasasOrdenadasPorEstado
AS
BEGIN
    SELECT 
        DescripcionCasa, 
        PrecioCasa, 
        UsuarioAlquiler, 
        CASE 
            WHEN Estado = 'Disponible' THEN 'Disponible'
            ELSE 'Reservada'
        END AS Estado,
        FORMAT(FechaAlquiler, 'dd/MM/yyyy') AS Fecha
    FROM 
        CasasSistema
    ORDER BY 
        CASE 
            WHEN Estado = 'Disponible' THEN 0
            ELSE 1
        END
END
