using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;


namespace FlirtyLocation {

    public partial class LocationMeasurementManager {

        public async Task<IEnumerable<LocationMeasurement>> GetAllDataAsync() {
            return await _locationMeasurementTable.ReadAsync();
        }

        // This does not work!!! Three frustrating days....
        public async Task<IEnumerable<LocationMeasurement>> GetSpecificTrackAsync(int trackId) {
            var query = _locationMeasurementTable.Where(m => m.TrackId == trackId);
            return await query.ToEnumerableAsync();
            
        }

    }
}

/* Correct syntax and usage??
IMobileServiceTableQuery<TodoItem> query = todoTable.CreateQuery().Select(todoItem => todoItem.Text);
List<string> items = await query.ToListAsync();
*/