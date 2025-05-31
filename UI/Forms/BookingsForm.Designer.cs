namespace UI
{
    partial class BookingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView bookingsGrid;
        private Button loadAllButton;
        private Button loadActiveButton;
        private Button bookButton;
        private Button cancelButton;
        private Button restoreButton;
        private Button deleteButton;
        private TextBox roomIdBox;
        private TextBox clientIdBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private Label roomLabel;
        private Label clientLabel;
        private Label startLabel;
        private Label endLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            bookingsGrid = new DataGridView();
            loadAllButton = new Button();
            loadActiveButton = new Button();
            bookButton = new Button();
            cancelButton = new Button();
            restoreButton = new Button();
            deleteButton = new Button();
            roomIdBox = new TextBox();
            clientIdBox = new TextBox();
            startDatePicker = new DateTimePicker();
            endDatePicker = new DateTimePicker();
            roomLabel = new Label();
            clientLabel = new Label();
            startLabel = new Label();
            endLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)bookingsGrid).BeginInit();
            SuspendLayout();
            // 
            // bookingsGrid
            // 
            bookingsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bookingsGrid.ColumnHeadersHeight = 46;
            bookingsGrid.Location = new Point(20, 20);
            bookingsGrid.Name = "bookingsGrid";
            bookingsGrid.RowHeadersWidth = 82;
            bookingsGrid.Size = new Size(2127, 863);
            bookingsGrid.TabIndex = 0;
            // 
            // loadAllButton
            // 
            loadAllButton.Location = new Point(2176, 99);
            loadAllButton.Name = "loadAllButton";
            loadAllButton.Size = new Size(311, 61);
            loadAllButton.TabIndex = 1;
            loadAllButton.Text = "Усі бронювання";
            loadAllButton.Click += loadAllButton_Click;
            // 
            // loadActiveButton
            // 
            loadActiveButton.Location = new Point(2176, 186);
            loadActiveButton.Name = "loadActiveButton";
            loadActiveButton.Size = new Size(311, 64);
            loadActiveButton.TabIndex = 2;
            loadActiveButton.Text = "Активні бронювання";
            loadActiveButton.Click += loadActiveButton_Click;
            // 
            // bookButton
            // 
            bookButton.Location = new Point(2186, 521);
            bookButton.Name = "bookButton";
            bookButton.Size = new Size(220, 62);
            bookButton.TabIndex = 3;
            bookButton.Text = "Забронювати";
            bookButton.Click += bookButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(2186, 605);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(220, 60);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Скасувати";
            cancelButton.Click += cancelButton_Click;
            // 
            // restoreButton
            // 
            restoreButton.Location = new Point(2186, 693);
            restoreButton.Name = "restoreButton";
            restoreButton.Size = new Size(220, 59);
            restoreButton.TabIndex = 5;
            restoreButton.Text = "Відновити";
            restoreButton.Click += restoreButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(2186, 771);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(220, 58);
            deleteButton.TabIndex = 6;
            deleteButton.Text = "Видалити";
            deleteButton.Click += deleteButton_Click;
            // 
            // roomIdBox
            // 
            roomIdBox.Location = new Point(2342, 269);
            roomIdBox.Name = "roomIdBox";
            roomIdBox.Size = new Size(185, 42);
            roomIdBox.TabIndex = 7;
            // 
            // clientIdBox
            // 
            clientIdBox.Location = new Point(2342, 321);
            clientIdBox.Name = "clientIdBox";
            clientIdBox.Size = new Size(185, 42);
            clientIdBox.TabIndex = 8;
            // 
            // startDatePicker
            // 
            startDatePicker.Location = new Point(2306, 395);
            startDatePicker.Name = "startDatePicker";
            startDatePicker.Size = new Size(325, 42);
            startDatePicker.TabIndex = 9;
            // 
            // endDatePicker
            // 
            endDatePicker.Location = new Point(2306, 456);
            endDatePicker.Name = "endDatePicker";
            endDatePicker.Size = new Size(325, 42);
            endDatePicker.TabIndex = 10;
            // 
            // roomLabel
            // 
            roomLabel.Location = new Point(2186, 269);
            roomLabel.Name = "roomLabel";
            roomLabel.Size = new Size(149, 39);
            roomLabel.TabIndex = 11;
            roomLabel.Text = "ID кімнати:";
            // 
            // clientLabel
            // 
            clientLabel.Location = new Point(2186, 324);
            clientLabel.Name = "clientLabel";
            clientLabel.Size = new Size(174, 42);
            clientLabel.TabIndex = 12;
            clientLabel.Text = "ID клієнта:";
            // 
            // startLabel
            // 
            startLabel.Location = new Point(2186, 395);
            startLabel.Name = "startLabel";
            startLabel.Size = new Size(132, 42);
            startLabel.TabIndex = 13;
            startLabel.Text = "Початок:";
            // 
            // endLabel
            // 
            endLabel.Location = new Point(2186, 461);
            endLabel.Name = "endLabel";
            endLabel.Size = new Size(114, 42);
            endLabel.TabIndex = 14;
            endLabel.Text = "Кінець:";
            // 
            // BookingsForm
            // 
            AutoScaleDimensions = new SizeF(14F, 36F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2813, 917);
            Controls.Add(bookingsGrid);
            Controls.Add(loadAllButton);
            Controls.Add(loadActiveButton);
            Controls.Add(bookButton);
            Controls.Add(cancelButton);
            Controls.Add(restoreButton);
            Controls.Add(deleteButton);
            Controls.Add(roomIdBox);
            Controls.Add(clientIdBox);
            Controls.Add(startDatePicker);
            Controls.Add(endDatePicker);
            Controls.Add(roomLabel);
            Controls.Add(clientLabel);
            Controls.Add(startLabel);
            Controls.Add(endLabel);
            Name = "BookingsForm";
            Text = "Бронювання";
            ((System.ComponentModel.ISupportInitialize)bookingsGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
