namespace imgEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap i = new Bitmap(800, 1422);
            Bitmap i2 = new Bitmap(800, 1422);
            byte[,] img_bw = new byte[800, 1422];
            i = (Bitmap) pictureBox1.Image;
            i2 = (Bitmap) pictureBox2.Image;

            for (int x = 0; x < i.Width; x++) 
            {
                for (int y = 0; y < i.Height; y++) 
                {
                  Color color =  i.GetPixel(x, y);
                    img_bw[x,y] = (byte)((color.R+color.B+color.G)/3);
                    i2.SetPixel(x, y, Color.FromArgb(img_bw[x, y], img_bw[x, y], img_bw[x, y]));

                }
            }
            pictureBox2.Image = i2;
        }
    }
}
