using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TaskManagementAPI.Models;
using TaskManagementAPI.ViewModels;
using Xunit.Abstractions;

namespace TaskManagementAPI.Tests
{
    public class TasksControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjQxNjYzNjYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMjIiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIn0.294ruR-N3BH_PaCupWA5pggrOq5emxN6aF1AbPrI8EU";

        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper _output;

        public TasksControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            _output = output;
        }

        [Fact]
        public async Task Registrar_Usuario_DeveRetornarOk()
        {
            //Arrange
            var registerViewModel = new RegisterModel
            {
                Email = "daniele@teste.com",
                Password = "12345.Ea"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Auth/register", registerViewModel);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().NotBeNull();

        }

        [Fact]
        public async Task Efetuar_Login_DeveRetornarOk_E_Token_QuandoUsuarioExistir()
        {
            //Arrange
            var loginModel = new LoginModel
            {
                Email = "daniele@teste.com",
                Password = "12345.Ea"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Auth/login", loginModel);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().NotBeNull();

            StreamContent streamContent = (StreamContent)response.Content;

            string responseBody = await streamContent.ReadAsStringAsync();

            _output.WriteLine($"Token: {responseBody}");
        }

        [Fact]
        public async Task ObterTarefas_DeveRetornarOk_QuandoTarefasExistem()
        {
            // Act
            var response = await _client.GetAsync("/api/tasks");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<TasksModel>>();
            tasks.Should().NotBeNull();
        }

        [Fact]
        public async Task ObterTarefa_DeveRetornarOk_QuandoTarefaExiste()
        {
            // Arrange
            var taskId = 1; // Use um Id válido

            // Act
            var response = await _client.GetAsync($"/api/tasks/{taskId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var task = await response.Content.ReadFromJsonAsync<TasksModel>();
            task.Should().NotBeNull();
            task.Id.Should().Be(taskId);
        }

        [Fact]
        public async Task CriarTarefa_DeveRetornarCriado_QuandoDadosValidosForemFornecidos()
        {
            // Arrange
            var newTask = new TasksModel
            {
                Title = "Teste Tarefa",
                Description = "Testando cadastro de tarefa.",
                DueDate = DateTime.Parse("2024-08-19"),
                IsCompleted = true
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/tasks", newTask);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdTask = await response.Content.ReadFromJsonAsync<TasksModel>();
            createdTask.Should().NotBeNull();
            createdTask.Title.Should().Be(newTask.Title);
        }

        [Fact]
        public async Task AlterarTarefa_DeveRetornarConcluida_QuandoDadosValidosForemFornecidos()
        {
            // Arrange
            var taskId = 1; // Use um Id válido
            var updatedTask = new TasksModel
            {
                Id = taskId,
                Title = "Atualizar Tarefa 2",
                Description = "Atualizando Tarefa 2",
                DueDate = DateTime.Parse("2024-08-20"),
                IsCompleted = false
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/tasks/{taskId}", updatedTask);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task ExcluirTarefa_NaoDeveRetornarNenhumConteudo_QuandoATarefaExiste()
        {
            // Arrange
            var taskId = 2; // Use um Id válido

            // Act
            var response = await _client.DeleteAsync($"/api/tasks/{taskId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}

