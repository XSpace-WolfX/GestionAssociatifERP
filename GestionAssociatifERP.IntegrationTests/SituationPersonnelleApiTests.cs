﻿using GestionAssociatifERP.Dtos.V1;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GestionAssociatifERP.IntegrationTests
{
    public class SituationPersonnelleApiTests
    {
        [Fact]
        public async Task GetAllSituationsPersonnelles_ShouldReturnEnfants_WhenDataExists()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            var url = "/api/v1/situationspersonnelles";
            var dto = new CreateSituationPersonnelleDto
            {
                Regime = "Test Regime",
                SituationFamiliale = "Description de la situation"
            };
            var postResponse = await client.PostAsJsonAsync(url, dto);
            postResponse.EnsureSuccessStatusCode();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var situationsPersonnelles = await response.Content.ReadFromJsonAsync<List<SituationPersonnelleDto>>();
            situationsPersonnelles.ShouldNotBeNull();
            situationsPersonnelles.ShouldContain(e => e.SituationFamiliale == "Description de la situation");
            situationsPersonnelles.ShouldContain(e => e.Regime == "Test Regime");
        }

        [Fact]
        public async Task GetAllSituationsPersonnelle_ShouldReturnOkAndEmptyList_WhenNoData()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/situationspersonnelles");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var situationsPersonnelles = await response.Content.ReadFromJsonAsync<List<SituationPersonnelleDto>>();
            situationsPersonnelles.ShouldNotBeNull();
            situationsPersonnelles.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetSituationPersonnelleById_ShouldReturnSituationPersonnelle_WhenExists()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            var createDto = new CreateSituationPersonnelleDto
            {
                Regime = "Test Regime",
                SituationFamiliale = "Description de la situation"
            };

            var postResponse = await client.PostAsJsonAsync("/api/v1/situationspersonnelles", createDto);
            postResponse.EnsureSuccessStatusCode();

            var createdSituation = await postResponse.Content.ReadFromJsonAsync<SituationPersonnelleDto>();

            // Act
            var response = await client.GetAsync($"/api/v1/situationspersonnelles/{createdSituation!.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var situation = await response.Content.ReadFromJsonAsync<SituationPersonnelleDto>();
            situation.ShouldNotBeNull();
            situation.Id.ShouldBe(createdSituation.Id);
            situation.Regime.ShouldBe("Test Regime");
            situation.SituationFamiliale.ShouldBe("Description de la situation");
        }

        [Fact]
        public async Task GetSituationPersonnelleById_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/situationspersonnelles/9999");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateSituationPersonnelle_ShouldReturnCreated_WhenValidData()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            var dto = new CreateSituationPersonnelleDto
            {
                Regime = "Test Regime",
                SituationFamiliale = "Description de la situation"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/situationspersonnelles", dto);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            var createdSituation = await response.Content.ReadFromJsonAsync<SituationPersonnelleDto>();
            createdSituation.ShouldNotBeNull();
            createdSituation.Regime.ShouldBe("Test Regime");
            createdSituation.SituationFamiliale.ShouldBe("Description de la situation");
        }

        [Fact]
        public async Task CreateSituationPersonnelle_ShouldReturnBadRequest_WhenDtoIsNull()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            HttpContent nullBody = JsonContent.Create<object?>(null);

            // Act
            var response = await client.PostAsync("/api/v1/situationspersonnelles", nullBody);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateSituationPersonnelle_ShouldReturnNoContent_WhenValid()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            var createDto = new CreateSituationPersonnelleDto
            {
                Regime = "Initial Regime",
                SituationFamiliale = "Initial Situation"
            };
            var postResponse = await client.PostAsJsonAsync("/api/v1/situationspersonnelles", createDto);
            postResponse.EnsureSuccessStatusCode();
            var createdSituation = await postResponse.Content.ReadFromJsonAsync<SituationPersonnelleDto>();

            var updateDto = new UpdateSituationPersonnelleDto
            {
                Id = createdSituation!.Id,
                Regime = "Updated Regime",
                SituationFamiliale = "Updated Situation"
            };

            // Act
            var response = await client.PutAsJsonAsync($"/api/v1/situationspersonnelles/{createdSituation.Id}", updateDto);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            var getResponse = await client.GetAsync($"/api/v1/situationspersonnelles/{createdSituation.Id}");
            var updatedSituation = await getResponse.Content.ReadFromJsonAsync<SituationPersonnelleDto>();
            updatedSituation.ShouldNotBeNull();
            updatedSituation.Id.ShouldBe(createdSituation.Id);
            updatedSituation.Regime.ShouldBe("Updated Regime");
            updatedSituation.SituationFamiliale.ShouldBe("Updated Situation");
        }

        [Fact]
        public async Task UpdateSituationPersonnelle_ShouldReturnBadRequest_WhenIdMismatch()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            var updateDto = new UpdateSituationPersonnelleDto
            {
                Id = 2,
                Regime = "Initial Regime",
                SituationFamiliale = "Initial Situation"
            };

            // Act
            var response = await client.PutAsJsonAsync("/api/v1/situationspersonnelles/1", updateDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateSituationPersonnelle_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            var updateDto = new UpdateSituationPersonnelleDto
            {
                Id = 9999, // ID qui n'existe pas
                Regime = "Updated Regime",
                SituationFamiliale = "Updated Situation"
            };

            // Act
            var response = await client.PutAsJsonAsync("/api/v1/situationspersonnelles/9999", updateDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteSituationPersonnelle_ShouldReturnNoContent_WhenExists()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            var createDto = new CreateSituationPersonnelleDto
            {
                Regime = "Test Regime",
                SituationFamiliale = "Description de la situation"
            };
            var postResponse = await client.PostAsJsonAsync("/api/v1/situationspersonnelles", createDto);
            postResponse.EnsureSuccessStatusCode();

            var createdSituation = await postResponse.Content.ReadFromJsonAsync<SituationPersonnelleDto>();

            // Act
            var response = await client.DeleteAsync($"/api/v1/situationspersonnelles/{createdSituation!.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            var getResponse = await client.GetAsync($"/api/v1/situationspersonnelles/{createdSituation.Id}");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteSituationPersonnelle_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            using var factory = new CustomWebApplicationFactory();
            var client = factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/v1/situationspersonnelles/9999");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
