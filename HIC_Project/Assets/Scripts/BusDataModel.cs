// Non-Executable Script that defines all structures for holding
// bus information to be used in other scripts.

using System;

// Class definition for Bus Stop information.
[Serializable]
public class BusStop
{
    public int stopID;
    public string name;
    public string address;
    public float x;
    public float y;
    // Add a "mapID" variable to define which map the bus stop
    // should be displayed on (Main Campus / Dix Stadium).
}

// Class definition for Bus Route information.
[Serializable]
public class BusRoute
{
    public int routeID;
    public string routeName;
    public int[] stops; // Array of Bus Stop IDs.
}

// Class definition for Bus Data information (Contains "BusStop" and "BusRoute" objects).
[Serializable]
public class BusData
{
    public BusStop[] busStops; // Arrray of "BusStop"
    public BusRoute[] busRoutes; // Array of "BusRoute"
}
