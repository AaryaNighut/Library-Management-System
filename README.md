# 📚 Library Management System

A web-based Library Management System built using ASP.NET Core MVC and MySQL.  
The system helps manage books, student records, and borrowing activities efficiently through a simple and user-friendly interface.

---

## 📌 Project Overview

Managing library records manually is time-consuming and prone to errors.  
This project provides a digital solution to manage books, users, and borrowing records efficiently.

The system allows administrators to:

- Manage books in the library
- Track issued and returned books
- Manage student records
- Monitor library activities through a dashboard

---

## 💡 Key Features

- User Authentication (Login & Registration)
- Add, Edit, and Delete Books
- Issue Books to Students
- Return Books Management
- Dashboard showing library statistics
- Organized MVC architecture for better maintainability

---

## 🛠 Tech Stack

### Frontend
- HTML
- CSS
- Bootstrap
- Razor Views

### Backend
- C#
- ASP.NET Core MVC

### Database
- MySQL

### Tools & Environment
- Visual Studio 2022
- Git
- GitHub

---

## 🧠 System Workflow

1. User logs into the system
2. Admin manages books and student records
3. Books can be issued to students
4. Issued books are tracked in the system
5. Returned books update the database
6. Dashboard displays library statistics

---

## 📂 Project Structure

```
Library_Management
│
├── Controllers
│   ├── Authentication.cs
│   ├── Books.cs
│   └── HomeController.cs
│
├── Models
│   ├── Login.cs
│   ├── Register.cs
│   └── ErrorViewModel.cs
│
├── Views
│   ├── Authentication
│   ├── Books
│   ├── Home
│   └── Shared
│
├── wwwroot
│
├── appsettings.json
└── Program.cs
```





---

## ⚙️ Installation & Setup

Clone the repository:
git clone https://github.com/mohiteekanksh/Library_Management.git

Navigate to the project folder:
cd Library_Management

Open the project in Visual Studio 2022.

Update the database connection string in:
appsettings.json

Run the project:
dotnet run
---





## 📊 Future Improvements

- Role-based access control (Admin / Student)
- Online book reservation system
- Email notifications for due dates
- Advanced search and filtering
- Reports and analytics dashboard

---

## 👨‍💻 Author

Ekanksh  
Computer Science Engineering (AI & ML)

GitHub:  
https://github.com/mohiteekanksh

---

## 📜 License

Copyright © 2026 Ekanksh

This project is created for educational and portfolio purposes.  
All Rights Reserved.
