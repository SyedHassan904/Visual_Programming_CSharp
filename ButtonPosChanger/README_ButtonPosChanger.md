# 🎮 ButtonPosChanger - Fun WinForms Game

A light-hearted C# WinForms app where the main button playfully avoids your mouse cursor. A second button reveals a sweet surprise message.

---

## 🖼️ Overview

This project is a fun little Windows Forms application written in C#. It demonstrates basic UI event handling by making a button jump to a random location when the mouse cursor gets near it.

---

## 🎮 Features

- **Interactive UI** with randomly moving button on hover
- A second button that pops up a **sweet message**
- Uses `Random`, `MouseEnter`, and `Point` for button repositioning

---

## ✨ How It Works

- When the mouse enters `button1`, it instantly jumps to a new random location within the form.
- Clicking `button2` shows a heartfelt message using a `MessageBox`.

### 💡 Code Logic (Snippet)

```csharp
int w = this.ClientSize.Width;
int h = this.ClientSize.Height;
int npx = r.Next(0, w - button1.Width);
int npy = r.Next(0, h - button1.Height);
button1.Location = new Point(npx, npy);
```

---

## ▶️ How to Run

1. Open the project in **Visual Studio**
2. Build and run the application
3. Hover over the "dodge" button and try to click it 😄
4. Click the second button for a lovely message

---

## 🛠️ Requirements

- Visual Studio with .NET Windows Forms support
- Windows OS

---



## 📄 License

This is a fun, personal project for learning event-driven programming in C#.  
Feel free to use or expand it.

---

## 🙌 Author

Syed Hassan 
