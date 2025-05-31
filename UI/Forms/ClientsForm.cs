using PL.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using UI.Models;

namespace UI
{
    public partial class ClientsForm : Form
    {
        private readonly HttpClient _http = new()
        {
            BaseAddress = new Uri("https://localhost:7269/")
        };

        public ClientsForm()
        {
            InitializeComponent();
        }

        private async void loadButton_Click(object sender, EventArgs e)
        {
            await LoadAllClientsAsync();
        }

        private async Task LoadAllClientsAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ClientPL>>("api/clients");
                var mapped = Program.Mapper.Map<List<ClientUI>>(result);
                clientsGrid.DataSource = mapped;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження: {ex.Message}");
            }
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            var name = nameTextBox.Text;
            var surname = surnameTextBox.Text;

            try
            {
                var response = await _http.GetAsync($"api/clients/search?name={name}&surname={surname}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<ClientPL>>();
                    var mapped = Program.Mapper.Map<List<ClientUI>>(result);
                    clientsGrid.DataSource = mapped;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка пошуку: {ex.Message}");
            }
        }

        private async void activeButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ClientPL>>("api/clients/active-bookings");
                var mapped = Program.Mapper.Map<List<ClientUI>>(result);
                clientsGrid.DataSource = mapped;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка активних бронювань: {ex.Message}");
            }
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            var name = nameTextBox.Text;
            var surname = surnameTextBox.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            {
                MessageBox.Show("Ім’я та прізвище не можуть бути порожні.");
                return;
            }

            var clientUI = new ClientUI { Name = name, SurName = surname };
            var dto = Program.Mapper.Map<ClientPL>(clientUI);



            try
            {
                var response = await _http.PostAsJsonAsync("api/clients", dto);
                if (response.IsSuccessStatusCode)
                {
                    await LoadAllClientsAsync();
                    nameTextBox.Clear();
                    surnameTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Не вдалося додати клієнта.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка додавання: {ex.Message}");
            }
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            if (clientsGrid.CurrentRow?.DataBoundItem is ClientUI selected)
            {
                var confirm = MessageBox.Show($"Видалити клієнта {selected.Name} {selected.SurName}?", "Підтвердження", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        var response = await _http.DeleteAsync($"api/clients/{selected.Id}");
                        if (response.IsSuccessStatusCode)
                        {
                            await LoadAllClientsAsync();
                        }
                        else
                        {
                            MessageBox.Show("Не вдалося видалити клієнта.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка видалення: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Оберіть клієнта в таблиці.");
            }
        }

        private void clientsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
