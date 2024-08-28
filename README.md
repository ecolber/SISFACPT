La API fue creada con el enfoque CodeFirst por lo cual hay que realizar las migraciones
1- Crear la base de datos en SQL Server
2-Cambiar la cadena de conexion en el archivo appsettings.json

Realizar las migraciones con los siguientes comandos
3- Add-Migration Inicial -Project CLNFactPT.Datos -StartupProject CLNFactPT.API
4- Update-Database -Project CLNFactPT.Datos -StartupProject CLNFactPT.API

Consideraciones
- no existe una tabla catalogo moneda pero en el campo monedaId en la facturacion usar 1 para dolar y 2 para cordobas
- el precio en el producto es dolar
- el precio cordoba que se muestra en la busqueda de productos (Activos) es al tipo de cambio de la fecha en que se hace la consulta
- En el caso de la facturacion si la factura es cordoba la tasa de cambio que se toma es la de la fecha de la factura
