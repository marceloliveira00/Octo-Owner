DECLARE @MyCounter INT;
SET @MyCounter = 1;

WHILE (@MyCounter <= 100)
BEGIN;

INSERT INTO Clients VALUES
	(
	'Cesar' + CAST(@MyCounter AS VARCHAR(16)),
	'Abrantes',
	'123.456.789-10',
	'Rua Whey Protein',
	'Comercial',
	'(00) 00000-0000',
	'Celular'
	)

SET @MyCounter = @MyCounter + 1;
END;