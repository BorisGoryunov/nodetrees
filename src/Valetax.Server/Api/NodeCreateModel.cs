namespace Valetax.Server.Api;

public record NodeCreateModel(int TreeId, int? ParentNodeId, string Name);
