namespace UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button openClientsButton;
        private Button openRoomsButton;
        private Button openBookingsButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.openClientsButton = new Button();
            this.openRoomsButton = new Button();
            this.openBookingsButton = new Button();

            SuspendLayout();

            // 
            // openClientsButton
            // 
            this.openClientsButton.Location = new Point(50, 50);
            this.openClientsButton.Name = "openClientsButton";
            this.openClientsButton.Size = new Size(300, 50);
            this.openClientsButton.Text = "Клієнти";
            this.openClientsButton.UseVisualStyleBackColor = true;
            this.openClientsButton.Click += new EventHandler(this.openClientsButton_Click);

            // 
            // openRoomsButton
            // 
            this.openRoomsButton.Location = new Point(50, 120);
            this.openRoomsButton.Name = "openRoomsButton";
            this.openRoomsButton.Size = new Size(300, 50);
            this.openRoomsButton.Text = "Кімнати";
            this.openRoomsButton.UseVisualStyleBackColor = true;
            this.openRoomsButton.Click += new EventHandler(this.openRoomsButton_Click);

            // 
            // openBookingsButton
            // 
            this.openBookingsButton.Location = new Point(50, 190);
            this.openBookingsButton.Name = "openBookingsButton";
            this.openBookingsButton.Size = new Size(300, 50);
            this.openBookingsButton.Text = "Бронювання";
            this.openBookingsButton.UseVisualStyleBackColor = true;
            this.openBookingsButton.Click += new EventHandler(this.openBookingsButton_Click);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(14F, 36F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 300);
            this.Controls.Add(this.openClientsButton);
            this.Controls.Add(this.openRoomsButton);
            this.Controls.Add(this.openBookingsButton);
            this.Name = "MainForm";
            this.Text = "Головне меню готелю";
            this.StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }

       

        #endregion
    }
}
