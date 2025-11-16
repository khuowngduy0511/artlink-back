# Hướng dẫn Set Environment Variable trên Render.com

## Bước 1: Vào Render Dashboard

1. Đăng nhập vào [Render.com](https://render.com)
2. Vào service backend của bạn (artlink-back)

## Bước 2: Set Environment Variable

1. Click vào tab **Environment** (bên trái menu)
2. Click nút **Add Environment Variable**
3. Thêm biến môi trường:

   **Option 1 (Khuyến nghị):**
   - **Key**: `ConnectionStrings__MSSQLServerDB`
   - **Value**: (Copy toàn bộ dòng này, không có dấu cách thừa ở đầu/cuối)
     ```
     Host=db.nnkoptjcvwnhqrywidcj.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=Matkhausieumanh123;SslMode=Require;Trust Server Certificate=true;
     ```

   **Option 2 (Nếu Option 1 không hoạt động):**
   - **Key**: `MSSQLServerDB`
   - **Value**: (Copy toàn bộ dòng này, không có dấu cách thừa ở đầu/cuối)
     ```
     Host=db.nnkoptjcvwnhqrywidcj.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=Matkhausieumanh123;SslMode=Require;Trust Server Certificate=true;
     ```

   **⚠️ LƯU Ý QUAN TRỌNG:**
   - Không có dấu cách thừa ở đầu hoặc cuối connection string
   - Không có dấu ngoặc kép `"` bao quanh value
   - Copy chính xác connection string như trên
   - Sau khi save, check logs để xem connection string có được đọc đúng không

4. Click **Save Changes**

## Bước 3: Redeploy

Sau khi save, Render sẽ tự động redeploy service. Hoặc bạn có thể:
- Click **Manual Deploy** → **Deploy latest commit**

## Kiểm tra

Sau khi deploy, check logs để xem:
- `[CONFIG] Database: db.nnkoptjcvwnhqrywidcj.supabase.co` (thành công)
- Hoặc `Connection string 'MSSQLServerDB' is required...` (chưa set đúng)

## Lưu ý

- Environment variable sẽ override tất cả config files
- Không commit connection string vào git (đã được ignore)
- Connection string sẽ được mã hóa trên Render.com

