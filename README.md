## 功能介紹

- **登入功能**
  - 測試帳號密碼 (使用 JWT 認證)
    - 帳號： `test`
    - 密碼： `password`
  
- **鄉鎮市公所名稱取得（By 新竹縣政府 Open Data）**
  - 透過快取機制取得鄉鎮市公所名稱。
  - 提供強制重設快取並重新取得名稱的功能。

---

## 資料夾結構說明

- **0.Template_NET_Core.Common**  
  共用模組及工具。
  
- **1.Template_NET_Core.Application**  
  應用層，負責處理業務邏輯的調用和應用邏輯。
  
- **2.Template_NET_Core.Services**  
  服務層，負責具體的業務邏輯實現。
  
- **3.Template_NET_Core.Repositories**  
  資料存取層，負責與資料庫或其他數據源的互動。

---

## 專案技術與工具

- **ASP.NET Core Web 應用程式**  
  - 使用 .NET 8.0 架構，支援現代 Web 應用的高效開發。

- **MemoryCache**  
  - 用於快取數據，提升讀取效能，減少重複查詢。

- **ActionResultFilter**  
  - 全局的回應處理機制，統一管理 API 回應的格式和行為。

- **ExceptionFilter**  
  - 全局的異常處理機制，捕捉並處理應用中的異常，返回一致的錯誤格式。

- **Swagger**  
  - 生成 API 文件，便於測試和集成。

- **HttpClientFactory**  
  - 用於管理和重用 `HttpClient` 實例，提升 HTTP 請求的效能和資源管理。

- **Scrutor**  
  - 一個依賴注入的輔助工具，提供 DI 容器的掃描與註冊功能，減少手動注入工作量。

- **Decorate**  
  - 用來為已註冊的服務添加裝飾器模式，增強功能或處理邏輯。

- **JWT (JSON Web Token)**  
  - 提供身份驗證服務，保護 API 並確保只有授權用戶可以訪問。

- **AutoMapper**  
  - 自動進行物件模型的映射，簡化數據傳輸對象（DTO）與實體模型之間的轉換。

- **FluentValidation**  
  - 使用口語化的驗證器來進行模型的驗證，提升代碼可讀性。
