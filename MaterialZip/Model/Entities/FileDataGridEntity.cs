namespace MaterialZip.Model.Entities;

/// <summary>
/// Represents <see cref="FileEntity"/> in <see cref="System.Windows.Controls.DataGrid"/>
/// </summary>
/// <param name="Name">Name of the file </param>
/// <param name="Size">Size of the file</param>
/// <param name="LastChanging">Last time, when file was changed </param>
/// <param name="CreatedAt">Time, when file was created</param>
public record struct FileDataGridEntity(
    string Name,
    long? Size,
    DateTime LastChanging,
    DateTime CreatedAt
);
