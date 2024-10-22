using BlazorAya.Shared;
using System.Net.Http.Json;

public class PatientService
{
    private readonly HttpClient _httpClient;

    public PatientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Patients>> GetAllPatientsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Patients>>("api/patients");
    }

    public async Task<Patients> GetPatientByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Patients>($"api/patients/{id}");
    }

    public async Task<Patients> AddPatientAsync(Patients patient)
    {
        var response = await _httpClient.PostAsJsonAsync("api/patients", patient);
        return await response.Content.ReadFromJsonAsync<Patients>();
    }

    public async Task UpdatePatientAsync(int id, Patients patient)
    {
        await _httpClient.PutAsJsonAsync($"api/patients/{id}", patient);
    }

    public async Task DeletePatientAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/patients/{id}");
    }
}
