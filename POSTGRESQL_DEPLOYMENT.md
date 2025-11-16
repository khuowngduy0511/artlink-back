# Hướng dẫn Deploy PostgreSQL (Supabase) - FREE

## Bước 1: Tạo Database trên Supabase

1. **Đăng ký tài khoản Supabase**
   - Truy cập: https://supabase.com
   - Click "Start your project"
   - Đăng nhập bằng GitHub hoặc Email

2. **Tạo Project mới**
   - Click "New Project"
   - Điền thông tin:
     - **Name**: `artlink-db` (hoặc tên bạn muốn)
     - **Database Password**: Tạo password mạnh (lưu lại để dùng sau)
     - **Region**: Chọn gần nhất (Singapore hoặc Tokyo)
   - Click "Create new project"
   - Đợi 2-3 phút để project được tạo

3. **Lấy Connection String**
   - Vào **Settings** → **Database**
   - Scroll xuống phần **Connection string**
   - Chọn tab **URI**
   - Copy connection string, format:
     ```
     postgresql://postgres:[YOUR-PASSWORD]@db.[PROJECT-REF].supabase.co:5432/postgres
     ```

## Bước 2: Cấu hình Connection String

### Option 1: Sử dụng Environment Variable trên Render.com (Khuyến nghị)

1. Vào Render.com dashboard
2. Chọn service backend của bạn
3. Vào **Environment** tab
4. Thêm environment variable:
   - **Key**: `DATABASE_CONNECTION_STRING`
   - **Value**: Connection string từ Supabase (format PostgreSQL):
     ```
     Host=db.[PROJECT-REF].supabase.co;Port=5432;Database=postgres;Username=postgres;Password=[YOUR-PASSWORD];SSL Mode=Require;Trust Server Certificate=true;
     ```
5. Click **Save Changes**

### Option 2: Update appsettings.Production.json

Mở file `src/WebApi/appsettings.Production.json` và thay connection string:

```json
{
  "ConnectionStrings": {
    "MSSQLServerDB": "Host=db.[YOUR-PROJECT-REF].supabase.co;Port=5432;Database=postgres;Username=postgres;Password=[YOUR-PASSWORD];SSL Mode=Require;Trust Server Certificate=true;"
  }
}
```

**Lưu ý**: Thay `[YOUR-PROJECT-REF]` và `[YOUR-PASSWORD]` bằng giá trị thực tế.

## Bước 3: Tạo Migrations cho PostgreSQL

Sau khi có connection string, chạy lệnh sau để tạo migrations:

```bash
cd src/Migrators.PostgreSQL
dotnet ef migrations add InitialCreate --context AppDBContext --startup-project ../WebApi
```

Nếu cần tạo thêm migrations sau này:

```bash
dotnet ef migrations add [MigrationName] --context AppDBContext --startup-project ../WebApi
```

## Bước 4: Apply Migrations

Migrations sẽ tự động được apply khi app khởi động (nhờ `UseApplyMigrations()` trong `Program.cs`).

Hoặc apply thủ công:

```bash
cd src/Migrators.PostgreSQL
dotnet ef database update --context AppDBContext --startup-project ../WebApi
```

## Bước 5: Deploy lên Render.com

1. **Commit và push code lên GitHub**
   ```bash
   git add .
   git commit -m "feat: migrate from SQL Server to PostgreSQL"
   git push
   ```

2. **Render.com sẽ tự động deploy**
   - Render.com sẽ detect changes và rebuild
   - App sẽ tự động apply migrations khi start

3. **Kiểm tra logs**
   - Vào Render.com dashboard
   - Xem logs để đảm bảo migrations chạy thành công
   - Tìm dòng: `[MIGRATION] Migrations applied successfully.`

## Bước 6: Kiểm tra Database trên Supabase

1. Vào Supabase dashboard
2. Click **Table Editor** ở sidebar
3. Bạn sẽ thấy các tables đã được tạo:
   - Account
   - Artwork
   - Comment
   - Category
   - Tag
   - ... và các tables khác

## Troubleshooting

### Lỗi: "SSL connection required"
- Đảm bảo connection string có: `SSL Mode=Require;Trust Server Certificate=true;`

### Lỗi: "password authentication failed"
- Kiểm tra lại password trong connection string
- Password có thể có ký tự đặc biệt, cần URL encode

### Lỗi: "database does not exist"
- Supabase sử dụng database `postgres` mặc định
- Không cần tạo database mới

### Lỗi: "relation does not exist"
- Migrations chưa được apply
- Kiểm tra logs trên Render.com
- Có thể cần apply migrations thủ công

## Supabase Free Tier Limits

- **Database Size**: 500 MB
- **Bandwidth**: 2 GB/month
- **API Requests**: 50,000/month
- **Storage**: 1 GB
- **File Uploads**: 50 MB max per file

## Lưu ý quan trọng

1. **Backup dữ liệu**: Nếu có dữ liệu trên SQL Server cũ, cần export và import vào PostgreSQL
2. **Test kỹ**: Test tất cả features sau khi migrate
3. **Monitor**: Theo dõi usage trên Supabase dashboard để tránh vượt free tier

## Hỗ trợ

Nếu gặp vấn đề:
- Supabase Docs: https://supabase.com/docs
- Supabase Discord: https://discord.supabase.com
- Render.com Docs: https://render.com/docs

