# Se solicita realizar una Api Restfull que tenga las siguientes características:
(La base de datos a utilizar podrá optarse por SQLServer o MySQL según su preferencia, pero deberá utilizar los scripts de creación adjuntos y respetar dicha estructura).
- [x] 1- Deberá estar desarrollada en .NET Core
- [x] 2- Deberá tener Swagger integrado
- [ ] 3- También debe tener seguridad JWT (validando usuario y contraseña con ls datos cargados en la base de datos [test])
- [x] 4- Todo el desarrollo debe estar subido a GIT (puede ser un repositorio personal) en una rama que se llame "desarrollo".
- [x] 5-A- Deberá contar con otra rama desagregada de desarrollo cuyo nombre será "alta_cliente" que contenga un endpoint con seguridad que reciba una estructura con todos los datos de un cliente y se deberá validar los tipos de datos ingresados y responder si la estructura es correcta.
- [ ] B- En caso de que no, indicar el o los motivos del rechazo.
- [x] 6- Los datos del alta en el caso de ser correctos deben guardarse en la base de datos y el cliente guardado podrá ser consultado por DNI a través de la Api.
- [x] 7- Se espera tener las llamadas correspondientes para poder consultar el listado de clientes cargados en la base como también el listado de cada uno en forma individual por su clave primaria.
- [x] 8- Tener en cuenta que para validar el cuit debe utilizarse una función que lo haga a través de su dígito verificador. Link de referencia: https://campus.ort.edu.ar/articulo/56654/documento-explicativo
- [ ] 9- La rama "alta_cliente" una vez funcional, deberá mergearse a la rama "desarrollo".