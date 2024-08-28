Realizar las migraciones con los siguientes comandos
1-
Add-Migration Inicial -Project CLNFactPT.Datos -StartupProject CLNFactPT.API
2-
Update-Database -Project CLNFactPT.Datos -StartupProject CLNFactPT.API

Consideraciones
- no existe una tabla catalogo moneda pero en el campo monedaId en la facturacion usar 1 para dolar y 2 para cordobas
- el precio en el producto es dolar
- el precio cordoba que se muestra en la busqueda de productos es al tipo de cambio de la fecha en que se hace la consulta
- En el caso de la facturacion si la factura es cordoba la tasa de cambio que se toma es la de la fecha de la factura
