# E-Commerce API

ASP.NET Core ile geliştirilmiş, katmanlı mimariye sahip e-ticaret backend projesi.

## 🚀 Özellikler
- Product, Category, Cart, Order yönetimi
- Repository Pattern ile veri erişimi
- Service katmanında iş kuralları
- DTO ile API kontratı
- Mapper ile SRP uyumu
- Global Exception Middleware
- Structured Logging
- Stok yönetimi (siparişte stok düşme)
- Sipariş raporları (günlük, aylık, en çok satanlar)

## 🛠️ Teknolojiler
- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Swagger

## 📁 Proje Yapısı
- Controllers → HTTP katmanı
- Services → İş kuralları
- Repositories → Veri erişimi
- Models/Entities → Veritabanı modelleri
- Models/Dtos → API kontratları
- Mappings → DTO ↔ Entity dönüşümü
- Middlewares → Exception handling
- Filters → Validation
