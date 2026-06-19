## Part A

As suggested, I removed join, but that resulted in the output getting scrambled. The output that was supposed to come after they where all done, got printed at the very start. All thread STARTs where shuffeled, but the thread ENDs seemed to get printed as expected.

## Part A and Part B:

Logging stuff within threads or tasks will affect performance, so the performance difference between "threads + join" VS "Tasks + WhenAll", especially with the randomized delay, is not something I expected to be blown away by.


## Async Drone Dash rewritten case to Hotel Booking Engine

Instead of a control tower with updated checkpoints, weather or retrictions, I created a booking engine for hotel rooms that would also contain weather data and success/fail data.
