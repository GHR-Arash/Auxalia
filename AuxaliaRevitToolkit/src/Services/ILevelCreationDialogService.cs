public interface ILevelCreationDialogService
{
    bool ShowDialog(out string levelName, out double levelElevation);
}