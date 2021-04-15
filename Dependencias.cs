namespace CodigoTestavel
{
    public interface IDependenciaInterface
    {
        Retorno Execute(string parametro);
    }

    public abstract class DependenciaAbstrata : IDependenciaInterface
    {
        public abstract Retorno Execute(string parametro);
    }

    public class DependenciaConcretaVirtual : IDependenciaInterface
    {
        public virtual Retorno Execute(string parametro)
        {
            return new Retorno { Valor = parametro };
        }
    }

    public class DependenciaConcretaNaoVirtual : IDependenciaInterface
    {
        public Retorno Execute(string parametro)
        {
            return new Retorno { Valor = parametro };
        }
    }

    public sealed class DependenciaConcretaSelada : IDependenciaInterface
    {
        public Retorno Execute(string parametro)
        {
            return new Retorno { Valor = parametro };
        }
    }

    public static class DependenciaEstatica
    {
        public static string CampoGlobalPerigoso = "";
        public static Retorno Execute(string parametro)
        {
            if (string.IsNullOrWhiteSpace(CampoGlobalPerigoso))
                CampoGlobalPerigoso = parametro;

            return new Retorno { Valor = CampoGlobalPerigoso };
        }
    }
}
