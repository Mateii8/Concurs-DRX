# 🎧 DRX Helpdesk

Sistem web full-stack pentru gestionarea reclamațiilor IT interne ale unei companii. Angajații pot raporta probleme tehnice, urmări statusul acestora și primi notificări, iar administratorii pot gestiona reclamațiile, angajații, departamentele și asset-urile.

---

## 🛠️ Tehnologii

**Backend**
- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

**Frontend**
- React 18 (Vite)
- React Router v6
- Axios / Fetch API
- CSS Modules

---

## 📁 Structura Proiectului

```
ProiectDRX/                  # Backend - ASP.NET Core
├── Controllers/             # Endpoint-uri REST
│   ├── AuthController.cs
│   ├── ComplaintsController.cs
│   ├── AssetsController.cs
│   ├── EmployeesController.cs
│   ├── DepartmentsController.cs
│   ├── ComplaintsCommentsController.cs
│   ├── ComplaintsWorkFlowsController.cs
│   └── ReportsController.cs
├── Models/                  # Entități bază de date
├── DTOs/                    # Obiecte de transfer
├── Repositories/
│   ├── Interfaces/          # Contracte repository
│   └── Implementations/     # Repository Pattern + Unit of Work
├── Data/
│   └── AppDbContext.cs
└── Program.cs

proiectdrx-frontend/         # Frontend - React
├── src/
│   ├── api/                 # Configurare API
│   ├── components/          # Componente reutilizabile
│   ├── hooks/               # Custom hooks (useApi)
│   └── pages/               # Pagini (Dashboard, Complaints, Assets...)
└── vite.config.js
```

---

## 🔐 Autentificare

Aplicația folosește autentificare bazată pe email + parolă, cu roluri stocate în baza de date.

**Reguli parolă:**
- Minim 8 caractere
- Cel puțin o literă mare și una mică
- Cel puțin o cifră
- Cel puțin un caracter special (`!@#$%^&*`)

**Roluri disponibile:**

| Rol | Permisiuni |
|-----|-----------|
| `User` | Creare reclamații, vizualizare proprie, notificări |
| `Admin` | Toate + gestionare angajați, departamente, export CSV |

---

## 🚀 Funcționalități

- ✅ Înregistrare și autentificare cu roluri
- ✅ Dashboard personalizat per rol (User / Admin)
- ✅ Creare și gestionare reclamații
- ✅ Flux status: `NEW` → `IN_PROGRESS` → `RESOLVED`
- ✅ Comentarii pe reclamații
- ✅ Notificări la răspunsuri noi
- ✅ Gestionare asset-uri IT
- ✅ Gestionare angajați și departamente (Admin)
- ✅ Export raport CSV (Admin)
- ✅ Statistici volum reclamații (Admin)

---

## 📡 Endpoint-uri principale

| Metodă | Endpoint | Descriere |
|--------|----------|-----------|
| POST | `/api/Auth/register` | Înregistrare angajat |
| POST | `/api/Auth/login` | Autentificare |
| GET | `/api/Complaints` | Listă reclamații |
| POST | `/api/Complaints` | Creare reclamație |
| GET | `/api/Assets` | Listă asset-uri |
| GET | `/api/Employees` | Listă angajați |
| GET | `/api/Departments` | Listă departamente |
| GET | `/api/ComplaintComments/{id}` | Comentarii reclamație |
| GET | `/api/Reports` | Rapoarte (Admin) |

---

## 🗄️ Modele principale

```
Employee ──< Complaint ──< ComplaintComment
    │              └──< ComplaintWorkflow
    └── Department
    
Asset ──< Complaint
```

---

## 📋 Arhitectură

Proiectul folosește **Repository Pattern** cu **Unit of Work** pentru accesul la date:

```
Controller → IUnitOfWork → IRepository<T> → AppDbContext
```

Fiecare entitate are propriul repository cu operații specifice, plus un repository generic de bază.
