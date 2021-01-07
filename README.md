# DatingApp
tài khoản: maiqd/123

dotnet new sln
dotnet new webapi -o API
dotnet sln add API

--thêm mới migrations
add-migration InitialDB -o Data/Migrations

Update-Database

--Tạo project Angular
ng new client
--Khởi động angular
ng serve -o

--Thêm thư viện ngx-bootstrap
ng add ngx-bootstrap 
--Cài icon
npm install font-awesome

Drop-database

-- tạo component
ng g c nav --skip-tests
-- tạo service
ng g s account --skip-tests
trong angular 
- () nghĩa là truyền từ view -> angular
- [] nghĩa là nhận từ angular -> view
- [()] nghĩa là binding 2 hướng