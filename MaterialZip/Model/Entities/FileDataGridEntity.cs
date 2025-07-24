namespace MaterialZip.Model.Entities;

/// <summary>
/// Represents <see cref="FileEntity"/> in <see cref="System.Windows.Controls.DataGrid"/>
/// </summary>
/// <param name="Name">Name of the file </param>
/// <param name="Size">Size of the file</param>
/// <param name="LastChanging">Last time, when file was changed </param>
/// <param name="CreatedAt">Time, when file was created</param>
///  <param name="Path">Full path from directory, do not show it the table, use in convertor</param>
public record struct FileDataGridEntity(
    string Name,
    long? Size,
    DateTime LastChanging,
    DateTime CreatedAt,
    string Path
);
