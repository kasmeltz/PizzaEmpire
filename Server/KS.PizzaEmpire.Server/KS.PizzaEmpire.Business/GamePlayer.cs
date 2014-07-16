using Microsoft.WindowsAzure.Storage.Table;

namespace KS.PizzaEmpire.Business
{
    public class GamePlayer : TableEntity
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PartitionKey = _name.Substring(0, 2);
            }
        }
    }
}
