﻿using Microsoft.Azure.Storage;

namespace KiCoKalender
{
    public static class StorageAccountSettings
    {
        public static CloudStorageAccount CreateStorageAccountFromConnectionString()
        {
            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");

            return storageAccount;
        }
    }
}
