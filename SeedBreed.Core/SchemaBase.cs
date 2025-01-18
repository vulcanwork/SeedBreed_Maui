using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SeedBreed.Core;
public abstract class SchemaBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
