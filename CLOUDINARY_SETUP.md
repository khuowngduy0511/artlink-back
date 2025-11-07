# Hướng dẫn cấu hình Cloudinary cho ArtLink

## Cloudinary là gì?
Cloudinary là dịch vụ cloud để quản lý, tối ưu hóa và phân phối hình ảnh, video.

## Lỗi hiện tại
```
System.ArgumentNullException: Value cannot be null. (Parameter 'Cloudinary:CloudName is missing')
```

Lỗi này xảy ra vì thiếu cấu hình Cloudinary trong `appsettings.json`.

## Cách khắc phục

### Bước 1: Đăng ký tài khoản Cloudinary (FREE)

1. Truy cập: https://cloudinary.com/users/register/free
2. Đăng ký tài khoản miễn phí
3. Sau khi đăng ký, bạn sẽ nhận được:
   - **Cloud Name**
   - **API Key**
   - **API Secret**

### Bước 2: Lấy thông tin credentials

1. Đăng nhập vào Cloudinary Dashboard: https://cloudinary.com/console
2. Tại trang Dashboard, bạn sẽ thấy:
   ```
   Cloud name: your-cloud-name
   API Key: 123456789012345
   API Secret: abcdefghijklmnopqrstuvwxyz
   ```

### Bước 3: Cập nhật appsettings.Development.json

Mở file `src/WebApi/appsettings.Development.json` và thay thế giá trị:

```json
{
  "Cloudinary": {
    "CloudName": "your-actual-cloud-name",
    "ApiKey": "your-actual-api-key",
    "ApiSecret": "your-actual-api-secret"
  }
}
```

### Bước 4: Restart ứng dụng

Sau khi cập nhật, restart lại ứng dụng:
```bash
# Stop ứng dụng (Ctrl+C)
# Start lại
dotnet run --project src/WebApi
```

## Lưu ý quan trọng

⚠️ **KHÔNG commit Cloudinary credentials lên Git!**

- File `appsettings.Development.json` đã được add vào `.gitignore` để bảo vệ thông tin
- Chỉ commit file template `.example`
- Mỗi developer cần cấu hình Cloudinary riêng cho môi trường local

## Alternative: Sử dụng User Secrets (Khuyến nghị cho Development)

Thay vì lưu trong appsettings.json, bạn có thể dùng User Secrets:

```bash
cd src/WebApi

# Set Cloudinary configs
dotnet user-secrets set "Cloudinary:CloudName" "your-cloud-name"
dotnet user-secrets set "Cloudinary:ApiKey" "your-api-key"
dotnet user-secrets set "Cloudinary:ApiSecret" "your-api-secret"
```

## Kiểm tra cấu hình

Sau khi cấu hình, test API endpoint có sử dụng Cloudinary để đảm bảo hoạt động:
- Upload artwork image
- Upload asset files
- Upload proposal assets

## Cloudinary Free Plan Limits

- **Storage**: 25 GB
- **Bandwidth**: 25 GB/month
- **Transformations**: 25,000/month
- **API Requests**: Unlimited

Đủ để sử dụng cho development và demo!