namespace Aspirate.Shared.Inputs;

public record BindMountImageBuilderOptions()
{
    public string ContainerBuilder { get; set; } = default!;
    public string StagingDirectory { get; set; } = default!;
    public string ImageName { get; set; } = default!;
    public Dictionary<string, string> Volumes { get; set; } = [];
    public bool NonInteractive { get; set; }

    public static BindMountImageBuilderOptions NewInstance(
        IFileSystem fileSystem,
        string containerBuilder,
        string imageName,
        Dictionary<string, string> volumes,
        bool? nonInteractive = false) =>
        new()
        {
            ContainerBuilder = containerBuilder,
            StagingDirectory = fileSystem.Path.Combine(fileSystem.Path.GetTempPath(), AspirateLiterals.AspirateName, AspirateLiterals.BindMountsTempFolder),
            ImageName = imageName,
            Volumes = volumes,
            NonInteractive = nonInteractive ?? false
        };
}
