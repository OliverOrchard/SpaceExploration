using Xunit;

namespace SpaceExploration.IntegrationTest;

public class LocalResourceCollection
{
    [CollectionDefinition("Local resources collection")]
    public class LocalResourcesCollection : ICollectionFixture<SpaceExplorationFixture> { }
}