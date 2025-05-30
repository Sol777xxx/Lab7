namespace UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openBookingsButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void openClientsButton_Click(object sender, EventArgs e)
        {
            Hide();
            using (var form = new ClientsForm())
            {
                form.ShowDialog();
            }
            Show();
        }
        private void openRoomsButton_Click(object sender, EventArgs e)
        {

            Hide();
            using (var form = new BookingsForm())
            {
                form.ShowDialog();
            }
            Show();
        }
    }
}
