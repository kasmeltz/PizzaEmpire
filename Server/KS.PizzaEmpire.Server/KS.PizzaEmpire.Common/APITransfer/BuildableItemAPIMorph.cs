namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class that can morph BuildableItem objects to and from a dto format
    /// </summary>
    public class BuildableItemAPIMorph : IAPIEntityMorph
    {        
        /// <summary>
        /// Converts a business object to an API dto object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IAPIEntity ToAPIFormat(IBusinessObjectEntity entity)
        {
            BuildableItemAPI api = new BuildableItemAPI();

            BuildableItem bi = entity as BuildableItem;
            api.ItemCode = bi.ItemCode;

            WorkItem workItem = entity as WorkItem;
            if (workItem != null)
            {
                api.WorkItem = workItem;
                return api;
            }

            ProductionItem productionItem = entity as ProductionItem;
            if (productionItem != null)
            {
                api.ProductionItem = productionItem;
                return api;
            }

            ConsumableItem consumableItem = entity as ConsumableItem;
            if (consumableItem != null)
            {
                api.ConsumableItem = consumableItem;
                return api;
            }

            StorageItem storageItem = entity as StorageItem;
            if (storageItem != null)
            {
                api.StorageItem = storageItem;
                return api;
            }

            return null;
        }

        /// <summary>
        /// Converts an API dto objec to a business object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IBusinessObjectEntity ToBusinessFormat(IAPIEntity entity)
        {
            BuildableItemAPI api = entity as BuildableItemAPI;

            if (api.WorkItem != null)
            {
                return api.WorkItem;
            }

            if (api.ProductionItem != null)
            {
                return api.ProductionItem;
            }

            if (api.StorageItem != null)
            {
                return api.StorageItem;
            }

            if (api.ConsumableItem != null)
            {
                return api.ConsumableItem;
            }

            return null;
        }
    }
}
