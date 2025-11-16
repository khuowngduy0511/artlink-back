# Hướng dẫn Deploy PostgreSQL Database trên Render.com

## Bước 1: Tạo PostgreSQL Database trên Render.com

1. Đăng nhập vào [Render.com](https://render.com)
2. Click **New +** → **PostgreSQL**
3. Điền thông tin:
   - **Name**: `artlink-db` (hoặc tên bạn muốn)
   - **Database**: `artlinkdb` (hoặc tên bạn muốn)
   - **User**: `artlinkuser` (hoặc tên bạn muốn)
   - **Region**: Chọn region gần nhất (ví dụ: Singapore)
   - **PostgreSQL Version**: Chọn version mới nhất (15 hoặc 16)
   - **Plan**: Chọn **Free** (hoặc paid plan nếu cần)
4. Click **Create Database**

## Bước 2: Lấy Connection String

Sau khi database được tạo:

1. Vào database service vừa tạo
2. Vào tab **Info**
3. Tìm phần **Internal Database URL** hoặc **Connection String**
4. Copy connection string, format sẽ là:
   ```
   postgresql://artlinkuser:password@dpg-xxxxx-a.singapore-postgres.render.com:5432/artlinkdb
   ```

## Bước 3: Convert Connection String sang Npgsql Format

**Ví dụ connection string từ Render:**
```
postgresql://artlinkuser:password123@dpg-xxxxx-a.singapore-postgres.render.com:5432/artlinkdb
```

**Convert thành Npgsql format:**
```
Host=dpg-xxxxx-a.singapore-postgres.render.com;Port=5432;Database=artlinkdb;Username=artlinkuser;Password=password123;SslMode=Require;Trust Server Certificate=true;
```

**Cách convert:**
- `postgresql://` → bỏ đi
- `artlinkuser:password123@` → `Username=artlinkuser;Password=password123;`
- `dpg-xxxxx-a.singapore-postgres.render.com:5432` → `Host=dpg-xxxxx-a.singapore-postgres.render.com;Port=5432;`
- `/artlinkdb` → `Database=artlinkdb;`
- Thêm: `SslMode=Require;Trust Server Certificate=true;`

## Bước 4: Set Environment Variable trên Render.com Backend Service

1. Vào backend service của bạn (artlink-back)
2. Click vào tab **Environment**
3. Click **Add Environment Variable** hoặc **Edit** nếu đã có
4. Thêm biến môi trường:

   **Key**: `ConnectionStrings__MSSQLServerDB`
   
   **Value**: (Connection string đã convert ở Bước 3)
   ```
   Host=dpg-xxxxx-a.singapore-postgres.render.com;Port=5432;Database=artlinkdb;Username=artlinkuser;Password=password123;SslMode=Require;Trust Server Certificate=true;
   ```

   **⚠️ LƯU Ý:**
   - Thay `dpg-xxxxx-a.singapore-postgres.render.com` bằng hostname thực tế từ Render
   - Thay `artlinkdb`, `artlinkuser`, `password123` bằng thông tin thực tế
   - Không có dấu cách thừa ở đầu/cuối
   - Không có dấu ngoặc kép `"`

5. Click **Save Changes**

## Bước 5: Apply Migrations

Sau khi set environment variable, backend sẽ tự động apply migrations khi khởi động.

Hoặc bạn có thể apply migrations thủ công:

1. SSH vào backend service (nếu có)
2. Hoặc chạy migrations từ local machine với connection string của Render

## Bước 6: Kiểm tra

Sau khi deploy, check logs để xem:
- `[CONFIG] Database connection configured. Host: dpg-xxxxx-a.singapore-postgres.render.com`
- `[MIGRATION] Migrations applied successfully`
- Không có lỗi connection

## Lưu ý

- Render.com PostgreSQL database có IPv4, không có vấn đề IPv6
- Free plan có giới hạn: 90 ngày, 1GB storage, 256MB RAM
- Database sẽ tự động sleep sau 90 ngày không dùng (free plan)
- Connection string được mã hóa trên Render.com
- Không commit connection string vào git

## Troubleshooting

**Lỗi connection:**
- Kiểm tra connection string format
- Kiểm tra database đã được tạo và running
- Kiểm tra region của database và backend service phải giống nhau (hoặc gần nhau)

**Lỗi migration:**
- Kiểm tra database user có quyền tạo tables
- Kiểm tra connection string có đúng không

