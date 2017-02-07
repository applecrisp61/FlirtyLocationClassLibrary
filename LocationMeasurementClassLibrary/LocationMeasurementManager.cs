/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
//#define OFFLINE_SYNC_ENABLED


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;


#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace FlirtyLocation {
    public partial class LocationMeasurementManager {
        // Don't understand why this is a partial class--> If correct, where does the rest of the definition come from??
        // Is it partial so that I can leave it completely unchanged and just add my addtions to a new file??  That would make sense

        private static LocationMeasurementManager _defaultInstance = new LocationMeasurementManager();
        private MobileServiceClient _client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<LocationMeasurement> locationMeasurementTable;
#else
        private IMobileServiceTable<LocationMeasurement> _locationMeasurementTable;
#endif

        private const string offline_db_path = @"localstore.db";

        private LocationMeasurementManager() {
            this._client = new MobileServiceClient(ConstantsRt.ApplicationUrl);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<LocationMeasurement>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.locationMeasurementTable = client.GetSyncTable<LocationMeasurement>();
#else
            this._locationMeasurementTable = _client.GetTable<LocationMeasurement>();
#endif
        }

        public static LocationMeasurementManager DefaultManager
        {
            get
            {
                return _defaultInstance;
            }
            private set
            {
                _defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return _client; }
        }

        public bool IsOfflineEnabled
        {
            get { return _locationMeasurementTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<LocationMeasurement>; }
        }

        public async Task<ObservableCollection<LocationMeasurement>> GetLocationMeasurementsAsync(bool syncItems = false) {
            try {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<LocationMeasurement> items = await _locationMeasurementTable
                    .ToEnumerableAsync();

                return new ObservableCollection<LocationMeasurement>(items);
            }
            catch (MobileServiceInvalidOperationException msioe) {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e) {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveMeasurementAsync(LocationMeasurement measurement) {
            if (measurement.Id == null) {
                await _locationMeasurementTable.InsertAsync(measurement);
            }
            else {
                await _locationMeasurementTable.UpdateAsync(measurement);
            }
        }

#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.locationMeasurementTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allTodoItems",
                    this.locationMeasurementTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
#endif

    }
}
