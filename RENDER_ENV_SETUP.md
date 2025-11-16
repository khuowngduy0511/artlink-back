# Hướng dẫn Set Environment Variable trên Render.com

## ⚠️ QUAN TRỌNG: Vấn đề IPv6

**Supabase database chỉ có IPv6, nhưng Render.com không hỗ trợ IPv6.**

**Giải pháp:** Sử dụng **Supabase Connection Pooling** (cung cấp IPv4 endpoint).

## Bước 1: Lấy Connection Pooling URL từ Supabase

1. Đăng nhập vào [Supabase Dashboard](https://app.supabase.com)
2. Chọn project của bạn
3. Vào **Settings** → **Database**
4. Scroll xuống phần **Connection Pooling**
5. Copy **Connection String** từ **Transaction mode** hoặc **Session mode**
   - Format sẽ là: `postgresql://postgres:[PASSWORD]@[POOLER_HOST]:6543/postgres?pgbouncer=true`
   - Hoặc: `postgresql://postgres.nnkoptjcvwnhqrywidcj:[PASSWORD]@aws-0-[REGION].pooler.supabase.com:6543/postgres`

## Bước 2: Convert Connection String sang Npgsql Format

Connection Pooling URL của Supabase cần được convert sang format Npgsql:

**Ví dụ:**
```
postgresql://postgres.nnkoptjcvwnhqrywidcj:Matkhausieumanh123@aws-0-ap-southeast-1.pooler.supabase.com:6543/postgres?pgbouncer=true
```

**Convert thành:**
```
Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.nnkoptjcvwnhqrywidcj;Password=Matkhausieumanh123;SslMode=Require;Trust Server Certificate=true;
```

**Lưu ý:**
- Port thay đổi từ `5432` → `6543` (connection pooling port)
- Hostname thay đổi từ `db.nnkoptjcvwnhqrywidcj.supabase.co` → `aws-0-[REGION].pooler.supabase.com`
- Username có thể cần thêm project reference: `postgres.nnkoptjcvwnhqrywidcj`

## Bước 3: Set Environment Variable trên Render.com

1. Đăng nhập vào [Render.com](https://render.com)
2. Vào service backend của bạn (artlink-back)
3. Click vào tab **Environment** (bên trái menu)
4. Click nút **Add Environment Variable** hoặc **Edit** nếu đã có
5. Thêm biến môi trường:

   **Option 1 (Khuyến nghị):**
   - **Key**: `ConnectionStrings__MSSQLServerDB`
   - **Value**: (Connection string đã convert ở Bước 2)
     ```
     Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.nnkoptjcvwnhqrywidcj;Password=Matkhausieumanh123;SslMode=Require;Trust Server Certificate=true;
     ```

   **Option 2 (Nếu Option 1 không hoạt động):**
   - **Key**: `MSSQLServerDB`
   - **Value**: (Giống như Option 1)

   **⚠️ LƯU Ý QUAN TRỌNG:**
   - Không có dấu cách thừa ở đầu hoặc cuối connection string
   - Không có dấu ngoặc kép `"` bao quanh value
   - Copy chính xác connection string
   - **PHẢI sử dụng Connection Pooling URL** (port 6543), không phải direct connection (port 5432)

6. Click **Save Changes**

## Bước 4: Redeploy

Sau khi save, Render sẽ tự động redeploy service. Hoặc bạn có thể:
- Click **Manual Deploy** → **Deploy latest commit**

## Kiểm tra

Sau khi deploy, check logs để xem:
- `[CONFIG] Successfully resolved [hostname] to IPv4: [IP]` (thành công)
- `[CONFIG] Database connection configured. Host: [hostname]` (thành công)
- Hoặc `Connection string 'MSSQLServerDB' is required...` (chưa set đúng)

## Lưu ý

- Environment variable sẽ override tất cả config files
- Không commit connection string vào git (đã được ignore)
- Connection string sẽ được mã hóa trên Render.com
- **BẮT BUỘC phải sử dụng Connection Pooling** để có IPv4 endpoint

