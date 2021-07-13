# DatingApp
tài khoản: maiqd/123

- dotnet new sln
- dotnet new webapi -o API
- dotnet sln add API

--thêm mới migrations
- add-migration InitialDB -o Data/Migrations

Update-Database

--Tạo project Angular
- ng new client
--Khởi động angular
- ng serve -o

--Thêm thư viện ngx-bootstrap
- ng add ngx-bootstrap 
--Cài icon
- npm install font-awesome

Drop-database

-- tạo component
- ng g c nav --skip-tests
-- tạo service
- ng g s account --skip-tests
- ng g s busy --skip-test
-- tạo directive
- ng g d has-role --skip-tests

-- cài đặt ngx-toastr
- npm install ngx-toastr

- ng g guard auth --skip-tests (CanActivate)
- ng g guard prevent-unsaved-changes --skip-tests (CanDeactivate)
- npm install bootswatch
- npm install @kolkov/ngx-gallery
- npm install @angular/cdk
- ng add ngx-spinner
- npm install ng2-file-upload
- npm install @microsoft/signalr

-- tạo module shared lưu trữ modules bên thứ 3
- ng g m shared --flat
-- tạo interceptor
- ng g interceptor error --skip-tests
- ng g interceptor jwt --skip-tests
- ng g interceptor loading --skip-tests
-- thêm es2019 vào file tsconfig.json

## Api
thêm thư viện 
AutoMapper.Extensions.Microsoft.DependencyInjection
CloudinaryDotNet

## angular 
- () nghĩa là truyền từ view -> angular
- [] nghĩa là nhận từ angular -> view
- [()] nghĩa là binding 2 hướng



## publish 
angular.json
    --tạo static file trong API
    "outputPath": "../API/wwwroot",
Startup.cs
    app.UseDefaultFiles();
	app.UseStaticFiles();
-ng build
-- build tối ưu
-ng build --prod


## Postgres
- docker run --name dev -e POSTGRES_USER=appuser -e POSTGRES_PASSWORD=a123 -p 5432:5432 -d postgres:latest

------------ Heroku
- heroku git:remote -a datingapp-maiqd
- heroku buildpacks:set jincod/dotnetcore
- heroku config:set ASPNETCORE_ENVIRONMENT=Production

- git push heroku main:master
