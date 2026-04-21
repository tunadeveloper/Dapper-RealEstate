# 🏠 Dapper Real Estate: JWT ve Rol Bazlı Yönetim Platformu

![C#](https://img.shields.io/badge/C%23-%23239120.svg?style=flat-square&logo=csharp&logoColor=white)
![.NET 10](https://img.shields.io/badge/.NET%2010-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET%20Core%20MVC-5C2D91?style=flat-square&logo=dotnet&logoColor=white)
![ASP.NET Core Web API](https://img.shields.io/badge/ASP.NET%20Core%20Web%20API-68217A?style=flat-square&logo=dotnet&logoColor=white)
![Dapper](https://img.shields.io/badge/Dapper-005571?style=flat-square)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)
![SignalR](https://img.shields.io/badge/SignalR-0078D4?style=flat-square&logo=microsoft&logoColor=white)
![Scalar](https://img.shields.io/badge/Scalar-FF4F00?style=flat-square)
![TailwindCSS](https://img.shields.io/badge/TailwindCSS-%2338B2AC.svg?style=flat-square&logo=tailwind-css&logoColor=white)
![Chart.js](https://img.shields.io/badge/Chart.js-FF6384?style=flat-square&logo=chartdotjs&logoColor=white)

## 📖 Proje Hakkında

Bu proje, emlak ilanlarının yönetimi, istatistiksel analizi ve rol bazlı panel deneyimi için geliştirilmiş iki katmanlı bir platformdur:

- `RealEstate.WebAPILayer`: Dapper + SQL Server ile veri erişimi, JWT üretimi, rol bazlı authorization, istatistik endpointleri
- `RealEstate.WebUILayer`: ASP.NET Core MVC (Razor) arayüzü, Admin ve Employee alanları, API tüketimi

Sistemde kimlik doğrulama JWT ile API tarafında yapılır; UI tarafında ise bu token cookie kimliğine dönüştürülerek oturum sürdürülür. Özellikle `AccountController` içinde claim normalizasyonu (`role/RoleId/EmployeeId`) ve role göre yönlendirme (Admin -> `/Admin/Product`, Employee -> `/Employee/Home`) akışı kurgulanmıştır.

## 🚀 Kullanılan Teknolojiler

* **C# & .NET 10:** Hem API hem MVC katmanlarında modern ASP.NET Core altyapısı.
* **ASP.NET Core Web API + MVC (Razor):** Backend servisleri ve server-side render edilen panel ekranları.
* **Dapper & SQL Server:** Repository katmanında performans odaklı, doğrudan SQL sorguları.
* **JWT + Cookie Authentication:** API için Bearer token; UI için Cookie tabanlı oturum ve claim taşıma.
* **Role-Based Authorization:** `Admin` ve `Employee` rolleriyle endpoint/controller seviyesinde erişim kontrolü.
* **IHttpClientFactory:** UI -> API iletişiminde merkezi, yönetilebilir HTTP istemcisi.
* **SignalR:** Kategori sayısı gibi canlı verilerin hub üzerinden yayınlanması (`/signalrhub`).
* **Scalar OpenAPI:** Development ortamında API dokümantasyon ve test arayüzü.
* **TailwindCSS + Chart.js + X.PagedList:** Modern panel görünümü, grafikler ve sayfalama.

## ✨ Öne Çıkan Özellikler

* **İki Katmanlı Mimari:** API ve UI ayrımıyla daha sürdürülebilir yapı.
* **Rol Bazlı Panel Yönetimi:** Admin ve Employee için ayrık alanlar ve yetkiler.
* **JWT Claim Normalizasyonu:** UI tarafında farklı claim adlarının tek standarda çevrilmesi.
* **Admin Dashboard:** İlan/adet/fiyat metrikleri, çalışan-kategori bazlı özetler ve chart verileri.
* **Employee Dashboard:** Çalışana ait ve genel ilan istatistikleri.
* **Modül Bazlı CRUD:** Kategori, Ürün, Ürün Detay, Müşteri, Mesaj, İletişim, Hakkımızda, Hizmet, Abone, Çalışan.
* **Sahiplik Kontrolü (Ownership):** Employee kullanıcısının sadece kendi ilanları üzerinde işlem yapabilmesi.
* **Sayfalama Desteği:** Liste ekranlarında `X.PagedList` ile performanslı pagination.

## 🔐 Kimlik Doğrulama ve Yetkilendirme Akışı

1. UI `Account/Login` formu ile API `api/Auth/login` endpointine istek atar.
2. API JWT üretir (`RoleId`, `Role`, gerekirse `EmployeeId` claim'leri ile).
3. UI token'ı okuyup claim'leri normalize eder ve cookie kimliği oluşturur.
4. `ApiAuthorizationMessageHandler` her API çağrısına `Bearer` token ekler.
5. Controller/endpoint erişimleri `[Authorize(Roles = "...")]` ile korunur.

Bu yapı sayesinde hem API güvenliği hem de UI içinde rol bazlı yönlendirme birlikte çalışır.

## 📊 API ve İş Alanı Kapsamı

Projede öne çıkan endpoint grupları:

- `Auth`: Login, Register, Me
- `Products` ve `ProductDetails`: İlan yönetimi + Employee sahiplik kontrolleri
- `Statistics`: Ürün/fiyat/kategori/şehir/çalışan bazlı metrikler
- `Categories`, `Services`, `Employees`, `Clients`, `Messages`, `Contacts`, `Subscribers`, `Abouts`

Özellikle `StatisticsController`, dashboard ekranlarını besleyen yoğun metrik seti sunar (ürün sayısı, ortalama fiyat, en yüksek/düşük fiyat, çalışan bazlı ilan sayısı vb.).

## 🔬 Dapper, SQL ve Mimari Referanslar

Projede veri erişimi repository katmanında Dapper üzerinden ilerler. Kullanılan temel SQL yaklaşımı:

```text
1.  SELECT       : Listeleme ve tekil kayıt sorgulama
2.  INSERT       : Yeni kayıt ekleme
3.  UPDATE       : Mevcut kaydı güncelleme
4.  DELETE       : Kayıt silme
5.  WHERE        : Filtreleme (id, role, employee vb.)
6.  JOIN         : İlişkili tablo birleştirme (Product-Category-Employee)
7.  COUNT/AVG    : Dashboard istatistikleri
8.  TOP + ORDER  : En yüksek fiyat/popüler ilan listeleri

-- Örnek Proje Sorgu Senaryoları --
9.  Employee bazlı ürün listeleme
10. Popüler ilanların fiyat karşılaştırması
11. Kategoriye göre ilan yoğunluğu
12. Çalışan bazlı ilan sayısı
13. Ürün detayı ve sahiplik doğrulaması
```

Mimari olarak:

```text
14. RealEstate.WebAPILayer  : Dapper, JWT, role authorization, SignalR hub
15. RealEstate.WebUILayer   : Razor Views, Area bazlı paneller, HttpClient ile API tüketimi
16. Pattern                 : Repository + DTO + Role-based access + claim-temelli yönlendirme
```

## 🧭 Proje Yapısı

```text
RealEstate.WebAPILayer/
  Controllers/
  Repositories/
  DTOs/
  Context/
  Tools/
  Hubs/

RealEstate.WebUILayer/
  Controllers/
  Areas/
    Admin/
    Employee/
  Views/
  DTOs/
  Infrastructure/
```

## ⚙️ Kurulum ve Çalıştırma

### 1) Gereksinimler

- .NET SDK 10
- SQL Server (connection string'e uygun)

### 2) Connection String

`RealEstate.WebAPILayer/appsettings.json` içinde:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=DapperRealEstate;..."
}
```

### 3) Projeleri Çalıştır

API:

```bash
dotnet run --project RealEstate.WebAPILayer
```

UI:

```bash
dotnet run --project RealEstate.WebUILayer
```

Varsayılan geliştirme adresleri:

- API: `https://localhost:7175` (Scalar: `/scalar/v1`)
- UI: `https://localhost:7211`

## 🎯 Kısa Notlar

- UI katmanı API'ye `IHttpClientFactory` ile bağlanır.
- API endpointleri rol bazlı koruma kullanır (`Admin`, `Employee`).
- Employee tarafında ilan işlemleri claim içindeki `EmployeeId` ile sınırlandırılır.
- Dashboard ekranları birden fazla istatistik endpointini paralel çağırarak beslenir.

