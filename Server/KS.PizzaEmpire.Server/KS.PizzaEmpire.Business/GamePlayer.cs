using Microsoft.WindowsAzure.Storage.Table;

namespace KS.PizzaEmpire.Business
{
    public class GamePlayer : TableEntity
    {
        public string Name { get; set; }
    }
}
