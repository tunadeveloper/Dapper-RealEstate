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

Bu proje, emlak ilanlarının yönetimi, istatistiksel analizi ve rol bazlı panel deneyimi için geliştirdiğim iki katmanlı bir platformdur:

- **RealEstate.WebAPILayer**:  
  - Dapper + SQL Server ile veri erişimi sağladım  
  - JWT üretimini gerçekleştirdim  
  - Rol bazlı authorization yapısını kurdum  
  - İstatistik endpointlerini geliştirdim  

- **RealEstate.WebUILayer**:  
  - ASP.NET Core MVC (Razor) arayüzünü geliştirdim  
  - Admin ve Employee alanlarını oluşturdum  
  - API tüketimini uyguladım  

Sistemde kimlik doğrulamayı JWT ile API tarafında gerçekleştirdim; UI tarafında ise bu token’ı cookie kimliğine dönüştürerek oturumun devamlılığını sağladım. Özellikle `AccountController` içinde claim normalizasyonunu (`role/RoleId/EmployeeId`) yaptım ve role göre yönlendirme (Admin -> `/Admin/Product`, Employee -> `/Employee/Home`) akışını yaptım.

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

<img width="1920" height="1408" alt="Image" src="https://github.com/user-attachments/assets/84a2ef10-654f-4b44-9d23-dda75828a23e" />
<img width="1920" height="2425" alt="Image" src="https://github.com/user-attachments/assets/3f079bae-3929-4f29-b1d7-e2da48ce0083" />
<img width="1920" height="1808" alt="Image" src="https://github.com/user-attachments/assets/282377af-aafd-4158-b997-070dc12398f3" />
<img width="1915" height="929" alt="Image" src="https://github.com/user-attachments/assets/d26a3a93-6dd8-4633-b40a-c3905f3c489b" />
<img width="1913" height="936" alt="Image" src="https://github.com/user-attachments/assets/3b46c53b-6af6-4098-b8f8-1a10c9a2eb33" />
<img width="1911" height="939" alt="Image" src="https://github.com/user-attachments/assets/bf2c85be-8db5-4e5e-b504-378b8a178a3b" />
<img width="1910" height="928" alt="Image" src="https://github.com/user-attachments/assets/d10d02b9-7f06-4fda-bd7c-d5b260bbe655" />
<img width="1914" height="936" alt="Image" src="https://github.com/user-attachments/assets/4ebda866-a1fe-46f1-8ad6-c170f73c4187" />
<img width="1909" height="924" alt="Image" src="https://github.com/user-attachments/assets/01f63c73-6d9c-4ab6-a1d7-a9b13188d27c" />
<img width="1909" height="932" alt="Image" src="https://github.com/user-attachments/assets/2285461c-de54-4f10-b8a2-40a63659b380" />
<img width="1914" height="934" alt="Image" src="https://github.com/user-attachments/assets/198f03b6-af31-4cd7-8e63-631ef34f0089" />
<img width="1915" height="934" alt="Image" src="https://github.com/user-attachments/assets/85945065-a4f9-405b-9b8f-6c46b83f69eb" />
<img width="1901" height="933" alt="Image" src="https://github.com/user-attachments/assets/5a5a9a21-2231-408b-846e-b700ba47e6d6" />
<img width="1900" height="933" alt="Image" src="https://github.com/user-attachments/assets/c6472a12-4789-4805-9bf8-8311d3280b6d" />
<img width="1897" height="921" alt="Image" src="https://github.com/user-attachments/assets/a93726c4-f44e-444c-bfc7-e69e9d407f0b" />
