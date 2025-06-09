using AutoMapper;
using GestionAssociatifERP.Dtos.V1;
using GestionAssociatifERP.Models;
using GestionAssociatifERP.Repositories;
using GestionAssociatifERP.Services;
using Moq;
using Shouldly;

namespace GestionAssociatifERP.UnitTests.Services
{
    public class EnfantServiceTests
    {
        private readonly IEnfantService _enfantService;
        private readonly Mock<IEnfantRepository> _enfantRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public EnfantServiceTests()
        {
            _enfantRepositoryMock = new Mock<IEnfantRepository>();
            _mapperMock = new Mock<IMapper>();
            _enfantService = new EnfantService(_enfantRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllEnfantsAsync_WhenEnfantsExist_ShouldReturnMappedDtoList()
        {
            // Arrange
            var enfants = new List<Enfant>
            {
                new() { Id = 1, Nom = "Alice" },
                new() { Id = 2, Nom = "Bob" }
            };

            var enfantsDto = new List<EnfantDto>
            {
                new() { Id = 1, Nom = "Alice" },
                new() { Id = 2, Nom = "Bob" }
            };

            _enfantRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(enfants);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<EnfantDto>>(enfants))
                .Returns(enfantsDto);

            // Act
            var result = await _enfantService.GetAllEnfantsAsync();

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count().ShouldBe(2);
            result.Data.ShouldContain(e => e.Nom == "Alice");
        }

        [Fact]
        public async Task GetAllEnfantsAsync_WhenNoEnfants_ShouldReturnEmptyDtoList()
        {
            // Arrange
            var enfants = new List<Enfant>();
            var enfantsDto = new List<EnfantDto>();

            _enfantRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(enfants);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<EnfantDto>>(enfants))
                .Returns(enfantsDto);

            // Act
            var result = await _enfantService.GetAllEnfantsAsync();

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetEnfantAsync_WhenEnfantExists_ShouldReturnMappedDto()
        {
            // Arrange
            var enfant = new Enfant { Id = 1, Nom = "Alice" };
            var enfantDto = new EnfantDto { Id = 1, Nom = "Alice" };

            _enfantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map<EnfantDto>(enfant))
                .Returns(enfantDto);

            // Act
            var result = await _enfantService.GetEnfantAsync(1);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Nom.ShouldBe("Alice");
        }

        [Fact]
        public async Task GetEnfantAsync_WhenEnfantDoesNotExist_ShouldReturnFail()
        {
            // Arrange
            _enfantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(null as Enfant);

            // Act
            var result = await _enfantService.GetEnfantAsync(1);

            // Assert
            result.Success.ShouldBeFalse();
            result.Data.ShouldBeNull();
            result.Message.ShouldBe("Aucun enfant correspondant n'a été trouvé");
        }

        [Fact]
        public async Task GetEnfantWithResponsablesAsync_WhenEnfantWithResponsablesExists_ShouldReturnMappedDto()
        {
            // Arrange
            var enfant = new Enfant
            {
                Id = 1,
                Nom = "Alice",
                ResponsableEnfants = new List<ResponsableEnfant>
                {
                    new() { Responsable = new Responsable { Id = 1, Nom = "Bob" } },
                    new() { Responsable = new Responsable { Id = 2, Nom = "Charlie" } }
                }
            };

            var enfantWithResponsablesDto = new EnfantWithResponsablesDto
            {
                Id = 1,
                Nom = "Alice",
                Responsables = new List<ResponsableDto>
                {
                    new() { Id = 1, Nom = "Bob" },
                    new() { Id = 2, Nom = "Charlie" }
                }
            };

            _enfantRepositoryMock
                .Setup(repo => repo.GetWithResponsablesAsync(1))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map<EnfantWithResponsablesDto>(enfant))
                .Returns(enfantWithResponsablesDto);

            // Act
            var result = await _enfantService.GetEnfantWithResponsablesAsync(1);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Nom.ShouldBe("Alice");
            result.Data.Responsables.ShouldNotBeNull();
            result.Data.Responsables.ShouldNotBeEmpty();
            result.Data.Responsables.Count.ShouldBe(2);
            result.Data.Responsables.ShouldContain(r => r.Nom == "Bob");
        }

