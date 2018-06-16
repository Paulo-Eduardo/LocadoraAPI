# Locadora"# LocadoraAPI" 

Base de dados local, depois de configurar a connectionString
Scrip para criação da base
```
Create table Locacao (
	Id int IDENTITY(1,1) PRIMARY KEY,
	Cpf varchar(15) not null,
	DataDeCriacao datetime,
)

Create table Locacao_Filme(
	Locacao_Id int FOREIGN KEY REFERENCES Locacao(Id),
	Filme_Id int FOREIGN KEY REFERENCES Locadora.dbo.Filme(Id),

)
```

Para rodar e testar essa aplicação você precisa ter o msbuild e o pacote npm em sua maquina,
Aconselho a usar o chocolatey
```choco install microsoft-build-tools```
```choco install nodejs.install```

Então instale as dependencias do projeto front
Navegue para app\FrontEnd\locadora e rode ```npm install```


Tenha certeza que o comando msbuild e npm esta na variavel de ambiente patch


Tendo isso configurado tudo que precisa ser feito é rodar o ```run.bat``` na raiz do projeto.