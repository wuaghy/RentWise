# 🚗 RentWise – Clean Architecture .NET 8 Project

**RentWise** là một nền tảng thuê xe tự lái được xây dựng bằng **.NET 8**, áp dụng các nguyên tắc **Clean Architecture, CQRS, Dependency Injection, và Unit Testing (TDD)**.  
Dự án được thiết kế nhằm trình diễn quy trình phát triển phần mềm hiện đại: từ kiến trúc sạch, tách lớp rõ ràng, đến kiểm thử tự động và tích hợp CI trên GitHub Actions.

---

## 🧱 Kiến trúc tổng quan

Dự án được tổ chức theo **Clean Architecture** (còn gọi là Onion Architecture):


### 🔹 Các nguyên tắc chính:
- **Domain**: chứa các entity cốt lõi như `Vehicle`, `RentalQuote`, `Money`, `Percentage`.
- **Application**: xử lý logic nghiệp vụ thông qua các `Handlers` và `Services`, dùng MediatR cho CQRS.
- **Infrastructure**: cung cấp hiện thực cho các interface của Application (repository, service).
- **WebApi**: định nghĩa các endpoint RESTful để giao tiếp với client.
- **UnitTests**: kiểm thử logic nghiệp vụ bằng xUnit, bảo đảm tính đúng đắn của tính toán thuê xe.

---

## ⚙️ Công nghệ sử dụng

| Loại | Công nghệ |
|------|------------|
| Ngôn ngữ | C# (.NET 8) |
| Framework | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Pattern | Clean Architecture, CQRS, Dependency Injection |
| Testing | xUnit, FluentAssertions, Moq |
| CI/CD | GitHub Actions |
| Source Control | Git + GitHub |

---

## 🧠 Tính năng nổi bật

- Tính toán giá thuê xe (Rental Quote) theo:
  - Số ngày thuê, phụ phí cuối tuần, bảo hiểm, và chiết khấu.
- Xử lý request qua **CQRS Pattern** (Command & Query).
- Dễ dàng mở rộng thêm module mới như:
  - Quản lý xe, người dùng, hoá đơn, phản hồi trạm.
- Áp dụng **Dependency Injection (DI)** để tách biệt các tầng.
- Viết **Unit Test** với 100% coverage cho logic tính giá thuê.
- Pipeline **CI** tự động build & test khi push code lên GitHub.

---

## 🧪 Kiểm thử tự động (Unit Tests)

Thư mục: `tests/UnitTests`

Ví dụ kiểm thử handler:
```csharp
[Fact]
public async Task Handle_Should_Return_Correct_Total()
{
    var mockSvc = new Mock<IRentalQuoteService>();
    mockSvc.Setup(s => s.Calculate(It.IsAny<RentalQuoteReq>()))
           .Returns(new RentalQuoteRes { Total = new Money(250_000) });

    var handler = new CalculateRentalQuoteHandler(mockSvc.Object);
    var query = new CalculateRentalQuoteQuery(
        StartDate: new DateOnly(2025, 10, 14),
        Days: 2,
        Category: "Standard",
        IncludeInsurance: true,
        Discount: 0.1m
    );

    var result = await handler.Handle(query, default);
    result.Total.Should().Be(250_000m);
}