        [Fact]
        public async Task GetEnfantWithResponsablesAsync_WhenResponsablesListIsEmpty_ShouldReturnMappedDto()
        {
            // Arrange
            var enfant = new Enfant
            {
                Id = 1,
                Nom = "Alice",
                ResponsableEnfants = new List<ResponsableEnfant>()
            };

            var enfantWithResponsablesDto = new EnfantWithResponsablesDto
            {
                Id = 1,
                Nom = "Alice",
                Responsables = new List<ResponsableDto>()
            };

            _enfantRepositoryMock
                .Setup(repo => repo.GetWithResponsablesAsync(1))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map<EnfantWithResponsablesDto>(enfant))
                .Returns(enfantWithResponsablesDto);

            // Act
            var result = await _enfantService.GetEnfantWithResponsablesAsync(1);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.Data.Nom.ShouldBe("Alice");
            result.Data.Responsables.ShouldNotBeNull();
            result.Data.Responsables.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetEnfantWithResponsablesAsync_WhenEnfantDoesNotExist_ShouldReturnFail()
        {
            // Arrange
            _enfantRepositoryMock
                .Setup(repo => repo.GetWithResponsablesAsync(1))
                .ReturnsAsync(null as Enfant);

            // Act
            var result = await _enfantService.GetEnfantWithResponsablesAsync(1);

            // Assert
            result.Success.ShouldBeFalse();
            result.Data.ShouldBeNull();
            result.Message.ShouldBe("Aucun enfant correspondant n'a été trouvé");
        }

        [Fact]
        public async Task GetEnfantWithPersonnesAutoriseesAsync_WhenEnfantWithPersonnesAutoriseesExists_ShouldReturnMappedDto()
        {
            // Arrange
            var enfant = new Enfant
            {
                Id = 1,
                Nom = "Alice",
                PersonneAutoriseeEnfants = new List<PersonneAutoriseeEnfant>
                {
                    new() { PersonneAutorisee = new PersonneAutorisee { Id = 1, Nom = "Bob" } },
                    new() { PersonneAutorisee = new PersonneAutorisee { Id = 2, Nom = "Charlie" } }
                }
            };

            var enfantWithPersonnesAutoriseesDto = new EnfantWithPersonnesAutoriseesDto
            {
                Id = 1,
                Nom = "Alice",
                PersonnesAutorisees = new List<PersonneAutoriseeDto>
                {
                    new() { Id = 1, Nom = "Bob" },
                    new() { Id = 2, Nom = "Charlie" }
                }
            };
            _enfantRepositoryMock
                .Setup(repo => repo.GetWithPersonnesAutoriseesAsync(1))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map<EnfantWithPersonnesAutoriseesDto>(enfant))
                .Returns(enfantWithPersonnesAutoriseesDto);

