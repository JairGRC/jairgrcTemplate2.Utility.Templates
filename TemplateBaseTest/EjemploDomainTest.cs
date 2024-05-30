using Moq;
using TemplateBaseMicroservice.Domain;
using TemplateBaseMicroservice.Entities.Filter;
using TemplateBaseMicroservice.Entities;
using TemplateBaseMicroservice.Entities.Model;
using TemplateBaseMicroservice.Exceptions;
using TemplateBaseMicroservice.Repository;


namespace TemplateBaseTest
{
    public class EjemploDomainTest
    {
        private readonly Mock<IEjemploRepository> _mockRepo;
        private readonly EjemploDomain _domain;
        public EjemploDomainTest()
        {
            _mockRepo = new Mock<IEjemploRepository>();
            _domain = new EjemploDomain(_mockRepo.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenServiceReturnsSuccess()
        {
            //Arrange
            var ejemplo = new EjemploEntity();
            // Simulamos que la respuesta del repoInsert es 1 
            _mockRepo.Setup(repo => repo.Insert(ejemplo)).ReturnsAsync(1);

            // Act
            var result = await _domain.CreateEjemplo(ejemplo);

            // Assert
            Assert.True((bool)result.Item);
            _mockRepo.Verify(repo => repo.Insert(ejemplo), Times.Once);
        }
        [Fact]
        public async Task EditEjemplo_ShouldReturnSuccess_WhenUpdateIsSuccessful()
        {
            // Arrange
            var ejemplo = new EjemploEntity();
            var ejemploUpdate = new EjemploUpdateDto("Jair",25,"jair@gmail.com");
            // Simulamos que la respuesta del repoUpdate es true 
            _mockRepo.Setup(repo => repo.Update(It.IsAny<EjemploEntity>())).ReturnsAsync(true);

            // Act
            var result = await _domain.EditEjemplo(ejemploUpdate);

            // Assert
            Assert.True((bool)result.Item);
            _mockRepo.Verify(repo => repo.Update(It.Is<EjemploEntity>(e => e.Nombre == ejemploUpdate.Nombre && e.Edad == ejemploUpdate.Edad && e.Email == ejemploUpdate.Email)), Times.Once);

        }

        [Fact]
        public async Task EditEjemplo_ShouldThrowException_WhenUpdateFails()
        {
            // Arrange
            var ejemplo = new EjemploEntity();
            var ejemploUpdate = new EjemploUpdateDto("Jair", 25, "jair@gmail.com");
            // Simulamos que la respuesta del repoUpdate es false y que sucede un error
            _mockRepo.Setup(repo => repo.Update(It.IsAny<EjemploEntity>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<EjemploEditHeaderException>(() => _domain.EditEjemplo(ejemploUpdate));
            _mockRepo.Verify(repo => repo.Update(It.IsAny<EjemploEntity>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEjemplo_ShouldReturnSuccess_WhenDeleteIsSuccessful()
        {
            // Arrange
            var ejemplo = new EjemploEntity { ID = 1 };
            // Simulamos que la respuesta del repoDelete es true
            _mockRepo.Setup(repo => repo.Delete(ejemplo.ID)).ReturnsAsync(true);

            // Act
            var result = await _domain.DeleteEjemplo(ejemplo);

            // Assert
            Assert.True((bool)result.Item);
            _mockRepo.Verify(repo => repo.Delete(ejemplo.ID), Times.Once);
        }

        [Fact]
        public async Task GetByItem_ShouldReturnItem_WhenRepositoryReturnsItem()
        {
            // Arrange
            var filter = new EjemploFilter(1);
            var ejemploEntity = new EjemploEntity();
            _mockRepo.Setup(repo => repo.GetItem(filter, EjemploFilterItemType.ByItemxID)).ReturnsAsync(ejemploEntity);

            // Act
            var result = await _domain.GetByItem(filter, EjemploFilterItemType.ByItemxID);

            // Assert
            Assert.Equal(ejemploEntity, result.Item);
            _mockRepo.Verify(repo => repo.GetItem(filter, EjemploFilterItemType.ByItemxID), Times.Once);
        }

        [Fact]
        public async Task GetByList_ShouldReturnList_WhenRepositoryReturnsList()
        {
            // Arrange
            var filter = new EjemploFilter(1);
            var pagination = new Pagination();
            var ejemploEntities = new List<EjemploEntity> { new EjemploEntity() };
            _mockRepo.Setup(repo => repo.GetLstItem(filter, EjemploFilterItemType.ListItemEjemplo, pagination)).ReturnsAsync(ejemploEntities);

            // Act
            var result = await _domain.GetByList(filter, EjemploFilterItemType.ListItemEjemplo, pagination);

            // Assert
            Assert.Equal(ejemploEntities, result.Item);
            _mockRepo.Verify(repo => repo.GetLstItem(filter, EjemploFilterItemType.ListItemEjemplo, pagination), Times.Once);
        }
    }
}