using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodigoTestavel
{
    public class Testes
    {
        public static Retorno[] GerarRetornos() => new[]
        {
            new Retorno{ Valor = "a" },
            new Retorno{ Valor = "b" },
            new Retorno{ Valor = "c" },
        };

        [Fact]
        public void DependenciaInterface()
        {
            // arrange
            var retornosEsperados = GerarRetornos();
            var parametros = retornosEsperados.Select(x => x.Valor).ToArray();
            var mockDependencia = new Mock<IDependenciaInterface>();
            foreach (var esperado in retornosEsperados)
                mockDependencia.Setup(x => x.Execute(esperado.Valor)).Returns(esperado);
            var dependenciaMocada = mockDependencia.Object;
            var sut = new SistemaSobTesteDependenteDeInterface(dependenciaMocada);

            // act
            var resultado = sut.MetodoTestado(parametros) as List<Retorno>;

            // assert
            Assert.NotNull(resultado);
            Assert.IsType<List<Retorno>>(resultado);
            Assert.True(resultado.Count == retornosEsperados.Length);
            Assert.True(resultado.Select(x => x.Valor).Distinct().Count() == parametros.Distinct().Count());
            foreach (var item in resultado)
            {
                Assert.Contains(item, retornosEsperados);
                Assert.Contains<Retorno>(retornosEsperados, i => i.Valor == item.Valor); // desnecessário mocando retornos
            }
        }

        [Fact]
        public void DependenciaAbstrata()
        {
            // arrange
            var retornosEsperados = GerarRetornos();
            var parametros = retornosEsperados.Select(x => x.Valor).ToArray();
            var mockDependencia = new Mock<DependenciaAbstrata>();
            foreach (var esperado in retornosEsperados)
                mockDependencia.Setup(x => x.Execute(esperado.Valor)).Returns(esperado);
            var dependenciaMocada = mockDependencia.Object;
            var sut = new SistemaSobTesteDependenteDeAbstrata(dependenciaMocada);

            // act
            var resultado = sut.MetodoTestado(parametros) as List<Retorno>;

            // assert
            Assert.NotNull(resultado);
            Assert.IsType<List<Retorno>>(resultado);
            Assert.True(resultado.Count == retornosEsperados.Length);
            Assert.True(resultado.Select(x => x.Valor).Distinct().Count() == parametros.Distinct().Count());
            foreach (var item in resultado)
            {
                Assert.Contains(item, retornosEsperados);
                Assert.Contains<Retorno>(retornosEsperados, i => i.Valor == item.Valor); // desnecessário mocando retornos
            }
        }

        [Fact]
        public void DependenciaConcretaVirtual()
        {
            // arrange
            var retornosEsperados = GerarRetornos();
            var parametros = retornosEsperados.Select(x => x.Valor).ToArray();
            var mockDependencia = new Mock<DependenciaConcretaVirtual>();
            foreach (var esperado in retornosEsperados)
                mockDependencia.Setup(x => x.Execute(esperado.Valor)).Returns(esperado);
            var dependenciaMocada = mockDependencia.Object;
            var sut = new SistemaSobTesteDependenteDeConcretaVirtual(dependenciaMocada);

            // act
            var resultado = sut.MetodoTestado(parametros) as List<Retorno>;

            // assert
            Assert.NotNull(resultado);
            Assert.IsType<List<Retorno>>(resultado);
            Assert.True(resultado.Count == retornosEsperados.Length);
            Assert.True(resultado.Select(x => x.Valor).Distinct().Count() == parametros.Distinct().Count());
            foreach (var item in resultado)
            {
                Assert.Contains(item, retornosEsperados);
                Assert.Contains<Retorno>(retornosEsperados, i => i.Valor == item.Valor); // desnecessário mocando retornos
            }
        }

        [Fact]
        public void DependenciaConcretaNaoVirtual()
        {
            // arrange
            var retornosEsperados = GerarRetornos();
            var parametros = retornosEsperados.Select(x => x.Valor).ToArray();

            // codigo gera erro por metodo não permitir sobreescrita 
            //var mockDependencia = new Mock<DependenciaConcretaNaoVirtual>();
            //foreach (var esperado in retornosEsperados)
            //    mockDependencia.Setup(x => x.Execute(esperado.Valor)).Returns(esperado);

            var dependenciaNaoMocavel = new DependenciaConcretaNaoVirtual();
            var sut = new SistemaSobTesteDependenteDeConcretaNaoVirtual(dependenciaNaoMocavel);

            // act
            var resultado = sut.MetodoTestado(parametros) as List<Retorno>;

            // assert
            Assert.NotNull(resultado);
            Assert.IsType<List<Retorno>>(resultado);
            Assert.True(resultado.Count == retornosEsperados.Length);
            Assert.True(resultado.Select(x => x.Valor).Distinct().Count() == parametros.Distinct().Count());

            foreach (var item in resultado)
            {
                Assert.Contains(item, retornosEsperados); // assert vai falhar
                Assert.Contains<Retorno>(retornosEsperados, i => i.Valor == item.Valor);
            }
        }

        [Fact]
        public void DependenciaConcretaSelada()
        {
            // arrange
            var retornosEsperados = GerarRetornos();
            var parametros = retornosEsperados.Select(x => x.Valor).ToArray();

            //codigo gera erro por metodo não permitir sobreescrita
            //var mockDependencia = new Mock<DependenciaConcretaSelada>();
            //foreach (var esperado in retornosEsperados)
            //    mockDependencia.Setup(x => x.Execute(esperado.Valor)).Returns(esperado);

            var dependenciaNaoMocavel = new DependenciaConcretaSelada();
            var sut = new SistemaSobTesteDependenteDeConcretaSelada(dependenciaNaoMocavel);

            // act
            var resultado = sut.MetodoTestado(parametros) as List<Retorno>;

            // assert
            Assert.NotNull(resultado);
            Assert.IsType<List<Retorno>>(resultado);
            Assert.True(resultado.Count == retornosEsperados.Length);
            Assert.True(resultado.Select(x => x.Valor).Distinct().Count() == parametros.Distinct().Count());

            foreach (var item in resultado)
            {
                Assert.Contains(item, retornosEsperados); // assert vai falhar
                Assert.Contains<Retorno>(retornosEsperados, i => i.Valor == item.Valor);
            }
        }

        [Fact]
        public void SemDependencia()
        {
            // arrange
            var retornosEsperados = GerarRetornos();
            var parametros = retornosEsperados.Select(x => x.Valor).ToArray();
            var sut = new SistemaSobTesteSemDependencia();

            // act
            var resultado = sut.MetodoTestado(parametros) as List<Retorno>;

            // assert
            Assert.NotNull(resultado);
            Assert.IsType<List<Retorno>>(resultado);
            Assert.True(resultado.Count == retornosEsperados.Length);
            Assert.True(resultado.Select(x => x.Valor).Distinct().Count() == parametros.Distinct().Count());

            foreach (var item in resultado)
            {
                Assert.Contains(item, retornosEsperados); // assert vai falhar
                Assert.Contains<Retorno>(retornosEsperados, i => i.Valor == item.Valor);
            }
        }

        [Fact]
        public void UsandoEstatico()
        {
            // arrange
            var retornosEsperados = GerarRetornos();
            var parametros = retornosEsperados.Select(x => x.Valor).ToArray();
            var sut = new SistemaSobTesteUsandoEstatico();

            // act
            var resultado = sut.MetodoTestado(parametros) as List<Retorno>;

            // assert
            Assert.NotNull(resultado);
            Assert.IsType<List<Retorno>>(resultado);
            Assert.True(resultado.Count == retornosEsperados.Length);

            // asserts vão falhar
            Assert.True(resultado.Select(x => x.Valor).Distinct().Count() == parametros.Distinct().Count());
            //foreach (var item in resultado)
            //{
            //    Assert.Contains(item, retornosEsperados);
            //    Assert.Contains<Retorno>(retornosEsperados, i => i.Valor == item.Valor);
            //}

            foreach (var item in resultado)
                Assert.Contains<Retorno>(retornosEsperados, i => i.Valor == item.Valor);
        }
    }
}
