# 📞 Phonebook Microservices

Bu proje, .NET Core tabanlı, mikroservis mimarisi ile geliştirilen bir telefon rehberi sistemidir. 
Servisler arası iletişim **HTTP** ve **RabbitMQ + MassTransit** ile sağlanır.
Her servis kendi veritabanına sahiptir ve **Docker Compose** ile konteyner ortamında çalıştırılır.

## 🚀 Özellikler

- 📁 Mikroservis Mimarisi
- 🐘 PostgreSQL veritabanı
- 🐇 RabbitMQ + MassTransit ile Event Driven mimari
- 🌐 RESTful API (Contact & Report)
- 📊 Asenkron rapor üretimi
- 🛡️ Clean Architecture & SOLID prensipleri
- 🧪 Unit Test Coverage
- 📦 Docker & Docker Compose desteği
- 📑 Swagger UI (OpenAPI)

---

## 📦 Proje Yapısı
📁 src
┣ 📁 Services
┃ ┣ 📁 ContactService
┃ ┃ ┣ 📁 API
┃ ┃ ┣ 📁 Application
┃ ┃ ┣ 📁 Domain
┃ ┃ ┣ 📁 Infrastructure
┃ ┃ ┗ 📁 Tests
┃ ┣ 📁 ReportService
┃ ┃ ┣ 📁 API
┃ ┃ ┣ 📁 Application
┃ ┃ ┣ 📁 Domain
┃ ┃ ┣ 📁 Infrastructure
┃ ┃ ┗ 📁 Tests
┣ 📁 Shared
┗ docker-compose.yml

## 🧰 Gereksinimler

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- [RabbitMQ Management UI](http://localhost:15672) (Kullanıcı: `guest`, Şifre: `guest`)
- [PostgreSQL Client](opsiyonel)

---

## 🚀 Projeyi Çalıştırma

### 1. 🔄 Depoyu Klonla

## bash
git clone https://github.com/MelihOmer/phonebook-microservices.git
cd phonebook-microservices

## 🐳 Docker Compose ile Servisleri Başlatma
docker-compose up --build
Bu komut ile postgresql, rabbitmq, contactservice,reportservice containerleri çalışır.
Bilgisayarınızda bir postgresql server veya rabbitmq varsa port çakışması yaşanmaması adına dış portlarını default değerden değiştirmeniz gerekmektedir.
Microservisler içinde 5001 ve 5002 portları kullanılmıyor olmalıdır.

🌐 Swagger UI’lara Erişim
	•	Contact Service: http://localhost:5001/swagger
	•	Report Service: http://localhost:5002/swagger

✨ Kullanım Senaryosu
	1.	POST /api/reports → Yeni bir rapor talebi oluşturur.
	2.	Bu talep RabbitMQ’ya publish edilir.
	3.	ReportService event’i consume eder, ContactService’ten HTTP ile lokasyon bazlı istatistik çeker.
	4.	Rapor hazırlanır ve veritabanına kaydedilir.
	5.	GET /api/reports → Hazır olan raporları listeler.
	6.	GET /api/v1/Reports/{reportId}/with-details → Detaylarıyla birlikte rapor döner.
  7.  GET /api/v1/Reports/with-details → Detaylarıyla birlikte raporları döner.

