dotnet ef migrations add StripeDemo --project "App.Repository" --startup-project "App.Web.MVC"  --context AppDbContext

dotnet ef database update --project "App.Repository" --startup-project "App.Web.MVC"  --context AppDbContext


Erro: "The user specified as a definer ('root'@'%') does not exist" ?
Execute:
CREATE USER 'root'@'%' IDENTIFIED BY 'sua_senha';
GRANT ALL PRIVILEGES ON sua_base_de_dados.* TO 'root'@'%';
FLUSH PRIVILEGES;

