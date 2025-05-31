using PL.Models;
using System.Net.Http;
using System.Net.Http.Json;
using UI.Models;

namespace UI
{
    public partial class BookingsForm : Form
    {
        private readonly HttpClient _http = new()
        {
            BaseAddress = new Uri("https://localhost:7269/") // заміни на свій актуальний порт
        };

        public BookingsForm()
        {
            InitializeComponent();
        }

        private async void loadAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<BookingPL>>("api/bookings");
                var mapped = Program.Mapper.Map<List<BookingUI>>(result);
                bookingsGrid.DataSource = mapped;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження всіх бронювань: {ex.Message}");
            }
        }

        private async void loadActiveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<BookingPL>>("api/bookings/active");
                var mapped = Program.Mapper.Map<List<BookingUI>>(result);
                bookingsGrid.DataSource = mapped;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження активних бронювань: {ex.Message}");
            }
        }

        private async void bookButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(roomIdBox.Text, out int roomId) || !int.TryParse(clientIdBox.Text, out int clientId))
            {
                MessageBox.Show("Невірні ID кімнати або клієнта.");
                return;
            }

            var bookingUI = new BookingUI
            {
                RoomId = roomId,
                ClientId = clientId,
                StartDate = startDatePicker.Value.Date,
                EndDate = endDatePicker.Value.Date
            };

            var dto = Program.Mapper.Map<BookingPL>(bookingUI);


            try
            {
                var response = await _http.PostAsJsonAsync("api/bookings", dto);
                if (response.IsSuccessStatusCode)
                    await RefreshBookingsAsync();
                else
                    MessageBox.Show("Не вдалося створити бронювання.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при бронюванні: {ex.Message}");
            }
        }

        private async void cancelButton_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedBookingId(out int id)) return;

            try
            {
                var response = await _http.PutAsync($"api/bookings/{id}/cancel", null);
                if (response.IsSuccessStatusCode)
                    await RefreshBookingsAsync();
                else
                    MessageBox.Show("Не вдалося скасувати.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка скасування: {ex.Message}");
            }
        }

        private async void restoreButton_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedBookingId(out int id)) return;

            try
            {
                var response = await _http.PutAsync($"api/bookings/{id}/restore", null);
                if (response.IsSuccessStatusCode)
                    await RefreshBookingsAsync();
                else
                    MessageBox.Show("Не вдалося відновити.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка відновлення: {ex.Message}");
            }
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedBookingId(out int id)) return;

            try
            {
                var response = await _http.DeleteAsync($"api/bookings/{id}");
                if (response.IsSuccessStatusCode)
                    await RefreshBookingsAsync();
                else
                    MessageBox.Show("Не вдалося видалити.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка видалення: {ex.Message}");
            }
        }

        private async Task RefreshBookingsAsync()
        {
            await loadAllButton_ClickAsync();
        }

        private async Task loadAllButton_ClickAsync()
        {
            loadAllButton_Click(this, EventArgs.Empty);
            await Task.CompletedTask;
        }

        private bool TryGetSelectedBookingId(out int id)
        {
            id = -1;
            if (bookingsGrid.CurrentRow?.DataBoundItem is not BookingUI selected)
            {
                MessageBox.Show("Оберіть бронювання в таблиці.");
                return false;
            }
            id = selected.Id;
            return true;
        }
    }
}
