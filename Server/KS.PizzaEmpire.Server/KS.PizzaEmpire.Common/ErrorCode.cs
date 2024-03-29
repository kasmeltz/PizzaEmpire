﻿
namespace KS.PizzaEmpire.Common
{
    /// <summary>
    /// A list of application error codes
    /// </summary>
    public enum ErrorCode
    {
        ERROR_OK = 0,
        CONNECTION_ERROR = 1000,        
        ERROR_RETRIEVING_ACCOUNT = 2000,
        START_WORK_INVALID_ITEM = 3000,
        START_WORK_ITEM_NOT_AVAILABLE = 3001,
        START_WORK_INSUFFICIENT_COINS = 3002,
        START_WORK_INSUFFICIENT_COUPONS = 3003,
        START_WORK_INVALID_INGREDIENTS = 3004,
        START_WORK_INSUFFICIENT_INGREDIENTS = 3005,
        START_WORK_NO_PRODUCTION_CAPACITY = 3006,
        START_WORK_NO_STORAGE_CAPACITY = 3007,
        START_WORK_INSUFFICIENT_NEGATIVE = 3008,
        START_WORK_MULTIPLE_NON_CONSUMABLE = 3009,
        START_WORK_INVALID_LEVEL = 3010,
        START_WORK_INVALID_LOCATION = 3011,
        ITEM_ALREADY_EXISTS = 4000,
        RESOURCE_CALLS_NOT_PAIRED = 5000
    }
}
