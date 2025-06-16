using RastreamentoPedido.Core.Repositories.Transporte;

interface ITransporte
{
  string Entregar();
}


namespace RastreamentoPedido.Core.Repositories.Transporte
{
  abstract class Creator
  {
    public abstract ITransporte transportar();

    public string operacao()
    {
      var transporte = transportar();
      var resultado = "Criador: O mesmo código do criador acabou de funcionar com" + transporte.Entregar();

      return resultado;
    }
  }

  class ConcreteOperation1 : Creator
  {
    public override ITransporte transportar()
    {
      return new concreteProduct1();
    }
  }

  class concreteProduct1 : ITransporte
  {
    public string Entregar()
    {
      return "{Result of ConcreteProduct2}";
    }
  }

}

namespace RastreamentoPedido.Core.Repositories.Client
{
  class Client
  {
    public void Main()
    {
      Console.WriteLine("App: Launched with the ConcreteCreator1.");
      ClientCode(new concreteProduct1());
    }

        private void ClientCode(concreteProduct1 concreteProduct1)
        {
            throw new NotImplementedException();
        }

        public void ClientCode(Creator creator)
    {
      // ...
      Console.WriteLine("Client: I'm not aware of the creator's class," +
          "but it still works.\n" + creator.operacao());
      // ...
    }
  }
}
