using PL.Models;
using System.Net.Http;
using System.Net.Http.Json;
using UI.Models;

namespace UI
{
    public partial class RoomsForm : Form
    {
        private readonly HttpClient _http = new()
        {
            BaseAddress = new Uri("https://localhost:7269/") // заміни на свій актуальний порт
        };

        public RoomsForm()
        {
            InitializeComponent();
        }

        private async void loadAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<RoomPL>>("api/rooms");
                var mapped = Program.Mapper.Map<List<RoomUI>>(result);
                PrepareGridIfNeeded();
                roomsGrid.DataSource = mapped;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження всіх кімнат: {ex.Message}");
            }
        }

        private async void loadAvailableButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<RoomPL>>("api/rooms/available");
                var mapped = Program.Mapper.Map<List<RoomUI>>(result);
                PrepareGridIfNeeded();
                roomsGrid.DataSource = mapped;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження доступних кімнат: {ex.Message}");
            }
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Додати кімнату з категорією 'Standard'?", "Додати кімнату", MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes) return;

            var dto = new RoomPL
            {
                Category = CategoriesPL.Standard,
                Status = RoomStatusPL.Available,
                PricePerNight = 0 // автоматично підраховується в BLL
            };

            try
            {
                var response = await _http.PostAsJsonAsync("api/rooms", dto);
                if (response.IsSuccessStatusCode)
                    await loadAllButton_ClickAsync();
                else
                    MessageBox.Show("Не вдалося додати кімнату.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка додавання: {ex.Message}");
            }
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            if (roomsGrid.CurrentRow?.DataBoundItem is RoomUI selected)
            {
                var confirm = MessageBox.Show(
                    $"Видалити кімнату ID {selected.Id} ({selected.Category})?",
                    "Підтвердження",
                    MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        var response = await _http.DeleteAsync($"api/rooms/{selected.Id}");
                        if (response.IsSuccessStatusCode)
                            await loadAllButton_ClickAsync();
                        else
                            MessageBox.Show("Не вдалося видалити кімнату.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка видалення: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Оберіть кімнату з таблиці.");
            }
        }


        private async void changeStatusButton_Click(object sender, EventArgs e)
        {
            if (roomsGrid.CurrentRow?.DataBoundItem is not RoomUI selected)
            {
                MessageBox.Show("Оберіть кімнату з таблиці.");
                return;
            }

            if (statusComboBox.SelectedItem is null)
            {
                MessageBox.Show("Оберіть новий статус.");
                return;
            }

            var statusText = statusComboBox.SelectedItem.ToString();

            try
            {
                var response = await _http.PutAsync($"api/rooms/{selected.Id}/status?status={statusText}", null);
                if (response.IsSuccessStatusCode)
                {
                    await loadAllButton_ClickAsync();
                }
                else
                {
                    MessageBox.Show("Не вдалося змінити статус.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка зміни статусу: {ex.Message}");
            }
        }
        private async void priceButton_Click(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedItem is not string categoryStr)
            {
                MessageBox.Show("Оберіть категорію (Cheap, Standard, Expensive).");
                return;
            }

            try
            {
                var response = await _http.GetAsync($"api/rooms/price-by-category?category={categoryStr}");
                if (response.IsSuccessStatusCode)
                {
                    var price = await response.Content.ReadFromJsonAsync<decimal>();
                    MessageBox.Show($"Ціна для категорії {categoryStr}: {price} грн");
                }
                else
                {
                    MessageBox.Show("Не вдалося отримати ціну.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка отримання ціни: {ex.Message}");
            }
        }


        private async Task loadAllButton_ClickAsync()
        {
            loadAllButton_Click(this, EventArgs.Empty);
            await Task.CompletedTask;
        }

        private void PrepareGridIfNeeded()
        {
            if (roomsGrid.Columns.Count == 0)
            {
                roomsGrid.AutoGenerateColumns = false;
                roomsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                roomsGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Width = 60
                });
                roomsGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Status",
                    HeaderText = "Статус"
                });
                roomsGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Category",
                    HeaderText = "Категорія"
                });
                roomsGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PricePerNight",
                    HeaderText = "Ціна/ніч"
                });
            }
        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
