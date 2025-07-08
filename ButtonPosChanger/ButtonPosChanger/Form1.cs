namespace ButtonPosChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

         

        }
        Random r = new Random();
        int w, h, npx, npy;


        private void button1_MouseEnter(object sender, EventArgs e)
        {

            w = this.ClientSize.Width;
            h = this.ClientSize.Height;
            npx = r.Next(0, w - button1.Width);
            npy = r.Next(0, h - button1.Height);
            button1.Location = new Point(npx, npy);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I Love you too!!!!❤️❤️❤️❤️🥰🥰");
        }
    }
}
