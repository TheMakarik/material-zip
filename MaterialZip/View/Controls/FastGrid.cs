using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace MaterialZip.View.Controls;

/// <summary>
/// Represents a specialized Grid control that provides a simplified syntax
/// for defining rows and columns using comma-separated string values.
/// Supports standard GridUnitType values (Auto, Star, and Pixel) in a compact format.
/// </summary>
public class FastGrid : Grid
{
    private const string Star = "*";
    private const char StarChar = '*'; 
    private const string Auto = "Auto";
    
    /// <summary>
    /// Identifies the <see cref="RowDefinitions"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RowDefinitionsProperty =
        DependencyProperty.Register(
            nameof(RowDefinitions),
            typeof(string),
            typeof(FastGrid),
            new PropertyMetadata(string.Empty, OnRowDefinitionsChanged));

    /// <summary>
    /// Identifies the <see cref="ColumnDefinitions"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColumnDefinitionsProperty =
        DependencyProperty.Register(
            nameof(ColumnDefinitions),
            typeof(string),
            typeof(FastGrid),
            new PropertyMetadata(string.Empty,  OnColumnDefinitionsChanged));
    
    /// <summary>
    /// Gets or sets a comma-separated string defining grid rows.
    /// <para>Valid values:</para>
    /// <list type="bullet">
    /// <item><description>"Auto": Auto-sized row</description></item>
    /// <item><description>"*": Star-sized row with default weight (1)</description></item>
    /// <item><description>"N*": Star-sized row with custom weight (e.g., "2*")</description></item>
    /// <item><description>Pixel values: Numeric values (e.g., "150")</description></item>
    /// </list>
    /// <example>
    /// "Auto, 2*, 100, *"
    /// </example>
    /// </summary>
    public new string RowDefinitions
    {
        get => (string)GetValue(RowDefinitionsProperty);
        set => SetValue(RowDefinitionsProperty, value);
    }

    /// <summary>
    /// Gets or sets a comma-separated string defining grid columns.
    /// <para>Valid values:</para>
    /// <list type="bullet">
    /// <item><description>"Auto": Auto-sized column</description></item>
    /// <item><description>"*": Star-sized column with default weight (1)</description></item>
    /// <item><description>"N*": Star-sized column with custom weight (e.g., "3*")</description></item>
    /// <item><description>Pixel values: Numeric values (e.g., "200")</description></item>
    /// </list>
    /// <example>
    /// "100, Auto, 3*"
    /// </example>
    /// </summary>
    public new string ColumnDefinitions
    {
        get => (string)GetValue(ColumnDefinitionsProperty);
        set => SetValue(ColumnDefinitionsProperty, value);
    }

    private static void OnRowDefinitionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FastGrid grid && e.NewValue is string definitions)
            grid.ParseRowDefinitions(definitions);
    }

    private static void OnColumnDefinitionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FastGrid grid && e.NewValue is string definitions)
            grid.ParseColumnDefinitions(definitions);
    }

    private void ParseRowDefinitions(string definitions)
    {
        base.RowDefinitions.Clear();
        if (string.IsNullOrWhiteSpace(definitions)) return;

        foreach (var  definition in definitions.Split(','))
        {
            var trimmed = definition.Trim();
            if (string.IsNullOrEmpty(trimmed)) continue;

            var row = new RowDefinition
            {
                Height = GetGridLength(trimmed)
            };
            
            base.RowDefinitions.Add(row);
        }
    }

    private void ParseColumnDefinitions(string definitions)
    {
        base.ColumnDefinitions.Clear();
        if (string.IsNullOrWhiteSpace(definitions)) return;

        foreach (var definition in definitions.Split(','))
        {
            var trimmed = definition.Trim();
            if (string.IsNullOrEmpty(trimmed)) continue;
            
            var column = new ColumnDefinition
            {
                Width = GetGridLength(trimmed)
            };

            base.ColumnDefinitions.Add(column);
        }
    }

    private GridLength GetGridLength(string definition)
    {
        if (definition.Equals(Auto, StringComparison.OrdinalIgnoreCase))
            return GridLength.Auto;
        if (definition == Star)
            return new GridLength(1, GridUnitType.Star);
        if (definition[^1] == StarChar)
            return GetStarDefinition(definition); 
        if (double.TryParse(definition, NumberStyles.Any, CultureInfo.InvariantCulture, out var pixelValue))
            return new GridLength(pixelValue);
        return GridLength.Auto;
    }

    private GridLength GetStarDefinition(string definition)
    {
        if (definition == Star)
            return GetDefaultStarLength();
        var value = definition.TrimEnd(StarChar);
        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var starValue))
            return new GridLength(starValue, GridUnitType.Star);
        return GetDefaultStarLength();
    }
    
    private GridLength GetDefaultStarLength() => new GridLength(1, GridUnitType.Star);
}