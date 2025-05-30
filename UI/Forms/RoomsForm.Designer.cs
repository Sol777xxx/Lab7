namespace UI
{
    partial class RoomsForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView roomsGrid;
        private Button loadAllButton;
        private Button loadAvailableButton;
        private Button addButton;
        private Button deleteButton;
        private Button changeStatusButton;
        private Button priceButton;
        private ComboBox statusComboBox;
        private Label statusLabel;
        private ComboBox categoryComboBox;
        private Label categoryLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            roomsGrid = new DataGridView();
            loadAllButton = new Button();
            loadAvailableButton = new Button();
            addButton = new Button();
            deleteButton = new Button();
            changeStatusButton = new Button();
            priceButton = new Button();
            statusComboBox = new ComboBox();
            statusLabel = new Label();
            categoryLabel = new Label();
            categoryComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)roomsGrid).BeginInit();
            SuspendLayout();
            // 
            // roomsGrid
            // 
            roomsGrid.ColumnHeadersHeight = 46;
            roomsGrid.Location = new Point(41, 15);
            roomsGrid.Name = "roomsGrid";
            roomsGrid.RowHeadersWidth = 82;
            roomsGrid.Size = new Size(994, 770);
            roomsGrid.TabIndex = 0;
            // 
            // loadAllButton
            // 
            loadAllButton.Location = new Point(1082, 120);
            loadAllButton.Name = "loadAllButton";
            loadAllButton.Size = new Size(262, 65);
            loadAllButton.TabIndex = 1;
            loadAllButton.Text = "Усі кімнати";
            loadAllButton.Click += loadAllButton_Click;
            // 
            // loadAvailableButton
            // 
            loadAvailableButton.Location = new Point(1082, 203);
            loadAvailableButton.Name = "loadAvailableButton";
            loadAvailableButton.Size = new Size(262, 54);
            loadAvailableButton.TabIndex = 2;
            loadAvailableButton.Text = "Доступні кімнати";
            loadAvailableButton.Click += loadAvailableButton_Click;
            // 
            // addButton
            // 
            addButton.Location = new Point(1082, 263);
            addButton.Name = "addButton";
            addButton.Size = new Size(262, 61);
            addButton.TabIndex = 3;
            addButton.Text = "Додати кімнату";
            addButton.Click += addButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(1082, 330);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(258, 49);
            deleteButton.TabIndex = 4;
            deleteButton.Text = "Видалити кімнату";
            deleteButton.Click += deleteButton_Click;
            // 
            // changeStatusButton
            // 
            changeStatusButton.Location = new Point(1082, 385);
            changeStatusButton.Name = "changeStatusButton";
            changeStatusButton.Size = new Size(258, 69);
            changeStatusButton.TabIndex = 5;
            changeStatusButton.Text = "Змінити статус";
            changeStatusButton.Click += changeStatusButton_Click;
            // 
            // priceButton
            // 
            priceButton.Location = new Point(1082, 460);
            priceButton.Name = "priceButton";
            priceButton.Size = new Size(258, 65);
            priceButton.TabIndex = 6;
            priceButton.Text = "Ціна за кімнату";
            priceButton.Click += priceButton_Click;
            // 
            // statusComboBox
            // 
            statusComboBox.Items.AddRange(new object[] { "Available", "Booked", "Occupied" });
            statusComboBox.Location = new Point(1225, 609);
            statusComboBox.Name = "statusComboBox";
            statusComboBox.Size = new Size(155, 44);
            statusComboBox.TabIndex = 8;
            statusComboBox.SelectedIndexChanged += statusComboBox_SelectedIndexChanged;
            // 
            // statusLabel
            // 
            statusLabel.Location = new Point(1082, 609);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(152, 54);
            statusLabel.TabIndex = 10;
            statusLabel.Text = "Статус:";
            // 
            // categoryLabel
            // 
            categoryLabel.Location = new Point(1082, 544);
            categoryLabel.Name = "categoryLabel";
            categoryLabel.Size = new Size(137, 36);
            categoryLabel.TabIndex = 12;
            categoryLabel.Text = "Категорія:";
            // 
            // categoryComboBox
            // 
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.Items.AddRange(new object[] { "Cheap", "Standard", "Expensive" });
            categoryComboBox.Location = new Point(1225, 544);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(150, 44);
            categoryComboBox.TabIndex = 11;
            // 
            // RoomsForm
            // 
            AutoScaleDimensions = new SizeF(14F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1565, 1067);
            Controls.Add(roomsGrid);
            Controls.Add(loadAllButton);
            Controls.Add(loadAvailableButton);
            Controls.Add(addButton);
            Controls.Add(deleteButton);
            Controls.Add(changeStatusButton);
            Controls.Add(priceButton);
            Controls.Add(statusComboBox);
            Controls.Add(statusLabel);
            Controls.Add(categoryComboBox);
            Controls.Add(categoryLabel);
            Name = "RoomsForm";
            Text = "Кімнати";
            ((System.ComponentModel.ISupportInitialize)roomsGrid).EndInit();
            ResumeLayout(false);
        }
    }
}
