using PKHeX.Core;

namespace PKDroid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnOpenSaveClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select Pokémon save file",
                FileTypes = FilePickerFileType.All
            });

            if (result == null)
            {
                StatusLabel.Text = "Canceled.";
                return;
            }

            var path = result.FullPath;
            var data = await File.ReadAllBytesAsync(path);

            var saveFile = SaveUtil.GetVariantSAV(data, path);

            if (saveFile == null)
            {
                StatusLabel.Text = "Unsupported or invalid save file.";
                return;
            }

            StatusLabel.Text =
                $"Loaded save:\nTrainer: {saveFile.OT}\nGame: {saveFile.Version}";
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Error: {ex.Message}";
        }
    }
}
