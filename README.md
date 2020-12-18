# DatingApp
dotnet new sln
donet new webapi -o API
dotnet sln add API

--thêm mới migrations
add-migration InitialDB -o Data/Migrations

Update-Database

Tạo project Angular
ng new client
Khởi động angular
ng serve -o
