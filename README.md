## 功能介紹

- **登入功能**
  - 測試帳號與密碼 (使用 JWT 認證)
    - 帳號：`test`
    - 密碼：`password`
  
- **鄉鎮市公所名稱取得（透過新竹縣政府 Open Data）**
  - 使用快取機制來取得鄉鎮市公所的名稱。
  - 提供強制重設快取並重新取得名稱的功能。

---

## 資料夾結構說明

- **0.Template_NET_Core.Common**  
  共用模組及工具。

- **1.Template_NET_Core.Application**  
  應用層，負責處理業務邏輯和應用流程的調用。

- **2.Template_NET_Core.Services**  
  服務層，負責具體業務邏輯的實現。

- **3.Template_NET_Core.Repositories**  
  資料存取層，負責與資料庫或其他數據源的互動。

---

## 專案技術與工具

- **ASP.NET Core Web 應用程式**  
  - 基於 .NET 8.0 架構，支援現代 Web 應用的高效開發。

- **MemoryCache**  
  - 用於快取數據，提升讀取效能並減少重複查詢。

- **ActionResultFilter**  
  - 全局回應處理機制，統一管理 API 回應的格式與行為。

- **ExceptionFilter**  
  - 全局異常處理機制，捕捉並處理應用中的異常，並返回一致的錯誤格式。

- **Swagger**  
  - 自動生成 API 文件，方便測試與集成。

- **HttpClientFactory**  
  - 管理和重用 `HttpClient` 實例，提升 HTTP 請求效能及資源管理。

- **Scrutor**  
  - 依賴注入輔助工具，可掃描並註冊 DI 容器，減少手動工作量。

- **Decorate**  
  - 使用裝飾器模式來增強已註冊的服務功能或處理邏輯。

- **JWT (JSON Web Token)**  
  - 提供身份驗證服務，保護 API 並確保只有授權使用者能夠存取。

- **BCrypt.Net-Next**  
  - 用於密碼加解密。

- **Dapper**  
  - 用於輕量級資料庫操作。

- **AutoMapper**  
  - 自動進行物件映射，簡化 DTO（數據傳輸對象）與實體模型間的轉換。

- **FluentValidation**  
  - 使用口語化的語法進行模型驗證，提升代碼可讀性。