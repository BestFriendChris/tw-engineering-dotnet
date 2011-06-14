using System;
using System.Configuration;

namespace VideoWorld.Utils
{
    public class Feature
    {
     
    private readonly int minIteration;

    private const int DEFAULT_ITERATION = 1;

    public Feature(int minIteration) {
        this.minIteration = minIteration;
    }

    public static Feature DetailedMovies { get { return new Feature(3); } }

    public static Feature AdminAccount { get { return new Feature(2); } }

    public bool IsEnabled() {
        return CurrentIteration() >= minIteration;
    }

    private static int CurrentIteration()
    {
        int currentIteration;
        try
        {
            currentIteration = Int32.Parse(ConfigurationManager.AppSettings["CurrentIteration"]);
        }
        catch(ArgumentNullException)
        {
            currentIteration = DEFAULT_ITERATION;
        }
        return currentIteration;
    }

    }
}