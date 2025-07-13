using MaterialDesignThemes.Wpf;
using MaterialZip.Factories.Abstractions;

namespace MaterialZip.Factories;

public class PaletteHelperFactory : IPaletteHelperFactory
{
    public PaletteHelper GetFactory()
    {
        return new PaletteHelper();
    }
}