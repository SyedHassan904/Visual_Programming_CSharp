# 🖼️ imgEditor - Grayscale Image Converter

A simple Windows Forms application in C# that converts a colored image to grayscale using pixel-by-pixel manipulation.

---

## 📌 Features

- Loads an image into the top `PictureBox` (`pictureBox1`)
- Converts the image to **grayscale** when the "Change" button is clicked
- Displays the grayscale result in the bottom `PictureBox` (`pictureBox2`)

---

## 🧠 How It Works

When the "Change" button is clicked, the app performs the following steps:

1. Gets the image from `pictureBox1`
2. Initializes a blank image (`i2`) with the same dimensions
3. Iterates over each pixel, calculates the grayscale value:
   ```
   gray = (R + G + B) / 3
   ```
4. Sets the corresponding pixel in the new image using:
   ```
   Color.FromArgb(gray, gray, gray)
   ```
5. Displays the new grayscale image in `pictureBox2`

### 🔍 Code Snippet

```csharp
Bitmap i = (Bitmap)pictureBox1.Image;
Bitmap i2 = new Bitmap(i.Width, i.Height);

for (int x = 0; x < i.Width; x++) {
    for (int y = 0; y < i.Height; y++) {
        Color color = i.GetPixel(x, y);
        byte gray = (byte)((color.R + color.G + color.B) / 3);
        i2.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
    }
}
pictureBox2.Image = i2;
```

## 🖼️ User Interface

- **Top Image:** Original colored input image
- **Button:** “Change” — triggers grayscale conversion
- **Bottom Image:** Grayscale output image
- 
---

## 🛠️ Requirements

- Visual Studio (any edition)
- Windows OS with .NET Framework or .NET Core (WinForms support)

---

## 🚀 Getting Started

1. Open the project in **Visual Studio**
2. Place an image in `pictureBox1` via the Designer or code
3. Run the project
4. Click the **Change** button to see the grayscale transformation

---

## 💡 Future Improvements

- Add file picker to load image at runtime
- Support image saving (e.g., save the grayscale output)
- Use `LockBits` and unsafe code for faster performance
- Add additional filters (sepia, invert, brightness)

---

## 🙌 Author

Syed Hassan 
