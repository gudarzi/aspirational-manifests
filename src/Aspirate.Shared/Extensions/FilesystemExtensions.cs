namespace Aspirate.Shared.Extensions;

public static class FilesystemExtensions
{
    public static void CopyDirectoryRecursively(this IFileSystem filesystem, string sourcePath, string targetPath)
    {
        if (filesystem.Directory.Exists(sourcePath))
        {
            filesystem.Directory.CreateDirectory(targetPath);

            var entries = filesystem.Directory.GetFileSystemEntries(sourcePath);

            foreach (var entry in entries)
            {
                var name = filesystem.Path.GetFileName(entry);
                var source = filesystem.Path.Combine(sourcePath, name);
                var target = filesystem.Path.Combine(targetPath, name);

                filesystem.CopyDirectoryRecursively(source, target);
            }
        }
        else
        {
            filesystem.File.Copy(sourcePath, targetPath);
        }
    }
}
