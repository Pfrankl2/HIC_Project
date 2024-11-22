// Non-Executable Script that defines all structures for holding
// bus information to be used in other scripts.
//
// DOES NOT DEAL WITH ANY LOGIC!

using System;

// Class definition for Bus Stop information.
[Serializable]
public class BusStop
{
    public int stopID;
    public string name;
    public float latitude;
    public float longitude;
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
