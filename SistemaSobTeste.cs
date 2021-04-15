using System.Linq;

namespace CodigoTestavel
{
    public abstract class SistemaSobTeste
    {
        private readonly IDependenciaInterface dependencia;

        public SistemaSobTeste(IDependenciaInterface dependencia)
        {
            this.dependencia = dependencia;
        }

        public virtual object MetodoTestado(params string[] parametros)
        {
            var retornos = (from parametro in parametros select dependencia.Execute(parametro)).ToList();
            return retornos;
        }
    }

    public class SistemaSobTesteDependenteDeInterface : SistemaSobTeste
    {
        public SistemaSobTesteDependenteDeInterface(IDependenciaInterface dependencia) : base(dependencia) { }
    }

    public class SistemaSobTesteDependenteDeAbstrata : SistemaSobTeste
    {
        public SistemaSobTesteDependenteDeAbstrata(DependenciaAbstrata dependencia) : base(dependencia) { }
    }

    public class SistemaSobTesteDependenteDeConcretaVirtual : SistemaSobTeste
    {
        public SistemaSobTesteDependenteDeConcretaVirtual(DependenciaConcretaVirtual dependencia) : base(dependencia) { }
    }

    public class SistemaSobTesteDependenteDeConcretaNaoVirtual : SistemaSobTeste
    {
        public SistemaSobTesteDependenteDeConcretaNaoVirtual(DependenciaConcretaNaoVirtual dependencia) : base(dependencia) { }
    }

    public class SistemaSobTesteDependenteDeConcretaSelada : SistemaSobTeste
    {
        public SistemaSobTesteDependenteDeConcretaSelada(DependenciaConcretaSelada dependencia) : base(dependencia) { }
    }

    public class SistemaSobTesteSemDependencia : SistemaSobTeste
    {
        public SistemaSobTesteSemDependencia() : base(new DependenciaConcretaVirtual()) { }
    }

    public class SistemaSobTesteUsandoEstatico : SistemaSobTeste
    {
        public SistemaSobTesteUsandoEstatico() : base(null) { }

        public override object MetodoTestado(params string[] parametros)
        {
            var retornos = (from parametro in parametros select DependenciaEstatica.Execute(parametro)).ToList();
            return retornos;
        }
    }
}
