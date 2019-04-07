using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Moq.AutoMock;
using FlexRule.Services;
using FlexRule.Contracts;
using FlexRule.Models;
using FlexRule.Models.Repository;
using Xunit;
using System.Linq;

namespace FlexRuleTest
{
    public class GlossaryTermServiceTests
    {
        private readonly GlossaryTermService _glossaryTermService;
        private readonly Mock<ITermRepository> _termRepository;
        private readonly List<GlossaryTerm> _glossaryTerms;

        public GlossaryTermServiceTests()
        {
            var mocker = new AutoMocker();
            _termRepository = mocker.GetMock<ITermRepository>();

            _glossaryTermService = mocker.CreateInstance<GlossaryTermService>();

            _glossaryTerms = new List<GlossaryTerm>()
            {
                new GlossaryTerm() { TermId = 1, Term = "abyssal plain",
                    Definition = "The ocean floor offshore from the continental margin, usually very flat with a slight slope." },
                new GlossaryTerm() { TermId = 2, Term = "accrete",
                    Definition = "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass." },
                new GlossaryTerm() { TermId = 3, Term = "alkaline",
                    Definition = "Term pertaining to a highly basic, as opposed to acidic, subtance. For example, hydroxide or carbonate of sodium or potassium." }
            };

            _termRepository.Setup(t => t.GetAll()).Returns(_glossaryTerms);
        }

        [Fact]
        public void Test_GetAll()
        {
            //Arrange

            // Act
            var result = _glossaryTermService.GetAll();

            var firstTerm = result.First();
            var lastTerm = result.ElementAt(2);

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(firstTerm.Term == "abyssal plain");
            Assert.True(lastTerm.Term == "alkaline");
        }

        [Fact]
        public void Test_CanCreateGlossaryTerm()
        {
            //Arrange
            var newGlossaryTerm = new CreateGlossaryTermRequest
            {
                Term = "Actuary",
                Definition = "Actuaries are employed by insurance companies and pensions providers to calculate factors such as life expectancy, accident rates and likely payouts by using complex mathematical formulas"
            };

            // Act
            var result = _glossaryTermService.CreateGlossaryTerm(newGlossaryTerm);

            // Assert
            _termRepository.Verify(r => r.Add(It.Is<GlossaryTerm>(t => t.Term == "Actuary")), Times.Once);
        }

        [Fact]
        public void Test_CanEditGlossaryTerm()
        {
            //Arrange
            var changedGlossaryTerm = new UpdateGlossaryTermRequest
            {
                Definition = "new definition goes here"
            };

            // Act
            var result = _glossaryTermService.UpdateGlossaryTerm(changedGlossaryTerm, 1);

            // Assert
            _termRepository.Verify(r => r.Get(It.Is<int>(t => t == 1)), Times.Once);
            _termRepository.Verify(r => r.Update(It.IsAny<GlossaryTerm>(), It.IsAny<GlossaryTerm>()), Times.Once);
        }

        [Fact]
        public void Test_CanDeleteGlossaryTerm()
        {
            //Arrange
            var term = new GlossaryTerm { TermId = 1, Term = "term 1", Definition = "Definition here" };
            _termRepository.Setup(t => t.Get(It.IsAny<int>())).ReturnsAsync(term);

            // Act
            var result = _glossaryTermService.DeleteGlossaryTerm(1);

            // Assert
            _termRepository.Verify(r => r.Get(It.Is<int>(t => t == 1)), Times.Once);
            _termRepository.Verify(r => r.Delete(It.IsAny<GlossaryTerm>()), Times.Once);
        }

    }
}
