# Quick Start: Chuyển sang PostgreSQL

## 1. Tạo Supabase Database (5 phút)

1. Đăng ký tại: https://supabase.com
2. Tạo project mới
3. Lấy connection string từ **Settings → Database → Connection string (URI)**

## 2. Cấu hình trên Render.com

Vào **Environment** tab, thêm:

**Key**: `DATABASE_CONNECTION_STRING`  
**Value**: 
```
Host=db.[PROJECT-REF].supabase.co;Port=5432;Database=postgres;Username=postgres;Password=[YOUR-PASSWORD];SSL Mode=Require;Trust Server Certificate=true;
```

Thay `[PROJECT-REF]` và `[YOUR-PASSWORD]` bằng giá trị thực tế.

## 3. Deploy

```bash
git add .
git commit -m "feat: migrate to PostgreSQL"
git push
```

Render.com sẽ tự động deploy và apply migrations.

## 4. Kiểm tra

- Xem logs trên Render.com để đảm bảo migrations chạy thành công
- Kiểm tra tables trên Supabase dashboard

## Xem hướng dẫn chi tiết

Xem file `POSTGRESQL_DEPLOYMENT.md` để biết thêm chi tiết.

