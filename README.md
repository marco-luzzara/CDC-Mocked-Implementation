# CDC Mocked Implementation
It is an architectural draft on how the CDC pattern could be implemented, assuming changes are stored in a Data Lake. secondary components have been mocked, like the data sources and the Data Lake. Even though it is a working demo, it is not tested and many invariants have not been checked, it is devised only as a demo to show the results of the CDC pattern.
Here is the class diagram for the core components:

![Class Diagram](/ClassDiagram.jpg)

* `IObjectStorage`: it is an interface defining a single method (`GetData`) called by a runner when it must retrieve data from the object storage (or data source). The only implemented one is the `MockDataStorage`, which returns objects generated randomly by `CDCImplementation.DataGenerator`. You can easily implement a `SqlDataStorage` if you want to take data from a sql table. In this last case, it is noteworthy to underline that if you want to apply the CDC on multiple partition of your table, you could easily create multiple `SqlDataStorage` referencing that table.
* `CDCRunnerContext`: it is the component in charge of setup the CDC execution, starting from initializing the Data Lake till the commit, which may be necessary if you have taken into consideration a move and rename pattern. Between those operation as many `CDCRunner` as the number of data sources are spawned. In this implementation their execution is parallelized but they could be potentially executed on remote machines.
* `CDCRunner`: its purpose is to retrieve objects from the data source and putting changes on the Data Lake. To do this it:
  1. Gets the last _sync.json_ saved on data lake
  1. Queries the data source
  1. Applies the requested CDCStrategy to obtain fresh rows
  1. Puts them on Data Lake
  1. Update the new _sync.json_
It does not contain logic by itself, but it is devised to process a large amount of data.
* `AbstractDataLake`: it is an abstract class from which `MockDataLake` and all possible Data Lake client must inherit from. About the only given implementation, it simulates a Data Lake on your local machine, see details on Usage paragraph. For the saving of objects, the `InsertFreshRows`'s current implementation takes a `sourceId` (data source id) and a `partitionId`. If you have more than a single partition, all _sync.json_ (one from each partition) are saved in the same folder, and the same goes for data files. Adding a further directory layer specific for each partition is pretty easy.
* `ICDCStrategy`: almost all business logic for the CDC technique chosen is contained here, or better in a class that implements this interface. The Diff&Where and the timestamp solutions are given, right now each CDCStrategy expects that the processed objects implement a custom interface, theoretically different for each technique (`IDiffWhereCompatible`, `ITimestampCompatible`, ...). 

## Installation

It is a Console Application, it just needs to be compiled and run. you can configure the `CDCRunnerContext` as much as you like in the _Program.cs_ . The Data Lake folder will be in the root of the output directory after the first execution. The output directory will probably be _CDCImplementation/bin/Debug/netcoreapp3.1/_.

## Usage

Again, you can find a usage example inside the _Program.cs_ . Essentially, you setup a new instance of `CDCRunnerContext`, passing to it 3 parameters:
* a reference to an `AbstractDataLake`
* a list of `IObjectStorage`
* the `ICDCStrategy` implementation that the context will use
