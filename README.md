# fura_fila

# to build
- build the solution

- open cmd and go to folder {base_path}\fura_fila\src\FuraFila.WebApp

- run the command `dotnet ef database update`


# Look later

- Implementing custom token providers for passwordless authentication in ASP.NET Core Identity

https://andrewlock.net/implementing-custom-token-providers-for-passwordless-authentication-in-asp-net-core-identity/

# heroku url

`https://rick-furafila.herokuapp.com/`

Still missing env variables... WIP


# publishing to heroku

`heroku login`

`docker build -t rick/furafila:heroku -f Dockerfile.heroku .`

`docker run -p 5000:5000 -d -e PORT=5000 rick/furafila:heroku`

`docker tag rick/furafila:heroku registry.heroku.com/rick-furafila/web`

`docker push registry.heroku.com/rick-furafila/web`

`heroku container:release web -a rick-furafila`

# Pagseguro references

`https://dev.pagseguro.uol.com.br/reference`

# Pagseguro Cartao credito de testes

Número
4111 1111 1111 1111

Bandeira
VISA

Valido até
12/2030

CVV
123

# Pagseguro comprador sandbox

xxxxxxx@sandbox.pagseguro.com.br.


# Xml serialization we are using extendedxmlserializer

https://github.com/wojtpl2/ExtendedXmlSerializer
