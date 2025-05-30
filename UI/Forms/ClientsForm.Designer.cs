namespace UI
{
    partial class ClientsForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView clientsGrid;
        private Button loadButton;
        private Button addButton;
        private Button deleteButton;
        private Button searchButton;
        private Button activeButton;
        private TextBox nameTextBox;
        private TextBox surnameTextBox;
        private Label nameLabel;
        private Label surnameLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            clientsGrid = new DataGridView();
            loadButton = new Button();
            addButton = new Button();
            deleteButton = new Button();
            searchButton = new Button();
            activeButton = new Button();
            nameTextBox = new TextBox();
            surnameTextBox = new TextBox();
            nameLabel = new Label();
            surnameLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)clientsGrid).BeginInit();
            SuspendLayout();
            // 
            // clientsGrid
            // 
            clientsGrid.ColumnHeadersHeight = 46;
            clientsGrid.Location = new Point(20, 20);
            clientsGrid.Name = "clientsGrid";
            clientsGrid.RowHeadersWidth = 82;
            clientsGrid.Size = new Size(1006, 784);
            clientsGrid.TabIndex = 0;
            clientsGrid.CellContentClick += clientsGrid_CellContentClick;
            clientsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 
            // loadButton
            // 
            loadButton.Location = new Point(1050, 256);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(290, 59);
            loadButton.TabIndex = 1;
            loadButton.Text = "Завантажити всіх";
            loadButton.Click += loadButton_Click;
            // 
            // addButton
            // 
            addButton.Location = new Point(1050, 321);
            addButton.Name = "addButton";
            addButton.Size = new Size(290, 58);
            addButton.TabIndex = 2;
            addButton.Text = "Додати";
            addButton.Click += addButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(1050, 396);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(290, 50);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "Видалити за ID";
            deleteButton.Click += deleteButton_Click;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(748, 822);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(147, 102);
            searchButton.TabIndex = 4;
            searchButton.Text = "Пошук";
            searchButton.Click += searchButton_Click;
            // 
            // activeButton
            // 
            activeButton.Location = new Point(1050, 462);
            activeButton.Name = "activeButton";
            activeButton.Size = new Size(290, 89);
            activeButton.TabIndex = 5;
            activeButton.Text = "З активними бронюваннями";
            activeButton.Click += activeButton_Click;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(146, 852);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(200, 42);
            nameTextBox.TabIndex = 6;
            // 
            // surnameTextBox
            // 
            surnameTextBox.Location = new Point(526, 852);
            surnameTextBox.Name = "surnameTextBox";
            surnameTextBox.Size = new Size(200, 42);
            surnameTextBox.TabIndex = 7;
            // 
            // nameLabel
            // 
            nameLabel.Location = new Point(41, 852);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(99, 48);
            nameLabel.TabIndex = 8;
            nameLabel.Text = "Ім’я:";
            // 
            // surnameLabel
            // 
            surnameLabel.Location = new Point(368, 852);
            surnameLabel.Name = "surnameLabel";
            surnameLabel.Size = new Size(152, 42);
            surnameLabel.TabIndex = 9;
            surnameLabel.Text = "Прізвище:";
            // 
            // ClientsForm
            // 
            AutoScaleDimensions = new SizeF(14F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1499, 999);
            Controls.Add(clientsGrid);
            Controls.Add(loadButton);
            Controls.Add(addButton);
            Controls.Add(deleteButton);
            Controls.Add(searchButton);
            Controls.Add(activeButton);
            Controls.Add(nameTextBox);
            Controls.Add(surnameTextBox);
            Controls.Add(nameLabel);
            Controls.Add(surnameLabel);
            Name = "ClientsForm";
            Text = "Клієнти";
            ((System.ComponentModel.ISupportInitialize)clientsGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