            // Act
            var result = await _enfantService.GetEnfantWithPersonnesAutoriseesAsync(1);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Nom.ShouldBe("Alice");
            result.Data.PersonnesAutorisees.ShouldNotBeNull();
            result.Data.PersonnesAutorisees.ShouldNotBeEmpty();
            result.Data.PersonnesAutorisees.Count.ShouldBe(2);
            result.Data.PersonnesAutorisees.ShouldContain(r => r.Nom == "Bob");
        }

        [Fact]
        public async Task GetEnfantWithPersonnesAutoriseesAsync_WhenPersonnesAutoriseesListIsEmpty_ShouldReturnMappedDto()
        {
            // Arrange
            var enfant = new Enfant
            {
                Id = 1,
                Nom = "Alice",
                PersonneAutoriseeEnfants = new List<PersonneAutoriseeEnfant>()
            };

            var enfantWithPersonnesAutoriseesDto = new EnfantWithPersonnesAutoriseesDto
            {
                Id = 1,
                Nom = "Alice",
                PersonnesAutorisees = new List<PersonneAutoriseeDto>()
            };

            _enfantRepositoryMock
                .Setup(repo => repo.GetWithPersonnesAutoriseesAsync(1))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map<EnfantWithPersonnesAutoriseesDto>(enfant))
                .Returns(enfantWithPersonnesAutoriseesDto);

            // Act
            var result = await _enfantService.GetEnfantWithPersonnesAutoriseesAsync(1);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.Data.Nom.ShouldBe("Alice");
            result.Data.PersonnesAutorisees.ShouldNotBeNull();
            result.Data.PersonnesAutorisees.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetEnfantWithPersonnesAutoriseesAsync_WhenEnfantDoesNotExist_ShouldReturnFail()
        {
            // Arrange
            _enfantRepositoryMock
                .Setup(repo => repo.GetWithPersonnesAutoriseesAsync(1))
                .ReturnsAsync(null as Enfant);

            // Act
            var result = await _enfantService.GetEnfantWithPersonnesAutoriseesAsync(1);

            // Assert
            result.Success.ShouldBeFalse();
            result.Data.ShouldBeNull();
            result.Message.ShouldBe("Aucun enfant correspondant n'a été trouvé");
        }

        [Fact]
        public async Task GetEnfantWithDonneesSupplementairesAsync_WhenEnfantWithDonneesSupplementairesExists_ShouldReturnMappedDto()
        {
            // Arrange
            var enfant = new Enfant
            {
                Id = 1,
                Nom = "Alice",
                DonneeSupplementaires = new List<DonneeSupplementaire>
                {
                    new() { Id = 1, Valeur = "A" },
                    new() { Id = 2, Valeur = "B" }
                }
            };

            var enfantWithDonneesSupplementairesDto = new EnfantWithDonneesSupplementairesDto
            {
                Id = 1,
                Nom = "Alice",
                DonneeSupplementaires = new List<DonneeSupplementaireDto>
                {
                    new() { Id = 1, Valeur = "A" },
                    new() { Id = 2, Valeur = "B" }
                }
            };

            _enfantRepositoryMock
                .Setup(repo => repo.GetWithDonneesSupplementairesAsync(1))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map<EnfantWithDonneesSupplementairesDto>(enfant))
                .Returns(enfantWithDonneesSupplementairesDto);

            // Act
            var result = await _enfantService.GetEnfantWithDonneesSupplementairesAsync(1);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Nom.ShouldBe("Alice");
            result.Data.DonneeSupplementaires.ShouldNotBeNull();
            result.Data.DonneeSupplementaires.ShouldNotBeEmpty();
            result.Data.DonneeSupplementaires.Count.ShouldBe(2);
            result.Data.DonneeSupplementaires.ShouldContain(r => r.Valeur == "A");
        }

        [Fact]
        public async Task GetEnfantWithDonneesSupplementairesAsync_WhenDonneesSupplementairesListIsEmpty_ShouldReturnMappedDto()
        {
            // Arrange
            var enfant = new Enfant
            {
                Id = 1,
                Nom = "Alice",
                DonneeSupplementaires = new List<DonneeSupplementaire>()
            };

            var enfantWithDonneesSupplementairesDto = new EnfantWithDonneesSupplementairesDto
            {
                Id = 1,
                Nom = "Alice",
                DonneeSupplementaires = new List<DonneeSupplementaireDto>()
            };

            _enfantRepositoryMock
                .Setup(repo => repo.GetWithDonneesSupplementairesAsync(1))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map<EnfantWithDonneesSupplementairesDto>(enfant))
                .Returns(enfantWithDonneesSupplementairesDto);

            // Act
            var result = await _enfantService.GetEnfantWithDonneesSupplementairesAsync(1);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.Data.Nom.ShouldBe("Alice");
            result.Data.DonneeSupplementaires.ShouldNotBeNull();
            result.Data.DonneeSupplementaires.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetEnfantWithDonneesSupplementairesAsync_WhenEnfantDoesNotExist_ShouldReturnFail()
        {
            // Arrange
            _enfantRepositoryMock
                .Setup(repo => repo.GetWithDonneesSupplementairesAsync(1))
                .ReturnsAsync(null as Enfant);

            // Act
            var result = await _enfantService.GetEnfantWithDonneesSupplementairesAsync(1);

            // Assert
            result.Success.ShouldBeFalse();
            result.Data.ShouldBeNull();
            result.Message.ShouldBe("Aucun enfant correspondant n'a été trouvé");
        }

        [Fact]
        public async Task CreateEnfantAsync_WhenEnfantIsCreated_ShouldReturnMappedDto()
        {
            // Arrange
            var newEnfantDto = new CreateEnfantDto { Nom = "Alice" };
            var enfant = new Enfant { Id = 1, Nom = "Alice" };
            var createdEnfantDto = new EnfantDto { Id = 1, Nom = "Alice" };

            _mapperMock
                .Setup(m => m.Map<Enfant>(newEnfantDto))
                .Returns(enfant);

            _enfantRepositoryMock
                .Setup(repo => repo.AddAsync(enfant))
                .Returns(Task.CompletedTask);

            _enfantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(enfant.Id))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map<EnfantDto>(enfant))
                .Returns(createdEnfantDto);

            // Act
            var result = await _enfantService.CreateEnfantAsync(newEnfantDto);

            // Assert
            result.Success.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Nom.ShouldBe("Alice");

            _enfantRepositoryMock.Verify(repo => repo.AddAsync(enfant), Times.Once);
        }

        [Fact]
        public async Task CreateEnfantAsync_WhenMappingFails_ShouldReturnFail()
        {
            // Arrange
            var newEnfantDto = new CreateEnfantDto { Nom = "Alice" };

            _mapperMock
                .Setup(m => m.Map<Enfant>(newEnfantDto))
                .Returns((Enfant)null!);

            // Act
            var result = await _enfantService.CreateEnfantAsync(newEnfantDto);

            // Assert
            result.Success.ShouldBeFalse();
            result.Data.ShouldBeNull();
            result.Message.ShouldBe("Erreur lors de la création de l'enfant : Le Mapping a échoué");

            _enfantRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Enfant>()), Times.Never);
        }

        [Fact]
        public async Task UpdateEnfantAsync_WhenEnfantExists_ShouldReturnOk()
        {
            // Arrange
            var id = 1;
            var updateEnfantDto = new UpdateEnfantDto { Id = 1, Nom = "Alice" };
            var enfant = new Enfant { Id = id, Nom = "Alice" };

            _enfantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(enfant);

            _mapperMock
                .Setup(m => m.Map(updateEnfantDto, enfant))
                .Returns(enfant);

            _enfantRepositoryMock
                .Setup(repo => repo.UpdateAsync(enfant))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _enfantService.UpdateEnfantAsync(id, updateEnfantDto);

            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();

            _enfantRepositoryMock.Verify(r => r.UpdateAsync(enfant), Times.Once);
        }

        [Fact]
        public async Task UpdateEnfantAsync_WhenIdMismatch_ShouldReturnFail()
        {
            // Arrange
            var id = 1;
            var updateEnfantDto = new UpdateEnfantDto { Id = 2, Nom = "Alice" };

            // Act
            var result = await _enfantService.UpdateEnfantAsync(id, updateEnfantDto);

            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
            result.Message.ShouldBe("L'identifiant de l'enfant ne correspond pas à celui de l'objet envoyé");

            _enfantRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Enfant>()), Times.Never);
        }

        [Fact]
        public async Task UpdateEnfantAsync_WhenEnfantDoesNotExist_ShouldReturnFail()
        {
            // Arrange
            var id = 1;
            var updateEnfantDto = new UpdateEnfantDto { Id = id, Nom = "Alice" };

            _enfantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(null as Enfant);

            // Act
            var result = await _enfantService.UpdateEnfantAsync(id, updateEnfantDto);

            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
            result.Message.ShouldBe("Aucun enfant correspondant n'a été trouvé");

            _enfantRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Enfant>()), Times.Never);
        }

        [Fact]
        public async Task DeleteEnfantAsync_WhenEnfantExists_ShouldReturnOk()
        {
            // Arrange
            var id = 1;
            var enfant = new Enfant { Id = id, Nom = "Alice" };

            _enfantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(enfant);

            _enfantRepositoryMock
                .Setup(repo => repo.DeleteAsync(id))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _enfantService.DeleteEnfantAsync(id);

            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();

            _enfantRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteEnfantAsync_WhenEnfantDoesNotExist_ShouldReturnFail()
        {
            // Arrange
            var id = 1;

            _enfantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(null as Enfant);

            // Act
            var result = await _enfantService.DeleteEnfantAsync(id);

            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
            result.Message.ShouldBe("Aucun enfant correspondant n'a été trouvé");

            _enfantRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}