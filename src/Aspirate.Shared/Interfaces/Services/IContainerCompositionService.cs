namespace Aspirate.Shared.Interfaces.Services;

/// <summary>
/// Represents a service for container composition.
/// </summary>
public interface IContainerCompositionService
{
    /// <summary>
    /// Builds and pushes a container for the specified project using the specified container details and interactive mode flag.
    /// </summary>
    /// <param name="projectResource">The project to build and push the container for.</param>
    /// <param name="containerDetails">The container properties used to build and push the container.</param>
    /// <param name="options">Container options.</param>
    /// <param name="nonInteractive">Flag indicating whether the process should run in non-interactive mode.</param>
    /// <param name="runtimeIdentifier">The runtime identifier to use for project builds.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result will be true if the container build and push was successful,
    /// or false if there was an error during the process.
    /// </returns>
    Task<bool> BuildAndPushContainerForProject(ProjectResource projectResource,
        MsBuildContainerProperties containerDetails,
        ContainerOptions options,
        bool nonInteractive = false,
        string? runtimeIdentifier = null);

    /// <summary>
    /// Build and push a container for a Dockerfile.
    /// </summary>
    /// <param name="dockerfileResource">The Dockerfile to build the container from.</param>
    /// <param name="options">The dockerfile options.</param>
    /// <param name="nonInteractive">Flag to determine if the build process should be non-interactive.</param>
    /// <returns>
    /// A <see cref="Task{T}"/> representing the asynchronous operation.
    /// The task will return true if the container was built and pushed successfully,
    /// otherwise it will return false.
    /// </returns>
    /// <remarks>
    /// This method builds a container using the provided Dockerfile and builder.
    /// It then pushes the created image to the specified registry.
    /// The nonInteractive parameter can be set to true to suppress any interactive prompts during the build process.
    /// </remarks>

    /// <summary>
    /// Asynchronously builds a Docker image for binding mounts.
    /// </summary>
    /// <param name="imageName">The name of the Docker image to be built.</param>
    /// <param name="volumes">A dictionary containing the source and target paths for the volumes to be included in the Docker image. The key is the source path on the host machine, and the value is the target path in the Docker image.</param>
    /// <param name="parameters">The parameters for the Docker container.</param>
    /// <param name="nonInteractive">A boolean value indicating whether the build process should run in non-interactive mode. Defaults to false.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation. The task result contains a boolean value indicating the success or failure of the Docker image build process.</returns>
    Task<bool> BuildImageForBindingMounts(string imageName,
        Dictionary<string, string> volumes,
        ContainerParameters parameters,
        bool nonInteractive = false);
    Task<bool> BuildAndPushContainerForDockerfile(DockerfileResource dockerfileResource, ContainerOptions options, bool? nonInteractive);
}
