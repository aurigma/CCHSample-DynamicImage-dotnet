using Aurigma.AssetStorage;

namespace DesignBrowserMVC.Models
{
    public class DesignsFolderModel
    {
        public DesignsFolderModel(string path, FolderContentOfImageDto content)
        {
            Path = path;
            Content = content;
        }
        public string Path { get; private set; }
        public FolderContentOfImageDto Content { get; private set; }
    }
}
